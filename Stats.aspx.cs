using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeReddit;

public partial class Stats : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = "lastGrab: " + Global.lastGrab.ToString() + "<br />";
        Label1.Text += "urlQ Count: " + Global.urlQueue.Count.ToString() + "<br />";
        Label1.Text += "spiderQ Count: " + Global.spiderQueue.Count.ToString() + "<br />";
        Label1.Text += "reddit Requests: " + Global.redditRequests.Count.ToString() + "<br />";
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        Label1.Text = "lastGrab: " + Global.lastGrab.ToString() + "<br />";
        Label1.Text += "urlQ Count: " + Global.urlQueue.Count.ToString() + "<br />";
        foreach (string str in Global.urlQueue)
            Label1.Text += "&nbsp;&nbsp;&nbsp;" + str + "<br />";
        Label1.Text += "spiderQ Count: " + Global.spiderQueue.Count.ToString() + "<br />";
        Label1.Text += "reddit Requests: " + Global.redditRequests.Count.ToString() + "<br />";
        foreach (string str in Global.redditRequests)
            Label1.Text += "&nbsp;&nbsp;&nbsp;" + str + "<br />";
    }


    protected void btnTrash_Click(object sender, EventArgs e)
    {
        Global.urlQueue.Clear();
        Global.spiderQueue.Clear();
    }
    protected void btnTrashList_Click(object sender, EventArgs e)
    {
        Global.redditRequests.Clear();
    }
}
