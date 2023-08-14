<%@ Page Title="設施維護檢查及輔導紀錄表" Language="C#" MasterPageFile="~/PriPage/MasterPageA.master" AutoEventWireup="true" CodeFile="SWCDT007L.aspx.cs" Inherits="SWCAPPLY_SWCDT007L" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-s">
        <div class="startReport">
            <div style="font-size:30px; font-weight: bold; text-align:center;">臺北市山坡地水土保持設施維護檢查及輔導紀錄表</div>
                <div class="content-s content-s-inquire">
                    <div class="inquireForm2 inquireForm_iris">
                        <span>設施維護檢查表編號：</span>
                        <asp:TextBox ID="TXTS001" runat="server" MaxLength="30"/>&nbsp;&nbsp;&nbsp;
                        
                        <span>水保局編號：</span>
                        <asp:TextBox ID="TXTS002" runat="server" MaxLength="30"/>&nbsp;&nbsp;&nbsp;
                        
                        <span>水土保持申請書件名稱：</span>
                        <asp:TextBox ID="TXTS003" runat="server" MaxLength="50"/>&nbsp;&nbsp;&nbsp;&nbsp;
                        <!--<span>檢查日期：</span>
                        <input name="TXTS003a" type="text" id="TXTS003a" style="width:120px;">&nbsp;&nbsp;&nbsp;
                        ~
                        <input name="TXTS003b" type="text" id="TXTS003b" style="width:120px;">-->
                        &nbsp;&nbsp;&nbsp;
                        <br>
                        <span>義務人：</span>
                        <asp:TextBox ID="TXTS004" runat="server" MaxLength="50"/>&nbsp;&nbsp;&nbsp;&nbsp;
                        
                        <span style="display:none;">現行水土保持義務人：</span>
                        <asp:TextBox ID="TXTS005" runat="server" MaxLength="50" Visible="false"/>&nbsp;&nbsp;&nbsp;&nbsp;
                        
                        <span>水保設施概要：</span>
                        <asp:CheckBox ID="CheckBox1" runat="server" /> 排水設施 &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="CheckBox2" runat="server" /> 擋土設施&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="CheckBox3" runat="server" /> 滯洪沉砂設施
                       
                        <br />
                        
                        <span>滯洪沉砂池：</span>
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="有滯洪沉砂設施">有滯洪沉砂設施</asp:ListItem>
                            <asp:ListItem Value="無滯洪沉砂設施">無滯洪沉砂設施</asp:ListItem>
                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                        
                        <span>排水設施：</span>
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="有排水設施">有排水設施</asp:ListItem>
                            <asp:ListItem Value="無排水設施">無排水設施</asp:ListItem>
                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                        
                        <span>邊坡保護設施：</span>
                        <asp:DropDownList ID="DropDownList3" runat="server">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="有邊坡保護設施">有邊坡保護設施</asp:ListItem>
                            <asp:ListItem Value="無邊坡保護設施">無邊坡保護設施</asp:ListItem>
                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                        
                        <br />
                        
                        
                        <div class="inquireForm2-btn">
                            <asp:Button ID="ExeQSel" runat="server" Text="查詢" OnClick="ExeQSel_Click" />
                            <asp:Button ID="RemoveSel" runat="server" Text="清除" OnClick="RemoveSel_Click" />
                        </div>
                    </div>

            <div class="inquireGrid">
              <div class="inquireGrid-menu">
                <h3>查詢到件數：<asp:Label ID="CaseCount" runat="server" Text="" />筆</h3>
                  <asp:ImageButton ID="WriteExcel" runat="server" OnClick="WriteExcel_Click" title="輸出Excel" ImageUrl="../images/btn/btn-excel.png" OnClientClick="return confirm('確認是否要輸出Excel？')"  />
                  <asp:ImageButton ID="WriteOds" runat="server" OnClick="WriteOds_Click" title="輸出ods" ImageUrl="../images/btn/btn-ods.png" />
              </div>


                <div>
                    <asp:GridView ID="GridView1" runat="server" CssClass="ApplyL" PagerStyle-CssClass="pgr"
                        AllowPaging="true" PageSize="20" AllowSorting="True"
                        OnRowCommand="GVSWCList_RowCommand" OnPageIndexChanging="GridView1_PageIndexChanging" OnPageIndexChanged="GridView1_PageIndexChanged" OnSorting="GridView1_Sorting" 
                        EmptyDataText="沒有符合查詢條件的資料"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="FD001" HeaderText="設施維護檢表編號" SortExpression="FD001" ItemStyle-Width="13%" />
                            <asp:BoundField DataField="FD004" HeaderText="水保局編號" SortExpression="FD004" />
                            <asp:BoundField DataField="FD005" HeaderText="水土保持申請書件名稱" SortExpression="FD005" />
                            <asp:BoundField DataField="FD002" HeaderText="檢查日期" SortExpression="FD002" />
                            <asp:BoundField DataField="FD003" HeaderText="義務人" SortExpression="FD003" />
                            <asp:ButtonField ButtonType="Button" CommandName="detail" Text="詳情" />
                        </Columns>
                        <PagerSettings LastPageText="最後一頁" FirstPageText="第一頁" Mode="NumericFirstLast" NextPageText="下一頁" PreviousPageText="上一頁" />
                        <PagerStyle CssClass="AAA " BackColor="#7baac5" />
                    </asp:GridView>





























</div>
                
            </div>
        </div>












        </div>


            
        </div>






















</asp:Content>

