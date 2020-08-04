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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineApply004.aspx.cs" Inherits="SWCDOC_OnlineApply004" %>
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
            var iValue01 = $("#TXTONA003").val();
            var iValue02 = $("#TXTONA004").val();
            var iValue03 = $("#TXTONA012").val(); 
            var iValue04 = $("#TXTONA011").val();

            if (iValue01.trim() == "") {
                alert('請輸入預定開工日期');
                return false;
            }
            if (iValue02.trim() == "") {
                alert('請輸入預定完工日期');
                return false;
            }
            if (iValue03.trim() == "") {
                alert('請輸入目的事業工期');
                return false;
            }
            if (typeof Link013 == 'object') { } else
            {
                alert('請上傳目的事業工期附件');
                return false;
            }
            if (iValue04.trim() == "") {
                alert('請輸入開工日期');
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
                    <span><asp:Literal ID="TextUserName" runat="server">孜然養樂多，您好</asp:Literal></span>
                </div>
            </div>
        </div>
    
        
  <div class="content-s">
    <div class="startReport facilityMaintain form">
      <h1>水土保持計畫開工申報書<br>
        <br>
      </h1>

            <table class="startReport-base">
            <tr><td colspan="2" style="text-align: center; font-weight:bold;">開工申報書編號</td>
                <td colspan="4">
                    <asp:Label ID="TXTONA001" runat="server" Visible="true"/></td></tr>
            <tr><td colspan="2" style="text-align: center; font-weight:bold;">水保局編號</td>
                <td colspan="4">
                    <asp:Label ID="LBSWC002" runat="server"/>
                    <asp:Label ID="LBSWC000" runat="server" Visible="false"/></td></tr>
            <tr><td colspan="2" style="text-align: center; font-weight:bold;">申報日期</td>
                <td colspan="4">
                    <asp:Label ID="TXTONA002" runat="server"/></td></tr>
            <tr><td rowspan="3" style="font-weight:bold;">水土保持義務人</td>
                <td>姓名或名稱</td>
                <td class="bgcolorb" colspan="3" >
                    <asp:Label ID="LBSWC013" runat="server"/></td></tr>
            <tr><td>身分證或營利事業統一編號</td>
                <td class="bgcolorb" colspan="3" >
                    <asp:Label ID="LBSWC013ID" runat="server"/></td></tr>
            <tr><td>住居所或營業所</td>
                <td class="bgcolorb" colspan="3">
                    <asp:Label ID="LBSWC014" runat="server"/></td></tr>
            <tr><td rowspan="3" style="font-weight:bold;">水土保持計畫(核定本)</td>
                <td>計畫名稱</td>
                <td class="bgcolorb" colspan="3">
                    <asp:Label ID="LBSWC005" runat="server"/></td></tr>
            <tr><td>核定日期及字號</td>
                <td class="bgcolorb" colspan="3">
                    <asp:Label ID="LBSWC038" runat="server"/>
                    <asp:Label ID="LBSWC039" runat="server"/></td></tr>
            <tr><td>實施地點及土地標示</td>
                <td class="bgcolorb" colspan="3">
                    <asp:GridView ID="GVCadastral" runat="server" AutoGenerateColumns="False" CssClass="ADDCadastral"  PagerStyle-CssClass="pgr" 
                        OnPageIndexChanged="GVCadastral_PageIndexChanged" OnPageIndexChanging="GVCadastral_PageIndexChanging" PageSize="5" AllowPaging="true">
                        <Columns>
                            <asp:BoundField DataField="序號" HeaderText="序號"  />
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
            <tr><td colspan="2" style="font-weight:bold;">預定開工日期</td>
                <td style="width:30%">
                    <asp:TextBox ID="TXTONA003" runat="server" width="120px" autocomplete="off"></asp:TextBox>
                    <asp:CalendarExtender ID="TXTONA003_CalendarExtender" runat="server" TargetControlID="TXTONA003" Format="yyyy-MM-dd"></asp:CalendarExtender></td>
                <td style="width:20%;font-weight:bold;">預定完工日期</td>
                <td style="width:40%">
                    <asp:TextBox ID="TXTONA004" runat="server" width="120px" autocomplete="off"></asp:TextBox>
                    <asp:CalendarExtender ID="TXTONA004_CalendarExtender" runat="server" TargetControlID="TXTONA004" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
            <tr><td colspan="2" style="font-weight:bold;">目的事業工期</td>
                <td style="width:30%" colspan="3">
                    <asp:TextBox ID="TXTONA012" runat="server" width="120px" autocomplete="off"></asp:TextBox>
                    <asp:CalendarExtender ID="TXTONA012_CalendarExtender" runat="server" TargetControlID="TXTONA012" Format="yyyy-MM-dd"></asp:CalendarExtender><br/>
                    <br/>
                    <asp:TextBox ID="TXTONA013" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA013_fileupload" runat="server" />
                    <asp:Button ID="TXTONA013_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA013_fileuploadok_Click" />
                    <asp:Button ID="TXTONA013_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA013_fileuploaddel_Click" />
                    <br/><asp:HyperLink ID="Link013" runat="server" Target="_blank" />
                    <br/><span style="color:red;">※ 上傳格式限定為pdf、odt或doc檔案，大小請於50mb以內</span><br/></td></tr>
            <tr><td rowspan="3" style="font-weight:bold;">承辦監造技師</td>
                <td>姓名</td>
                <td class="bgcolorb">
                    <asp:Label ID="LBSWC045Name" runat="server"/></td>
                <td class="bgcolorb">執業執照字號</td>
                <td class="bgcolorb">
                    <asp:Label ID="LBSWC045OrgIssNo" runat="server"/></td></tr>
            <tr><td>事務所或公司名稱</td>
                <td class="bgcolorb">
                    <asp:Label ID="LBSWC045OrgName" runat="server"/></td>
                <td class="bgcolorb">營利事業統一編號</td>
                <td class="bgcolorb">
                    <asp:Label ID="LBSWC045OrgGUINo" runat="server"/></td></tr>
            <tr><td>事務所或公司地址</td>
                <td colspan="3" class="bgcolorb">
                    <asp:Label ID="LBSWC045OrgAddr" runat="server"/></td></tr>
            <tr><td rowspan="7" style="font-weight:bold;">檢附文件</td>
                <td></td>
                <td>1.山坡地開發利回饋金及水土保持保證金繳納證明</td>
                <td colspan="2">
                    <asp:TextBox ID="TXTONA005" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA005_fileupload" runat="server" />
                    <asp:Button ID="TXTONA005_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA005_fileuploadok_Click" />
                    <asp:Button ID="TXTONA005_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA005_fileuploaddel_Click" />
                    <br/><asp:HyperLink ID="Link005" runat="server" />
                </td></tr>
            <tr><td></td>
                <td>2.監造契約影本</td>
                <td colspan="2">
                    <asp:TextBox ID="TXTONA006" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA006_fileupload" runat="server" />
                    <asp:Button ID="TXTONA006_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA006_fileuploadok_Click" />
                    <asp:Button ID="TXTONA006_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA006_fileuploaddel_Click" />
                    <br/><asp:HyperLink ID="Link006" runat="server" /></td></tr>
            <tr><td></td>
                <td>3.現場豎立開發範圍界樁證明文件</td>
                <td colspan="2">
                    <asp:TextBox ID="TXTONA007" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA007_fileupload" runat="server" />
                    <asp:Button ID="TXTONA007_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA007_fileuploadok_Click" />
                    <asp:Button ID="TXTONA007_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA007_fileuploaddel_Click" />
                    <br/><asp:HyperLink ID="Link007" runat="server" /></td></tr>
            <tr><td></td>
                <td>4.標示或切結開挖整地範圍</td>
                <td colspan="2">
                    <asp:TextBox ID="TXTONA008" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA008_fileupload" runat="server" />
                    <asp:Button ID="TXTONA008_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA008_fileuploadok_Click" />
                    <asp:Button ID="TXTONA008_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA008_fileuploaddel_Click"/>
                    <br/><asp:HyperLink ID="Link008" runat="server" /></td></tr>
            <tr><td></td>
                <td>5.依規定樹立施工標示牌證明文件</td>
                <td colspan="2">
                    <asp:TextBox ID="TXTONA009" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA009_fileupload" runat="server" />
                    <asp:Button ID="TXTONA009_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA009_fileuploadok_Click"/>
                    <asp:Button ID="TXTONA009_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA009_fileuploaddel_Click"/>
                    <br/><asp:HyperLink ID="Link009" runat="server" /></td></tr>
            <tr><td></td>
                <td>6.災害搶救小組名冊(含行動電話)</td>
                <td colspan="2">
                    <asp:TextBox ID="TXTONA010" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA010_fileupload" runat="server" />
                    <asp:Button ID="TXTONA010_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA010_fileuploadok_Click" />
                    <asp:Button ID="TXTONA010_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA010_fileuploaddel_Click"/>
                    <br/><asp:HyperLink ID="Link010" runat="server" /></td></tr>
            <tr><td colspan="5" style="color:red; text-align:center" > ※ 上傳格式限定為pdf、odt或doc檔案，大小請於50mb以內</td></tr>
<%--            <tr><td colspan="5" style="text-align: center;border-bottom: none;">
                上開水土保持計畫訂於
                    <asp:TextBox ID="TXTONA011" runat="server" width="120px" autocomplete="off"></asp:TextBox>
                    <asp:CalendarExtender ID="TXTONA011＿CalendarExtender" runat="server" TargetControlID="TXTONA011" Format="yyyy-MM-dd"></asp:CalendarExtender>
                開工，此致</td></tr>
            <tr><td colspan="3" style="border-top: none;border-right:none;border-bottom:none;text-align: center;vertical-align: top;">臺北市政府工務局大地工程處</td>
                <td colspan="3" style="border-top: none;border-left:none;border-bottom:none;"></td></tr>
            <tr><td colspan="3" style="border-top: none;border-right:none;text-align: center;vertical-align: top;"></td>
                <td colspan="3" style="border-top: none;border-left:none;">
                    水土保持義務人：<asp:Label ID="LBSWC013a" runat="server"/><br/>
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
