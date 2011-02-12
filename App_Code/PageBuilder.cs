using System;
using System.Collections.Generic;
using System.Web;
using CodeReddit;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for PageBuilder
/// </summary>
public class PageBuilder
{

	public PageBuilder()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void GenerateAd(ref string output)
    {
        int rand = new Random().Next();


        output += "<span class=\"keyword\">public class</span> <span class=\"class\">Unsolicited_Advertisement</span><br />";
        output += "{<br />";
        output += "<div class=\"indent\">";

        if (rand % 7 == 0)
        {
            output += "<span class=\"keyword\">string</span> desc = <span class=\"string\">\"Bacon.\"</span><br />";
            output += "<span class=\"keyword\">string</span> url = <span class=\"string\">\"<a class=\"string\" href=\"http://amzn.com/B00126ESJU/?tag=codereddit-20\">http://amzn.com/B00126ESJU/</a>\"</span><br />";

        }
        else if (rand % 7 == 1)
        {
            output += "<span class=\"keyword\">string</span> desc = <span class=\"string\">\"We have done the impossible, and that makes us mighty.\"</span><br />";
            output += "<span class=\"keyword\">string</span> url = <span class=\"string\">\"<a class=\"string\" href=\"http://amzn.com/B0000AQS0F/?tag=codereddit-20\">http://amzn.com/B0000AQS0F/</a>\"</span><br />";
        }
        else if (rand % 7 == 2)
        {
            output += "<span class=\"keyword\">string</span> desc = <span class=\"string\">\"Death by snu-snu.\"</span><br />";
            output += "<span class=\"keyword\">string</span> url = <span class=\"string\">\"<a class=\"string\" href=\"http://amzn.com/B00013RBX0/?tag=codereddit-20\">http://amzn.com/B00013RBX0/</a>\"</span><br />";
        }
        else if (rand % 7 == 3)
        {
            output += "<span class=\"keyword\">string</span> desc = <span class=\"string\">\"You take the blue pill, the story ends. You take the red pill, you stay in Wonderland, and I show you how deep the rabbit hole goes.\"</span><br />";
            output += "<span class=\"keyword\">string</span> url = <span class=\"string\">\"<a class=\"string\" href=\"http://amzn.com/B001NXBRJG/?tag=codereddit-20\">http://amzn.com/B001NXBRJG/</a>\"</span><br />";
        }
        else if (rand % 7 == 4)
        {
            output += "<span class=\"keyword\">string</span> desc = <span class=\"string\">\"eBook Reader + Project Gutenberg = Books for life.\"</span><br />";
            output += "<span class=\"keyword\">string</span> url = <span class=\"string\">\"<a class=\"string\" href=\"http://amzn.com/B002FQJT3Q/?tag=codereddit-20\">http://amzn.com/B002FQJT3Q/</a>\"</span><br />";
        }
        else if (rand % 7 == 5)
        {
            output += "<span class=\"keyword\">string</span> desc = <span class=\"string\">\"Magnets. How do they work?\"</span><br />";
            output += "<span class=\"keyword\">string</span> url = <span class=\"string\">\"<a class=\"string\" href=\"http://amzn.com/B003D7CFTQ/?tag=codereddit-20\">http://amzn.com/B003D7CFTQ/</a>\"</span><br />";
        }
        else if (rand % 7 == 6)
        {
            output += "<span class=\"keyword\">string</span> desc = <span class=\"string\">\"Then who was phone?\"</span><br />";
            output += "<span class=\"keyword\">string</span> url = <span class=\"string\">\"<a class=\"string\" href=\"http://amzn.com/B002SBHUVG/?tag=codereddit-20\">http://amzn.com/B002SBHUVG/?tag=codereddit-20</a>\"</span><br />";
        }
        

        output += "</div>";
        output += "}<br />";
    }

