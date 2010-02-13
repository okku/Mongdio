using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MongoDB.Driver;

namespace Mongdio.code
{
	class RootNode : TreeNode
	{
		public RootNode(string text, ContextMenuStrip cms)
			: base(text)
		{
			ContextMenuStrip = cms;
			ImageIndex = 1;
			SelectedImageIndex = 1;
		}
	}

	class DbNode : TreeNode
	{
		public DbNode(string text, ContextMenuStrip cms)
			: base(text)
		{
			ContextMenuStrip = cms;
			ImageIndex = 0;
			SelectedImageIndex = 0;
		}

		public string DbName
		{
			get { return Text; }
		}
	}

	class CollectionNode : TreeNode
	{
		public CollectionNode(string text, ContextMenuStrip cms)
			: base(text)
		{
			ContextMenuStrip = cms;
			ImageIndex = 2;
			SelectedImageIndex = 2;
		}

		public string CollectionNamespace
		{
			get
			{
				return Text;
			}
		}

		public string CollectionName
		{
			get 
			{
				var p0 = Text.IndexOf(".");
				if(p0 >= 0)
					return Text.Substring(p0+1);

				return Text;
			}
		}

		public DbNode DbNode
		{
			get { return (DbNode) Parent; }
		}
	}

	class IndexNode : TreeNode
	{
		private Document _doc;

		public IndexNode(Document document, ContextMenuStrip cms)
		{
			ContextMenuStrip = cms;
			ImageIndex = 3;
			SelectedImageIndex = 3;

			_doc = document;
			var keys = (Document) _doc["key"];
			Text = String.Join(",", keys.Keys.OfType<string>().ToArray());
		}

		public string NiceIndexName
		{
			get { return Text; }
		}

		public string IndexName
		{
			get { return _doc["name"].ToString(); }
		}

		public Document Key
		{
			get { return (Document) _doc["key"]; }
		}
	}

}
