<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stats.aspx.cs" Inherits="Stats" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager runat="server" id="ScriptManager1">
        </asp:ScriptManager>
        <asp:UpdatePanel runat="server" id="UpdatePanel1">
        <ContentTemplate>
        <asp:Timer runat="server" id="Timer1" Interval="5000" OnTick="Timer1_Tick"></asp:Timer>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        <br /><br /><br />
        <asp:Button ID="btnTrash" runat="server" Text="Empty Queue" onclick="btnTrash_Click" />
        <asp:Button ID="btnTrashList" runat="server" Text="Empty Page List" onclick="btnTrashList_Click" />
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
