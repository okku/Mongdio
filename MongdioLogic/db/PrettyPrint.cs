using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using MongoDB.Driver;

namespace MongdioLogic.db
{
	public interface IDocumentPrettyPrint
	{
		string Print(Document doc);
		string Print(IEnumerable<Document> documents);
	}

	public class PrettyPrint : IDocumentPrettyPrint
	{
		public string Print(Document doc)
		{
			var json = new StringBuilder();
			PPSerializeType(doc, json, "");
			return json.ToString();
		}

		public string Print(IEnumerable<Document> documents)
		{
			var json = new StringBuilder();
			foreach(var doc in documents)
			{
				PPSerializeType(doc, json, "");
				json.Append(Environment.NewLine);
			}
			return json.ToString();
		}

		private const string INDENT = "    ";
		private static void PPSerializeType(object value, StringBuilder json, string indent)
		{
			if(value == null)
			{
				json.Append("null");
				return;
			}
			var t = value.GetType();
			if(value is bool)
			{
				json.Append(((bool)value) ? "true" : "false");
			}
			else if(t.IsArray)
			{
				json.Append("[ ");
				json.Append(Environment.NewLine);
				indent += INDENT;
				bool first = true;
				foreach(var v in (Array)value)
				{
					if(first)
						first = false;
					else
					{
						json.Append(",");
						json.Append(Environment.NewLine);
					}
					json.Append(indent);
					PPSerializeType(v, json, indent);
				}
				indent = indent.Substring(0, indent.Length - INDENT.Length);
				json.AppendFormat("{1}{0}]", indent, Environment.NewLine);
			}
			else if(value is Document)
			{
				var doc = value as Document;
				json.AppendFormat("{{{1}", indent, Environment.NewLine);
				indent += INDENT;
				bool first = true;
				foreach(String key in doc.Keys)
				{
					if(first)
						first = false;
					else
					{
						json.Append(",");
						json.Append(Environment.NewLine);
					}
					json.AppendFormat(@"{1}""{0}"": ", key, indent);
					PPSerializeType(doc[key], json, indent);
				}
				indent = indent.Substring(0, indent.Length - INDENT.Length);
				json.AppendFormat("{1}{0}}}", indent, Environment.NewLine);
			}
			else if(value is Oid ||
			  value is int ||
			  value is Int32 ||
			  value is long)
			{
				json.Append(value);
			}
			else if( value is float || value is double)
			{
				var s = string.Format(CultureInfo.InvariantCulture, "{0}", value);
				if(!s.Contains("."))
					s += ".0";
				json.Append(s);
			}
			else if(value is DateTime)
			{
				json.AppendFormat(@"""{0}""", ((DateTime)value).ToUniversalTime().ToString("o"));
			}
			else
			{
				json.AppendFormat(@"""{0}""", value);
			}
			return;
		}
	}

	public class RTFPrettyPrinter : IDocumentPrettyPrint
	{
		//new line \par
		//{ \{
		//} \}
		private const string NEWLINE = "\\par";
		private const string INDENT = "    ";

		public string Print(Document doc)
		{
			var json = new StringBuilder();
			PPSerializeType(doc, json, "");
			return json.ToString();
		}

		public string Print(IEnumerable<Document> documents)
		{
			var json = new StringBuilder();
			foreach(var doc in documents)
			{
				PPSerializeType(doc, json, "");
				json.Append(NEWLINE);
			}
			return json.ToString();
		}

		private static void PPSerializeType(object value, StringBuilder json, string indent)
		{
			if(value == null)
			{
				json.Append("null");
				return;
			}
			var t = value.GetType();
			if(value is bool)
			{
				json.Append(((bool)value) ? "\\cf3 true" : "\\cf3 false");
			}
			else if(t.IsArray)
			{
				json.Append("\\cf0 [ ");
				json.Append(NEWLINE);
				indent += INDENT;
				bool first = true;
				foreach(var v in (Array)value)
				{
					if(first)
						first = false;
					else
					{
						json.Append("\\cf0 ,");
						json.Append(NEWLINE);
					}
					json.Append(indent);
					PPSerializeType(v, json, indent);
				}
				indent = indent.Substring(0, indent.Length - INDENT.Length);
				json.AppendFormat("{1}{0}\\cf0 ]", indent, NEWLINE);
			}
			else if(value is Document)
			{
				var doc = value as Document;
				json.AppendFormat("\\cf0 \\{{{1}", indent, NEWLINE);
				indent += INDENT;
				bool first = true;
				foreach(String key in doc.Keys)
				{
					if(first)
						first = false;
					else
					{
						json.Append("\\cf0 ,");
						json.Append(NEWLINE);
					}
					json.AppendFormat(@"{1}""{{\b {0}}}"": ", key, indent);
					PPSerializeType(doc[key], json, indent);
				}
				indent = indent.Substring(0, indent.Length - INDENT.Length);
				json.AppendFormat("{1}{0}\\cf0 \\}}", indent, NEWLINE);
			}
			else if(value is Oid ||
			  value is int ||
			  value is Int32 ||
			  value is long)
			{
				json.AppendFormat(@"\cf3 {0}", value);
			}
			else if(value is float || value is double)
			{
				var s = string.Format(CultureInfo.InvariantCulture, "{0}", value);
				if(!s.Contains("."))
					s += ".0";
				json.AppendFormat(@"\cf3 {0}", s);
			}
			else if(value is DateTime)
			{
				json.AppendFormat(@"\cf3 ""{0}""", ((DateTime)value).ToUniversalTime().ToString("o"));
			}
			else
			{
				json.AppendFormat(@"\cf1 ""{0}""", value);
			}
			return;
		}

	}
}
