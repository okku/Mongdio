using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using MongoDB.Driver;

namespace MongdioLogic.db
{
	public class DocumentParser
	{
		public const int TOKEN_NONE = 0;
		public const int TOKEN_CURLY_OPEN = 1;
		public const int TOKEN_CURLY_CLOSE = 2;
		public const int TOKEN_SQUARED_OPEN = 3;
		public const int TOKEN_SQUARED_CLOSE = 4;
		public const int TOKEN_COLON = 5;
		public const int TOKEN_COMMA = 6;
		public const int TOKEN_STRING = 7;
		public const int TOKEN_NUMBER = 8;
		public const int TOKEN_TRUE = 9;
		public const int TOKEN_FALSE = 10;
		public const int TOKEN_NULL = 11;
		public const int TOKEN_OID = 12;
		public const int TOKEN_REGEX = 13;

		private const int BUILDER_CAPACITY = 2000;

		/// <summary>
		/// On decoding, this value holds the position at which the parse failed (-1 = no error).
		/// </summary>
		protected int lastErrorIndex = -1;
		protected string lastDecode = "";

		/// <summary>
		/// Parses the string json into a value
		/// </summary>
		/// <param name="json">A JSON string.</param>
		/// <returns>An List<object>, a Document, Oid, a double, a string, null, true, or false</returns>
		public object Parse(string json)
		{
			try
			{
				// save the string for debug information
				lastDecode = json;

				if(json != null)
				{
					char[] charArray = json.ToCharArray();
					int index = 0;
					bool success = true;
					object value = ParseValue(charArray, ref index, ref success);
					if(success)
					{
						lastErrorIndex = -1;
					}
					else
					{
						lastErrorIndex = index;
					}
					return value;
				}
				else
				{
					return null;
				}
			}
			catch(Exception e)
			{
				return null;
			}
		}

		/// <summary>
		/// On decoding, this function returns the position at which the parse failed (-1 = no error).
		/// </summary>
		/// <returns></returns>
		public bool LastDecodeSuccessful()
		{
			return lastErrorIndex == -1;
		}

		/// <summary>
		/// On decoding, this function returns the position at which the parse failed (-1 = no error).
		/// </summary>
		/// <returns></returns>
		public int GetLastErrorIndex()
		{
			return lastErrorIndex;
		}

		/// <summary>
		/// If a decoding error occurred, this function returns a piece of the JSON string 
		/// at which the error took place. To ease debugging.
		/// </summary>
		/// <returns></returns>
		public string GetLastErrorSnippet()
		{
			if(lastErrorIndex == -1)
			{
				return "";
			}
			else
			{
				int startIndex = lastErrorIndex - 5;
				int endIndex = lastErrorIndex + 15;
				if(startIndex < 0)
				{
					startIndex = 0;
				}
				if(endIndex >= lastDecode.Length)
				{
					endIndex = lastDecode.Length - 1;
				}

				return lastDecode.Substring(startIndex, endIndex - startIndex + 1);
			}
		}

		protected Document ParseObject(char[] json, ref int index)
		{
			Document doc = new Document();
			int token;

			// {
			NextToken(json, ref index);

			bool done = false;
			while(!done)
			{
				token = LookAhead(json, index);
				//				if (token == TOKEN_NONE) {
				//					return null;
				//				} else 
				if(token == TOKEN_COMMA)
				{
					NextToken(json, ref index);
				}
				else if(token == TOKEN_CURLY_CLOSE)
				{
					NextToken(json, ref index);
					return doc;
				}
				else
				{
					// name
					string name = ParseString(json, ref index, true);
					if(name == null)
					{
						return null;
					}

					// :
					token = NextToken(json, ref index);
					if(token != TOKEN_COLON)
					{
						return null;
					}

					// value
					bool success = true;
					object value = ParseValue(json, ref index, ref success);
					if(!success)
					{
						return null;
					}

					doc.Add(name, value);
				}
			}

			return doc;
		}

