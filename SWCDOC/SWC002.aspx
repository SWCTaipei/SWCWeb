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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWC002.aspx.cs" Inherits="SWCDOC_SWC002" %>
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
    <meta http-equiv="X-UA-Compatible" content="IE=Edge, chrome=1"/>
    <meta name="viewport" content="width=device-width">
    <link rel="stylesheet" type="text/css" href="../css/reset.css" />
    <link rel="stylesheet" type="text/css" href="../css/all.css" />
    <link rel="stylesheet" type="text/css" href="../css/iris.css" />
    <script type="text/javascript">
        function textcount(txtobj, labobj, txtcount) { 
            var textboxtemp = txtobj;
            var labeltemp = document.getElementById(labobj);

            if (window.event.keyCode == 13) {
                window.event.keyCode = null;
            }
            var ttval = textboxtemp.value.length;
            if (ttval > parseInt(txtcount)) {
                window.event.keyCode = null;
                textboxtemp.innerText = textboxtemp.value.substring(0, parseInt(txtcount));
                ttval = textboxtemp.value.length;
                labeltemp.innerText = "(" + ttval + "/" + txtcount + ")";
            }
            else {
                labeltemp.innerText = "(" + ttval + "/" + txtcount + ")";
            }
        }
        function chknumber(objElement) {
            var chrstring = objElement.value;
            var chr = chrstring.substring(chrstring.length - 1, chrstring.length);
            if ((chr != '.') && (chr != '0') && (chr != '1') && (chr != '2') && (chr != '3') && (chr != '4') && (chr != '5') && (chr != '6') && (chr != '7') && (chr != '8') && (chr != '9')) {
                objElement.value = objElement.value.substring(0, chrstring.length - 1);
            }
        }
        function chkInput(jChkType) {
            jCHKValue01 = document.getElementById("TXTSWC005").value;
            jCHKValue02 = document.getElementById("DDLSWC007").value;
            jCHKValue04 = document.getElementById("TXTSWC015").value;
            jCHKValue05 = document.getElementById("TXTSWC016").value;

            jCHKValue13 = document.getElementById("TXTSWC013").value;
            jCHKValue13ID = document.getElementById("TXTSWC013ID").value;
            jCHKValue13TEL = document.getElementById("TXTSWC013TEL").value;
            
            jCHKValue14 = document.getElementById("TXTSWC014").value;
            jCHKValue23 = document.getElementById("TXTSWC023").value;
            jCHKValue27 = document.getElementById("TXTSWC027").value;
            jCHKValue28 = document.getElementById("TXTSWC028").value;

            jCHKValue108 = document.getElementById("TXTSWC108").value;
            jCHKValue108chk = document.getElementById("TXTSWC108_chk").value;
            
            if (jCHKValue01.trim() == '') {
                alert('請輸入書件名稱');
                document.getElementById("TXTSWC005").focus();
                return false;
            }
            if (jCHKValue02.trim() == '') {
                alert('請輸入書件類別');
                document.getElementById("DDLSWC007").focus();
                return false;
            } if (document.getElementById("GVCadastral")) { } else {
                alert('請輸入地籍');
                return false;
            }
            if (jCHKValue13.trim() == '') {
                alert('請輸入義務人');
                document.getElementById("TXTSWC013").focus();
                return false;
            }
            if (jCHKValue13ID.trim() == '') {
                alert('請輸入義務人身份證字號');
                document.getElementById("TXTSWC013").focus();
                return false;
            }
            //if (jCHKValue13ID.trim().length != 10) {
            //    alert('請輸入義務人身份證字號');
            //    document.getElementById("TXTSWC013").focus();
            //    return false;
            //}
            if (jCHKValue13TEL.trim() == '' || jCHKValue13TEL.substr(0, 2) != '09' || jCHKValue13TEL.length != 10)
            {
                alert('手機格式錯誤，請輸入正確之義務人(代表人)手機號碼');
                document.getElementById("TXTSWC013TEL").focus();
                return false;
            }
            if (jCHKValue14.trim() == '') {
                alert('請輸入義務人地址');
                document.getElementById("TXTSWC014").focus();
                return false;
            }
            //if (!checkID(jCHKValue13ID)) {
            //    alert('請輸入合法身份證號碼');
            //    document.getElementById("TXTSWC013ID").focus();
            //    return false;
            //}
            if (jCHKValue04.trim() == '') {
                alert('請輸入聯絡人');
                document.getElementById("TXTSWC015").focus();
                return false;
            }
            if (jCHKValue05.trim() == '') {
                alert('請輸入聯絡人手機');
                document.getElementById("TXTSWC016").focus();
                return false;
            }
            if (jCHKValue108.trim() == '') {
                alert('請輸入聯絡人E-mail');
                document.getElementById("TXTSWC108").focus();
                return false;
            } else
            {
                if (jCHKValue108chk.trim() == jCHKValue108.trim())
                {
                    alert('聯絡人E-mail與技師 E-mail相同，請重新輸入聯絡人E-mail');
                    document.getElementById("TXTSWC108").focus();
                    return false;
                }
            }

            if (jCHKValue23.trim() != '') {
                if (isNaN(jCHKValue23)) {
                    alert('計畫面積(公頃) 請輸入正確數字');
                    document.getElementById("TXTSWC023").focus();
                    return false;
                }
            }
            if (isNaN(jCHKValue27))
            {
                alert('座標X請輸入數字');
                document.getElementById("TXTSWC027").focus();
                return false;
            }
            if (isNaN(jCHKValue28)) {
                alert('座標Y請輸入數字');
                document.getElementById("TXTSWC028").focus();
                return false;
            }
            if (jCHKValue27 != '' || jCHKValue27 !='') {
                if (jCHKValue27 >= 295190 && jCHKValue27 <= 317350 && jCHKValue28 >= 2761450 && jCHKValue28 <= 2789250)
                { } else
                {
                    alert('座標位置不在臺北市，請重新輸入，謝謝!!');
                    document.getElementById("TXTSWC027").focus();
                    return false;
                }
            }

            if (rr) { rr = ChkLength(jCHKValue13, 100, '義務人'); } 
            if (rr) { rr = ChkLength(jCHKValue14, 255, '義務人地址'); }
            return rr;
        }
        function chknull(jTxtBox) {
            var jChkValue = document.getElementById(jTxtBox).value;
            var jUpLoadFile = document.getElementById('Link001').innerText;

            if (jUpLoadFile.trim() == '') {
                alert('請先上傳計畫申請書');
                return false;
            }
        }
        function chknull2() {
            var x1 = document.getElementById("DDLDistrict");
            var dd1 = (x1.options[x1.selectedIndex].text);

            var x2 = document.getElementById("DDLSection");
            var dd2 = (x2.options[x2.selectedIndex].text);

            var x3 = document.getElementById("DDLSection2");
            var dd3 = (x3.options[x3.selectedIndex].text);

            var x4 = document.getElementById("DDLNumber");
            var dd4 = (x4.options[x4.selectedIndex].text);

            if (dd1 == '') {
                alert('請輸入完整地段號');
                document.getElementById("DDLDistrict").focus();
                return false;
            }
            if (dd2 == '') {
                alert('請輸入完整地段號');
                document.getElementById("DDLSection").focus();
                return false;
            }
            if (dd3 == '') {
                alert('請輸入完整地段號');
                document.getElementById("DDLSection2").focus();
                return false;
            }
            if (dd4 == '') {
                alert('請輸入完整地段號');
                document.getElementById("DDLNumber").focus();
                return false;
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
            <div class="header header-s clearfix">
                <a href="SWC001.aspx" class="logo-s"></a>
                <div class="header-menu-s">
                    <ul>
                        <li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
                        <li>|</li>
                        <li><a href="http://tcgeswc.taipei.gov.tw/index_new.aspx" title="水土保持計畫查詢系統" target="_blank">水土保持計畫查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="http://172.28.100.55/TSLM" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
                        <asp:Panel ID="TitleLink00" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWCBase001.aspx" title="帳號管理">帳號管理</a></li></asp:Panel>
                        <li>|</li>
                        <li><a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li>
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
            <div class="detailsMenu"><img src="../images/title/title-edit.png" alt=""/>
                <div class="detailsMenu-btn">
                    <asp:ImageButton ID="SwcList" runat="server" OnClick="SwcList_Click" title="返回總表" ImageUrl="../images/btn/btn-back.png" />
                    <%--<a href="details_edit.html"><img src="images/btn/btn-edit.png" alt=""/></a>
                    <a href="details_edit.html"><img src="images/btn/btn-change.png" alt=""/></a>--%>
                </div>
            </div>

            <div class="detailsGrid">

            <asp:Panel ID="Area01" runat="server">
                <h2 class="detailsBar_title_basic">基本資料<img src="../images/btn/btn-close.png" alt="" class="open"/></h2>
                <div class="detailsGrid_wrap">
                    <table class="detailsGrid_skyblue">
                    <tr><td style="background: #d9d9d9;">水保局編號</td>
                        <td class="detailsGrid_gray">
                            <asp:Label ID="LBSWC002" runat="server"/>
                            <asp:Label ID="LBSWC000" runat="server"/>
                            <asp:Label ID="TempSwc000" runat="server" /></td>
                        <td rowspan="18" class="innerBR" style="vertical-align: top;">
                            
                                <asp:GridView ID="GVSWCCHG" runat="server" CssClass="aa" AutoGenerateColumns="False"
                                    ShowHeader="false">
                                    <Columns>
                                        <asp:HyperLinkField DataNavigateUrlFields="SWC002Link" DataTextField="SWC002" HeaderText="變更設計" Target="_blank" />
                                    </Columns>
                                </asp:GridView>
                                </td>
                        </tr>

                    <tr><td style="background: #d9d9d9;">案件狀態</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC004" runat="server"/></td></tr>
                    <tr><td>書件名稱<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTSWC005" runat="server" width="100%" Height="100px" TextMode="MultiLine" onkeyup="textcount(this,'TXTSWC005_count','512');" />
                            <asp:Label ID="TXTSWC005_count" runat="server" Text="(0/512)" ForeColor="Red"></asp:Label></td></tr>
                    <tr><td>書件類別<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:DropDownList ID="DDLSWC007" runat="server" Height="25px"/>
                            <asp:Label ID="LBSWC007" runat="server"></asp:Label></td></tr>
                        <tr><td>地籍<span style="color: red;font-family:cursive;">＊</span></td>
                            <td style="line-height:2;"><asp:DropDownList ID="DDLDistrict" runat="server" Height="25px" AutoPostBack="true" OnSelectedIndexChanged="DDLDistrict_SelectedIndexChanged"/>區
                                <asp:DropDownList ID="DDLSection" runat="server" Height="25px" AutoPostBack="true" OnSelectedIndexChanged="DDLSection_SelectedIndexChanged"/>段
                                <asp:DropDownList ID="DDLSection2" runat="server" Height="25px" AutoPostBack="true" OnSelectedIndexChanged="DDLSection2_SelectedIndexChanged"/>小段
                                <asp:TextBox ID="TXTNumber" runat="server" Height="25px" MaxLength="8" />地號
                                <br/>
                                使用分區：<asp:DropDownList ID="DDLCA01" runat="server" Height="25px"/>
                                可利用限度：<asp:DropDownList ID="DDLCA02" runat="server" Height="25px"/><br/>
                                林地類別：<asp:DropDownList ID="DDLCA03" runat="server" Height="25px"/>
                                地質敏感區：<asp:DropDownList ID="DDLCA04" runat="server" Height="25px"/>
                                <asp:Button ID="ADDLIST01" runat="server" Text="加入清單" Height="26px" OnClientClick="return chknull2();" OnClick="ADDLIST01_Click" />
                               
                                <asp:TextBox ID="CDNO" runat="server" Text="0" Visible="false"/>
                                <asp:GridView ID="GVCadastral" runat="server" CssClass="detailsGrid_skyblue_innerTable" AutoGenerateColumns="False"
                                    OnRowCommand="GVCadastral_RowCommand" PageSize="5" AllowPaging="True"
                                    OnPageIndexChanged="GVCadastral_PageIndexChanged" OnPageIndexChanging="GVCadastral_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="序號" HeaderText="序號" />
                                        <asp:BoundField DataField="區" HeaderText="區" />
                                        <asp:BoundField DataField="段" HeaderText="段" />
                                        <asp:BoundField DataField="小段" HeaderText="小段" />
                                        <asp:BoundField DataField="地號" HeaderText="地號" />
                                        <asp:BoundField DataField="土地使用分區" HeaderText="使用分區" />
                                        <asp:BoundField DataField="土地可利用限度" HeaderText="可利用限度" />
                                        <asp:BoundField DataField="林地類別" HeaderText="林地類別" />
                                        <asp:BoundField DataField="地質敏感區" HeaderText="地質敏感區" />
                                        <asp:TemplateField Visible="true">
                                            <ItemTemplate>
                                                <asp:Button runat="server" CommandArgument="<%#Container.DataItemIndex %>" CommandName="delfile001" Text="刪除" OnClientClick="return confirm('確認刪除這筆資料?')"  />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </td>
                        </tr>

                    <tr><td>義務人<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTSWC013" runat="server" width="100%" Height="100px" TextMode="MultiLine" onkeyup="textcount(this,'TXTSWC013_count','100');" MaxLength="100" />
                            <asp:Label ID="TXTSWC013_count" runat="server" Text="(0/100)" ForeColor="Red"></asp:Label><br/>
                            <span> 範例：王大明、陳小華（用、分開）</span></td></tr>
                    <tr><td>義務人身份證字號<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTSWC013ID" runat="server" MaxLength="10" /></td></tr>
                    <tr><td>義務人手機<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTSWC013TEL" runat="server" MaxLength="10" /></td></tr>
                    <tr><td>義務人地址<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTSWC014" runat="server" width="100%" Height="100px" TextMode="MultiLine" onkeyup="textcount(this,'TXTSWC014_count','255');" MaxLength="255" />
                            <asp:Label ID="TXTSWC014_count" runat="server" Text="(0/255)" ForeColor="Red"></asp:Label></td></tr>
                    <tr><td>聯絡人<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTSWC015" runat="server" width="100%" onkeyup="textcount(this,'TXTSWC015_count','50');" MaxLength="50" />
                            <asp:Label ID="TXTSWC015_count" runat="server" Text="(0/50)" ForeColor="Red"/></td></tr>
                    <tr><td>聯絡人手機<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTSWC016" runat="server" width="100%" onkeyup="textcount(this,'TXTSWC016_count','50');" MaxLength="50" />
                            <asp:Label ID="TXTSWC016_count" runat="server" Text="(0/50)" ForeColor="Red"/><span>範例：0928123456  分隔請用 ";"</span></td></tr>
                    <tr><td>聯絡人E-mail<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTSWC108" runat="server" width="100%" onkeyup="textcount(this,'TXTSWC108_count','200');" MaxLength="200" />
                            <asp:Label ID="TXTSWC108_count" runat="server" Text="(0/200)" ForeColor="Red"/>
                            <span style="display:none;"><asp:TextBox ID="TXTSWC108_chk" runat="server" width="100%" /></span>
                        </td></tr>
                    <tr><td>目的事業主管機關</td>
                        <td><asp:DropDownList ID="DDLSWC017" runat="server" Height="25px"/>　
                            其他：
                            <asp:TextBox ID="TXTSWC018" runat="server" width="30%" onkeyup="textcount(this,'TXTSWC018_count','50');" MaxLength="50" />
                            <asp:Label ID="TXTSWC018_count" runat="server" Text="(0/50)" ForeColor="Red"></asp:Label></td></tr>
                    <tr><td>計畫面積(公頃)</td>
                        <td><asp:TextBox ID="TXTSWC023" runat="server" CssClass="wordright" Width="200px" onkeyup="chknumber(this);" MaxLength="10" />
                            <span style="margin-left:7px;">公頃</span></td></tr>
                    <tr><td style="background: #d9d9d9;">承辦技師</td>
                        <td class="detailsGrid_gray">
                            <asp:Label ID="LBSWC021" runat="server" />
                            <asp:Label ID="LBSWC021ID" runat="server" Visible="false" /></td></tr>
                    <tr><td style="background: #d9d9d9;">承辦人員</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC025" runat="server" /></td></tr>
                    <tr><td>座標</td>
                        <td>X：<asp:TextBox ID="TXTSWC027" runat="server" onkeyup="chknumber(this);" MaxLength="6" Width="80"/> &nbsp;&nbsp;
                            Y：<asp:TextBox ID="TXTSWC028" runat="server" onkeyup="chknumber(this);" MaxLength="7" Width="80" />
                            <br/><span>範例：X：300580 Y：2778810（請輸入67座標系）</span></td></tr>
                    <tr><td>計畫申請書</td>
                        <td style="line-height: 2;"><asp:TextBox ID="TXTFILES001" runat="server" Width="70px" style="display:none;" />
                            <asp:FileUpload ID="TXTFILES001_fileupload" runat="server" />
                            <asp:Button ID="TXTFILES001_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTFILES001_fileuploadok_Click" />
                            <asp:Button ID="BTNFILES001" runat="server" CssClass="wordttb" Text="X" OnClientClick="return confirm('確認刪除這筆資料?')" OnClick="BTNFILES001_Click"  />
                            <br/><span style="color:red;">※ 上傳格式限定為PDF檔案大小請於150mb以內</span>
                            <asp:Label ID="Files001No" runat="server" Text="0" Visible="false"/>
                            <br/> <%--多筆上傳--%>                            
                            <asp:HyperLink ID="Link001" runat="server" CssClass="word" Target="_blank"/><br/>
                            <asp:Button ID="AddFile001" runat="server" Text="加入清單" CssClass="wordttb" OnClick="AddFile001_Click" OnClientClick="return chknull('TXTFILES001');" Height="26px" />
                            <%--<asp:GridView ID="GridView1" runat="server" CssClass="detailsGrid_skyblue_innerTable detailsGrid_skyblue_innerTable_lastCenter" AutoGenerateColumns="False"
                               OnRowCommand="SWCFILES001_RowCommand"  >--%>
                            <asp:GridView ID="SWCFILES001" runat="server" CssClass="detailsGrid_skyblue_innerTable detailsGrid_skyblue_innerTable_lastCenter" AutoGenerateColumns="False"
                               OnRowCommand="SWCFILES001_RowCommand"  >
                                <Columns>
                                    <asp:BoundField DataField="File001000" HeaderText="序號" />
                                    <asp:HyperLinkField DataNavigateUrlFields="File001004" DataTextField="File001003" HeaderText="計畫申請書" Target="_blank" />
                                    <asp:TemplateField Visible="true" >
                                        <ItemTemplate>
                                            <asp:Button runat="server" CommandArgument="<%#Container.DataItemIndex %>" CommandName="delfile002" Text="刪除" OnClientClick="return confirm('確認刪除這筆資料?')" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView></td></tr>
                    </table>
                </div>
            </asp:Panel>

            <asp:Panel ID="Area02" runat="server">
                <h2 class="detailsBar_title_accept">受理<img src="../images/btn/btn-close.png" alt="" class="open"/></h2>
                <div class="detailsGrid_wrap">
                    <table class="detailsGrid_purple">
                    <tr><td>補正期限</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC032" runat="server"/></td></tr>
                    <tr><td>退補件說明</td>
                        <td class="detailsGrid_gray">
                            <asp:Label ID="LBSWC103" runat="server"/></td></tr>
                    <tr><td>第二次補正期限</td>
                        <td class="detailsGrid_gray">
                            <asp:Label ID="LBSWC104" runat="server"/></td></tr>
                    <tr><td>第二次退補件說明</td>
                        <td class="detailsGrid_gray">
                            <asp:Label ID="LBSWC105" runat="server"/></td></tr>
                    <tr><td>審查費金額</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC035" runat="server"/></td></tr>
                    <tr><td>審查費繳納期限</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC031" runat="server"/></td></tr>
                    <tr><td>審查費繳納日期</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC033" runat="server"/></td></tr>
                    </table>
                </div>
            </asp:Panel>

            <asp:Panel ID="Area03" runat="server">
                <h2 class="detailsBar_title_review">審查<img src="../images/btn/btn-close.png" alt="" class="open"/></h2>
                <div class="detailsGrid_wrap">
                    <table class="detailsGrid_green">
                    <tr><td style="background: #d9d9d9;">受理日期</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC034" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">審查單位</td>
                        <td class="detailsGrid_gray">
                            <asp:Label ID="LBSWC022" runat="server"/>
                            <asp:Label ID="LBSWC022ID" runat="server" Visible="false" /></td></tr>
                    <tr><td>審查委員</td>
                        <td><asp:TextBox ID="TXTSWC087" runat="server" width="100%" Height="100px" TextMode="MultiLine" onkeyup="textcount(this,'TXTSWC087_count','255');" />
                            <asp:Label ID="TXTSWC087_count" runat="server" Text="(0/255)" ForeColor="Red"></asp:Label></td></tr>

                    <tr><td>審查紀錄<br/><%--<span>(鎖審查單位填)</span>--%></td>
                        <td><asp:Button ID="DT001" runat="server" Text="+ 審查紀錄" CssClass="detailsGrid_wrap_btn" OnClick="DT001_Click"/>
                            <asp:GridView ID="SWCDTL01" runat="server" CssClass="greenPause" AutoGenerateColumns="False" 
                                OnRowDataBound="SWCDTL01_RowDataBound"
                                    >
                                <Columns>
                                    <asp:BoundField DataField="DTLA001" HeaderText="審查表單編號"  />
                                    <asp:BoundField DataField="DTLA002" HeaderText="審查日期" />
                                    <asp:BoundField DataField="DTLA003" HeaderText="補正期限" />
                                    <asp:BoundField DataField="DTLA004" HeaderText="主旨" />
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:Button ID="ButtonDTL01" runat="server" CommandArgument='<%# Eval("DTLA001") %>' OnClick="ButtonDTL01_Click" Text="詳情"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="true">
                                        <ItemTemplate>
                                            <asp:Button ID="ButtonDEL01" runat="server" CommandArgument='<%# Eval("DTLA001") %>' OnClick="ButtonDEL01_Click" Text="刪除" OnClientClick="return confirm('資料刪除後無法復原，確認是否刪除這筆資料?')"  />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="Lock01" runat="server" Value='<%# Bind("DTLA005") %>'  />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView></td></tr>
                <tr><td style="background: #d9d9d9;">審查期限</td>
                    <td class="detailsGrid_gray">
                        <asp:Label ID="LBSWC088" runat="server"/></td></tr>
                <tr><td style="background: #d9d9d9;">暫停審查期限</td>
                    <td class="detailsGrid_gray">
                        <asp:Label ID="LBSWC089" runat="server"/></td></tr>
                <tr><td>核定本</td>
                    <td><asp:FileUpload ID="TXTSWC080_fileupload" runat="server" CssClass="wordtt" />
                        <asp:Button ID="TXTSWC080_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTSWC080_fileuploadok_Click" />
                        <asp:TextBox ID="TXTSWC080" runat="server" CssClass="word" Width="20px" Visible="False"/>
                        <asp:Button ID="TXTSWC080_fileclean" runat="server" CssClass="wordttb" Text="X" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTSWC080_fileclean_Click" />
                        <br/><span style="color:red;">※ 上傳格式限定為PDF，檔案大小請於150mb以內</span><br/>
                        <asp:HyperLink ID="Link080" runat="server" CssClass="word" Target="_blank"/></td></tr>
                <tr><td>水土保持設施配置圖<br/><%--<span>(圖6-1)</span>--%></td>
                    <td><asp:FileUpload ID="TXTSWC029CAD_fileupload" runat="server" CssClass="wordtt" />
                        <asp:Button ID="TXTSWC029CAD_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTSWC029CAD_fileuploadok_Click" />
                        <asp:TextBox ID="TXTSWC029CAD" runat="server" CssClass="word" Width="20px" Visible="False"/>
                        <asp:Button ID="TXTSWC029CAD_fileclean" runat="server" CssClass="wordttb" Text="X" OnClick="TXTSWC029CAD_fileclean_Click" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" />
                        <br/><span style="color:red;">※ 上傳格式限定為CAD，檔案大小請於50mb以內</span><br/>
                        <asp:HyperLink ID="Link029CAD" runat="server" CssClass="word" Target="_blank"/>
                        <br/>
                        <asp:FileUpload ID="TXTSWC029_fileupload" runat="server" CssClass="wordtt" />
                        <asp:Button ID="TXTSWC029_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTSWC029_fileuploadok_Click" />
                        <asp:TextBox ID="TXTSWC029" runat="server" CssClass="word" Width="20px" Visible="False"/>
                        <asp:Button ID="TXTSWC029_fileclean" runat="server" CssClass="wordttb" Text="X" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTSWC029_fileclean_Click" />
                        <br/><span style="color:red;">※ 上傳格式限定為PDF，檔案大小請於50mb以內</span><br/>
                        <asp:HyperLink ID="Link029" runat="server" CssClass="word" Target="_blank"/></td></tr>
                <tr><td>臨時性防災設施配置圖<br/><%--<span>(圖7-1)</span>--%></td>
                    <td><asp:FileUpload ID="TXTSWC030_fileupload" runat="server" CssClass="wordtt" />
                        <asp:Button ID="TXTSWC030_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTSWC030_fileuploadok_Click" />
                        <asp:TextBox ID="TXTSWC030" runat="server" CssClass="word" Width="20px" Visible="False"/>
                        <asp:Button ID="TXTSWC030_fileclean" runat="server" CssClass="wordttb" Text="X" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTSWC030_fileclean_Click" /><br/>
                        <span style="color:red;">※ 上傳格式限定為PDF，檔案大小請於50mb以內</span><br/>
                        <asp:HyperLink ID="Link030" runat="server" CssClass="word" Target="_blank"/>
                        </td></tr>
                <tr><td>公會建議核定日期</td>
                    <td><asp:TextBox ID="TXTSWC109" runat="server" width="120px"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTSWC109_CalendarExtender" runat="server" TargetControlID="TXTSWC109" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                <tr><td>審查單位查核表</td>
                    <td><asp:FileUpload ID="TXTSWC110_fileupload" runat="server" CssClass="wordtt" />
                        <asp:Button ID="TXTSWC110_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTSWC110_fileuploadok_Click" />
                        <asp:TextBox ID="TXTSWC110" runat="server" CssClass="word" Width="20px" Visible="False"/>
                        <asp:Button ID="TXTSWC110_fileclean" runat="server" CssClass="wordttb" Text="X" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTSWC110_fileclean_Click"/><br/>
                        <span style="color:red;">※ 上傳格式限定為PDF，檔案大小請於50mb以內</span><br/>
                        <asp:HyperLink ID="Link110" runat="server" CssClass="word" Target="_blank"/>
                    </td></tr>
                <tr><td style="background: #d9d9d9;">核定日期</td>
                    <td class="detailsGrid_gray"><asp:Label ID="LBSWC038" runat="server"/></td></tr>
                <tr><td style="background: #d9d9d9;">核定文號</td>
                    <td class="detailsGrid_gray"><asp:Label ID="LBSWC039" runat="server"/></td></tr>
                <tr><td style="background: #d9d9d9;">保證金金額</td>
                    <td class="detailsGrid_gray"><asp:Label ID="LBSWC040" runat="server"/></td></tr>
                <tr><td style="background: #d9d9d9;">審查費核銷</td>
                    <td class="detailsGrid_gray"><asp:Label ID="LBSWC036" runat="server"/></td></tr>
                    </table>
                </div>
            </asp:Panel>

            <asp:Panel ID="Area04" runat="server">
                <h2 class="detailsBar_title_construction">施工<img src="../images/btn/btn-close.png" alt="" class="open"/></h2>
                <div class="detailsGrid_wrap">
                    <table class="detailsGrid_purple2">
                    <tr><td style="background: #d9d9d9;">開工期限</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC082" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">開工展延次數</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC083" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">保證金繳納</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC041" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">施工許可證核發日期</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC043" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">施工許可證核發文號</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC044" runat="server"/></td></tr>
                    <tr><td>開工日期</td>
                        <td><asp:TextBox ID="TXTSWC051" runat="server" width="120px"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTSWC051_CalendarExtender" runat="server" TargetControlID="TXTSWC051" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                    <tr><td style="background: #d9d9d9;">預定完工日期</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC052" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">監造技師</td>
                        <td class="detailsGrid_gray">
                            <asp:Label ID="LBSWC045" runat="server"/>
                            <asp:Label ID="LBSWC045ID" runat="server" Visible="false" /></td></tr>
                    <tr><td style="background: #d9d9d9;">監造技師手機</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC047" runat="server"/></td></tr>
                    <tr><td>施工廠商</td>
                        <td><asp:TextBox ID="TXTSWC048" runat="server" width="100%" Height="100px" TextMode="MultiLine" onkeyup="textcount(this,'TXTSWC048_count','255');" />
                            <asp:Label ID="TXTSWC048_count" runat="server" Text="(0/255)" ForeColor="Red" /></td></tr>
                    <tr><td>工地負責人</td>
                        <td><asp:TextBox ID="TXTSWC049" runat="server" width="200px" onkeyup="textcount(this,'TXTSWC049_count','50');" MaxLength="50" /><br/>
                            <asp:Label ID="TXTSWC049_count" runat="server" Text="(0/50)" ForeColor="Red" /></td></tr>
                    <tr><td>工地負責人手機</td>
                        <td><asp:TextBox ID="TXTSWC050" runat="server" width="200px" onkeyup="textcount(this,'TXTSWC050_count','50');" MaxLength="50" /><br/>
                            <asp:Label ID="TXTSWC050_count" runat="server" Text="(0/50)" ForeColor="Red" />
                            <span> 範例：0928123456  分隔請用 ";"</span></td></tr>
                    <tr><td style="background: #d9d9d9;">檢查單位</td>
                        <td class="detailsGrid_gray">
                            <asp:Label ID="LBSWC024" runat="server"/>
                            <asp:Label ID="LBSWC024ID" runat="server" Visible="false" /></td></tr>
                    <tr><td>施工中監督檢查紀錄<%--<span>(鎖檢查單位(施工檢查紀錄表)、機關人員(施工抽查紀錄表)填)</span>--%></td>
                        <td><asp:Button ID="DT003" runat="server" Text="+ 施工檢查紀錄表" CssClass="detailsGrid_wrap_btn" OnClick="DT003_Click"/>
                            <asp:HyperLink ID="DT002" runat="server" Text="+ 水保抽查紀錄表" CssClass="detailsGrid_wrap_btn" Visible="false"/>
                            <asp:GridView ID="SWCDTL0302" runat="server" CssClass="detailsGrid_purple2_innerTable" AutoGenerateColumns="False"
                                OnRowDataBound="SWCDTL0302_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="DTLC001" HeaderText="施工監督表編號" />
                                    <asp:BoundField DataField="DTLC002" HeaderText="檢查日期" />
                                    <asp:BoundField DataField="DTLC003" HeaderText="檢查類型" />
                                    <asp:BoundField DataField="DTLC004" HeaderText="檢查公會" />
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:Button ID="ButtonDTL03" runat="server" CommandArgument='<%# Eval("DTLC001") %>' OnClick="ButtonDTL03_Click" Text="詳情"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="true">
                                        <ItemTemplate>
                                            <asp:Button ID="ButtonDEL03" runat="server" CommandArgument='<%# Eval("DTLC001") %>' CommandName="deldtl" Text="刪除" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="ButtonDEL03_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="Lock03" runat="server" Value='<%# Bind("DTLC005") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView></td></tr>
                    <tr><td>颱風豪雨設施自主檢查表<%--<span>(鎖監造技師填)</span>--%></td>
                        <td><asp:Button ID="DT004" runat="server" Text="+ 颱風豪雨設施自主檢查表" CssClass="detailsGrid_wrap_btn" style="font-size:13px;" OnClick="DT004_Click"/>
                            <asp:GridView ID="SWCDTL04" runat="server" CssClass="detailsGrid_purple2_innerTable" AutoGenerateColumns="False"
                                   OnRowDataBound="SWCDTL04_RowDataBound"   >
                                <Columns>
                                    <asp:BoundField DataField="DTLD001" HeaderText="自主檢查表編號" />
                                    <asp:BoundField DataField="DTLD002" HeaderText="檢查日期" />
                                    <asp:BoundField DataField="DTLD003" HeaderText="防災標的" />
                                    <asp:BoundField DataField="DTLD004" HeaderText="自主檢查結果" />
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:Button ID="ButtonDTL04" runat="server" CommandArgument='<%# Eval("DTLD001") %>' OnClick="ButtonDTL04_Click" Text="詳情"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="true">
                                        <ItemTemplate>
                                            <asp:Button ID="ButtonDEL04" runat="server" CommandArgument='<%# Eval("DTLD001") %>' CommandName="deldtl" Text="刪除" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="ButtonDEL04_Click"  />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="Lock04" runat="server" Value='<%# Bind("DTLD005") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView></td></tr>
                    <tr><td>監造紀錄表<%--<span>(鎖監造技師填)</span>--%></td>
                        <td><asp:Button ID="DT005" runat="server" Text="+ 監造紀錄表" CssClass="detailsGrid_wrap_btn" OnClick="DT005_Click"/>
                            <asp:GridView ID="SWCDTL05" runat="server" CssClass="detailsGrid_purple2_innerTable" AutoGenerateColumns="False"
                                    OnRowDataBound="SWCDTL05_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="DTLE001" HeaderText="監造紀錄表編號" />
                                    <asp:BoundField DataField="DTLE002" HeaderText="檢查日期" />
                                    <asp:BoundField DataField="DTLE003" HeaderText="累積進度百分比" />
                                    <asp:BoundField DataField="DTLE004" HeaderText="監造結果" />
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:Button ID="ButtonDTL05" runat="server" CommandArgument='<%# Eval("DTLE001") %>' OnClick="ButtonDTL05_Click" Text="詳情"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="true">
                                        <ItemTemplate>
                                            <asp:Button ID="ButtonDEL05" runat="server" CommandArgument='<%# Eval("DTLE001") %>' CommandName="deldtl" Text="刪除" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="ButtonDEL05_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="Lock05" runat="server" Value='<%# Bind("DTLE005") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView></td></tr>
                    <tr><td style="background: #d9d9d9;">核備圖說變更</td>
                        <td class="detailsGrid_gray">
                            <asp:HyperLink ID="Link106" runat="server" target="_blank" /></td></tr>
                    <tr><td style="background: #d9d9d9;">停工日期</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC053" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">停工期限</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC084" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">停工展延次數</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC092" runat="server"/></td></tr>
                    <tr><td>完工檢查紀錄<%--<span>(鎖檢查單位填)</span>--%></td>
                        <td><asp:Button ID="DT006" runat="server" Text="+ 完工檢查紀錄" CssClass="detailsGrid_wrap_btn" OnClick="DT006_Click"/>
                            <asp:GridView ID="SWCDTL06" runat="server" CssClass="detailsGrid_purple2_innerTable" AutoGenerateColumns="False"
                                OnRowDataBound="SWCDTL06_RowDataBound" >
                                <Columns>
                                    <asp:BoundField DataField="DTLF001" HeaderText="完工檢查表編號" />
                                    <asp:BoundField DataField="DTLF002" HeaderText="檢查日期" />
                                    <asp:BoundField DataField="DTLF003" HeaderText="達完工標準" />
                                    <asp:BoundField DataField="DTLF004" HeaderText="檢查公會" />
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:Button ID="ButtonDTL06" runat="server" CommandArgument='<%# Eval("DTLF001") %>' OnClick="ButtonDTL06_Click" Text="詳情"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="true">
                                        <ItemTemplate>
                                            <asp:Button ID="ButtonDEL06" runat="server" CommandArgument='<%# Eval("DTLF001") %>' CommandName="deldtl" Text="刪除" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="ButtonDEL06_Click"  />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="Lock06" runat="server" Value='<%# Bind("DTLF005") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView></td></tr>
                    <tr><td>完工日期</td>
                        <td><asp:TextBox ID="TXTSWC058" runat="server" width="120px"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTSWC058_CalendarExtender" runat="server" TargetControlID="TXTSWC058" Format="yyyy-MM-dd"></asp:CalendarExtender>
                            <span style="color:red;"> ※完工申報書填的完工日期</span></td></tr>
                    <tr><td>竣工圖說</td>
                        <td><asp:FileUpload ID="TXTSWC101CAD_fileupload" runat="server" CssClass="wordtt" />
                            <asp:Button ID="BTNSWC101CAD_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTSWC101CAD_fileupload_Click" />
                            <asp:TextBox ID="TXTSWC101CAD" runat="server" CssClass="word" Width="20px" Visible="False"/>
                            <asp:Button ID="BTNSWC101CAD_fileuploaddel" runat="server" CssClass="wordttb" Text="X" OnClick="BTNSWC101CAD_fileuploaddel_Click" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" />
                            <br/><span style="color:red;">※ 上傳格式限定為CAD，檔案大小請於50mb以內</span><br/>
                            <asp:HyperLink ID="Link101CAD" runat="server" CssClass="word" Target="_blank"/>
                            <br/>
                            <asp:TextBox ID="TXTSWC101" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTSWC101_fileupload" runat="server" />
                            <asp:Button ID="BTNSWC101_fileuploadok" runat="server" Text="上傳檔案" OnClick="BTNSWC101_fileuploadok_Click" />
                            <asp:Button ID="BTNSWC101_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClick="BTNSWC101_fileuploaddel_Click" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" /><br/>
                            <span style="color:red;">※ 上傳格式限定為PDF，檔案大小請於50mb以內</span><br/>
                            <asp:HyperLink ID="Link101" runat="server" Target="_blank" />
                            </td></tr>
                    <tr><td style="background: #d9d9d9;">完工證明書核發日期</td>
                        <td class="detailsGrid_gray">
                            <asp:Label ID="LBSWC059" runat="server"/></td></tr>
                    <tr><td>水保設施</td>
                        <td><asp:CheckBox ID="CHKSWC067" runat="server" Text="滯洪沉砂設施" />
                            <asp:TextBox ID="TXTSWC068" runat="server" onkeyup="chknumber(this);" MaxLength="8" /> 座，滯洪量 <asp:TextBox ID="TXTSWC069" runat="server" onkeyup="chknumber(this);" MaxLength="8" /> m³，沉砂量 <asp:TextBox ID="TXTSWC070" runat="server" onkeyup="chknumber(this);" MaxLength="8"/> m³<br/>
                            <asp:CheckBox ID="CHKSWC071" runat="server" Text="排水設施" />
                            <asp:TextBox ID="TXTSWC072" runat="server" MaxLength="8" onkeyup="chknumber(this);" /> 條，<asp:CheckBox ID="CHKSWC073" runat="server" Text="擋土設施" />
                            <asp:TextBox ID="TXTSWC074" runat="server" MaxLength="8" onkeyup="chknumber(this);" /> 道<br/>
                            <asp:CheckBox ID="CHKSWC075" runat="server" Text="植生工程" /></td></tr>
                    <tr><td style="background: #d9d9d9;">保證金退還</td>
                        <td class="detailsGrid_gray">
                            <asp:Label ID="LBSWC056" runat="server"/></td></tr>
                    </table>
                </div>
            </asp:Panel>

            <asp:Panel ID="Area05" runat="server">
                <h2 class="detailsBar_title_finish">完工後水土保持設施檢查<img src="../images/btn/btn-close.png" alt="" class="open"></h2>
                <div class="detailsGrid_wrap">
                    <table class="detailsGrid_orange">
                    <tr><td>維護管理人</td>
                        <td><asp:TextBox ID="TXTSWC093" runat="server" Width="350" onkeyup="textcount(this,'TXTSWC093_count','255');" />
                            <asp:Label ID="TXTSWC093_count" runat="server" Text="(0/255)" ForeColor="Red" />
                            <asp:Label ID="LBSWC107ID" runat="server" Visible="false"/>
                        </td></tr>
                    <tr><td>維護管理人手機</td>
                        <td><asp:TextBox ID="TXTSWC095" runat="server" Width="200" onkeyup="textcount(this,'TXTSWC095_count','50');" />
                            <asp:Label ID="TXTSWC095_count" runat="server" Text="(0/50)" ForeColor="Red" />
                            <span>，範例：0928123456  分隔請用 ";" </span></td></tr>
                    <tr><td>維護管理人地址</td>
                        <td><asp:TextBox ID="TXTSWC094" runat="server" width="100%" Height="120" TextMode="MultiLine" onkeyup="textcount(this,'TXTSWC094_count','255');" /><br/>
                            <asp:Label ID="TXTSWC094_count" runat="server" Text="(0/255)" ForeColor="Red" /></td></tr>
                    <tr><td>基地概況</td>
                        <td><asp:CheckBox ID="CHKSWC061" runat="server" Text="建物" />
                            <asp:TextBox ID="TXTSWC062" runat="server" /> 戶，
                            <asp:CheckBox ID="CHKSWC063" runat="server" Text="道路" />
                            <asp:TextBox ID="TXTSWC064" runat="server" /> 條<br/>
                            <asp:CheckBox ID="CHKSWC065" runat="server" Text="其他" /><br/>
                            <asp:TextBox ID="TXTSWC066" runat="server" width="100%" Height="120" TextMode="MultiLine" onkeyup="textcount(this,'TXTSWC066_count','255');" />
                            <asp:Label ID="TXTSWC066_count" runat="server" Text="(0/255)" ForeColor="Red" /></td></tr>
                    <tr><td>設施維護檢查表</td>
                        <td><asp:Button ID="DT007" runat="server" Text="+ 設施維護檢查表" CssClass="detailsGrid_wrap_btn" OnClick="DT007_Click"/>
                            <asp:GridView ID="SWCDTL07" runat="server" CssClass="detailsGrid_orange_innerTable detailsGrid_orange_innerTable2" AutoGenerateColumns="False"
                                OnRowDataBound="SWCDTL07_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="DTLG001" HeaderText="設施維護表編號" />
                                    <asp:BoundField DataField="DTLG002" HeaderText="檢查日期" />
                                    <asp:BoundField DataField="DTLG003" HeaderText="檢查情形" />
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:Button ID="ButtonDTL07" runat="server" CommandArgument='<%# Eval("DTLG001") %>' OnClick="ButtonDTL07_Click" Text="詳情"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="true">
                                        <ItemTemplate>
                                            <asp:Button id="ButtonDEL07" runat="server" CommandArgument='<%# Eval("DTLG001") %>' CommandName="deldtl" Text="刪除" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="ButtonDEL07_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="Lock07" runat="server" Value='<%# Bind("DTLG005") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView></td></tr>
                    </table>
                </div>
            </asp:Panel>

           
                        <div class="checklist-btn">
                            <asp:Button ID="SaveCase" runat="server" Text="存檔" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" />&nbsp&nbsp
                            <asp:Button ID="GoHomePage" runat="server" Text="返回總表" OnClick="GoHomePage_Click" />
                        </div>

                    </div>
                </div>
        
            <%--<div class="footer-s">
                <div class="footer-s-green"></div>
                <div class="footer-b-brown">
                    <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                       <span class="span2">110臺北市信義區松德路300號3樓 　(02)27593001   臺北市民當家熱線1999</span><br/>
                       <span class="span2">資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span></p>
                </div>
            </div>--%>
        
            <div class="footer">
                 <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                    <span class="span2">110臺北市信義區松德路300號3樓 　服務專線(02)27591109   臺北市民當家熱線1999</span><br/>
                    <span class="span2">建議使用IE11.0(含)以上，Chrome或Firefox版本瀏覽器 資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                    <span class="span2">客服電話：02-27593001#3718 許先生 本系統由多維空間資訊有限公司開發維護 TEL:(02)27929328</span></p>
            </div>

        </div>

        <asp:Literal ID="error_msg" runat="server"></asp:Literal>

        <script src="../js/jquery-3.1.1.min.js"></script>
        <script src="../js/inner.js"></script>
        <script src="../js/BaseNorl.js"></script>
        <script src="../js/allhref.js"></script>

    </div>
    </form>
</body>
</html>
