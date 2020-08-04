<!--
    Soil and Water Conservation Platform Project is a web applicant tracking system which allows citizen can search, view and manage their SWC applicant case.
    Copyright (C) <2020>  <Geotechnical Engineering Office, Public Works Department, Taipei City Government>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or any later version.
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
-->

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCGOV012.aspx.cs" Inherits="SWCDOC_SWCBase001" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

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
    
    <script type="text/javascript">
        function chkInput(jChkType) {
            jCHKValue01 = document.getElementById("TXTBBDateStart").value;
            jCHKValue02 = document.getElementById("TXTBBDateEnd").value;

            if (jCHKValue01.trim() == '') {
                alert('請輸入公告日期');
                return false;
            } else {
                return dateValidationCheck(jCHKValue01);
            }
            if (jCHKValue02.trim() == '') {
                alert('請輸入公告日期');
                return false;
            } else {
                return dateValidationCheck(jCHKValue02);
            }

            if (jCHKValue03.trim() == '') {
                alert('請輸入通知內容');
                return false;
            }

        }
        function textcount(txtobj, labobj, txtcount) {
            var textboxtemp = txtobj;
            var labeltemp = document.getElementById(labobj);

            if (window.event.keyCode == 13) {
                window.event.keyCode = null;
            }
            var ttval = textboxtemp.value.length;
            if (ttval > parseInt(txtcount)) {
                window.event.keyCode = null;
                textboxtemp.innerText = textboxtemp.value.substring(0, parseInt(txtcount));
                ttval = textboxtemp.value.length;
                labeltemp.innerText = "(" + ttval + "/" + txtcount + ")";
            }
            else {
                labeltemp.innerText = "(" + ttval + "/" + txtcount + ")";
            }
        }
    </script>
    <script src="../js/BaseNorl.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"/>

    <div class="wrap-s">
        <div class="header-wrap-s">
            <div class="header header-s clearfix"><a href="../SWCDOC/SWC001.aspx" class="logo-s"></a>
                <div class="header-menu-s">
                    <ul>
                        <li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
                        <li>|</li>
                        <li><a href="http://tcgeswc.taipei.gov.tw/index_new.aspx" title="水土保持計畫查詢系統" target="_blank">水土保持計畫查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="http://172.28.100.55/TSLM" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
                        <asp:Panel ID="TitleLink00" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="../SWCDOC/SWCBase001.aspx" title="帳號管理">帳號管理</a></li></asp:Panel>
                        <asp:Panel ID="GOVMG" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="../SWCTCGOV/SWCGOV001.aspx" title="系統管理">系統管理</a><ul><li><a href="../SWCTCGOV/SWCGOV001.aspx">防災事件通知</a></li><li><a href="../SWCTCGOV/SWCGOV011.aspx">公佈欄</a></li></ul></li></asp:Panel>
                        <li>|</li>
                        <li><a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li>
                    </ul>
                </div>
            </div>

            <div class="header-s-green">
                <div class="header-s-green-nameWrap">
                    <span><asp:Literal ID="TextUserName" runat="server">大雷包，您好</asp:Literal></span>
                </div>
            </div>
        </div>
        
        <div class="content-s">
            <div class="detailsMenu"><asp:Image ID="Image1" runat="server" ImageUrl="../images/title/bbd.png" /></div>
                <div class="ph-applyGrid" >
                    <h2 class="detailsBar_title_basic">公佈欄</h2>
          
                    <table class="ph-applyGrid">
                    <tr><th>流水號</th>
                        <th><asp:Label ID="TXTBBNO" runat="server" /></th></tr>
                    <tr><th>公告日期<span style="color: red;font-family:cursive;">＊</span></th>
                        <th><asp:TextBox ID="TXTBBDateStart" runat="server" width="120px" Style="margin-right: 0px;" autocomplete="off"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTBBDateStart_CalendarExtender" runat="server" TargetControlID="TXTBBDateStart" Format="yyyy-MM-dd"></asp:CalendarExtender>
                            ~
                            <asp:TextBox ID="TXTBBDateEnd" runat="server" width="120px" Style="margin-right: 0px;" autocomplete="off"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTBBDateEnd_CalendarExtender" runat="server" TargetControlID="TXTBBDateEnd" Format="yyyy-MM-dd"></asp:CalendarExtender></th></tr>
                    <tr><th>公告主旨<span style="color: red;font-family:cursive;">＊</span></th>
                        <th><asp:TextBox ID="TXTBBTitle" runat="server" Style="margin-right: 0px;" autocomplete="off" MaxLength="100"/></th></tr>
                    <tr><th>公告內容<span style="color: red;font-family:cursive;">＊</span></th>
                        <th><asp:TextBox ID="TXTBBText" runat="server" TextMode="MultiLine" Height="100" Width="100%" onkeyup="textcount(this,'TXTBBText_count','300');" /><br/>
                            <asp:Label ID="TXTBBText_count" runat="server" Text="(0/300)" ForeColor="Red" style="padding:10px;"/></th></tr>
                    <tr><th>是否顯示<span style="color: red;font-family:cursive;">＊</span></th>
                        <th><asp:RadioButtonList ID="RABBShow" runat="server" RepeatColumns="2" /></th></tr>
                    <tr><th>公佈單位<span style="color: red;font-family:cursive;">＊</span></th>
                        <th><asp:DropDownList ID="DDLBBUnit" runat="server" /></th></tr>
                    </table>
        </div>

            <br/>


            

            <div class="checklist-btn" style="width:250px;">
                <asp:Button ID="SaveCase" runat="server" Text="資料存檔" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" />
                <asp:Button ID="GoHomePage" runat="server" Text="返回列表" OnClick="GoHomePage_Click" />
            </div>
        </div>
        


            <%--<div class="footer-s">
                <div class="footer-s-green"></div>
                <div class="footer-b-brown">
                    <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                       <span class="span2">110臺北市信義區松德路300號3樓 　(02)27593001   臺北市民當家熱線1999</span><br/>
                       <span class="span2">資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span></p>
                </div>
            </div>--%>
        
            <div class="footer">
                 <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                    <span class="span2">110臺北市信義區松德路300號3樓 　服務專線(02)27591109   臺北市民當家熱線1999</span><br/>
                    <span class="span2">建議使用IE11(含)以上，Chrome或Firefox版本瀏覽器 資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                    <span class="span2">客服電話：02-27593001#3718 許先生 本系統由多維空間資訊有限公司開發維護 TEL:(02)27929328</span></p>
            </div>

        <asp:Literal ID="error_msg" runat="server"></asp:Literal>

        <script src="../js/jquery-3.1.1.min.js"></script>
        <script src="../js/inner.js"></script>
        <script src="../js/allhref.js"></script>
    </div>
    </form>
</body>
</html>
