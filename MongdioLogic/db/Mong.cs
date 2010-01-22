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
		public const string DB_USERS = "users";
		public const string DB_ARTISTS = "artists";
		public const string DB_ALBUMS = "albums";
		public const string DB_SONGS = "songs";
		public const string DB_PLAYLISTS = "pls";
		public const string DB_STREAMEVENTS = "strevs";
		public const string DB_PLAYEVENTS = "plevs";
		public const string DB_TAGARTISTS = "tagarts";

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

		public IMongoCollection Users
		{
			get { return this[DBN][DB_USERS]; }
		}
		
		public IMongoCollection Artists
		{
			get { return this[DBN][DB_ARTISTS]; }
		}

		public IMongoCollection Albums
		{
			get { return this[DBN][DB_ALBUMS]; }
		}

		public IMongoCollection Songs
		{
			get { return this[DBN][DB_SONGS]; }
		}

		public IMongoCollection Playlists
		{
			get { return this[DBN][DB_PLAYLISTS]; }
		}

		public IMongoCollection StreamEvents
		{
			get { return this[DBN][DB_STREAMEVENTS]; }
		}

		public IMongoCollection PlayEvents
		{
			get { return this[DBN][DB_PLAYEVENTS]; }
		}

		public IMongoCollection TagArtists
		{
			get { return this[DBN][DB_TAGARTISTS]; }
		}
	
		
	}
}