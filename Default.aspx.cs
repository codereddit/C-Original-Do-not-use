using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;
using CodeReddit;

public partial class _Default : System.Web.UI.Page
{


    private string output;
    private bool stale;

    public _Default()
    {
        output = "";
        stale = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Check if we're returning from a failed page load
        bool attempted = false;
        if (Request.QueryString["attempted"] != null)
            attempted = true;

        //Set up the url we'll be grabbing
        string reddit_url = "";
        if (Request.QueryString["url"] != null)
            reddit_url = Request.QueryString["url"].ToString();
        if (reddit_url == "" || (reddit_url.Substring(0, 22) != "http://www.reddit.com/") && reddit_url.Substring(0, 18) != "http://reddit.com/")
            reddit_url = "http://www.reddit.com/.json";

        #region DEBUG_URLs
        //reddit_url = "http://localhost:1120/CodeReddit/TestUrl/JSONResponse.txt.json";
        //reddit_url = "http://localhost:1120/CodeReddit/TestUrl/JSONComment.txt.json";
        //reddit_url = "http://localhost:1120/CodeReddit/TestUrl/JSONUser.txt.json";
        //reddit_url = "http://localhost:1120/CodeReddit/TestUrl/JSONError.txt.json";
        #endregion

        //Check if we already have a cached version of the page
        SqlConnection sqlcon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CRCS"].ConnectionString);
        SqlCommand sqlcmd = new SqlCommand("SELECT Response,ISNULL(DATEDIFF(s,LastUpdate,getdate()),0) AS SecondsOld FROM Reddit_Cache WHERE Url = @Url", sqlcon);
        sqlcmd.Parameters.AddWithValue("Url", reddit_url);
        sqlcon.Open();
        SqlDataReader dr = sqlcmd.ExecuteReader();
        if (dr.Read())
        {
            output = dr["Response"].ToString();
            if ((int)dr["SecondsOld"] > Global.MIN_REFRESH_INTERVAL) //Stale cache hit
                stale = true;
        }
        dr.Close();
        sqlcon.Close();

        //Cache miss!
        if (output == "" || stale)
        {
            try
            {
                //Get the json string and deserialize it
                string page = "";
                bool dlOK = false;

                //If there's already stuff in the queue, don't bother checking, just
                //add it to the end of the queue
                if (Global.urlQueue.Count > 0)
                {
                    if(!Global.urlQueue.Contains(reddit_url))
                        Global.urlQueue.Add(reddit_url);
                }
                else
                    dlOK = Util.DownloadWebPage(reddit_url, ref page, false);

                //Includes Debugging Pages
                if (!dlOK && attempted)
                    PageBuilder.PrintErrorPage(ref output); //we already tried to wait for the page, maybe it's broken so stop trying
                else if (!dlOK && !attempted)
                    PageBuilder.Process_TooFast(ref output);
                else if (reddit_url.Contains("/comments/") || reddit_url == "http://localhost:1120/CodeReddit/TestUrl/JSONComment.txt")
                    PageBuilder.Process_Comments(ref page, ref output);
                else if (reddit_url.Contains("/user/") || reddit_url == "http://localhost:1120/CodeReddit/TestUrl/JSONUser.txt")
                    PageBuilder.Process_User(ref page, ref output, reddit_url);
                else
                    PageBuilder.Process_Topics(ref page, ref output, reddit_url);

                //Save our results to cache (as long as it's not the error page)
                if (dlOK && page.Length != 0)
                {
                    sqlcmd = new SqlCommand("", sqlcon);

                    if (!stale)//Not stale, new cache
                        sqlcmd.CommandText = "INSERT INTO Reddit_Cache VALUES (@Url, getdate(),@Response)";
                    else//Stale, update the old cache we have
                        sqlcmd.CommandText = "UPDATE Reddit_Cache SET LastUpdate = getdate(), Response = @Response WHERE Url = @Url";
                    sqlcmd.Parameters.AddWithValue("Response", output);
                    sqlcmd.Parameters.AddWithValue("Url", reddit_url);
                    sqlcon.Open();
                    sqlcmd.ExecuteNonQuery();
                    sqlcon.Close();
                }
            }
            catch (Exception ex)
            {
                //If it's stale our output will be filled so we can at least post stale stuff
                //but if not, we need to output an error page or something
                if (output == "")
                    PageBuilder.PrintErrorPage(ref output);
            }
        }

        //Now write everything out to the page
        lblOutput.Text = output;

        string adText = "";
        PageBuilder.GenerateAd(ref adText);
        lblAd.Text = adText;
    }
}