    public static void Process_TooFast(ref string output)
    {
        output += "<span class=\"keyword\">public class</span> <span class=\"class\">Too_Many_Requests</span><br />";
        output += "{<br />";
        output += "<div class=\"indent\">";
        output += "<span class=\"keyword\">double</span> Do_Maths(<span class=\"keyword\">int</span> iteration)<br />";
        output += "{<br />";
        output += "<div class=\"indent\">";
        output += "<span style=\"cursor:pointer\" onclick=\"javascript:history.go(-1);\" class=\"keyword\">return</span> 1 + iteration / (2.0 * iteration + 1) * Do_Maths(iteration + 1);<br />";
        output += "</div>";
        output += "}<br />";
        output += "<br />";
        output += "<span class=\"keyword\">protected void</span> Page_Load(<span class=\"keyword\">object</span> sender, <span class=\"class\">EventArgs</span> e)<br />";
        output += "{<br />";
        output += "<div class=\"indent\">";
        output += "<span class=\"keyword\">double</span> z = 6378.137; <span class=\"comment\">//km</span><br />";
        output += "<span class=\"keyword\">double</span> a = 6356.7523; <span class=\"comment\">//km</span><br />";
        output += "<span class=\"keyword\">double</span> pi = 2 * Do_Maths(1);<br />";
        output += "<span class=\"keyword\">double</span> v = 4.0 / 3.0 * pi * z * z * a; <span class=\"comment\">//cubic km.</span><br />";
        output += "<br />";
        output += "<span class=\"comment\">/*<br />";
        output += "Our robot monkey butler has stubbed his toe while retrieving your page. Please be patient, his estimated arrival is in " + (Global.urlQueue.Count * Global.MIN_CRAWL_INTERVAL).ToString() + " seconds. You can visit less obscure pages in the meantime like the <a class=\"comment\" href=\"/\">front page</a><br />";
        output += "*/</span><br />";
        output += "<br />";

        //Add in some sort of indication that it will be the second attempt so we can stop and not constantly loop over bad pages
        string rawUrl = HttpContext.Current.Request.RawUrl;
        int idx = rawUrl.IndexOf('?');
        if(idx == -1)
            rawUrl += "?attempted=1";
        else
            rawUrl = rawUrl.Insert(idx + 1,"attempted=1&");

        output += "<script language=\"Javascript\">setTimeout(\"location.href='" + System.Web.HttpUtility.HtmlEncode(rawUrl) + "';\"," + (1000 * Global.urlQueue.Count * Global.MIN_CRAWL_INTERVAL).ToString() + ");</script>";
        output += "Response.Write(v.ToString());";
        output += "<br />";
        output += "<span style=\"cursor:pointer\" onclick=\"javascript:history.go(-1);\" class=\"keyword\">return</span>;<br />";
        output += "</div>";
        output += "}<br />";
        output += "</div>";
        output += "}<br />";
    }

    public static void PrintErrorPage(ref string output)
    {
        output += "<span class=\"keyword\">public class</span> <span class=\"class\">Page_Error</span><br />";
        output += "{<br />";
        output += "<div class=\"indent\">";
        output += "<span class=\"keyword\">double</span> Do_Maths(<span class=\"keyword\">int</span> iteration)<br />";
        output += "{<br />";
        output += "<div class=\"indent\">";
        output += "<span style=\"cursor:pointer\" onclick=\"javascript:history.go(-1);\" class=\"keyword\">return</span> 1 + iteration / (2.0 * iteration + 1) * Do_Maths(iteration + 1);<br />";
        output += "</div>";
        output += "}<br />";
        output += "<br />";
        output += "<span class=\"keyword\">protected void</span> Page_Load(<span class=\"keyword\">object</span> sender, <span class=\"class\">EventArgs</span> e)<br />";
        output += "{<br />";
        output += "<div class=\"indent\">";
        output += "<span class=\"keyword\">double</span> z = 6378.137; <span class=\"comment\">//km</span><br />";
        output += "<span class=\"keyword\">double</span> a = 6356.7523; <span class=\"comment\">//km</span><br />";
        output += "<span class=\"keyword\">double</span> pi = 2 * Do_Maths(1);<br />";
        output += "<span class=\"keyword\">double</span> v = 4.0 / 3.0 * pi * z * z * a; <span class=\"comment\">//cubic km.</span><br />";
        output += "<br />";
        output += "<span class=\"comment\">//There was a problem loading the site, please try again later</span><br />";
        output += "<br />";

        output += "<img src=\"/overflow.gif\" /><br />";
        output += "<br />";
        output += "Response.Write(v.ToString());";
        output += "<br />";

        output += "<span style=\"cursor:pointer\" onclick=\"javascript:history.go(-1);\" class=\"keyword\">return</span>;<br />";
        output += "</div>";
        output += "}<br />";
        output += "</div>";
        output += "}<br />";
    }

