using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SearchReaderMVC.Models;

namespace SearchReaderMVC.Controllers
{
    public class SearchController : Controller
    {
        
        public ActionResult SearchGoogle()
        {
            string GoogleAdd = "https://www.google.com.au/search";
            string myUrl = "www.infotrack.com.au";
            string SearchThis = "online title search";

            var Response = ActualSearch(GoogleAdd, SearchThis);

            var SearchVal = new SearchVal { };
            
            if (Response.Contains(myUrl))
            {
                var CountResult = CountmyUrl(Response, myUrl);                
                SearchVal.SearchResult = "The URL " + myUrl +" appears " + CountResult.ToString() + " times in the Google search Results.";
            }
            else
            {
                SearchVal.SearchResult = "No Results were found for " + myUrl + ".";
            }

            return View(SearchVal);
        }


        public ActionResult SearchBing()
        {
            string BingAdd = "https://www.bing.com/search";
            string myUrl = "www.infotrack.com.au";
            string SearchThis = "online title search";

            var Response = ActualSearch(BingAdd, SearchThis);

            var SearchVal = new SearchVal { };           


            if (Response.Contains(myUrl))
            {
                var CountResult = CountmyUrl(Response, myUrl);
                SearchVal.SearchResult = "The URL " + myUrl + " appears " + CountResult.ToString() + " times in the Bing search Results.";
            }
            else
            {
                SearchVal.SearchResult = "No Results were found for " + myUrl + ".";
            }

            return View(SearchVal);
        }

        public static int CountmyUrl(string Response, string URL)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = Response.IndexOf(URL, i)) != -1)
            {
                i += URL.Length;
                count++;
            }
            return count;
        }

        public static string ActualSearch(string SearchEngine, string SearchString)
        {
            // This will do actual search on Google or Bing and bring back results in string format

            WebClient webClient = new WebClient();

            NameValueCollection nameValueCollection = new NameValueCollection();
            nameValueCollection.Add("q", SearchString);

            webClient.QueryString.Add(nameValueCollection);            
            return webClient.DownloadString(SearchEngine).ToString(); 
        }
    }
}