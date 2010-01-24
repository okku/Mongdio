using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;

namespace MongdioLogic.db
{
	public class Mong : Mongo, IDisposable
	{
		public const string DBN = "cl";

		public Mong()
		{
		}

		public Mong(string host, int port) : base(host, port)
		{
		}

		public void Dispose()
		{
			Disconnect();
		}

		public double EvalToDouble(string function,params object[] args)
		{
			return (double) Eval(function, args);
		}

		public Document EvalToDocument(string function, params object[] args)
		{
			var d= Eval(function, args);
			if(d is MongoDBNull)
				return null;
			return (Document) d;
		}

//		public T Eval<T>(string function, params object[] args) where T : new()
//		{
//			var d = Eval(function, args);
//			if(d is MongoDBNull)
//				return default(T);
//			return new T((Document) d);
//		}

		private object Eval(string function, params object[] args)
		{
			var doc = DocumentExtensions.Eval(function, args);
			var retVal = this[DBN].SendCommand(doc);
			return retVal["retval"];
		}
		
	}
}