    public static void Process_Topics(ref string page, ref string output, string reddit_url)
    {
        //trim any querystring variables off our reddit_url so we don't do things like
        // /r/reddit.com/?after=blah?after=blah2
        int idx = reddit_url.IndexOf('?');
        if (idx > 0)
            reddit_url = reddit_url.Substring(0, idx);

        //For List pages
        JavaScriptSerializer ser = new JavaScriptSerializer();
        Thing thing = ser.Deserialize<Thing>(page.Replace("\"replies\": \"\"", "\"replies\": {}"));


        //Help us to title case the titles
        TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

        //Make a regex for cleaning titles into nice function names
        Regex regEx = new Regex("[^a-zA-Z0-9 ]");

        string title = "";
        string author = "";
        string date = "";
        string ups = "";
        string downs = "";
        string net_ups = "";
        string subreddit = "";
        string over_18 = "";
        string selftext = "";
        string name = "";
        string id = "";
        string num_comments = "";
        string url = "";
        string is_self = "";

        //Replace these words with functions that generate unique names
        string lit_title = "full_title";
        string lit_upvote = "upvotes";
        string lit_comment = "Load_Comments";


        PrintIncludes(ref output);


        output += "<span class=\"keyword\">public class</span> <span class=\"class\">Whats_Hot</span><br />";
        output += "{<br />";
        output += "<div class=\"indent\">";

        foreach (Thing child in thing.data.children)
        {
            title = regEx.Replace(child.data.title, "").Replace(' ', '_');
            title = myTI.ToTitleCase(title);
            author = child.data.author;
            date = new DateTime(1970, 1, 1).AddSeconds(child.data.created_utc).ToString();
            ups = child.data.ups;
            downs = child.data.downs;
            net_ups = child.data.score;
            subreddit = child.data.subreddit;
            over_18 = child.data.over_18;
            selftext = child.data.selftext;
            name = child.data.name;
            id = child.data.id;
            num_comments = child.data.num_comments;
            url = child.data.url;
            is_self = child.data.is_self;

            //If it's a self post, we should redirect the url to ourselves so the user doesn't jump out to full reddit
            if (is_self == "True")
                url = "?url=" + System.Web.HttpUtility.HtmlEncode(url + ".json");

            output += "<span class=\"comment\">//" + date + " : <a class=\"comment\" href=\"?url=" + System.Web.HttpUtility.HtmlEncode("http://www.reddit.com/user/" + author + "/" + ".json") + "\">" + author + "</a></span><br />";
            output += "<span class=\"keyword\">protected void</span> <a href=\"" + url + "\">" + title.Substring(0, Math.Min(20, title.Length)) + "</a>(<span class=\"keyword\">Category</span> <a href=\"?url=" + System.Web.HttpUtility.HtmlEncode("http://www.reddit.com/r/" + subreddit + "/" + ".json") + "\">" + subreddit + "</a> , ";
            if (over_18 == "True")
                output += "<span class=\"class\">Boolean</span> IsAdult, <br />";
            output += "<span class=\"class\">EventArgs</span> e)<br />";
            output += "{<br />";
            output += "<div class=\"indent\">";
            output += "<span class=\"keyword\">string </span>" + lit_title + " = <span class=\"string\">\"<a class=\"string\" href=\"" + url + "\">" + title + "</a>\"</span>;<br />";
            output += "<span class=\"keyword\">int </span>" + lit_upvote + " = +" + ups + "-" + downs + ";<br />";
            output += "<span class=\"class\">Debug</span>.Assert(" + lit_upvote + " == " + net_ups + ");<br />";

            if (selftext != null && selftext != "")
            {
                output += "<span style=\"cursor:pointer;\" onclick=\"javascript:ToggleDiv('" + id + "')\"><span class=\"keyword\">#region</span> selfText</span><br />";
                output += "<div id=\"" + id + "\" style=\"display:none\">";
                output += "<span class=\"comment\">/*<br />";
                output += selftext + "<br />";
                output += "*/</span><br />";
                output += "<span class=\"keyword\">#endregion</span><br />";
                output += "</div>";
            }

            output += "<br />";
            output += "<a href=\"?url=" + System.Web.HttpUtility.HtmlEncode("http://www.reddit.com/r/" + subreddit + "/comments/" + id + "/" + ".json") + "\">" + lit_comment + "(" + num_comments + ");</a><br />";

            output += "<br />";
            output += "<span style=\"cursor:pointer\" onclick=\"javascript:history.go(-1);\" class=\"keyword\">return</span>;<br />";
            output += "</div>";
            output += "}<br /><br />";
        }

        output += "<span class=\"keyword\">public virtual void</span> <a href=\"?url=" + System.Web.HttpUtility.HtmlEncode(reddit_url + "?after=" + thing.data.after) + "\">Next_Page()</a> {}";

        output += "</div>";
        output += "}<br />";

    }

