<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCTimes.aspx.cs" Inherits="SWCDOC_SWCTimes" %>
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
        function addtext(unitelement, peopleelement, listelement) {
            listelement.value = listelement.value + "\r\n" + unitelement.value + " " + peopleelement.value;
            return false;
        }
        function chkInput(jChkType) {
            jCHKValue01 = document.getElementById("TXTDTL002").value;
            jCHKValue02 = document.getElementById("TXTDTL003").value;

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
            if (jCHKValue02.trim() == '') {
                alert('請輸入檢查情形');
                document.getElementById("TXTDTL003").focus();
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
            if (jCHKValue16.trim() != '') {
                if (isNaN(jCHKValue16)) {
                    alert('基地現況 其他 請輸入正確數字');
                    document.getElementById("TXTDTL016").focus();
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
                if (isNaN(jCHKValue16)) {
                    alert('水保設施概要 滯洪量 請輸入正確數字');
                    document.getElementById("TXTDTL023").focus();
                    return false;
                }
            }
            if (jCHKValue24.trim() != '') {
                if (isNaN(jCHKValue16)) {
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

    <div class="wrap-s">
        <div class="header-wrap-s">
            <div class="header header-s clearfix"><a href="HaloPage001.aspx" class="logo-s"></a>
                <div class="header-menu-s">
                    <ul>
                        <li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
                        <li>|</li>
                        <li><a href="http://www.swc.taipei/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="https://tslm.swc.taipei/tslmwork/" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
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
                <h1>管制時程表<br/><br/></h1>

                <div class="detailsMenu-btn">
                    <asp:ImageButton ID="OutPdf" runat="server" title="輸出PDF" ImageUrl="../images/btn/icon_exportpdf.png" OnClick="OutPdf_Click" Visible="true" />
                </div>
                
                <table class="facilityMaintain-out">
                    <tr><td style="width:200px; text-align: center;">案名</td>
                        <td><asp:Label ID="LBSWC005" runat="server"/></td></tr>
                    <tr><td style="width:200px; text-align: center;">水土保持義務人</td>
                        <td><asp:Label ID="LBSWC013" runat="server"/></td></tr>
                    <tr><td style="width:200px; text-align: center;">承辦技師</td>
                        <td><asp:Label ID="LBSWC021" runat="server"/></td></tr>
                    <tr><td style="width:200px; text-align: center;">審查委員</td>
                        <td><asp:Label ID="LBSWC087" runat="server"/></td></tr>
                </table>

                <br/><br/>

                <asp:Table ID="dt" runat="server" BorderColor="#FFFFFF" BorderStyle="Solid" BorderWidth="1px" CellPadding="0" CellSpacing="0" GridLines="Both">
				</asp:Table>

                <div class="form-btn">
                    <asp:Button ID="GoHomePage" runat="server" Text="返回案件" OnClick="GoHomePage_Click" />
                </div>
            </div>
        </div>


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
