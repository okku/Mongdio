using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using log4net;
using MongoDB.Driver;

namespace MongdioLogic.db
{
	public class MDB
	{
		private const string ADMIN = "admin";
		static ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public static Mong GetMongo()
		{
			string host = ConfigurationManager.AppSettings["connection_host"];
			string port = ConfigurationManager.AppSettings["connection_port"];
			var db = new Mong(host,int.Parse(port));
			try
			{
				db.Connect();
			}
			catch(Exception e)
			{
				logger.Error("Unable to connect to MongoDB");
				throw;
			}
			return db;
		}

		public static List<Document> GetDatabases()
		{
			using(var db = GetMongo())
			{
				var cmd = new Document().Append("listDatabases", 1);
				var d = db[ADMIN].SendCommand(cmd);
				if(d!=null && d["databases"] != null)
				{
					var dbs = d["databases"] as Document[];
					if(dbs==null)
						return null;

					return dbs.ToList();
				}
				return null;
			}
		}

		public static List<Document> GetCollections(string dbName)
		{
			using(var db = GetMongo())
			{
				var cmd = new Document().Sort(new Document().Append("name", 1));
				var d = db[dbName]["system.namespaces"].Find(cmd);
				if(d != null)
					return d.Documents.ToList();
				return null;
			}
		}

		public static bool DropCollection(string dbName,string collectionName)
		{
			using(var db = GetMongo())
			{
				var cmd = new Document().Append("drop", collectionName);
				var d = db[dbName].SendCommand(cmd);
				if(d != null)
					return (double) d["ok"]==1;
				return false;
			}
		}

		public static bool DropDatabase(string dbName)
		{
			using(var db = GetMongo())
			{
				var cmd = new Document().Append("dropDatabase", 1);
				var d = db[dbName].SendCommand(cmd);
				if(d != null)
					return (double)d["ok"] == 1;
				return false;
			}
		}

		public static void CreateCollection(string dbName, string collectionName)
		{
			using(var db = GetMongo())
			{
				db[dbName][collectionName].Update(new Document().Append("key", 1));
				db[dbName][collectionName].Delete(new Document().Append("key", 1));
			}
		}

		public static bool CreateDatabase(string dbName)
		{
			using(var db = GetMongo())
			{
				db[dbName]["_dummy"].Update(new Document().Append("key", 1));
			}
			DropCollection(dbName, "_dummy");
			return true;
		}

		public static List<Document> GetIndexsByNS(string dbName,string collectionNamespace)
		{
			using(var db = GetMongo())
			{
				var cmd = new Document().Append("ns", collectionNamespace);
				return db[dbName]["system.indexes"].Find(cmd).Documents.ToList();
			}
		}

		public static Document CreateIndexes(string dbName, string collectionNamespace, string pattern)
		{
			using(var db = GetMongo())
			{
				var dp = new DocumentParser();
				var d = dp.Parse(pattern) as Document;
				if(d == null)
					throw new ArgumentException("Illegal pattern");

				var name = d.ToLine();
				var cmd = new Document().Append("name",name).Append("ns", collectionNamespace).Append("key",d);
				db[dbName]["system.indexes"].Insert(cmd);

				cmd = new Document().Append("name", name);
				return db[dbName]["system.indexes"].FindOne(cmd);
			}
		}

		public static void DropIndex(string dbName, string collectionName, Document key)
		{
			using(var db = GetMongo())
			{
				//var cmd = new Document().Append("name", name).Append("ns", collectionNamespace);
				//db[dbName]["system.indexes"].Delete(cmd);
				var cmd = new Document().Append("deleteIndexes", collectionName).Append("index", key);
				db[dbName].SendCommand(cmd);
			}
		}

	}
}