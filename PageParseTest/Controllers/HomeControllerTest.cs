using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using PageParse.Controllers;

namespace PageParseTest.Controllers
{
    [TestFixture]
    class HomeControllerTest
    {
        [TestCase("", "index", TestName = "PageParse_NoInput_ReturnsView")]
        [TestCase("https://www.google.com", "index", TestName = "PageParse_Google_ReturnsView")]
        [TestCase("http://www.cnn.com", "index", TestName = "PageParse_Cnn_ReturnsView")]
        public void ParsePage_CaseInput_ReturnsExpectedView(string targetPage, string viewName)
        {
            // Arrange
            ParsePageController controllerUnderTest = new ParsePageController();

            //Act
            ViewResult result = controllerUnderTest.ParsePage(targetPage) as ViewResult;

            // Assert
            Assert.AreEqual(viewName, result.ViewName);
        }

        [TestCase("ThisIsNotAVlidURI")]
        [TestCase("1337 h4xxx")]
        public void ParsePage_BadURI_ViewBagHasErrorMessage(string badTargetUri)
        {
            // Arrange
            ParsePageController controllerUnderTest = new ParsePageController();

            //Act
            ViewResult result = controllerUnderTest.ParsePage(badTargetUri) as ViewResult;

            // Assert
            Assert.IsNotNull(result.ViewData["ErrorMessage"]);
        }

        [TestCase("https://www.google.com")]
        [TestCase("http://www.cnn.com")]
        public void ParsePage_GoodURI_ViewBagHasNoErrorMessage(string goodTargetUri)
        {
            // Arrange
            ParsePageController controllerUnderTest = new ParsePageController();

            //Act
            ViewResult result = controllerUnderTest.ParsePage(goodTargetUri) as ViewResult;

            // Assert
            Assert.IsNull(result.ViewData["ErrorMessage"]);
        }

    }
}
