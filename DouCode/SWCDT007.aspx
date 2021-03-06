﻿<!--
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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCDT007.aspx.cs" Inherits="SWCDOC_SWCDT007" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>施工監督檢查紀錄表</title>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge, chrome=1">
    <link rel="stylesheet" type="text/css" href="../css/reset.css" />
    <link rel="stylesheet" type="text/css" href="../css/allSwc.css" />
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
        function addtext(unitelement, peopleelement, listelement) {
            listelement.value = listelement.value + "\r\n" + unitelement.value + " " + peopleelement.value;
            return false;
        }
        function chkInput(jChkType) {
            jCHKValue01 = document.getElementById("TXTDTL002").value;

            jCHKValue08 = document.getElementById("TXTDTL008").value;
            jCHKValue12 = document.getElementById("TXTDTL012").value;
            jCHKValue14 = document.getElementById("TXTDTL014").value;
            jCHKValue16 = document.getElementById("TXTDTL016").value;
            jCHKValue18 = document.getElementById("TXTDTL018").value;
            jCHKValue20 = document.getElementById("TXTDTL020").value;
            jCHKValue22 = document.getElementById("TXTDTL022").value;
            jCHKValue23 = document.getElementById("TXTDTL023").value;
            jCHKValue24 = document.getElementById("TXTDTL024").value;

            if (jCHKValue01.trim() == '') {
                alert('請輸入檢查日期');
                document.getElementById("TXTDTL002").focus();
                return false;
            }
            if (jCHKValue08.trim() != '') {
                if (isNaN(jCHKValue08)) {
                    alert('基地面積(公頃) 請輸入正確數字');
                    document.getElementById("TXTDTL008").focus();
                    return false;
                }
            }
            if (jCHKValue12.trim() != '') {
                if (isNaN(jCHKValue12)) {
                    alert('基地現況 建物 請輸入正確數字');
                    document.getElementById("TXTDTL012").focus();
                    return false;
                }
            }
            if (jCHKValue14.trim() != '') {
                if (isNaN(jCHKValue14)) {
                    alert('基地現況 道路 請輸入正確數字');
                    document.getElementById("TXTDTL014").focus();
                    return false;
                }
            }
            if (jCHKValue18.trim() != '') {
                if (isNaN(jCHKValue18)) {
                    alert('水保設施概要 排水設施 請輸入正確數字');
                    document.getElementById("TXTDTL018").focus();
                    return false;
                }
            }
            if (jCHKValue20.trim() != '') {
                if (isNaN(jCHKValue20)) {
                    alert('水保設施概要 擋土設施 請輸入正確數字');
                    document.getElementById("TXTDTL020").focus();
                    return false;
                }
            }
            if (jCHKValue22.trim() != '') {
                if (isNaN(jCHKValue22)) {
                    alert('水保設施概要 滯洪沉砂設施 請輸入正確數字');
                    document.getElementById("TXTDTL022").focus();
                    return false;
                }
            }
            if (jCHKValue23.trim() != '') {
                if (isNaN(jCHKValue23)) {
                    alert('水保設施概要 滯洪量 請輸入正確數字');
                    document.getElementById("TXTDTL023").focus();
                    return false;
                }
            }
            if (jCHKValue24.trim() != '') {
                if (isNaN(jCHKValue24)) {
                    alert('水保設施概要 沉砂量 請輸入正確數字');
                    document.getElementById("TXTDTL024").focus();
                    return false;
                }
            }
            if (jChkType=='DataLock') {
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

        <div class="header">
            <a href="http://tcgeswc.taipei.gov.tw/tslmservice/" title="臺北市政府大地工程處坡地管理資料庫" class="header-link"><img src="../images/banner.jpg" alt=""></a>
            <div class="header-menu">
                <a href="SwcDtl03.aspx"><img src="../images/title/swcch_m01a.png" alt="施工監督檢查"/></a>
                <a href="SwcDtl07.aspx"><img src="../images/title/swcch_m02b.png" alt="設施維護檢查"/></a>
            </div>
            <a href="#" style="display: inline-block;"><img src="../images/title/title_swcch.png" alt=""></a>
            <span class="l-span" style="float: right;">
                <asp:Literal ID="TextUserName" runat="server">大雷包，您好</asp:Literal>&nbsp&nbsp&nbsp
                <asp:Button ID="BtnLogOut" runat="server" Text="登出" Height="22px" UseSubmitBehavior="False" OnClick="BtnLogOut_Click" Font-Names="標楷體" />
            </span>
            <%--<img src="../images/btn/icon_exportpdf.png" alt="輸出pdf" class="l-Block">--%>
        </div>
        <div class="content">
            <div class="facilityMaintain form">
                <h1>臺北市山坡地水土保持設施維護檢查及輔導紀錄表<br/><br/></h1>
                
                <table class="facilityMaintain-out">
                    <tr><td>設施維護表編號</td>
                        <td><asp:Label ID="LBDTL001" runat="server"/>
                            <asp:Label ID="LBSWC000" runat="server" Visible="false"/>
                        </td></tr>
                    <tr><td>檢查日期<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTDTL002" runat="server" width="120px"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTDTL002_CalendarExtender" runat="server" TargetControlID="TXTDTL002" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                </table>

                <br/><br/>

                <table class="facilityMaintain-base">
                <tr><td colspan="2" style="text-align: center;">計畫名稱</td>
                    <td colspan="4"><asp:Label ID="LBSWC005" runat="server"/></td></tr>
                <tr><td rowspan="3">水土保持義務人</td>
                    <td>姓名或名稱</td>
                    <td colspan="3"><asp:Label ID="LBSWC013" runat="server"/></td></tr>
                <tr><td>連絡地址</td>
                    <td colspan="3"><asp:Label ID="LBSWC014" runat="server"/></td></tr>
                <tr><td>連絡電話</td>
                    <td colspan="3"><asp:Label ID="LBSWC013TEL" runat="server"/></td></tr>
                <tr><td rowspan="3">現行義務人資料</td>
                    <td>姓名或名稱</td>
                    <td colspan="3">
                        <asp:TextBox ID="TXTDTL004" runat="server" Width="300" onkeyup="textcount(this,'TXTDTL004_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTDTL004_count" runat="server" Text="(0/100)" ForeColor="Red" /></td></tr>
                <tr><td>連絡地址</td>
                    <td colspan="3">
                        <asp:TextBox ID="TXTDTL005" runat="server" Width="500" onkeyup="textcount(this,'TXTDTL005_count','200');" MaxLength="200" />
                        <asp:Label ID="TXTDTL005_count" runat="server" Text="(0/200)" ForeColor="Red" /></td></tr>
                <tr><td>連絡電話</td>
                    <td colspan="3">
                        <asp:TextBox ID="TXTDTL006" runat="server" Width="300" onkeyup="textcount(this,'TXTDTL006_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTDTL006_count" runat="server" Text="(0/100)" ForeColor="Red" /></td></tr>
                <tr><td colspan="2">地點</td>
                    <td width="30%"><asp:TextBox ID="TXTDTL007" runat="server" MaxLength="200"/></td>
                    <td width="20%">基地面積(公頃)</td>
                    <td width="40%"><asp:TextBox ID="TXTDTL008" runat="server" MaxLength="10"/></td></tr>
                <tr><td colspan="2">圖說來源</td>
                    <td colspan="4">
                        <asp:DropDownList ID="DDLDTL009" runat="server" />　
                        其他：<asp:TextBox ID="TXTDTL010" runat="server" MaxLength="200"/></td></tr>
                <tr><td colspan="2">基地現況</td>
                    <td colspan="4">
                        <asp:CheckBox ID="CHKDTL011" runat="server" Text="建物" />
                        <asp:TextBox ID="TXTDTL012" runat="server" />戶<br/>
                        <asp:CheckBox ID="CHKDTL013" runat="server" Text="道路" />
                        <asp:TextBox ID="TXTDTL014" runat="server" />條<br/>
                        <asp:CheckBox ID="CHKDTL015" runat="server" Text="其他" />
                        <asp:TextBox ID="TXTDTL016" runat="server" MaxLength="200"/></td></tr>
                <tr><td colspan="2" rowspan="3">水保設施概要</td>
                    <td colspan="4">
                        <asp:CheckBox ID="CHKDTL017" runat="server" Text="排水設施" />
                        <asp:TextBox ID="TXTDTL018" runat="server" /> 條
                        <asp:CheckBox ID="CHKDTL019" runat="server" Text="擋土設施" />
                        <asp:TextBox ID="TXTDTL020" runat="server" /> 道
                        </td></tr>
                <tr>
                    <td colspan="4">
                        <asp:CheckBox ID="CHKDTL021" runat="server" Text="滯洪沉砂設施" />
                        <asp:TextBox ID="TXTDTL022" runat="server" /> 座
                        （滯洪量 <asp:TextBox ID="TXTDTL023" runat="server" /> m³ 
                        　沉砂量 <asp:TextBox ID="TXTDTL024" runat="server" /> m³）
                        </td></tr>
                <tr>
                    <td colspan="4">
                        <asp:CheckBox ID="CHKDTL025" runat="server" Text="其他" />
                        <asp:TextBox ID="TXTDTL026" runat="server" /> </td></tr>
                </table>

                <table class="facilityMaintain-check">
                <tr><td>一、檢查項目</td>
                    <td style="text-align: center;">執行情形</td></tr>
                <tr><td>（一）滯洪沉砂設施</td>
                    <td><asp:DropDownList ID="DDLDTL027" runat="server" /></td></tr>
                <tr><td class="td-padding">1.淤砂情形</td>
                    <td><asp:DropDownList ID="DDLDTL028" runat="server" /></td></tr>
                <tr><td class="td-padding">2.聯外排水情形</td>
                    <td><asp:DropDownList ID="DDLDTL030" runat="server" />
                        <asp:TextBox ID="TXTDTL031" runat="server" /></td></tr>
                <tr><td>（二）排水設施</td>
                    <td><asp:DropDownList ID="DDLDTL032" runat="server" /></td></tr>
                <tr><td class="td-padding">1.淤積或堵塞</td>
                    <td><asp:DropDownList ID="DDLDTL033" runat="server" /></td></tr>
                <tr><td class="td-padding">2.斷裂或基礎沉陷</td>
                    <td><asp:DropDownList ID="DDLDTL035" runat="server" /></td></tr>
                <tr><td>（三）邊坡保護設施</td>
                    <td><asp:DropDownList ID="DDLDTL037" runat="server" /></td></tr>
                <tr><td class="td-padding">1.擋土牆排水</td>
                    <td><asp:DropDownList ID="DDLDTL038" runat="server" /><br/>
                        <asp:TextBox ID="TXTDTL039" runat="server" placeholder="其他說明" style="width: 50%;" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">2.擋土牆外凸變形</td>
                    <td><asp:DropDownList ID="DDLDTL040" runat="server" /></td></tr>
                <tr><td class="td-padding">3.擋土牆龜裂</td>
                    <td><asp:DropDownList ID="DDLDTL042" runat="server" /></td></tr>
                <tr><td>二、諮詢及輔導</td>
                    <td style="text-align: center;">執行情形</td> </tr>
                <tr><td>（一）義務人諮詢內容</td>
                    <td colspan="3">
                        <asp:DropDownList ID="DDLDTL044" runat="server" />
                        <br/>
                        <asp:TextBox ID="TXTDTL045" runat="server" TextMode="MultiLine" Height="100" onkeyup="textcount(this,'TXTDTL045_count','500');" />
                        <asp:Label ID="TXTDTL045_count" runat="server" Text="(0/500)" ForeColor="Red" /></td></tr>
                <tr><td rowspan="3">（二）輔導內容</td>
                    <td colspan="3">
                        <asp:CheckBox ID="CHKDTL046" runat="server" Text="摺頁發放及解說：" />
                        <asp:TextBox ID="TXTDTL047" runat="server" Width="50px" Visible="false" />
                        <asp:FileUpload ID="TXTDTL047_fileupload" runat="server" />
                        <asp:Button ID="TXTDTL047_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL047_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL047_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請確認真的要刪除照片!!!')" OnClick="TXTDTL047_fileuploaddel_Click" />
                        <br/>
                        <span style="color:red;">檔案大小請小於 5Mb，請上傳 jpg, png 格式圖檔</spa><br/>
                        <asp:HyperLink ID="Link047" runat="server" Target="_blank" Visible="false" /></td></tr>
                <tr><td colspan="3">
                        <asp:CheckBox ID="CHKDTL048" runat="server" Text="諮詢事項回復：" />
                        <br/>
                        <asp:TextBox ID="TXTDTL049" runat="server" TextMode="MultiLine" Height="100" onkeyup="textcount(this,'TXTDTL049_count','300');" />
                        <asp:Label ID="TXTDTL049_count" runat="server" Text="(0/300)" ForeColor="Red" /></td></tr>
                <tr><td colspan="3">
                        <asp:CheckBox ID="CHKDTL050" runat="server" Text="其他輔導內容：" />
                        <br/>
                        <asp:TextBox ID="TXTDTL051" runat="server" TextMode="MultiLine" Height="100" onkeyup="textcount(this,'TXTDTL051_count','300');" />
                        <asp:Label ID="TXTDTL051_count" runat="server" Text="(0/300)" ForeColor="Red" /></td></tr>
                <tr><td>三、檢查結果</td>
                    <td colspan="3">
                        <asp:CheckBox ID="CHKDTL052" runat="server" Text="水保設施維護均正常" />，無應改正事項
                        <br/>
                        <asp:CheckBox ID="CHKDTL053" runat="server" Text="須辦理複檢" />，義務人同意於
                        <asp:TextBox ID="TXTDTL054" runat="server" width="120px"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTDTL054_CalendarExtender" runat="server" TargetControlID="TXTDTL054" Format="yyyy-MM-dd"></asp:CalendarExtender>
                        日前完成改善
                        <br/>
                        <asp:CheckBox ID="CHKDTL055" runat="server" Text="有變更使用" />，影響安全情事
                        <asp:TextBox ID="TXTDTL056" runat="server" width="200px"/>
                        <br/>
                        <asp:CheckBox ID="CHKDTL057" runat="server" Text="前次檢查之改正事項辦理情形" />
                        <asp:DropDownList ID="DDLDTL058" runat="server" /></td></tr>
                </table>

                <table class="facilityMaintain-imgUpload">
                <tr><td>四、相關單位人員簽名<br/>
                        檢查單位：<asp:DropDownList ID="DropDownList_01" runat="server" />
                        相關人員：<asp:TextBox ID="TextBox_01" runat="server" width="185px"/>
                        <asp:Button ID="BTN_01" runat="server" Text="加入" OnClientClick="addtext(DropDownList_01,TextBox_01,TXTDTL059);" />
                        <br/>
                        <asp:TextBox ID="TXTDTL059" runat="server" TextMode="MultiLine" style="height:250px;width:100%;" /></td>
                    <td><asp:Image ID="TXTDTL060_img" runat="server" CssClass="imgUpload-l90" Visible="false" /><br/>
                        <asp:HyperLink ID="HyperLink060" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink>
                        <span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:TextBox ID="TXTDTL060" runat="server" Width="70px" Visible="false" />
                        <asp:FileUpload ID="TXTDTL060_fileupload" runat="server" />
                        <asp:Button ID="TXTDTL060_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL060_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL060_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請確認真的要刪除照片!!!')" OnClick="TXTDTL060_fileuploaddel_Click" /></td></tr>
                </table>

                <table class="facilityMaintain-fileUpload">
                <tr><td>檢查項目名稱</td>
                    <td><asp:DropDownList ID="DDLDTL061" runat="server" /></td>
                    <td>檢查項目名稱</td>
                    <td><asp:DropDownList ID="DDLDTL064" runat="server" /></td></tr>
                <tr><td>照片1 說明</td>
                    <td><asp:TextBox ID="TXTDTL063" runat="server"  />
                        <asp:DropDownList ID="DDLDTL063" runat="server" Visible="false" /></td>
                        <%--上面項目有勾選的進入下拉選單<br/>新建空白--%>
                    <td>照片2 說明</td>
                    <td><asp:TextBox ID="TXTDTL066" runat="server" />
                        <asp:DropDownList ID="DDLDTL066" runat="server" Visible="false" /></td></tr>
                <tr><td colspan="2">
                        <div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL062" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL062_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL062_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL062_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL062_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請確認真的要刪除照片!!!')" OnClick="TXTDTL062_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL062_img" runat="server" CssClass="imgUpload" /></td>
                    <td colspan="2">
                        <div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL065" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL065_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL065_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL065_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL065_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請確認真的要刪除照片!!!')" OnClick="TXTDTL065_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL065_img" runat="server" CssClass="imgUpload" /></td></tr>
                <tr><td>檢查項目名稱</td>
                    <td><asp:DropDownList ID="DDLDTL067" runat="server" /></td>
                    <td>檢查項目名稱</td>
                    <td><asp:DropDownList ID="DDLDTL070" runat="server" /></td></tr>
                <tr><td>照片3 說明</td>
                    <td><asp:TextBox ID="TXTDTL069" runat="server"  />
                        <asp:DropDownList ID="DDLDTL069" runat="server" Visible="false" /></td>
                        <%--上面項目有勾選的進入下拉選單<br/>新建空白--%>
                    <td>照片4 說明</td>
                    <td><asp:TextBox ID="TXTDTL072" runat="server"  />
                        <asp:DropDownList ID="DDLDTL072" runat="server" Visible="false" /></td></tr>
                <tr><td colspan="2">
                        <div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL068" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL068_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL068_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL068_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL068_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請確認真的要刪除照片!!!')" OnClick="TXTDTL068_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 5Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL068_img" runat="server" CssClass="imgUpload" /></td>
                    <td colspan="2">
                        <div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL071" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL071_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL071_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL071_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL071_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請確認真的要刪除照片!!!')" OnClick="TXTDTL071_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 5Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL071_img" runat="server" CssClass="imgUpload" /></td></tr>
                <tr><td>檢查項目名稱</td>
                    <td><asp:DropDownList ID="DDLDTL073" runat="server" /></td>
                    <td>檢查項目名稱</td>
                    <td><asp:DropDownList ID="DDLDTL076" runat="server" /></td></tr>
                <tr><td>照片5 說明</td>
                    <td><asp:TextBox ID="TXTDTL075" runat="server" />
                        <asp:DropDownList ID="DDLDTL075" runat="server" Visible="false" /></td>
                        <%--上面項目有勾選的進入下拉選單<br/>新建空白--%>
                    <td>照片6 說明</td>
                    <td><asp:TextBox ID="TXTDTL078" runat="server"  />
                        <asp:DropDownList ID="DDLDTL078" runat="server" Visible="false" /></td></tr>
                <tr><td colspan="2">
                        <div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL074" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL074_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL074_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL074_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL074_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請確認真的要刪除照片!!!')" OnClick="TXTDTL074_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 5Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL074_img" runat="server" CssClass="imgUpload" /></td>
                    <td colspan="2">
                        <div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL077" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL077_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL077_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL077_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL077_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請確認真的要刪除照片!!!')" OnClick="TXTDTL077_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 5Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL077_img" runat="server" CssClass="imgUpload" /></td></tr>
                <tr><td colspan="4">其他附件</td></tr>
                <tr><td colspan="4">其他附件檔案上傳：
                        <asp:TextBox ID="TXTDTL079" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTDTL079_fileupload" runat="server" />
                        <asp:Button ID="TXTDTL079_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL079_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL079_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請確認真的要刪除檔案!!!')" OnClick="TXTDTL079_fileuploaddel_Click" />
                        <br/><br/>
                        <span style="color:red;">檔案大小請小於 5Mb，請上傳 jpg, png, doc, pdf, dwg, dxf 格式</span>
                        <br/><br/>
                        <asp:HyperLink ID="Link079" runat="server" Text ="其他附件檔案下載" Target="_blank" Visible="false" /></td></tr>
                <tr><td colspan="4">圖說</td></tr>
                <tr><td colspan="4">圖說檔案上傳：
                        <asp:TextBox ID="TXTDTL080" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTDTL080_fileupload" runat="server" />
                        <asp:Button ID="TXTDTL080_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL080_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL080_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請確認真的要刪除檔案!!!')" OnClick="TXTDTL080_fileuploaddel_Click" />
                        <br/><br/>
                        <span style="color:red;">檔案大小請小於 5Mb，請上傳 jpg, png, doc, pdf, dwg, dxf 格式</span>
                        <br/><br/>
                        <asp:HyperLink ID="Link080" runat="server" Text ="圖說檔案下載" Target="_blank" Visible="false" /></td></tr>
                <tr><td colspan="4">地形圖</td></tr>
                <tr><td colspan="4">地形圖檔案上傳：
                        <asp:TextBox ID="TXTDTL081" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTDTL081_fileupload" runat="server" />
                        <asp:Button ID="TXTDTL081_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL081_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL081_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請確認真的要刪除檔案!!!')" OnClick="TXTDTL081_fileuploaddel_Click" />
                        <br/><br/>
                        <span style="color:red;">檔案大小請小於 5Mb，請上傳 jpg, png, doc, pdf, dwg, dxf 格式</span>
                        <br/><br/>
                        <asp:HyperLink ID="Link081" runat="server" Text ="地形圖檔案下載" Target="_blank" Visible="false" /></td></tr>
                </table>

                <div class="form-btn">
                    <asp:Button ID="DataLock" runat="server" Text="確認送出" OnClientClick="return chkInput('DataLock');" OnClick="DataLock_Click" />&nbsp&nbsp
                    <asp:Button ID="SaveCase" runat="server" Text="暫時儲存" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" />&nbsp&nbsp
                    <asp:Button ID="GoHomePage" runat="server" Text="返回案件列表" OnClick="GoHomePage_Click" />
                </div>
            </div>
        </div>

        <asp:Literal ID="error_msg" runat="server"></asp:Literal>

        <script src="../js/jquery-3.1.1.min.js"></script>
        <script src="../js/inner.js"></script>

    </div>
    </form>
</body>
</html>
