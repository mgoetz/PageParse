using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;
using System.Text;
using PageParse.Extensions;

namespace PageParse.Tools
{
    /// <summary>
    /// A helper class for extracting user-visable words from an HTML document.
    /// </summary>
    public static class WordExtractor
    {
        /// <summary>
        /// Extracts the words.
        /// </summary>
        /// <param name="rawContent">Content of the raw.</param>
        /// <returns></returns>
        public static string ExtractWords(string rawContent)
        {
            var returnValue = string.Empty;
            StringBuilder stringBuilder = new StringBuilder();

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(rawContent);

            IEnumerable<HtmlNode> allNodes = htmlDoc.DocumentNode.DescendantsAndSelf().Where(IsNodeValidToProcess);

            foreach (HtmlNode node in allNodes)
            {
                string altText = node.GetAttributeValue("alt", string.Empty);
                string titleText = node.GetAttributeValue("title", string.Empty);
                string rawNodeText = (node.NodeType == HtmlNodeType.Text) ? node.InnerText : string.Empty;
                string cleanedNodeText = HttpUtility.HtmlDecode(rawNodeText).RemoveNonWordCharacters();
                stringBuilder.Append(altText);
                stringBuilder.Append(" ");
                stringBuilder.Append(titleText);
                stringBuilder.Append(" ");
                stringBuilder.Append(cleanedNodeText);
                stringBuilder.Append(" ");
            }

            returnValue = stringBuilder.ToString();

            return returnValue;
        }

        /// <summary>
        /// Determines whether an HTML node contains words which need to be extracted.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>
        ///   <c>true</c> if the words in the node should be extracted; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsNodeValidToProcess(HtmlNode node)
        {
            bool returnValue = true;
            if ((node.Name == "script" || node.Name == "style" || node.NodeType == HtmlNodeType.Comment))
            {
                returnValue = false;
            }

            if (returnValue && node.ParentNode != null && (node.ParentNode.Name == "script" || node.ParentNode.Name == "style"))
            {
                returnValue = false;
            }

            return returnValue;
        }

    }
}