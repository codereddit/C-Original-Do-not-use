using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.IO;

namespace CodeReddit
{
    //Holds our nice global application-level variables
    public static class Global
    {

        public static DateTime lastGrab;
        public static List<string> urlQueue;
        public static List<string> spiderQueue;
        public static List<string> redditRequests;
        public const int MIN_REFRESH_INTERVAL = 600; //10 min (per page)
        public const int MIN_CRAWL_INTERVAL = 5; //seconds (per request)

    }
    
    public static class Util
    {
        /// <summary>
        /// Returns the content of a given web adress as string.
        /// </summary>
        /// <param name="Url">URL of the webpage</param>
        /// <returns>Website content</returns>
        public static bool DownloadWebPage(string Url, ref string PageContent, bool force_download)
        {
            // Make sure we're not crawling too fast!!!! (but sometimes we force it)
            if (!force_download)
            {
                //If we already have stuff in the queue or if it's empty but we're not yet at the crawl interval,
                //add the url to our queue
                if (Global.lastGrab.AddSeconds(Global.MIN_CRAWL_INTERVAL) > DateTime.Now)
                {
                    //If it's too soon, add the url we're trying to find into the queue so our bot can pick it up
                    if(!Global.urlQueue.Contains(Url))
                        Global.urlQueue.Add(Url); //strip off the json extension when saving to queue
                    return false;
                }
            }

            //Reset our counter to now so it blocks other requests
            Global.lastGrab = DateTime.Now;

            // Open a connection
            HttpWebRequest WebRequestObject = (HttpWebRequest)HttpWebRequest.Create(Url);

            // You can also specify additional header values like 
            // the user agent or the referer:
            //WebRequestObject.UserAgent = ".NET Framework/2.0";
            WebRequestObject.Referer = "";

            // Request response:
            WebResponse Response = WebRequestObject.GetResponse();
            
            // Open data stream:
            Stream WebStream = Response.GetResponseStream();

            // Create reader object:
            StreamReader Reader = new StreamReader(WebStream);

            Global.redditRequests.Add(DateTime.Now + " " + Url);

            // Read the entire stream content:
            PageContent = Reader.ReadToEnd();

            //Reset our counter to now after we actually grabbed the page
            Global.lastGrab = DateTime.Now;

            // Cleanup
            Reader.Close();
            WebStream.Close();
            Response.Close();

            return true;
        }
    }

    //The reddit json blob
    public class Thing
    {
        public string kind;
        public Data data;
    }

    //json blob's details
    public class Data
    {
        public string modhash;
        public List<Thing> children = new List<Thing>();
        public string domain;

        //public string media_embed; //ignore
        public string subreddit; //used
        public string selftext_html; //ignore
        public string selftext; //used
        public string likes; //ignore
        public string saved; //ignore
        public string id; //ignore
        public string clicked; //ignore
        public string author; //used

        //public string media; //ignore
        public string score; //used
        public string over_18; //ignore
        public string hidden; //ignore
        public string thumbnail; //ignore
        public string subreddit_id; //ignore
        public string downs; //used
        public string is_self; //ignore
        public string permalink;
        public string name; //ignore
        public double created; //ignore
        public string url; //used
        public string title; //used
        public double created_utc; //used
        public string num_comments; //used
        public string ups; //used
        public string after; //ignore
        public string before; //ignore

        //The following are for kind=t1 (ie replies)
        public string body; //used
        public string body_html; //ignore
        public string link_id;
        public string link_title;
        public string parent_id; //ignore
        public Thing replies; //used
    }
}