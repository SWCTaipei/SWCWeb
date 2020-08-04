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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCDT005.aspx.cs" Inherits="SWCDOC_SWCDT005" %>
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
    <meta name="viewport" content="width=device-width">
    <link rel="stylesheet" type="text/css" href="../css/reset.css" />
    <link rel="stylesheet" type="text/css" href="../css/all.css" />
    
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
        function chknumber(objElement) {
            var chrstring = objElement.value;
            var chr = chrstring.substring(chrstring.length - 1, chrstring.length);
            if ((chr != '.') && (chr != '0') && (chr != '1') && (chr != '2') && (chr != '3') && (chr != '4') && (chr != '5') && (chr != '6') && (chr != '7') && (chr != '8') && (chr != '9')) {
                objElement.value = objElement.value.substring(0, chrstring.length - 1);
            }
        }
        function chkInput(jChkType) {
            jCHKValue01 = document.getElementById("TXTDTL002").value;
            jCHKValue02 = document.getElementById("TXTDTL003").value;
            jCHKValue03 = document.getElementById("TXTDTL004").value;
            //jCHKValue04 = document.getElementById("TXTSWC015").value;
            //jCHKValue05 = document.getElementById("TXTSWC016").value;

            if (jCHKValue01.trim() == '') {
                alert('請輸入上傳日期');
                document.getElementById("TXTDTL002").focus();
                return false;
            } else
            {
                if (!dateValidationCheck(jCHKValue01)) {
                    document.getElementById("TXTDTL002").focus();
                    return false;
                }
            }
            if (jCHKValue02.trim() == '') {
                alert('請輸入工程進度');
                document.getElementById("TXTDTL003").focus();
                return false;
            }
            if (jCHKValue03.trim() == '') {
                alert('請輸入監造結果');
                document.getElementById("TXTDTL004").focus();
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
            <div class="header header-s clearfix"><a href="SWC000.aspx" class="logo-s"></a>
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
            <div class="supervision form">
                <h1>臺北市水土保持計畫監造紀錄<br/><br/></h1>

                <table class="checklist-out">
                <tr><td>監造紀錄表編號</td>
                    <td><asp:Label ID="LBDTL001" runat="server"/>
                        <asp:Label ID="LBSWC000" runat="server" Visible="false"/></td></tr>
                <tr><td>案件名稱</td>
                    <td><asp:Label ID="LBSWC005" runat="server"/></td></tr>
                <tr><td>檢查日期<span style="color: red;font-family:cursive;">＊</span></td>
                    <td><asp:TextBox ID="TXTDTL002" runat="server" width="150px" autocomplete="off"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTDTL002_CalendarExtender" runat="server" TargetControlID="TXTDTL002" Format="yyyy-MM-dd"></asp:CalendarExtender>
                        <asp:TextBox ID="TXTDTL005" runat="server" width="150px" Visible="false"></asp:TextBox></td></tr>
                <tr><td>預定工程進度<span style="color: red;font-family:cursive;">＊</span></td>
                    <td><asp:TextBox ID="TXTDTL003" runat="server" width="100%" onkeyup="textcount(this,'TXTDTL003_count','200');" MaxLength="200" /><br/>
                        <asp:Label ID="TXTDTL003_count" runat="server" Text="(0/200)" ForeColor="Red" /></td></tr>
                <tr><td>監造結果<span style="color: red;font-family:cursive;">＊</span></td>
                    <td><asp:TextBox ID="TXTDTL004" runat="server" width="100%" onkeyup="textcount(this,'TXTDTL004_count','200');" MaxLength="200" /><br/>
                        <asp:Label ID="TXTDTL004_count" runat="server" Text="(0/200)" ForeColor="Red" /></td></tr>
                </table>

                <br/><br/>

                <table class="supervision-one">
                <tr><td class="supervision-td1">日期</td>
                    <td colspan="3">自
                        <asp:TextBox ID="TXTDTL006" runat="server" width="120px" autocomplete="off"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTDTL006_CalendarExtender" runat="server" TargetControlID="TXTDTL006" Format="yyyy-MM-dd"></asp:CalendarExtender> 至
                        <asp:TextBox ID="TXTDTL007" runat="server" width="120px" autocomplete="off"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTDTL007_CalendarExtender" runat="server" TargetControlID="TXTDTL007" Format="yyyy-MM-dd"></asp:CalendarExtender> (以週計)</td></tr>
                <tr><td class="supervision-td1">水土保持計畫名稱</td>
                    <td colspan="3"><asp:Label ID="LBSWC005_01" runat="server"/></td></tr>
                <tr><td class="supervision-td1">水土保持義務人</td>
                    <td colspan="3"><asp:Label ID="LBSWC013" runat="server"/></td></tr>
                <tr><td class="supervision-td1">主管機關</td>
                    <td colspan="3"><asp:Label ID="LBSWC017" runat="server"/></td></tr>
                <tr><td class="supervision-td1">水土保持施工許可證日期文號</td>
                    <td colspan="3">臺北市政府
                        <asp:Label ID="LBSWC043" runat="server"/>
                        <asp:Label ID="LBSWC044" runat="server"/>
                        號函</td></tr>
                <tr><td class="supervision-td1">承辦監造技師</td>
                    <td colspan="3"><asp:Label ID="LBSWC045" runat="server"/></td></tr>
                <tr><td class="supervision-td1">營造單位</td>
                    <td colspan="3"><asp:Label ID="LBSWC048" runat="server"/></td></tr>
                <tr><td class="supervision-td1">監造項目</td>
                    <td colspan="2" class="supervision-td1">是否與計畫或規定相符</td>
                    <td class="supervision-td1">備註</td></tr>
                <tr><td>（一）水土保持施工告示牌</td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLDTL016" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL017" runat="server" MaxLength="50" /></td>
                    <td><asp:TextBox ID="TXTDTL018" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL018_count','50');" />
                        <br/><asp:Label ID="TXTDTL018_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </td></tr>
                <tr><td>（二）開發範圍界樁</td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLDTL019" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL020" runat="server" MaxLength="50" /></td>
                    <td><asp:TextBox ID="TXTDTL021" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL021_count','50');" />
                        <br/><asp:Label ID="TXTDTL021_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </td></tr>
                <tr><td>（三）開挖整地範圍界樁</td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLDTL022" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL023" runat="server" MaxLength="50" /></td>
                    <td><asp:TextBox ID="TXTDTL024" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL024_count','50');" />
                        <br/><asp:Label ID="TXTDTL024_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </td></tr>
                <tr><td>（四）災害搶救小組是否成立</td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLDTL025" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL026" runat="server" MaxLength="50" /></td>
                    <td><asp:TextBox ID="TXTDTL027" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL027_count','50');" />
                        <br/><asp:Label ID="TXTDTL027_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </td></tr>
                <tr><td>（五）臨時性防災措施</td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLDTL028" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL029" runat="server" MaxLength="50" /></td>
                    <td><asp:TextBox ID="TXTDTL030" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL030_count','50');" />
                        <br/><asp:Label ID="TXTDTL030_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">1.排水設施</td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLDTL031" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL032" runat="server" MaxLength="50" /></td>
                    <td><asp:TextBox ID="TXTDTL033" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL033_count','50');" />
                        <br/><asp:Label ID="TXTDTL033_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">2.沉砂設施</td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLDTL034" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL035" runat="server" MaxLength="50" /></td>
                    <td><asp:TextBox ID="TXTDTL036" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL036_count','50');" />
                        <br/><asp:Label ID="TXTDTL036_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">3.滯洪設施</td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLDTL037" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL038" runat="server" MaxLength="50" /></td>
                    <td><asp:TextBox ID="TXTDTL039" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL039_count','50');" />
                        <br/><asp:Label ID="TXTDTL039_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">4.土方暫置</td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLDTL040" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL041" runat="server" MaxLength="50" /></td>
                    <td><asp:TextBox ID="TXTDTL042" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL042_count','50');" />
                        <br/><asp:Label ID="TXTDTL042_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">5.邊坡保護措施</td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLDTL043" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL044" runat="server" MaxLength="50" /></td>
                    <td><asp:TextBox ID="TXTDTL045" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL045_count','50');" />
                        <br/><asp:Label ID="TXTDTL045_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">6.施工便道</td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLDTL046" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL047" runat="server" MaxLength="50" /></td>
                    <td><asp:TextBox ID="TXTDTL048" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL048_count','50');" />
                        <br/><asp:Label ID="TXTDTL048_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">7.其他</td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLDTL049" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL050" runat="server" MaxLength="50" /></td>
                    <td><asp:TextBox ID="TXTDTL051" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL051_count','50');" />
                        <br/><asp:Label ID="TXTDTL051_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </td></tr>
                <tr><td>（六）永久性防災措施</td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLDTL052" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL053" runat="server" MaxLength="50" /></td>
                    <td><asp:TextBox ID="TXTDTL054" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL054_count','50');" />
                        <br/><asp:Label ID="TXTDTL054_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">1.排水設施</td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLDTL055" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL056" runat="server" MaxLength="50" /></td>
                    <td><asp:TextBox ID="TXTDTL057" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL057_count','50');" />
                        <br/><asp:Label ID="TXTDTL057_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">2.沉砂設施</td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLDTL058" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL059" runat="server" MaxLength="50" /></td>
                    <td><asp:TextBox ID="TXTDTL060" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL060_count','50');" />
                        <br/><asp:Label ID="TXTDTL060_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">3.滯洪設施</td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLDTL061" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL062" runat="server" MaxLength="50" /></td>
                    <td><asp:TextBox ID="TXTDTL063" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL063_count','50');" />
                        <br/><asp:Label ID="TXTDTL063_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">4.聯外排水</td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLDTL064" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL065" runat="server" MaxLength="50" /></td>
                    <td><asp:TextBox ID="TXTDTL066" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL066_count','50');" />
                        <br/><asp:Label ID="TXTDTL066_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">5.擋土設施</td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLDTL067" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL068" runat="server" MaxLength="50" /></td>
                    <td><asp:TextBox ID="TXTDTL069" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL069_count','50');" />
                        <br/><asp:Label ID="TXTDTL069_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">6.植生工程</td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLDTL070" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL071" runat="server" MaxLength="50" /></td>
                    <td><asp:TextBox ID="TXTDTL072" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL072_count','50');" />
                        <br/><asp:Label ID="TXTDTL072_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">7.邊坡穩定措施</td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLDTL073" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL074" runat="server" MaxLength="50" /></td>
                    <td><asp:TextBox ID="TXTDTL075" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL075_count','50');" />
                        <br/><asp:Label ID="TXTDTL075_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">8.其他</td>
                    <td colspan="2">
                        <asp:DropDownList ID="DDLDTL076" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL077" runat="server" MaxLength="50" /></td>
                    <td><asp:TextBox ID="TXTDTL078" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL078_count','50');" />
                        <br/><asp:Label ID="TXTDTL078_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </td></tr>
                <tr><td>（七）當週重大事件</td>
                    <td colspan="3">
                        <asp:TextBox ID="TXTDTL079" runat="server" width="300" onkeyup="textcount(this,'TXTDTL079_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTDTL079_count" runat="server" Text="(0/100)" ForeColor="Red" /></td></tr>
                </table>

                <table class="supervision-borderTN">
                <tr><td>（八）通知水土保持義務人改正事項</td>
                    <td  colspan="3">
                        <asp:TextBox ID="TXTDTL080" runat="server" width="300" onkeyup="textcount(this,'TXTDTL080_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTDTL080_count" runat="server" Text="(0/100)" ForeColor="Red" /></td></tr>
                <tr><td>（九）通知改正期限</td>
                    <td colspan="3">
                        <asp:TextBox ID="TXTDTL081" runat="server" width="120px"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTDTL081_CalendarExtender" runat="server" TargetControlID="TXTDTL081" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                <tr><td>（十）累計進度百分比</td>
                    <td colspan="3"><asp:TextBox ID="TXTDTL082" Width="50" runat="server" onkeyup="chknumber(this);"/> %</td></tr>
                </table>
        
                <table class="supervision-innerTable">
                <tr><td colspan="12" style="text-align: left;">
                    （十一）臨時水保設施完成項目（如尚未施作完成則免）
                    <asp:HyperLink ID="DLExcel01" runat="server" Text="excel範本下載" NavigateUrl="../sysFile/臨時水保設施完成項目.xlsx" /></td></tr>
                <tr><td colspan="12">
                        <asp:FileUpload ID="TXTDTL083_FileUpload" runat="server" />
                        <asp:Button ID="TXTDTL083_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL083_fileuploadok_Click"/>
                        <asp:TextBox ID="TXTDTL083" runat="server" Width="70px" Visible="false"/>
                        <asp:Button ID="TXTDTL083_fileclean" runat="server" CssClass="wordttb" Text="X" OnClientClick="return confirm('檔案刪除後無法復原，請確認真的要刪除檔案!!!')" OnClick="TXTDTL083_fileclean_Click" /><br />
                        <asp:HyperLink ID="Link083" runat="server" CssClass="word" Target="_blank"></asp:HyperLink><br />
                        <span>※ 上傳格式限定為excel，檔案大小請於50mb以內</span></td></tr>
                <tr><td colspan="12">
                    （十二）永久水保設施完成項目（如尚未施作完成則免）
                    <asp:HyperLink ID="DLExcel02" runat="server" Text="excel範本下載" NavigateUrl="../sysFile/永久水保設施完成項目.xlsx" /></td></tr>
                <tr><td colspan="12">
                        <asp:FileUpload ID="TXTDTL084_FileUpload" runat="server" />
                        <asp:Button ID="TXTDTL084_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL084_fileuploadok_Click"/>
                        <asp:TextBox ID="TXTDTL084" runat="server" Width="70px" Visible="false"/>
                        <asp:Button ID="TXTDTL084_fileclean" runat="server" CssClass="wordttb" Text="X" OnClientClick="return confirm('檔案刪除後無法復原，請確認真的要刪除檔案!!!')" /><br/>
                        <asp:HyperLink ID="Link084" runat="server" CssClass="word" Target="_blank"></asp:HyperLink><br/>
                        <span>※ 上傳格式限定為excel，檔案大小請於50mb以內</span></td></tr>

                <tr><td colspan="2">承辦監造技師：
                        <asp:HyperLink ID="HyperLink085" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink>
                        <asp:Image ID="TXTDTL085_img" runat="server" Visible="False" /></td>
                    <td colspan="10">(簽章)<asp:TextBox ID="TXTDTL085" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTDTL085_FileUpload" runat="server"  />
                        <asp:Button ID="TXTDTL085_fileuploadok" runat="server" Text="上傳簽章" OnClick="TXTDTL085_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL085_fileuploaddel" runat="server" Text="x" ToolTip="刪除簽章" OnClientClick="return confirm('簽章後無法復原，請確認真的要簽章!!!')" OnClick="TXTDTL085_fileuploaddel_Click" />
                        <span>檔案大小請小於 10Mb，請上傳 jpg, png 格式圖檔</span></td></tr>
                <tr><td colspan="2">營造單位：
                        <asp:HyperLink ID="HyperLink086" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink>
                        <asp:Image ID="TXTDTL086_img" runat="server" Visible="False" /></td>
                    <td colspan="10">(簽章)<asp:TextBox ID="TXTDTL086" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTDTL086_FileUpload" runat="server"  />
                        <asp:Button ID="TXTDTL086_fileuploadok" runat="server" Text="上傳簽章" OnClick="TXTDTL086_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL086_fileuploaddel" runat="server" Text="x" ToolTip="刪除簽章" OnClientClick="return confirm('簽章後無法復原，請確認真的要簽章!!!')" OnClick="TXTDTL086_fileuploaddel_Click" />
                        <span>檔案大小請小於 10Mb，請上傳 jpg, png 格式圖檔</span></td></tr>
                <tr><td colspan="2">水土保持義務人：
                        <asp:HyperLink ID="HyperLink087" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink>
                        <asp:Image ID="TXTDTL087_img" runat="server" Visible="False" /></td>
                    <td colspan="10">(簽章)<asp:TextBox ID="TXTDTL087" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTDTL087_FileUpload" runat="server"  />
                        <asp:Button ID="TXTDTL087_fileuploadok" runat="server" Text="上傳簽章" OnClick="TXTDTL087_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL087_fileuploaddel" runat="server" Text="x" ToolTip="刪除簽章" OnClientClick="return confirm('簽章後無法復原，請確認真的要簽章!!!')" OnClick="TXTDTL087_fileuploaddel_Click" />
                        <span>檔案大小請小於 10Mb，請上傳 jpg, png 格式圖檔</span></td></tr>

                </table>

                <div class="form-btn">
                    <asp:Button ID="DataLock" runat="server" Text="確認送出" OnClientClick="return chkInput('DataLock');" OnClick="DataLock_Click" />&nbsp&nbsp
                    <asp:Button ID="SaveCase" runat="server" Text="暫時儲存" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" />&nbsp&nbsp
                    <asp:Button ID="GoHomePage" runat="server" Text="返回編輯案件" OnClick="GoHomePage_Click" />
                </div>
            </div>
        </div>
        <%--<div class="footer-s">
            <div class="footer-s-green"></div>
            <div class="footer-b-brown">
                <p> <span class="span1">臺北市政府工務局大地工程處</span>
                    <br><span class="span2">110臺北市信義區松德路300號3樓 　(02)27593001   臺北市民當家熱線1999</span>
                    <br><span class="span2">資料更新：2017.5.19　來訪人數：123456789 </span></p>
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
        <script src="../js/BaseNorl.js"></script>
    </div>
    </form>
</body>
</html>
