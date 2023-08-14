<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCGOV003.aspx.cs" Inherits="SWCDOC_SWCBase001" %>
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
    <link rel="stylesheet" type="text/css" href="../css/iris.css?202208250511"/>
     <link rel="stylesheet" type="text/css" href="css/min.css"/>
    
    <script type="text/javascript">
        function chkInput(jChkType) {
            jCHKValue01 = document.getElementById("TXTDENAME").value;
            jCHKValue02 = document.getElementById("TXTDEDATE").value;
            jCHKValue03 = document.getElementById("TXTDENTTEXT").value;

            if (jCHKValue01.trim() == '') {
                alert('請輸入事件名稱');
                return false;
            }
            if (jCHKValue02.trim() == '') {
                alert('請輸入發送日期');
                return false;
            } else {
                return dateValidationCheck(jCHKValue02);
            }

            if (jCHKValue03.trim() == '') {
                alert('請輸入通知內容');
                return false;
            }

        }
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
    </script>
    <script src="../js/BaseNorl.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"/>

    <div class="wrap-s">
        <div class="header-wrap-s">
            <div class="header header-s clearfix"><a href="../SWCDOC/SWC001.aspx" class="logo-s"></a>
                <div class="header-menu-s">
                    <ul>
                        <li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
                        <li>|</li>
                        <li><a href="https://swc.taipei/swcinfo" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="https://tslm.swc.taipei/tslmwork" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
                        <asp:Panel ID="TitleLink00" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="../SWCDOC/SWCBase001.aspx" title="帳號管理">帳號管理</a></li></asp:Panel>
                        <asp:Panel ID="GOVMG" runat="server" Visible="false"><li class="flip">|&nbsp&nbsp&nbsp&nbsp<a href="#" title="系統管理">系統管理+</a>
					<ul class="openlist" style="display: none;">
					  <li><a href="../SWCRD/FPage001ALL.aspx" target="_blank">審查/檢查行事曆</a></li>
					  <li><a href="../SWCTCGOV/SWCGOV001.aspx">防災事件通知</a></li>
					  <li><a href="../SWCTCGOV/SWCGOV011.aspx">公佈欄</a></li>
					  <li><a href="../SWCDOC/UserBoardList.aspx">留言版</a></li>
					  <li><a href="http://tgeo.swc.taipei/">T-GEO空間地理資訊平台</a></li>
					</ul></li></asp:Panel>
                    <li>|</li>
                        <li><a href="../SWCDOC/SWC000.aspx?ACT=LogOut" title="登出">登出</a></li>
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
            <div class="detailsMenu"><asp:Image ID="Image1" runat="server" ImageUrl="../images/title/btn-87.png" /></div>
                <div class="applyGrid">
                    <h2 class="detailsBar_title_basic">基本資料</h2>
          
                    <table class="ph-table">
                    <tr><th>流水號</th>
                        <td><asp:Label ID="TXTDENO" runat="server" /></td></tr>
                    <tr><th>通知事件<span style="color: red;font-family:cursive;">＊</span></th>
                        <td><asp:Label ID="DDLDETYPE" runat="server" /></td></tr>
                    <tr><th>行政區<span style="color: red;font-family:cursive;">＊</span></th>
                        <td><asp:Label ID="LBDISTRICT" runat="server" /></td></tr>
                    <tr><th style="vertical-align:middle">水土保持申請案件<span style="color: red;font-family:cursive;">＊</span></th>
                        <td><span class="ittype">案件狀態</span><asp:Label ID="LBSWCSTATUS" runat="server" />
							<span class="ittype">通知對象</span><asp:Label ID="LBSENDMBR" runat="server" />
							<span class="ittype">通知管道</span><asp:Label ID="LBSENDFUN" runat="server" /></td></tr>
					<tr><th style="vertical-align:middle">水土保持違規案件<span style="color: red;font-family:cursive;">＊</span></th>
                        <td><span class="ittype2">案件狀態</span><asp:Label ID="LBSWCSTATUS_ILG" runat="server" />
							<span class="ittype2">通知對象</span><asp:Label ID="LBSENDMBR_ILG" runat="server" />
							<span class="ittype2">通知管道</span><asp:Label ID="LBSENDFUN_ILG" runat="server" /></td></tr>
					<tr class="none"><th>案件狀態<span style="color: red;font-family:cursive;">＊</span></th>
                        <td><asp:Label ID="CHKSWCSTATUS" runat="server" RepeatColumns="3" /></td></tr>
                    <tr><th>通知統計</th>
                        <td><div style="display:none;">
                            發送筆數：<asp:Label ID="St05" runat="server"/></div>
                            <span class="PH_list">事件筆數：<asp:Label ID="St06" runat="server" /></span>
                            <span class="PH_list">已送出：<asp:Label ID="St03" runat="server" /></span>
                            <span class="PH_list">未送出：<asp:Label ID="St04" runat="server" /></span>
                            <span class="PH_list">已整備完成：<asp:Label ID="St01" runat="server" /></span>
                            <span class="PH_list">整備中：<asp:Label ID="St02" runat="server" /></span></td></tr>
                    <tr class="none"><th>通知對象<span style="color: red;font-family:cursive;">＊</span></th>
                        <td><asp:Label ID="CHKSENDMBR" runat="server" RepeatColumns="3" /></td></tr>
                    <tr class="none"><th>通知管道<span style="color: red;font-family:cursive;">＊</span></th>
                        <td><asp:Label ID="CHKSENDFUN" runat="server" RepeatColumns="3" /></td></tr>
                    <tr><th>事件名稱<span style="color: red;font-family:cursive;">＊</span></th>
                        <td><asp:Label ID="TXTDENAME" runat="server" MaxLength="50" style="width:200px;"/></td></tr>
                    <tr><th>通知內容<span style="color: red;font-family:cursive;">＊</span></th>
                        <td><asp:Label ID="TXTDENTTEXT" runat="server" TextMode="MultiLine" Height="100" Width="100%" onkeyup="textcount(this,'TXTDENTTEXT_count','300');" /></td></tr>
                        
                    </table>
                </div><br/>
            <br />
           
                <div  class="skybluebtn">
                    <asp:ImageButton ID="ShowRed" runat="server" OnClick="ShowRed_Click" title="未送出" ImageUrl="../images/btn/notsent-01.png" />
                    
                    <div style="margin-bottom:1em;"></div>

                        <div class="TFtitle">發送詳情</div>
                        <asp:GridView ID="SWCDTL04" runat="server" CssClass="TFbase AutoNewLine" AutoGenerateColumns="False" PagerStyle-CssClass="pgr"
                            PageSize="20" AllowPaging="true"
                            OnPageIndexChanging="SWCDTL04_PageIndexChanging" OnPageIndexChanged="SWCDTL04_PageIndexChanged" >
                            <Columns>
                                <asp:BoundField DataField="DTLD001" HeaderText="水保案件編號" />
                                <asp:BoundField DataField="DTLD002" HeaderText="案件名稱" HeaderStyle-Width="35%"/>
                                <asp:BoundField DataField="DTLD003" HeaderText="義務人" />
                                <asp:BoundField DataField="DTLD004" HeaderText="監造技師" />
                                <asp:ImageField DataImageUrlField="DTLD005" HeaderText="未送出" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="ButtonDTL04" runat="server" CommandArgument='<%# Eval("DTLD001") %>' Text="連結表單" OnClick="ButtonDTL04_Click" />
                                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("DTLD006") %>' />
                                        <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Eval("DTLD007") %>' />
                                        <asp:HiddenField ID="HiddenField3" runat="server" Value='<%# Eval("DTLD005") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>


            <div class="btncenter">
                <asp:Button ID="GoHomePage" runat="server" Text="返回瀏覽案件" OnClick="GoHomePage_Click" />
            </div>
        </div>
        
         

            <%--<div class="footer-s">
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

        <asp:Literal ID="error_msg" runat="server"></asp:Literal>

        <script src="../js/jquery-3.1.1.min.js"></script>
        <script src="../js/inner.js"></script>
        <script src="../js/allhref.js"></script>
    </div>
    </form>
</body>
</html>
