using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;


namespace PageParse.Tools
{
    /// <summary>
    /// A helper class for composing absolute URLs based on a (possibly) relative URI, and the refering pages URI
    /// </summary>
    public static class AbsolutePathDeterminer
    {
        /// <summary>
        /// Gets the absolute URL.
        /// </summary>
        /// <param name="imageSrc">The image source.</param>
        /// <param name="pageUrl">The page URL.</param>
        /// <remarks>If the page URL includes only the path, but not the file, AND doesn't end with a slash, the returned image URL may be incorrect.</remarks>
        /// <returns></returns>
        public static string GetAbsoluteURL(string imageSrc, string pageUrl)
        {
            string returnValue = imageSrc;

            if (Regex.IsMatch(returnValue, "^https?://.*"))
            {
                // The path is already absolute, return it as is
                return returnValue;
            }

            Uri page = new Uri(pageUrl);

            if (imageSrc.StartsWith("//"))
            {
                returnValue = page.Scheme + Uri.SchemeDelimiter + imageSrc.Remove(0, 2);
            }
            else if (imageSrc.StartsWith("/"))
            {
                returnValue = page.Scheme + Uri.SchemeDelimiter + page.Host + ":" + page.Port + imageSrc;
            }
            else //The page-relative cases for both "example.jpg" and "../example.jpg are both covered here:
            {
                returnValue = page.Scheme +
                              Uri.SchemeDelimiter + 
                              page.Host + 
                              ":" + 
                              page.Port + 
                              String.Join("", page.Segments.Where(segment => segment.EndsWith("/"))) +
                              imageSrc;

            }


            return returnValue;
        }
    }


}