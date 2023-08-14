<%@ Page Language="VB" AutoEventWireup="false" CodeFile="mailcheck.aspx.vb" Inherits="mailcheck" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="refresh" content="3600" />
</head>
<body>
    <form id="form1" runat="server">
        
    <div>
        <asp:Panel ID="Panel1" runat="server">
            <asp:CheckBox ID="mailunsend" runat="server" Text ="純檢查，不寄信" Checked="false" />
            <asp:CheckBox ID="mailonecheck" runat="server" Text ="不管日期寄給技師" Checked="false" />
            <asp:Button ID="startnow" runat="server" Text="現在開始" /><br />
            <asp:Button ID="Button1" runat="server" Text="檢查圖6-1 7-1的路徑並更新資料庫" />
            <br />
            <asp:Button ID="Button2" runat="server" Text="發測試簡訊給美伶" Visible="false" />
        </asp:Panel>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        
        
    </div>
    </form>
</body>
</html>
