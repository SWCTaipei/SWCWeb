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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineApply001L.aspx.cs" Inherits="SWCDOC_OnlineApply009" %>
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
    <link rel="stylesheet" type="text/css" href="../css/OnlineApply001.css"/>
    <link rel="stylesheet" type="text/css" href="../css/iris.css"/>
    
    <script type="text/javascript">
        function chkInput(jChkType) {

            if (jChkType == 'DataLock') {
                var r = confirm('確認送出後，即不可修改，請再次確認是否要完成送出。');
                return r;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
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
                        <asp:Panel ID="TitleLink00" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWCBase001.aspx" title="帳號管理">帳號管理</a></li></asp:Panel>
                        <asp:Panel ID="LogOutLink" runat="server" Visible="true"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li></asp:Panel>
                    </ul>
                </div>
            </div>

            <div class="header-s-green">
                <div class="header-s-green-nameWrap">
                    <span><asp:Literal ID="TextUserName" runat="server">大雷包，您好</asp:Literal></span>
                </div>
            </div>
        </div>
        
        <div class="content-s">
            <div class="startReport">

				<div style="font-size:30px; font-weight: bold; text-align:center;">臺北市山坡地水土保持設施安全自主檢查表</div>

               
                <div class="content-s content-s-inquire">
                    <div class="inquireForm2 inquireForm_iris">
                        <span>安全自主檢查表編號：</span>
                        <asp:TextBox ID="TXTS001" runat="server" MaxLength="30"/>
                        <%--<br/>--%>
                        <span>水保局編號：</span>
                        <asp:TextBox ID="TXTS002" runat="server" MaxLength="30"/>
                        <%--<br/>--%>
                        <span>檢查日期：</span>
                        <asp:TextBox ID="TXTS003a" runat="server" width="120px"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTS003a_CalendarExtender" runat="server" TargetControlID="TXTS003a" Format="yyyy-MM-dd"></asp:CalendarExtender>
                        ~
                        <asp:TextBox ID="TXTS003b" runat="server" width="120px"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTS003b_CalendarExtender" runat="server" TargetControlID="TXTS003b" Format="yyyy-MM-dd"></asp:CalendarExtender>
                        <br/>
                        <span>義務人(聯絡人)：</span>
                        <asp:TextBox ID="TXTS004" runat="server" MaxLength="30"/>
                        <%--<br/>--%>
                        <span>行動電話：</span>
                        <asp:TextBox ID="TXTS005" runat="server" MaxLength="30"/>
                        <%--<br/>--%>
                        <span>聯絡電話：</span>
                        <asp:TextBox ID="TXTS006" runat="server" MaxLength="30"/>
                        <br/>
                        <span>社區(設施)地址：</span>
                        <asp:TextBox ID="TXTS007" runat="server" MaxLength="30" Width="440px"/>
                        <%--<br/>--%>
                        <span>聯絡地址：</span>
                        <asp:TextBox ID="TXTS008" runat="server" MaxLength="30" Width="440px"/>
                        <br/>

                        <div class="inquireForm2-btn">
                            <asp:Button ID="ExeQSel" runat="server" Text="查詢" OnClick="ExeQSel_Click"/>
                            <asp:Button ID="RemoveSel" runat="server" Text="清除" OnClick="RemoveSel_Click" />
                        </div>
                    </div>

            <div class="inquireGrid">
              <div class="inquireGrid-menu">
                <h3>查詢到件數:<asp:Label ID="CaseCount" runat="server" Text="" />筆</h3>
              </div>


                <asp:GridView ID="GVSWCList" runat="server" DataSourceID="SqlDataSource" CssClass="ApplyL" PagerStyle-CssClass="pgr"
                    OnRowCommand="GVSWCList_RowCommand" OnPageIndexChanging="GVSWCList_PageIndexChanging" OnSorting="GVSWCList_Sorting"
                    EmptyDataText="沒有符合查詢條件的資料" AllowPaging="true" PageSize="20" AllowSorting="True"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="ONA01001" HeaderText="安全自主檢查表編號" SortExpression="ONA01001" ItemStyle-Width="13%" />
                        <asp:BoundField DataField="SWC002" HeaderText="水保局編號" SortExpression="SWC002" />
                        <asp:BoundField DataField="SWC005" HeaderText="水土保持申請書件名稱" SortExpression="SWC002" />
                        <asp:BoundField DataField="ONA01002" HeaderText="檢查日期" SortExpression="SWC002"/>
                        <asp:BoundField DataField="ONA01004" HeaderText="義務人(聯絡人)" SortExpression="SWC088" />
                        <asp:ButtonField ButtonType="Button" CommandName="detail" Text="詳情"  />
                    </Columns>
                    <PagerSettings LastPageText="最後一頁" FirstPageText="第一頁" Mode="NumericFirstLast" NextPageText="下一頁" PreviousPageText="上一頁"  />
                    <PagerStyle CssClass="AAA " BackColor="#7baac5"/>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SWCConnStr %>" OnSelected="SqlDataSource_Selected"
                    SelectCommand="select ONA.id,ONA.ONA01001,ONA.SWC002,SWC.SWC005,left(convert(char, ONA.ONA01002, 120),10) as ONA01002,ONA01004 from OnlineApply01 ONA LEFT JOIN SWCCASE SWC ON ONA.SWC002=SWC.SWC002 ORDER BY [id] "
                    DeleteCommand="DELETE FROM [SWCCASE] WHERE [SWC000] = @SWC000">
                    <DeleteParameters>
                        <asp:Parameter Name="SWC000" Type="String" />
                    </DeleteParameters>
                </asp:SqlDataSource>
            </div>
        </div>












        </div>


            
        </div>




            <div class="footer">
                 <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                           <span class="span2">110臺北市信義區松德路300號3樓 　服務專線(02)27591109   臺北市民當家熱線1999</span><br/>
                            <span class="span2">建議使用IE11(含)以上，Chrome或Firefox版本瀏覽器 資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                           <span class="span2">客服電話：02-27593001#3718 許先生 本系統由多維空間資訊有限公司開發維護 TEL:(02)27929328</span></p>
            </div>


        <asp:Literal ID="error_msg" runat="server"></asp:Literal>































        <script src="../js/jquery-3.1.1.min.js"></script>
        <script src="../js/inner.js"></script>
        <script src="../js/allhref.js"></script>
    </div>
    </form>
</body>
</html>
