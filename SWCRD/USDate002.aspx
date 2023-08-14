<%@ Page Title="義務人基本資料" Language="C#" MasterPageFile="~/PriPage/MasterPageA.master" AutoEventWireup="true" CodeFile="USDate002.aspx.cs" Inherits="SWCRD_USDate002" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<script type="text/javascript">
	</script>
<style>
  legend{
	  font-size:19px;
	  font-weight:bold;
	  padding:0 15px;
	  color:rgba(115,115,115,1.00);
	  margin-bottom:0px;
	  font-size:28px;
	  margin-left:50px;
 
  }
  fieldset {
    display: block;
    margin-inline-start: 2px;
    margin-inline-end: 2px;
    padding-block-start: 0.35em;
    padding-inline-start: 0.75em;
    padding-inline-end: 0.75em;
    padding-block-end: 0.625em;
    min-inline-size: min-content;
    border-width: 2px;
    border-style: groove;
    border-color: rgb(192, 192, 192);
    border-image: initial;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<div class="content-s">
		<div class="detailsMenu"><img id="ContentPlaceHolder1_Image1" src="../images/title/dataupdate-14.jpg" /></div>
		<div class="applyGrid none">
			<h2 class="detailsBar_title_basic">基本資料</h2>
            <table class="ph-table">
				<tbody>
					<tr>
                        <th style="width:20%;">姓名</th>
                        <td >
							<asp:TextBox runat="server" ID="TBName" />
						</td>
					</tr>
                    <tr> 
                        <th>手機</th>
                        <td>
							<asp:TextBox runat="server" ID="TBCellPhone" />
						</td>
                    </tr>
                    <tr>
                        <th>信箱</th>
                        <td>
							<asp:TextBox runat="server" ID="TBEmail" />
						</td>
                    </tr>
                </tbody>
			</table>
		</div>
		<asp:Panel runat="server" ID="PanelTable" />
	</div>
</asp:Content>

