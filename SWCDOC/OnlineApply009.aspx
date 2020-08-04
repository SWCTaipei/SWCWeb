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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineApply009.aspx.cs" Inherits="SWCDOC_OnlineApply009" %>
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
    <link rel="stylesheet" type="text/css" href="../css/OnlineApply001.css"/>
    <link rel="stylesheet" type="text/css" href="../css/iris.css"/>
    
    <script type="text/javascript">
        function chkInput(jChkType) {

            if (document.getElementById("Link003").innerText == '')
            {
                alert('請上傳水土保持竣工書圖及照片');
                return false;
            }
            if (document.getElementById("Link004").innerText == '') 
            {
                alert('請上傳承辦監造技師簽證竣工檢核表');
                return false;
            }
            if (document.getElementById("Link007").innerText == '')
            {
                alert('請上傳聯外排水屬抽排者，檢附專業技師簽證之查驗成果及後續管理維護計畫');
                return false;
            }
            //if (document.getElementById("TXTONA005").value == '') {
            //    alert('請輸入完工日期');
            //    return false;
            //}
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
    <div class="startReport completed form">
      <h1>水土保持計畫完工申報書<br>
        <br>
      </h1>
     

            <table class="Apply009">
            <tr><td colspan="2">完工申報編號</td>
                <td colspan="2"><asp:Label ID="TXTONA001" runat="server" Visible="true"/></td></tr>
            <tr><td colspan="2">申報日期</td>
                <td colspan="2"><asp:Label ID="TXTONA002" runat="server" Visible="true"/></td></tr>
            <tr><td rowspan="6" class="text_tabletitle">水土保持書件</td>
                <td class="bgcolorb">水保局編號</td>
                <td colspan="2" class="bgcolorb">
                    <asp:Label ID="LBSWC002" runat="server"/>
                    <asp:Label ID="LBSWC000" runat="server" Visible="false"/></td></tr>
            <tr><td class="bgcolorb">計畫名稱</td>
                <td class="bgcolorb" colspan="2">
                    <asp:Label ID="LBSWC005" runat="server"/></td></tr>
            <tr><td class="bgcolorb">核定日期及字號</td>
                <td class="bgcolorb" colspan="2">
                    <asp:Label ID="LBSWC038" runat="server"/>
                    <asp:Label ID="LBSWC039" runat="server"/></td></tr>
            <tr><td class="bgcolorb">實施地點及土地標示</td>
                <td class="bgcolorb" colspan="2"style="line-height:40px;">
                    <asp:GridView ID="GVCadastral" runat="server" AutoGenerateColumns="False" CssClass="ADDCadastral"  PagerStyle-CssClass="pgr" 
                        OnPageIndexChanged="GVCadastral_PageIndexChanged" OnPageIndexChanging="GVCadastral_PageIndexChanging" PageSize="5" AllowPaging="true">
                        <Columns>
                            <asp:BoundField DataField="序號" HeaderText="序號" />
                            <asp:BoundField DataField="區" HeaderText="區" />
                            <asp:BoundField DataField="段" HeaderText="段" />
                            <asp:BoundField DataField="小段" HeaderText="小段" />
                            <asp:BoundField DataField="地號" HeaderText="地號" />
                            <asp:BoundField DataField="土地使用分區" HeaderText="使用分區" />
                            <asp:BoundField DataField="土地可利用限度" HeaderText="可利用限度" />
                            <asp:BoundField DataField="林地類別" HeaderText="林地類別" />
                            <asp:BoundField DataField="地質敏感區" HeaderText="地質敏感區" />
                        </Columns>
                    </asp:GridView></td></tr>
            <tr><td class="bgcolorb">水土保持施工許可證日期文號</td>
                <td class="bgcolorb" colspan="2">
                    <asp:Label ID="LBSWC043" runat="server"/>
                    <asp:Label ID="LBSWC044" runat="server"/></td></tr>
            <tr><td class="bgcolorb">開工日期</td>
                <td class="bgcolorb" colspan="2"><asp:Label ID="LBSWC051" runat="server"/></td></tr>
            <tr><td rowspan="3" class="text_tabletitle"> 水土保持義務人</td>
                <td class="bgcolorb">姓名或名稱</td>
                <td class="bgcolorb"colspan="2"><asp:Label ID="LBSWC013" runat="server"/></td></tr>
            <tr><td class="bgcolorb">身分證或營利事業統一編號</td>
                <td class="bgcolorb"colspan="2"><asp:Label ID="LBSWC013ID" runat="server"/></td></tr>
            <tr><td class="bgcolorb">住居所或營業所</td>
                <td class="bgcolorb"colspan="2" style="line-height:40px;">
                    <asp:Label ID="LBSWC014" runat="server"/></td></tr>
            <tr><td rowspan="6" class="text_tabletitle">承辦監造技師</td>
                <td class="bgcolorb">姓名</td>
                <td class="bgcolorb" colspan="2">
                    <asp:Label ID="LBSWC045Name" runat="server"/></td></tr>
            <tr><td class="bgcolorb">執業機構名稱</td>
                <td class="bgcolorb" colspan="2"><asp:Label ID="LBSWC045OrgName" runat="server"/></td></tr>
            <tr><td class="bgcolorb">執業機構地址</td>
                <td class="bgcolorb" style="line-height:40px;" colspan="2">
                    <asp:Label ID="LBSWC045OrgAddr" runat="server"/></td></tr>
            <tr><td class="bgcolorb">執業執照字號</td>
                <td class="bgcolorb" colspan="2">
                    <asp:Label ID="LBSWC045OrgIssNo" runat="server"/></td></tr>
            <tr><td class="bgcolorb">營利事業統一編號</td>
                <td class="bgcolorb" colspan="2"><asp:Label ID="LBSWC045OrgGUINo" runat="server"/></td></tr>
            <tr><td class="bgcolorb">電話</td>
                <td class="bgcolorb" colspan="3"><asp:Label ID="LBSWC045OrgTel" runat="server"/></td></tr>
            <tr><td rowspan="3" class="text_tabletitle"> 檢附文件</td>
                <td>1.水土保持竣工書圖及照片</td>
                <td colspan="2">
                    <asp:TextBox ID="TXTONA003" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA003_fileupload" runat="server" />
                    <asp:Button ID="TXTONA003_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA003_fileuploadok_Click"/>
                    <asp:Button ID="TXTONA003_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA003_fileuploaddel_Click" />
                    <br/><asp:HyperLink ID="Link003" runat="server" Target="_blank" />
                    <br/><span style="color:red;">※ 上傳格式限定為pdf、odt或doc檔案，大小請於50mb以內</span><br/>
                    <asp:TextBox ID="TXTONA008" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA008_fileupload" runat="server" />
                    <asp:Button ID="TXTONA008_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA008_fileuploadok_Click" />
                    <asp:Button ID="TXTONA008_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA008_fileuploaddel_Click" />
                    <br/><asp:HyperLink ID="Link008" runat="server" Target="_blank" />
                    <br/><span style="color:red;">※ 上傳格式限定為CAD，檔案大小請於50mb以內</span><br/>
                </td></tr>
            <tr><td>2.承辦監造技師簽證竣工檢核表<br/><asp:HyperLink ID="NewUser" runat="server" Text="excel範本下載" NavigateUrl="../sysFile/計畫竣工檢核表範本.xlsx" /></td>
                <td colspan="2">
                    <asp:TextBox ID="TXTONA004" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA004_fileupload" runat="server" />
                    <asp:Button ID="TXTONA004_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA004_fileuploadok_Click"/>
                    <asp:Button ID="TXTONA004_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA004_fileuploaddel_Click" />
                    <br/><asp:HyperLink ID="Link004" runat="server" Target="_blank" />
                    <br/><span style="color:red;">※ 上傳格式限定為pdf、odt或doc檔案，大小請於50mb以內</span><br/></td></tr>
            <tr><td style="line-height:1.4">3.聯外排水屬抽排者，檢附水土保持專業技師簽證之查驗成果及後續管理維護計畫(包含至少3年期專業技師管理檢測委託契約)。<br/></td>
                <td colspan="2">
                    <asp:TextBox ID="TXTONA007" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA007_fileupload" runat="server" />
                    <asp:Button ID="TXTONA007_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA007_fileuploadok_Click"/>
                    <asp:Button ID="TXTONA007_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA007_fileuploaddel_Click" />
                    <br/><asp:HyperLink ID="Link007" runat="server" Target="_blank" />
                    <br/><span style="color:red;">※ 上傳格式限定為pdf、odt或doc檔案，大小請於50mb以內</span><br/></td></tr>

<%--            <tr><td colspan="5" style="text-align: center;border-bottom: none;">
                上開水土保持計畫訂於
                <asp:TextBox ID="TXTONA005" runat="server" width="120px" autocomplete="off"></asp:TextBox>
                <asp:CalendarExtender ID="TXTONA005_CalendarExtender" runat="server" TargetControlID="TXTONA005" Format="yyyy-MM-dd"></asp:CalendarExtender>
                完工，此致</td></tr>
            <tr><td colspan="5" style="padding-left: 50px; border-top: none;border-bottom:none;">臺北市政府工務局大地工程處 </td></tr>
            <tr><td colspan="3" style="border-top: none;text-align: right;border-right:none;"></td>
                <td colspan="1" style="border-top: none;text-align: left;border-left:none;">
                    水土保持義務人：<asp:Label ID="LBSWC013a" runat="server"/><br/><br/>
                    　承辦監造技師：<asp:Label ID="LBSWC045a" runat="server"/></td></tr>--%>

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
                            <span class="span2">建議使用IE11(含)以上，Chrome或Firefox版本瀏覽器 資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
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
