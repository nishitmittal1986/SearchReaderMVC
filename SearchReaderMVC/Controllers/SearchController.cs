using System.Collections.Specialized;
using System.Net;
using System.Web.Mvc;
using SearchReaderMVC.Models;

namespace SearchReaderMVC.Controllers
{
    public class SearchController : Controller
    {
             
        public ActionResult Result()
        {
            string GoogleAdd = "https://www.google.com.au/search";
            string BingAdd = "https://www.bing.com/search";
            string myUrl = "www.infotrack.com.au";
            string SearchThis = "online title search";
            var GoogleResponse = ActualSearch(GoogleAdd, SearchThis);
            var BingResponse = ActualSearch(BingAdd, SearchThis);

            var SearchVal = new SearchVal { };
            
            if (GoogleResponse.Contains(myUrl))
            {                          
                SearchVal.GoogleSearchResult = "The URL " + myUrl +" appears " + CountmyUrl(GoogleResponse, myUrl).ToString() + " times in the Google search results.";
            }
            else
            {
                SearchVal.GoogleSearchResult = "No Results were found for " + myUrl + "in Google search results.";
            }

            if (BingResponse.Contains(myUrl))
            {
                SearchVal.BingSearchResult = "The URL " + myUrl + " appears " + CountmyUrl(BingResponse, myUrl).ToString() + " times in the Bing search results.";
            }
            else
            {
                SearchVal.BingSearchResult = "No Results were found for " + myUrl + " in Bing search results.";
            }

            return View(SearchVal);
        }

      
        public static int CountmyUrl(string Response, string URL)
        {
            // Loop through all instances of the string 'URL' and returns count.
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