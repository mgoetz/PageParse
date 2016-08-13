using System;
using PageParse.Tools;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

namespace PageParseTest.Tools
{
    [TestFixture]
    public class AbsolutePathDeterminerTest
    {
        public const string pagePath = @"https://www.example.com/path/to/page/index.html";
        public const string pagePathDefaultFile = @"https://www.example.com/path/to/page/";
        public const string relativeImagePath1 = @"/path/to/images/image.jpg";
        public const string relativeImagePath1Resolved = @"https://www.example.com:443/path/to/images/image.jpg";
        public const string relativeImagePath2 = @"subpath/to/images/image.jpg";
        public const string relativeImagePath2Resolved = @"https://www.example.com:443/path/to/page/subpath/to/images/image.jpg";
        public const string relativeImagePath3 = @"../../images/image.jpg";
        public const string relativeImagePath3Resolved = @"https://www.example.com:443/path/to/page/../../images/image.jpg";
        public const string relativeProtocolImagePath = @"//www.example.org/path/to/images/image.jpg";
        public const string relativeProtocolImagePathResolved = @"https://www.example.org/path/to/images/image.jpg";
        public const string absoluteImagePath = @"http://www.example.net/path/to/images/image.jpg";
        public const string absoluteImagePathResolved = @"http://www.example.net/path/to/images/image.jpg";


        [TestCase(pagePath, relativeImagePath1, relativeImagePath1Resolved)]
        [TestCase(pagePath, relativeImagePath2, relativeImagePath2Resolved)]
        [TestCase(pagePath, relativeImagePath3, relativeImagePath3Resolved)]
        [TestCase(pagePath, relativeProtocolImagePath, relativeProtocolImagePathResolved)]
        [TestCase(pagePath, absoluteImagePath, absoluteImagePathResolved)]
        [TestCase(pagePathDefaultFile, relativeImagePath1, relativeImagePath1Resolved)]
        [TestCase(pagePathDefaultFile, relativeImagePath2, relativeImagePath2Resolved)]
        [TestCase(pagePathDefaultFile, relativeImagePath3, relativeImagePath3Resolved)]
        [TestCase(pagePathDefaultFile, relativeProtocolImagePath, relativeProtocolImagePathResolved)]
        [TestCase(pagePathDefaultFile, absoluteImagePath, absoluteImagePathResolved)]
        public void AbsolutePathDeterminer_ImagePaths_AsExpected(string pageUrl, string imageSrc, string expectedOutput)
        {
            // Arrange

            // Act
            string absolutePath = AbsolutePathDeterminer.GetAbsoluteURL(imageSrc, pageUrl);

            // Act & Assert
            Assert.IsTrue(expectedOutput.CompareTo(absolutePath) == 0);
        }
    }
}
