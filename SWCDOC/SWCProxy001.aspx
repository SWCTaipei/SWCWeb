<%@ Page Title="臺北市水土保持申請書件管理平台" Language="C#" MasterPageFile="~/PriPage/MasterPageA.master" AutoEventWireup="true" CodeFile="SWCProxy001.aspx.cs" Inherits="SWCDOC_SWCProxy001" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<link rel="stylesheet" type="text/css" href="../css/calendar/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="../css/calendar/calendar_test.css">
    <script src="../js/calendar/jquery-2.2.3.min.js"></script>
    <script src="../js/calendar/angular.min.js"></script>
    <script src="../js/calendar/moment.js"></script>
    <script src="../js/calendar/fullcalendar.min.js"></script>
    <script src="../js/calendar/bootstrap.min.js"></script>
    <script src="../js/calendar/ui-bootstrap-tpls.js"></script>
    <script src="../js/calendar/calendar.js"></script>
    <script src="../js/calendar/lang-all.js"></script>
    <script src="https://apis.google.com/js/api.js"></script>
    <script src="../js/calendar/calendar_test.js"></script>
    <script src="../js/calendar/calendar_test1.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="content-s">
        <div  class="lookCD"><a href="#">設定代理人</a></div><br><br><br>
		<div class="applyGrid">
          <h2 class="detailsBar_title_basic">基本資料</h2>
        <%--<h1>技師－新增代理人</h1>--%>
        <table class="ph-table">
            <tr><th>姓名</th>
                <td><asp:TextBox ID="TXTProxyName" runat="server"/></td></tr>
            <tr><th>身分證字號</th>
                <td><asp:TextBox ID="TXTProxyID" runat="server" maxlength="10"/></td></tr>
            <tr><th>手機</th>
                <td><asp:TextBox ID="TXTProxyPhoneNo" runat="server" maxlength="10"/></td></tr>
        </table>
        <br><br>
		<div class="center">
			<span class="red">※ 本平台提供台北通登入方式，請確認您的代理人已擁有台北通註冊帳號並與上方輸入資訊一致。</span><br/><br/>
            <asp:Button ID="DataLock" runat="server" Text="新增代理人" OnClick="DataLock_Click" OnClientClick="return confirm('身分證字號送出後即不可再修正，請再次確認');" />
        </div>
		<div  class="lookCD1"><a href="#">代理人列表</a></div><br><br><br>
		<asp:GridView ID="ProxyList" runat="server" CssClass="ADDGridView AutoNewLine" AutoGenerateColumns="False" Height="50" EmptyDataText="查無資料" OnRowCommand="ProxyList_RowCommand">
            <Columns>
				<asp:TemplateField HeaderText="姓名">
                    <ItemTemplate>
                        <asp:TextBox ID="Proxy_Name" runat="server" style="width:120px;" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("Proxy_Name"))) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Proxy_ID" HeaderText="身分證字號" HeaderStyle-Width="280px" />
				<asp:TemplateField HeaderText="手機">
                    <ItemTemplate>
                        <asp:TextBox ID="Proxy_PhoneNo" runat="server" style="width:120px;" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("Proxy_PhoneNo"))) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
				<asp:TemplateField HeaderText="狀態">
                    <ItemTemplate>
						<asp:DropDownList ID="Proxy_Status" runat="server" SelectedValue='<%# Server.HtmlEncode(Convert.ToString(Eval("Proxy_Status"))) %>'>
							<asp:ListItem>啟用</asp:ListItem>
							<asp:ListItem>停用</asp:ListItem>
						</asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:ButtonField ButtonType="Button" CommandName="update_ProxyStatus" Text="存檔"/>
            </Columns>
        </asp:GridView>
		
		<br><br>
		<div  class="lookCD2"><a href="#">代理人編輯紀錄</a></div><br><br><br>
		<asp:GridView ID="ProxyList_record" runat="server" CssClass="changerequest AutoNewLine" AutoGenerateColumns="False" Height="50" EmptyDataText="無資料" >
            <Columns>
                <asp:BoundField DataField="Proxy_Name" HeaderText="姓名" HeaderStyle-Width="280px" />
                <asp:BoundField DataField="Proxy_ID" HeaderText="身分證字號" HeaderStyle-Width="280px" />
                <asp:BoundField DataField="Proxy_PhoneNo" HeaderText="手機" HeaderStyle-Width="280px" />
                <asp:BoundField DataField="Proxy_Status" HeaderText="狀態" HeaderStyle-Width="280px" />
                <asp:BoundField DataField="DateTime" HeaderText="變更時間" HeaderStyle-Width="280px" />
            </Columns>
        </asp:GridView>
	</div>
  </div>
</asp:Content>

