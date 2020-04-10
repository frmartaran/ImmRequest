using ImmRequest.Domain.UserManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImmRequest.BusinessLogic.Tests
{
    [TestClass]
    public class AdministratorValidatorTest
    {
        Administrator administrator;

        [TestInitialize]
        public void SetUp()
        {
            administrator = new Administrator
            {
                UserName = "왕 잭슨",
                Password = "Aite Aite",
            };
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
