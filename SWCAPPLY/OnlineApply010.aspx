<%@ Page Title="恢復審查" Language="C#" MasterPageFile="~/PriPage/MasterPageA.master" AutoEventWireup="true" CodeFile="OnlineApply010.aspx.cs" Inherits="SWCAPPLY_OnlineApply010" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-s">
        <div class="txttitle">恢復審查</div>

        <div>
        <table class="resend"><tbody>
        <tr><th>水保局編號</th>
            <td><asp:Label ID="LBSWC000" runat="server" Text="" Visible="false" />
                <asp:Label ID="LBSWC002" runat="server" Text="" /></td></tr>
        <tr><th>恢復審查編號</th>
            <td><asp:Label ID="LBOA001" runat="server" Text="" /></td></tr>
        <tr><th>原審查期限</th>
            <td><asp:Label ID="LBOA002" runat="server" Text="" /></td></tr>
        <tr><th>剩餘審查天數</th>
            <td><asp:Label ID="LBOA003" runat="server" Text="" /></td></tr>
        <tr><th>復審後審查期限</th>
            <td><asp:Label ID="LBOA004" runat="server" Text="" /></td></tr>
	   <tr><th>上傳修正本<span style="color: red;font-family:cursive;">＊</span></th>
            <td>
				<asp:FileUpload ID="ONA10File_FileUpload" runat="server" style="height:25px;" />
				<asp:Button ID="Btn_UPONA10File" runat="server" Text="上傳檔案" style="height:25px;" OnClick="Btn_UPSFile_Click"/>
				<asp:Button ID="Btn_DelONA10File" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="javascript:return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" style="height:25px;" OnClick="Btn_DelSFile_Click" />
				<asp:TextBox ID="ONA10File" runat="server" Width="70px" Visible="False" />
				<asp:HyperLink ID="ONA10FileLink" runat="server" Target="_blank"/>
				<span class="red" style="font-weight:bold; display:block; margin:10px 0px 0px;">※ 上傳格式限定為PDF，檔案大小請於150mb以內</span>
			</td>
	   </tr>
                </tbody></table>
        <br/><br/></div>
        
        <div class="btncenter">
            <asp:Button ID="DataLock" runat="server" Text="送出申請" OnClientClick="javascript:return confirm('提醒您，確認後將立即恢復審查，請再次確認，恢復後請於系統上傳修正本以供審查單位進行後續審查作業。');" OnClick="DataLock_Click" />
            <asp:Button ID="GoHomePage" runat="server" Text="返回瀏覽案件" OnClick="GoHomePage_Click" />
        </div>
    </div><!--content-s-->
</asp:Content>

