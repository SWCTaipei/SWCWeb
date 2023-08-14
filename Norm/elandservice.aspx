<%@ Page Title="山坡地水土保持諮詢服務線上預約" Language="C#" MasterPageFile="~/PriPage/MasterPageA.master" AutoEventWireup="true" CodeFile="elandservice.aspx.cs" Inherits="Norm_elandservice" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style>
 .ajax__calendar td{
 border:none !important;
 padding:2px;
 }
</style>


    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True"/>

    <div class="content-s">
        <div class="txttitle">山坡地水土保持諮詢服務線上預約</div>
        <span class="surveybtn"><a href="https://swc.taipei/elandservice/survey.aspx" target="_blank">滿意度調查</a></span>
		<div>
            <table class="applyfrom"><tbody>
            <tr><th>申請地點<span class="must">＊</span> <asp:TextBox ID="THISID" runat="server" Visible="false" /></th>
                <td>
				    
				    本人所有（或使用）臺北市
                    <asp:DropDownList ID="DDLDistrict" runat="server" AutoPostBack="True" Height="22px" Width="60px" Font-Size="16px" OnSelectedIndexChanged="DDLDistrict_SelectedIndexChanged" /> 區 &nbsp;
                    <asp:DropDownList ID="DDLSection" runat="server" AutoPostBack="True" Height="22px" Width="60px" Font-Size="16px" OnSelectedIndexChanged="DDLSection_SelectedIndexChanged" /> 段 &nbsp;
                    <asp:DropDownList ID="DDLSection2" runat="server" AutoPostBack="True" Height="22px" Width="60px" Font-Size="16px" /> 小段 &nbsp;
                    <asp:TextBox ID="TXTNumber" runat="server" Height="22px" Width="60px" Font-Size="16px" MaxLength="8"/>地號 &nbsp; 
                    &nbsp; 
                    <asp:Button ID="ADDLIST01" runat="server" Text="新增" Height="26px" OnClientClick="return chknull2();" OnClick="ADDLIST01_Click" /><br/>
					<span class="red" style="display:block; margin-bottom:10px;">※ 請於勾選地號後點擊新增以登錄地籍資料</span>

                                <asp:TextBox ID="CDNO" runat="server" Text="0" Visible="false"/>
                                <asp:GridView ID="GVCadastral" runat="server" CssClass="imgTB" AutoGenerateColumns="False"
                                    OnRowCommand="GVCadastral_RowCommand" PageSize="5" AllowPaging="True"
                                    OnPageIndexChanged="GVCadastral_PageIndexChanged" OnPageIndexChanging="GVCadastral_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="序號" HeaderText="序號" />
                                        <asp:BoundField DataField="區" HeaderText="區" />
                                        <asp:BoundField DataField="段" HeaderText="段" />
                                        <asp:BoundField DataField="小段" HeaderText="小段" />
                                        <asp:BoundField DataField="地號" HeaderText="地號" />
                                        <asp:TemplateField Visible="true">
                                            <ItemTemplate>
                                                <asp:Button runat="server" CommandArgument="<%#Container.DataItemIndex %>" CommandName="delfile001" Text="刪除" OnClientClick="return confirm('確認刪除這筆資料?')"  />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
               </td>
            </tr>
            <tr><th>申請項目</th>
                <td><span><asp:CheckBox ID="chkswccase" runat="server" Text="水土保持申請程序諮詢" /></span>
                    <span class="appleck"><asp:CheckBox ID="chkswcprotect" runat="server" Text="水土保持處理技術輔導" /></span>
                    <span class="appleck"><asp:CheckBox ID="chkswcselfcheck" runat="server" Text="水土保持設施檢查輔導" /></span>
                    <span class="appleck"><asp:CheckBox ID="chkswcilg" runat="server" Text="水土保持違規改正輔導" /></span><br><br>
                    <span><asp:CheckBox ID="chkswcfix" runat="server" Text="山坡地災害之搶修輔導" /></span>
                    <span class="appleck"><asp:CheckBox ID="chkswclandtype" runat="server" Text="山坡地地質諮詢" /></span><br><br>
                    <span><asp:CheckBox ID="chkswcother" runat="server" Text="其他 "  AutoPostBack ="true" OnCheckedChanged="chkswcother_CheckedChanged" />
                          <asp:TextBox ID="othertext" runat="server" Width="260px" Height="22px" Font-Size="16px" Enabled="False"></asp:TextBox></span><br><br>
                    <span><asp:CheckBox ID="chkswcphoto" runat="server" Text="相關照片" AutoPostBack="true" OnCheckedChanged="chkswcphoto_CheckedChanged" />
                          <asp:FileUpload ID="swcphoto" runat="server" Enabled="False" BackColor="White" />
                          <asp:Button ID="swcphotoupload" runat="server" Text="上傳照片" Enabled="False" OnClick="swcphotoupload_Click" /></span><br />
                    <span class="red">※ 上傳格式限定為jpg、png，檔案大小請於5mb以內</span><br />
          
                    <asp:Panel ID="swcphotopanel" runat="server" Visible="False">
                        <asp:TextBox ID="TBFileDesc" runat="server" Visible="false" Text="01"/>
                        <asp:Label ID="Files001No" runat="server" Text="0" Visible="false"/>
                        <asp:GridView ID="GridView2" runat="server" CssClass="imgTB" AutoGenerateColumns="False" OnRowCommand="GridView2_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="File001000" HeaderText="序號" />
                                <asp:HyperLinkField DataNavigateUrlFields="File001004" DataTextField="File001003" HeaderText="檔名" Target="_blank" />
                                <asp:TemplateField Visible="true" >
                                    <ItemTemplate>
                                        <asp:Button runat="server" CommandArgument="<%#Container.DataItemIndex %>" CommandName="delfile002" Text="刪除" OnClientClick="return confirm('確認刪除這筆資料?')" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr><th>預約時段<span class="must">＊</span></th>
                <td><asp:RadioButton ID="radself" runat="server" Text="本人將於" AutoPostBack="True" OnCheckedChanged="radself_CheckedChanged" />
                    <asp:TextBox ID="selfdatatext" runat="server" Width="90px" AutoPostBack="True" OnTextChanged="selfdatatext_TextChanged" autocomplete="off" Enabled="False"></asp:TextBox>
                    <asp:CalendarExtender ID="selfdatatext_CalendarExtender" runat="server" Enabled="True" TargetControlID="selfdatatext" Format="yyyy-MM-dd" ></asp:CalendarExtender>
                    （週二）上午親自前來諮詢<br /><br />
                    <asp:RadioButton ID="radtel" runat="server" Text="本人無法前來，請於" AutoPostBack="True" OnCheckedChanged="radtel_CheckedChanged" />&nbsp;
                    <asp:TextBox ID="teldatatext" runat="server" Width="90px" Enabled="False" AutoPostBack="true" autocomplete="off" OnTextChanged="teldatatext_TextChanged" />
                    <asp:CalendarExtender ID="teldatatext_CalendarExtender" runat="server" Enabled="True" TargetControlID="teldatatext" Format="yyyy-MM-dd" ></asp:CalendarExtender>
                    (週二)上午下列時段電話回覆<br /><br />
                    <asp:CheckBox ID="chk0910" runat="server" Text="09：00～10：00" Enabled="False" CssClass="appleck mt-15" />  
                    <asp:CheckBox ID="chk1011" runat="server" Text="10：00～11：00" Enabled="False" CssClass="appleck mt-15" />  
                    <asp:CheckBox ID="chk1112" runat="server" Text="11：00～12：00" Enabled="False" CssClass="appleck mt-15" />  
                    <br /><br />
                    <asp:RadioButton ID="radneedpeople" runat="server" Text="請專業技師至現場輔導，並請電話與本人確認時間。" AutoPostBack="True" OnCheckedChanged="radneedpeople_CheckedChanged" />
                </td>
            </tr>
            <tr><th>申請人資訊<span class="must">＊</span></th>
                <td><table class="servicebase"><tbody>
                    <tr><th>申請人：</th>
                        <td><asp:TextBox ID="nametext" runat="server" MaxLength="10" Height="22px"></asp:TextBox><span class="gray">（ex：王小明）</span><br>
                         <span class="red">※ 務必填寫以便回覆</span>
                        </td>
                    </tr>
                    <tr><th>手機號碼：</th>
                        <td><asp:TextBox ID="mobiletext" runat="server" Height="22px" ToolTip="0928123456" AutoPostBack="true" MaxLength="10"></asp:TextBox>
                            <asp:CheckBox ID="chksms" runat="server" Text="是否需簡訊通知進度" /><span class="gray">（ex：0912345678）</span>
                        </td>
                    </tr>
                    <tr><th>聯絡電話：</th>
                        <td><asp:TextBox ID="teltext" runat="server"  Height="22px" ToolTip="02-12345678#123" AutoPostBack="true" MaxLength="20"></asp:TextBox><span class="gray">（ex：02-27912345#1234）</span></td>
                    </tr>
                    <tr><th>住址：</th>
                        <td><asp:TextBox ID="addresstext" runat="server" style="width:60%;height:22px;"></asp:TextBox><span class="gray"><br />（ex：臺北市信義區松德路300號3樓）</span></td>
                    </tr>
                    </tbody></table>
                </td>
            </tr>
            </tbody></table>  
        </div> 
        <div class="btncenter">
            <asp:Button ID="servicesend" runat="server" Text="確定申請" Font-Size="11pt" Height="25px" OnClick="servicesend_Click" />
        </div>
    </div><!--content-s-->
</asp:Content>

