using System;
using PageParse.Tools;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

namespace PageParseTest.Tools
{
    [TestFixture]
    public class WordCounterTest
    {
        [TestCase("")]
        [TestCase("foo")]
        [TestCase("foo foo foo")]
        [TestCase("Foo FOO foo")]
        [TestCase("foo bar bas")]
        public void WordCounter_TestString_NoCrash(string input)
        {
            // Arrange
            string testString = input;

            // Act

            // Act & Assert
            Assert.DoesNotThrow(() => WordCounter.CountWords(testString));
        }

        [TestCase("", 0)]
        [TestCase("foo", 1)]
        [TestCase("foo foo foo", 3)]
        [TestCase("Foo FOO foo", 3)]
        [TestCase("Foo-FOO-foo", 3)]
        [TestCase("foo bar bas", 3)]
        [TestCase("foo,bar. bas", 3)]
        public void WordCounter_TestString_CorrectTotal(string input, int count)
        {
            // Arrange
            string testString = input;

            // Act
            WordCountList output = WordCounter.CountWords(testString);

            // Assert
            Assert.AreEqual(count, output.TotalCount);

        }

        [TestCase("", 0)]
        [TestCase("foo", 1)]
        [TestCase("foo foo foo", 1)]
        [TestCase("Foo FOO foo", 1)]
        [TestCase("Foo:FOO:foo", 1)]
        [TestCase("foo bar bas", 3)]
        [TestCase("foo,bar. bas", 3)]
        public void WordCounter_TestString_CorrectUniqueCount(string input, int uniqueCount)
        {
            // Arrange
            string testString = input;

            // Act
            WordCountList output = WordCounter.CountWords(testString);

            // Assert
            Assert.AreEqual(uniqueCount, output.UniqueWordCount);

        }

        [TestCase("", null, 0)]
        [TestCase("foo", "foo", 1)]
        [TestCase("foo foo foo", "foo", 3)]
        [TestCase("Foo FOO bar", "foo", 2)]
        [TestCase("foo bar bas foo", "foo", 2)]
        public void WordCounter_TestString_CorrectTopWordAndCount(string input, string topWordExpected, int expectedCount)
        {
            // Arrange
            string testString = input;

            // Act
            WordCountList output = WordCounter.CountWords(testString);
            KeyValuePair<string, int> topWordStats = output.WordsByFrequency.FirstOrDefault();
            string topWord = topWordStats.Key;
            int topWordCount = topWordStats.Value;

            // Assert
            Assert.AreEqual(topWordExpected, topWord);
            Assert.AreEqual(expectedCount, topWordCount);

        }
    }
}
