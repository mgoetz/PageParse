using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PageParse.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string RemoveNonWordCharacters(this string str)
        {
            return Regex.Replace(HttpUtility.HtmlDecode(str), "[^A-Za-z0-9' ]", " ");
        }
    }
}