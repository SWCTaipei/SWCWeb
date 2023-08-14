<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FPage002ALL.aspx.cs" Inherits="SWCRD_FPage002ALL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>審查/檢查行事曆</title>
    <link rel="Shortcut Icon" type="image/x-icon" href="../images/logo-s.ico">
    <meta name="keywords" content="臺北市水土保持申請書件管理平台, 書件管理平台">
    <meta name="description" content="臺北市水土保持申請書件管理平台">
    <meta name="author" content="dobubu">
    <meta name="copyright" content="© 2017 臺北市水土保持申請書件管理平台">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge, chrome=1">
	
	<link type="text/css" rel="stylesheet" href="../css/tslmcss/all.css?202208250348"/>
    <link type="text/css" rel="stylesheet" href="../css/tslmcss/menu.css?202208030526"/>
	<link rel="stylesheet" type="text/css" href="../css/calendar/bootstrap.min.css?202208250339">
	<link rel="stylesheet" type="text/css" href="../css/calendar/calendar_test.css">
	<meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    
    <script src="../js/calendar/jquery-2.2.3.min.js"></script>
	<script type="text/javascript" src="../js/inmenu.js"></script>
    <script src="../js/calendar/angular.min.js"></script>
    <script src="../js/calendar/moment.js"></script>
    <script src="../js/calendar/fullcalendar.min.js"></script>
    <script src="../js/calendar/bootstrap.min.js"></script>
    <script src="../js/calendar/ui-bootstrap-tpls.js"></script>
    <script src="../js/calendar/calendar.js"></script>
    <script src="../js/calendar/lang-all.js"></script>
    <script src="https://apis.google.com/js/api.js"></script>
    <script src="../js/calendar/calendar_test2_2.js?20220330"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    
	<div id="bannerwrap">
			<div class="header"><a href="https://tslm.swc.taipei/tslmwork/NF000/PG000.aspx"><div class="img"></div></a></div>
        </div>
   
		
        <nav class="nav is-fixed" role="navigation">
            <div class="wrapper wrapper-flush">
                <button class="nav-toggle">
                    <div class="icon-menu">
                        <span class="line line-1"></span>
                        <span class="line line-2"></span>
                        <span class="line line-3"></span>
                    </div>
                </button>
                <div class="nav-container">
                    <ul class="nav-menu menu">
                        <li class="menu-item has-dropdown">
                            <a href="https://tslm.swc.taipei/tslmwork/SWC001/swc_104.aspx" class="menu-link">水土保持案件</a>
                            <ul class="nav-dropdown menu">
                                <li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC001/swc_104.aspx" class="menu-link"> 水保申請案件</a></li>
                                <li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC001/swcudr.aspx" class="menu-link"> 都審/環評水保案件</a></li>
                                <li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC001/swcdp.aspx" class="menu-link"> 緊急防災水保案件</a></li>
                                <li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC001/swcnn.aspx" class="menu-link"> 免擬具水保案件</a></li>
                                <li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC001/swcg.aspx" class="menu-link"> 水保輔導案件</a></li>
                               
                            </ul>
                        </li>
                        <li class="menu-item has-dropdown">
                            <a href="https://tslm.swc.taipei/tslmwork/SWC002/ilg_104.aspx" class="menu-link">違規查報取締</a>
                            <ul class="nav-dropdown menu">
                                <li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC002/ilg_104.aspx" class="menu-link"> 水保違規案件</a></li>
                                <li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC002/ilgformountant.aspx" class="menu-link"> 山保違規案件</a></li>
                                <li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC002/maybeilg.aspx" class="menu-link"> 疑似違規案件</a></li>
                            </ul>
                        </li>
                        <li class="menu-item has-dropdown">
                             <a href="https://tslm.swc.taipei/tslmwork/SWC003/dutyquery.aspx" class="menu-link">線上報表</a>
                            <ul class="nav-dropdown menu">
                                <li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC003/dutyquery.aspx" class="menu-link"> 執勤日報表</a></li>
                                <li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC003/patrolquery.aspx" class="menu-link"> 市地巡查紀錄表</a></li>
                                <li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC003/swcgquery.aspx" class="menu-link"> 水保輔導複查表</a></li>
                                <li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC003/satellitequery.aspx" class="menu-link"> 衛星變異點查證表</a></li>
                                <li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC003/gpsrecordM.aspx" class="menu-link"> 月巡查表</a></li>
                                <li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC003/score.aspx" class="menu-link"> 報表評分</a></li>
                                <li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC003/swctbM.aspx" class="menu-link"> 輔導案件巡查紀錄表</a></li>
                            </ul>
                        </li>
                        <li class="menu-item has-dropdown">
                            <a href="https://tslm.swc.taipei/tslmwork/SWC004/DAModel.aspx" class="menu-link" style="letter-spacing:-0.5px">Web based管理</a>
                            <ul class="nav-dropdown menu">
                                <li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC004/DAModel.aspx" class="menu-link">決策支援分析</a></li>
                                <%--<li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC004/SPanalysis.aspx" class="menu-link">107大數據分析</a></li>--%>
                                <li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC004/KeyWSRH.aspx" class="menu-link">自訂查詢</a></li>
                                <li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC004/swcgdatem.aspx" class="menu-link">輔導預約管理</a></li>
                                <li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC004/survey.aspx" class="menu-link">滿意度調查管理</a></li>
								<%--<li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC004/SPanalysis19.aspx" class="menu-link">108大數據分析</a></li>--%>
                                <li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC004/MoneyMoney.aspx" class="menu-link"> 坡地管理虛擬帳戶對帳</a></li>
								<li class="menu-item"> <a href="https://app.powerbi.com/view?r=eyJrIjoiNzQ4YWQ5MDQtZmRhNS00MDQ1LWJiNjQtZDYyODRkNTc0NTlhIiwidCI6IjViNjRlZThjLTkwYjctNDUwNy04ZDJhLTdhNDE5MWM1NzQ2MCIsImMiOjEwfQ%3D%3D" target="blank" class="menu-link">數位儀錶板</a></li>
                                <li class="menu-item"> <a href="https://swc.taipei/SWCAPPLY/SWCDT007L.aspx" class="menu-link"> 已完工管理介面</a></li> 
                                <li class="menu-item"> <a href="https://tslm.swc.taipei/tslmwork/SWC004/maillist.aspx" class="menu-link"> 空拍影像套疊通知紀錄</a></li>                           
						   </ul>
                        </li>
                        <li class="menu-item"><a href="http://172.28.100.55/tslm/WEBGIS1.aspx" class="menu-link">圖台查詢</a></li>
                        <li class="menu-item"><a href="https://swc.taipei/APKDL/TSM_LINK/SWC006/link.html" class="menu-link">相關連結</a></li>
                        <li class="menu-item has-dropdown">
                            <a href="https://tslm.swc.taipei/tslmwork/BaseData/UserList.aspx" class="menu-link">帳號管理</a>
                            <ul class="nav-dropdown menu">
                                <li class="menu-item"><a href="https://tslm.swc.taipei/tslmwork/BaseData/UserList.aspx" class="menu-link">帳號管理</a></li>
                                <li class="menu-item"><a href="https://tslm.swc.taipei/tslmwork/BaseData/tempuserlog.aspx" class="menu-link">帳號瀏覽紀錄</a></li>
                                <li class="menu-item"><a href="https://tslm.swc.taipei/tslmwork/SWC007/SwcDocLog.aspx" class="menu-link">書件帳號瀏覽紀錄</a></li>
                                <li class="menu-item"><a href="https://tslm.swc.taipei/tslmwork/SWC007/SwcSwcLog.aspx" class="menu-link">水土保持申請歷程</a></li>
                                <li class="menu-item"><a href="https://tslm.swc.taipei/tslmwork/BaseData/areausercompare.aspx" class="menu-link">修正案件的區承辦人</a></li>
                                <li class="menu-item"><a href="https://tslm.swc.taipei/tslmwork/BaseData/TechnicianData.aspx" class="menu-link">技師帳號審核</a></li>
                                <li class="menu-item"><a href="https://tslm.swc.taipei/tslmwork/BaseData/ETUserReview.aspx" class="menu-link">廠商子帳號審核</a></li>
                                <li class="menu-item"><a href="https://tslm.swc.taipei/tslmwork/BaseData/ETUserDetailSV.aspx" class="menu-link">水保服務團子帳號設定</a></li>
                                <li class="menu-item"><a href="https://tslm.swc.taipei/tslmwork/SWC007/GuildShifts01.aspx" class="menu-link">審查/檢查公會輪值名冊</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
		
		 <div class="content">
            <div>
                <div class="home"><img src="../images/tslmimages/home-02.png" width="16" height="14" alt="" />
                    <span><a href="https://tslm.swc.taipei/tslmwork/NF000/PG000.aspx">首頁</a></span> > 
                    <span><a href="https://tslm.swc.taipei/tslmwork/SWC001/swc_104.aspx">水土保持申請案件</a></span> > 
                    <span><a href="#">審查/檢查行事曆</a></span></div>
      
                <div class="out none">
                    <asp:Literal ID="TextUserName" runat="server"></asp:Literal>
                    <%--<asp:Button ID="SignOut" runat="server" Text="登出" OnClick="SignOut_Click" CssClass="SignOutCss" /></div>--%>
                </div> 
        
            <div style="clear:both"></div>
	
	
	<div class="content-s content-s-inquire">
        <div class="content-s1" style="margin-top:-40px;">

            <div class="linkwrap">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/SWCRD/FPage001ALL.aspx" CssClass="linkstyle2">審查案件列表</asp:HyperLink>
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/SWCRD/FPage002ALL.aspx" CssClass="linkstyle1">檢查案件列表</asp:HyperLink>

                <div class="tabssss">
                    <div class="inquireForm2 none">
                        <span>水保局編號：</span>
                        <asp:TextBox ID="TBQ001" runat="server" MaxLength="20" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                        <span>轄區：</span>
                                        <asp:DropDownList ID="DDLQ002" runat="server" AutoPostBack="True">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value="北一區">北一區</asp:ListItem>
                                            <asp:ListItem Value="北二區">北二區</asp:ListItem>
                                            <asp:ListItem Value="北三區">北三區</asp:ListItem>
                                            <asp:ListItem Value="北四區">北四區</asp:ListItem>
                                            <asp:ListItem Value="北五區">北五區</asp:ListItem>
                                            <asp:ListItem Value="北六區">北六區</asp:ListItem>
                                            <asp:ListItem Value="北七區">北七區</asp:ListItem>
                                            <asp:ListItem Value="北八區">北八區</asp:ListItem>
                                            <asp:ListItem Enabled="false" Value="北九區">北九區</asp:ListItem>
                                            <asp:ListItem Value="南一區">南一區</asp:ListItem>
                                            <asp:ListItem Value="南二區">南二區</asp:ListItem>
                                            <asp:ListItem Value="南三區">南三區</asp:ListItem>
                                            <asp:ListItem Value="南四區">南四區</asp:ListItem>
                                            <asp:ListItem Value="南五區">南五區</asp:ListItem>
                                            <asp:ListItem Value="南六區">南六區</asp:ListItem>
                                            <asp:ListItem Value="南七區">南七區</asp:ListItem>
                                            <asp:ListItem Value="南八區">南八區</asp:ListItem>
                                            <asp:ListItem Enabled="false" Value="南九區">南九區</asp:ListItem>
                                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 
                        <span>計畫名稱：</span>
                        <asp:TextBox ID="TBQ003" runat="server" MaxLength="20" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 
                         <span>監造技師：</span>
                        <asp:TextBox ID="TBQ006" runat="server" MaxLength="20" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         
                        <br />
                        <span>下次檢查日期：</span>
                        <asp:TextBox ID="TBQ004a" runat="server" autocomplete="off"></asp:TextBox>&nbsp;&nbsp;
                        <asp:CalendarExtender ID="TBQ004a_CalendarExtender" runat="server" TargetControlID="TBQ004a" Format="yyyy-MM-dd"></asp:CalendarExtender>~
                        <asp:TextBox ID="TBQ004b" runat="server" autocomplete="off"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                        <asp:CalendarExtender ID="TBQ004b_CalendarExtender" runat="server" TargetControlID="TBQ004b" Format="yyyy-MM-dd"></asp:CalendarExtender>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <span>完工檢查日期：</span>
                        <asp:TextBox ID="TBQ005a" runat="server" autocomplete="off"></asp:TextBox>&nbsp;&nbsp;
                        <asp:CalendarExtender ID="TBQ005a_CalendarExtender" runat="server" TargetControlID="TBQ005a" Format="yyyy-MM-dd"></asp:CalendarExtender>~
                        <asp:TextBox ID="TBQ005b" runat="server" autocomplete="off"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                        <asp:CalendarExtender ID="TBQ005b_CalendarExtender" runat="server" TargetControlID="TBQ005b" Format="yyyy-MM-dd"></asp:CalendarExtender>

                       
                  
                     <div class="inquireForm-btn">
                            <asp:Button ID="ExeQSel" runat="server" Text="查詢" OnClick="ExeQSel_Click" OnClientClick="return InputChk();"/>
                            <asp:Button ID="RemoveSel" runat="server" Text="清除" OnClick="RemoveSel_Click"/>
                     </div>
              </div>

               
                  <asp:GridView ID="GridView2" runat="server" CellSpacing="0" CellPadding="4" CssClass="ADDGridView AutoNewLine FP02 none" GridLines="None" Style="width: 100%; border-collapse: collapse; border-collapse: collapse;"
                      OnRowDataBound="GridView2_RowDataBound" AutoGenerateColumns="False" AllowSorting="true" OnSorting="GridView2_Sorting">
                      <Columns>
                          <asp:BoundField DataField="SWC002" HeaderText="水保局編號" SortExpression="SWC002" />
                          <asp:BoundField DataField="SWC012" HeaderText="轄區" SortExpression="SWC012" />
                          <asp:BoundField DataField="SWC005" HeaderText="計畫名稱" SortExpression="SWC005" />
                          <asp:BoundField DataField="SWC013" HeaderText="義務人" SortExpression="SWC013" />
                          <asp:BoundField DataField="SWC045" HeaderText="監造技師" SortExpression="SWC045" />
                            <asp:TemplateField HeaderText="檢查委員">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList><br/>
                                    <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>
                                    <asp:HiddenField ID="DDLValue" runat="server" Value='<%# Bind("CHKSETID") %>' />
                                    <asp:HiddenField ID="HDSWC024ID" runat="server" Value='<%# Bind("SWC024ID") %>' />
                                    <asp:HiddenField ID="HDSWC025" runat="server" Value='<%# Bind("SWC025") %>' />
                                    <asp:HiddenField ID="HDSWC045ID" runat="server" Value='<%# Bind("SWC045ID") %>' />
                                    <asp:HiddenField ID="HDSWC108" runat="server" Value='<%# Bind("SWC108") %>' />
                                    <asp:HiddenField ID="HDSWC013TEL" runat="server" Value='<%# Bind("SWC013TEL") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="下次檢查日期">
                                <ItemTemplate>
                                    <asp:TextBox ID="SWC114_NEW" textmode="DateTimeLocal" runat="server" style=" text-align:center;" class="FP01date" />
									<asp:HiddenField ID="HFDate" runat="server" Value='<%# Bind("SWC114") %>' />
									
                                    <asp:HiddenField ID="HFHour" runat="server" Value='<%# Bind("SWC114H") %>' />
									<asp:HiddenField ID="HFMinute" runat="server" Value='<%# Bind("SWC114M") %>' />
									<br/>~<br/>
									<asp:TextBox ID="SWC114_2NEW" textmode="DateTimeLocal" runat="server" style="text-align:center;"  class="FP01date" />
									<asp:HiddenField ID="HFDate_2" runat="server" Value='<%# Bind("SWC114_2") %>' />
									<br/>
									<asp:HiddenField ID="HFHour_2" runat="server" Value='<%# Bind("SWC114_2H") %>' />
									<asp:HiddenField ID="HFMinute_2" runat="server" Value='<%# Bind("SWC114_2M") %>' />
									<br/>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Text="寄信通知"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
							<asp:TemplateField HeaderText="下次檢查地點">
                                <ItemTemplate>
                                    <asp:TextBox ID="TBSWC121" runat="server" TextMode="MultiLine" style="width:140px; font-size:15px; height:123px; " Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SWC121"))) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="SaveRow" runat="server" CommandArgument="<%#Container.DataItemIndex %>" Text="存檔" OnClick="SaveRow_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="RowInfo" runat="server" CommandArgument="<%#Container.DataItemIndex %>" Text="詳情" OnClick="RowInfo_Click" />
                                    <asp:HiddenField ID="RSWC00" runat="server" Value='<%# Bind("SWC000") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                      </Columns>
                  </asp:GridView>

                <div class="intitle none">完工檢查列表</div>

               
                    
                  <asp:GridView ID="GridView3" runat="server" CellSpacing="0" CellPadding="4" CssClass="ADDGridView FP02 ADDother AutoNewLine none" GridLines="None" Style="width: 100%; border-collapse: collapse; border-collapse: collapse;"
                     OnRowDataBound="GridView3_RowDataBound"  AutoGenerateColumns="False" AllowSorting="true" OnSorting="GridView3_Sorting">
                      <Columns>
                          <asp:BoundField DataField="SWC002" HeaderText="水保局編號" SortExpression="SWC002" />
                          <asp:BoundField DataField="SWC012" HeaderText="轄區" SortExpression="SWC012" />
                          <asp:BoundField DataField="SWC005" HeaderText="計畫名稱" SortExpression="SWC005" />
                          <asp:BoundField DataField="SWC013" HeaderText="義務人" SortExpression="SWC013" />
                          <asp:BoundField DataField="SWC045" HeaderText="監造技師" SortExpression="SWC045" />
                            <asp:TemplateField HeaderText="檢查委員">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList><br/>
                                    <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>
                                    <asp:HiddenField ID="DDLValue" runat="server" Value='<%# Bind("CHKSETID") %>' />
                                    <asp:HiddenField ID="HDSWC024ID" runat="server" Value='<%# Bind("SWC024ID") %>' />
                                    <asp:HiddenField ID="HDSWC025" runat="server" Value='<%# Bind("SWC025") %>' />
                                    <asp:HiddenField ID="HDSWC045ID" runat="server" Value='<%# Bind("SWC045ID") %>' />
                                    <asp:HiddenField ID="HDSWC108" runat="server" Value='<%# Bind("SWC108") %>' />
                                    <asp:HiddenField ID="HDSWC013TEL" runat="server" Value='<%# Bind("SWC013TEL") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="完工檢查日期">
                                <ItemTemplate>
                                    <asp:TextBox ID="SWC115_NEW" textmode="DateTimeLocal" runat="server" style=" text-align:center;" class="FP01date" />
									<asp:HiddenField ID="HFDate_SWC115" runat="server" Value='<%# Bind("SWC115") %>' />
									
                                    <asp:HiddenField ID="HFHour_SWC115" runat="server" Value='<%# Bind("SWC115H") %>' />
									<asp:HiddenField ID="HFMinute_SWC115" runat="server" Value='<%# Bind("SWC115M") %>' />
									<br/>|<br/>
									<asp:TextBox ID="SWC115_2NEW" textmode="DateTimeLocal" runat="server" style="text-align:center;"  class="FP01date" />
									<asp:HiddenField ID="HFDate_SWC115_2" runat="server" Value='<%# Bind("SWC115_2") %>' />
									<br/>
									<asp:HiddenField ID="HFHour_SWC115_2" runat="server" Value='<%# Bind("SWC115_2H") %>' />
									<asp:HiddenField ID="HFMinute_SWC115_2" runat="server" Value='<%# Bind("SWC115_2M") %>' />
									<br/>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Text="通知寄信"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="完工檢查地點">
                                <ItemTemplate>
                                    <asp:TextBox ID="TBSWC122" runat="server" TextMode="MultiLine" style="width:140px; font-size:15px; height:123px; " Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SWC122"))) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
							<asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="SaveRow" runat="server" CommandArgument="<%#Container.DataItemIndex %>" Text="存檔" OnClick="SaveRow2_Click" />
                                    <asp:Button ID="RowInfo" runat="server" CommandArgument="<%#Container.DataItemIndex %>" Text="詳情" OnClick="RowInfo2_Click" />
                                    <asp:HiddenField ID="RSWC00" runat="server" Value='<%# Bind("SWC000") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                      </Columns>
                  </asp:GridView>
           </div>
       </div>
     </div>
	 
	 
	 	<div class="CDtitle"><img src="../images/icon/calendar.png">會勘行事曆</div>
        <div  class="rentdiv">
		 <span class="rentbox4"></span>檢查&nbsp;&nbsp;&nbsp;
         <span class="rentbox3"></span>完工
        </div>			
				<br>
				<div ng-app="myApp">
					<div ng-controller="Ctrl">
						<p></p>
						<div class="container">
							<script type="text/ng-template" id="myModalContent.html">
								<div class="modal-header">
									<button type="button" class="close" data-dismiss="modal" aria-label="Close" ng-click="cancel()"><span aria-hidden="true">&times;</span></button>
									<h3 class="modal-title">詳細資訊</h3>
								</div>
								<div class="modal-body">
									<div class="text-center well">
									<table class="wellD">
									  <tr>
									    <th><label for="nome">案件編號：</label></th>
										<td>{{    caseno   }}</td>
									  </tr>
									  <tr>
									    <th><label for="nome">案件名稱：</label></th>
										<td>{{    case   }}</td>
									  </tr>
									  <tr>
									    <th><label for="nome">義務人：</label></th>
										<td>{{    obligor   }}</td>
									  </tr>
									  <tr>
									    <th><label for="nome">行政區：</label></th>
										<td>{{    area   }}</td>
									  </tr>
									  <tr>
									    <th><label for="nome">下次檢查日期(起)：</label></th>
										<td>{{   gfExtenso    }}</td>
									  </tr>
									  <tr>
									    <th><label for="nome">下次檢查日期(迄)：</label></th>
										<td>{{   gfExtenso_end    }}</td>
									  </tr>
									  <tr>
									    <th><label for="nome">檢查地點：</label></th>
										<td>{{    place   }}</td>
									  </tr>
									  <tr>
									    <th><label for="nome">監造技師：</label></th>
										<td>{{    people   }}</td>
									  </tr>
									  <tr>
									    <th><label for="nome">檢查單位：</label></th>
										<td>{{    guild   }}</td>
									  </tr>
									</table>
									
								</div>
								<div style="display:none;">
									<div class="form-group">
										<label class="control-label">名稱</label>
										<input type="text" disabled="false" class="form-control" id="title" ng-model="nome">
									</div>
									<div class="form-group">
										<label class="control-label">時間(起)</label>
										<input type="text" disabled="false" class="form-control" id="date" ng-model="date">
									</div>
									<div class="form-group">
										<label class="control-label">時間(迄)</label>
										<input type="text" disabled="false" class="form-control" id="date_end" ng-model="date_end">
									</div>
								</div>
								<div class="modal-footer">
									<button class="btn btn-default" type="button" ng-click="cancel()">取消</button>
									<button class="btn btn-primary" type="button" onclick="login()">登入</button>
									<button class="btn btn-primary" type="button" disabled id="addgoogle" onclick="execute()">新增到google行事曆</button>
								</div>
							</script>
							<div class="alert-success calAlert ng-scope" ng-show="alertMessage != undefined &amp;&amp; alertMessage != ''">
								<h4 class="ng-binding">{{alertMessage}}</h4>
							</div>
							<div>
								<div></div>
								<div select="renderCalender('myCalendar1');">
									<div class="calendar" ng-model="eventSources" calendar="myCalendar1" ui-calendar="uiConfig.calendar"></div>
								</div>
							</div>
						</div>
						<p></p>
					</div>
				</div>
