<!--
    Soil and Water Conservation Platform Project is a web applicant tracking system which allows citizen can search, view and manage their SWC applicant case.
    Copyright (C) <2020>  <Geotechnical Engineering Office, Public Works Department, Taipei City Government>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or any later version.
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
-->

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCGOV001.aspx.cs" Inherits="SWCTCGOV_SWCGOV001" %>

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
            <div class="header header-s clearfix"><a href="SWC001.aspx" class="logo-s"></a>
                <div class="header-menu-s">
                <ul>
                    <li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
                    <li>|</li>
                    <li><a href="http://tcgeswc.taipei.gov.tw/index_new.aspx" title="水土保持計畫查詢系統" target="_blank">水土保持計畫查詢系統 </a></li>
                    <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="http://172.28.100.55/TSLM" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
                    <asp:Panel ID="TitleLink00" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="../SWCDOC/SWCBase001.aspx" title="帳號管理">帳號管理</a></li></asp:Panel>
                    <asp:Panel ID="GOVMG" runat="server" Visible="false"><li class="system">|&nbsp&nbsp&nbsp&nbsp<a href="../SWCTCGOV/SWCGOV001.aspx" title="系統管理">系統管理</a><ul><li><a href="../SWCTCGOV/SWCGOV001.aspx">防災事件通知</a></li><li><a href="../SWCTCGOV/SWCGOV011.aspx">公佈欄</a></li></ul></li></asp:Panel>
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
                       
        <span>通知事件：</span>
        <select name="" >
	            <option selected="selected" value=""></option>
	            <option value="16">颱風豪雨通知回傳自主檢查表</option>
	            <option value="15">颱風豪雨通知回傳自主檢查表</option>
	            <option value="14">颱風豪雨通知回傳自主檢查表</option>
	            <option value="10">颱風豪雨通知回傳自主檢查表</option>
	            <option value="03">颱風豪雨通知回傳自主檢查表</option>
	            <option value="17">颱風豪雨通知回傳自主檢查表</option>
        </select>
       <span style="margin-left:2em;">案件狀態：</span>
        <select name="" >
	            <option selected="selected" value=""></option>
	            <option value="16">施工中</option>
	            <option value="15">施工中</option>
	            <option value="14">施工中</option>
	            <option value="10">施工中</option>
	            <option value="03">施工中</option>
	            <option value="17">施工中</option>
        </select>
       
   
   <span style="margin-left:2em;">通知對象：</span>
               <input id="" type="checkbox" name=""><label >義務人</label>
               <input id="" type="checkbox" name=""><label >承辦技師</label>
               <input id="" type="checkbox" name=""><label >審查單位</label>
               <input id="" type="checkbox" name=""><label >監造技師</label>
               <input id="" type="checkbox" name=""><label >檢查單位</label><br />
       
    <span>內容查詢:</span><input type="text" />
    <span>事件名稱:</span><input type="text" />
    <span>發送日期:</span><input type="text" />~<input type="text" /><br />
    
    <span>通知管道：</span>
               <input id="" type="checkbox" name=""><label >簡訊</label>
               <input id="" type="checkbox" name=""><label >E-mail</label>
         
             
       
     <div class="inquireForm-btn">
               <input type="submit" name="" value="查詢" >
               <input type="submit" name="" value="清除">
     </div>
    </div>

    <div style="clear:both">
    <br />
   

         
            <div class="GOV001">
              <div class="inquireGrid-menu" style="margin-left:22em;">
                <h3>查詢到件數:<asp:Label ID="CaseCount" runat="server" Text="" />筆</h3>
                  <asp:ImageButton ID="NewSwc" runat="server" OnClick="NewSwc_Click" title="新增案件" ImageUrl="../images/btn/btn-addCase.png" />
                  <asp:ImageButton ID="WriteExcel" runat="server" title="輸出Excel" ImageUrl="../images/btn/btn-excel.png" OnClientClick="return confirm('確認是否要輸出Excel？')"  />
              </div>



                <%--OnRowCommand="GVSWCList_RowCommand"  OnPageIndexChanging="GVSWCList_PageIndexChanging" OnSorting="GVSWCList_Sorting"--%>



                
                <asp:GridView ID="GVSWCList" runat="server" DataSourceID="SqlDataSource" CssClass="ADDGridView AutoNewLine" PagerStyle-CssClass="pgr"
                    OnRowDataBound="GVSWCList_RowDataBound"
                    EmptyDataText="沒有符合查詢條件的資料" AllowPaging="true" PageSize="20" AllowSorting="True"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="" HeaderText="" SortExpression=""></asp:BoundField>
                        <asp:BoundField DataField="DENO" HeaderText="流水號" SortExpression="DENO" ItemStyle-Width="150px"/>
                        <asp:BoundField DataField="DENAME" HeaderText="事件名稱" SortExpression="DENO" ItemStyle-Width="220px"/>
                        <asp:BoundField DataField="DEDATE" HeaderText="發送日期" SortExpression="DENO" ItemStyle-Width="130px"/>
                        <asp:BoundField DataField="DESENDCOUNT" HeaderText="發送筆數" SortExpression="DENO"  ItemStyle-Width="350px"/>
                        <asp:BoundField DataField="DEINPUTCOUNT" HeaderText="已登入筆數" SortExpression="DENO" ItemStyle-Width="120px"/>
                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:Button ID="BtnView" runat="server" CommandArgument='<%# Eval("DENO") %>' Text="詳情" OnClick="BtnView_Click" Visible="false" />
                                <asp:Button ID="BtnEdit" runat="server" CommandArgument='<%# Eval("DENO") %>' Text="編輯" OnClick="BtnEdit_Click" Visible="false" />
                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("DESENT") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:Button ID="BtnDelete" runat="server" CommandArgument='<%# Eval("DENO") %>' Text="刪除" Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings LastPageText="最後一頁" FirstPageText="第一頁" Mode="NumericFirstLast" NextPageText="下一頁" PreviousPageText="上一頁"  />
                    <PagerStyle  CssClass="pgr"  BackColor="#7baac5" Width="100%"/>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SWCConnStr %>"
                    SelectCommand=" select * from DisasterEvent ORDER BY [DENO] DESC ">
                </asp:SqlDataSource>

                </div>
          </div>
        </div>
            <br/><br/><br/>
        
            <div class="footer">
                <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                    <span class="span2">110臺北市信義區松德路300號3樓 　服務專線(02)27591109   臺北市民當家熱線1999</span><br/>
                    <span class="span2">建議使用IE11(含)以上，Chrome或Firefox版本瀏覽器 資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                    <span class="span2">客服電話：02-27593001#3718 許先生 本系統由多維空間資訊有限公司開發維護 TEL:(02)27929328</span></p>
            </div>
       

    </form>
</body>
</html>
