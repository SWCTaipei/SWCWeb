<%@ Page Title="公佈欄" Language="C#" MasterPageFile="~/PriPage/MasterPageA.master" AutoEventWireup="true" CodeFile="SWCGOV013.aspx.cs" Inherits="SWCTCGOV_SWCGOV013" validateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<script>
		function chkmessage()
		{
			if(document.getElementById('<%=TXT_Message.ClientID %>').value == ""){
				alert("留言內容不可為空白!");
				return false;
			}
			return confirm('確定要送出嗎?(送出後不能修改)');
		}
		
		function textcount(txtobj, labobj, txtcount) {
            var textboxtemp = txtobj;
            var labeltemp = document.getElementById(labobj);

            if (window.event.keyCode == 13) {
                window.event.keyCode = null;
            }
            var ttval = textboxtemp.value.length;
            if (ttval > parseInt(txtcount)) {
                window.event.keyCode = null;
                textboxtemp.value = textboxtemp.value.substring(0, parseInt(txtcount));
                ttval = textboxtemp.value.length;
                labeltemp.innerText = "(" + ttval + "/" + txtcount + ")";
            }
            else {
                labeltemp.innerText = "(" + ttval + "/" + txtcount + ")";
            }
        }
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<asp:ScriptManager ID="ScriptManager1" runat="server" />	
        <div class="content-s">
            <div class="detailsMenu"><asp:Image ID="Image1" runat="server" ImageUrl="../images/title/bbd.png" /></div>
			<span style="float:right;">
				觀看人次：<asp:Label ID="LBViewers" runat="server" />人　我要按讚<asp:ImageButton ID="Btn_Like" runat="server" width="20px" OnClick="Btn_Like_Click" />&nbsp;&nbsp;<asp:Label ID="lb_likecount" runat="server" style="color:#3E61A6;" Text="0"/>&nbsp;個讚
			</span>
			<br/>
			<br/>
            
			 <div class="detailsGrid"><div class="detailsGrid_wrap">
                <h2 class="detailsBar_title_basic">公佈欄</h2>
          
                <table class="detailsGrid_skyblue">
                <tr><td>流水號</td>
                    <td><asp:Label ID="TXTBBNO" runat="server" /></td></tr>
                <tr><td>公告日期</td>
                    <td><asp:Label ID="TXTBBDateStart" runat="server" Style="margin-right: 0px;" autocomplete="off"></asp:Label> ~                            
                        <asp:Label ID="TXTBBDateEnd" runat="server" Style="margin-right: 0px;" autocomplete="off"></asp:Label></td></tr>
                <tr><td>公告主旨</td>
                    <td><asp:Label ID="TXTBBTitle" runat="server" Style="margin-right: 0px; width:100%;" autocomplete="off" MaxLength="100"/></td></tr>
                <tr><td style="vertical-align:middle">公告內容</td>
                    <td><asp:Label ID="TXTBBText" runat="server" TextMode="MultiLine" Height="100" Width="100%" /></td></tr>
                <tr><td style="vertical-align:middle">附件檔案</td>
                    <td><asp:HyperLink ID="LinkFile" runat="server" Target="_blank" /></td></tr>
                <tr><td>公佈單位</td>
                    <td><asp:Label ID="DDLBBUnit" runat="server" /></td></tr>
                </table>
			</div>
		</div>
			<br>
			<div>
				<div  class="lookCDmessage"><a href="">我要留言</a></div><br><br><br>
				<div style="clear:both"></div><br>
				<asp:UpdatePanel ID="GoodPanel" UpdateMode="Conditional" runat="server">
					<ContentTemplate>
						<div>
							<b style="font-weight:bold; font-size:18px;">姓名：</b><asp:TextBox ID="TXT_Name" runat="server" placeholder="若無輸入以匿名顯示" /><br/><br/>
							<asp:TextBox ID="TXT_Message" runat="server" placeholder="我要留言" TextMode="MultiLine" width="100%" height="82px" onkeyup="textcount(this,'ContentPlaceHolder1_TXT_Message_count','300');" /><br/>
                            <asp:Label ID="TXT_Message_count" runat="server" Text="(0/300)" ForeColor="Red" /></td></tr>
							<br/><br/>
							<asp:Button ID="BTN_SendMessage" runat="server" text="送出留言" OnClick="BTN_SendMessage_Click" class="sendms" OnClientClick="return chkmessage()" />
							<br/><br/>
							<span><img src="../images/icon/comment.png" width="30px" style="margin-bottom:-10px;">&nbsp;&nbsp;<asp:Label ID="lb_messagecount" runat="server" Text="0" />個留言</span>
							<table id="TBSearchList" runat="server" width="100%" class="commentlist" style="display:none;" >
								
							</table>
							
							<asp:GridView ID="GVBILLBOARDLIST" runat="server" DataSourceID="SqlDataSource" CssClass="commentlist AutoNewLine" PagerStyle-CssClass="listpgr"
								EmptyDataText="沒有符合查詢條件的資料" AllowPaging="true" PageSize="20" AllowSorting="True" ShowHeader="False"
								AutoGenerateColumns="false" OnDataBound="GVBILLBOARDLIST_DataBound">
								<Columns>
									<asp:TemplateField>
										<ItemTemplate>
											<asp:Label ID="BBWHO" runat="server" Text='<%# Server.HtmlEncode(Eval("BBWHO").ToString()) %>' />
											<asp:Label ID="BBDATETIME" runat="server" Text='<%# Eval("BBDATETIME") %>' />
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField>
										<ItemTemplate>
											<asp:TextBox ID="BBMESSAGE" runat="server" Text='<%# Server.HtmlEncode(Eval("BBMESSAGE").ToString()) %>' />
										</ItemTemplate>
									</asp:TemplateField>
								</Columns>
								<PagerSettings LastPageText="最後一頁" FirstPageText="第一頁" Mode="NumericFirstLast" NextPageText="下一頁" PreviousPageText="上一頁"  />
								<PagerStyle  CssClass="listpgr" />
							</asp:GridView>
							<asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SWCConnStr %>" 
								SelectCommand=" select * from (select BBWHO,convert(varchar, BBDATETIME, 120) BBDATETIME,BBMESSAGE from BillBoardMessage where BBNO = @BBNO union all select BBWHO,convert(varchar, BBDATETIME, 120) BBDATETIME,BBMESSAGEBBMESSAGE from BillBoardMessage where BBNO = @BBNO) A ORDER BY [BBDateTime] DESC ">
								<SelectParameters>
									<asp:ControlParameter ControlID="TXTBBNO" Name="BBNO" PropertyName="Text" Type="String" />
								</SelectParameters>
							</asp:SqlDataSource>
						</div>
					</ContentTemplate>
				</asp:UpdatePanel>
				
	
			</div>
			<div class="btncenter">
				<input onclick="window.close();" value="關閉" type="button">
			</div>
		</div>
</asp:Content>

