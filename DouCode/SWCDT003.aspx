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
            jCHKValue03 = document.getElementById("TXTDTL004").value;

            if (jCHKValue01.trim() == '') {
                alert('請輸入檢查日期');
                document.getElementById("TXTDTL002").focus();
                return false;
            }
            if (jCHKValue03.trim() == '') {
                alert('請輸入檢查單位');
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

        <div class="header">
            <a href="http://tcgeswc.taipei.gov.tw/tslmservice/" title="臺北市政府大地工程處坡地管理資料庫" class="header-link"><img src="../images/banner.jpg" alt=""></a>
            <div class="header-menu">
                <a href="SwcDtl03.aspx"><img src="../images/title/swcch_m01b.png" alt="施工監督檢查"/></a>
                <a href="SwcDtl07.aspx"><img src="../images/title/swcch_m02a.png" alt="設施維護檢查"/></a>
            </div>
            <a href="#" style="display: inline-block;"><img src="../images/title/title_swcchg.png" alt=""/></a>
            <span class="l-span" style="float: right;">
                <asp:Literal ID="TextUserName" runat="server">大雷包，您好</asp:Literal>&nbsp&nbsp&nbsp
                <asp:Button ID="BtnLogOut" runat="server" Text="登出" Height="22px" UseSubmitBehavior="False" OnClick="BtnLogOut_Click" Font-Names="標楷體" />
            </span>
            <%--<img src="../images/btn/icon_exportpdf.png" alt="輸出pdf" class="l-Block">--%>
        </div>
        <div class="content">
            <div class="checkRecord form">
                <h1>水土保持施工監督檢查紀錄<br/><br/></h1>

                <table class="checkRecord-out">
                    <tr><td>施工監督表編號</td>
                        <td><asp:Label ID="LBDTL001" runat="server"/>
                            <asp:Label ID="LBSWC000" runat="server" Visible="false" /></td></tr>
                    <tr><td>行政管理編號</td>
                        <td><asp:Label ID="LBSWC002" runat="server"/></td></tr>
                    <tr><td>檢查日期<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTDTL002" runat="server" width="120px" ></asp:TextBox>
                            <asp:CalendarExtender ID="TXTDTL002_CalendarExtender" runat="server" TargetControlID="TXTDTL002" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                    <tr><td>檢查類型</td>
                        <td><asp:Label ID="TXTDTL003" runat="server" text="施工監督檢查"/></td></tr>
                    <tr><td>檢查單位</td>
                        <td><asp:Label ID="TXTDTL004" runat="server" width="100%" /></td></tr>
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
                    <td><asp:TextBox ID="TXTDTL007" runat="server" MaxLength="100"/></td></tr>
                <tr><td>（二）開發範圍界樁</td>
                    <td><asp:DropDownList ID="DDLDTL008" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL009" runat="server" MaxLength="100"/></td></tr>
                <tr><td>（三）開挖整地範圍界樁</td>
                    <td><asp:DropDownList ID="DDLDTL010" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL011" runat="server" MaxLength="100"/></td></tr>
                <tr><td>（四）臨時性防災措施</td>
                    <td></td>
                    <td></td></tr>
                <tr><td class="td-padding">1.排水設施</td>
                    <td><asp:DropDownList ID="DDLDTL012" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL013" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">2.沉砂設施</td>
                    <td><asp:DropDownList ID="DDLDTL014" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL015" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">3.滯洪設施</td>
                    <td><asp:DropDownList ID="DDLDTL016" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL017" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">4.土方暫置</td>
                    <td><asp:DropDownList ID="DDLDTL018" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL019" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">5.邊坡保護措施</td>
                    <td><asp:DropDownList ID="DDLDTL020" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL021" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">6.施工便道</td>
                    <td><asp:DropDownList ID="DDLDTL022" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL023" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">7.其他</td>
                    <td><asp:DropDownList ID="DDLDTL024" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL025" runat="server" MaxLength="100" /></td></tr>
                <tr><td>（五）永久性防災措施</td>
                    <td></td>
                    <td></td></tr>
                <tr><td class="td-padding">1.排水設施</td>
                    <td><asp:DropDownList ID="DDLDTL026" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL027" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">2.沉砂設施</td>
                    <td><asp:DropDownList ID="DDLDTL028" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL029" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">3.滯洪設施</td>
                    <td><asp:DropDownList ID="DDLDTL030" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL031" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">4.聯外排水</td>
                    <td><asp:DropDownList ID="DDLDTL032" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL033" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">5.擋土設施</td>
                    <td><asp:DropDownList ID="DDLDTL034" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL035" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">6.植生工程</td>
                    <td><asp:DropDownList ID="DDLDTL036" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL037" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">7.邊坡穩定措施</td>
                    <td><asp:DropDownList ID="DDLDTL038" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL039" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">8.其他</td>
                    <td><asp:DropDownList ID="DDLDTL040" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL041" runat="server" MaxLength="100" /></td></tr>
                <tr><td>（六）承辦監造技師是否在場</td>
                    <td><asp:DropDownList ID="DDLDTL042" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL043" runat="server" MaxLength="100" /></td></tr>
                <tr><td>（七）是否備妥監造紀錄</td>
                    <td><asp:DropDownList ID="DDLDTL044" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL045" runat="server" MaxLength="100" /></td></tr>
                <tr><td>（八）災害搶救小組是否成立</td>
                    <td><asp:DropDownList ID="DDLDTL046" runat="server" /></td>
                    <td><asp:TextBox ID="TXTDTL047" runat="server" MaxLength="100" /></td></tr>

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
                        <asp:TextBox ID="TXTDTL051" runat="server" TextMode="MultiLine" Height="100" Width="100%" onkeyup="textcount(this,'TXTDTL051_count','500');" />
                        <asp:Label ID="TXTDTL051_count" runat="server" Text="(0/500)" ForeColor="Red" /><br/><br/>
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
           
               
                <div class="form-btn">
                    <asp:Button ID="DataLock" runat="server" Text="確認送出" OnClientClick="return chkInput('DataLock');" OnClick="DataLock_Click" />&nbsp&nbsp
                    <asp:Button ID="SaveCase" runat="server" Text="暫時儲存" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" />&nbsp&nbsp
                    <asp:Button ID="GoHomePage" runat="server" Text="返回案件列表" OnClick="GoHomePage_Click" />
                </div>

            </div>
        </div>

    </div>
        
        <asp:Literal ID="error_msg" runat="server"></asp:Literal>

    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/inner.js"></script>














    </div>
    </form>
</body>
</html>
