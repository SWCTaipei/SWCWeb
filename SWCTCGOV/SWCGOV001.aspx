<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCGOV001.aspx.cs" Inherits="SWCTCGOV_SWCGOV001" %>
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
                    <li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
                    <li>|</li>
                    <li><a href="https://swc.taipei/swcinfo/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                    <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="http://tslm.swc.taipei/tslmwork/" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
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
                    <li><a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li>
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
                    <img src="../images/title/btn-87.png" alt=""/>
                </div>
            </div>
        </div>
        
        <div class="inwrap">
            <div class="inquireForm-typhoom date">
                通知事件：
                <asp:DropDownList ID="DDLDETYPE" runat="server" /><br />

                案件狀態：
				<asp:CheckBoxList ID="CHKSWCSTATUS" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" RepeatColumns="12">
					<asp:ListItem>退補件</asp:ListItem>
					<asp:ListItem>不予受理</asp:ListItem>
					<asp:ListItem>受理中</asp:ListItem>
					<asp:ListItem>審查中</asp:ListItem>
					<asp:ListItem>暫停審查</asp:ListItem>
					<asp:ListItem>撤銷</asp:ListItem>
					<asp:ListItem>不予核定</asp:ListItem>
					<asp:ListItem>已核定</asp:ListItem>
					<asp:ListItem>施工中</asp:ListItem>
					<asp:ListItem>停工中</asp:ListItem>
					<asp:ListItem>已完工</asp:ListItem>
					<asp:ListItem>廢止</asp:ListItem>
					<asp:ListItem>失效</asp:ListItem>
					<asp:ListItem>已變更</asp:ListItem>
					
					<asp:ListItem>結案</asp:ListItem>
					<asp:ListItem>撤銷</asp:ListItem>
					<asp:ListItem>續予處分</asp:ListItem>
					<asp:ListItem>改正中</asp:ListItem>
					<asp:ListItem>無改正事項</asp:ListItem>
					<asp:ListItem>程序補正中</asp:ListItem>
				</asp:CheckBoxList>
                <asp:DropDownList ID="DDLSWCSTATUS" runat="server" Visible="false" />
                
                
                
                事件名稱：<asp:TextBox ID="TXTDENAME" runat="server" MaxLength="50" style="width:200px;"/>&nbsp;&nbsp;&nbsp;
                
				內容查詢：<asp:TextBox ID="TXTDENTTEXT" runat="server" MaxLength="50" style="width:200px;"/>&nbsp;&nbsp;&nbsp;
                發送日期：
				<asp:TextBox ID="TXTDATE1" runat="server" width="120px" Style="margin-right: 0px;"></asp:TextBox>
                <asp:CalendarExtender ID="TXTDATE1_CalendarExtender" runat="server" TargetControlID="TXTDATE1" Format="yyyy-MM-dd"></asp:CalendarExtender>
                ~
                <asp:TextBox ID="TXTDATE2" runat="server" width="120px"></asp:TextBox>
                <asp:CalendarExtender ID="TXTDATE2_CalendarExtender" runat="server" TargetControlID="TXTDATE2" Format="yyyy-MM-dd"></asp:CalendarExtender><br />
				
				通知對象：<asp:CheckBoxList ID="CHKSENDMBR" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"/><br />
    
                通知管道：<asp:CheckBoxList ID="CHKSENDFUN" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"/><br/>
                
                <div class="inquireForm-btn">
                    <asp:Button ID="ExeQSel" runat="server" Text="查詢" OnClick="ExeQSel_Click" OnClientClick="return InputChk();"/>
                    <asp:Button ID="RemoveSel" runat="server" Text="清除" OnClick="RemoveSel_Click" />
                </div>
        </div>

        <div style="clear:both"><br />
            
        <div class="GOV001">
              <div class="inquireGrid-menu_s" >
                <h3>查詢到件數：<asp:Label ID="CaseCount" runat="server" Text="0" /> 筆</h3>
				<asp:Button ID="NewSwc" class="greentitle" runat="server" OnClick="NewSwc_Click" Text="新增通知" />
				<asp:ImageButton ID="WriteExcel" runat="server" title="輸出Excel" ImageUrl="../images/btn/btn-excel.png" OnClientClick="return confirm('確認是否要輸出Excel？')" Visible="false" />
              </div>



                <%--OnRowCommand="GVSWCList_RowCommand"  OnPageIndexChanging="GVSWCList_PageIndexChanging" OnSorting="GVSWCList_Sorting"--%>



                
                <asp:GridView ID="GVSWCList" runat="server" DataSourceID="SqlDataSource" CssClass="ADDGridView AutoNewLine" PagerStyle-CssClass="pgr"
                    OnRowDataBound="GVSWCList_RowDataBound" OnPageIndexChanging="GVSWCList_PageIndexChanging"
                    EmptyDataText="沒有符合查詢條件的資料" AllowPaging="true" PageSize="20" AllowSorting="True"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="" HeaderText="" SortExpression=""></asp:BoundField>
                        <asp:BoundField DataField="DENO" HeaderText="流水號" SortExpression="DENO" ItemStyle-Width="200px"/>
                        <asp:BoundField DataField="DETYPE1" HeaderText="通知事件" SortExpression="DETYPE1" ItemStyle-Width="250px"/>
                        <asp:BoundField DataField="DENAME" HeaderText="事件名稱" SortExpression="DENAME" ItemStyle-Width="300px"/>
                        <asp:BoundField DataField="DEDATE" HeaderText="發送日期" SortExpression="DEDATE" ItemStyle-Width="180px"/>
                        <asp:BoundField DataField="DESENDCOUNT" HeaderText="發送筆數" SortExpression="DESENDCOUNT"  ItemStyle-Width="180px" HeaderStyle-VerticalAlign="Middle" Visible="false" />
                        <asp:BoundField DataField="DEINPUTCOUNT" HeaderText="已登入筆數" SortExpression="DEINPUTCOUNT" ItemStyle-Width="180px"/>
                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:Button ID="BtnView" runat="server" CommandArgument='<%# Eval("DENO") %>' Text="詳情" OnClick="BtnView_Click" Visible="false"/>
                                <asp:Button ID="BtnEdit" runat="server" CommandArgument='<%# Eval("DENO") %>' Text="編輯" OnClick="BtnEdit_Click" Visible="false" />
                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("DESENT") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:Button ID="BtnDelete" runat="server" CommandArgument='<%# Eval("DENO") %>' Text="刪除" OnClientClick="return confirm('是否確定要刪除');" OnClick="BtnDelete_Click" Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings LastPageText="最後一頁" FirstPageText="第一頁" Mode="NumericFirstLast" NextPageText="下一頁" PreviousPageText="上一頁"  />
                    <PagerStyle  CssClass="pgr"  BackColor="#7baac5" Width="100%"/>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SWCConnStr %>" OnSelected="SqlDataSource_Selected"
                    SelectCommand=" select *,case DETYPE when '0104' then '颱風豪雨通知回傳自主檢查表' when '0203' then '違規案件防災整備通知' end DETYPE1 from DisasterEvent ORDER BY [DENO] DESC ">
                </asp:SqlDataSource>

                </div>
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
       
        <script src="../js/jquery-3.1.1.min.js"></script>
        <script src="../js/inner.js"></script>
    </form>
</body>
</html>
