<%@ Page Title="退補正補件" Language="C#" MasterPageFile="~/PriPage/MasterPageA.master" AutoEventWireup="true" CodeFile="SwcApply2002.aspx.cs" Inherits="SWCAPPLY_SwcApply2001" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:Label ID="LBSWC000" runat="server" Text=""></asp:Label>

    <br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>

    <asp:Button ID="Button1" runat="server" Text="資料送出中................" OnClick="Button1_Click" CssClass="word" style="display:none;" />

        <br/><br/><br/><br/><br/><br/><br/><br/><br/>
    <script type="text/javascript">
        var i = confirm("提醒您，送出後無法修改案件資料，請確認資料是否正確。");
        if (i) {
            document.getElementById("<%=Button1.ClientID%>").click();
        } else {
            var swcId = document.getElementById("<%=LBSWC000.ClientID%>").innerText;
            location.href = '../SWCDOC/SWC003.aspx?SWCNO=' + swcId;
        }
    </script>
</asp:Content>

