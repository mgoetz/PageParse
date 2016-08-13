using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PageParse.Tools
{
    /// <summary>
    /// A helper class for case-insensitive counting of the frequency of words in a string
    /// </summary>
    public static class WordCounter
    {
        private static char[] seperators = {' '}; // Space is the only seperator as punctuation is handled/removed by a regex 

        /// <summary>
        /// Counts frequency of each word.
        /// </summary>
        /// <param name="rawInput">A string containing words to be counted.</param>
        /// <returns></returns>
        public static WordCountList CountWords(string rawInput)
        {
            WordCountList returnVal = new WordCountList();
            string stripped = Regex.Replace(rawInput, "[^A-Za-z0-9]", " ");

            string[] splitInput = stripped.ToLowerInvariant().Split(seperators, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in splitInput)
            {
                if (returnVal.ContainsKey(word))
                {
                    returnVal[word] = returnVal[word] + 1;
                }
                else
                {
                    returnVal.Add(word, 1);
                }
            }

            return returnVal;
        }
    }


    /// <summary>
    /// A helper object for passing around counted word frequency information.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.Dictionary{System.String, System.Int32}" />
    public class WordCountList : Dictionary<string, int>
    {
        /// <summary>
        /// Gets the sum of the frequencies of all words.
        /// </summary>
        /// <value>
        /// The total count of all words.
        /// </value>
        public int TotalCount { get { return this.Sum(x => x.Value); } }

        /// <summary>
        /// Gets the number of unique words counted.
        /// </summary>
        /// <value>
        /// The unique word count.
        /// </value>
        public int UniqueWordCount { get { return this.Count; } }

        /// <summary>
        /// Gets an Enumerable of counted words and their frequency.
        /// </summary>
        /// <value>
        /// The words by frequency.
        /// </value>
        public IEnumerable<KeyValuePair<string, int>> WordsByFrequency
        {
            get {
                return this.OrderByDescending(x => x.Value);
            }
        }
    }
}