<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Utilize Synergy of Business Fundamentals</title>
    <link href="StyleSheet.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript">
    function ToggleDiv(id_of_div)
    {
        myDiv = document.getElementById(id_of_div);

        if (myDiv.style.display == 'none') {
            myDiv.style.display = 'block';
            if(document.getElementById(id_of_div + 'dotdotdot') != null)
                document.getElementById(id_of_div + 'dotdotdot').style.display = 'none';
        }
        else {
            myDiv.style.display = 'none';
            if (document.getElementById(id_of_div + 'dotdotdot') != null)
                document.getElementById(id_of_div + 'dotdotdot').style.display = 'block';
        }
    }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="lblOutput" runat="server"></asp:Label>
    <br />
    <asp:Label ID="lblAd" runat="server"></asp:Label>
    </div>
    </form>
    
    <!-- Start of StatCounter Code
    <script type="text/javascript">
    var sc_project=6134667;
    var sc_invisible=1;
    var sc_security="a629935b";
    </script>
    
    <script type="text/javascript"
    src="http://www.statcounter.com/counter/counter.js"></script><noscript><div
    class="statcounter"><a title="joomla statistics"
    href="http://www.statcounter.com/joomla/" target="_blank"><img
    class="statcounter" src="http://c.statcounter.com/6134667/0/a629935b/1/"
    alt="joomla statistics" ></a></div></noscript>
    End of StatCounter Code -->    
</body>
</html>
