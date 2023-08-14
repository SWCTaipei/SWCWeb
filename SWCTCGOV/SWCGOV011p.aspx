<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCGOV011p.aspx.cs" Inherits="SWCTCGOV_SWCGOV011p" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>臺北市水土保持申請書件管理平台</title>
    <link rel="Shortcut Icon" type="image/x-icon" href="../images/logo-s.ico">
    <meta name="keywords" content="臺北市水土保持申請書件管理平台, 書件管理平台">
    <meta name="description" content="臺北市水土保持申請書件管理平台">
    <meta name="author" content="dobubu">
    <meta name="copyright" content="© 2017 臺北市水土保持申請書件管理平台">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge, chrome=1">
    <link rel="stylesheet" type="text/css" href="../css/reset.css" />
    <link rel="stylesheet" type="text/css" href="../css/all.css" />
    <link rel="stylesheet" type="text/css" href="../css/iris.css" />
    <style type="text/css">
        .hiddencol {
            display: none;
        }

        .viscol {
            display: block;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <div class="wrap-s">
            <div class="header-wrap-s">
                <div class="header header-s clearfix">
                    <a href="../SWCDOC/SWC001.aspx" class="logo-s"></a>
                    <div class="header-menu-s">
                        <ul>
                            <li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
                            <li>|</li>
                            <li><a href="http://swc.taipei/swcinfo/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                            <%--<asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="http://172.28.100.55/tslmwork" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
                    <asp:Panel ID="TitleLink00" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="../SWCDOC/SWCBase001.aspx" title="帳號管理">帳號管理</a></li></asp:Panel>
                    <asp:Panel ID="GOVMG" runat="server" Visible="false"><li class="system">|&nbsp&nbsp&nbsp&nbsp<a href="../SWCTCGOV/SWCGOV001.aspx" title="系統管理">系統管理</a><ul><li><a href="../SWCTCGOV/SWCGOV001.aspx">防災事件通知</a></li><li><a href="../SWCTCGOV/SWCGOV011.aspx">公佈欄</a></li></ul></li></asp:Panel>--%>
                            <%--<li>|</li>
                    <li><a href="../SWCDOC/SWC000.aspx?ACT=LogOut" title="登出">登出</a></li>--%>
                        </ul>
                    </div>
                </div>

                <div class="header-s-green">
                    <div class="header-s-green-nameWrap">
                        <span>
                            <%--<asp:Literal ID="TextUserName" runat="server">多維，您好</asp:Literal>--%></span>
                    </div>
                </div>
            </div>
            <div class="contentFooter">
                <div class="content-add">
                    <div class="detailsMenu">
                        <img src="../images/title/bbd.png" alt="" />
                    </div>
                </div>
            </div>

            <div class="inwrap">
                <div style="clear: both">
                    <br />
                    <div class="GOV001">

                        <asp:GridView ID="GVSWCList" runat="server" DataSourceID="SqlDataSource" CssClass="ADDGridView systemadd AutoNewLine " PagerStyle-CssClass="pgr"
                            OnRowDataBound="GVSWCList_RowDataBound" EmptyDataText="沒有符合查詢條件的資料" AllowPaging="true" PageSize="20" AllowSorting="True"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="BBNo" HeaderText="流水號" SortExpression="BBNo" ItemStyle-Width="150px" HeaderStyle-CssClass="bbw-red hiddencol" ItemStyle-CssClass="hiddencol" />
                                <asp:BoundField DataField="BBDateStart" HeaderText="公佈日期" SortExpression="BBDateStart" ItemStyle-Width="220px" />
                                <asp:BoundField DataField="BBUnit" HeaderText="公佈單位" SortExpression="BBUnit" ItemStyle-Width="220px" />
                                <asp:TemplateField HeaderText="公佈主旨" SortExpression="BBTitle">
                                    <ItemTemplate>
                                        <asp:Label ID="BBTitle" runat="server" Text='<%# Bind("BBTitle") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
								<asp:BoundField DataField="BBViewers" HeaderText="觀看人次" SortExpression="BBViewers" ItemStyle-Width="200px" />
                                <asp:TemplateField ShowHeader="false" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:Button ID="BtnView" runat="server" CommandArgument='<%# Eval("BBNo") %>' Text="詳情" OnClick="BtnView_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings LastPageText="最後一頁" FirstPageText="第一頁" Mode="NumericFirstLast" NextPageText="下一頁" PreviousPageText="上一頁" />
                            <PagerStyle CssClass="pgr" BackColor="#7baac5" Width="100%" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SWCConnStr %>"
                            SelectCommand=" select replace(replace(BBShow,1,'Y'),0,'N') as Display ,* from BillBoard Where left(convert(varchar, getdate(), 120), 10) >= BBDateStart and left(convert(varchar, getdate(), 120), 10) <= BBDateEnd and BBShow='是' ORDER BY [BBDateStart] DESC "></asp:SqlDataSource>

                    </div>
                </div>
            </div>
			
            <div class="btncenter"><a href="../Default.aspx"><input type="button" value="回到首頁"></a></div>

            <div class="footer">
                <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                     <span class="span2">110臺北市信義區松德路300號3樓 　服務專線(02)27591109   臺北市民當家熱線1999</span><br/>
                      <span class="span2">建議使用IE11(含)以上，Chrome或Firefox版本瀏覽器　<b>資料更新：</b><asp:Label ID="ToDay" runat="server" Text=""/>　<b>來訪人數：</b><asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                      <span class="span2"><b>客服電話：</b>02-27929328 陳小姐　<b>信箱：</b>tcge7@geovector.com.tw　本系統由多維空間資訊有限公司開發維護 TEL：(02)27929328</span><br/>
			          <span class="span2">※為維護系統服務品質，本平台訂於每周三凌晨AM 4:00-6:30 進行系統維護更新，更新期間偶有瞬斷情形，敬請使用者避開該時段使用。謝謝！</span></p>
            </div>
    </form>
</body>
</html>
