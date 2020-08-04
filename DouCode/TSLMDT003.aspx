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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSLMDT003.aspx.cs" Inherits="SWCDOC_SWCDT003" %>
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
    <link rel="stylesheet" type="text/css" href="../css/hf.css" />
    
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

    <div class="wrap-s">
        <div class="head" style="background:#e9e4e4;">
            <div class="titleWrap">
                <div class="logo"><input type="image" name="logoimage" id="logoimage" src="../images/logo.png" /></div>
                <%--<div class="menu">
					<input type="image" name="imgmountantinfo" id="imgmountantinfo" src="image/click/click_1.png"  />
					<input type="image" name="imgopenswcinfo" id="imgopenswcinfo" src="image/btn/btn_2.png"  />
					<input type="image" name="imgswcmoneyinfo" id="imgswcmoneyinfo" src="image/btn/btn_3.png"  />
					 <img id="imggis" src="image/btn/btn_4.png" /> 
				</div>--%>
            </div>
            <div style="margin:0 auto;background:#6c381b;height:10px;width:100%;min-width: 1200px;"></div>
        </div>


        <div class="content-s">
            <div class="checkRecord form">
                <h1>水土保持施工監督檢查紀錄<br/><br/></h1>

                <table class="checkRecord-out">
                    <tr><td>施工監督表編號</td>
                        <td><asp:Label ID="LBDTL001" runat="server"/>
                            <asp:Label ID="LBSWC000" runat="server" Visible="false" /></td></tr>
                    <tr><td>檢查日期</td>
                        <td><asp:Label ID="TXTDTL002" runat="server" width="120px" ></asp:Label></td></tr>
                    <tr><td>檢查類型</td>
                        <td><asp:Label ID="TXTDTL003" runat="server" text="施工監督檢查"/></td></tr>
                    <tr><td>檢查單位</td>
                        <td><asp:Label ID="TXTDTL004" runat="server" width="100%" /></td></tr>
                </table>

                <br/><br/>

                <table class="checkRecord-verticalText">
                <tr><td colspan="2" >計畫名稱</td>
                    <td><asp:Label ID="LBSWC005" runat="server"/>
                        （<asp:Label ID="LBSWC007" runat="server"/>）</td></tr>
                <tr><td colspan="2">實施地點土地標示</td>
                    <td><asp:Label ID="TXTDTL005" runat="server" width="100%" MaxLength="200" /></td></tr>
                </table>

                <table class="checkRecord-verticalText-inner">
                <tr><td>一、檢查項目</td>
                    <td style="text-align: center;">目前執行情形</td>
                    <td style="text-align: center;">備註</td></tr>
                <tr><td>（一）水土保持施工告示牌</td>
                    <td><asp:Label ID="DDLDTL006" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL007" runat="server" MaxLength="100"/></td></tr>
                <tr><td>（二）開發範圍界樁</td>
                    <td><asp:Label ID="DDLDTL008" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL009" runat="server" MaxLength="100"/></td></tr>
                <tr><td>（三）開挖整地範圍界樁</td>
                    <td><asp:Label ID="DDLDTL010" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL011" runat="server" MaxLength="100"/></td></tr>
                <tr><td>（四）臨時性防災措施</td>
                    <td></td>
                    <td></td></tr>
                <tr><td class="td-padding">1.排水設施</td>
                    <td><asp:Label ID="DDLDTL012" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL013" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">2.沉砂設施</td>
                    <td><asp:Label ID="DDLDTL014" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL015" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">3.滯洪設施</td>
                    <td><asp:Label ID="DDLDTL016" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL017" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">4.土方暫置</td>
                    <td><asp:Label ID="DDLDTL018" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL019" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">5.邊坡保護措施</td>
                    <td><asp:Label ID="DDLDTL020" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL021" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">6.施工便道</td>
                    <td><asp:Label ID="DDLDTL022" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL023" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">7.其他</td>
                    <td><asp:Label ID="DDLDTL024" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL025" runat="server" MaxLength="100" /></td></tr>
                <tr><td>（五）永久性防災措施</td>
                    <td></td>
                    <td></td></tr>
                <tr><td class="td-padding">1.排水設施</td>
                    <td><asp:Label ID="DDLDTL026" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL027" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">2.沉砂設施</td>
                    <td><asp:Label ID="DDLDTL028" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL029" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">3.滯洪設施</td>
                    <td><asp:Label ID="DDLDTL030" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL031" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">4.聯外排水</td>
                    <td><asp:Label ID="DDLDTL032" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL033" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">5.擋土設施</td>
                    <td><asp:Label ID="DDLDTL034" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL035" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">6.植生工程</td>
                    <td><asp:Label ID="DDLDTL036" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL037" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">7.邊坡穩定措施</td>
                    <td><asp:Label ID="DDLDTL038" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL039" runat="server" MaxLength="100" /></td></tr>
                <tr><td class="td-padding">8.其他</td>
                    <td><asp:Label ID="DDLDTL040" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL041" runat="server" MaxLength="100" /></td></tr>
                <tr><td>（六）承辦監造技師是否在場</td>
                    <td><asp:Label ID="DDLDTL042" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL043" runat="server" MaxLength="100" /></td></tr>
                <tr><td>（七）是否備妥監造紀錄</td>
                    <td><asp:Label ID="DDLDTL044" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL045" runat="server" MaxLength="100" /></td></tr>
                <tr><td>（八）災害搶救小組是否成立</td>
                    <td><asp:Label ID="DDLDTL046" runat="server" /></td>
                    <td><asp:Label ID="TXTDTL047" runat="server" MaxLength="100" /></td></tr>

                        <tr>
                            <td colspan="3">二、實施與計畫或規定不符事項及改正期限
                                <br>
                                <br>
                        <asp:Label ID="TXTDTL048" runat="server" TextMode="MultiLine" Height="100" Width="100%" MaxLength="500" />
                                <br/><br/>改正期限：
                                <asp:Label ID="TXTDTL049" runat="server" width="120px"></asp:Label></td></tr>
                            
                        
                        <tr>
                            <td colspan="3">三、其他注意事項
                                <br>
                                <br>
                        <asp:Label ID="TXTDTL050" runat="server" TextMode="MultiLine" Height="100" Width="100%" /></td></tr>
                <tr class="input-ML0">
                    <td colspan="3">
                        四、前次施工檢查之改正事項及限期改正情形<br/><br/>
                        前次施工檢查注意事項<br/><br/>
                        <asp:Label ID="TXTDTL051" runat="server" TextMode="MultiLine" Height="100" Width="100%" /><br/><br/>
                        現場改正情形<br/><br/>
                        <asp:Label ID="TXTDTL052" runat="server" TextMode="MultiLine" Height="100" Width="100%" /><br/><br/>
                        <asp:Label ID="DDLDTL053" runat="server" />
                        <asp:Label ID="TXTDTL054" runat="server" MaxLength="50" /></td></tr>
                </table>

                <table class="checkRecord-imgUpload">
                <tr><td>五、相關單位及人員簽名<br/><br/>
                        <asp:Label ID="TXTDTL055" runat="server" TextMode="MultiLine" style="height:250px;width:100%;" /></td>
                    <td><asp:Image ID="TXTDTL056_img" runat="server" CssClass="imgUpload-l80" Visible="false" /><br/>
                        <asp:HyperLink ID="HyperLink056" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink>
                        <br/><br/><br/>
                        <asp:TextBox ID="TXTDTL056" runat="server" Width="70px" Visible="false" /></td></tr>

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
           
              <%--  
                <div class="form-btn">
                    <asp:Button ID="DataLock" runat="server" Text="確認送出" OnClientClick="return chkInput('DataLock');" OnClick="DataLock_Click" />&nbsp&nbsp
                    <asp:Button ID="SaveCase" runat="server" Text="暫時儲存" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" />&nbsp&nbsp
                    <asp:Button ID="GoHomePage" runat="server" Text="返回編輯案件" OnClick="GoHomePage_Click" />
                </div>--%>

            </div>
        </div>
        <div class="copyArea">
            <div class="copyWrap">
                <div style="line-height:1.5;font-size:14px;font-weight:bold;">
                    <span style="font-size: 20px;">臺北市政府工務局大地工程處</span>
                    <br> 110臺北市信義區松德路300號3樓
                    <br> (02)27593001 &nbsp;&nbsp;臺北市民當家熱線1999
                </div>
                <div class="fL"><!-- <img src="image/footer_line.png" alt=""> --></div>
                <div>
                    <ul style="line-height:1.7;margin-top:4px;margin-left:50px;font-size: 14px; ">
                        <li style="list-style: none;margin-left:-20px;">注意事項</li>
                        <li>本系統查詢結果僅做案件進度之參考應用，不做為其他證明用途。</li>
                        <li>為保護個人隱私權利，請勿任意查詢他人之資料。</li>
                    </ul>
                </div>

                <!-- <div style="margin-top:-9px;margin-left:-10px; width: 479px">
                    <ul style="font-size: 14px; ">
                        <li style="list-style: none;margin-left:-20px;">注意事項</li>
                        <li>本查詢結果地籍資料使用臺北市政府地政局103年3月圖資</li>
                        <li>如土地位置有疑義應以臺北市政府地政局各地政事務所核發之謄本</li>
                        <li>及大地工程處列表清冊為準</li>
                        <li>本查詢結果僅作為水土保持計畫申請之參考，不作為其他證明用途</li>
                        <li>為保護個人隱私權利，請勿任意查詢他人之資料。</li>
                    </ul>
                </div> -->

                <div class="fL"><!-- <img src="image/footer_line.png" alt=""> --></div>
                <div style="font-size: 14px;padding-top: 10px;">
                    <div>資料更新：<span id="todaylabel">2018.1.2</span></div>
                    <br> <div style="margin-top:7px;">來訪人數：<img alt="瀏覽人次" style="margin-bottom:-5px;" src="servicelogincount.aspx?webpagename=山坡地土地利用資訊查詢結果201610版" /></div>
                </div>
				<P></P>
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
