using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MongdioLogic.db;
using MongoDB.Driver;
using Procurios.Public;

namespace MongdioLogic
{
	public class MongoEditorSession
	{
		public string DataBaseName { get; set; }
		public MongoEditorSession(string dbname)
		{
			DataBaseName = dbname;
		}

		public string Execute(string command, out int objectCount, IDocumentPrettyPrint printer)
		{
			objectCount = 0; 
			try
			{
				string collectionName, operation;
				string innerCommand = ParseCollectionAndOperation(command, out collectionName, out operation);
				Document doc;
				switch(operation)
				{
					case "find":
						doc = DocumentExtensions.Parse(innerCommand);
						using(var db = MDB.GetMongo())
						{
							var l = db[DataBaseName][collectionName].Find(doc);
							objectCount = l.Documents.Count();
							return printer.Print(l.Documents);
						}
						break;
					case "findone":
						doc = DocumentExtensions.Parse(innerCommand);
						using(var db = MDB.GetMongo())
						{
							var l = db[DataBaseName][collectionName].FindOne(doc);
							objectCount = 1;
							return printer.Print(l);
						}
						break;
					case "update":
						var args = SplitIntoArguments(innerCommand);
						using(var db = MDB.GetMongo())
						{
							objectCount = 0;
							if(args.Count == 1 && args[0] is Document)
								db[DataBaseName][collectionName].Update(args[0] as Document);
							else if(args.Count == 2 && args[0] is Document && args[1] is Document)
								db[DataBaseName][collectionName].Update(args[1] as Document, args[0] as Document);
							else if(args.Count == 3 && args[0] is Document && args[1] is Document && (args[2] is double || args[2] is bool))
							{
								int upsert = GetIntboolFromDoubleOrBool(args[2]);
								db[DataBaseName][collectionName].Update(args[1] as Document, args[0] as Document, upsert);
							}
							else if(args.Count == 4 && args[0] is Document && args[1] is Document && (args[2] is double || args[2] is bool) 
								&& (args[3] is double || args[3] is bool))
							{
								int multi = GetIntboolFromDoubleOrBool(args[3]);
								if(multi == 1)
									db[DataBaseName][collectionName].UpdateAll(args[1] as Document, args[0] as Document);
								else
								{
									int upsert = GetIntboolFromDoubleOrBool(args[2]);
									db[DataBaseName][collectionName].Update(args[1] as Document, args[0] as Document, upsert);
								}
							}
							else
								return "Syntax update(doc) or update(sel, doc, [upsert 1 or 0, [multi 1 or 0]])";

							return "Sent update command";
						}
						break;
					default:
						return "Unknown operation '" + operation + "'";
						break;
				}
				//var doc = db["cl"].SendCommand(command);
				//return doc.ToString();
			}
			catch(MongoCommandException e)
			{
				return e.Error.ToString();
			}
			catch(Exception e)
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

		private int GetIntboolFromDoubleOrBool(object args)
		{
			int upsert = 0;
			if(args is double)
				upsert = (int) ((double)args);
			else
				upsert = (bool)args?1:0;
			return upsert;
		}

		private List<object> SplitIntoArguments(string s)
		{
			var fakeJSONArray = "[" + s + "]";
			var arr = DocumentExtensions.ParseArray(fakeJSONArray);
			return arr;
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
