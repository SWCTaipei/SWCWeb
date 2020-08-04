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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCDT005v.aspx.cs" Inherits="SWCDOC_SWCDT005" %>
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
                alert('請輸入檢查日期');
                document.getElementById("TXTDTL002").focus();
                return false;
            }
            if (jCHKValue02.trim() == '') {
                alert('請輸入防災標的');
                document.getElementById("TXTDTL003").focus();
                return false;
            }
            if (jCHKValue03.trim() == '') {
                alert('請輸入自主檢查結果');
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
                <h1>臺北市水土保持計畫監造紀錄<br><br></h1>
                
                <table class="checklist-out">
                <tr><td>監造紀錄表編號</td>
                    <td><asp:Label ID="LBDTL001" runat="server"/>
                        <asp:Label ID="LBSWC000" runat="server" Visible="false"/></td></tr>
                <tr><td>案件名稱</td>
                    <td><asp:Label ID="LBSWC005" runat="server"/></td></tr>
                <tr><td>檢查日期<span style="color: red;font-family:cursive;">＊</span></td>
                    <td><asp:Label ID="TXTDTL002" runat="server"></asp:Label>
                        <asp:Label ID="TXTDTL005" runat="server" Visible="false"></asp:Label></td></tr>
                <tr><td>預定工程進度<span style="color: red;font-family:cursive;">＊</span></td>
                    <td><asp:Label ID="TXTDTL003" runat="server" /></td></tr>
                <tr><td>監造結果<span style="color: red;font-family:cursive;">＊</span></td>
                    <td><asp:Label ID="TXTDTL004" runat="server" /></td></tr>
                </table>

                <br/><br/>

                <table class="supervision-one">
                <tr><td class="supervision-td1">日期</td>
                    <td colspan="3">自
                        <asp:Label ID="TXTDTL006" runat="server" width="120px"></asp:Label> 至
                        <asp:Label ID="TXTDTL007" runat="server" width="120px"></asp:Label> (以週計)</td></tr>
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
                        <asp:Label ID="DDLDTL016" runat="server" />
                        <asp:Label ID="TXTDTL017" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL018" runat="server" /></td></tr>
                <tr><td>（二）開發範圍界樁</td>
                    <td colspan="2">
                        <asp:Label ID="DDLDTL019" runat="server" />
                        <asp:Label ID="TXTDTL020" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL021" runat="server" /></td></tr>
                <tr><td>（三）開挖整地範圍界樁</td>
                    <td colspan="2">
                        <asp:Label ID="DDLDTL022" runat="server" />
                        <asp:Label ID="TXTDTL023" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL024" runat="server" /></td></tr>
                <tr><td>（四）災害搶救小組是否成立</td>
                    <td colspan="2">
                        <asp:Label ID="DDLDTL025" runat="server" />
                        <asp:Label ID="TXTDTL026" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL027" runat="server" /></td></tr>
                <tr><td>（五）臨時性防災措施</td>
                    <td colspan="2">
                        <asp:Label ID="DDLDTL028" runat="server" />
                        <asp:Label ID="TXTDTL029" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL030" runat="server" /></td></tr>
                <tr><td class="td-padding">1.排水設施</td>
                    <td colspan="2">
                        <asp:Label ID="DDLDTL031" runat="server" />
                        <asp:Label ID="TXTDTL032" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL033" runat="server" /></td></tr>
                <tr><td class="td-padding">2.沉砂設施</td>
                    <td colspan="2">
                        <asp:Label ID="DDLDTL034" runat="server" />
                        <asp:Label ID="TXTDTL035" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL036" runat="server" /></td></tr>
                <tr><td class="td-padding">3.滯洪設施</td>
                    <td colspan="2">
                        <asp:Label ID="DDLDTL037" runat="server" />
                        <asp:Label ID="TXTDTL038" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL039" runat="server" /></td></tr>
                <tr><td class="td-padding">4.土方暫置</td>
                    <td colspan="2">
                        <asp:Label ID="DDLDTL040" runat="server" />
                        <asp:Label ID="TXTDTL041" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL042" runat="server" /></td></tr>
                <tr><td class="td-padding">5.邊坡保護措施</td>
                    <td colspan="2">
                        <asp:Label ID="DDLDTL043" runat="server" />
                        <asp:Label ID="TXTDTL044" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL045" runat="server" /></td></tr>
                <tr><td class="td-padding">6.施工便道</td>
                    <td colspan="2">
                        <asp:Label ID="DDLDTL046" runat="server" />
                        <asp:Label ID="TXTDTL047" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL048" runat="server" /></td></tr>
                <tr><td class="td-padding">7.其他</td>
                    <td colspan="2">
                        <asp:Label ID="DDLDTL049" runat="server" />
                        <asp:Label ID="TXTDTL050" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL051" runat="server" /></td></tr>
                <tr><td>（六）永久性防災措施</td>
                    <td colspan="2">
                        <asp:Label ID="DDLDTL052" runat="server" />
                        <asp:Label ID="TXTDTL053" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL054" runat="server" /></td></tr>
                <tr><td class="td-padding">1.排水設施</td>
                    <td colspan="2">
                        <asp:Label ID="DDLDTL055" runat="server" />
                        <asp:Label ID="TXTDTL056" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL057" runat="server" /></td></tr>
                <tr><td class="td-padding">2.沉砂設施</td>
                    <td colspan="2">
                        <asp:Label ID="DDLDTL058" runat="server" />
                        <asp:Label ID="TXTDTL059" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL060" runat="server" /></td></tr>
                <tr><td class="td-padding">3.滯洪設施</td>
                    <td colspan="2">
                        <asp:Label ID="DDLDTL061" runat="server" />
                        <asp:Label ID="TXTDTL062" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL063" runat="server" /></td></tr>
                <tr><td class="td-padding">4.聯外排水</td>
                    <td colspan="2">
                        <asp:Label ID="DDLDTL064" runat="server" />
                        <asp:Label ID="TXTDTL065" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL066" runat="server" /></td></tr>
                <tr><td class="td-padding">5.擋土設施</td>
                    <td colspan="2">
                        <asp:Label ID="DDLDTL067" runat="server" />
                        <asp:Label ID="TXTDTL068" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL069" runat="server" /></td></tr>
                <tr><td class="td-padding">6.植生工程</td>
                    <td colspan="2">
                        <asp:Label ID="DDLDTL070" runat="server" />
                        <asp:Label ID="TXTDTL071" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL072" runat="server" /></td></tr>
                <tr><td class="td-padding">7.邊坡穩定措施</td>
                    <td colspan="2">
                        <asp:Label ID="DDLDTL073" runat="server" />
                        <asp:Label ID="TXTDTL074" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL075" runat="server" /></td></tr>
                <tr><td class="td-padding">8.其他</td>
                    <td colspan="2">
                        <asp:Label ID="DDLDTL076" runat="server" />
                        <asp:Label ID="TXTDTL077" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL078" runat="server" /></td></tr>
                <tr><td>（七）當週重大事件</td>
                    <td colspan="3">
                        <asp:Label ID="TXTDTL079" runat="server" /></td></tr>
                </table>

                <table class="supervision-borderTN">
                <tr><td>（八）通知水土保持義務人改正事項</td>
                    <td  colspan="3">
                        <asp:Label ID="TXTDTL080" runat="server" /></td></tr>
                <tr><td>（九）通知改正期限</td>
                    <td colspan="3">
                        <asp:Label ID="TXTDTL081" runat="server" width="120px"></asp:Label></td></tr>
                <tr><td>（十）累計進度百分比</td>
                    <td colspan="3"><asp:Label ID="TXTDTL082" Width="50" runat="server" onkeyup="chknumber(this);"/> %</td></tr>
                </table>
        
                <table class="supervision-innerTable">
                <tr><td colspan="12" style="text-align: left;">
                    （十一）臨時水保設施完成項目（如尚未施作完成則免）
                    <asp:HyperLink ID="DLExcel01" runat="server" Text="excel範本下載" NavigateUrl="../sysFile/臨時水保設施完成項目.xlsx" Visible="false" /></td></tr>
                <tr><td colspan="12">
                        <asp:TextBox ID="TXTDTL083" runat="server" Width="70px" Visible="false"/>
                        <asp:HyperLink ID="Link083" runat="server" CssClass="word" Target="_blank"></asp:HyperLink></td></tr>
                <tr><td colspan="12">
                    （十二）永久水保設施完成項目（如尚未施作完成則免）
                    <asp:HyperLink ID="DLExcel02" runat="server" Text="excel範本下載" NavigateUrl="../sysFile/永久水保設施完成項目.xlsx" Visible="false" /></td></tr>
                <tr><td colspan="12">
                        <asp:TextBox ID="TXTDTL084" runat="server" Width="70px" Visible="false"/>
                        <asp:HyperLink ID="Link084" runat="server" CssClass="word" Target="_blank"></asp:HyperLink><br/></td></tr>
                <tr><td colspan="2">承辦監造技師：
                        <asp:HyperLink ID="HyperLink085" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink>
                        <asp:Image ID="TXTDTL085_img" runat="server" Visible="False" /></td>
                    <td colspan="10">(簽章)<asp:TextBox ID="TXTDTL085" runat="server" Width="70px" Visible="False" /></td></tr>
                <tr><td colspan="2">營造單位：
                        <asp:HyperLink ID="HyperLink086" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink>
                        <asp:Image ID="TXTDTL086_img" runat="server" Visible="False" /></td>
                    <td colspan="10">(簽章)<asp:TextBox ID="TXTDTL086" runat="server" Width="70px" Visible="False" /></td></tr>
                <tr><td colspan="2">水土保持義務人：
                        <asp:HyperLink ID="HyperLink087" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink>
                    <asp:Image ID="TXTDTL087_img" runat="server" Visible="False" /></td>
                    <td colspan="10">(簽章)<asp:TextBox ID="TXTDTL087" runat="server" Width="70px" Visible="False" /></td></tr>

                </table>

                <div class="form-btn">
                    <asp:Button ID="GoHomePage" runat="server" Text="返回案件詳情" OnClick="GoHomePage_Click" />
                </div>
            </div>
        </div>
<%--        <div class="footer-s">
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

    </div>
    </form>
</body>
</html>