</div>
	<script type="text/javascript">
        var CLIENT_ID = '272620897647-f8ckjg7gh332v2ekrq4cc42fqcabbqfl.apps.googleusercontent.com';
        var API_KEY = 'AIzaSyCtbLShVfKYagm_4MaU8TgH6RIUdRVZtFA';

        var authParams = {
            'response_type': 'token', // Retrieves an access token only
            'client_id': CLIENT_ID, // Client ID from Cloud Console
            'immediate': false, // For the demo, force the auth window every time
            'scope': ['https://www.googleapis.com/auth/calendar https://www.googleapis.com/auth/calendar.events']  // Array of scopes
        };

        //init
        gapi.load("client:auth2", function () {
            gapi.auth2.init({ client_id: CLIENT_ID });
        });

        //登入結果,設定token
        function myCallback(authResult) {
            //if (authResult && authResult['access_token']) {
            gapi.auth.setToken(authResult);
            //console.log(authResult);
            loadClient();
            //} else {
            // Authorization failed or user declined
            //}
        }

        //連API
        function loadClient() {
            gapi.client.setApiKey(API_KEY);
            return gapi.client.load("https://content.googleapis.com/discovery/v1/apis/calendar/v3/rest")
                .then(function () { console.log("GAPI client loaded for API"); },
                    function (err) { console.error("Error loading GAPI client for API", err); });
        }

        function login() {
            gapi.auth.authorize(authParams, myCallback).then(function () {
                document.getElementById('addgoogle').disabled = false;
            });
        }

        // 新增事件
        function execute() {
            var events = [{
                'summary': document.getElementById('title').value,
                'description': document.getElementById('title').value,
                'start': {
                    'dateTime': document.getElementById('date').value,
                    'timeZone': 'Asia/Taipei'
                },
                'end': {
                    'dateTime': document.getElementById('date_end').value,
                    'timeZone': 'Asia/Taipei',
                }
            }];
            var request = gapi.client.calendar.events.insert({
                calendarId: 'primary',
                resource: events[0]
            });
            request.execute();
            alert('已新增至google行事曆');
        }
    </script>
	
    </form>
</body>
</html>

