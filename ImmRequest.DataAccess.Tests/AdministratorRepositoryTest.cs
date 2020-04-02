using System.Linq;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Exceptions;
using ImmRequest.DataAccess.Repositories;
using ImmRequest.Domain.UserManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImmRequest.DataAccess.Tests
{
    [TestClass]
    public class AdministratorRepositoryTest
    {
        Administrator administrator;
        AdministratorRepository repository;
        ImmDbContext context;

        [TestInitialize]
        public void SetUp()
        {
            administrator = new Administrator
            {
                UserName = "Julie",
                PassWord = "1234"
            };
        }

        private void CreateRepostory(string name)
        {
            context = ContextFactory.GetMemoryContext(name);
            repository = new AdministratorRepository(context);
        }


        [TestMethod]
        public void InsertTest()
        {
            CreateRepostory("InsertTest");
            repository.Insert(administrator);
            var amountOfAdministrators = context.Administrators.Count();
            Assert.AreEqual(1, amountOfAdministrators);

        }

        [TestMethod]
        public void GetTest()
        {
            CreateRepostory("GetTest");
            context.Administrators.Add(administrator);
            context.SaveChanges();

            var administratorInDb = repository.Get(administrator.Id);
            Assert.IsNotNull(administratorInDb);
            Assert.AreEqual(administrator.UserName, administratorInDb.UserName);
        }

        [TestMethod]
        public void DeleteTest()
        {
            CreateRepostory("DeleteTest");
            context.Administrators.Add(administrator);
            context.SaveChanges();

            repository.Delete(administrator.Id);
            var deletedAdministrator = context.Administrators.FirstOrDefault();
            var administratorCount = context.Administrators.Count();
            Assert.IsNull(deletedAdministrator);
            Assert.AreEqual(0, administratorCount);
        }

        [TestMethod]
        public void UpdateTest()
        {
            CreateRepostory("UpdateTest");
            context.Administrators.Add(administrator);
            context.SaveChanges();

            administrator.UserName = "Juliette";
            repository.Update(administrator);

            var updatedAdministrator = context.Administrators.FirstOrDefault();
            Assert.AreEqual("Juliette", updatedAdministrator.UserName);
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseNotFoundException))]
        public void DeleteNonExistantAdministratorTest()
        {
            CreateRepostory("DeleteNotFound");
            repository.Delete(administrator.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseNotFoundException))]
        public void UpdateNotFoundTest()
        {
            CreateRepostory("UpdateNotFound");
            repository.Update(administrator);
        }

        [TestMethod]
        public void GetAllTest()
        {
            CreateRepostory("GetAllTest");
            var otherAdministrator = new Administrator
            {
                UserName = "Francisco",
                PassWord = "6543210"
            };
            context.Administrators.Add(otherAdministrator);
            context.Administrators.Add(administrator);
            context.SaveChanges();

            var allAdministrators = repository.GetAll();
            Assert.AreEqual(2, allAdministrators);

        }
    }
}
