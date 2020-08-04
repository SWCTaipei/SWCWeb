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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCDT001v.aspx.cs" Inherits="SWCDOC_SWCDT001" %>
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
        function chkInput(jChkType) {
            jCHKValue01 = document.getElementById("TXTDTL002").value;
            jCHKValue02 = document.getElementById("TXTDTL003").value;
            jCHKValue03 = document.getElementById("TXTDTL004").value;
            //jCHKValue04 = document.getElementById("TXTSWC015").value;
            //jCHKValue05 = document.getElementById("TXTSWC016").value;

            if (jCHKValue01.trim() == '') {
                alert('請輸入審查日期');
                document.getElementById("TXTDTL002").focus();
                return false;
            }
            if (jCHKValue02.trim() == '') {
                alert('請輸入補正期限');
                document.getElementById("TXTDTL003").focus();
                return false;
            }
            if (jCHKValue03.trim() == '') {
                alert('請輸入主旨');
                document.getElementById("TXTDTL004").focus();
                return false;
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
            <div class="review form">
                <h1>審查表單<br/><br/></h1>
                <table class="review-out">
                    <tr><td>審查表單編號</td>
                        <td><asp:Label ID="LBDTL001" runat="server"/>
                            <asp:Label ID="LBSWC000" runat="server" Visible="false"/></td></tr>
                    <tr><td>補正期限</td>
                        <td><asp:Label ID="TXTDTL003" runat="server" width="120px"></asp:Label></td></tr>
                    <tr><td>主旨</td>
                        <td><asp:Label ID="TXTDTL004" runat="server" /></td></tr>
                    <tr><td>函送日期</td>
                        <td><asp:Label ID="TXTSENDDATE" runat="server"/></td></tr>
                </table>

                <br /><br />

                <table class="review-text">
                <tr><td>申請案件名稱</td>
                    <td><asp:Label ID="LBSWC005" runat="server"/></td></tr>
                <tr><td>會議次別</td>
                    <td><asp:Label ID="DDLDTL006" runat="server" Height="25px"/></td></tr>
                <tr><td>會議時間</td>
                    <td><asp:Label ID="TXTDTL007" runat="server" width="120px"></asp:Label></td></tr>
                <tr><td>會議地點</td>
                    <td><asp:Label ID="TXTDTL008" runat="server" width="100%" MaxLength="100" /></td></tr>
                <tr><td>主席姓名</td>
                    <td><asp:Label ID="TXTDTL009" runat="server" width="100%" MaxLength="20" /></td></tr>
                <tr><td>出席姓名</td>
                    <td><asp:Label ID="TXTDTL010" runat="server" width="100%" MaxLength="100" /></td></tr>
                <tr><td>列席人員姓名</td>
                    <td><asp:Label ID="TXTDTL011" runat="server" width="100%" MaxLength="20" /></td></tr>
                <tr><td>記錄人員姓名</td>
                    <td><asp:Label ID="TXTDTL012" runat="server" width="100%" MaxLength="20" /></td></tr>
                <tr><td>報告事項之案由及決定</td>
                    <td><asp:Label ID="TXTDTL013" runat="server" /></td></tr>
                <tr><td>討論事項之案由及決議</td>
                    <td><asp:Label ID="TXTDTL014" runat="server"/></td></tr>
                <tr><td>臨時動議之案由及決議</td>
                    <td><asp:Label ID="TXTDTL015" runat="server" /></td></tr>
                <tr><td>其他應行記載之事項</td>
                    <td><asp:Label ID="TXTDTL016" runat="server" /></td></tr></table>

                <table class="review-imgUpload">
                <tr><td>相關單位及人員簽名<br/><br/>
                        <asp:Label ID="TXTDTL017" runat="server" /></td>
                    <td><asp:Image ID="TXTDTL018_img" runat="server" CssClass="imgUpload-l80" Visible="false" /><br/>
                        <asp:HyperLink ID="HyperLink018" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink><br/><br/>
                        <asp:TextBox ID="TXTDTL018" runat="server" Width="70px" Visible="False" /></td></tr>
                
                <tr><td colspan="2"><asp:Label ID="LBSWC005_2" runat="server"/>審查意見</td></tr>
                <tr><td colspan="2">審查意見<br/><br/>
                        <asp:Label ID="TXTDTL019" runat="server" /></td></tr></table>

                <table class="review-excelUpload">
                <tr><td style="width:250px;">查核表上傳</td>
                    <td><asp:HyperLink ID="Link020" runat="server" />
                        <asp:TextBox ID="TXTDTL020" runat="server" Width="70px" Visible="False" /></td></tr>
                </table>
                
                <br/><br/>
                
                <table class="checkRecord-fileUpload">
                <tr><td colspan="2">
                        <asp:Label ID="LBSWC005a" runat="server" CssClass="redn"/></td></tr>
                <tr><td>現場相片一</td>
                    <td>現場相片二</td></tr>
                <tr><td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL021" runat="server" Width="70px" Visible="False" />
                        </div>
                        <asp:Image ID="TXTDTL021_img" runat="server" CssClass="imgUpload" Visible="False" />
                        <asp:HyperLink ID="HyperLink021" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink><br/><br/>
                        <br/>
                        <asp:Label ID="TXTDTL022" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText" /><br/></td>
                    <td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL023" runat="server" Width="70px" Visible="False" />
                        </div>
                        <asp:Image ID="TXTDTL023_img" runat="server" CssClass="imgUpload" Visible="False" />
                        <asp:HyperLink ID="HyperLink023" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink><br/><br/>
                        <br/>
                        <asp:Label ID="TXTDTL024" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText" /><br/></td></tr>
                <tr><td>現場相片三</td>
                    <td>現場相片四</td></tr>
                <tr><td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL025" runat="server" Width="70px" Visible="False" />
                        </div>
                        <asp:Image ID="TXTDTL025_img" runat="server" CssClass="imgUpload" Visible="False" />
                        <asp:HyperLink ID="HyperLink025" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink><br/><br/>
                        <br/>
                        <asp:Label ID="TXTDTL026" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText" /><br/></td>
                    <td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL027" runat="server" Width="70px" Visible="False" />
                        </div>
                        <asp:Image ID="TXTDTL027_img" runat="server" CssClass="imgUpload" Visible="False" />
                        <asp:HyperLink ID="HyperLink027" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink><br/><br/>
                        <br/>
                        <asp:Label ID="TXTDTL028" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText" /><br/></td></tr>
                <tr><td>現場相片五</td>
                    <td>現場相片六</td></tr>
                <tr><td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL029" runat="server" Width="70px" Visible="False" />
                        </div>
                        <asp:Image ID="TXTDTL029_img" runat="server" CssClass="imgUpload" Visible="False" />
                        <asp:HyperLink ID="HyperLink029" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink><br/><br/>
                        <br/>
                        <asp:Label ID="TXTDTL030" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText" /><br/></td>
                    <td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL031" runat="server" Width="70px" Visible="False" />
                        </div>
                        <asp:Image ID="TXTDTL031_img" runat="server" CssClass="imgUpload" Visible="False" />
                        <asp:HyperLink ID="HyperLink031" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink><br/><br/>
                        <br/>
                        <asp:Label ID="TXTDTL032" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText" /><br/></td></tr></table>

                <div class="form-btn">
                    <asp:Button ID="GoHomePage" runat="server" Text="返回案件詳情" OnClick="GoHomePage_Click" />
                </div>
            </div>
        </div>




















<%--        <div class="footer-s">
            <div class="footer-s-green"></div>
                <div class="footer-b-brown">
                    <p> <span class="span1">臺北市政府工務局大地工程處</span><br><span class="span2">110臺北市信義區松德路300號3樓 　(02)27593001   臺北市民當家熱線1999</span><br><span class="span2">資料更新：2017.5.19　來訪人數：123456789 </span></p>
                </div>
            </div>--%>

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
