using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeBeatStreet.Pages;
using NeBeatStreet.AppData;
using System.Data.Entity;
using System.Windows; 
using System.Collections.Generic;

namespace UnitTestWPF
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        //public void ValidatPasswordTest()
        //{
        //    var page = new Authorization();
        //    Assert.IsTrue(page.Auth("admin1", "123"));

        //}
        public void RegistrationTest()
        {
            bool expected = true;

            var page = new Registration();

            bool result = (page.Reg("vikusik", "ddd@mail.com", "+764465566556", "user666", "123", 1));
            
            // Assert: проверка, что метод вернул false (пользователь уже существует)
            Assert.AreEqual(expected, result);
        }
    }
}
