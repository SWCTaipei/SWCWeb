<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserBoardList.aspx.cs" Inherits="SWCDOC_UserBoard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>臺北市水土保持申請書件管理平台-留言版</title>

    <link rel="stylesheet" type="text/css" href="../css/reset.css" />
    <link rel="stylesheet" type="text/css" href="../css/all.css" />
    <link rel="stylesheet" type="text/css" href="../css/iris.css" />
    <link rel="stylesheet" type="text/css" href="../css/ad.css" />
</head>

<body>
    <form id="form1" runat="server">
        <div class="wrap-s">
            <div class="header-wrap-s">
                <div class="header header-s clearfix"><a href="SWC001.aspx" class="logo-s"></a>

                <div class="header-menu-s">
                    <ul>
                        <li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
                        <li>|</li>
                        <li><a href="http://www.swc.taipei/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false">
                            <li>|&nbsp&nbsp&nbsp&nbsp<a href="https://tslm.swc.taipei/tslmwork/NF000/PG000.aspx" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li>
                        </asp:Panel>
                        <asp:Panel ID="TitleLink00" runat="server" Visible="false">
                            <li>|&nbsp&nbsp&nbsp&nbsp<a href="SWCBase001.aspx" title="帳號管理">帳號管理</a></li>
                        </asp:Panel>
                        <asp:Panel ID="UserBoard00" runat="server" Visible="false">
                            <li>|&nbsp&nbsp&nbsp&nbsp<a href="UserBoardList.aspx" title="留言板">留言板</a></li>
                        </asp:Panel>
                        <asp:Panel ID="GOVMG" runat="server" Visible="false">
							<li class="flip">|&nbsp&nbsp&nbsp<a href="#" title="系統管理">系統管理+</a>
								<ul class="openlist" style="display: none;">
									<li><a href="../SWCTCGOV/SWCGOV001.aspx">防災事件通知</a></li>
									<li><a href="../SWCTCGOV/SWCGOV011.aspx">公佈欄</a></li>
									<li><a href="../SWCDOC/UserBoardList.aspx">留言版</a></li>
									<li><a href="http://tgeo.swc.taipei/">T-GEO空間地理資訊平台</a></li>
								</ul>
							</li>
                        </asp:Panel>
                        <li>|</li>
                        <li><a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li>
                    </ul>
                </div>
                </div>
                <div class="header-s-green">
                    <div class="header-s-green-nameWrap">
                        <span>
                            <asp:Literal ID="TextUserName" runat="server">多維，您好</asp:Literal></span>
                    </div>
                </div>
            </div>

            <div class="contentFooter">
                <div class="content-s content-s-inquire">
                    <div class="detailsMenu">
                        <img src="../images/title/message-02.png" /><br><br><br>
                        <asp:ImageButton ID="NewCase" runat="server" OnClick="NewCase_Click" title="新增案件" ImageUrl="../images/btn/btn-addCase.png" />
                    </div>

                    <asp:GridView ID="GVUserBoard" runat="server" DataSourceID="SqlDataSource_GVRPJ" CssClass="ADDGridView AutoNewLine" DataKeyNames="UB001" Width="100%"
                        AllowSorting="True" AutoGenerateColumns="False" OnRowDataBound="GVUserBoard_RowDataBound" PageSize="10" EmptyDataText="沒有符合查詢條件的資料">
                        <Columns>
                            <asp:BoundField DataField="" HeaderText="" SortExpression="" HeaderStyle-Width="10" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="UB001" HeaderText="編號" SortExpression="UB001"  />
                            <asp:BoundField DataField="UB002" HeaderText="填寫者名稱" SortExpression="UB002"  />
                            <asp:BoundField DataField="UB003" HeaderText="主旨" SortExpression="UB003" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="UB004" HeaderText="內容" SortExpression="UB004"  ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="savedate" HeaderText="送出時間" SortExpression="savedate"  ItemStyle-HorizontalAlign="Right" />
                            <%--<asp:BoundField DataField="UB006" HeaderText="處理方式" SortExpression="UB006" ItemStyle-HorizontalAlign="Right" />--%>
                            <%--<asp:TemplateField ItemStyle-Width="40">
                    <ItemTemplate>
                        <asp:Button ID="ButtonDTL" runat="server" CommandArgument='<%# Eval("UBNo") %>' Text="詳情" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="40">
                    <ItemTemplate>
                        <asp:Button ID="ButtonEDIT" runat="server" CommandArgument='<%# Eval("UBNo") %>' Text="編輯" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                            <%--<asp:TemplateField ItemStyle-Width="40" Visible="false">
                    <ItemTemplate>
                    <asp:Button ID="delbutton" DataField="UBNo" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('確認刪除這筆資料?')" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                        </Columns>
                        <HeaderStyle Font-Bold="True" ForeColor="White" />
                    </asp:GridView>

                    <asp:SqlDataSource ID="SqlDataSource_GVRPJ" runat="server" ConnectionString="<%$ ConnectionStrings:SWCConnStr %>"
                        SelectCommand=" SELECT * FROM [UserBoard] ORDER BY [id] DESC "></asp:SqlDataSource>
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
		
        <script src="../js/jquery-3.1.1.min.js"></script>
        <script src="../js/inner.js"></script>
    </form>
</body>
</html>
