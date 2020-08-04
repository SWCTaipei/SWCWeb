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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineApply001.aspx.cs" Inherits="SWCDOC_OnlineApply001" %>
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
        function SetRadioSel()
        {
            //08：09、10
            if(document.all.RaONA008b.checked)
            {
                document.getElementById("RaONA009a").disabled = true;
                document.getElementById("RaONA009b").disabled = true;
                document.getElementById("RaONA010a").disabled = true;
                document.getElementById("RaONA010b").disabled = true;

                document.getElementById("RaONA009a").checked = false;
                document.getElementById("RaONA009b").checked = false;
                document.getElementById("RaONA010a").checked = false;
                document.getElementById("RaONA010b").checked = false;
            }
            else
            {
                document.getElementById("RaONA009a").disabled = false;
                document.getElementById("RaONA009b").disabled = false;
                document.getElementById("RaONA010a").disabled = false;
                document.getElementById("RaONA010b").disabled = false;
            }

            //11：12、13     ************************************************************
            if (document.all.RaONA011b.checked) {
                document.getElementById("RaONA012a").disabled = true;
                document.getElementById("RaONA012b").disabled = true;
                document.getElementById("RaONA013a").disabled = true;
                document.getElementById("RaONA013b").disabled = true;

                document.getElementById("RaONA012a").checked = false;
                document.getElementById("RaONA012b").checked = false;
                document.getElementById("RaONA013a").checked = false;
                document.getElementById("RaONA013b").checked = false;
            }
            else {
                document.getElementById("RaONA012a").disabled = false;
                document.getElementById("RaONA012b").disabled = false;
                document.getElementById("RaONA013a").disabled = false;
                document.getElementById("RaONA013b").disabled = false;
            }

            //14：15、16、17 ************************************************************
            if (document.all.RaONA014b.checked) {
                document.getElementById("RaONA015a").disabled = true;
                document.getElementById("RaONA015b").disabled = true;
                document.getElementById("RaONA016a").disabled = true;
                document.getElementById("RaONA016b").disabled = true;
                document.getElementById("RaONA017a").disabled = true;
                document.getElementById("RaONA017b").disabled = true;
                document.getElementById("RaONA017c").disabled = true;

                document.getElementById("RaONA015a").checked = false;
                document.getElementById("RaONA015b").checked = false;
                document.getElementById("RaONA016a").checked = false;
                document.getElementById("RaONA016b").checked = false;
                document.getElementById("RaONA017a").checked = false;
                document.getElementById("RaONA017b").checked = false;
                document.getElementById("RaONA017c").checked = false;
            }
            else {
                document.getElementById("RaONA015a").disabled = false;
                document.getElementById("RaONA015b").disabled = false;
                document.getElementById("RaONA016a").disabled = false;
                document.getElementById("RaONA016b").disabled = false;
                document.getElementById("RaONA017a").disabled = false;
                document.getElementById("RaONA017b").disabled = false;
                document.getElementById("RaONA017c").disabled = false;
            }

            //18：19、20     ************************************************************
            if (document.all.RaONA018b.checked) {
                document.getElementById("RaONA019a").disabled = true;
                document.getElementById("RaONA019b").disabled = true;
                document.getElementById("RaONA020a").disabled = true;
                document.getElementById("RaONA020b").disabled = true;

                document.getElementById("RaONA019a").checked = false;
                document.getElementById("RaONA019b").checked = false;
                document.getElementById("RaONA020a").checked = false;
                document.getElementById("RaONA020b").checked = false;
            }
            else {
                document.getElementById("RaONA019a").disabled = false;
                document.getElementById("RaONA019b").disabled = false;
                document.getElementById("RaONA020a").disabled = false;
                document.getElementById("RaONA020b").disabled = false;
            }
        }
        function chkInput(jChkType) {
            var vData01 = $("#TXTSWC002").val();
            var vData02 = $("#TXTONA002").val();
            var vData03 = $("#TXTONA003").val();
            var vData04 = $("#TXTONA004").val();
            var vData05 = $("#TXTONA005").val();
            var vData06 = $("#TXTONA006").val();
            var vData07 = $("#TXTONA007").val();

            if (typeof TXTSWC002 == 'object')
            {
                if (vData01.trim() == "") {
                    alert('請輸入水保局編號');
                    return false;
                }
            }

            if (vData02.trim() == "") {
                alert('請輸入檢查日期');
                return false;
            }
            if (vData03.trim() == "") {
                alert('請輸入社區(設施)地址');
                return false;
            }
            if (vData04.trim() == "") {
                alert('請輸入義務人(聯絡人)');
                return false;
            }
            if (vData05.trim() == "") {
                alert('請輸入聯絡地址');
                return false;
            }
            if (vData06.trim() == "") {
                alert('請輸入聯絡電話');
                return false;
            }
            if (vData07.trim() == "") {
                alert('請輸入行動電話');
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
                        <asp:Panel ID="LogOutLink" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li></asp:Panel>
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
            <div class="facilityMaintain form">
                <h1>臺北市山坡地水土保持設施安全自主檢查表<br/><br/></h1>
                
                <table class="facilityMaintain-out">
                    <tr><td>安全自主檢查表編號</td>
                        <td><asp:Label ID="LBONA001" runat="server" />
                            <asp:Label ID="LBSWC000" runat="server" Visible="false"/>
                        </td></tr>
                    <tr><td>水保局編號<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTSWC002" runat="server" MaxLength="14" width="200px"/>
                            <%--<asp:TextBox ID="TXTSWC002" runat="server" MaxLength="12" placeholder="(如不知可由市府填寫)" width="200px"/>--%>
                            <asp:Label ID="LBSWC002" runat="server" Visible="false" />
                        </td></tr>
                    <tr><td>檢查日期<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTONA002" runat="server" width="120px" autocomplete="off"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTONA002_CalendarExtender" runat="server" TargetControlID="TXTONA002" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                    <tr><td>社區(設施)地址<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTONA003" runat="server" width="100%" onkeyup="textcount(this,'TXTONA003_count','100');" MaxLength="100" />
                            <asp:Label ID="TXTONA003_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /></td></tr>
                    <tr><td>義務人(聯絡人)<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTONA004" runat="server" width="100%" onkeyup="textcount(this,'TXTONA004_count','100');" MaxLength="100" />
                            <asp:Label ID="TXTONA004_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /></td></tr>
                    <tr><td>聯絡地址<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTONA005" runat="server" width="100%" onkeyup="textcount(this,'TXTONA005_count','100');" MaxLength="100" />
                            <asp:Label ID="TXTONA005_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /></td></tr>
                    <tr><td>聯絡電話<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTONA006" runat="server" width="100%" onkeyup="textcount(this,'TXTONA006_count','100');" MaxLength="100" />
                            <asp:Label ID="TXTONA006_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /></td></tr>
                    <tr><td>行動電話<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTONA007" runat="server" width="100%" onkeyup="textcount(this,'TXTONA007_count','100');" MaxLength="100" />
                            <asp:Label ID="TXTONA007_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /></td></tr>
                </table>

                <br/><br/>
                
                <table class="OA01-T2">
                <tr><th colspan="4">檢查項目與名稱</th>
                    <th>檢查及改善結果</th></tr>
                <tr class="tr-title">
                    <td>一、滯洪沉砂設施</td>
                    <td><asp:RadioButton ID="RaONA008a" GroupName="ONA008" runat="server" Text="有此設施" onchange="SetRadioSel();" /></td>
                    <td><asp:RadioButton ID="RaONA008b" GroupName="ONA008" runat="server" Text="無此設施" onchange="SetRadioSel();" /></td>
                    <td></td><td></td>
                </tr>
                    <%--<tr><td></td><td></td><td></td><td style="width:200px;"></td><td></td></tr>--%>
                <tr><td class="td-padding" colspan="4" style="width:50%;">1.池內是否淤積？</td>
                    <td style="text-align:left;width:50%;">
                    <asp:RadioButton ID="RaONA009a" GroupName="ONA009" runat="server" Text="是" /><br/>
                    <asp:RadioButton ID="RaONA009b" GroupName="ONA009" runat="server" Text="否" /></td></tr>
                <tr><td class="td-padding" colspan="4" style="width:50%;">2.進水及排放處是否堵塞？</td>
                    <td style="text-align:left;width:50%;">
                    <asp:RadioButton ID="RaONA010a" GroupName="ONA010" runat="server" Text="是" /><br/>
                    <asp:RadioButton ID="RaONA010b" GroupName="ONA010" runat="server" Text="否" /></td></tr>
                <tr class="tr-title">
                    <td>二、排水設施</td>
                    <td><asp:RadioButton ID="RaONA011a" GroupName="ONA011" runat="server" Text="有此設施" onchange="SetRadioSel();" /></td>
                    <td><asp:RadioButton ID="RaONA011b" GroupName="ONA011" runat="server" Text="無此設施" onchange="SetRadioSel();" /></td>
                    <td></td><td></td>
                </tr>
                <tr><td class="td-padding" colspan="4">1.排水溝是否損壞？</td>
                    <td style="text-align:left;">
                    <asp:RadioButton ID="RaONA012a" GroupName="ONA012" runat="server" Text="是" /><br/>
                    <asp:RadioButton ID="RaONA012b" GroupName="ONA012" runat="server" Text="否" /></td></tr>
                <tr><td class="td-padding" colspan="4">2.排水溝是否雜物淤積？</td>
                    <td style="text-align:left;">
                    <asp:RadioButton ID="RaONA013a" GroupName="ONA013" runat="server" Text="是" /><br/>
                    <asp:RadioButton ID="RaONA013b" GroupName="ONA013" runat="server" Text="否" /></td></tr>
                <tr class="tr-title">
                    <td>三、邊坡保護設施 </td>
                    <td><asp:RadioButton ID="RaONA014a" GroupName="ONA014" runat="server" Text="有此設施" onchange="SetRadioSel();" /></td>
                    <td><asp:RadioButton ID="RaONA014b" GroupName="ONA014" runat="server" Text="無此設施" onchange="SetRadioSel();" /></td>
                    <td></td><td></td>
                </tr>
                <tr><td class="td-padding" colspan="4">1.是否外凸變形？</td>
                    <td style="text-align:left;">
                    <asp:RadioButton ID="RaONA015a" GroupName="ONA015" runat="server" Text="是" /><br/>
                    <asp:RadioButton ID="RaONA015b" GroupName="ONA015" runat="server" Text="否" /></td></tr>
                <tr><td class="td-padding" colspan="4">2.是否龜裂？</td>
                    <td style="text-align:left;">
                    <asp:RadioButton ID="RaONA016a" GroupName="ONA016" runat="server" Text="是" /><br/>
                    <asp:RadioButton ID="RaONA016b" GroupName="ONA016" runat="server" Text="否" /></td></tr>
                <tr><td class="td-padding" colspan="4">3.排水孔是否堵塞？</td>
                    <td style="text-align:left;">
                    <asp:RadioButton ID="RaONA017a" GroupName="ONA017" runat="server" Text="是" /><br/>
                    <asp:RadioButton ID="RaONA017b" GroupName="ONA017" runat="server" Text="否" /><br/>
                    <asp:RadioButton ID="RaONA017c" GroupName="ONA017" runat="server" Text="無排水孔" /></td></tr>
                <tr class="tr-title">
                    <td>四、抽水設施</td>
                    <td><asp:RadioButton ID="RaONA018a" GroupName="ONA018" runat="server" Text="有此設施" onchange="SetRadioSel();" /></td>
                    <td><asp:RadioButton ID="RaONA018b" GroupName="ONA018" runat="server" Text="無此設施" onchange="SetRadioSel();" /></td>
                    <td></td><td></td>
                </tr>
                <tr><td class="td-padding" colspan="4">1.是否功能正常？</td>
                    <td style="text-align:left;">
                    <asp:RadioButton ID="RaONA019a" GroupName="ONA019" runat="server" Text="是" /><br/>
                    <asp:RadioButton ID="RaONA019b" GroupName="ONA019" runat="server" Text="否" /></td></tr>
                <tr><td class="td-padding" colspan="4">2.是否有定期維修保養檢查及記錄？</td>
                    <td style="text-align:left;">
                    <asp:RadioButton ID="RaONA020a" GroupName="ONA020" runat="server" Text="是" /><br/>
                    <asp:RadioButton ID="RaONA020b" GroupName="ONA020" runat="server" Text="否" /></td></tr>
                <tr class="tr-title">
                    <td>五、其他</td>
                    <td></td>
                    <td></td>
                    <td></td><td></td>
                </tr>
                <tr><td class="td-padding" colspan="4">1.是否需要專業技師現場指導</td>
                    <td style="text-align:left;">
                    <asp:RadioButton ID="RaONA021a" GroupName="ONA021" runat="server" Text="是" />，
                    說明：<asp:TextBox ID="TXTONA022" runat="server" Width="150px" MaxLength="50" /><br/>
                    <asp:RadioButton ID="RaONA021b" GroupName="ONA021" runat="server" Text="否" /></td></tr>
                <tr><td class="td-padding" colspan="4">2.設施淤積、堵塞與龜裂…等異常徵兆是否規劃進行改善?</td>
                    <td style="text-align:left;">
                    <asp:RadioButton ID="RaONA023a" GroupName="ONA024" runat="server" Text="是" />，
                    規劃於
                    <asp:TextBox ID="TXTONA024" runat="server" width="120px"></asp:TextBox>
                    <asp:CalendarExtender ID="TXTONA024_CalendarExtender" runat="server" TargetControlID="TXTONA024" Format="yyyy-MM-dd"></asp:CalendarExtender>
                    改善完成<br/>
                    <asp:RadioButton ID="RaONA023b" GroupName="ONA024" runat="server" Text="否" /></td></tr>
                <tr><td>檢查人員</td>
                    <td colspan="4"><asp:TextBox ID="TXTONA025" runat="server" Width="100%" MaxLength="100" /></td>
                </tr>
                </table>
                
            <br/>

      <div style="color:red";> 備註：<br/>
        <br/>
        1.	檢查時機<br/>
        <br/>
        <span style="padding-left:15px;">(1)	每年於五月一日前至少1次維修、檢查，並維持正常運作，若有損壞或阻塞，應立即修繕及清淤。<br></span>
        <br/>
        <span style="padding-left:15px;">(2)	於中央氣象局發布北部區域列入海上颱風警報警戒範圍或豪雨警報以上等級後，應自行檢查清淤以維持功能。<br></span>
        <br/>
        2.	前項之維修、檢查應製成紀錄並保存五年，供市府抽查。 </div>





                <div class="form-btn">
                    <asp:Button ID="DataLock" runat="server" Text="送出" OnClientClick="return chkInput('DataLock');" Visible="false" OnClick="DataLock_Click" />&nbsp&nbsp
                    <asp:Button ID="SaveCase" runat="server" Text="存檔" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" />&nbsp&nbsp
                    <asp:Button ID="GoHomePage" runat="server" Text="返回案件列表" Visible="false" OnClick="GoHomePage_Click" />
                </div>
            </div>
        </div>

<%--        <div class="footer-s">
            <div class="footer-s-green"></div>
                <div class="footer-b-brown">
                    <p> <span class="span1">臺北市政府工務局大地工程處</span>
                    <br/><span class="span2">110臺北市信義區松德路300號3樓 　(02)27593001   臺北市民當家熱線1999</span>
                    <br/><span class="span2">資料更新：2017.5.19　來訪人數：123456789 </span></p>
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
