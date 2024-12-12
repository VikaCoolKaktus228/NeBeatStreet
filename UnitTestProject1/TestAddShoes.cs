using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NeBeatStreet.AppData;
using NeBeatStreet.Pages;

namespace UnitTestProject1
{
    [TestClass]
    public class TestAddShoes
    {
        [TestMethod]
        public void AddShoes()
        {
            var page = new AddEditShoes(null);
            Assert.IsTrue(page.Add("fff", 2, 2, 2, 2, Convert.ToDecimal("2.12"), "dsd", "adsads", "pic.png"));

        }
        public void PhoneCheck()
        {
            var page = new Registration();
            string phone = "+7343349239";
            Assert.IsTrue(phone.Length > 10);
        }
        public void EmailCheck()
        {
            var page = new Registration();
            string email = "dsd@gmail.com";
            Assert.IsTrue(email.Contains("@"));
        }


    }
}
