using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Helpers;
using ImmRequest.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.DataAccess.Tests
{
    [TestClass]
    public class FinderTests
    {
        [TestMethod]
        public void FindAreaByNameTest()
        {
            var context = ContextFactory.GetMemoryContext("Find Area");
            var area = new Area { Name = "Name" };
            context.Areas.Add(area);
            context.SaveChanges();

            var finder = new DatabaseFinder(context);
            var areaFound = finder.Find<Area>(a => a.Name == "Name");
            Assert.IsNotNull(area);
        }
    }
}
