using System.Linq;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces.Exceptions;
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
                Password = "1234"
            };
        }

        [TestCleanup]
        public void CleanUp()
        {
            context.Dispose();
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
                Password = "6543210"
            };
            context.Administrators.Add(otherAdministrator);
            context.Administrators.Add(administrator);
            context.SaveChanges();

            var allAdministrators = repository.GetAll();
            Assert.AreEqual(2, allAdministrators.Count);

        }

        [TestMethod]
        public void ExistsTest()
        {
            CreateRepostory("Exists");
            var administrator2 = new Administrator {
                Email = administrator.Email
            };
            context.Administrators.Add(administrator2);
            context.SaveChanges();

            var exists = repository.Exists(administrator);
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void DoesNotExistsTest()
        {
            CreateRepostory("Does not Exist");
            var exists = repository.Exists(administrator);
            Assert.IsFalse(exists);
        }
    }
}
