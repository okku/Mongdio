using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongdioLogic.db;
using MongoDB.Driver;
using NUnit.Framework;

namespace MongdioLogic.test
{
	[TestFixture]
	public class TestParse
	{
		[Test]
		public void TestNu()
		{
			//var d = DocumentExtensions.Parse("[1,2,3]");
			//var d = DocumentExtensions.Parse("{\"top\":1}");
			var d = DocumentExtensions.Parse("{\"objects\":[1,{\"top\":1},3]}");
			Assert.That(d, Is.Not.Null);
			Assert.That(d["objects"], Is.Not.Null);
		}
	}
}
