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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCGOV002.aspx.cs" Inherits="SWCDOC_SWCBase001" %>
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
     <link rel="stylesheet" type="text/css" href="../css/iris.css"/>
    
    <script type="text/javascript">
        function chkInput(jChkType) {
            jCHKValue01 = document.getElementById("TXTDENAME").value;
            jCHKValue02 = document.getElementById("TXTDEDATE").value;
            jCHKValue03 = document.getElementById("TXTDENTTEXT").value;

            if (jCHKValue01.trim() == '') {
                alert('請輸入事件名稱');
                return false;
            }
            if (jCHKValue02.trim() == '') {
                alert('請輸入發送日期');
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
                        <asp:Panel ID="GoTslm" runat="server" Visible="true"><li>|&nbsp&nbsp&nbsp&nbsp<a href="http://172.28.100.55/TSLM" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
                        <asp:Panel ID="TitleLink01" runat="server" Visible="false" CssClass="last-divLi"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li></asp:Panel>
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
            <div class="detailsMenu"><asp:Image ID="Image1" runat="server" ImageUrl="../images/title/btn-87.png" /></div>
                 <div class="ph-applyGrid">
                       <h2 class="detailsBar_title_basic ">基本資料</h2>

                       <div class="detailsGrid_wrap">
                        <table class="ph-table">
                    <tr><th>流水號</th>
                        <th><asp:Label ID="TXTDENO" runat="server" /></th></tr>
                    <tr><th>通知事件<span style="color: red;font-family:cursive;">＊</span></th>
                        <th><asp:DropDownList ID="DDLDETYPE" runat="server" /></th></tr>
                    <tr><th>案件狀態<span style="color: red;font-family:cursive;">＊</span></th>
                        <th><asp:CheckBoxList ID="CHKSWCSTATUS" runat="server" RepeatColumns="3" /></th></tr>
                    <tr><th>通知對象<span style="color: red;font-family:cursive;">＊</span></th>
                        <th><asp:CheckBoxList ID="CHKSENDMBR" runat="server" RepeatColumns="3" /></th></tr>
                    <tr><th>通知管道<span style="color: red;font-family:cursive;">＊</span></th>
                        <th><asp:CheckBoxList ID="CHKSENDFUN" runat="server" RepeatColumns="3" style="border:none;" /></th></tr>
                    <tr><th>事件名稱<span style="color: red;font-family:cursive;">＊</span></th>
                        <th><asp:TextBox ID="TXTDENAME" runat="server" MaxLength="50" style="width:200px;"/></th></tr>
                    </table>

                    <table style="border:1px solid #000000; border-top:none;  line-height:1.5; width:1200px;">
                    <%--<tr><td>發送日期<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTDEDATE" runat="server" width="120px" Style="margin-right: 0px;"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTDEDATE_CalendarExtender" runat="server" TargetControlID="TXTDEDATE" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>--%>
                    <tr><td  style="border-right:1px solid #000000; width:215px; padding:20px;  text-align:center; font-weight:bold">通知內容<span style="color: red;font-family:cursive; " >＊</span></td>
                        <td style="padding:10px;"><asp:TextBox ID="TXTDENTTEXT" runat="server" TextMode="MultiLine" Height="80" Width="99%" onkeyup="textcount(this,'TXTDENTTEXT_count','300');" /><br/>
                            <asp:Label ID="TXTDENTTEXT_count" runat="server" Text="(0/300)" ForeColor="Red" style="padding:10px;"/></td></tr>
                        
          </table>
        </div>
                     </div>

            <br/>


            

            <div class="checklist-btn" style="width:250px;">
                <asp:Button ID="DataLock" runat="server" Text="確認送出" OnClientClick="return chkInput('');" />
                <asp:Button ID="SaveCase" runat="server" Text="暫時儲存" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" />
                <asp:Button ID="GoHomePage" runat="server" Text="返回瀏覽案件" OnClick="GoHomePage_Click" />
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
