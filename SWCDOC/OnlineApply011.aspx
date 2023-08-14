<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineApply011.aspx.cs" Inherits="SWCDOC_OnlineApply011" %>
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
                labeltemp.innerText = "（" + ttval + "/" + txtcount + "）";
            }
            else {
                labeltemp.innerText = "（" + ttval + "/" + txtcount + "）";
            }
        }
		
		function chkInput() {

            if (!document.getElementById("CHKONA11006_Y").checked || document.getElementById("DDLONA11007").value=="請選擇") {
                alert('您好，檢核項目尚未填寫完成或不符申辦相關規定，請重新確認後再送出。');
                return false;
            }
            if (!document.getElementById("CHKONA11008_Y").checked) {
                alert('您好，檢核項目尚未填寫完成或不符申辦相關規定，請重新確認後再送出。');
                return false;
            }
            if (!document.getElementById("CHKONA11010_Y").checked) {
                alert('您好，檢核項目尚未填寫完成或不符申辦相關規定，請重新確認後再送出。');
                return false;
            }
            if (!document.getElementById("CHKONA11012_Y").checked) {
                alert('您好，檢核項目尚未填寫完成或不符申辦相關規定，請重新確認後再送出。');
                return false;
            }
            if (!document.getElementById("CHKONA11014_Y").checked) {
                alert('您好，檢核項目尚未填寫完成或不符申辦相關規定，請重新確認後再送出。');
                return false;
            }
            if (!document.getElementById("CHKONA11016").checked) {
                alert('您好，請確認是否符合規定且水土保持設施安全無虞，請重新確認後再送出。');
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
            <div class="header header-s clearfix"><a href="HaloPage001.aspx" class="logo-s"></a>
                <div class="header-menu-s">
                    <ul>
                        <li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
                        <li>|</li>
                        <li><a href="http://www.swc.taipei/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="http://172.28.100.55/tslmwork" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
                        <asp:Panel ID="TitleLink00" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWCBase001.aspx" title="帳號管理">帳號管理</a></li></asp:Panel>
                        <asp:Panel ID="LogOutLink" runat="server" Visible="true"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li></asp:Panel>
                    </ul>
                </div>
            </div>

            <div class="header-s-green">
                <div class="header-s-green-nameWrap">
                    <span><asp:Literal ID="TextUserName" runat="server">先生，您好</asp:Literal></span>
                </div>
            </div>
        </div>
        
        <div class="content-s">
            <div class="review form">
                <h1>水土保持計畫失效重新核定申請表</h1>
                
                <br/><br/>
                
                <table class="resend">
					<tr style="display: none;"><th style="width:26%;">SWC000</th>
						<td colspan="2"><asp:Label ID="LBSWC000" runat="server"/></td></tr>
					<tr style="display: none;"><th style="width:26%;">SWC002</th>
						<td colspan="2"><asp:Label ID="LBSWC002" runat="server"/></td></tr>
					<tr><th style="width:26%;">OA編號</th>
						<td colspan="2"><asp:Label ID="LBONA11001" runat="server"/></td></tr>
					<tr><th style="width:26%;">申報日期</th>
						<td colspan="2"><asp:Label ID="LBONA11002" runat="server"/></td></tr>
					<tr style="display: none;"><th style="width:26%;">承辦人員</th>
						<td colspan="2"><asp:Label ID="LBONA11003" runat="server"/></td></tr>
					<tr><th style="width:26%;">計畫名稱</th>
						<td colspan="2"><asp:Label ID="LBONA11004" runat="server"/></td></tr>
					<tr style="display: none;"><th style="width:26%;">原核定日期</th>
						<td colspan="2"><asp:Label ID="LBONA11005" runat="server"/></td></tr>
					<tr style="background-color:#D8FCFF;"><th style="text-align:center">檢核項目</th>
						<td style="width:10%;text-align:center;" class="center">是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;否</td>
						<td style="text-align:center;" class="center">備&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;註</td></tr>
					<tr><th>壹、經目的事業主管機關核轉：<span style="color: red;font-family:cursive;">＊</span><br />
						<span class="innertxt">一、已取得完工證明書</span>
						<span class="innertxt">二、已開工且距原核定日期未逾6年</span>
						<span class="innertxt">三、距原核定日期未逾4年</span></th>
						<td style="text-align:center;" class="center"><asp:RadioButton ID="CHKONA11006_Y" runat="server" GroupName="CHKONA11006" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="CHKONA11006_N" runat="server" GroupName="CHKONA11006" /></td>
						<td><span style="color: red;font-family:cursive;">＊</span>符合第<asp:DropDownList ID="DDLONA11007" runat="server" style="font-size:16px;height: 22px">
									<asp:ListItem Value="請選擇">請選擇</asp:ListItem>
									<asp:ListItem Value="一">一</asp:ListItem>
									<asp:ListItem Value="二">二</asp:ListItem>
									<asp:ListItem Value="三">三</asp:ListItem>
								  </asp:DropDownList>
						點</td></tr>
					
					<tr><th>貳、原案未曾辦理失效重新核定<span style="color: red;font-family:cursive;">＊</span></th>
						<td style="text-align:center;" class="center"><asp:RadioButton ID="CHKONA11008_Y" runat="server" GroupName="CHKONA11008" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="CHKONA11008_N" runat="server" GroupName="CHKONA11008" /></td>
						<td><asp:TextBox ID="TXTONA11009" TextMode="MultiLine" runat="server" MaxLength="250" onkeyup="textcount(this,'TXTONA11009_count','250');" />
							<asp:Label ID="TXTONA11009_count" runat="server" Text="（0/250）" ForeColor="Red" /></td></tr>
							
					<tr><th>參、計畫面積無變更<span style="color: red;font-family:cursive;">＊</span></th>
						<td style="text-align:center;" class="center"><asp:RadioButton ID="CHKONA11010_Y" runat="server" GroupName="CHKONA11010" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="CHKONA11010_N" runat="server" GroupName="CHKONA11010" /></td>
						<td><asp:TextBox ID="TXTONA11011" TextMode="MultiLine" runat="server" MaxLength="250" onkeyup="textcount(this,'TXTONA11011_count','250');" />
							<asp:Label ID="TXTONA11011_count" runat="server" Text="（0/250）" ForeColor="Red" /></td></tr>
						
					<tr><th>肆、開挖整地位置無變更<span style="color: red;font-family:cursive;">＊</span></th>
						<td style="text-align:center;" class="center"><asp:RadioButton ID="CHKONA11012_Y" runat="server" GroupName="CHKONA11012" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="CHKONA11012_N" runat="server" GroupName="CHKONA11012" /></td>
						<td><asp:TextBox ID="TXTONA11013" TextMode="MultiLine" runat="server" MaxLength="250" onkeyup="textcount(this,'TXTONA11013_count','250');" />
							<asp:Label ID="TXTONA11013_count" runat="server" Text="（0/250）" ForeColor="Red" /></td></tr>
						
					<tr><th>伍、水土保持設施數量、尺寸無變更<br />（己完工者一併確認現場符合計畫）<span style="color: red;font-family:cursive;">＊</span></th>
						<td style="text-align:center;" class="center"><asp:RadioButton ID="CHKONA11014_Y" runat="server" GroupName="CHKONA11014" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="CHKONA11014_N" runat="server" GroupName="CHKONA11014"/></td>
						<td><asp:TextBox ID="TXTONA11015" TextMode="MultiLine" runat="server" MaxLength="250" onkeyup="textcount(this,'TXTONA11015_count','250');" />
							<asp:Label ID="TXTONA11015_count" runat="server" Text="（0/250）" ForeColor="Red" /></td></tr>
						
					<tr><th colspan="3">
						<span style="color: red;font-family:cursive;">＊</span><asp:CheckBox ID="CHKONA11016" runat="server" />
						<b style="font-weight:bold; font-size:14pt;">已確認符合規定且水土保持設施安全無虞</b></th></tr>
                    
        
				</table>
				<br><br><br>
				<table>
				  <tr><td style="width:20%;" class="bgcolor">審查結果</td>
                        <td class="bgcolor2" style="line-height:40px;"><asp:Label ID="YorN" runat="server"  Text="" /></td>
					</tr>
				</table>
				
				<div class="form-btn">
					<asp:Button ID="DataLock" runat="server" Text="確認送出" OnClientClick="return chkInput()" OnClick="DataLock_Click" />&nbsp&nbsp
					<asp:Button ID="SaveCase" runat="server" Text="暫時儲存" OnClientClick="return chkInput()" OnClick="SaveCase_Click" Visible=false />&nbsp&nbsp
					<asp:Button ID="GoHomePage" runat="server" Text="返回瀏覽案件" OnClick="GoHomePage_Click" />
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
        
	<script type="text/javascript">
	
	</script>
	<script src="../js/jquery-3.1.1.min.js"></script>
	<script src="../js/inner.js"></script>
	<script src="../js/allhref.js"></script>

    </div>
    </form>
</body>
</html>
