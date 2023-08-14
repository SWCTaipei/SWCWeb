<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCOrganList.aspx.cs" Inherits="SWCDOC_SWCOrganList" %>
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
    <link rel="stylesheet" type="text/css" href="../css/all.css?202103230541"/>
    <link rel="stylesheet" type="text/css" href="../css/iris.css"/>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"/>
    
		<div class="wrap-s">
			<div class="header-wrap-s">
				<div class="header header-s clearfix"><a href="../SWCDOC/SWC001.aspx" class="logo-s"></a>
					<div class="header-menu-s">
						<ul>
							<li>
								<a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a>
							</li>
							<li>
								|
							</li>
							<li>
								<a href="https://swc.taipei/swcinfo/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a>
							</li>
							<asp:Panel ID="GoTslm" runat="server" Visible="false">
								<li>
									|&nbsp&nbsp&nbsp&nbsp<a href="http://tslm.swc.taipei/tslmwork/" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a>
								</li>
							</asp:Panel>
							<asp:Panel ID="TitleLink00" runat="server" Visible="false">
								<li>
									|&nbsp&nbsp&nbsp&nbsp<a href="../SWCDOC/SWCBase001.aspx" title="帳號管理">帳號管理</a>
								</li>
							</asp:Panel>
							<asp:Panel ID="GOVMG" runat="server" Visible="false">
								<li class="flip">
									|&nbsp&nbsp&nbsp&nbsp<a href="#" title="系統管理">系統管理+</a>
									<ul class="openlist" style="display: none;">
										<li>
											<a href="../SWCTCGOV/SWCGOV001.aspx">防災事件通知</a>
										</li>
										<li>
											<a href="../SWCTCGOV/SWCGOV011.aspx">公佈欄</a>
										</li>
										<li>
											<a href="../SWCDOC/UserBoardList.aspx">留言版</a>
										</li>
										<li>
											<a href="http://tgeo.swc.taipei/">T-GEO空間地理資訊平台</a>
										</li>
									</ul>
								</li>
							</asp:Panel>
							<li>
								|
							</li>
							<li>
								<a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a>
							</li>
						</ul>
					</div>
				</div>

				<div class="header-s-green">
					<div class="header-s-green-nameWrap">
						<span><asp:Literal ID="TextUserName" runat="server">大雷包，您好</asp:Literal></span>
					</div>
				</div>
			</div>
			<div class="contentFooter">
				<div class="content-s content-s-details">
					<div class="detailsMenu">
						<img src="../images/title/organ.png" alt=""/>
					</div>
				</div>
			</div>
        
			<div class="inwrap">
				<div class="inquireForm-typhoom date">
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<span>編號：
						<asp:TextBox ID="TXTACCTNO" runat="server" />
					</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<span>姓名：
						<asp:TextBox ID="TXTNAME" runat="server" />
					</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<span>身分證字號：
						<asp:TextBox ID="TXTIDNO" runat="server" />
					</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<span>審核狀態：
						<asp:DropDownList ID="DDLSTATUS" runat="server">
							<asp:ListItem></asp:ListItem>
							<asp:ListItem>申請中</asp:ListItem>
							<asp:ListItem>已開通</asp:ListItem>
							<asp:ListItem>駁回</asp:ListItem>
							<asp:ListItem>停用</asp:ListItem>
						</asp:DropDownList>
					</span>
					<div class="inquireForm-btn">
						<asp:Button ID="ExeQSel" runat="server" Text="查詢" />
						<asp:Button ID="RemoveSel" runat="server" Text="清除" />
					</div>
				</div>
			</div>

			<div style="clear:both"><br />
            
				<div class="GOV001">
					<div class="inquireGrid-menu_s" >
						<h3>查詢到件數：<asp:Label ID="CaseCount" runat="server" Text="0" /> 筆</h3>
					</div>
                
					<asp:GridView ID="GVOrganList" runat="server" CssClass="ADDGridView AutoNewLine" PagerStyle-CssClass="pgr"
						OnRowDataBound="GVOrganList_RowDataBound" DataSourceID="SqlDataSource"
						EmptyDataText="沒有符合查詢條件的資料" AllowPaging="true" PageSize="20" AllowSorting="True"
						AutoGenerateColumns="false">
						<Columns>
							<asp:BoundField DataField="ACCOUNTNO" HeaderText="編號" SortExpression="ACCOUNTNO" ItemStyle-Width="250px"/>
							<asp:BoundField DataField="NAME" HeaderText="姓名" SortExpression="NAME" ItemStyle-Width="250px"/>
							<asp:BoundField DataField="ROLE" HeaderText="使用者群組" SortExpression="ROLE" ItemStyle-Width="250px"/>
							<asp:BoundField DataField="STATUS" HeaderText="審核狀態" SortExpression="STATUS" ItemStyle-Width="200px"/>
							<asp:TemplateField ShowHeader="false">
								<ItemTemplate>
									<asp:Button ID="BtnView" runat="server" CommandArgument='<%# Eval("ACCOUNTNO") %>' Text="詳情" OnClick="BtnView_Click" />
								</ItemTemplate>
							</asp:TemplateField>
						</Columns>
						<PagerSettings LastPageText="最後一頁" FirstPageText="第一頁" Mode="NumericFirstLast" NextPageText="下一頁" PreviousPageText="上一頁"  />
						<PagerStyle  CssClass="pgr"  BackColor="#7baac5" Width="100%"/>
					</asp:GridView>
					<asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SWCConnStr %>" OnSelected="SqlDataSource_Selected"
                    SelectCommand=" select AccountNo,Name,Role,Status from Organ ORDER BY [CreateDate] DESC ">
                </asp:SqlDataSource>
                </div>
			</div>
        
            <br/><br/><br/>
        
            <div class="footer">
                <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                     <span class="span2">110臺北市信義區松德路300號3樓 　服務專線(02)27591109   臺北市民當家熱線1999</span><br/>
                      <span class="span2">建議使用IE11(含)以上，Chrome或Firefox版本瀏覽器　<b>資料更新：</b><asp:Label ID="ToDay" runat="server" Text=""/>　<b>來訪人數：</b><asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                      <span class="span2"><b>客服電話：</b>02-27929328 陳小姐　<b>信箱：</b>tcge7@geovector.com.tw　本系統由多維空間資訊有限公司開發維護 TEL：(02)27929328</span><br/>
			          <span class="span2">※為維護系統服務品質，本平台訂於每周三凌晨AM 4:00-6:30 進行系統維護更新，更新期間偶有瞬斷情形，敬請使用者避開該時段使用。謝謝！</span></p>
            </div>
		</div>
        <script src="../js/jquery-3.1.1.min.js"></script>
        <script src="../js/inner.js?202103230541"></script>
    </form>
</body>
</html>
