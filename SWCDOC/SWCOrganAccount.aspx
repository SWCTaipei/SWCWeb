<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCOrganAccount.aspx.cs" Inherits="SWCDOC_SWCOrganAccount" %>
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
	<link rel="stylesheet" type="text/css" href="../css/all.css?202110061124"/>
	<link rel="stylesheet" type="text/css" href="../css/iris.css"/>
	
	<script src="../js/BaseNorl.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"/>

			<div class="wrap-s">
				<div class="header-wrap-s">
					<div class="header header-s clearfix"><a href="SWC001.aspx" class="logo-s"></a>
						<div class="header-menu-s">
							<ul>
								<li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
								<li>|</li>
								<li><a href="https://swc.taipei/swcinfo/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
								<asp:Panel ID="GoTslm" runat="server" Visible="true"><li>|&nbsp&nbsp&nbsp&nbsp<a href="http://tslm.swc.taipei/tslmwork/" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
								<asp:Panel ID="TitleLink01" runat="server" Visible="false" CssClass="last-divLi"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li></asp:Panel>
							</ul>
						</div>
					</div>
		
					<div class="header-s-green">
						<div class="header-s-green-nameWrap">
							<span><asp:Literal ID="TextUserName" runat="server"></asp:Literal></span><input type="password" style="display:none" />
						</div>
					</div>
				</div>
				
				<div class="content-s">
					<div class="detailsMenu"><asp:Image ID="Image1" runat="server" ImageUrl="../images/title/organ.png" /></div>
						<div class="applyGrid">
							<h2 class="detailsBar_title_basic">基本資料</h2>
				
							<table class="MTB">
								<tr>
									<th style="width:17.5%;">
										編號
									</th>
									<td>
										<asp:Label ID="LBACCTNO" runat="server" />
									</td>
								</tr>
								<tr>
									<th>
										單位
									</th>
									<td>
										<asp:Label ID="LBUNITNAME" runat="server" />
									</td>
								</tr>
								<tr>
									<th>
										姓名
									</th>
									<td>
										<asp:Label ID="LBNAME" runat="server" />
									</td>
								</tr>
								<tr>
									<th>
										行動電話
									</th>
									<td>
										<asp:Label ID="LBCELLPHONE" runat="server" />
									</td>
								</tr>
								<tr>
									<th>
										聯絡電話
									</th>
									<td>
										<asp:Label ID="LBTELEPHONE" runat="server" />
									</td>
								</tr>
								<tr>
									<th>
										身分證字號
									</th>
									<td>
										<asp:Label ID="LBIDNO" runat="server" />
									</td>
								</tr>
								<tr>
									<th>
										電子郵件
									</th>
									<td>
										<asp:Label ID="LBEMAIL" runat="server" />
									</td>
								</tr>
							</table>
							
							<h2 class="detailsBar_title_basic" style="background:rgba(195,225,217,1.00);">權限管理</h2>
				
							<table class="MTB">
								<tr>
									<th style="width:17.5%;">
										科室<span style="color: red;font-family:cursive;">＊</span>
									</th>
									<td>
										<asp:TextBox ID="TXTDEPARTMENT" runat="server" />
									</td>
								</tr>
								<tr>
									<th style="width:17.5%;">
										職稱<span style="color: red;font-family:cursive;">＊</span>
									</th>
									<td>
										<asp:TextBox ID="TXTJOBTITLE" runat="server" />
									</td>
								</tr>
								<tr>
									<th style="width:17.5%;">
										使用者群組<span style="color: red;font-family:cursive;">＊</span>
									</th>
									<td>
										<asp:DropDownList ID="DDLROLE" runat="server" />
									</td>
								</tr>
							
								<tr>
									<th style="width:17.5%;">
										申請狀態
									</th>
									<td>
										<asp:DropDownList ID="DDLSTATUS" runat="server" /><br><br>
										<span>駁回原因：</span>
										<asp:TextBox ID="TXTREASON" runat="server" CssClass="w-70" />
									</td>
								</tr>
							</table>
						</div>
				
		
						<br/>
				
							<div class="checklist-btn">
								<asp:Button ID="GoHome" runat="server" Text="返回列表" OnClick="GoHome_Click" />
								<asp:Button ID="SaveAcc" runat="server" Text="確認送出" OnClick="SaveAcc_Click" />
							</div>
			
				</div>
        
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
