<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCDT003v2.aspx.cs" Inherits="SWCDOC_SWCDT003" %>
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
    <link rel="stylesheet" type="text/css" href="../css/all.css?202108240200" />
	<link rel="stylesheet" type="text/css" href="../css/iris.css?202108240200" />
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
                alert('請輸入檢查類型');
                document.getElementById("TXTDTL003").focus();
                return false;
            }
            if (jCHKValue03.trim() == '') {
                alert('請輸入檢查單位');
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
                        <li><a href="https://swc.taipei/swcinfo/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="http://tslm.swc.taipei/tslmwork/" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
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
        <div class="content-t">
            <div class="checkRecord form">
                <h1>水土保持施工監督檢查紀錄<br/><br/></h1>
                
                <div class="detailsMenu-btn">
                    <asp:ImageButton ID="OutPdf" runat="server" title="輸出PDF" ImageUrl="../images/btn/icon_exportpdf.png" OnClick="OutPdf_Click" />
                </div>

                <table class="checkRecord-out">
                    <tr><td>施工監督表編號</td>
                        <td><asp:Label ID="LBDTL001" runat="server"/>
                            <asp:Label ID="LBSWC000" runat="server" Visible="false" /></td></tr>
                    <tr><td>檢查日期</td>
                        <td><asp:Label ID="TXTDTL002" runat="server" width="120px"></asp:Label></td></tr>
                    <tr><td>檢查結果</td>
                        <td><asp:Label ID="TXTDTL071" runat="server" /></td></tr>
                    <tr style="display:none;">
                        <td>檢查類型</td>
                        <td><asp:Label ID="TXTDTL003" runat="server" /></td></tr>
                    <tr><td>檢查單位</td>
                        <td><asp:Label ID="TXTDTL004" runat="server" /></td></tr>
                    <tr><td>面積</td>
                        <td><asp:Label ID="LBSWC023" runat="server" /> 公頃</td></tr>
                    <tr><td>函送紀錄日期</td>
                        <td><asp:Label ID="TXTSENDDATE" runat="server" /> </td></tr>
                    <tr><td>送達天數</td>
                        <td><asp:Label ID="TXTSENDDAY" runat="server" /> </td></tr>
                </table>

                <br/><br/>

                <table class="checkRecord_sarea">
                <tr><td rowspan="7" class="CR01">水土保持書件</td>
                    <td class="CRTT">計畫名稱</td>
                    <td><asp:Label ID="LBSWC005" runat="server"/>
                        （<asp:Label ID="LBSWC007" runat="server"/>）</td></tr>
                <tr><td class="CRTT">核定日期文號</td>
                    <td>臺北市政府
                        <asp:Label ID="LBSWC038" runat="server"/>
                        <asp:Label ID="LBSWC039" runat="server"/>
                        函</td></tr>
                <tr><td class="CRTT">水土保持施工許可證日期文號</td>
                    <td>臺北市政府
                        <asp:Label ID="LBSWC043" runat="server"/>
                        <asp:Label ID="LBSWC044" runat="server"/>
                        函</td></tr>
                <tr><td class="CRTT">開工日期</td>
                    <td><asp:Label ID="LBSWC051" runat="server"/></td></tr>
                <tr><td class="CRTT">預定完工日期</td>
                    <td><asp:Label ID="LBSWC052" runat="server"/></td></tr>
                <tr><td class="CRTT">工程進度</td>
                    <td><asp:Label ID="LBSWCO01" runat="server"/></td></tr>
                <tr><td class="CRTT">檢查結果</td>
                    <td><asp:Label ID="LBSWC071" runat="server"/></td></tr>
                <tr><td rowspan="3" class="CR01">水土保持義務人</td>
                    <td class="CRTT">姓名或名稱</td>
                    <td><asp:Label ID="LBSWC013" runat="server"/></td></tr>
                <tr style="display:none;">
                    <td class="CRTT">身分證或營利事業統一編號</td>
                    <td><asp:Label ID="LBSWC013ID" runat="server"/></td></tr>
                <tr><td class="CRTT">住居所或營業所</td>
                    <td><asp:Label ID="LBSWC014" runat="server"/></td></tr>
                <tr><td class="CRTT">電話</td>
                    <td><asp:Label ID="LBSWC013TEL" runat="server"/></td></tr>
                <tr><td rowspan="4">承辦監造技師</td>
                    <td class="CRTT">姓名</td>
                    <td><asp:Label ID="LBSWC021" runat="server"/></td></tr>
                <tr><td class="CRTT">執業機構名稱</td>
                    <td><asp:Label ID="LBSWC021Name" runat="server"/></td></tr>
                <tr style="display:none;">
                    <td class="CRTT">執業執照字號</td>
                    <td><asp:Label ID="LBSWC021OrgIssNo" runat="server"/></td></tr>
                <tr style="display:none;">
                    <td class="CRTT">營利事業統一編號</td>
                    <td><asp:Label ID="LBSWC021OrgGUINo" runat="server"/></td></tr>
                <tr><td class="CRTT">執業機構地址</td>
                    <td><asp:Label ID="LBSWC021OrgAddr" runat="server"/></td></tr>
                <tr><td class="CRTT">電話</td>
                    <td><asp:Label ID="LBSWC021OrgTel" runat="server"/></td></tr>
                <tr><td class="CRTT" colspan="2">實施地點土地標示</td>
                    <td><asp:Label ID="TXTDTL005" runat="server" /></td></tr>
                </table>

                <table class="checkRecord_TB">
                <tr><td style="width:25%">一、檢查項目</td>
                    <td style="text-align: center;">檢查結果</td></tr>
                <tr><td>（一）水土保持施工告示牌</td>
                    <td><asp:Label ID="DDLDTL006" runat="server" /><br/><asp:Label ID="TXTDTL007" runat="server" /></td></tr>
                <tr><td>（二）開發範圍界樁</td>
                    <td><asp:Label ID="DDLDTL008" runat="server" /><br/><asp:Label ID="TXTDTL009" runat="server" /></td></tr>
                <tr><td>（三）開挖整地範圍界樁</td>
                    <td><asp:Label ID="DDLDTL010" runat="server" /><br/><asp:Label ID="TXTDTL011" runat="server"/></td></tr>
                <tr><td>（四）臨時性防災措施</td>
                    <td></td></tr>
                <tr><td class="td-padding">1.排水設施</td>
                    <td><asp:Label ID="DDLDTL012" runat="server" /><br/><asp:Label ID="TXTDTL013" runat="server"  /></td></tr>
                <tr><td class="td-padding">2.沉砂設施</td>
                    <td><asp:Label ID="DDLDTL014" runat="server" /><br/><asp:Label ID="TXTDTL015" runat="server" /></td></tr>
                <tr><td class="td-padding">3.滯洪設施</td>
                    <td><asp:Label ID="DDLDTL016" runat="server" /><br/><asp:Label ID="TXTDTL017" runat="server"/></td></tr>
                <tr><td class="td-padding">4.土方暫置</td>
                    <td><asp:Label ID="DDLDTL018" runat="server" /><br/><asp:Label ID="TXTDTL019" runat="server" /></td></tr>
                <tr><td class="td-padding">5.邊坡保護措施</td>
                    <td><asp:Label ID="DDLDTL020" runat="server" /><br/><asp:Label ID="TXTDTL021" runat="server" /></td></tr>
                <tr><td class="td-padding">6.施工便道</td>
                    <td><asp:Label ID="DDLDTL022" runat="server" /><br/><asp:Label ID="TXTDTL023" runat="server" /></td></tr>
                <tr><td class="td-padding">7.臨時攔砂設施(如砂包、防溢座等)</td>
                    <td><asp:Label ID="DDLDTL072" runat="server" /><br/><asp:Label ID="TXTDTL073" runat="server" /></td></tr>
                <tr><td class="td-padding">8.其他</td>
                    <td><asp:Label ID="DDLDTL024" runat="server" /><br/><asp:Label ID="TXTDTL025" runat="server" /></td></tr>
                <tr><td>（五）永久性防災措施</td>
                    <td></td></tr>
                <tr><td class="td-padding">1.排水設施</td>
                    <td><asp:Label ID="DDLDTL026" runat="server" /><br/><asp:Label ID="TXTDTL027" runat="server" /></td></tr>
                <tr><td class="td-padding">2.沉砂設施</td>
                    <td><asp:Label ID="DDLDTL028" runat="server" /><br/><asp:Label ID="TXTDTL029" runat="server"/></td></tr>
                <tr><td class="td-padding">3.滯洪設施</td>
                    <td><asp:Label ID="DDLDTL030" runat="server" /><br/><asp:Label ID="TXTDTL031" runat="server" /></td></tr>
                <tr><td class="td-padding">4.聯外排水</td>
                    <td><asp:Label ID="DDLDTL032" runat="server" /><br/><asp:Label ID="TXTDTL033" runat="server" /></td></tr>
                <tr><td class="td-padding">5.擋土設施</td>
                    <td><asp:Label ID="DDLDTL034" runat="server" /><br/><asp:Label ID="TXTDTL035" runat="server" /></td></tr>
                <tr><td class="td-padding">6.植生工程</td>
                    <td><asp:Label ID="DDLDTL036" runat="server" /><br/><asp:Label ID="TXTDTL037" runat="server" /></td></tr>
                <tr><td class="td-padding">7.邊坡穩定措施</td>
                    <td><asp:Label ID="DDLDTL038" runat="server" /><br/><asp:Label ID="TXTDTL039" runat="server" /></td></tr>
                <tr><td class="td-padding">8.其他</td>
                    <td><asp:Label ID="DDLDTL040" runat="server" /><br/><asp:Label ID="TXTDTL041" runat="server" /></td></tr>
                <tr><td>（六）承辦監造技師是否在場</td>
                    <td><asp:Label ID="DDLDTL042" runat="server" /><br/><asp:Label ID="TXTDTL043" runat="server" /></td></tr>
                <tr><td>（七）是否備妥監造紀錄</td>
                    <td><asp:Label ID="DDLDTL044" runat="server" /><br/><asp:Label ID="TXTDTL045" runat="server" /></td></tr>
                <tr style="display:none;">
                    <td>（八）災害搶救小組是否成立</td>
                    <td><asp:Label ID="DDLDTL046" runat="server" /><br/><asp:Label ID="TXTDTL047" runat="server" /></td></tr>
                </table>
                <table class="CR02">
                 <tr>
                     <td colspan="3">二、通知水土保持義務人及營造單位施工缺失改正事項
                         <br />
                         <br />
                 <asp:Label ID="TXTDTL048" runat="server" /><br/><br/>改正期限：
                         <asp:Label ID="TXTDTL049" runat="server" ></asp:Label></td></tr>

                    
                        <asp:Panel ID="PNDA3a" runat="server" Visible="false">
                <tr><td colspan="3">
                    三、未依計畫施作事項及改正期限：<br/><br/>
                        <asp:Label ID="LBDTL051" runat="server" TextMode="MultiLine" Text="2255666" Height="100" Width="100%" />
                                <br/><br/>改正期限：
                                <asp:Label ID="TXTDTL074" runat="server" width="120px" AUTOCOMPLETE="off"></asp:Label>
                                </td></tr>
                        </asp:Panel>
                            
                        <asp:Panel ID="PNDA3b" runat="server" Visible="false">
                <tr class="input-ML0">
                    <td colspan="3">
                        三、前次施工檢查之改正事項及限期改正情形<br/><br/>
                        前次施工檢查注意事項<br/><br/>
                        <asp:Label ID="TXTDTL051" runat="server" /><br/><br/>
                        現場改正情形<br/><br/>
                        <asp:Label ID="TXTDTL052" runat="server" /><br/><br/>
                        前次監督檢查缺失之複查&nbsp&nbsp&nbsp(&nbsp&nbsp&nbsp是否已改正：
                        <asp:Label ID="DDLDTL053" runat="server" />
                        <asp:Label ID="TXTDTL054" runat="server" placeholder="其他說明" />&nbsp&nbsp&nbsp)</td></tr>
                        </asp:Panel>
                        
                        <tr>
                            <td colspan="3">四、其他注意事項
                                <br>
                                <br>
                        <asp:Label ID="TXTDTL050" runat="server" /></td></tr>
                </table>

                <table class="checkRecord-imgUpload">
                <tr><td>五、相關單位及人員簽名<br/><br/>
                        <asp:Label ID="TXTDTL055" runat="server" TextMode="MultiLine" style="height:250px;width:100%;" /></td>
                    <td><asp:Image ID="TXTDTL056_img" runat="server" CssClass="imgUpload-l80" Visible="false" /><br/>
                        <asp:HyperLink ID="HyperLink056" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink>
                        <asp:TextBox ID="TXTDTL056" runat="server" Width="70px" Visible="false" /></td></tr>

                   <%-- <td>
                                <div class="imgUpload-l80"></div>
                                <br>
                                <input type="file" value="選擇檔案">
                                <input type="button" value="上傳">
                            </td>
                        </tr>--%>
                        <tr>
                            <td colspan="2">
                               六、說明：
                                <table class="txtbox">
                                  <tr>
                                      <th>（一）</th>
                                      <td>本項檢查係屬行政監督檢查，檢查困難、隱蔽或不影響水保設施正常功能者（如圖面未標示之尺寸、水溝蓋板之型式、滯洪沉沙池之告示牌…等）得免查驗，應由水土保持義務人及承辦監造技師負責。</td>
                                  </tr>
                                    <tr>
                                      <th>（二）</th>
                                      <td>承辦監造技師未能到場時，應以書面方式委任符合水土保持法規定之技師代理之。</td>
                                  </tr>
                                </table>

                               <br /><br />
                                 七、填表注意事項：<br />
                                <table class="txtbox">
                                  
                                  <tr>
                                      <th>（一）</th>
                                      <td>如有施工缺失應改未改或未依核定計畫施作情形，應說明與計畫不符事項。</td>
                                  </tr>
                                  <tr style="display:none;">
                                      <th>（二）</th>
                                      <td>前次監督檢查缺失及應注意事項之複查，請註明辦理情形及是否同意結案（或持續列管）。</td>
                                  </tr>
                                  <tr>
                                      <th>（二）</th>
                                      <td>滯洪、沉砂池檢查項目及標準如下：<br />
                                          <b style="font-size:20pt; vertical-align:middle;">◆</b><span>滯洪、沉砂量體：增減不得逾20%，且不得小於所須最小滯洪、沉砂量。</span><br />
                                          <b style="font-size:20pt; vertical-align:middle;">◆</b><span>放流口及溢洪口通水斷面積：增加不超過20%或減少不超過10%。</span>
                                      </td>
                                  </tr>
                                  <tr>
                                      <th>（三）</th>
                                      <td>植生工程檢查項目及標準如下：<br />
                                          <b style="font-size:20pt; vertical-align:middle;">◆</b><span>植生面積：增減不得逾20%。</span><br />
                                          <b style="font-size:20pt; vertical-align:middle;">◆</b><span>覆蓋率：</span><br />
                                          <span style="padding-left:20px; line-height:2;">a.以種子撒播及草皮鋪植等方式直接栽植者，以植被生長後之覆蓋率審認。</span><br />
                                          <span style="padding-left:20px; line-height:2;">b.以噴植、植生帶、土袋植生及草袋等配合資材方式栽植者，以資材覆蓋率審認。</span>
                                      </td>
                                  </tr>
                                    <tr>
                                        <th>（四）</th>
                                        <td>相片說明應與紀錄文字勾稽。</td>
                                    </tr>
                                </table>



                            </td>
                        </tr>
                    </table>
                    <br>
                    <br>
                </table>

                <table class="checkRecord-fileUpload">
                <tr><td colspan="2">
                    <asp:Label ID="LBSWC005a" runat="server" Visible="false" />檢查現場照片</td></tr>
                <tr><td>現場相片一</td>
                    <td>現場相片二</td></tr>
                <tr><td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL057" runat="server" Width="70px" Visible="False" />
                        </div><br/>
                        <asp:Image ID="TXTDTL057_img" runat="server" CssClass="imgUpload" Visible="false" />
                        <asp:HyperLink ID="HyperLink057" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink>
                        <br/>
                        <asp:Label ID="TXTDTL058" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td>
                    <td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL059" runat="server" Width="70px" Visible="False" />
                        </div><br/>
                        <asp:Image ID="TXTDTL059_img" runat="server" CssClass="imgUpload" Visible="false" />
                        <asp:HyperLink ID="HyperLink059" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink>
                        <br/>
                        <asp:Label ID="TXTDTL060" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td></tr>
                        <tr>
                            <td>現場相片三</td>
                            <td>現場相片四</td>
                        </tr>
                        <tr>
                            <td>
                        <div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL061" runat="server" Width="70px" Visible="False" />
                        </div><br/>
                        <asp:Image ID="TXTDTL061_img" runat="server" CssClass="imgUpload" Visible="false" />
                        <asp:HyperLink ID="HyperLink061" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink>
                        <br/>
                        <asp:Label ID="TXTDTL062" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td>
                    <td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL063" runat="server" Width="70px" Visible="False" />
                        </div><br/>
                        <asp:Image ID="TXTDTL063_img" runat="server" CssClass="imgUpload" Visible="false" />
                        <asp:HyperLink ID="HyperLink063" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink>
                        <br/>
                        <asp:Label ID="TXTDTL064" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td></tr>
                <tr><td>現場相片五</td>
                    <td>現場相片六</td></tr>
                <tr><td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL065" runat="server" Width="70px" Visible="False" />
                        </div><br/>
                        <asp:Image ID="TXTDTL065_img" runat="server" CssClass="imgUpload" Visible="false" />
                        <asp:HyperLink ID="HyperLink065" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink>
                        <br/>
                        <asp:Label ID="TXTDTL066" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td>
                    <td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL067" runat="server" Width="70px" Visible="False" />
                        </div><br/>
                        <asp:Image ID="TXTDTL067_img" runat="server" CssClass="imgUpload" Visible="false" />
                        <asp:HyperLink ID="HyperLink067" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink>
                        <br/>
                        <asp:Label ID="TXTDTL068" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td></tr>
                <tr><td colspan="4">附件</td></tr>
                <tr><td colspan="4">附件檔案下載：
                        <asp:TextBox ID="TXTDTL070" runat="server" Width="70px" Visible="False" />
                        <asp:HyperLink ID="Link070" runat="server" Target="_blank" /></td></tr>
                    </table>
                </div>
        </div>  


            <div class="OFseg">
                <div style="float:left"> <img src="../images/btn/btn005-17.png" alt=""></div>
                <div class="lab">
                   <div class="labcolor1"><div class="icon1"></div>原核定</div>
                   <div class="labcolor2"><div class="icon2"></div>現場量測</div>
                </div>
                
                <asp:Label ID="GVMSG" runat="server" Text="查無資料" Visible="true" class="nodata"/>
               <div style="clear:both;"></div>
                   <div class="detailsGrid SWCfOh2">
                      <h2 class="SWCfO openh2">水保設施項目<img src="../images/btn/btn-close.png" alt=""/></h2>

                                <asp:GridView ID="SDIList" runat="server" CssClass="OFcheck AutoNewLine" AutoGenerateColumns="False" Height="50" EmptyDataText="查無資料">
								
                                    
                                    <Columns>
                                        <asp:BoundField DataField="SDIFD017" HeaderText="技師報備<br>施工完成" HeaderStyle-Width="300px" HtmlEncode="false" />
                                        <asp:BoundField DataField="SDIFD003" HeaderText="水土保持設施類別" HeaderStyle-Width="260px" />
                                        <asp:BoundField DataField="SDIFD004" HeaderText="設施名稱<br>（位置或編號）" HeaderStyle-Width="380px" HtmlEncode="false" />
                                        <asp:BoundField DataField="SDIFD005" HeaderText="設施型式" HeaderStyle-Width="280px" />
                                        <asp:BoundField DataField="SDIFD019" HeaderText="是否漸變" HeaderStyle-Width="100px" />
                                        <asp:BoundField DataField="SDICHK001D" HeaderText="數量" HeaderStyle-Width="200px" HtmlEncode="false" />
                                        <asp:BoundField DataField="SDICHK002" HeaderText="數量差異百分比" HeaderStyle-Width="300px" HtmlEncode="false" />
                                        <asp:BoundField DataField="SDIFD008" HeaderText="檢核項目" HeaderStyle-Width="300px" />
                                        <asp:BoundField DataField="SDICHK004D" HeaderText="尺寸" HeaderStyle-Width="300px" HtmlEncode="false" />
                                        <asp:BoundField DataField="SDICHK007" HeaderText="尺寸差異百分比" HeaderStyle-Width="300px" HtmlEncode="false" />
                                        <asp:BoundField DataField="SDICHK008" HeaderText="檢查日期" HeaderStyle-Width="200px" />
                                        <asp:BoundField DataField="SDICHK010" HeaderText="檢查結果" HeaderStyle-Width="200px" />

                                        <asp:TemplateField Visible="true" >
                                            <ItemTemplate>
                                                <asp:Button runat="server" CommandArgument="<%#Container.DataItemIndex %>" CommandName="Modify" Text="修改" Visible="false" />
                                                <asp:HiddenField ID="HDSDI001" runat="server" Value='<%# Eval("SDIFD001") %>' />
                                                <asp:HiddenField ID="HDSDINI" runat="server" Value='<%# Container.DataItemIndex %>' />
                                                <asp:HiddenField ID="HDSDI011" runat="server" Value='<%# Eval("SDIFD011") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
</div>




                <div class="form-btn">
                    <asp:Button ID="GoHomePage" runat="server" Text="返回案件詳情" OnClick="GoHomePage_Click" />
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
                    <span class="span2">建議使用IE11(含)以上，Chrome或Firefox版本瀏覽器　<b>資料更新：</b><asp:Label ID="ToDay" runat="server" Text=""/>　<b>來訪人數：</b><asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                    <span class="span2"><b>客服電話：</b>02-27929328 陳小姐　<b>信箱：</b>tcge7@geovector.com.tw　本系統由多維空間資訊有限公司開發維護 TEL：(02)27929328</span><br/>
			        <span class="span2">※為維護系統服務品質，本平台訂於每周三凌晨AM 4:00-6:30 進行系統維護更新，更新期間偶有瞬斷情形，敬請使用者避開該時段使用。謝謝！</span></p>
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