    public static void Process_Comments(ref string page, ref string output)
    {

        //For comment pages
        JavaScriptSerializer ser = new JavaScriptSerializer();
        Thing[] things = ser.Deserialize<Thing[]>(page.Replace("\"replies\": \"\"", "\"replies\": {}"));
        Thing child = things[0].data.children[0]; //The start post is the first child
        Thing reply_thing = things[1];

        //Help us to title case the titles
        TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

        //Make a regex for cleaning titles into nice function names
        Regex regEx = new Regex("[^a-zA-Z0-9 ]");

        string title = "";
        string author = "";
        string date = "";
        string ups = "";
        string downs = "";
        string net_ups = "";
        string subreddit = "";
        string over_18 = "";
        string selftext = "";
        string name = "";
        string id = "";
        string num_comments = "";
        string url = "";
        string is_self = "";

        //Replace these words with functions that generate unique names
        string lit_title = "full_title";
        string lit_upvote = "upvotes";
        string lit_comment = "Load_Comments";

        PrintIncludes(ref output);

        title = regEx.Replace(child.data.title, "").Replace(' ', '_');
        title = myTI.ToTitleCase(title);
        author = child.data.author;
        date = new DateTime(1970, 1, 1).AddSeconds(child.data.created_utc).ToString();
        ups = child.data.ups;
        downs = child.data.downs;
        net_ups = child.data.score;
        subreddit = child.data.subreddit;
        over_18 = child.data.over_18;
        selftext = child.data.selftext;
        name = child.data.name;
        id = child.data.id;
        num_comments = child.data.num_comments;
        url = child.data.url;
        is_self = child.data.is_self;

        //If it's a self post, we should redirect the url to ourselves so the user doesn't jump out to full reddit
        if (is_self == "True")
            url = "?url=" + System.Web.HttpUtility.HtmlEncode(url + ".json");

        output += "<span class=\"comment\">//" + date + " : <a class=\"comment\" href=\"?url=" + System.Web.HttpUtility.HtmlEncode("http://www.reddit.com/user/" + author + "/" + ".json") + "/\">" + author + "</a></span><br />";
        output += "<span class=\"keyword\">public class</span> <span class=\"class\"><a href=\"" + url + "\">" + title.Substring(0, Math.Min(20, title.Length)) + "</a></span> : <a href=\"?url=" + System.Web.HttpUtility.HtmlEncode("http://www.reddit.com/r/" + subreddit) + "/" + ".json" +"\">" + subreddit + "</a><br />";
        output += "{<br />";
        output += "<div class=\"indent\">";

        output += "<span class=\"keyword\">public string </span>contributors;<br />";
        output += "<span class=\"keyword\">public int </span>tmpScore;<br />";
        output += "<span class=\"keyword\">public DateTime </span>tmpDate;<br />";
        output += "<span class=\"keyword\">public string </span>" + lit_title + ";<br />";
        output += "<span class=\"keyword\">public int </span>" + lit_upvote + ";<br />";
        output += "<span class=\"keyword\">public bool</span> IsAdult; <br />";

        output += "<br />";
        output += "<span class=\"comment\">//Constructor</span><br />";
        output += "<span class=\"keyword\">public </span> <span class=\"class\"><a href=\"" + url + "\">" + title.Substring(0, Math.Min(20, title.Length)) + "()</a></span><br />";
        output += "{<br />";
        output += "<div class=\"indent\">";
        output += "contributors = <span class=\"string\">\"\"</span>;<br />";
        output += "tmpScore = <span class=\"keyword\">new </span><span class=\"class\">Random()</span>.Next();<br />";
        output += "tmpDate = <span class=\"keyword\">null</span>;<br />";
        output += lit_title + " = <span class=\"string\">\"<a class=\"string\" href=\"" + url + "\">" + title + "</a>\"</span>;<br />";
        output += lit_upvote + " = +" + ups + "-" + downs + ";<br />";
        if (over_18 == "True")
            output += "IsAdult = <span class=\"keyword\">true</span>;<br />";
        else
            output += "IsAdult = <span class=\"keyword\">false</span>;<br />";

        output += "<span class=\"class\">Debug</span>.Assert(" + lit_upvote + " == " + net_ups + ");<br />";

        if (selftext != null && selftext != "")
        {
            output += "<span style=\"cursor:pointer;\" onclick=\"javascript:ToggleDiv('" + id + "')\"><span class=\"keyword\">#region</span> selfText</span><br />";
            output += "<div id=\"" + id + "\" style=\"display:none\">";
            output += "<span class=\"comment\">/*<br />";
            output += selftext + "<br />";
            output += "*/</span><br />";
            output += "<span class=\"keyword\">#endregion</span><br />";
            output += "</div>";
        }

        output += "<br />";
        output += "</div>";
        output += "}<br /><br />";

        //Header for replies function
        output += "<span class=\"keyword\">public void</span> Load_Comments(<span class=\"keyword\">int</span> num_comments)<br />";
        output += "{<br />";
        output += "<div class=\"indent\">";
        output += "<span class=\"class\">Debug</span>.Assert(num_comments == " + num_comments + ");<br />";
        output += "<br />";

        //Print out all of the replies
        PrintReplies(reply_thing, ref output, 0);

        //Footer for replies function
        output += "</div>";
        output += "}<br />";

        output += "</div>";
        output += "}<br />";

    }

