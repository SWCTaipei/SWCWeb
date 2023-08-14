<%@ Page Title="委託歷程一覽表" Language="C#" MasterPageFile="~/PriPage/MasterPageA.master" AutoEventWireup="true" CodeFile="shiftsList002.aspx.cs" Inherits="SWCRD_shiftsList001" %>
   
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" rel="stylesheet" href="css/all.css?202006020242">
<link type="text/css" rel="stylesheet" href="css/table.css">
<link type="text/css" rel="stylesheet" href="css/iris.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-s"><!--<div class="txttitle">輪值歷程</div>-->
        <div><img src="../images/btn/btn-12.jpg" /></div><br />
         <!--<div style="text-align:center;"><h1>臺北市水土保持申請案件委託審查/檢查單位歷程一覽表</h1></div>-->
            <div class="linkwrap">
                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/SWCRD/shiftsList001.aspx" CssClass="linkstyle2">&nbsp;&nbsp;&nbsp;審&nbsp;&nbsp;&nbsp;查&nbsp;&nbsp;&nbsp;</asp:HyperLink>
                <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/SWCRD/shiftsList002.aspx" CssClass="linkstyle1">&nbsp;&nbsp;&nbsp;檢&nbsp;&nbsp;&nbsp;查&nbsp;&nbsp;&nbsp;</asp:HyperLink>
            </div>


    














    

        
         
         <div class="tab-frame">
           <!--<input type="radio" checked="" name="tab" id="tab1">
           <label for="tab1">&nbsp;&nbsp;&nbsp;審&nbsp;&nbsp;&nbsp;查&nbsp;&nbsp;&nbsp;</label>
           <input type="radio" name="tab" id="tab2">
           <label for="tab2">&nbsp;&nbsp;&nbsp;檢&nbsp;&nbsp;&nbsp;查&nbsp;&nbsp;&nbsp;</label>-->
             
         
         <div class="tab9">
            <div><!--以下藍色表格條件為:施工中且檢查公會為該帳號-->
          <span class="intitle">目前待派檢查單位順序</span>
                
                    <asp:GridView ID="GridView1" runat="server" Cellspacing="0" CellPadding="4" CssClass="ADDGridView AutoNewLine" GridLines="None" style="width:100%;border-collapse:collapse;border-collapse:collapse;"
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="ROWID" HeaderText="輪值順位" SortExpression="ROWID" />
                            <asp:BoundField DataField="GS003" HeaderText="檢查單位" SortExpression="GS003" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:TSLMSWCCONN %>"
                        SelectCommand=" select top 4 RANK() OVER(ORDER BY isnull(GS005, 0) + isnull(GS006, 0)+isnull(GS007, 0),isnull(GS004, 0)) AS ROWID,* from GuildShifts where GS001='2' and GS002<>'ge-50702' and isnull(GS009,'1')='1' order by isnull(GS005, 0) + isnull(GS006, 0)+isnull(GS007, 0),isnull(GS004, 0); " >
                    </asp:SqlDataSource> </div>

        </div>
        <br />
        <div><!--以下綠色表格條件為:施工中且檢查公會為該帳+完工申報書決行且核准的-->
          <span class="intitle">已委派案件之檢查單位（顯示近10筆）</span><br />
        
                  
              <asp:GridView ID="GridView2" runat="server" CssClass="ADDGridView AutoNewLine ADDother" style="width:100%;border-collapse:collapse;border-collapse:collapse;"
                        AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                        <Columns>
                           
                            <asp:BoundField DataField="SWC02" HeaderText="行政審查案件編號" SortExpression="SWC02"  >
                             <ItemStyle Width="175px" />
                            </asp:BoundField>
                             <asp:BoundField DataField="SWC05" HeaderText="水土保持申請書件名稱" SortExpression="SWC05" >
                          
                             <ItemStyle Width="450px" />
                            </asp:BoundField>
                          
                             <asp:BoundField DataField="SWC24" HeaderText="檢查單位" SortExpression="SWC24" >
                          
                       
                            <ItemStyle Width="290px" />
                            </asp:BoundField>
                          
                       
                            <asp:BoundField DataField="GSL002" HeaderText="指派原因" SortExpression="GSL002" />
                            <asp:BoundField DataField="GSL003" HeaderText="委派日期" SortExpression="GSL003" >
                          
                       
                            <ItemStyle Width="110px" />
                            </asp:BoundField>
                          
                       
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TSLMSWCCONN %>"
                        SelectCommand="" >
                    </asp:SqlDataSource> 





        <!--
           <table class="ADDGridView AutoNewLine ADDother" >
                   <tbody>
                   <tr>
                       <th>行政審查案件編號</th>
                       <th style="width:40%;">水土保持申請書件名稱</th>
                       <th>審查單位</th>
                       <th>指派原因</th>
                       <th>受理日期</th>
                   </tr>
                   <tr>
                       <td>UA1410810001</td>
                       <td>臺北市內湖區碧山段二小段696、696-1、696-2、696-3、696-4地號等5筆土地(露營場專用區)碧山露營場管理室新建工程水土保持計畫</td>
                       <td>社團法人新北市水土保持技師公會</td>
                       <td>自動輪派</td>
                       <td>2019-10-22</td>
                   </tr>
                   <tr>
                       <td>UA1510812001</td>
                       <td>臺北市士林區華岡段一小段178地號一筆土地(特定住宅區(二))建築新建工程水土保持計畫</td>
                       <td>中原大學</td>
                       <td>自動輪派</td>
                       <td>2020-02-14</td>
                   </tr>
                   <tr>
                       <td>UA1510901002</td>
                       <td>臺北市士林區天玉段一小段61、61-13地號2筆土地(保護區)申請私設通路水土保持計畫</td>
                       <td>社團法人台灣省水利技師公會</td>
                       <td>自動輪派</td>
                       <td>2020-02-06</td>
                   </tr>
                   <tr>
                       <td>TT9910812028</td>
                       <td>臺北市士林區華岡段四小段309地號及311地號(部份)(保護區)農路及園內道新建工程簡易水土保持申報書</td>
                       <td>高雄市土木技師公會</td>
                       <td>自動輪派</td>
                       <td>2020-03-12</td>
                   </tr>
                   <tr>
                       <td>UA1510902001</td>
                       <td>臺北市士林區新安段六小段2地號等38筆土地(公共設施用地)住六-六自辦市地重劃污水管線工程水土保持計畫</td>
                       <td>社團法人臺北市水土保持技師公會</td>
                       <td>自動輪派</td>
                       <td>2019-02-26</td>
                   </tr>
                   <tr>
                       <td>UA1610902002</td>
                       <td>臺北市北投區秀山段三小段 306、315、316地號等3筆土地住宅新建工程(住宅區)水土保持計畫</td>
                       <td>社團法人台灣坡地防災學會</td>
                       <td>自動輪派</td>
                       <td>2020-02-27</td>
                   </tr>
                   <tr>
                       <td>UA1110902001</td>
                       <td>臺北市文山區萬芳段一小段146地號(第参類住宅用地)集合住宅大樓新建工程水土保持計畫</td>
                       <td>社團法人臺灣省水土保持技師公會</td>
                       <td>自動輪派</td>
                       <td>2020-02-27</td>
                   </tr>
                   <tr>
                       <td>UA1510903001</td>
                       <td>台北市士林區力行段二小段95、95-2、95-5、95-8、95-9、99、99-1、100-4等8筆地號土地(皆為部分使用)–陽明山國家公園松園遊憩區(遊十二)開發經營計畫水土保持計畫</td>
                       <td>社團法人中華水土保持學會</td>
                       <td>自動輪派</td>
                       <td>2020-03-11</td>
                   </tr>
                   <tr>
                       <td>UA1410703002-1</td>
                       <td>臺北市內湖區碧湖段三小段339、339-5、346、347-1地號等4筆土地（第二種住宅區）集合住宅新建工程水土保持計畫第一次變更設計</td>
                       <td>社團法人新北市土木技師公會</td>
                       <td>變更設計</td>
                       <td>2020-03-12</td>
                   </tr>
                   <tr>
                       <td>UA1510603001-1</td>
                       <td>臺北市士林區至善段四小段191-4地號（使用分區：保護區）住宅改建工程水土保持計畫(第一次變更設計)</td>
                       <td>社團法人臺北市水土保持技師公會</td>
                       <td>變更設計</td>
                       <td>2020-03-06</td>
                   </tr>
                </tbody></table>-->
        </div>    
         
     </div>    
         
         
         
       <br /><br /><br /><br /><br />
        
    </div><!--content-s-->
</asp:Content>

