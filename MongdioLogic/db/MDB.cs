using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using log4net;
using MongoDB.Driver;

namespace MongdioLogic.db
{
	public class MDB
	{
		static ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public static Mong GetMongo()
		{
			var db = new Mong();
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
	}
}