using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;


namespace PageParse.Tools
{
    /// <summary>
    /// A helper class for extracting image paths from image tags in a well-formed HTML document.
    /// </summary>
    public static class ImageExtractor
    {
        /// <summary>
        /// Gets the page-relative image urls from an html document.
        /// </summary>
        /// <param name="rawContent">Content of the HTML document.</param>
        /// <returns></returns>
        public static IEnumerable<string> GetImageRelativeURLS(string rawContent)
        {
            IEnumerable<string> returnValue = new List<string>();

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(rawContent);

            var allNodes = htmlDoc.DocumentNode.DescendantsAndSelf();
            var imageNodes = allNodes.Where(node => node.Name == "img");
            var imagePaths = imageNodes.Select(node => node.GetAttributeValue("src", ""));
            var nonEmptyImagePaths = imagePaths.Where(src => !string.IsNullOrEmpty(src));//Consider a stringextention

            returnValue = nonEmptyImagePaths.ToList();

            return returnValue;
        }
    }


}