    public static void Process_User(ref string page, ref string output, string reddit_url)
    {
        //trim any querystring variables off our reddit_url so we don't do things like
        // /r/reddit.com/?after=blah?after=blah2
        int idx = reddit_url.IndexOf('?');
        if (idx > 0)
            reddit_url = reddit_url.Substring(0, idx);

        //For User pages
        JavaScriptSerializer ser = new JavaScriptSerializer();
        Thing thing = ser.Deserialize<Thing>(page.Replace("\"replies\": \"\"", "\"replies\": {}"));

        //Help us to title case the titles
        TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

        //Make a regex for cleaning titles into nice function names
        Regex regEx = new Regex("[^a-zA-Z0-9 ]");

        string title = "";
        string body = "";
        string author = "";
        string date = "";
        string ups = "";
        string downs = "";
        string net_ups = "";
        string subreddit = "";
        string over_18 = "";
        string selftext = "";
        string name = "";
        string id = "";
        string num_comments = "";
        string url = "";
        string is_self = "";
        string link_id = "";

        //Replace these words with functions that generate unique names
        string lit_title = "full_title";
        string lit_upvote = "upvotes";
        string lit_comment = "Load_Comments";


        PrintIncludes(ref output);


        output += "<span class=\"keyword\">public class</span> <span class=\"class\">" + thing.data.children[0].data.author + "</span><br />";
        output += "{<br />";
        output += "<div class=\"indent\">";

        foreach (Thing child in thing.data.children)
        {
            if (child.data.title != null)
            {
                title = regEx.Replace(child.data.title, "").Replace(' ', '_');
                title = myTI.ToTitleCase(title);
            }
            else
            {
                title = regEx.Replace(child.data.link_title, "").Replace(' ', '_');
                title = myTI.ToTitleCase(title);
            }

            if (child.data.body != null)
                body = child.data.body;

            author = child.data.author;
            date = new DateTime(1970, 1, 1).AddSeconds(child.data.created_utc).ToString();
            ups = child.data.ups;
            downs = child.data.downs;
            if (child.data.score != null)
                net_ups = child.data.score;
            else
                net_ups = (int.Parse(ups) - int.Parse(downs)).ToString();
            subreddit = child.data.subreddit;
            over_18 = child.data.over_18;
            selftext = child.data.selftext;
            name = child.data.name;
            id = child.data.id;
            num_comments = child.data.num_comments;
            url = child.data.url;
            is_self = child.data.is_self;
            if (child.data.link_id != null)
                link_id = child.data.link_id.Substring(3, child.data.link_id.Length-3); //need to chop off the preceding "type"
            else
                link_id = "whyistherenolinkid?";

            //If it's a self post, we should redirect the url to ourselves so the user doesn't jump out to full reddit
            if (is_self == "True")
                url = "?url=" + System.Web.HttpUtility.HtmlEncode(url + ".json");

            output += "<span class=\"comment\">//" + date + " : <a class=\"comment\" href=\"?url=" + System.Web.HttpUtility.HtmlEncode("http://www.reddit.com/user/" + author + "/" + ".json") + "\">" + author + "</a></span><br />";
            output += "<span class=\"keyword\">protected void</span> <a href=\"?url=" + System.Web.HttpUtility.HtmlEncode("http://www.reddit.com/r/" + subreddit + "/comments/" + link_id + "/" + ".json") + "\">" + title.Substring(0, Math.Min(20, title.Length)) + "</a>(<span class=\"keyword\">Category</span> <a href=\"?url=" + System.Web.HttpUtility.HtmlEncode("http://www.reddit.com/r/" + subreddit + "/" + ".json") + "\">" + subreddit + "</a> , ";
            if (over_18 == "True")
                output += "<span class=\"class\">Boolean</span> IsAdult, <br />";
            output += "<span class=\"class\">EventArgs</span> e)<br />";
            output += "{<br />";
            output += "<div class=\"indent\">";
            output += "<span class=\"keyword\">string </span>" + lit_title + " = <span class=\"string\">\"<a class=\"string\" href=\"?url=" + System.Web.HttpUtility.HtmlEncode("http://www.reddit.com/r/" + subreddit + "/comments/" + link_id + "/" + ".json") + "\">" + title + "</a>\"</span>;<br />";
            output += "<span class=\"comment\">/*<br />";
            output += body + "</a>;<br />";
            output += "*/</span><br />";
            output += "<span class=\"keyword\">int </span>" + lit_upvote + " = +" + ups + "-" + downs + ";<br />";
            output += "<span class=\"class\">Debug</span>.Assert(" + lit_upvote + " == " + net_ups + ");<br />";

            output += "<br />";
            output += "<span style=\"cursor:pointer\" onclick=\"javascript:history.go(-1);\" class=\"keyword\">return</span>;<br />";
            output += "</div>";
            output += "}<br /><br />";
        }

        output += "<span class=\"keyword\">public virtual void</span> <a href=\"?url=" + System.Web.HttpUtility.HtmlEncode(reddit_url + "?after=" + thing.data.after) + "\">Next_Page()</a> {}";

        output += "</div>";
        output += "}<br />";
    }

