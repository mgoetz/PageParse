using System;
using System.Text;
using PageParse.Tools;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

namespace PageParseTest.Tools
{
    [TestFixture]
    public class WordExtractorTest
    {
        public const string htmlSample = @"<html><head><title>html sample</title><script>var variable = 1;</script><style>.style{display:inline;}</style><body>Hello World!</body></html>";
        public const string htmlSampleExpectedWords = @"html sample Hello World";
        public const string htmlSampleOfHtml = @"<html><head><title>html sample with html</title><script>var variable = 1;</script><style>.style{display:inline;}</style><body>&lt;strong&gt;Hello World!&lt;/strong&gt;<img alt='foo' /><a title='bar'></body></html>";
        public const string htmlSampleOfHtmlExpectedWords = @"html sample with html strong Hello World strong foo bar";

        [TestCase("")]
        [TestCase("foo bar bas")]
        [TestCase(htmlSample)]
        [TestCase(htmlSampleOfHtml)]
        public void WordExtractor_TestString_NoCrash(string input)
        {
            // Arrange
            string testString = input;

            // Act

            // Act & Assert
            Assert.DoesNotThrow(() => WordExtractor.ExtractWords(testString));
        }


        [TestCase(htmlSample)]
        [TestCase(htmlSampleOfHtml)]
        public void WordExtractor_TestString_NoTags(string input)
        {
            // Arrange
            string testString = input;

            // Act
            string wordlist = WordExtractor.ExtractWords(testString);

            // Act & Assert
            StringAssert.DoesNotContain("<html>", wordlist);
            StringAssert.DoesNotContain("script", wordlist);
            StringAssert.DoesNotContain("variable", wordlist);
        }


        [TestCase(htmlSample, htmlSampleExpectedWords)]
        [TestCase(htmlSampleOfHtml, htmlSampleOfHtmlExpectedWords)]
        public void WordExtractor_TestString_HasExpectedWords(string input, string expectedWordsRaw)
        {
            // Arrange
            string testString = input;

            // Act
            string wordlist = WordExtractor.ExtractWords(testString);

            // Act & Assert
            string[] actualWords = wordlist.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] expectedWords = expectedWordsRaw.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string expectedWord in expectedWords)
            {
                Assert.IsTrue(actualWords.Any(x => x.CompareTo(expectedWord) == 0));
            }
        }


        [TestCase(htmlSample, htmlSampleExpectedWords)]
        [TestCase(htmlSampleOfHtml, htmlSampleOfHtmlExpectedWords)]
        public void WordExtractor_TestString_AllWordsReturnedAreExpected(string input, string expectedWordsRaw)
        {
            // Arrange
            string testString = input;

            // Act
            string wordlist = WordExtractor.ExtractWords(testString);

            // Act & Assert
            string[] actualWords = wordlist.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] expectedWords = expectedWordsRaw.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string actualWord in actualWords)
            {
                Assert.IsTrue(expectedWords.Any(x => x.CompareTo(actualWord) == 0));
            }
        }


    }
}
