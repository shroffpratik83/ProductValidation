using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductValidation.Engine;
using ProductValidation.Engine.Interface;
using ProductValidation.Entity;

namespace ProductValidation.Test
{
    [TestClass]
    public class ProductEngineTest
    {
        IProductEngine productEngine = new ProductEngine();

        [TestMethod]
        public void ValidateProducts_PositiveTest()
        {
            //Arrange
            var productList = _generateProductList();

            //Act
            var result = productEngine.ValidateProducts(productList);

            //Assert
            Assert.AreEqual(0, result.BadProducts.Count);
            Assert.AreEqual(3, result.GoodProducts.Count);
            Assert.IsTrue(result.GoodProducts[0].ContactTime == 5);
        }

        [TestMethod]
        public void ValidateProducts_ContactTimeAscedingOrder_PositiveTest()
        {
            //Arrange
            var productList = _generateProductList();

            //Act
            var result = productEngine.ValidateProducts(productList);

            //Assert
            Assert.IsTrue(result.GoodProducts[0].ContactTime == 5);
        }


        [TestMethod]
        public void ValidateProducts_ContactTimeThenByName_PositiveTest()
        {
            //Arrange
            var productList = _generateProductList();
            productList[1].ContactTime = "5";

            //Act
            var result = productEngine.ValidateProducts(productList);

            //Assert
            Assert.IsTrue(result.GoodProducts[0].ProductName == "JProduct2");
            Assert.IsTrue(result.GoodProducts[1].ProductName == "LProduct3");
        }

        [TestMethod]
        public void ValidateProducts_VirusesKilledFieldwithSemiColon_PositiveTest()
        {
            //Arrange
            var productList = _generateProductList();
           
            //Act
            var result = productEngine.ValidateProducts(productList);

            //Assert
            Assert.IsTrue(result.GoodProducts[0].VirusesKilled.Contains(";"));            
        }

        [TestMethod]
        public void ValidateProducts_ActiveIngredientswithSemiColon_PositiveTest()
        {
            //Arrange
            var productList = _generateProductList();

            //Act
            var result = productEngine.ValidateProducts(productList);

            //Assert
            Assert.IsTrue(result.GoodProducts[1].ActiveIngredients.Contains(";"));
        }


        [TestMethod]
        public void ValidateProducts_RegIdBlank_NegativeTest()
        {
            //Arrange
            var productList = _generateProductList();
            productList[0].RegistrationId = "";

            //Act
            var result = productEngine.ValidateProducts(productList);

            //Assert
            Assert.AreEqual(1, result.BadProducts.Count);
            Assert.AreEqual(2, result.GoodProducts.Count);
        }

        [TestMethod]
        public void ValidateProducts_RegIdDuplicate_NegativeTest()
        {
            //Arrange
            var productList = _generateProductList();
            productList[0].RegistrationId = "RegID-02";

            //Act
            var result = productEngine.ValidateProducts(productList);

            //Assert
            Assert.AreEqual(2, result.BadProducts.Count);
            Assert.AreEqual(1, result.GoodProducts.Count);
        }

        [TestMethod]
        public void ValidateProducts_ContactTimeBlank_NegativeTest()
        {
            //Arrange
            var productList = _generateProductList();
            productList[0].ContactTime = "";

            //Act
            var result = productEngine.ValidateProducts(productList);

            //Assert
            Assert.AreEqual(1, result.BadProducts.Count);
            Assert.AreEqual(2, result.GoodProducts.Count);
        }

        [TestMethod]
        public void ValidateProducts_ContactTimeString_NegativeTest()
        {
            //Arrange
            var productList = _generateProductList();
            productList[0].ContactTime = "CT";

            //Act
            var result = productEngine.ValidateProducts(productList);

            //Assert
            Assert.AreEqual(1, result.BadProducts.Count);
            Assert.AreEqual(2, result.GoodProducts.Count);
        }

        [TestMethod]
        public void ValidateProducts_ContactAllBlankRegId_NegativeTest()
        {
            //Arrange
            var productList = _generateProductList();
            productList[0].RegistrationId = "";
            productList[1].RegistrationId = "";
            productList[2].RegistrationId = "";

            //Act
            var result = productEngine.ValidateProducts(productList);

            //Assert
            Assert.AreEqual(3, result.BadProducts.Count);
            Assert.AreEqual(0, result.GoodProducts.Count);
        }

        private List<Product> _generateProductList()
        {
            var productList = new List<Product>() 
            { 
                new Product(){
                    RegistrationId = "RegID-01",
                    ContactTime = "7",
                    ActiveIngredients = new List<string>() { "ActiveIdg1" },
                    ProductName = "KProduct1",
                    VirusesKilled = new List<string>() { "V1" }
                },
                new Product()
                {
                    RegistrationId = "RegID-02",
                    ContactTime = "6",
                    ActiveIngredients = new List<string>() { "ActiveIdg1", "ActiveIdg2" },
                    ProductName = "JProduct2",
                    VirusesKilled = new List<string>() { "V2" }
                },
                new Product()
                {
                    RegistrationId = "RegID-03",
                    ContactTime = "5",
                    ActiveIngredients = new List<string>() { "ActiveIdg3"},
                    ProductName = "LProduct3",
                    VirusesKilled = new List<string>() { "V3","V4" }
                }
            };

            return productList;
        }
    }
}