    public static void PrintReplies(Thing parent, ref string output, int depth)
    {
        //Short circuit at depth 20
        if (depth > 20 || parent == null || parent.data == null)
            return;

        for (int i = 0; i < parent.data.children.Count; i++)
        {
            Thing reply = parent.data.children[i];

            if (reply.data.body != null)
            {
                string date = new DateTime(1970, 1, 1).AddSeconds(reply.data.created_utc).ToString();

                int score = 0;
                if (reply.data.ups != null && reply.data.ups != "")
                    score += int.Parse(reply.data.ups);
                if (reply.data.downs != null && reply.data.downs != "")
                    score -= int.Parse(reply.data.downs);

                output += "<span onclick=\"javascript:ToggleDiv('" + reply.data.id + "');\" class=\"keyword\" style=\"cursor:pointer;\">";
                if (i != 0)
                    output += "else ";
                output += "if</span> (tmpScore == " + score.ToString() + ")<br />";
                output += "{<br />";
                output += "<span id=\"" + reply.data.id + "\">";//Hider span
                output += "<div class=\"indent\">";
                output += "<span class=\"comment\">/*<br />";
                output += reply.data.body + "<br />";
                output += "*/</span><br />";
                output += "contributors += <span class=\"string\">\"<a class=\"string\" href=\"?url=" + System.Web.HttpUtility.HtmlEncode("http://www.reddit.com/user/" + reply.data.author + "/" + ".json") + "\">" + reply.data.author + "</a>\"</span>; ";
                output += "tmpDate = <span class=\"class\">DateTime</span>.Parse(<span class=\"string\">\"" + date + "\"</span>);<br />";
                output += "<br />";

                //Recurse down the reply tree!
                PrintReplies(reply.data.replies, ref output, depth + 1);

                output += "</div>";
                output += "</span>";//Close the hider span
                output += "<span id=\"" + reply.data.id + "dotdotdot\" style=\"display:none;border:1px solid #CCCCCC\">...</span>";//Close the hider span
                output += "}";//Close the if tmpScore
                output += "<br />";//Close the if tmpScore
            }
        }
    }

    public static void PrintIncludes(ref string output)
    {
        output += "<span class=\"keyword\">using</span> System;<br />";
        output += "<span class=\"keyword\">using</span> System.Collections.Generic;<br />";
        output += "<span class=\"keyword\">using</span> System.Diagnostics;<br />";
        output += "<span class=\"keyword\">using</span> System.IO;<br />";
        output += "<span class=\"keyword\">using</span> System.Web.Script.Serialization;<br />";
        output += "<span class=\"keyword\">using</span> System.Web;<br />";
        output += "<span class=\"keyword\">using</span> System.Web.UI;<br />";
        output += "<span class=\"keyword\">using</span> System.Net;<br />";
        output += "<span class=\"keyword\">using</span> System.Text.RegularExpressions;<br /><br />";
        
        output += "<span class=\"keyword\">using</span> <a href=\"/\">CodeReddit.Home;</a><br />";
        output += "<br />";
    }
}
