<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SwcDtl07.aspx.cs" Inherits="DouCode_SwcDtl07" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>設施維護檢查及輔導紀錄表</title>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge, chrome=1">
    <link rel="stylesheet" type="text/css" href="../css/reset.css" />
    <link rel="stylesheet" type="text/css" href="../css/allSwc.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"/>
            <div class="header">
                <a href="http://tcgeswc.taipei.gov.tw/tslmservice/" title="臺北市政府大地工程處坡地管理資料庫" class="header-link"><img src="../images/banner.jpg" alt=""></a>
                <div class="header-menu">
                    <a href="SwcDtl03.aspx"><img src="../images/title/swcch_m01a.png" alt="施工監督檢查" /></a>
                    <a href="#"><img src="../images/title/swcch_m02b.png" alt="設施維護檢查"></a>
                </div>
                <a href="#" style="display: inline-block;"><img src="../images/title/title_swcch.png" alt=""></a>
                <span class="l-span" style="float: right;">
                    <asp:Literal ID="TextUserName" runat="server">大雷包，您好</asp:Literal>&nbsp&nbsp&nbsp
                    <asp:Button ID="BtnLogOut" runat="server" Text="登出" Height="22px" UseSubmitBehavior="False" OnClick="BtnLogOut_Click" Font-Names="標楷體" />
                </span>
            </div>
            <div class="content">
                <div class="content-select">
                    <table>
                        <tbody>
                            <tr><td>案件編號：
                                    <asp:TextBox ID="Sh001" runat="server" MaxLength="30"/></td>
                                <td>水保案件編號：
                                    <asp:TextBox ID="Sh002" runat="server" MaxLength="30"/></td></tr>
                            <tr><td>檢查日期：
                                    <asp:TextBox ID="Sh003" runat="server" width="150px"></asp:TextBox>
                                    <asp:CalendarExtender ID="Sh003_CalendarExtender" runat="server" TargetControlID="Sh003" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                    ~
                                    <asp:TextBox ID="Sh004" runat="server" width="150px"></asp:TextBox>
                                    <asp:CalendarExtender ID="Sh004_CalendarExtender" runat="server" TargetControlID="Sh004" Format="yyyy-MM-dd"></asp:CalendarExtender></td>
                                <td>地點：
                                    <asp:TextBox ID="Sh005" runat="server" MaxLength="30"/></td></tr>
                            <tr><td colspan="2">計畫名稱：
                                    <asp:TextBox ID="Sh006" runat="server" MaxLength="30"/></td></tr>
                            <tr><td colspan="2" style="text-align:center;">
                                    <asp:Button ID="ExeQSel" runat="server" Text="查詢" OnClick="ExeQSel_Click" />
                                    <asp:Button ID="RemoveSel" runat="server" Text="清除" OnClick="RemoveSel_Click" /></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="l-add">
                    <asp:HyperLink ID="DT007" runat="server" ImageUrl="../images/btn/icon_newswcch.png" CssClass="detailsGrid_wrap_btn" NavigateUrl="~/DouCode/SWCDT007.aspx?SWCNO=&DTLNO=AddNew" Visible="false"/>
                </div>
                <asp:GridView ID="SWCDTL07" runat="server" CssClass="content-grid" AutoGenerateColumns="False"
                    PageSize="20" AllowPaging="True"
                    OnRowCreated="SWCDTL07_RowCreated" OnRowCommand="SWCDTL07_RowCommand"
                    OnRowDataBound="SWCDTL07_RowDataBound" OnPageIndexChanging="SWCDTL07_PageIndexChanging" OnPageIndexChanged="SWCDTL07_PageIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="DTLG001" HeaderText="案件編號" />
                        <asp:BoundField DataField="DTLG002" HeaderText="水保案件編號" />                
                        <asp:BoundField DataField="DTLG003" HeaderText="檢查日期" />
                        <asp:BoundField DataField="DTLG004" HeaderText="計畫名稱" ItemStyle-Width="270px"/>
                        <asp:BoundField DataField="DTLG005" HeaderText="申請水土保持義務人" ItemStyle-Width="150px"/>
                        <asp:BoundField DataField="DTLG006" HeaderText="SWC000" ItemStyle-Width="150px" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:Button ID="ButtonDTL07" runat="server" CommandArgument='<%# Eval("DTLG007") %>' CommandName="EditPage" Text="詳情"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Button id="ButtonDEL07" runat="server" CommandArgument='<%# Eval("DTLG001") %>' CommandName="deldtl" Text="刪除" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="ButtonDEL07_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>

        </div>
    </form>
</body>
</html>
