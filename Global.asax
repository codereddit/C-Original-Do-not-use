<%@ Application Language="C#" %>
<script runat="server">
    
    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        CodeReddit.Global.lastGrab = DateTime.Now;
        CodeReddit.Global.urlQueue = new System.Collections.Generic.List<string>();
        CodeReddit.Global.spiderQueue = new System.Collections.Generic.List<string>();
        CodeReddit.Global.redditRequests = new System.Collections.Generic.List<string>();

        //Create a background worker that will be our caching bot
        System.ComponentModel.BackgroundWorker cacher = new System.ComponentModel.BackgroundWorker();
        cacher.DoWork += new System.ComponentModel.DoWorkEventHandler(CachePage);
        cacher.WorkerReportsProgress = false;
        cacher.WorkerSupportsCancellation = false;
        //cacher.RunWorkerAsync(); // Calling the CachePage Method Asynchronously
        
        //Create a background worker that will be our spider
        System.ComponentModel.BackgroundWorker spider = new System.ComponentModel.BackgroundWorker();
        spider.DoWork += new System.ComponentModel.DoWorkEventHandler(CrawlFrontPage);
        spider.WorkerReportsProgress = false;
        spider.WorkerSupportsCancellation = false;
        //spider.RunWorkerAsync(); // Calling the CachePage Method Asynchronously
    }

    private static void CrawlFrontPage(object sender, System.ComponentModel.DoWorkEventArgs e)
    {
        while (true)
        {
            if (CodeReddit.Global.spiderQueue.Count == 0)
            {//Don't even bother to respider if we haven't completed the old one yet

                //First we set our timer to the future (this prevents our cacher from doing work)
                CodeReddit.Global.lastGrab = DateTime.Now.AddSeconds(CodeReddit.Global.MIN_CRAWL_INTERVAL * 3);
                
                //Now we wait for the crawl interval (in the worst case scenario a page was just cached)
                System.Threading.Thread.Sleep(1000 * CodeReddit.Global.MIN_CRAWL_INTERVAL); //1000* is for milliseconds

                //Now we start grabbing the front page
                string page = "";

                //We don't care if we have a cached version already... just do it
                CodeReddit.Util.DownloadWebPage("http://www.reddit.com/.json", ref page, true);
                string after_id = CrawlPage(ref page); //Crawling the page will dump everything into our spiderQueue
                
                //Now we wait for the crawl interval so we can get the next page
                System.Threading.Thread.Sleep(1000 * CodeReddit.Global.MIN_CRAWL_INTERVAL); //1000* is for milliseconds

                //We don't care if we have a cached version already... just do it
                CodeReddit.Util.DownloadWebPage("http://www.reddit.com/.json?after=" + after_id, ref page, true);
                CrawlPage(ref page); //Crawling the page will dump everything into our spiderQueue
                
                //Finally, set our time
                CodeReddit.Global.lastGrab = DateTime.Now;
            }
            
            //Only do complete crawls once every 2 hours 
            //since the front page doesn't change too often
            System.Threading.Thread.Sleep(120*60000); //120 * 1 minutes
        }
    }

    private static string CrawlPage(ref string page)
    {
        //For List pages
        System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
        CodeReddit.Thing thing = ser.Deserialize<CodeReddit.Thing>(page.Replace("\"replies\": \"\"", "\"replies\": {}"));

        foreach (CodeReddit.Thing child in thing.data.children)
        {
            //If it's a self post, we should redirect the url to ourselves so the user doesn't jump out to full reddit
            if (child.data.is_self == "True")
                CodeReddit.Global.spiderQueue.Add(System.Web.HttpUtility.HtmlEncode(child.data.url + ".json"));
            
            //Add the user to our queue
            CodeReddit.Global.spiderQueue.Add(System.Web.HttpUtility.HtmlEncode("http://www.reddit.com/user/" + child.data.author + "/" + ".json"));
            
            //Add the subreddit to our queue (if it's not already in it)
            if (!CodeReddit.Global.spiderQueue.Contains(System.Web.HttpUtility.HtmlEncode("http://www.reddit.com/r/" + child.data.subreddit + "/" + ".json")))
                CodeReddit.Global.spiderQueue.Add(System.Web.HttpUtility.HtmlEncode("http://www.reddit.com/r/" + child.data.subreddit + "/" + ".json"));
            
            //Add the comments to our queue
            CodeReddit.Global.spiderQueue.Add(System.Web.HttpUtility.HtmlEncode("http://www.reddit.com/r/" + child.data.subreddit + "/comments/" + child.data.id) + "/" + ".json");
        }

        return thing.data.after;
    }
    
    private static void NoOp()
    {
        return;
    }
    
    private static void CachePage(object sender, System.ComponentModel.DoWorkEventArgs e)
    {
        while (true)
        {
            if (CodeReddit.Global.lastGrab.AddSeconds(CodeReddit.Global.MIN_CRAWL_INTERVAL) < DateTime.Now)
            {
                if (CodeReddit.Global.urlQueue.Count + CodeReddit.Global.spiderQueue.Count > 0)
                {
                    //If there's pages waiting to be cached
                    string page = "";
                    string url = "";
                    
                    try
                    {

                        //Give preference to user selected pages, otherwise tackle our spidered pages
                        if (CodeReddit.Global.urlQueue.Count > 0)
                            url = CodeReddit.Global.urlQueue[0];
                        else
                            url = CodeReddit.Global.spiderQueue[0];

                        bool stale = false;
                        bool skip = false; //Determines if we should skip the file download and pretend it went ok
                                           //Due to a fresh cache copy
                        string output = "";

                        //Check if we already have a cached version of the page already
                        System.Data.SqlClient.SqlConnection sqlcon = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CRCS"].ConnectionString);
                        System.Data.SqlClient.SqlCommand sqlcmd = new System.Data.SqlClient.SqlCommand("SELECT ISNULL(DATEDIFF(s,LastUpdate,getdate()),0) AS SecondsOld FROM Reddit_Cache WHERE Url = @Url", sqlcon);
                        sqlcmd.Parameters.AddWithValue("Url", url);
                        sqlcon.Open();
                        object objSecondsOld = sqlcmd.ExecuteScalar();
                        if (objSecondsOld != null && (int)objSecondsOld > CodeReddit.Global.MIN_REFRESH_INTERVAL) //Stale cache hit
                            stale = true;
                        else if (objSecondsOld != null)
                        {
                            CodeReddit.Global.urlQueue.Remove(url);
                            CodeReddit.Global.spiderQueue.Remove(url);
                            skip = true; //in this case, it exists and is not stale, therefore we're done here
                        }
                        sqlcon.Close();

                        bool dlOK = false;

                        if (skip)
                            dlOK = true;
                        else
                            dlOK = CodeReddit.Util.DownloadWebPage(url, ref page, false);

                        //Includes Debugging Pages
                        if (!dlOK)
                            NoOp(); //Ignore it if things go bad
                        else if (url.Contains("/comments/") || url == "http://localhost:1120/CodeReddit/JSONComment.txt")
                            PageBuilder.Process_Comments(ref page, ref output);
                        else if (url.Contains("/user/") || url == "http://localhost:1120/CodeReddit/JSONUser.txt")
                            PageBuilder.Process_User(ref page, ref output, url);
                        else
                            PageBuilder.Process_Topics(ref page, ref output, url);

                        //Save our results to cache (as long as it's not the error page)
                        if (dlOK && page.Length != 0)
                        {
                            sqlcmd = new System.Data.SqlClient.SqlCommand("", sqlcon);

                            if (!stale)//Not stale, new cache
                                sqlcmd.CommandText = "INSERT INTO Reddit_Cache VALUES (@Url, getdate(),@Response)";
                            else//Stale, update the old cache we have
                                sqlcmd.CommandText = "UPDATE Reddit_Cache SET LastUpdate = getdate(), Response = @Response WHERE Url = @Url";
                            sqlcmd.Parameters.AddWithValue("Response", output);
                            sqlcmd.Parameters.AddWithValue("Url", url);
                            sqlcon.Open();
                            sqlcmd.ExecuteNonQuery();
                            sqlcon.Close();
                            
                            CodeReddit.Global.urlQueue.Remove(url);
                            CodeReddit.Global.spiderQueue.Remove(url);
                        }
                    }
                    catch (Exception ex)
                    {
                        //Swallow any errors and remove the url if there was one
                        CodeReddit.Global.urlQueue.Remove(url);
                        CodeReddit.Global.spiderQueue.Remove(url);
                    }
                }
            }
            
            //Rest for 1 second, then go back to work
            System.Threading.Thread.Sleep(1000);
        }
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
