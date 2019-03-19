using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quack;
using Quack.Controllers;
using Quack.Models;

namespace Quack.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void TestUserRegistration()
        {
            // Arrange
            UserModel model = new UserModel();

            // Act
            int? userID = model.Register("fakeemail@test.com", "fakePass", "fakeName");

            // Assert
            Assert.IsNotNull(userID);
            Assert.IsTrue(userID > 0);

            // Cleanup (Ideally, would use mock db instance and avoid (like the plague) real data manipulation on same db as the live app )
            int rowsDeleted = model.DeleteUser(userID);
            Assert.AreEqual(1, rowsDeleted);
        }
    }
}
