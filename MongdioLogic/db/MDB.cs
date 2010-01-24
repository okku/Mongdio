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
	}
}