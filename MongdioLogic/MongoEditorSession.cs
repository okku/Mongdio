using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MongdioLogic.db;
using MongoDB.Driver;

namespace MongdioLogic
{
	public class MongoEditorSession
	{
		public string Execute(string command)
		{
			try
			{
				string collectionName, operation;
				string innerCommand = ParseCollectionAndOperation(command, out collectionName, out operation);
				switch(operation)
				{
					case "find":
						var doc = DocumentExtensions.Parse(innerCommand);
						break;
				}
				//var doc = db["cl"].SendCommand(command);
				//return doc.ToString();
			}
			catch(MongoCommandException e)
			{
				return e.Error.ToString();
			}
			catch(ArgumentException e)
			{
				return e.Message;
			}
			/*			if(command.StartsWith("find"))
						{
							var doc = GetQueryCommand(command);
							using(var db = MDB.GetMongo())
							{
								try
								{
									db["cl"]["pls"].Find(doc);
									return doc.ToString();
								}
								catch(MongoCommandException e)
								{
									return e.Error.ToString();
								}
							}

						}
						using(var db = MDB.GetMongo())
						{

						}*/
			return "";
		}

		private string ParseCollectionAndOperation(string command, out string name, out string operation)
		{
			var re = new Regex(@"\s*(db\.)?([^\.]+)\.([^\(]+)\(");
			var m = re.Match(command);
			string content = null;
			if(m.Success)
			{
				name = m.Groups[2].Value;
				operation = m.Groups[3].Value.ToLower();
				var p1 = command.LastIndexOf(")");
				content = command.Substring(m.Length, p1 - m.Length);
				return content;
			}

			name = null;
			operation = null;
			return null;
		}

		private Document GetQueryCommand(string command)
		{
			return null;
		}
	}
}
