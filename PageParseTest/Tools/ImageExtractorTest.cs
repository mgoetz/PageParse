using System;
using PageParse.Tools;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

namespace PageParseTest.Tools
{
    [TestFixture]
    public class ImageExtractorTest
    {
        #region constant testing strings
        public const string htmlSampleNoImage = @"<html><head><title>html sample</title><script>var variable = 1;</script><style>.style{display:inline;}</style><body>Hello World!</body></html>";
        public const string htmlSampleRelativeImagePath1 = @"<html><head><title>html sample</title><script>var variable = 1;</script><style>.style{display:inline;}</style><body>Hello World! <img src='/foo/bar/bas.jpg' /></body></html>";
        public const string relativeImagePath1 = "/foo/bar/bas.jpg";
        public const string htmlSampleRelativeImagePath2 = @"<html><head><title>html sample</title><script>var variable = 1;</script><style>.style{display:inline;}</style><body>Hello World! <img src='foo/bar/bas.gif' /></body></html>";
        public const string relativeImagePath2 = "foo/bar/bas.gif";
        public const string htmlSampleRelativeImagePath3 = @"<html><head><title>html sample</title><script>var variable = 1;</script><style>.style{display:inline;}</style><body>Hello World! <img src='../foo/bar/bas.gif' /></body></html>";
        public const string relativeImagePath3 = "../foo/bar/bas.gif";
        public const string htmlSampleRelativeProtocolImagePath = @"<html><head><title>html sample</title><script>var variable = 1;</script><style>.style{display:inline;}</style><body>Hello World! <img src='//example.com/foo/bar/bas.png' /></body></html>";
        public const string relativeProtocolImagePath = "//example.com/foo/bar/bas.png";
        public const string htmlSampleAbsoluteImagePath1 = @"<html><head><title>html sample</title><script>var variable = 1;</script><style>.style{display:inline;}</style><body>Hello World! <img src='http://example.com/foo/bas.jpg' /></body></html>";
        public const string htmlSampleAbsoluteImagePath2 = @"<html><head><title>html sample</title><script>var variable = 1;</script><style>.style{display:inline;}</style><body>Hello World! <img src='http://example.com/foo/bas.jpg'></img></body></html>";
        public const string absoluteImagePath = "http://example.com/foo/bas.jpg";
        public const string htmlSampleNoImagePath = @"<html><head><title>html sample</title><script>var variable = 1;</script><style>.style{display:inline;}</style><body>Hello World! <img src=''></img></body></html>";
        public const string htmlSampleTwoImagesFirstImageNoPathNoImagePath = @"<html><head><title>html sample</title><script>var variable = 1;</script><style>.style{display:inline;}</style><body>Hello World! <img src=''></img><img src='hello.jpg' /></body></html>";
        public const string secondImagePath = @"hello.jpg";
        //TODO: one for AllTheTags
        #endregion

        [TestCase("")]
        [TestCase("foo")]
        [TestCase("foo foo foo")]
        [TestCase("Foo FOO foo")]
        [TestCase("foo bar bas")]
        public void ImageExtractor_TestString_NoCrash(string input)
        {
            // Arrange
            string testString = input;

            // Act

            // Act & Assert
            Assert.DoesNotThrow(() => ImageExtractor.GetImageRelativeURLS(testString));
        }

        [TestCase(htmlSampleNoImage, "")]
        [TestCase(htmlSampleRelativeImagePath1, relativeImagePath1)]
        [TestCase(htmlSampleRelativeImagePath2, relativeImagePath2)]
        [TestCase(htmlSampleRelativeImagePath3, relativeImagePath3)]
        [TestCase(htmlSampleRelativeProtocolImagePath, relativeProtocolImagePath)]
        [TestCase(htmlSampleAbsoluteImagePath1, absoluteImagePath)]
        [TestCase(htmlSampleAbsoluteImagePath2, absoluteImagePath)]
        [TestCase(htmlSampleNoImagePath, "")]
        [TestCase(htmlSampleTwoImagesFirstImageNoPathNoImagePath, secondImagePath)]
        public void ImageExtractor_TestString_FirtstImageExpectedPath(string input, string expectedOutput)
        {
            // Arrange

            // Act
            IEnumerable<string> results = ImageExtractor.GetImageRelativeURLS(input);
            string firstResult = results.FirstOrDefault() ?? string.Empty;


            // Act & Assert
            Assert.IsTrue(expectedOutput.CompareTo(firstResult) == 0);
        }

        [Test]
        public void ImageExtractor_TestString_MultipleImagesExpectedPath()
        {
            // Arrange
            string testHtml = @"<html><head /><body><img src='1.jpg' /><img src='2.jpg'></img><br/><div><img src=""3.jpg"" /></div></body></html>";

            // Act
            IEnumerable<string> results = ImageExtractor.GetImageRelativeURLS(testHtml);
            string firstResult = results.ElementAt(0);
            string secondResult = results.ElementAt(1);
            string thirdResult = results.ElementAt(2);

            // Act & Assert
            Assert.IsTrue("1.jpg".CompareTo(firstResult) == 0);
            Assert.IsTrue("2.jpg".CompareTo(secondResult) == 0);
            Assert.IsTrue("3.jpg".CompareTo(thirdResult) == 0);
        }

    }
}
