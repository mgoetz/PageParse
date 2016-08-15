using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PageParse.Tools;

namespace PageParse.Controllers
{
    public class ParsePageController : Controller
    {


        [HttpPost]
        public ActionResult ParsePage(string userInput)
        {
            //Preserve the value for the next view:
            ViewBag.userInput = userInput;

            if (string.IsNullOrEmpty(userInput))
            {
                return View("index");
            }

            Uri uri;
            try
            {
                uri = new Uri(userInput);
            }
            catch (UriFormatException ufe)
            {
                //Tell the user they're doing it wrong.
                ViewBag.ErrorMessage = String.Format("The Url you entered, '{0}', is invalid.  Please correct your entry and try again.", userInput);
                return View("index");//Make this some error view?
            }

            KeyValuePair<string, string> pageInfo = GetPageContent(uri);
            string pageLocation = pageInfo.Key;
            string html = pageInfo.Value;
            string rawWordList = WordExtractor.ExtractWords(html);
            WordCountList wordCount = WordCounter.CountWords(rawWordList);
            IEnumerable<string> relImagePaths = ImageExtractor.GetImageRelativeURLS(html);
            IEnumerable<string> absImagePaths = relImagePaths.Select(relPath => AbsolutePathDeterminer.GetAbsoluteURL(relPath, pageLocation)).ToList();// Done processing, so enumerate result

            ViewBag.WordCountList = wordCount.WordsByFrequency.Take(10);
            ViewBag.ImageURLs = absImagePaths;


            return View("index");
        }

        /// <summary>
        /// Gets the content of the page.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <remarks>Page content pulling derived from here: http://stackoverflow.com/a/4510300/835589 </remarks>
        /// <returns></returns>
        private KeyValuePair<string, string> GetPageContent(Uri uri)
        {
            KeyValuePair<string, string> returnValue;

            WebRequest request = WebRequest.Create(uri);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            string html = String.Empty;
            using (StreamReader sr = new StreamReader(data))
            {
                html = sr.ReadToEnd();
            }

            var sourceUri = response.ResponseUri;
            returnValue = new KeyValuePair<string, string>(sourceUri.ToString(), html);
            return returnValue;
        }






        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}