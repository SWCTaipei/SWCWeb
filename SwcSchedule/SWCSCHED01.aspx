<%@ Page Title="恢復審查排程" Language="C#" AutoEventWireup="true" CodeFile="SWCSCHED01.aspx.cs" Inherits="SwcSchedule_SWCSCHED01" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>臺北市水土保持申請書件管理平台</title>
    <link rel="Shortcut Icon" type="image/x-icon" href="../images/logo-s.ico">
    <meta name="keywords" content="臺北市水土保持申請書件管理平台, 書件管理平台">
    <meta name="description" content="臺北市水土保持申請書件管理平台">
    <meta name="author" content="dobubu">
    <meta name="copyright" content="© 2017 臺北市水土保持申請書件管理平台">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge, chrome=1">
    <link rel="stylesheet" type="text/css" href="../css/reset.css"/>
    <link rel="stylesheet" type="text/css" href="../css/all.css"/>
    <link rel="stylesheet" type="text/css" href="../css/OnlineApply001.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<asp:Button ID="BTN01" runat="server" Text="找出案件且寄信(先不改狀態,單純測寄信)" OnClick="BTN01_Click"/>
		<asp:Label ID="LB01" runat="server" />
    </div>
    </form>
</body>
</html>
