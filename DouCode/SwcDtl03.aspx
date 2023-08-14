<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SwcDtl03.aspx.cs" Inherits="DouCode_SwcDtl03" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>施工監督檢查紀錄表</title>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge, chrome=1">
    <link rel="stylesheet" type="text/css" href="../css/reset.css" />
    <link rel="stylesheet" type="text/css" href="../css/allSwc.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="header">
                <a href="http://tcgeswc.taipei.gov.tw/tslmservice/" title="臺北市政府大地工程處坡地管理資料庫" class="header-link"><img src="../images/banner.jpg" alt=""/></a>
                <div class="header-menu">
                    <a href="SwcDtl03.aspx"><img src="../images/title/swcch_m01b.png" alt="施工監督檢查" /></a>
                    <a href="SwcDtl07.aspx"><img src="../images/title/swcch_m02a.png" alt="設施維護檢查"/></a>
                </div>
                <a href="#" style="display: inline-block;"><img src="../images/title/title_swcchg.png" alt=""/></a>
                <span class="l-span" style="float: right;">
                    <asp:Literal ID="TextUserName" runat="server">大雷包，您好</asp:Literal>&nbsp&nbsp&nbsp
                    <asp:Button ID="BtnLogOut" runat="server" Text="登出" Height="22px" UseSubmitBehavior="False" OnClick="BtnLogOut_Click" Font-Names="標楷體" />
                </span>
            </div>
            <div class="content">
                <div class="content-select">
                    <table>
                        <tbody>
                            <tr>
                                <td colspan="2">案件編號：<input type="text"></td>
                            </tr>
                            <tr>
                                <td>檢查日期：<input type="date">~<input type="date"></td>
                                <td>地點：<input type="text"></td>
                            </tr>
                            <tr>
                                <td colspan="2">計畫名稱：<input type="text"></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="l-add">
                    <asp:HyperLink ID="DT003" runat="server" ImageUrl="../images/btn/icon_newswcch.png" CssClass="detailsGrid_wrap_btn" NavigateUrl="~/DouCode/SWCDT003.aspx?SWCNO=&DTLNO=AddNew" Visible="true"/>
                </div>

                <asp:GridView ID="SWCDTL03" runat="server" CssClass="content-grid" AutoGenerateColumns="False"
                    PageSize="20" AllowPaging="True"
                    OnRowCommand="SWCDTL03_RowCommand" OnRowCreated="SWCDTL03_RowCreated" >
                    <%--OnRowDataBound="SWCDTL07_RowDataBound" OnPageIndexChanging="SWCDTL07_PageIndexChanging" OnPageIndexChanged="SWCDTL07_PageIndexChanged"--%>
                    <Columns>
                        <asp:BoundField DataField="DTLC001" HeaderText="施工檢查案件編號" />
                        <asp:BoundField DataField="DTLC002" HeaderText="施工檢查日期" />                
                        <asp:BoundField DataField="DTLC003" HeaderText="計畫名稱" />
                        <asp:BoundField DataField="DTLC004" HeaderText="義務人姓名" ItemStyle-Width="270px"/>
                        <asp:BoundField DataField="DTLC005" HeaderText="技師姓名" ItemStyle-Width="150px"/>
                        <asp:BoundField DataField="DTLC006" HeaderText="SWC000" ItemStyle-Width="150px" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:Button ID="ButtonDTL03" runat="server" CommandArgument='<%# Eval("DTLC007") %>' CommandName="EditPage" Text="詳情" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Button id="ButtonDEL03" runat="server" CommandArgument='<%# Eval("DTLC001") %>' CommandName="deldtl" Text="刪除" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
