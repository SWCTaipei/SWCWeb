<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineApply001v.aspx.cs" Inherits="SWCDOC_OnlineApply001" %>
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
                        <li><a href="https://swc.taipei/swcinfo/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="http://tslm.swc.taipei/tslmwork/" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
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
                <h1>臺北市山坡地水土保持設施安全自主檢查表</h1>
                
                <div class="detailsMenu-btn">
                    <asp:ImageButton ID="OutPdf" runat="server" title="輸出PDF" ImageUrl="../images/btn/icon_exportpdf.png" OnClick="OutPdf_Click" Visible="true" />
                </div>
                
                <table class="facilityMaintain-out">
                    <tr><td>安全自主檢查表編號</td>
                        <td><asp:Label ID="LBONA001" runat="server" />
                            <asp:Label ID="LBSWC000" runat="server" Visible="false"/>
                        </td></tr>
                    <tr><td>項次編號</td>
                        <td><asp:Label ID="TXTONA027" runat="server" MaxLength="10" width="100px"/>
                        </td></tr>
                    <tr><td>水保局編號</td>
                        <td><asp:Label ID="TXTSWC002" runat="server" MaxLength="12" width="200px"/>
                            <%--<asp:TextBox ID="TXTSWC002" runat="server" MaxLength="12" placeholder="(如不知可由市府填寫)" width="200px"/>--%>
                            <asp:Label ID="LBSWC002" runat="server" Visible="false" />
                        </td></tr>
                    <tr><td>檢查日期</td>
                        <td><asp:Label ID="TXTONA002" runat="server" width="120px"></asp:Label></td></tr>
                    <tr><td>社區(設施)地址</td>
                        <td><asp:Label ID="TXTONA003" runat="server" width="100%" onkeyup="textcount(this,'TXTONA003_count','100');" MaxLength="100" /></td></tr>
                    <tr><td>水土保持義務人</td>
                        <td><asp:Label ID="TXTONA004" runat="server" width="100%" onkeyup="textcount(this,'TXTONA004_count','100');" MaxLength="100" />
                            <asp:Label ID="TXTONA004_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /></td></tr>
                    <tr><td>聯絡人</td>
                        <td><asp:Label ID="TXTONA028" runat="server" width="100%" onkeyup="textcount(this,'TXTONA028_count','100');" MaxLength="100" />
                            <asp:Label ID="TXTONA028_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /></td></tr>
                    <tr><td>聯絡地址</td>
                        <td><asp:Label ID="TXTONA005" runat="server" width="100%" onkeyup="textcount(this,'TXTONA005_count','100');" MaxLength="100" />
                            <asp:Label ID="TXTONA005_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /></td></tr>
                    <tr><td>聯絡電話（市話）</td>
                        <td><asp:Label ID="TXTONA006" runat="server" width="100%" onkeyup="textcount(this,'TXTONA006_count','100');" MaxLength="100" />
                            <asp:Label ID="TXTONA006_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /></td></tr>
                    <tr><td>行動電話</td>
                        <td><asp:Label ID="TXTONA007" runat="server" width="100%" onkeyup="textcount(this,'TXTONA007_count','100');" MaxLength="100" />
                            <asp:Label ID="TXTONA007_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /></td></tr>
                </table>

                <br/><br/>
                
                <table class="OA01-T2">
                <tr><th colspan="4">檢查項目與名稱</th>
                    <th>檢查及改善結果</th></tr>
                <tr class="tr-title">
                    <td>一、滯洪沉砂設施：</td>
                    <td><asp:Label ID="LBONA008" runat="server" /></td>
                    <td></td>
                    <td></td><td></td>
                </tr>
                    <%--<tr><td></td><td></td><td></td><td style="width:200px;"></td><td></td></tr>--%>
                <tr><td class="td-padding" colspan="4" style="width:50%;">1.池內是否淤積？</td>
                    <td style="text-align:left;width:50%;">
                        <asp:Label ID="LBONA009" runat="server" /></td></tr>
                <tr><td class="td-padding" colspan="4" style="width:50%;">2.進水及排放處是否堵塞？</td>
                    <td style="text-align:left;width:50%;">
                        <asp:Label ID="LBONA010" runat="server" /></td></tr>
                <tr class="tr-title">
                    <td>二、排水設施</td>
                    <td><asp:Label ID="LBONA011" runat="server" /></td>
                    <td></td>
                    <td></td><td></td>
                </tr>
                <tr><td class="td-padding" colspan="4">1.排水溝是否損壞？</td>
                    <td style="text-align:left;">
                        <asp:Label ID="LBONA012" runat="server" /><asp:Label ID="LBONA012D" runat="server" /></td></tr>
                <tr><td class="td-padding" colspan="4">2.排水溝是否雜物淤積？</td>
                    <td style="text-align:left;">
                        <asp:Label ID="LBONA013" runat="server" /><asp:Label ID="LBONA013D" runat="server" /></td></tr>
                <tr class="tr-title">
                    <td>三、邊坡保護設施 </td>
                    <td><asp:Label ID="LBONA014" runat="server" /></td>
                    <td></td>
                    <td></td><td></td>
                </tr>
                <tr><td class="td-padding" colspan="4">1.是否外凸變形？</td>
                    <td style="text-align:left;">
                        <asp:Label ID="LBONA015" runat="server" /><asp:Label ID="LBONA015D" runat="server" /></td></tr>
                <tr><td class="td-padding" colspan="4">2.是否龜裂？</td>
                    <td style="text-align:left;">
                        <asp:Label ID="LBONA016" runat="server" /><asp:Label ID="LBONA016D" runat="server" /></td></tr>
                <tr><td class="td-padding" colspan="4">3.排水孔是否堵塞？</td>
                    <td style="text-align:left;">
                        <asp:Label ID="LBONA017" runat="server" /><asp:Label ID="LBONA017D" runat="server" /></td></tr>
                <tr class="tr-title">
                    <td>四、抽水設施</td>
                    <td><asp:Label ID="LBONA018" runat="server" /></td>
                    <td></td>
                    <td></td><td></td>
                </tr>
                <tr><td class="td-padding" colspan="4">1.是否功能正常？</td>
                    <td style="text-align:left;">
                        <asp:Label ID="LBONA019" runat="server" /><asp:Label ID="LBONA019D" runat="server" /></td></tr>
                <tr><td class="td-padding" colspan="4">2.是否有定期維修保養檢查及記錄？</td>
                    <td style="text-align:left;">
                        <asp:Label ID="LBONA020" runat="server" /><asp:Label ID="LBONA020D" runat="server" /></td></tr>
                <tr class="tr-title">
                    <td>五、其他</td>
                    <td></td>
                    <td></td>
                    <td></td><td></td>
                </tr>
                <tr><td class="td-padding" colspan="4">1.是否需要專業技師現場指導</td>
                    <td style="text-align:left;">
                        <asp:Label ID="LBONA021" runat="server" />
                        <asp:Label ID="TXTONA022" runat="server" MaxLength="50" /></td></tr>
                <tr><td class="td-padding" colspan="4">2.設施淤積、堵塞與龜裂…等異常徵兆是否規劃進行改善?</td>
                    <td style="text-align:left;">
                        <asp:Label ID="LBONA023" runat="server" />
                        <asp:Label ID="TXTONA024" runat="server"><asp:Label ID="TXTONA024D" runat="server" /></asp:Label></td></tr>
                <tr>
                    <td>六、建議事項</td>
                    <td colspan="4">
                        <asp:Label ID="TXTONA026" runat="server" TextMode="MultiLine"  onkeyup="textcount(this,'TXTONA026_count','250');" MaxLength="250" style="width:100%; height:80px; vertical-align:middle;" />
                        <asp:Label ID="TXTONA026_count" runat="server" Text="(0/500)" ForeColor="Red" Visible="false" /></td>
                </tr>

                <tr><td>檢查人員</td>
                    <td colspan="4"><asp:Label ID="TXTONA025" runat="server" Width="100%" MaxLength="100" /></td>
                </tr>
                <tr><td style="border:none"></td>
                    <td style="border:none"></td></tr>

                <asp:Panel ID="ReviewResults" runat="server" Visible="false">

                <tr><td class="bgcolor" colspan="2">審查結果</td>
                    <td class="bgcolor2" style="line-height:40px;" colspan="3">
                        <asp:radiobutton ID="CHKRRa" runat="server" Text="准" value="1" GroupName="CHKRR" />、
                        <asp:radiobutton ID="CHKRRb" runat="server" Text="駁：" value="0" GroupName="CHKRR" />
                        <asp:TextBox ID="CHK" runat="server" Width="70px" Visible="False" />
                        <asp:Label ID="LBRR" runat="server" Visible="false" />
                        <asp:TextBox ID="ResultsExplain" runat="server" Width="580px" />
                        <asp:Label ID="LBResultsExplain" runat="server" Width="300px" /><br/>
                        <asp:Panel ID="ReViewUL" runat="server">
                        上傳公文：
                        <asp:FileUpload ID="TXTReviewDoc_fileupload" runat="server" CssClass="wordtt" />
                        <asp:Button ID="TXTReviewDoc_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTReviewDoc_fileuploadok_Click" />
                        <asp:TextBox ID="TXTReviewDoc" runat="server" CssClass="word" Width="20px" Visible="False"/>
                        <asp:Button ID="TXTReviewDoc_fileclean" runat="server" CssClass="wordttb" Text="X" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTReviewDoc_fileclean_Click" />
                        <br/><span style="color:red;">※ 上傳格式限定為pdf、odt或doc檔案，大小請於50mb以內</span><br/></asp:Panel>
                        <asp:HyperLink ID="LinkReviewDoc" runat="server" CssClass="word" Target="_blank"/><br/>
                        存檔人：<asp:Label ID="ReviewID" runat="server"/></td></tr>
                    
                </asp:Panel>
                </table>
                
            <br/>

      <div style="color:red";> 備註：<br>
        <br>
        1.	檢查時機<br>
        <br>
        <span style="padding-left:15px;">(1)	每年於五月一日前至少1次維修、檢查，並維持正常運作，若有損壞或阻塞，應立即修繕及清淤。<br></span>
        <br>
        <span style="padding-left:15px;">(2)	於中央氣象局發布北部區域列入海上颱風警報警戒範圍或豪雨警報以上等級後，應自行檢查清淤以維持功能。<br></span>
        <br>
        2.	前項之維修、檢查應製成紀錄並保存五年，供市府抽查。 </div>





                <div class="form-btn">
                    <asp:Button ID="DataLock" runat="server" Text="送出" OnClientClick="return chkInput('DataLock');" Visible="false" OnClick="DataLock_Click" />&nbsp&nbsp
                    <asp:Button ID="SaveCase" runat="server" Text="暫時儲存" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" Visible="false" />&nbsp&nbsp
                    <asp:Button ID="GoHomePage" runat="server" Text="返回案件列表" Visible="true" OnClick="GoHomePage_Click" />
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
