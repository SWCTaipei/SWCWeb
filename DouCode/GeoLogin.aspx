<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GeoLogin.aspx.cs" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>坡地管理通-帳號管理</title>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge, chrome=1">
    <link rel="stylesheet" type="text/css" href="../css/reset.css" />
    <link rel="stylesheet" type="text/css" href="../css/allSwc.css" />
    <style type="text/css">
       .style1 {
            height:400px;
            vertical-align:middle;
       }
       .style2 {   
            text-align:center;              
            height:35px;
            width:250px;
       }
       #title {
            font-family: 微軟正黑體; 
            font-size: 20px; 
            font-weight: bold; 
            color: #0000FF;
            text-align: center;
       }
    </style>
</head>
<body>
<form id="form1" runat="server">
        <div class="header">
            <a href="http://tcgeswc.taipei.gov.tw/tslmservice/" title="臺北市政府大地工程處坡地管理資料庫" class="header-link"><img src="../images/banner.jpg" alt=""/></a>
            <div class="header-menu">
                <a href="#"><img src="../images/title/menu08_B.png" alt="帳號管理" /></a>
            </div>
                
            <div class="hedaer-login" style="clear:both;">
                <span class="l-span" >現在位置：
                <a href='<%=ConfigurationManager.AppSettings["thisip"] + "tslm/" %>' style="text-decoration:none" target="_self">首頁</a> &gt;<span style="color:#CC3300"> 使用者登入</span></span>
            </div>
            <a href="#" style="display: inline-block;margin-top:20px;"><img src="../images/title/title_userLogin.png" alt=""></a>
        </div>

        <%--<div class="PageTitle">--%>
            <%--<a href="http://172.28.100.55/tslm/" title="臺北市政府大地工程處坡地管理資料庫"><img src="../image/banner.PNG" height="80" width="990" alt="" border="0" /></a>

            <div class="TitleButton">
                <ul>
                    <li><a href="#"><img src="../images/title/menu08_B.png" alt="帳號管理" /></a></li>
                </ul>
            </div>--%>

            <%--<div class="PageBar">
                <div class="TitleLink_01">現在位置：
                <a href='<%=ConfigurationManager.AppSettings["thisip"] + "tslm/" %>' style="text-decoration:none" target="_self">首頁</a> &gt;<span style="color:#CC3300"> 使用者登入</span></div>
            </div>--%>
            
            <%--<div class="Ewrap"><img src="../images/title/title_userLogin.png" alt="使用者登入" /></div>--%>
        <%--</div>--%>

        <div class="loginBox">
            <div id="title">使用者登入</div>
            <br/>
            <div class="wrapInBtn">
                帳號：<asp:TextBox ID="txtuid" runat="server" Height="21px" Width="150px" placeholder="" MaxLength="20"></asp:TextBox>
                <br/><br/>
                密碼：<asp:TextBox ID="txtpassword" runat="server" Height="21px" Width="150px" TextMode="Password" placeholder="" MaxLength="20"></asp:TextBox>
            
                <br/><br/><br/>

                <div class="loginBtn">
                    <asp:Button ID="btnenter" runat="server" OnClick="btnenter_Click" Text="登&nbsp;&nbsp;入" CssClass="loginBtnON btnOK"/>&nbsp;&nbsp;
                    <asp:Button ID="btncancel" runat="server" OnClientClick="self.location.href='http://localhost/tslm/'" Text="清&nbsp;&nbsp;除" CssClass="loginBtnON btnCancel" />
                </div>

                <div class="errorShow">
                    <asp:label ID="laberror" runat="server"></asp:label>
                </div>
                <br/>
            </div>
        </div>


</form>
</body>
</html>
