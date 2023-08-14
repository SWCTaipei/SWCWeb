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
    <link rel="stylesheet" type="text/css" href="../css/all.css?202109300333" />
    <link rel="stylesheet" type="text/css" href="../css/iris.css" />
	<link href="../css/search.css" rel="stylesheet">
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
            document.getElementById('SaveCase').style.display = "none";
            document.getElementById('SendMsg').style.display = "";

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
                document.getElementById('SaveCase').style.display = "";
                document.getElementById('SendMsg').style.display = "none";
                document.getElementById("TXTSWC005").focus();
                return false;
            }
            if (jCHKValue02.trim() == '') {
                alert('請輸入書件類別');
                document.getElementById('SaveCase').style.display = "";
                document.getElementById('SendMsg').style.display = "none";
                document.getElementById("DDLSWC007").focus();
                return false;
            } if (document.getElementById("GVCadastral")) { } else {
                alert('請輸入地籍');
                document.getElementById('SaveCase').style.display = "";
                document.getElementById('SendMsg').style.display = "none";
                return false;
            } if (document.getElementById("GVPEOPLE")) { } else {
                alert('請輸入義務人資訊');
                document.getElementById('SaveCase').style.display = "";
                document.getElementById('SendMsg').style.display = "none";
                return false;
            }
            //if (jCHKValue13.trim() == '') {
            //    alert('請輸入義務人');
            //    document.getElementById('SaveCase').style.display = "";
            //    document.getElementById('SendMsg').style.display = "none";
            //    document.getElementById("TXTSWC013").focus();
            //    return false;
            //}
            //if (jCHKValue13ID.trim() == '') {
            //    alert('請輸入義務人身份證字號');
            //    document.getElementById('SaveCase').style.display = "";
            //    document.getElementById('SendMsg').style.display = "none";
            //    document.getElementById("TXTSWC013").focus();
            //    return false;
            //}
            //if (jCHKValue13ID.trim().length != 10) {
            //    alert('請輸入義務人身份證字號');
            //    document.getElementById("TXTSWC013").focus();
            //    return false;
            //}
            //if (jCHKValue13TEL.trim() == '' || jCHKValue13TEL.substr(0, 2) != '09' || jCHKValue13TEL.length != 10)
            //{
            //    alert('手機格式錯誤，請輸入正確之義務人(代表人)手機號碼');
            //    document.getElementById('SaveCase').style.display = "";
            //    document.getElementById('SendMsg').style.display = "none";
            //    document.getElementById("TXTSWC013TEL").focus();
            //    return false;
            //}
            //if (jCHKValue14.trim() == '') {
            //    alert('請輸入義務人地址');
            //    document.getElementById('SaveCase').style.display = "";
            //    document.getElementById('SendMsg').style.display = "none";
            //    document.getElementById("TXTSWC014").focus();
            //    return false;
            //}
            //if (!checkID(jCHKValue13ID)) {
            //    alert('請輸入合法身份證號碼');
            //    document.getElementById("TXTSWC013ID").focus();
            //    return false;
            //}
            if (jCHKValue04.trim() == '') {
                alert('請輸入聯絡人');
                document.getElementById('SaveCase').style.display = "";
                document.getElementById('SendMsg').style.display = "none";
                document.getElementById("TXTSWC015").focus();
                return false;
            }
            if (jCHKValue05.trim() == '') {
                alert('請輸入聯絡人手機');
                document.getElementById('SaveCase').style.display = "";
                document.getElementById('SendMsg').style.display = "none";
                document.getElementById("TXTSWC016").focus();
                return false;
            }
            if (jCHKValue108.trim() == '') {
                alert('請輸入聯絡人E-mail');
                document.getElementById('SaveCase').style.display = "";
                document.getElementById('SendMsg').style.display = "none";
                document.getElementById("TXTSWC108").focus();
                return false;
            } else
            {
                if (jCHKValue108chk.trim() == jCHKValue108.trim())
                {
                    alert('聯絡人E-mail與技師 E-mail相同，請重新輸入聯絡人E-mail');
                    document.getElementById('SaveCase').style.display = "";
                    document.getElementById('SendMsg').style.display = "none";
                    document.getElementById("TXTSWC108").focus();
                    return false;
                }
            }
            
            if (jCHKValue23.trim() == '') {
                alert('請輸入計畫面積(公頃)');
                document.getElementById('SaveCase').style.display = "";
                document.getElementById('SendMsg').style.display = "none";
                return false;
            }
            else {
                if (isNaN(jCHKValue23)) { 
                    alert('計畫面積(公頃) 請輸入正確數字');
                    document.getElementById('SaveCase').style.display = "";
                    document.getElementById('SendMsg').style.display = "none";
                    document.getElementById("TXTSWC023").focus();
                    return false;
                }
            }
            if (isNaN(jCHKValue27) || jCHKValue27.trim()=="")
            {
                alert('座標X請輸入數字');
                document.getElementById('SaveCase').style.display = "";
                document.getElementById('SendMsg').style.display = "none";
                document.getElementById("TXTSWC027").focus();
                return false;
            }
            if (isNaN(jCHKValue28) || jCHKValue28.trim() == "") {
                alert('座標Y請輸入數字');
                document.getElementById('SaveCase').style.display = "";
                document.getElementById('SendMsg').style.display = "none";
                document.getElementById("TXTSWC028").focus();
                return false;
            }
            if (jCHKValue27 != '' || jCHKValue27 !='') {
                if (jCHKValue27 >= 296020 && jCHKValue27 <= 318180 && jCHKValue28 >= 2761246 && jCHKValue28 <= 2789046)
                { } else
                {
                    alert('座標位置不在臺北市，請重新輸入，謝謝!!');
                    document.getElementById('SaveCase').style.display = "";
                    document.getElementById('SendMsg').style.display = "none";
                    document.getElementById("TXTSWC027").focus();
                    return false;
                }
            }
            //document.getElementById('SaveCase').disabled = false;
            //if (rr) { rr = ChkLength(jCHKValue13, 100, '義務人'); } 
            //if (rr) { rr = ChkLength(jCHKValue14, 255, '義務人地址'); }
            return true;
        }
        function chknull(jTxtBox) {
            var jChkValue = document.getElementById(jTxtBox).value;
            var jUpLoadFile = document.getElementById('Link001').innerText;

            if (jUpLoadFile.trim() == '') {
                alert('請先上傳計畫申請書');
                return false;
            }
        }
        function UpLoadChk(ObjId) {
            if (document.getElementById(ObjId).value == '') { alert('請選擇檔案'); return false; } else { return ChkFileSize(ObjId, '500', '.pdf'); }
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
		function chkObligorData()
		{
			jCHKPhone = document.getElementById("TXTSWCPPHONE").value;
			if (jCHKPhone.substr(0, 2) != '09' || jCHKPhone.length != 10)
            {
                alert('手機格式錯誤，請輸入正確之義務人(代表人)手機號碼');
                document.getElementById("TXTSWCPPHONE").focus();
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
                        <li><a href="https://swc.taipei/swcinfo" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="https://tslm.swc.taipei/tslmwork" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
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
                </div>
            </div>

            <div class="detailsGrid">

            <asp:Panel ID="Area01" runat="server">
                <h2 class="detailsBar_title_basic">基本資料<img src="../images/btn/btn-close.png" alt="" class="open"/></h2>
                <div class="detailsGrid_wrap">
                    <table class="detailsGrid_skyblue">
                    <tr><td style="background: #d9d9d9;">案件編號</td>
                        <td class="detailsGrid_gray">
                            <asp:Label ID="LBSWC000" runat="server" /></td>
                        
                        </tr>
                    <tr><td style="background: #d9d9d9;">水保局編號</td>
                        <td class="detailsGrid_gray">
                            <asp:Label ID="LBSWC002" runat="server"/></td>
                        </tr>

                    <tr><td style="background: #d9d9d9;">案件狀態</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC004" runat="server"/></td></tr>
					<tr>
					  <td>變更設計</td>
					  <td>
					    <asp:GridView ID="GVSWCCHG" runat="server" CssClass="aa" AutoGenerateColumns="False"
                                    ShowHeader="false">
                                    <Columns>
                                        <asp:HyperLinkField DataNavigateUrlFields="SWC002Link" DataTextField="SWC002" HeaderText="變更設計" Target="_blank" />
                                    </Columns>
                                </asp:GridView>
					  </td>
					</tr>
                    <tr><td>書件名稱<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTSWC005" runat="server" width="100%" Height="100px" TextMode="MultiLine" onkeyup="textcount(this,'TXTSWC005_count','512');" />
                            <asp:Label ID="TXTSWC005_count" runat="server" Text="(0/512)" ForeColor="Red"></asp:Label></td></tr>
                    <tr><td>書件類別<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:DropDownList ID="DDLSWC007" runat="server" Height="25px"/>
                            <asp:Label ID="LBSWC007" runat="server"></asp:Label>
                            <asp:Label ID="LBSWC012" runat="server" Visible="false"></asp:Label>
                        </td></tr>
                    <tr><td>毗鄰土地</td>
                        <td><asp:TextBox ID="TBSWC120" runat="server" Height="25px" Width="230px" MaxLength="15" onkeyup="value=value.replace(/[\W]/g,'')" placeholder="UA9910905001，若沒有則無須填寫" /><br>
                            <span class="red">※ 若有毗鄰土地同時申請案件，請於本欄填入該案水保局編號（EX:UA9910905001），若沒有則無須填寫。</span>
						</td></tr>
                        

                        <tr><td>地籍<span style="color: red;font-family:cursive;">＊</span></td>
                            <td style="line-height:2;"><asp:DropDownList ID="DDLDistrict" runat="server" Height="25px" AutoPostBack="true" OnSelectedIndexChanged="DDLDistrict_SelectedIndexChanged"/>區
                                <asp:DropDownList ID="DDLSection" runat="server" Height="25px" AutoPostBack="true" OnSelectedIndexChanged="DDLSection_SelectedIndexChanged"/>段
                                <asp:DropDownList ID="DDLSection2" runat="server" Height="25px" AutoPostBack="true" OnSelectedIndexChanged="DDLSection2_SelectedIndexChanged"/>小段
                                <asp:TextBox ID="TXTNumber" runat="server" Height="25px" MaxLength="8" />地號

                                <%--使用分區：--%><asp:DropDownList ID="DDLCA01" runat="server" Height="25px" style="display:none"/>
                                <asp:Button ID="ADDLIST01" runat="server" Text="加入清單" Height="26px" OnClientClick="return chknull2();" OnClick="ADDLIST01_Click" />
                               
                                <asp:TextBox ID="CDNO" runat="server" Text="0" Visible="false"/>
                                <asp:GridView ID="GVCadastral" runat="server" CssClass="detailsGrid_skyblue_innerTable w-100" AutoGenerateColumns="False"
                                    OnRowCommand="GVCadastral_RowCommand" PageSize="100" AllowPaging="True" OnRowDataBound="GVCadastral_RowDataBound"
                                    OnPageIndexChanged="GVCadastral_PageIndexChanged" OnPageIndexChanging="GVCadastral_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="序號" HeaderText="序號" />
                                        <asp:BoundField DataField="區" HeaderText="區" />
                                        <asp:BoundField DataField="段" HeaderText="段" />
                                        <asp:BoundField DataField="小段" HeaderText="小段" />
                                        <asp:BoundField DataField="地號" HeaderText="地號" />
                                        <asp:BoundField DataField="山坡地範圍" HeaderText="山坡地範圍" />
                                        <asp:BoundField DataField="水保計畫申請紀錄" HeaderText="水保計畫申請紀錄" />
                                        <asp:BoundField DataField="水土保持法違規紀錄" HeaderText="水土保持法違規紀錄" />
                                        <asp:BoundField DataField="土地可利用限度" HeaderText="可利用限度" />
                                        <asp:BoundField DataField="陽明山國家公園範圍" HeaderText="陽明山國家公園範圍" />
                                        <asp:BoundField DataField="林地類別" HeaderText="林地類別" />
                                        <asp:BoundField DataField="地質敏感區" HeaderText="地質敏感區" />
                                        <asp:BoundField DataField="土地使用分區" HeaderText="使用分區" />
                                        <asp:TemplateField Visible="true">
                                            <ItemTemplate>
                                                <asp:Button runat="server" CommandArgument="<%#Container.DataItemIndex %>" CommandName="delfile001" Text="刪除" OnClientClick="return confirm('確認刪除這筆資料?')"  />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
								<asp:Panel ID="P_Message" runat="server" Visible="false">
									<span class="red">※ 如地號狹長或面積過小地號可能導致系統誤判，如有疑問請電洽本處審查管理科。</span>
								</asp:Panel>

                            </td>
                        </tr>
                        <%--<tr>
                            <td>毗鄰案件編號</td>
                            <td><input type="text" value="TT9910902034" /></td>
                        </tr>--%>

                    <tr style="display:none;"><td>義務人<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTSWC013" runat="server" width="100%" Height="100px" TextMode="MultiLine" onkeyup="textcount(this,'TXTSWC013_count','100');" MaxLength="100" />
                            <asp:Label ID="TXTSWC013_count" runat="server" Text="(0/100)" ForeColor="Red"></asp:Label><br/>
                            <span class="gray">（範例：王大明、陳小華，請用、分開）</span></td></tr>
                    <tr style="display:none;"><td>義務人身份證字號<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTSWC013ID" runat="server" MaxLength="10" /></td></tr>
                    <tr style="display:none;"><td>義務人手機<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTSWC013TEL" runat="server" MaxLength="10" /></td></tr>
                    <tr style="display:none;"><td>義務人地址<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTSWC014" runat="server" width="100%" Height="100px" TextMode="MultiLine" onkeyup="textcount(this,'TXTSWC014_count','255');" MaxLength="255" />
                            <asp:Label ID="TXTSWC014_count" runat="server" Text="(0/255)" ForeColor="Red"></asp:Label></td></tr>
                    <tr><td>義務人資訊<span style="color: red;font-family:cursive;">＊</span></td>
						<td>
							<table class="bold">
								<tbody>
									<tr>
										<th>姓名</th>
										<td><asp:TextBox ID="TXTSWCPNAME" runat="server" /></td>
										<th>身分證字號/統一編號</th>
										<td><asp:TextBox ID="TXTSWCPID" runat="server" MaxLength="20" /></td>
									</tr>
									<tr>
										<th>手機</th>
										<td colspan="3"><asp:TextBox ID="TXTSWCPPHONE" runat="server" MaxLength="20" /></td>
									</tr>
									<tr>
										<th style="vertical-align:middle;">地址</th>
										<td colspan="3">
											<asp:TextBox ID="TXTSWCPCODE" runat="server" width="60px" Enabled="false" />
											<asp:DropDownList ID="DDLSWCPCITY" AutoPostBack="true" OnSelectedIndexChanged="DDLSWCPCITY_SelectedIndexChanged" runat="server" width="80px" />
											<asp:DropDownList ID="DDLSWCPAREA" AutoPostBack="true" OnSelectedIndexChanged="DDLSWCPAREA_SelectedIndexChanged" runat="server" width="80px" />
											<asp:TextBox ID="TXTSWCPADDRESS" runat="server" width="400px" />
										</td>
										
									</tr>
								</tbody>
							</table>
							<br>
							<asp:Button ID="AddAddress" runat="server" Text="加入清單" Height="26px" OnClientClick="return chkObligorData('')" OnClick="AddAddress_Click" />
							<asp:TextBox ID="AddNO" runat="server" Text="0" Visible="false"/>
							<br>							
							<asp:GridView ID="GVPEOPLE" runat="server" AutoGenerateColumns="False" CssClass="detailsGrid_skyblue_innerTable w-100" OnRowCommand="GVPEOPLE_RowCommand" >
                                <Columns>
                                    <asp:BoundField DataField="序號" HeaderText="序號" />
                                    <asp:BoundField DataField="姓名" HeaderText="姓名" />
                                    <asp:BoundField DataField="身分證字號" HeaderText="身分證字號/統一編號" />
                                    <asp:BoundField DataField="手機" HeaderText="手機" />
                                    <asp:BoundField DataField="地址ZipCode" HeaderText="地址" Visible="false" />
                                    <asp:BoundField DataField="地址City" HeaderText="地址" Visible="false" />
                                    <asp:BoundField DataField="地址District" HeaderText="地址" Visible="false" />
                                    <asp:BoundField DataField="地址Address" HeaderText="地址" Visible="false" />
                                    <asp:BoundField DataField="地址" HeaderText="地址" />
                                    <asp:TemplateField Visible="true">
                                        <ItemTemplate>
                                            <asp:Button runat="server" CommandArgument="<%#Container.DataItemIndex %>" CommandName="delfile001" Text="刪除" OnClientClick="return confirm('確認刪除這筆資料?')"  />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
						</td>
                    </tr>
					
					<tr><td>聯絡人<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTSWC015" runat="server" width="100%" onkeyup="textcount(this,'TXTSWC015_count','50');" MaxLength="50" />
                            <asp:Label ID="TXTSWC015_count" runat="server" Text="(0/50)" ForeColor="Red"/></td></tr>
                    <tr><td>聯絡人手機<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTSWC016" runat="server" width="100%" onkeyup="textcount(this,'TXTSWC016_count','50');" MaxLength="50" />
                            <asp:Label ID="TXTSWC016_count" runat="server" Text="(0/50)" ForeColor="Red"/><br><span class="gray">（範例：0928123456  分隔請用 ";"）</span></td></tr>
                    <tr><td>聯絡人E-mail<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTSWC108" runat="server" width="100%" onkeyup="textcount(this,'TXTSWC108_count','200');" MaxLength="200" />
                            <asp:Label ID="TXTSWC108_count" runat="server" Text="(0/200)" ForeColor="Red"/>
                            <span style="display:none;"><asp:TextBox ID="TXTSWC108_chk" runat="server" width="100%" /></span>
							<br/><span style="color:red;">※ 以一組電子信箱地址為限</span>
                        </td></tr>
                    <tr><td>目的事業主管機關</td>
                        <td><asp:DropDownList ID="DDLSWC017" runat="server" Height="25px"/>　
                            其他：
                            <asp:TextBox ID="TXTSWC018" runat="server" width="30%" onkeyup="textcount(this,'TXTSWC018_count','50');" MaxLength="50" />
                            <asp:Label ID="TXTSWC018_count" runat="server" Text="(0/50)" ForeColor="Red"></asp:Label></td></tr>
                    <tr><td>計畫面積(公頃)<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTSWC023" runat="server" CssClass="wordright" Width="100px" onkeyup="chknumber(this);" MaxLength="10" />
                            <span style="margin-left:7px;">公頃</span></td></tr>
                    <tr><td style="background: #d9d9d9;">承辦技師</td>
                        <td class="detailsGrid_gray">
                            <asp:Label ID="LBSWC021" runat="server" />
                            <asp:Label ID="LBSWC021ID" runat="server" Visible="false" /></td></tr>
                    <tr><td style="background: #d9d9d9;">承辦人員</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC025" runat="server" /></td></tr>
                    <tr><td>承辦建築師</td>
                        <td><asp:DropDownList ID="DDLSWC134" runat="server" CssClass="select" /></td></tr>
					<tr><td>座標</td>
                        <td>X：<asp:TextBox ID="TXTSWC027" runat="server" onkeyup="chknumber(this);" MaxLength="6" Width="80"/> &nbsp;&nbsp;
                            Y：<asp:TextBox ID="TXTSWC028" runat="server" onkeyup="chknumber(this);" MaxLength="7" Width="80" />
                            <br/><span style="color:red;">※ 請輸入TWD-97座標系，範例：X：301410 Y：2778606</span></td></tr>
                    <tr><td>計畫申請書</td>
                        <td><asp:TextBox ID="TXTFILES001" runat="server" Width="70px" style="display:none;" />
                            <asp:FileUpload ID="TXTFILES001_fileupload" runat="server" />
                            <asp:Button ID="TXTFILES001_fileuploadok" runat="server" Text="上傳檔案" OnClientClick="return UpLoadChk('TXTFILES001_fileupload');" OnClick="TXTFILES001_fileuploadok_Click" />
                            <asp:Button ID="BTNFILES001" runat="server" CssClass="wordttb" Text="X" OnClientClick="return confirm('確認刪除這筆資料?')" OnClick="BTNFILES001_Click"  />
                            
                            <asp:Label ID="Files001No" runat="server" Text="0" Visible="false"/>
                            <asp:TextBox ID="TBuploadNtc" runat="server" Text="0" style="display:none;" />
                            <br/> <%--多筆上傳--%>                            
                            <asp:HyperLink ID="Link001" runat="server" CssClass="word" Target="_blank"/><br/>
                            <asp:Button ID="AddFile001" runat="server" Text="加入清單" CssClass="wordttb" OnClick="AddFile001_Click" OnClientClick="return chknull('TXTFILES001');" Height="26px" />
							<br/><span style="color:red;">※ 上傳格式限定為PDF，檔案大小請於500mb以內</span>
							<br/><span style="color:red;">※ 上傳檔案請勿使用+、空格、/、\、?、%、#、&、=、!...等特殊符號(包含全形符號)</span>
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
					<tr><td>環評報告書/免環評證明文件<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:FileUpload ID="TXTSWC138_fileupload" runat="server" CssClass="wordtt" />
							<asp:Button ID="TXTSWC138_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTSWC138_fileuploadok_Click" />
							<asp:TextBox ID="TXTSWC138" runat="server" CssClass="word" Width="20px" Visible="False"/>
							<asp:Button ID="TXTSWC138_fileclean" runat="server" CssClass="wordttb" Text="X" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTSWC138_fileclean_Click" />
							<br/><span style="color:red;">※ 上傳格式限定為PDF、JPG，檔案大小請於500mb以內</span>
							<br/><span style="color:red;">※ 上傳檔案請勿使用+、空格、/、\、?、%、#、&、=、!...等特殊符號(包含全形符號)</span>
							<asp:HyperLink ID="Link138" runat="server" CssClass="word" Target="_blank"/></td></tr>
                    <tr><td>水保設施項目</td>
                        <td>
                            <asp:Panel ID="SDIAREA" runat="server" Visible="false">
                             <span class="step">step1</span>
						     <table class="swcfacility">   
                              <tr>
                                <th>水土保持設施類別</th> 
                                <td><asp:DropDownList ID="DDLSIC01" runat="server" style="height:25px;" AutoPostBack="true" OnSelectedIndexChanged="DDLSIC01_SelectedIndexChanged" />
								    </td>
                             </tr>
                             <tr>
                                <th>設施名稱<br />（位置或編號）</th>
                                <td><asp:TextBox ID="TXTSDI004" runat="server" style="height:25px;" MaxLength="200"/></td>
                            </tr>
                            <tr>
                                <th>設施型式</th>
                                <td><asp:DropDownList ID="DDLSIC02" runat="server"  style="height:25px; margin-top:0.5em;" AutoPostBack="true" OnSelectedIndexChanged="DDLSIC02_SelectedIndexChanged" />
                                    <asp:TextBox ID="TXTSDI002" runat="server" Visible="false"/></td>
							</tr>
                            <tr>
                                <th>數量</th>
                                <td><asp:TextBox ID="TXTSDI006" runat="server" style="margin-top:0.5em; width:70px; height:25px;" Enabled="false" onkeyup="return chknumber(this,value)" />
                                <asp:Label ID="AA" runat="server" Visible="false">~</asp:Label>
								<asp:TextBox ID="TXTSDI006_1" runat="server" style="margin-top:0.5em; width:70px; height:25px;" Enabled="false" Visible="false" onkeyup="return chknumber(this,value)" />
								<asp:TextBox ID="TXTSDI006D" runat="server" style="margin-top:0.5em; width:70px; height:25px;" Enabled="false" Visible="false" />
                                <asp:TextBox ID="TXTSDI009" runat="server" style="margin-top:0.5em; width:70px; height:25px;" Visible="false" />
                                <asp:Label ID="LBSDI007" runat="server"/>&nbsp;
                                <span style="color:#A3A3A3; margin-top:0.5em; display:inline-block;">※ 本欄位限輸入數值格式，若要輸入非數字格式「設施形式」請選擇「其他」類別。</span></td>
                            </tr>
                           </table>

                                <span class="step">step2</span>
                                <table class="swcfacility">   
                                  <tr>
                                    <th><span>檢核項目&nbsp;</span></th>
                                    <td><asp:DropDownList ID="DDLSIC05" runat="server" style="height:25px;margin-top:0.5em;" AutoPostBack="true" OnSelectedIndexChanged="DDLSIC05_SelectedIndexChanged" />
                                        <asp:TextBox ID="TXTSIC05" runat="server" Visible="false"/>
										&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="CHKSDI019" runat="server" Text="屬漸變設施" CssClass="checkbox" OnCheckedChanged="CHKSDI019_CheckedChanged" AutoPostBack="true"/>
                                        </td>
                                 </tr>
                               
                                <tr>
                                    <th>尺寸<b><br />（斷面積-W*H）</b></th>
                                    <td><asp:Label ID="LBSDI010" runat="server"/>
                               
                             
                                    <asp:Panel ID="ThreeArea" runat="server">
                                        <asp:TextBox ID="TXTSDI012" runat="server" placeholder="" style="margin-top:0.5em;  width:40px; height:25px;" Enabled="false" onkeyup="return chknumber(this,value)" /><asp:Label ID="A1" runat="server">~</asp:Label><asp:TextBox ID="TXTSDI012_1" runat="server" placeholder="" style="margin-top:0.5em;  width:40px; height:25px;" Enabled="false" onkeyup="return chknumber(this,value)" /> &nbsp;X&nbsp;
                                        <asp:TextBox ID="TXTSDI013" runat="server" placeholder="" style="margin-top:0.5em;  width:40px; height:25px;" Enabled="false" onkeyup="return chknumber(this,value)" /><asp:Label ID="B1" runat="server">~</asp:Label><asp:TextBox ID="TXTSDI013_1" runat="server" placeholder="" style="margin-top:0.5em;  width:40px; height:25px;" Enabled="false" onkeyup="return chknumber(this,value)" /> &nbsp;X&nbsp;
                                        <asp:TextBox ID="TXTSDI014" runat="server" placeholder="" style="margin-top:0.5em;  width:40px; height:25px;" Enabled="false" onkeyup="return chknumber(this,value)" /><asp:Label ID="C1" runat="server">~</asp:Label><asp:TextBox ID="TXTSDI014_1" runat="server" placeholder="" style="margin-top:0.5em;  width:40px; height:25px;" Enabled="false" onkeyup="return chknumber(this,value)" />
                                    </asp:Panel>
							 
                                <asp:TextBox ID="TXTSDI012D" runat="server" style="margin-top:0.5em; width:600px; height:25px;" Visible="false" />
                                <asp:TextBox ID="TXTSDI011" runat="server" style="margin-top:0.5em; width:70px; height:25px;" Visible="false" />
                                <asp:TextBox ID="TXTSDI016" runat="server" style="margin-top:0.5em; width:70px; height:25px;" Visible="false" />
                                <asp:Label ID="LBSDI015" runat="server" />
                                <span style="color:#A3A3A3; margin-top:0.5em; display:inline-block;">※ 本欄位限輸入小數點第二位。</span><br>
							    <span style="color:#A3A3A3; margin-top:0.5em; display:inline-block;">※ 本欄位限輸入數值格式，若要輸入非數字格式「設施形式」請選擇「其他」類別。</span>
                                </td>
                               </tr>
                               </table>
                                <span class="step">step3</span> 
                                 <asp:Button ID="BTNADDSDI" runat="server" Text="加入清單" CssClass="wordttb" OnClick="BTNADDSDI_Click" Height="26px" />
                                
                      
                                <asp:TextBox ID="TXTSDINI" runat="server" Text="0" style="margin-top:0.5em; width:70px; height:25px;" Visible="false" />
                        </asp:Panel>
                            <br />
                                <asp:GridView ID="SDIList" runat="server" CssClass="segtable AutoNewLine" AutoGenerateColumns="False" Height="50"
                                     OnRowCommand="SDIList_RowCommand">
                                    <Columns>
                                        
                                        <asp:BoundField DataField="SDIFD003" HeaderText="水土保持設施類別" HeaderStyle-Width="20%" />
                                        <asp:BoundField DataField="SDIFD004" HeaderText="設施名稱<br>（位置或編號）" HeaderStyle-Width="17%" HtmlEncode="false" />
                                        <asp:BoundField DataField="SDIFD019" HeaderText="漸變設施" HeaderStyle-Width="10%" />
										<asp:BoundField DataField="SDIFD005" HeaderText="設施型式" HeaderStyle-Width="12%" />
                                        <asp:BoundField DataField="SDIFD006D" HeaderText="數量" HeaderStyle-Width="13%" />
                                        <asp:BoundField DataField="SDIFD008" HeaderText="檢核項目"/>
                                        <asp:BoundField DataField="SDIFD012D" HeaderText="尺寸" HeaderStyle-Width="16%" />

                                        <asp:TemplateField Visible="true" >
                                            <ItemTemplate>
                                                <asp:Button runat="server" CommandArgument="<%#Container.DataItemIndex %>" CommandName="SDIDEL" Text="刪除" OnClientClick="return confirm('確認刪除這筆資料?')" />
                                                <asp:HiddenField ID="HDSDI001" runat="server" Value='<%# Eval("SDIFD001") %>' />
                                                <asp:HiddenField ID="HDSDINI" runat="server" Value='<%# Container.DataItemIndex %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView></td></tr>
                        </table>
                </div>
            </asp:Panel>
                 
            <asp:Panel ID="Area06" runat="server" CssClass="none">
                <h2 class="detailsBar_title_accept openh2" style="background:#bce2e8">檔案交換區<img src="../images/btn/btn-close.png" alt="" class="open"/></h2>
                
                <div class="detailsGrid_wrap close" style="display: block;">
                <table class="filechange" style="border:1px solid #000000;"><tbody>
                <tr><td><div style="text-align:left; font-size:14pt;">修正本上傳區</div>
                    <table class="filebase"><tbody>
                    <tr><th>序號</th>
                        <th style="width:480px;">修正本上傳</th>
                        <th>連結</th>
                        <th style="width:13%">上傳人</th>
                        <th style="width:15%">上傳日期</th>
                        </tr>
                    <tr><td>第一次修正本</td>
                                <td><asp:FileUpload ID="SFFile01_FileUpload" runat="server" style="height:25px;" />
                                    <asp:Button ID="Btn_UPSFile01" runat="server" Text="上傳檔案" style="height:25px;" OnClick="Btn_UPSFile_Click" />
                                    <asp:Button ID="Btn_DelSFile01" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" style="height:25px;" OnClick="Btn_DelSFile_Click" />
                                    <asp:TextBox ID="TXTSFile01" runat="server" Width="70px" Visible="False" /></td>
                        <td style="color:blue; text-decoration:underline;">
                            <asp:HyperLink ID="SFLINK01" runat="server" Target="_blank"/></td>
                        <td><asp:Label ID="LBUPLOADU01" runat="server"/></td>
                        <td><asp:Label ID="LBUPLOADD01" runat="server"/></td>
                    </tr>
                            <tr>
                                <td>第二次修正本</td>
                                <td><asp:FileUpload ID="SFFile02_FileUpload" runat="server" style="height:25px;" />
                                    <asp:Button ID="Btn_UPSFile02" runat="server" Text="上傳檔案" style="height:25px;" OnClick="Btn_UPSFile_Click" />
                                    <asp:Button ID="Btn_DelSFile02" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" style="height:25px;" OnClick="Btn_DelSFile_Click" />
                                    <asp:TextBox ID="TXTSFile02" runat="server" Width="70px" Visible="False" /></td>
                                <td style="color:blue;text-decoration:underline;">
                                    <asp:HyperLink ID="SFLINK02" runat="server" Target="_blank"/></td>
                                <td><asp:Label ID="LBUPLOADU02" runat="server"/></td>
                                <td><asp:Label ID="LBUPLOADD02" runat="server"/></td>
                            </tr>
                            <tr><td>第三次修正本</td>
                                <td><asp:FileUpload ID="SFFile03_FileUpload" runat="server" style="height:25px;" />
                                    <asp:Button ID="Btn_UPSFile03" runat="server" Text="上傳檔案" style="height:25px;" OnClick="Btn_UPSFile_Click" />
                                    <asp:Button ID="Btn_DelSFile03" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" style="height:25px;" OnClick="Btn_DelSFile_Click" />
                                    <asp:TextBox ID="TXTSFile03" runat="server" Width="70px" Visible="False" /></td>
                                <td style="color:blue">
                                    <asp:HyperLink ID="SFLINK03" runat="server" Target="_blank"/></td>
                                <td><asp:Label ID="LBUPLOADU03" runat="server"/></td>
                                <td><asp:Label ID="LBUPLOADD03" runat="server"/></td>
                            </tr>
                            <tr><td>第四次修正本</td>
                                <td><asp:FileUpload ID="SFFile06_FileUpload" runat="server" style="height:25px;" />
                                    <asp:Button ID="Btn_UPSFile06" runat="server" Text="上傳檔案" style="height:25px;" OnClick="Btn_UPSFile_Click" />
                                    <asp:Button ID="Btn_DelSFile06" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" style="height:25px;" OnClick="Btn_DelSFile_Click" />
                                    <asp:TextBox ID="TXTSFile06" runat="server" Width="70px" Visible="False" /></td>
                                <td style="color:blue">
                                    <asp:HyperLink ID="SFLINK06" runat="server" Target="_blank"/></td>
                                <td><asp:Label ID="LBUPLOADU06" runat="server"/></td>
                                <td><asp:Label ID="LBUPLOADD06" runat="server"/></td>
                            </tr>
                            <tr><td>第五次修正本</td>
                                <td><asp:FileUpload ID="SFFile07_FileUpload" runat="server" style="height:25px;" />
                                    <asp:Button ID="Btn_UPSFile07" runat="server" Text="上傳檔案" style="height:25px;" OnClick="Btn_UPSFile_Click" />
                                    <asp:Button ID="Btn_DelSFile07" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" style="height:25px;" OnClick="Btn_DelSFile_Click" />
                                    <asp:TextBox ID="TXTSFile07" runat="server" Width="70px" Visible="False" /></td>
                                <td style="color:blue">
                                    <asp:HyperLink ID="SFLINK07" runat="server" Target="_blank"/></td>
                                <td><asp:Label ID="LBUPLOADU07" runat="server"/></td>
                                <td><asp:Label ID="LBUPLOADD07" runat="server"/></td>
                            </tr>
                        </tbody></table>
                        <span style="color:red; margin-top:0.5em; display:inline-block;">※ 上傳格式限定為PDF，檔案大小請於150mb以內</span>
                    </td>
                </tr>

                 <tr>
                    <td>
                        <div style="text-align:left; font-size:14pt; margin-top:1em;">相關檔案交換區</div>
                        <table class="filebase">
                            <tbody><tr>
                                <th>序號</th>
                                <th style="width:480px;">相關檔案上傳</th>
                                <th>連結</th>
                                <th style="width:13%">上傳人</th>
                                <th style="width:15%">上傳日期</th>
                            </tr>
                            <tr><td>01</td>
                                <td><asp:FileUpload ID="SFFile04_FileUpload" runat="server" style="height:25px;" />
                                    <asp:Button ID="Btn_UPSFile04" runat="server" Text="上傳檔案" style="height:25px;" OnClick="Btn_UPSFile_Click" />
                                    <asp:Button ID="Btn_DelSFile04" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" style="height:25px;" OnClick="Btn_DelSFile_Click" />
                                    <asp:TextBox ID="TXTSFile04" runat="server" Width="70px" Visible="False" /></td>
                                <td style="color:blue; text-decoration:underline;">
                                    <asp:HyperLink ID="SFLINK04" runat="server" Target="_blank"/></td>
                                <td><asp:Label ID="LBUPLOADU04" runat="server"/></td>
                                <td><asp:Label ID="LBUPLOADD04" runat="server"/></td>
                            </tr>
                            <tr><td>02</td>
                                <td><asp:FileUpload ID="SFFile05_FileUpload" runat="server" style="height:25px;" />
                                    <asp:Button ID="Btn_UPSFile05" runat="server" Text="上傳檔案" style="height:25px;" OnClick="Btn_UPSFile_Click" />
                                    <asp:Button ID="Btn_DelSFile05" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" style="height:25px;" OnClick="Btn_DelSFile_Click" />
                                    <asp:TextBox ID="TXTSFile05" runat="server" Width="70px" Visible="False" /></td>
                                <td style="color:blue; text-decoration:underline;">
                                    <asp:HyperLink ID="SFLINK05" runat="server" Target="_blank"/></td>
                                <td><asp:Label ID="LBUPLOADU05" runat="server"/></td>
                                <td><asp:Label ID="LBUPLOADD05" runat="server"/></td>
                            </tr>
                        </tbody></table>
                        <span style="color:red;margin-top:0.5em; display:inline-block;">※ 上傳格式限定為PDF、JPG、PNG，檔案大小請於50mb以內</span>
                    </td>
                </tr>
                 
            </tbody></table>
                </div>
            </asp:Panel>

            <asp:Panel ID="Area02" runat="server" CssClass="none">
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
                    <tr><td class="middle">審查費金額</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC035" runat="server" Visible="false"/>
                                            <asp:GridView ID="GVPay01" runat="server" AutoGenerateColumns="False" CssClass="constructionPurple" DataSourceID="SqlDataSource01">
                                                <Columns>
                                                    <asp:BoundField DataField="FD004" HeaderText="繳費單號" />
                                                    <asp:BoundField DataField="FD002" HeaderText="繳款期限" />
                                                    <asp:BoundField DataField="FD003" HeaderText="應納金額" />
                                                    <asp:BoundField DataField="CPI004" HeaderText="繳費日期" />
                                                    <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnPrint" runat="server" CommandArgument='<%#Container.DataItemIndex %>' OnClick="btnPrint01_Click" Text="列印繳費單" />
                                                            <asp:Label ID="LBCSMSG" runat="server" Text='<%# Bind("FD006") %>' Visible="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="HDKID" runat="server" Value='<%# Bind("FD001") %>' />
                                                            <asp:HiddenField ID="HDACT" runat="server" Value='<%# Bind("FD005") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
       <asp:SqlDataSource ID="SqlDataSource01" runat="server" ConnectionString="<%$ ConnectionStrings:SWCConnStr %>" OnSelected="SqlDataSource01_Selected"
           SelectCommand=" select BillID as FD001,CONVERT(varchar(100), CPI002, 23) as FD002,CPI003 as FD003, BillID as FD004,CaseID3 as FD005,CPI006 as FD006,CPI007,CPI004 from tslm2.dbo.CasePaymentInfo where CaseType = '審查費' and CPI006='已列印' order by id; "></asp:SqlDataSource>

                        <a href="https://smis.swcb.gov.tw/MainSys/WinInfo/WinCalCharge.aspx" target="_blank">試算連結</a>

                        </td></tr>
                    <tr><td>審查費繳納期限</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC031" runat="server"/></td></tr>
                    <tr><td>審查費繳納日期</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC033" runat="server"/></td></tr>
                    </table>
                </div>
            </asp:Panel>

            <asp:Panel ID="Area03" runat="server" CssClass="none">
                <h2 class="detailsBar_title_review">審查<img src="../images/btn/btn-close.png" alt="" class="open"/></h2>
                <div class="detailsGrid_wrap">
                    <table class="detailsGrid_green">
                    <tr><td style="background: #d9d9d9;">受理日期</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC034" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">審查單位</td>
                        <td class="detailsGrid_gray">
                            <asp:Label ID="LBSWC022" runat="server"/>
                            <asp:Label ID="LBSWC022ID" runat="server" Visible="false" /></td></tr>
                    <tr><td>審查小組</td>
                        <td>召集人：
                            <asp:DropDownList ID="DDLSA01" runat="server" style="height:25px;" />
                            <asp:Label ID="LBSAOID" runat="server" style="height:25px;" Visible="false" /><br/><br/>
                            委　員：
                            <asp:DropDownList ID="DDLSA02" runat="server" style="height:25px;" />
                            <asp:DropDownList ID="DDLSA03" runat="server" style="height:25px;" />
                            <asp:DropDownList ID="DDLSA04" runat="server" style="height:25px;" />
                            <asp:DropDownList ID="DDLSA05" runat="server" style="height:25px;" />
                            <asp:DropDownList ID="DDLSA06" runat="server" style="height:25px;" />
                            <asp:DropDownList ID="DDLSA07" runat="server" style="height:25px;" />
                            <asp:DropDownList ID="DDLSA08" runat="server" style="height:25px;" />
                            <asp:DropDownList ID="DDLSA09" runat="server" style="height:25px;" />
                            <asp:DropDownList ID="DDLSA10" runat="server" style="height:25px;" /><br/><br/>
                            審查會議日期：
                            <asp:TextBox ID="TXTSWC113" runat="server" width="120px" autocomplete="off"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTSWC113_CalendarExtender" runat="server" TargetControlID="TXTSWC113" Format="yyyy-MM-dd"></asp:CalendarExtender>
                            <asp:CheckBox ID="CHKSendMail01" runat="server" Text="於存檔後發信通知委員" />
                        </td>
                    </tr>
                    <tr><td>審查委員</td>
                        <td><asp:TextBox ID="TXTSWC087" runat="server" width="100%" Height="100px" TextMode="MultiLine" onkeyup="textcount(this,'TXTSWC087_count','255');" />
                            <asp:Label ID="TXTSWC087_count" runat="server" Text="(0/255)" ForeColor="Red"></asp:Label></td></tr>

                    <tr><td>審查紀錄<br/><%--<span>(鎖審查單位填)</span>--%></td>
                        <td><asp:Button ID="DT001" runat="server" Text="+ 審查紀錄" CssClass="detailsGrid_wrap_btn" OnClick="DT001_Click"/>
                            <asp:GridView ID="SWCDTL01" runat="server" CssClass="greenPause" AutoGenerateColumns="False" 
                                OnRowDataBound="SWCDTL01_RowDataBound"
                                    >
                                <Columns>
                                    <asp:BoundField DataField="DTLA001" HeaderText="審查表單編號" />
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
                <tr><td>公會建議核定/不予核定<br>日期<</td>
                    <td><asp:Label ID="TXTSWC109" runat="server" width="120px"></asp:Label>
                            <asp:TextBox ID="TBSWC109o" runat="server" style="display:none;" /></td></tr>
                <tr><td>建議核定補正期限<b/r>日期<</td>
                    <td><asp:Label ID="LBSWC125" runat="server" width="120px"></asp:Label>
                            <asp:TextBox ID="TBSWC125" runat="server" style="display:none;" /></td></tr>
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
                    <td class="detailsGrid_gray"><asp:Label ID="LBSWC040" runat="server" Visible="false"/>

                                            <asp:GridView ID="GVPay02" runat="server" AutoGenerateColumns="False"  CssClass="greenPause" DataSourceID="SqlDataSource02">
                                                <Columns>
                                                    <asp:BoundField DataField="FD004" HeaderText="繳費單號" />
                                                    <asp:BoundField DataField="FD002" HeaderText="繳款期限" />
                                                    <asp:BoundField DataField="FD003" HeaderText="應納金額" />
                                                    <asp:BoundField DataField="CPI004" HeaderText="繳費日期" />
                                                    <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnPrint" runat="server" CommandArgument='<%#Container.DataItemIndex %>' OnClick="btnPrint_Click" Text="列印繳費單" />
                                                            <asp:Label ID="LBCSMSG" runat="server" Text='<%# Bind("FD006") %>' Visible="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="HDKID" runat="server" Value='<%# Bind("FD001") %>' />
                                                            <asp:HiddenField ID="HDACT" runat="server" Value='<%# Bind("FD005") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
       <asp:SqlDataSource ID="SqlDataSource02" runat="server" ConnectionString="<%$ ConnectionStrings:SWCConnStr %>" OnSelected="SqlDataSource02_Selected"
           SelectCommand=" select BillID as FD001,CONVERT(varchar(100), CPI002, 23) as FD002,CPI003 as FD003, BillID as FD004,CaseID3 as FD005,CPI006 as FD006,CPI007,CPI004 from tslm2.dbo.CasePaymentInfo where CaseType = '保證金' and CPI006='已列印' order by id; "></asp:SqlDataSource>

                    
                    <a href="https://tcgeswc.taipei.gov.tw/swcmoneyinfo.aspx#a" target="_blank">試算連結</a>
                    </td></tr>
                <tr><td style="background: #d9d9d9;">審查費核銷</td>
                    <td class="detailsGrid_gray"><asp:Label ID="LBSWC036" runat="server"/></td></tr>
                    </table>
                </div>
            </asp:Panel>

            <asp:Panel ID="Area04" runat="server" CssClass="none">
                <h2 class="detailsBar_title_construction">施工<img src="../images/btn/btn-close.png" alt="" class="open"/></h2>
                <div class="detailsGrid_wrap">
                    <table class="detailsGrid_purple2">
                    <tr><td style="background: #d9d9d9;">開工期限</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC082" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">開工展延次數</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC083" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">保證金繳納</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC041" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">目的事業主管機關核定完工期限</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC112" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">施工許可證核發日期</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC043" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">施工許可證核發文號</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC044" runat="server"/></td></tr>
                    <tr><td>開工日期</td>
                        <td><asp:TextBox ID="TXTSWC051" runat="server" width="120px"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTSWC051_CalendarExtender" runat="server" TargetControlID="TXTSWC051" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                    <tr><td style="background: #d9d9d9;">核定完工日期</td>
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
                        
                    <tr><td>檢查委員</td>
                        <td>委員：
                            <asp:DropDownList ID="DDLSB01" runat="server" style="height:25px;" />
                            <asp:DropDownList ID="DDLSB02" runat="server" style="height:25px;" /><br/><br/>
                            施工檢查日期：
                            <asp:TextBox ID="TXTSWC114" runat="server" width="120px" autocomplete="off"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTSWC114_CalendarExtender" runat="server" TargetControlID="TXTSWC114" Format="yyyy-MM-dd"></asp:CalendarExtender>
                            <asp:CheckBox ID="CHKSendMail02" runat="server" Text="於存檔後發信通知委員" /><br/><br/>
                            完工檢查日期：
                            <asp:TextBox ID="TXTSWC115" runat="server" width="120px" autocomplete="off"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTSWC115_CalendarExtender" runat="server" TargetControlID="TXTSWC115" Format="yyyy-MM-dd"></asp:CalendarExtender>
                            <asp:CheckBox ID="CHKSendMail03" runat="server" Text="於存檔後發信通知委員" />
                        </td>
                    </tr>



                    <tr><td>施工中監督檢查紀錄<%--<span>(鎖檢查單位(施工檢查紀錄表)、機關人員(施工抽查紀錄表)填)</span>--%></td>
                        <td><asp:Button ID="DT003" runat="server" Text="+ 施工檢查紀錄表" CssClass="detailsGrid_wrap_btn" OnClick="DT003_Click"/>
                            <asp:HyperLink ID="DT002" runat="server" Text="+ 水保抽查紀錄表" CssClass="detailsGrid_wrap_btn" Visible="false"/>
                            <asp:Button ID="Button1" runat="server" Text="+ 輸出空白表單" CssClass="detailsGrid_wrap_btn" OnClick="Button1_Click"/>
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
                    <tr><td style="background: #d9d9d9;">空拍影像套疊</td>
                        <td class="detailsGrid_gray">
							<!--<asp:HyperLink ID="OutLink1" runat="server" target="_blank" NavigateUrl="https://data.geodac.tw/TCGEO/" Text="空拍影像套疊" />-->
							<asp:LinkButton id="LB1" OnClick="LB1_Click" runat="server" Text="空拍影像套疊"></asp:LinkButton></td></tr>
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

            <asp:Panel ID="Area05" runat="server" CssClass="none">
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
                            <asp:Label ID="SendMsg" runat="server" Text="資料傳送中..." Style="display:none;"></asp:Label>
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
                    <span class="span2">建議使用IE11(含)以上，Chrome或Firefox版本瀏覽器　<b>資料更新：</b><asp:Label ID="ToDay" runat="server" Text=""/>　<b>來訪人數：</b><asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                    <span class="span2"><b>客服電話：</b>02-27929328 陳小姐　<b>信箱：</b>tcge7@geovector.com.tw　本系統由多維空間資訊有限公司開發維護 TEL：(02)27929328</span><br/>
			        <span class="span2">※為維護系統服務品質，本平台訂於每周三凌晨AM 4:00-6:30 進行系統維護更新，更新期間偶有瞬斷情形，敬請使用者避開該時段使用。謝謝！</span></p>
            </div>

        </div>

        <asp:Literal ID="error_msg" runat="server"></asp:Literal>

        <script src="../js/jquery-3.1.1.min.js"></script>
        <script src="../js/inner.js"></script>
        <script src="../js/BaseNorl.js"></script>
        <script src="../js/allhref.js"></script>
        <script src="../js/NorJs.js"></script>
		<script src="../js/search.js"></script>
		<script>
			$(function (){
				$(".select").select2({
					language: 'zh-TW',
					width: '20%',
					maximumInputLength: 10,
					minimumInputLength: 0,
					tags: false,
					placeholder: "請選擇",
					allowClear : true,
				});
			})
		</script>
        <script type="text/javascript">
            document.getElementById("TBSWC109o").value = document.getElementById("SWC109").value;
        </script>
    </div>
    </form>
</body>
</html>
