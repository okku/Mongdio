using System;
using System.Collections;
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
		private string _lastCollectionUsed;

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
				_lastCollectionUsed = null;
				int take;
				string collectionName, operation, sort;
				string innerCommand = ParseCollectionAndOperation(command, out collectionName, out operation, out take, out sort);
				Document doc,sortDoc = null;
				switch(operation)
				{
					case "find":
						doc = DocumentExtensions.Parse(innerCommand);
						if(!string.IsNullOrEmpty(sort))
							sortDoc = DocumentExtensions.Parse(sort);
						using(var db = MDB.GetMongo())
						{
							var l = db[DataBaseName][collectionName].Find(doc, take, 0);
							if(sortDoc != null)
								l.Sort(sortDoc);
							objectCount = l.Documents.Count();
							_lastCollectionUsed = collectionName;
							return printer.Print(l.Documents);
						}
						break;
					case "findone":
						doc = DocumentExtensions.Parse(innerCommand);
						using(var db = MDB.GetMongo())
						{
							var l = db[DataBaseName][collectionName].FindOne(doc);
							objectCount = 1;
							_lastCollectionUsed = collectionName;
							return printer.Print(l);
						}
						break;
					case "update":
						var args = SplitIntoArguments(innerCommand);
						using(var db = MDB.GetMongo())
						{
							objectCount = 0;
							if(args.Count == 1 && args[0] is Document)
								UpdateObjectAlwaysUpsert(collectionName, args[0] as Document, db);
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
					case "eval":
						string function;
						List<object> oargs;
						if(!SplitIntoFunctionAndArguments(innerCommand, out function, out oargs))
							return "Parse error";
						using(var db = MDB.GetMongo())
						{
							if(oargs!=null && oargs.Count>0)
								doc = DocumentExtensions.Eval(function, oargs.ToArray());
							else
								doc = DocumentExtensions.Eval(function);
							var retVal = db[DataBaseName].SendCommand(doc);
							return printer.Print(retVal);
						}
						break;
					case "count":
						doc = DocumentExtensions.Parse(innerCommand);
						using(var db = MDB.GetMongo())
						{
							long count;
							if(doc != null)
								count = db[DataBaseName][collectionName].Count(doc);
							else
								count = db[DataBaseName][collectionName].Count();
							return "Count " + count;
						}
						break;
					case "delete":
						doc = DocumentExtensions.Parse(innerCommand);
						using(var db = MDB.GetMongo())
						{
							if(doc != null)
								db[DataBaseName][collectionName].Delete(doc);
							return "Sent delete command";
						}
						break;
					default:
						return "Unknown operation '" + operation + "'";
						break;
				}
			}
			catch(MongoCommandException e)
			{
				return e.Error.ToString();
			}
			catch(Exception e)
			{
				return e.Message;
			}
		}

		//If _id exists but has changed save new object
		private void UpdateObjectAlwaysUpsert(string collectionName, Document doc, Mong db)
		{
            if(doc.Contains("_id") && doc["_id"] != null)
            {
            	var selector = new Document();
				selector["_id"] = doc["_id"];
				db[DataBaseName][collectionName].Update(doc,selector,1);
            }   
			else
				db[DataBaseName][collectionName].Update(doc);
		}

		private bool SplitIntoFunctionAndArguments(string command, out string function, out List<object> args)
		{
			function = null;
			args = null;

			var p0 = command.IndexOf("function");
			if(p0 < 0)
				return false;

			int x = 0;
			bool ff = false;
			for(int i = p0; i < command.Length; i++)
			{
				var c = command[i];
				if(c == '{')
				{
					x++;
					ff = true;
				}
				else if(c == '}')
					x--;

				if(ff && x==0)
				{
					//Found function
					function = command.Substring(p0, i - p0 + 1);

					//Parse args
					var p1 = command.IndexOf(",", i);
					if(p1>0)
					{
						var s = command.Substring(p1 + 1);
						var fakeJSONArray = "[" + s + "]";
						var arr = DocumentExtensions.ParseArray(fakeJSONArray);
						args = arr;
					}
					return true;
				}
			}

			return false;
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

		private string ParseCollectionAndOperation(string command, out string name, out string operation, out int take, out string sort)
		{
			string content = null;
			take = 0;
			sort = null;

			var lccmd = command.ToLower();
			if(lccmd.StartsWith("db.eval(") || lccmd.StartsWith("eval("))
			{
				name = null;
				operation = "eval";
				var p0 = command.IndexOf("eval(");
				var p1 = command.LastIndexOf(")");
				content = command.Substring(p0 + 5, p1 - p0 - 5);
				return content;
			}

			var reLimit = new Regex(@"limit\((\d+)\)",RegexOptions.IgnoreCase);
			var lm = reLimit.Match(command);
			if(lm.Success)
			{
				take = int.Parse(lm.Groups[1].Value);
				command = reLimit.Replace(command,"");
			}

			var reSort = new Regex(@"sort\(([^\)]+)\)", RegexOptions.IgnoreCase);
			var sm = reSort.Match(command);
			if(sm.Success)
			{
				sort = sm.Groups[1].Value;
				command = reSort.Replace(command, "");
			}

			var re = new Regex(@"\s*(db\.)?([^\.]+)\.([^\(]+)\(");
			var m = re.Match(command);
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

		public bool TryParseTextAsDocument(string text, out bool containsId)
		{
			containsId = false;
			var dp = new DocumentParser();
			var d = dp.Parse(text) as Document;
			if(d != null)
				containsId = d.Contains("_id");
			return d != null;
		}

		public string SaveObject(string text)
		{
			var dp = new DocumentParser();
			var d = dp.Parse(text) as Document;
			if(d == null)
				return "No object";
			if(_lastCollectionUsed == null)
				return "No collection";

			using(var db = MDB.GetMongo())
			{
				UpdateObjectAlwaysUpsert(_lastCollectionUsed,d,db);
				return string.Format("Object saved in collection '{0}'", _lastCollectionUsed);
			}
		}

		public string DeleteObject(string text)
		{
			var dp = new DocumentParser();
			var d = dp.Parse(text) as Document;
			if(d == null)
				return "No object";
			if(_lastCollectionUsed == null)
				return "No collection";

			using(var db = MDB.GetMongo())
			{
				if(d.Contains("_id") && d["_id"] != null)
				{
					var selector = new Document();
					selector["_id"] = d["_id"];
					db[DataBaseName][_lastCollectionUsed].Delete(selector);
					return string.Format("Object with _id: {1} deleted in collection '{0}'", _lastCollectionUsed, d["_id"]);
				}   				
				return string.Format("Object contains no id");
			}
		}

	}
}
