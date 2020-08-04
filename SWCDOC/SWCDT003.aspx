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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCDT003.aspx.cs" Inherits="SWCDOC_SWCDT003" %>
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
        function addtext(unitelement, peopleelement, listelement) {
            listelement.value = listelement.value + "\r\n" + unitelement.value + " " + peopleelement.value;
            return false;
        }
        function chkInput(jChkType) {
            jCHKValue01 = document.getElementById("TXTDTL002").value;

            if (jCHKValue01.trim() == '') {
                alert('請輸入檢查日期');
                document.getElementById("TXTDTL002").focus();
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
            <div class="checkRecord form">
                <h1>水土保持施工監督檢查紀錄</h1>

                <div class="detailsMenu-btn">
                    <asp:ImageButton ID="OutPdf" runat="server" title="輸出PDF" ImageUrl="../images/btn/icon_exportpdf.png" OnClick="OutPdf_Click" Visible="false" />
                </div>

                <br/><br/>

                <table class="checkRecord-out">
                    <tr><td>施工監督表編號</td>
                        <td><asp:Label ID="LBDTL001" runat="server"/>
                            <asp:Label ID="LBSWC000" runat="server" Visible="false" /></td></tr>
                    <tr><td>檢查日期<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTDTL002" runat="server" width="120px" autocomplete="off" ></asp:TextBox>
                            <asp:CalendarExtender ID="TXTDTL002_CalendarExtender" runat="server" TargetControlID="TXTDTL002" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                    <tr><td>檢查類型</td>
                        <td><asp:Label ID="TXTDTL003" runat="server" text="施工監督檢查"/></td></tr>
                    <tr><td>檢查單位</td>
                        <td><asp:Label ID="TXTDTL004" runat="server" width="100%" /></td></tr>
                    <tr><td>面積</td>
                        <td><asp:Label ID="LBSWC023" runat="server" /> 公頃</td></tr>
                </table>

                <br/><br/>

                <table class="checkRecord-verticalText">
                <tr><td rowspan="5">水土保持書件</td>
                    <td>計畫名稱</td>
                    <td><asp:Label ID="LBSWC005" runat="server"/>
                        （<asp:Label ID="LBSWC007" runat="server"/>）</td></tr>
                <tr><td>核定日期文號</td>
                    <td>臺北市政府
                        <asp:Label ID="LBSWC038" runat="server"/>
                        <asp:Label ID="LBSWC039" runat="server"/>
                        號函</td></tr>
                <tr><td>水土保持施工許可證日期文號</td>
                    <td>臺北市政府
                        <asp:Label ID="LBSWC043" runat="server"/>
                        <asp:Label ID="LBSWC044" runat="server"/>
                        號函</td></tr>
                <tr><td>開工日期</td>
                    <td><asp:Label ID="LBSWC051" runat="server"/></td></tr>
                <tr><td>預定完工日期</td>
                    <td><asp:Label ID="LBSWC052" runat="server"/></td></tr>
                <tr><td rowspan="3">水土保持義務人</td>
                    <td>姓名或名稱</td>
                    <td><asp:Label ID="LBSWC013" runat="server"/></td></tr>
                <tr><td>身分證或營利事業統一編號</td>
                    <td><asp:Label ID="LBSWC013ID" runat="server"/></td></tr>
                <tr><td>住居所或營業所</td>
                    <td><asp:Label ID="LBSWC014" runat="server"/></td></tr>
                <tr><td rowspan="5">承辦監造技師</td>
                    <td>姓名</td>
                    <td><asp:Label ID="LBSWC021" runat="server"/></td></tr>
                <tr><td>執業機構名稱</td>
                    <td><asp:Label ID="LBSWC021Name" runat="server"/></td></tr>
                <tr><td>執業執照字號</td>
                    <td><asp:Label ID="LBSWC021OrgIssNo" runat="server"/></td></tr>
                <tr><td>營利事業統一編號</td>
                    <td><asp:Label ID="LBSWC021OrgGUINo" runat="server"/></td></tr>
                <tr><td>電話</td>
                    <td><asp:Label ID="LBSWC021OrgTel" runat="server"/></td></tr>
                <tr><td colspan="2">實施地點土地標示</td>
                    <td><asp:TextBox ID="TXTDTL005" runat="server" width="100%" MaxLength="200" /></td></tr>
                </table>

                <table class="checkRecord-verticalText-inner">
                <tr><td>一、檢查項目</td>
                    <td style="text-align: center;">目前執行情形</td>
                    <td style="text-align: center;">備註</td></tr>
                <tr><td>（一）水土保持施工告示牌</td>
                    <td><asp:DropDownList ID="DDLDTL006" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL007" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL007_count','100');" />
                        <br/><asp:Label ID="TXTDTL007_count" runat="server" Text="(0/100)" ForeColor="Red" />
                       </td></tr>
                <tr><td>（二）開發範圍界樁</td>
                    <td><asp:DropDownList ID="DDLDTL008" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL009" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL009_count','100');" />
                        <br/><asp:Label ID="TXTDTL009_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td>（三）開挖整地範圍界樁</td>
                    <td><asp:DropDownList ID="DDLDTL010" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL011" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL011_count','100');" />
                        <br/><asp:Label ID="TXTDTL011_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td>（四）臨時性防災措施</td>
                    <td></td>
                    <td></td></tr>
                <tr><td class="td-padding">1.排水設施</td>
                    <td><asp:DropDownList ID="DDLDTL012" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL013" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL013_count','100');" />
                        <br/><asp:Label ID="TXTDTL013_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">2.沉砂設施</td>
                    <td><asp:DropDownList ID="DDLDTL014" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL015" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL015_count','100');" />
                        <br/><asp:Label ID="TXTDTL015_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">3.滯洪設施</td>
                    <td><asp:DropDownList ID="DDLDTL016" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL017" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL017_count','100');" />
                        <br/><asp:Label ID="TXTDTL017_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">4.土方暫置</td>
                    <td><asp:DropDownList ID="DDLDTL018" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL019" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL019_count','100');" />
                        <br/><asp:Label ID="TXTDTL019_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">5.邊坡保護措施</td>
                    <td><asp:DropDownList ID="DDLDTL020" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL021" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL021_count','100');" />
                        <br/><asp:Label ID="TXTDTL021_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">6.施工便道</td>
                    <td><asp:DropDownList ID="DDLDTL022" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL023" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL023_count','100');" />
                        <br/><asp:Label ID="TXTDTL023_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">7.其他</td>
                    <td><asp:DropDownList ID="DDLDTL024" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL025" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL025_count','100');" />
                        <br/><asp:Label ID="TXTDTL025_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td>（五）永久性防災措施</td>
                    <td></td>
                    <td></td></tr>
                <tr><td class="td-padding">1.排水設施</td>
                    <td><asp:DropDownList ID="DDLDTL026" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL027" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL027_count','100');" />
                        <br/><asp:Label ID="TXTDTL027_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">2.沉砂設施</td>
                    <td><asp:DropDownList ID="DDLDTL028" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL029" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL029_count','100');" />
                        <br/><asp:Label ID="TXTDTL029_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">3.滯洪設施</td>
                    <td><asp:DropDownList ID="DDLDTL030" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL031" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL031_count','100');" />
                        <br/><asp:Label ID="TXTDTL031_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">4.聯外排水</td>
                    <td><asp:DropDownList ID="DDLDTL032" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL033" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL033_count','100');"  />
                        <br/><asp:Label ID="TXTDTL033_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">5.擋土設施</td>
                    <td><asp:DropDownList ID="DDLDTL034" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL035" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL035_count','100');"  />
                        <br/><asp:Label ID="TXTDTL035_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">6.植生工程</td>
                    <td><asp:DropDownList ID="DDLDTL036" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL037" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL037_count','100');"  />
                        <br/><asp:Label ID="TXTDTL037_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">7.邊坡穩定措施</td>
                    <td><asp:DropDownList ID="DDLDTL038" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL039" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL039_count','100');"  />
                        <br/><asp:Label ID="TXTDTL039_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">8.其他</td>
                    <td><asp:DropDownList ID="DDLDTL040" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL041" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL041_count','100');"  />
                         <br/><asp:Label ID="TXTDTL041_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td>（六）承辦監造技師是否在場</td>
                    <td><asp:DropDownList ID="DDLDTL042" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL043" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL043_count','100');"  />
                        <br/><asp:Label ID="TXTDTL043_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td>（七）是否備妥監造紀錄</td>
                    <td><asp:DropDownList ID="DDLDTL044" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL045" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL045_count','100');"  />
                        <br/><asp:Label ID="TXTDTL045_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td>（八）災害搶救小組是否成立</td>
                    <td><asp:DropDownList ID="DDLDTL046" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL047" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL047_count','100');"/>
                        <br/><asp:Label ID="TXTDTL047_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>

                        <tr>
                            <td colspan="3">二、實施與計畫或規定不符事項及改正期限
                                <br>
                                <br>
                        <asp:TextBox ID="TXTDTL048" runat="server" TextMode="MultiLine" Height="100" Width="100%" onkeyup="textcount(this,'TXTDTL048_count','500');" MaxLength="500" />
                        <asp:Label ID="TXTDTL048_count" runat="server" Text="(0/500)" ForeColor="Red" />
                                <br/><br/>改正期限：
                                <asp:TextBox ID="TXTDTL049" runat="server" width="120px"></asp:TextBox>
                                <asp:CalendarExtender ID="TXTDTL049_CalendarExtender" runat="server" TargetControlID="TXTDTL049" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                            
                        
                        <tr>
                            <td colspan="3">三、其他注意事項
                                <br>
                                <br>
                        <asp:TextBox ID="TXTDTL050" runat="server" TextMode="MultiLine" Height="100" Width="100%" onkeyup="textcount(this,'TXTDTL050_count','500');" />
                        <asp:Label ID="TXTDTL050_count" runat="server" Text="(0/500)" ForeColor="Red" />
                            </td>
                        </tr>
                <tr class="input-ML0">
                    <td colspan="3">
                        四、前次施工檢查之改正事項及限期改正情形<br/><br/>
                        前次施工檢查注意事項<br/><br/>
                        <asp:TextBox ID="TXTDTL051" runat="server" TextMode="MultiLine" Height="100" Width="100%" onkeyup="textcount(this,'TXTDTL051_count','1000');" />
                        <asp:Label ID="TXTDTL051_count" runat="server" Text="(0/1000)" ForeColor="Red" /><br/><br/>
                        現場改正情形<br/><br/>
                        <asp:TextBox ID="TXTDTL052" runat="server" TextMode="MultiLine" Height="100" Width="100%" onkeyup="textcount(this,'TXTDTL052_count','500');" />
                        <asp:Label ID="TXTDTL052_count" runat="server" Text="(0/500)" ForeColor="Red" /><br/><br/>
                        前次監督檢查缺失之複查&nbsp&nbsp&nbsp(&nbsp&nbsp&nbsp是否已改正：
                        <asp:DropDownList ID="DDLDTL053" runat="server" />
                        <asp:TextBox ID="TXTDTL054" runat="server" placeholder="其他說明" MaxLength="50" />&nbsp&nbsp&nbsp)</td></tr>
                </table>

                <table class="checkRecord-imgUpload">
                <tr><td>五、相關單位及人員簽名<br/><br/>
                        <asp:TextBox ID="TXTDTL055" runat="server" TextMode="MultiLine" style="height:250px;width:100%;" /></td>
                    <td><asp:Image ID="TXTDTL056_img" runat="server" CssClass="imgUpload-l80" Visible="false" /><br/>
                        <asp:HyperLink ID="HyperLink056" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink>
                        <br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/><br/>
                        <asp:TextBox ID="TXTDTL056" runat="server" Width="70px" Visible="false" />
                        <asp:FileUpload ID="TXTDTL056_fileupload" runat="server" />
                        <asp:Button ID="TXTDTL056_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL056_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL056_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL056_fileuploaddel_Click" /></td></tr>

                        <tr>
                            <td colspan="2">六、說明：
                                <br>
                                <br>（一） 本項檢查係屬行政監督檢查，屬未抽驗、隱蔽或工程品質部分，應由水土保持義務人及承辦監造技師負責。
                                <br>
                                <br>（二） 承辦監造技師未能到場時，應以書面方式委任符合水土保持法規定之技師代理之。
                                <br>
                                <br> 七、填表注意事項：
                                <br>
                                <br>（一） 如有未依核定計畫施作情形，請於來函及紀錄中說明與計畫（或規定）不符事項。
                                <br>
                                <br>（二） 前次監督檢查缺失及應注意事項之複查，請註明辦理情形及是否同意結案（或持續列管）。
                                <br>
                                <br>（三） 完工檢查請就各項水土保持設施逐一檢查，並抽查各項設施斷面或尺寸。
                                <br>
                                <br>（四） 檢查紀錄請呈現公會戳章及出席人員親筆簽名，相片說明應與紀錄文字勾稽。
                            </td>
                        </tr>
                    </table>
                    <br>
                    <br>
                </table>
           
                <table class="checkRecord-fileUpload">
                <tr><td colspan="2">
                    <asp:Label ID="LBSWC005a" runat="server" CssClass="redn"/></td></tr>
                <tr><td>現場相片一</td>
                    <td>現場相片二</td></tr>
                <tr><td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL057" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL057_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL057_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL057_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL057_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL057_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:HyperLink ID="HyperLink057" runat="server" CssClass="imgUpload" Target="_blank" />
                        <asp:Image ID="TXTDTL057_img" runat="server" CssClass="imgUpload-l80" Visible="false" />
                        <br/>
                        <asp:TextBox ID="TXTDTL058" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td>
                    <td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL059" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL059_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL059_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL059_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL059_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL059_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:HyperLink ID="HyperLink059" runat="server" CssClass="imgUpload" Target="_blank" />
                        <asp:Image ID="TXTDTL059_img" runat="server" CssClass="imgUpload-l80" Visible="false" />
                        <br/>
                        <asp:TextBox ID="TXTDTL060" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td></tr>
                        <tr>
                            <td>現場相片三</td>
                            <td>現場相片四</td>
                        </tr>
                        <tr>
                            <td>
                        <div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL061" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL061_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL061_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL061_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL061_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL061_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:HyperLink ID="HyperLink061" runat="server" CssClass="imgUpload" Target="_blank" />
                        <asp:Image ID="TXTDTL061_img" runat="server" CssClass="imgUpload-l80" Visible="false" />
                        <br/>
                        <asp:TextBox ID="TXTDTL062" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td>
                    <td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL063" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL063_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL063_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL063_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL063_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL063_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:HyperLink ID="HyperLink063" runat="server" CssClass="imgUpload" Target="_blank" />
                        <asp:Image ID="TXTDTL063_img" runat="server" CssClass="imgUpload-l80" Visible="false" />
                        <br/>
                        <asp:TextBox ID="TXTDTL064" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td></tr>
                <tr><td>現場相片五</td>
                    <td>現場相片六</td></tr>
                <tr><td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL065" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL065_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL065_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL065_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL065_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL065_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:HyperLink ID="HyperLink065" runat="server" CssClass="imgUpload" Target="_blank" />
                        <asp:Image ID="TXTDTL065_img" runat="server" CssClass="imgUpload-l80" Visible="false" />
                        <br/>
                        <asp:TextBox ID="TXTDTL066" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td>
                    <td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL067" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL067_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL067_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL067_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL067_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL067_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:HyperLink ID="HyperLink067" runat="server" CssClass="imgUpload" Target="_blank" />
                        <asp:Image ID="TXTDTL067_img" runat="server" CssClass="imgUpload-l80" Visible="false" />
                        <br/>
                        <asp:TextBox ID="TXTDTL068" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td></tr>
                <tr><td colspan="4">附件</td></tr>
                <tr><td colspan="4">附件檔案上傳：
                        <asp:TextBox ID="TXTDTL070" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTDTL070_fileupload" runat="server" />
                        <asp:Button ID="TXTDTL070_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL070_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL070_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請確認真的要刪除檔案!!!')" OnClick="TXTDTL070_fileuploaddel_Click" />
                        <br/><br/>
                        <span style="color:red;">檔案大小請小於 50 Mb，請上傳 pdf, odt, word 格式</span>
                        <br/><br/>
                        <asp:HyperLink ID="Link070" runat="server" Text ="其他附件檔案下載" Target="_blank" Visible="false" /></td></tr>
                    </table>
                
                <div class="form-btn">
                    <asp:Button ID="DataLock" runat="server" Text="確認送出" OnClientClick="return chkInput('DataLock');" OnClick="DataLock_Click" />&nbsp&nbsp
                    <asp:Button ID="SaveCase" runat="server" Text="暫時儲存" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" />&nbsp&nbsp
                    <asp:Button ID="GoHomePage" runat="server" Text="返回編輯案件" OnClick="GoHomePage_Click" />
                </div>

            </div>
        </div>
<%--        <div class="footer-s">
            <div class="footer-s-green"></div>
            <div class="footer-b-brown">
                <p> <span class="span1">臺北市政府工務局大地工程處</span>
                    <br><span class="span2">110臺北市信義區松德路300號3樓 　(02)27593001   臺北市民當家熱線1999</span>
                    <br><span class="span2">資料更新：2017.5.19　來訪人數：2378 </span></p>
            </div>
        </div>--%>

        
            <div class="footer">
                 <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                           <span class="span2">110臺北市信義區松德路300號3樓 　服務專線(02)27591109   臺北市民當家熱線1999</span><br/>
                            <span class="span2">建議使用IE11.0(含)以上，Chrome或Firefox版本瀏覽器 資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                           <span class="span2">客服電話：02-27593001#3718 許先生 本系統由多維空間資訊有限公司開發維護 TEL:(02)27929328</span></p>
            </div>

    </div>
        
        <asp:Literal ID="error_msg" runat="server"></asp:Literal>

    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/inner.js"></script>
    <script src="../js/allhref.js"></script>













    </div>
    </form>
</body>
</html>
