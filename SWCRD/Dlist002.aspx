<%@ Page Title="待辦報表" Language="C#" MasterPageFile="~/PriPage/MasterPageA.master" AutoEventWireup="true" CodeFile="Dlist002.aspx.cs" Inherits="SWCRD_Dlist002" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-s">
        <div class="detailsMenu">
            <img src="../images/title/upcoming-15.jpg" />
        </div>
        
        <div class="linkwrap">
            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/SWCRD/Dlist001.aspx" CssClass="linkstyle2">暫&nbsp;&nbsp;存&nbsp;&nbsp;中</asp:HyperLink>
            <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/SWCRD/Dlist002.aspx" CssClass="linkstyle1">近期報表</asp:HyperLink>

       

       <div class="tab">
        <div class="intitle">近期報表&nbsp;&nbsp;&nbsp;<span class="red">預設帶入決行日+7個日曆天之案件，超過請返回原案檢視</span></div>   
           
                <asp:GridView ID="GVSWCList" runat="server" DataSourceID="SqlDataSource" CssClass="ADDGridView AutoNewLine" PagerStyle-CssClass="pgr"
                    OnRowCommand="GVSWCList_RowCommand" OnRowDataBound="GVSWCList_RowDataBound" OnSorting="GVSWCList_Sorting"
                    EmptyDataText="沒有符合查詢條件的資料" AllowSorting="True"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="" HeaderText="" SortExpression=""></asp:BoundField>
                        <asp:BoundField DataField="SWC000" HeaderText="案件編號" SortExpression="SWC000" ItemStyle-Width="200px"/>
                        <asp:BoundField DataField="SWC002" HeaderText="水保局編號" SortExpression="SWC002" ItemStyle-Width="170px"/>
                        <asp:BoundField DataField="SWC004" HeaderText="案件狀態" SortExpression="SWC004" ItemStyle-Width="130px"/>
                        <asp:BoundField DataField="SWC005" HeaderText="水土保持申請書件名稱" SortExpression="SWC005"  ItemStyle-Width="350px"/>
                        <asp:BoundField DataField="SWC088" HeaderText="審查期限" SortExpression="SWC088" ItemStyle-Width="120px"/>
                        <asp:BoundField DataField="SWC013" HeaderText="義務人" SortExpression="SWC013"  ItemStyle-Width="120px"/>    
                        <asp:ButtonField ButtonType="Button" CommandName="detail" Text="詳情"/>
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:Button ID="BtnEdit" runat="server" CommandArgument='<%# Eval("SWC000") %>' OnClick="BtnEdit_Click" Text="編輯" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                    </Columns>
                    <PagerSettings LastPageText="最後一頁" FirstPageText="第一頁" Mode="NumericFirstLast" NextPageText="下一頁" PreviousPageText="上一頁"  />
                    <PagerStyle  CssClass="pgr"  BackColor="#7baac5" Width="100%"/>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SWCConnStr %>"
                    SelectCommand="SELECT * FROM [SWCCASE] ORDER BY [SWC000] DESC"
                    DeleteCommand="DELETE FROM [SWCCASE] WHERE [SWC000] = @SWC000">
                    <DeleteParameters>
                        <asp:Parameter Name="SWC000" Type="String" />
                    </DeleteParameters>
                </asp:SqlDataSource>


       </div>
                    
     </div>

  
  

        
</div>

    <div class="btncenter">
        <a href="../SWCDOC/HaloPage001.aspx"><input type="button" value="返回我的案件列表" /></a>
    </div>








</asp:Content>

