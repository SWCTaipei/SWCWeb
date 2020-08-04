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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCGOV003.aspx.cs" Inherits="SWCDOC_SWCBase001" %>
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
     <link rel="stylesheet" type="text/css" href="css/min.css"/>
    
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
                <div class="applyGrid">
                    <h2 class="detailsBar_title_basic">基本資料</h2>
          
                    <table>
                    <tr><td>流水號</td>
                        <td><asp:Label ID="TXTDENO" runat="server" /></td></tr>
                    <tr><td>通知事件<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:Label ID="DDLDETYPE" runat="server" /></td></tr>
                    <tr><td>案件狀態<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:Label ID="CHKSWCSTATUS" runat="server" RepeatColumns="3" /></td></tr>
                    <tr><td>通知對象<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:Label ID="CHKSENDMBR" runat="server" RepeatColumns="3" /></td></tr>
                    <tr><td>通知管道<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:Label ID="CHKSENDFUN" runat="server" RepeatColumns="3" /></td></tr>
                    <tr><td>事件名稱<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:Label ID="TXTDENAME" runat="server" MaxLength="50" style="width:200px;"/></td></tr>
                    </table>
                    <table class="cgPassn">
                    <tr><td>通知內容<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:Label ID="TXTDENTTEXT" runat="server" TextMode="MultiLine" Height="100" Width="100%" onkeyup="textcount(this,'TXTDENTTEXT_count','300');" /></td></tr>
                        
          </table>
        </div>

            <br/>


            
   <div  class="skybluebtn"><a href=".html"><img src="img/btn/btn-81.png" /></a>
      <div style="margin-bottom:1em;"></div>
        <table class="ADDGridView AutoNewLine" cellpadding="0" style="border-collapse:collapse; width:100%; font-family:'微軟正黑體'">
     <tr>
       <td colspan="7" style="background-color:#a0d8ef;color:#000; font-size:19px; font-weight:bold;">發送詳情</td>    
      </tr>
      <tr>
       <th>水保案件編號</th>
       <th>案件名稱</th>
       <th>義務人</th>
       <th>變更技師</th>
       <th>未送出</th>
       <th>連結表單</th>
      </tr>
      <tr>
       <td>DP1060405001</td>
       <td>山竹颱風</td>
       <td>陳小明</td>
       <td>王包子</td>
       <td><img src="../images/icon/red.png" width="42" height="42"/></td>
       <td><input  type="button" value="連結表單" onclick="location.href=''">
        </td>
       </tr>
       <tr>
       <td>DP1060405001</td>
       <td>山竹颱風</td>
       <td>陳小明</td>
       <td>王包子</td>
       <td><img src="../images/icon/red.png" width="42" height="42" /></td>
       <td><input type="submit" value="連結表單" onclick="location.href=''">
        </td>
       </tr>
       <tr>
       <td>DP1060405001</td>
       <td>山竹颱風</td>
       <td>陳小明</td>
       <td>王包子</td>
       <td><img src="../images/icon/red.png" width="42" height="42" /></td>
       <td><input type="submit" value="連結表單" onclick="location.href=''">
        </td>
       </tr>
       <tr>
       <td>DP1060405001</td>
       <td>山竹颱風</td>
       <td>陳小明</td>
       <td>王包子</td>
       <td><img src="../images/icon/red.png" width="42" height="42" /></td>
       <td><input type="submit" value="連結表單" onclick="location.href=''">
        </td>
       </tr>
       <tr>
       <td>DP1060405001</td>
       <td>山竹颱風</td>
       <td>陳小明</td>
       <td>王包子</td>
       <td><img src="../images/icon/red.png" width="42" height="42" /></td>
       <td><input type="submit" value="連結表單" onclick="location.href=''">
        </td>
       </tr>
      </table>
      

 </div>


            <div class="checklist-btn">
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
