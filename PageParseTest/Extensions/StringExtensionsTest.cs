using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PageParse.Extensions;

namespace PageParseTest.Extensions
{
    [TestFixture]
    public class StringExtensionsTest
    {
        [TestCase(null, true, TestName = "IsNullOrEmpty_OnNull_ReturnsTrue")]
        [TestCase("", true, TestName = "IsNullOrEmpty_OnEmpty_ReturnsTrue")]
        [TestCase(" ", false, TestName = "IsNullOrEmpty_OnWhitespace_ReturnsFalse")]
        [TestCase("foo", false, TestName = "IsNullOrEmpty_OnPopulatedString_ReturnsFalse")]
        public void IsNullOrEmpty_GenericContent_ExpectedResult(string input, bool expected)
        {
            // Arrange

            // Act
            bool result = input.IsNullOrEmpty();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestCase(" ")]
        [TestCase("foo")]
        [TestCase("foo,.;':\"[]{}-=_+\\|~`!@#$%^&*() \t\n")]
        public void RemoveNonWordCharacters_GenericContent_OnlyAlphaNumericApostropheAndSpaceRemain(string input)
        {
            // Arrange

            // Act
            string result = input.RemoveNonWordCharacters();

            // Assert
            Assert.True(result.ToCharArray().All(c => (char.IsLetterOrDigit(c) || c == ' ' || c == '\'')));
        }

        [TestCase(" ")]
        [TestCase("foo")]
        [TestCase("Lorem ipsum dolar sed imet")]
        public void RemoveNonWordCharacters_WordContent_IsUnchaged(string input)
        {
            // Arrange

            // Act
            string result = input.RemoveNonWordCharacters();

            // Assert
            Assert.AreEqual(input, result);
        }

    }
}
