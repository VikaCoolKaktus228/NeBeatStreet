using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NeBeatStreet.AppData;
using NeBeatStreet.Pages;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class TestAddShoes
    {
        [TestMethod]

        //public void PhoneCheck()
        //{
        //    var page = new Registration();
        //    string phone = "+7343349239";
        //    Assert.IsTrue(phone.Length > 10);
        //}
        //public void EmailCheck()
        //{
        //    var page = new Registration();
        //    string email = "dsd@gmail.com";
        //    Assert.IsTrue(email.Contains("@"));
        //}
        public void AddEditCheck()
        {
            var page = new AddEditShoes(null);

            // Act
            var addResult = page.Add(
                "TestName",   
                1,            
                2,            
                3,            
                4,            
                100.50m,      
                "TestDesc",   
                "TestArt",    
                "TestPhoto"   
            );

            
            var addedShoes = Entities5.GetContext().Shoes.FirstOrDefault(s => s.ShoesName == "TestName");

            
            Assert.IsTrue(addResult);
            Assert.IsNotNull(addedShoes);
            Assert.AreEqual("TestName", addedShoes.ShoesName);
            Assert.AreEqual(1, addedShoes.Color);
            Assert.AreEqual(2, addedShoes.TypeOfShoes);
            Assert.AreEqual(3, addedShoes.Firm);
            Assert.AreEqual(4, addedShoes.Material);
            Assert.AreEqual(100.50m, addedShoes.Price);
            Assert.AreEqual("TestDesc", addedShoes.Description);
            Assert.AreEqual("TestArt", addedShoes.Article);
            Assert.AreEqual("TestPhoto", addedShoes.Photo);

            // Act
            var editResult = page.Edit(
                "UpdatedName",  
                3,              
                1,             
                3,              
                2,              
                200.75m,        
                "UpdatedDesc",  
                "UpdatedArt",   
                "UpdatedPhoto"  
            );
           
            Assert.IsTrue(editResult);
            Assert.AreEqual("UpdatedName", addedShoes.ShoesName);
            Assert.AreEqual(5, addedShoes.Color);
            Assert.AreEqual(6, addedShoes.TypeOfShoes);
            Assert.AreEqual(7, addedShoes.Firm);
            Assert.AreEqual(8, addedShoes.Material);
            Assert.AreEqual(200.75m, addedShoes.Price);
            Assert.AreEqual("UpdatedDesc", addedShoes.Description);
            Assert.AreEqual("UpdatedArt", addedShoes.Article);
            Assert.AreEqual("UpdatedPhoto", addedShoes.Photo);
        }
    }
        
}
