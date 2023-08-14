<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReSMS.aspx.cs" Inherits="TestFolder_ReSMS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            電話號碼：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br/>
            簡訊內容：<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br/><br/>
            <asp:Button ID="Button1" runat="server" Text="發送簡訊" OnClick="Button1_Click" />

        </div>
    </form>
</body>
</html>