		protected List<object> ParseArray(char[] json, ref int index)
		{
			var array = new List<object>();

			// [
			NextToken(json, ref index);

			bool done = false;
			while(!done)
			{
				int token = LookAhead(json, index);
				if(token == TOKEN_NONE)
				{
					return null;
				}
				else if(token == TOKEN_COMMA)
				{
					NextToken(json, ref index);
				}
				else if(token == TOKEN_SQUARED_CLOSE)
				{
					NextToken(json, ref index);
					break;
				}
				else
				{
					bool success = true;
					object value = ParseValue(json, ref index, ref success);
					if(!success)
					{
						return null;
					}

					array.Add(value);
				}
			}

			return array;
		}

		protected object ParseValue(char[] json, ref int index, ref bool success)
		{
			switch(LookAhead(json, index))
			{
				case TOKEN_STRING:
					return ParseStringOrDate(json, ref index);
				case TOKEN_NUMBER:
					var d = ParseNumber(json, ref index);
					return d.HasValue ? (object) d.Value : null;
				case TOKEN_CURLY_OPEN:
					return ParseObject(json, ref index);
				case TOKEN_SQUARED_OPEN:
					return ParseArray(json, ref index);
				case TOKEN_TRUE:
					NextToken(json, ref index);
					return Boolean.Parse("TRUE");
				case TOKEN_FALSE:
					NextToken(json, ref index);
					return Boolean.Parse("FALSE");
				case TOKEN_NULL:
					NextToken(json, ref index);
					return null;
				case TOKEN_OID:
					NextToken(json, ref index);
					return ParseOID(json, ref index);
					break;
				case TOKEN_REGEX:
					return ParseRegEx(json, ref index);
					break;
				case TOKEN_NONE:
					break;
			}

			success = false;
			return null;
		}

		private object ParseStringOrDate(char[] json, ref int index)
		{
			var s = ParseString(json, ref index, false);
			if(s==null)
				return null;

			DateTime date;
			if(s.IndexOf("T")>=0 && s.EndsWith("Z") && DateTime.TryParse(s, out date))
				return date;

			return s;
		}

		private MongoRegex ParseRegEx(char[] json, ref int index)
		{
			EatWhitespace(json, ref index);

			var c = json[index++];
			if(c != '/')
				return null;

			var lastIndex = GetLastIndexOfRegEx(json, index);
			int charLength = lastIndex - index;
			char[] charArray = new char[charLength];
			Array.Copy(json, index, charArray, 0, charLength);
			var mr = new MongoRegex(new string(charArray));

			index = lastIndex+1;

			if(index < json.Length)
			{
				if(json[index] == 'i' || json[index] == 'm' || json[index] == 'g')
				{
					mr.Options = json[index].ToString();
					index++;
				}
			}

			return mr;
		}

		private object ParseOID(char[] json, ref int index)
		{
			var s = ParseString(json, ref index, false);
			if(s == null || index == json.Length)
				return null;

			EatWhitespace(json, ref index);
			if(index == json.Length)
				return null;

			var c = json[index++];
			if(c != ')')
				return null;

			return new Oid(s);
		}

		protected string ParseString(char[] json, ref int index, bool allowNonQutedString)
		{
			string s = "";
			char c;

			EatWhitespace(json, ref index);

			// "
			c = json[index++];
			if(allowNonQutedString && c != '"')
				index--;

			bool complete = false;
			while(!complete)
			{

				if(index == json.Length)
				{
					break;
				}

				c = json[index++];
				if(c == '"' || (allowNonQutedString && (c == ':' || c == ' ')))
				{
					if(c == ':')
						index--;
					complete = true;
					break;
				}
				else if(c == '\\')
				{

					if(index == json.Length)
					{
						break;
					}
					c = json[index++];
					if(c == '"')
					{
						s += '"';
					}
					else if(c == '\\')
					{
						s += '\\';
					}
					else if(c == '/')
					{
						s += '/';
					}
					else if(c == 'b')
					{
						s += '\b';
					}
					else if(c == 'f')
					{
						s += '\f';
					}
					else if(c == 'n')
					{
						s += '\n';
					}
					else if(c == 'r')
					{
						s += '\r';
					}
					else if(c == 't')
					{
						s += '\t';
					}
					else if(c == 'u')
					{
						int remainingLength = json.Length - index;
						if(remainingLength >= 4)
						{
							// fetch the next 4 chars
							char[] unicodeCharArray = new char[4];
							Array.Copy(json, index, unicodeCharArray, 0, 4);
							// parse the 32 bit hex into an integer codepoint
							uint codePoint = UInt32.Parse(new string(unicodeCharArray), NumberStyles.HexNumber);
							// convert the integer codepoint to a unicode char and add to string
							s += Char.ConvertFromUtf32((int)codePoint);
							// skip 4 chars
							index += 4;
						}
						else
						{
							break;
						}
					}

				}
				else
				{
					s += c;
				}

			}

			if(!complete)
			{
				return null;
			}

			return s;
		}

