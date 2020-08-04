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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineApply005.aspx.cs" Inherits="SWCDOC_OnlineApply005" %>

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
    <link rel="stylesheet" type="text/css" href="../css/OnlineApply001.css"/>
    
    <script type="text/javascript">
        function chkInput(jChkType) {
            var i = 0;

            if (document.getElementById("CHKBOXONA003").checked) { i++; }
            if (document.getElementById("CHKBOXONA004").checked) { i++; }
            if (document.getElementById("CHKBOXONA005").checked) { i++; }
            if (document.getElementById("CHKBOXONA006").checked) { i++; }
            if (document.getElementById("CHKBOXONA007").checked) { i++; }
            if (document.getElementById("CHKBOXONA008").checked) { i++; }
            if (document.getElementById("CHKBOXONA009").checked) { i++; }
            if (document.getElementById("CHKBOXONA010").checked) { i++; }

            if (i < 1)
            {
                alert('請勾選申請調整項目');
                return false;
            }
            if (typeof Link011 == 'object')
            {
                if (document.getElementById("Link011").innerText == '') {
                    alert('請上傳承辦監造技師確認安全無虞之簽證說明書');
                    return false;
                }
            }
            else
            {
                alert('請上傳承辦監造技師確認安全無虞之簽證說明書');
                return false;
            }
            if (jChkType == 'DataLock') {
                var r = confirm('確認送出後，即不可修改，請再次確認是否要完成送出。');
                return r;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
    <asp:ScriptManager ID="ScriptManager1" runat="server"/>

    <div class="wrap-s">
        <div class="header-wrap-s">
            <div class="header header-s clearfix"><a href="SWC001.aspx" class="logo-s"></a>
                <div class="header-menu-s">
                    <ul>
                        <li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
                        <li>|</li>
                        <li><a href="http://tcgeswc.taipei.gov.tw/index_new.aspx" title="水土保持計畫查詢系統" target="_blank">水土保持計畫查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="http://172.28.100.55/TSLM" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
                        <asp:Panel ID="TitleLink00" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWCBase001.aspx" title="帳號管理">帳號管理</a></li></asp:Panel>
                        <asp:Panel ID="LogOutLink" runat="server" Visible="true"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li></asp:Panel>
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
    <div class="review form">
      <h1>水土保持計畫設施調整報備<br>
        <br>
      </h1>
          
                <table class="review-out">
                <tr><td>設施調整報備編號</td>
                    <td><asp:Label ID="TXTONA001" runat="server" Visible="true"/></td></tr>
                <tr><td>水保局編號</td>
                    <td><asp:Label ID="LBSWC002" runat="server"/>
                        <asp:Label ID="LBSWC000" runat="server" Visible="false"/></td></tr>
                <tr><td>計畫名稱</td>
                    <td><asp:Label ID="LBSWC005" runat="server"/></td></tr>
                <tr><td>調整報備項目是否屬永久設施</td>
                    <td><asp:DropDownList ID="DDLONA002" runat="server" Height="25px"/>
                        <span style="color:red;padding:10px;">※(是者須經目的事業主管機關核轉)</span></td></tr>
                <tr><td>申請調整項目</td>
                    <td style="line-height:40px;">
                        <asp:CheckBox ID="CHKBOXONA003" runat="server" Text=" 一、道路開發面積增減未超過原計畫10%" /><br/>
                        <asp:CheckBox ID="CHKBOXONA004" runat="server" Text=" 二、目的事業開發配置調整，未涉及計畫面積變更且無變更開挖整地位置及水保設施" /><br/>
                        <asp:CheckBox ID="CHKBOXONA005" runat="server" Text=" 三、各單項水保設施，其計量單位之數量增減不超過20%者" /><br/>
                        <asp:CheckBox ID="CHKBOXONA006" runat="server" Text=" 四、地形、地質與原設計不符，原水保設施仍可發揮正常功能" /><br/>
                        <asp:CheckBox ID="CHKBOXONA007" runat="server" Text=" 五、變更水保設施位置者，原水保設施仍可發揮正常功能" /><br/>
                        <asp:CheckBox ID="CHKBOXONA008" runat="server" Text=" 六、變更水保設施之構造物斷面及通水斷面，面積增加不超過20%或減少不超過10%，且不影響原構造物正常功能" /><br/>
                        <asp:CheckBox ID="CHKBOXONA009" runat="server" Text=" 七、因應實際需要，依水土保持技術規範增設必要臨時防災措施" /><br/>
                        <asp:CheckBox ID="CHKBOXONA010" runat="server" Text=" 八、其它未涉及變更水保開挖整地位置及水保設施之調整報備事項，如純屬建築配置變更、二樓板勘驗前完工限制解列等" /><br/>
                    </td></tr>
                <tr><td> 承辦監造技師確認安全無虞之簽證說明書 </td>
                    <td><asp:TextBox ID="TXTONA011" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTONA011_fileupload" runat="server" />
                        <asp:Button ID="TXTONA011_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA011_fileuploadok_Click" />
                        <asp:Button ID="TXTONA011_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA011_fileuploaddel_Click" />
                        <br/><asp:HyperLink ID="Link011" runat="server" Target="_blank" /><br/>
                        <span style="color:red;">※ 上傳格式限定為pdf或doc檔案，大小請於50mb以內</span>
                    </td></tr>
                <tr><td colspan="2">
                        <asp:CheckBox ID="CHKBOXONA012" runat="server" Text="經確認符合水土保持計畫審核監督辦法第19條變更設計且安全無虞。" /></td></tr>
                                    </table>
      
                    <div class="form-btn">
                        <asp:Button ID="DataLock" runat="server" Text="確認送出" OnClientClick="return chkInput('DataLock');" OnClick="DataLock_Click" />&nbsp&nbsp
                        <asp:Button ID="SaveCase" runat="server" Text="暫時儲存" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" />&nbsp&nbsp
                        <asp:Button ID="GoHomePage" runat="server" Text="返回瀏覽案件" OnClick="GoHomePage_Click" />
                    </div>
        </div>
    </div>



            <div class="footer">
                 <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                           <span class="span2">110臺北市信義區松德路300號3樓 　服務專線(02)27591109   臺北市民當家熱線1999</span><br/>
                            <span class="span2">建議使用IE8.0(含)以上，Chrome或Firefox版本瀏覽器 資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                           <span class="span2">客服電話：02-27593001#3718 許先生 本系統由多維空間資訊有限公司開發維護 TEL:(02)27929328</span></p>
            </div>

        </div>

        <asp:Literal ID="error_msg" runat="server"></asp:Literal>































        <script src="../js/jquery-3.1.1.min.js"></script>
        <script src="../js/inner.js"></script>
        <script src="../js/allhref.js"></script>
    </div>
    </form>
</body>
</html>
