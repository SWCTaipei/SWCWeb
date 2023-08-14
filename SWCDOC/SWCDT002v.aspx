<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCDT002v.aspx.cs" Inherits="SWCDOC_SWCDT002" %>
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

        <div class="content-s">
            <div class="swcchg form">
                <h1>水土保持施工抽查紀錄</h1><br/>

                <table class="swcchg-out">
                    <tr><td>施工監督表編號</td>
                        <td><asp:Label ID="LBDTL001" runat="server"/>
                            <asp:Label ID="LBSWC000" runat="server" Visible="false" /></td></tr>
                    <tr><td>檢查日期<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:Label ID="TXTDTL002" runat="server" width="120px"></asp:Label></td></tr>
                    <tr><td>檢查類型<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:Label ID="TXTDTL003" runat="server" /></td></tr>
                    <tr><td>檢查單位<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:Label ID="TXTDTL004" runat="server" /></td></tr>
                </table>

                <br><br>

                <table>
                <tr><th>水土保持書件名稱</th>
                    <th><asp:Label ID="LBSWC005" runat="server"/></th></tr>
                <tr><td>一、檢查項目</td>
                    <td style="text-align:center;">現場情形</td></tr>
                <tr><td>（一）水土保持施工告示牌</td>
                    <td><asp:Label ID="CHKDTL007" runat="server" /><br/>
                        <asp:Label ID="CHKDTL008" runat="server" /><br/>
                        <asp:Label ID="CHKDTL009" runat="server" />
                        <asp:Label ID="TXTDTL010" runat="server"></asp:Label></td></tr>
                <tr><td>（二）臨時性防災措施</td>
                    <td></td></tr>
                <tr><td class="td-padding">1.泥水外流未疏導</td>
                    <td><asp:Label ID="DropList011" runat="server"></asp:Label></td></tr>
                <tr><td class="td-padding">2.開挖邊坡裸露未覆蓋</td>
                    <td><asp:Label ID="DropList012" runat="server"></asp:Label></td></tr>
                <tr><td class="td-padding">3.土方暫置未覆蓋</td>
                    <td><asp:Label ID="DropList013" runat="server"></asp:Label></td></tr>
                <tr><td class="td-padding">4.其他</td>
                    <td><asp:Label ID="TXTDTL014" runat="server" /></td></tr>
                <tr><td>（三）災害搶救小組測試</td>
                    <td><asp:Label ID="CHKBOX015" runat="server" /><br/>
                        <asp:Label ID="CHKBOX016" runat="server" /><br/>
                        <asp:Label ID="CHKBOX017" runat="server" /><br/>
                        <asp:Label ID="CHKBOX018" runat="server" /><br/>
                        <asp:Label ID="TXTDTL019" runat="server"></asp:Label></td></tr>
                <tr><td colspan="2">（四）其他注意事項：<br/><br/>
                        <asp:Label ID="TXTDTL020" runat="server"/></td></tr>
                </table>

                <table class="swcchg-imgUpload">
                <tr><td>二、相關單位及人員簽名<br/><br/>
                        <asp:Label ID="TXTDTL021" runat="server" /></td>
                    <td><asp:Image ID="TXTDTL022_img" runat="server" CssClass="imgUpload-l80" /><br/><br/>
                        <asp:TextBox ID="TXTDTL022" runat="server" Width="70px" Visible="False" /></td></tr>
                <tr><td colspan="2">三、上傳檔案<br/><br/>
                        <asp:HyperLink ID="Link023" runat="server" Target="_blank" />
                        <asp:TextBox ID="TXTDTL023" runat="server" Width="70px" Visible="False" /></td></tr>
                </table>

                <div class="form-btn">
                    <asp:Button ID="GoHomePage" runat="server" Text="返回案件詳情" OnClick="GoHomePage_Click" />
                </div>
        </div>
      </div>
        


<%--            <div class="footer-s">
                <div class="footer-s-green"></div>
                <div class="footer-b-brown">
                    <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                       <span class="span2">110臺北市信義區松德路300號3樓 　(02)27593001   臺北市民當家熱線1999</span><br/>
                       <span class="span2">資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span></p>
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
        
                <script>
                    var jqC = $("#TXTDTL022_img").attr("src");
                    if (jqC.trim() == "") { $("#TXTDTL022_img").hide();}
                </script>
            </div>
        </form>
    </body>
</html>
