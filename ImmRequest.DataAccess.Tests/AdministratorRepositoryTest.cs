using System.Linq;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Repositories;
using ImmRequest.Domain.UserManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImmRequest.DataAccess.Tests
{
    [TestClass]
    public class AdministratorRepositoryTest
    {

        [TestMethod]
        public void InsertTest()
        {
            var context = ContextFactory.GetMemoryContext("InsertTest");
            var repository = new AdministratorRepository(context);

            var administrator = new Administrator
            {
                UserName = "Julie",
                PassWord = "1234"
            };

            repository.Insert(administrator);
            var amountOfAdministrators = context.Administrators.Count();
            Assert.AreEqual(1, amountOfAdministrators);

        }
    }
}
