using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Mongdio.code
{
	class RootNode : TreeNode
	{
		public RootNode(string text, ContextMenuStrip cms)
			: base(text)
		{
			ContextMenuStrip = cms;
		}
	}

	class DbNode : TreeNode
	{
		public DbNode(string text, ContextMenuStrip cms)
			: base(text)
		{
			ContextMenuStrip = cms;
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
	}

}