		protected double? ParseNumber(char[] json, ref int index)
		{
			EatWhitespace(json, ref index);

			int lastIndex = GetLastIndexOfNumber(json, index);
			int charLength = (lastIndex - index) + 1;
			char[] numberCharArray = new char[charLength];

			Array.Copy(json, index, numberCharArray, 0, charLength);
			index = lastIndex + 1;
			double d;
			if(double.TryParse(new string(numberCharArray), NumberStyles.Any, CultureInfo.InvariantCulture, out d))
				return d;

			return null;
			//return Double.Parse(new string(numberCharArray), CultureInfo.InvariantCulture);
		}

		protected int GetLastIndexOfNumber(char[] json, int index)
		{
			int lastIndex;
			for(lastIndex = index; lastIndex < json.Length; lastIndex++)
			{
				if("0123456789+-.eE".IndexOf(json[lastIndex]) == -1)
				{
					break;
				}
			}
			return lastIndex - 1;
		}

		protected int GetLastIndexOfRegEx(char[] json, int index)
		{
			int lastIndex;
			for(lastIndex = index; lastIndex < json.Length; lastIndex++)
			{
				if( json[lastIndex] == '/' )
					break;
			}
			return lastIndex;
		}

		protected void EatWhitespace(char[] json, ref int index)
		{
			for(; index < json.Length; index++)
			{
				if(" \t\n\r".IndexOf(json[index]) == -1)
				{
					break;
				}
			}
		}

		protected int LookAhead(char[] json, int index)
		{
			int saveIndex = index;
			return NextToken(json, ref saveIndex);
		}

		protected int NextToken(char[] json, ref int index)
		{
			EatWhitespace(json, ref index);

			if(index == json.Length)
			{
				return TOKEN_NONE;
			}

			char c = json[index];
			index++;
			switch(c)
			{
				case '{':
					return TOKEN_CURLY_OPEN;
				case '}':
					return TOKEN_CURLY_CLOSE;
				case '[':
					return TOKEN_SQUARED_OPEN;
				case ']':
					return TOKEN_SQUARED_CLOSE;
				case ',':
					return TOKEN_COMMA;
				case '"':
					return TOKEN_STRING;
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
				case '-':
					return TOKEN_NUMBER;
				case ':':
					return TOKEN_COLON;
				case '/':
					return TOKEN_REGEX;
			}
			index--;

			int remainingLength = json.Length - index;

			// false
			if(remainingLength >= 5)
			{
				if(json[index] == 'f' &&
					json[index + 1] == 'a' &&
					json[index + 2] == 'l' &&
					json[index + 3] == 's' &&
					json[index + 4] == 'e')
				{
					index += 5;
					return TOKEN_FALSE;
				}
			}

			// true
			if(remainingLength >= 4)
			{
				if(json[index] == 't' &&
					json[index + 1] == 'r' &&
					json[index + 2] == 'u' &&
					json[index + 3] == 'e')
				{
					index += 4;
					return TOKEN_TRUE;
				}
			}

			// null
			if(remainingLength >= 4)
			{
				if(json[index] == 'n' &&
					json[index + 1] == 'u' &&
					json[index + 2] == 'l' &&
					json[index + 3] == 'l')
				{
					index += 4;
					return TOKEN_NULL;
				}
			}

			// ObjectId(
			if(remainingLength >= 9)
			{
				if(json[index] == 'O' &&
					json[index + 1] == 'b' &&
					json[index + 2] == 'j' &&
					json[index + 3] == 'e' &&
					json[index + 4] == 'c' &&
					json[index + 5] == 't' &&
					json[index + 6] == 'I' &&
					json[index + 7] == 'd' &&
					json[index + 8] == '(')
				{
					index += 9;
					return TOKEN_OID;
				}
			}

			return TOKEN_NONE;
		}
	}
}
