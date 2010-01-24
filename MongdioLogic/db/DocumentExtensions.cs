using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using Procurios.Public;

namespace MongdioLogic.db
{
	public static class DocumentExtensions
	{
		public static Document Append2(this Document doc,string key,object value)
		{
			if(value != null)
			{
				doc.Append(key, value);
			}
				
			return doc;
		}

		public static Document Append2(this Document doc, string key, int? value)
		{
			if(value.HasValue)
			{
				doc.Append(key, value);
			}

			return doc;
		}

		public static Document Append2(this Document doc, string key, DateTime? value)
		{
			if(value.HasValue)
			{
				doc.Append(key, value);
			}

			return doc;
		}

		public static int GetInt(this Document doc, string key)
		{
			var i = (int) doc[key];
			return i;
		}
		public static int? GetNullableInt(this Document doc, string key)
		{
			object o = doc[key];
			if(o!=null)
				return (int) o;

			return null;
		}

		public static DateTime GetDate(this Document doc, string key)
		{
			var i = (DateTime)doc[key];
			return i;
		}
		public static DateTime? GetNullableDate(this Document doc, string key)
		{
			object o = doc[key];
			if(o != null)
				return (DateTime)o;

			return null;
		}

		public static string GetString(this Document doc, string key)
		{
			return (string)doc[key];
		}

		public static bool GetBool(this Document doc, string key)
		{
			return (bool)doc[key];
		}

		public static ICollection GetValues(this Document doc, string key)
		{
			var d = (Document)doc[key];
			if(d != null)
				return d.Values;
			return new ArrayList();
		}


		public static IEnumerable<T> GetValues<T>(this Document doc, string key)
		{
			var o = doc[key];
			if(o is Document)
				return new List<T>();

			var arr = (T[]) o;
			if(arr!=null)
				return arr;

			return new List<T>();
		}

		/*
		public static IEnumerable<T> GetValues<T>(this Document doc, string key)
		{
			//Get the values in key order
			var l = new List<T>();
			var d = (Document)doc[key];
			if(d != null)
			{
				foreach(string k in d.Keys)
				{
					var t = (T)d[k];
					l.Add(t);
				}
			}
			return l;
		}

		public static IEnumerable<T> GetComplexValues<T>(this Document doc, string key)
		{
			var d = (Document)doc[key];
			if(d != null)
				return d.Values.OfType<T>().Select(x => new T(x));
			return new List<T>();
		}
		*/

		//new Document().Append("query", new Document()).Append("orderby", new Document().Append(name:1).Append(age,-1))
		public static Document Sort(this Document doc, Document sort)
		{
			return new Document().Append("query", doc).Append("orderby", sort);
		}

		public static Document Eval(string function,params object[] args)
		{
			var d = new Document().Append("$eval", function);
			if(args.Count()>0)
			{
				//var argsd = args.Select(x=>new Document())

//				for(int i = 0; i < args.Count(); i++)
//				{
//					var o = args[i];
//
//				}
				d.Append("args", args);
			}
			return d;
		}

		public static List<object> ParseArray(string array)
		{
			var arr = JSON.JsonDecode(array);
			if(arr==null && arr is ArrayList)
				throw new ArgumentException("Unparsable arguments: " + array);
			var list = new List<object>();
			return (arr as ArrayList).OfType<object>().Select(x=>ConvertTo(x)).ToList();
		}

		public static Document Parse(string text)
		{
			var decoded = JSON.JsonDecode(text);
			if(text.Trim().Length > 0 && decoded == null)
				throw new Exception("Document unparsable '" + text + "'");
			var bson = ConvertTo(decoded);
			return bson as Document;
		}

		private static object ConvertTo(object v)
		{
			if(v is ArrayList)
			{
				var newArr = new List<object>();
				var val = v as ArrayList;
				foreach(var o in val)
				{
					newArr.Add(ConvertTo(o));
				}
				return newArr.ToArray();
			}
			else if(v is Hashtable)
			{
				var d = new Document();
				var vht = v as Hashtable;
				foreach(DictionaryEntry entry in vht)
				{
					d.Append(entry.Key.ToString(), ConvertTo(entry.Value));
				}
				return d;
			}
			else if(v is string)
			{
				string s = v.ToString();
				if(s.StartsWith("/") && (s.EndsWith("/") || s.EndsWith("/i") || s.EndsWith("/m")))
				{
					MongoRegex mr;
					if(s.EndsWith("/i") || s.EndsWith("/m"))
						mr = new MongoRegex(s.Substring(1,s.Length-3),s.Substring(s.Length-2,1));
					else
						mr = new MongoRegex(s.Substring(1, s.Length - 3));
					return mr;
				}
				else
					return v;
			}
			else
				return v;
		}

	}
}