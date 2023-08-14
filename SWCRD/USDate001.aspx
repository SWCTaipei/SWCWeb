<%@ Page Title="義務人基本資料" Language="C#" MasterPageFile="~/PriPage/MasterPageA.master" AutoEventWireup="true" CodeFile="USDate001.aspx.cs" Inherits="SWCRD_USDate001" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
      function setvalue() {

  var a = document.getElementById("Semail");
          var b = document.getElementById("SSWC014");
            a.value = document.getElementById("email").value;;
            b.value = document.getElementById("SWC014").value;;
         }
       
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
             <div class="content-s">
            <div class="detailsMenu"><img id="ContentPlaceHolder1_Image1" src="../images/title/dataupdate-14.jpg" /></div>
                <div class="applyGrid">
                    <h2 class="detailsBar_title_basic">基本資料</h2>
          
                    <table>
                        <tr>
                            <td>義務人姓名</td>
                            <td><asp:Label ID="SWC013" runat="server" Text="王大明"></asp:Label>
                        </tr>
                        <tr>
                            <td>義務人身份證字號</td>
                            <td><asp:Label ID="SWC013ID" runat="server" Text="F159875600"></asp:Label>
                               </td>
                        </tr>
                        <tr> 
                            <td>義務人手機</td>
                            <td><asp:Label ID="SWC013TEL" runat="server" Text="0915698744"></asp:Label>
                              </td>
                        </tr>
                        <tr>
                            <td>信箱<span style="color: red;font-family:cursive;">＊</span></td>
                            <td><asp:TextBox ID="email" runat="server" Text=""  style="width:400px;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>義務人地址<span style="color: red;font-family:cursive;">＊</span></td>
                            <td><asp:TextBox ID="SWC014" runat="server" text="台北市內湖區石潭路27號" style="width:400px;" ></asp:TextBox>
                            </td>
                        </tr>
                    </table>
              </div>

              <div class="btncenter">
                  &nbsp;
                   <a href="../SWCDOC/HaloPage001.aspx"><input type="button" value="返回我的案件" /></a>
              
                  <asp:Button ID="Button1" runat="server" Text="確定送出"  OnClientClick="setvalue()"   OnClick=" Button1_Click"  />
                  <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                <input type="hidden" id="Semail" runat='server' />
                    <input type="hidden" id="SSWC014" runat='server' />
              </div>
    </div>
</asp:Content>

