<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCBase001.aspx.cs" Inherits="SWCDOC_SWCBase001" %>
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
    <meta http-equiv="X-UA-Compatible" content="IE=Edge, chrome=1">
    <link rel="stylesheet" type="text/css" href="../css/reset.css"/>
    <link rel="stylesheet" type="text/css" href="../css/all.css"/>
    <link rel="stylesheet" type="text/css" href="../css/iris.css"/>
    
    <script type="text/javascript">
        function chkInput() {
            jUjs = document.getElementById("JSID").value;

            jCHKValue01 = document.getElementById("TXTETName").value;
            jCHKValue03 = document.getElementById("TXTETTel").value;
            jCHKValue04 = document.getElementById("TXTETEmail").value;
            jCHKValue05 = document.getElementById("TXTETOrgName").value;
            jCHKValue06 = document.getElementById("TXTETPW").value;
            jCHKValue07 = document.getElementById("TXTETPWChk").value;
            jCHKValue08 = document.getElementById("TXTETOrgAddr").value;

            if (jCHKValue01.trim() == '') {
                alert('請輸入姓名');
                return false;
            }
            if (jUjs == 'NEW') {
                jCHKValue02 = document.getElementById("TXTETIDNo").value; 

                if (jCHKValue02.trim() == '') {
                    alert('請輸入身份證號碼');
                    return false;
                }
                if (!checkID(jCHKValue02)) {
                    alert('請輸入合法身份證號碼');
                    return false;
                }
                if (jCHKValue06.trim() == '') {
                    alert('請輸入密碼');
                    return false;
                }
                if (jCHKValue07.trim() == '') {
                    alert('請輸入密碼確認');
                    return false;
                }
            } else {
                //if (jCHKValue08.trim() == '') {
                //    alert('請輸入密碼');
                //    return false;
                //}

            }
            if (jCHKValue03.trim() == '') {
                alert('請輸入手機');
                return false;
            }
            if (jCHKValue03.length != 10 || isNaN(jCHKValue03)) {
                alert('手機請輸入10位數字號碼');
                return false;
            }
            if (jCHKValue04.trim() == '') {
                alert('請輸入電子信箱');
                return false;
            }
            if (jCHKValue08.trim() == '') {
                alert('請輸入通訊地址');
                return false;
            }
            if (jCHKValue05.trim() == '') {
                alert('請輸入執業機構名稱');
                return false;
            }

            //jCHKValueExpDate = document.getElementById("TXTETCOPCExp").value;

            //if (jCHKValueExpDate.trim() != "") {
            //    vvv = chkDateTodayAfter(jCHKValueExpDate);

            //    if (vvv == 'N') {
            //        alert('執照日期已過期，請輸入有效之執照日期');
            //        return false;
            //    }
            //}
            jCHKValueDDLIdentity = document.getElementById("DDLIdentity").value;
            jCHKValueDate01 = document.getElementById("TXTTCNo01ED").value;
            jCHKValueDate01img = document.getElementById("TXTETTCNo01_img").src;
            jCHKValueDate02 = document.getElementById("TXTTCNo02ED").value;
            jCHKValueDate02img = document.getElementById("TXTETTCNo02_img").src;
            jCHKValueDate03 = document.getElementById("TXTTCNo03ED").value;
            jCHKValueDate03img = document.getElementById("TXTETTCNo03_img").src;
            jCHKValueDate04 = document.getElementById("TXTTCNo04ED").value;
            jCHKValueDate04img = document.getElementById("TXTETTCNo04_img").src;
			
			if (jCHKValueDDLIdentity == "技師" && !((jCHKValueDate01.trim() != '' && jCHKValueDate01img.trim() != '')||(jCHKValueDate02.trim() != '' && jCHKValueDate02img.trim() != '')||(jCHKValueDate03.trim() != '' && jCHKValueDate03img.trim() != '')||(jCHKValueDate04.trim() != '' && jCHKValueDate04img.trim() != ''))) {
                alert('請填寫至少一個技師執業執照');
                //document.getElementById("TXTTCNo01ED").focus();
                return false;
            }

            if (jCHKValueDate01.trim() != "") {
                vvv = chkDateTodayAfter(jCHKValueDate01);

                if (vvv == 'N') {
                    alert('證書日期已過期，請輸入有效之證書日期');
                    return false;
                }
            }
            if (jCHKValueDate02.trim() != "")
            {
                vvv = chkDateTodayAfter(jCHKValueDate02);

                if (vvv == 'N') {
                    alert('證書日期已過期，請輸入有效之證書日期');
                    return false;
                }
            }
            if (jCHKValueDate03.trim() != "") {
                vvv = chkDateTodayAfter(jCHKValueDate03);

                if (vvv == 'N') {
                    alert('證書日期已過期，請輸入有效之證書日期');
                    return false;
                }
            }
            if (jCHKValueDate04.trim() != "") {
                vvv = chkDateTodayAfter(jCHKValueDate04);

                if (vvv == 'N') {
                    alert('證書日期已過期，請輸入有效之證書日期');
                    return false;
                }
            }

            //jCHKValueDate05 = document.getElementById("TXTTCNo05ED").value;
            //
            //if (jCHKValueDate05.trim() != "") {
            //    vvv = chkDateTodayAfter(jCHKValueDate05);
            //    if (vvv == 'N') {
            //        alert('證書日期已過期，請輸入有效之證書日期');
            //        return false;
            //    }
            //}

            if (jUjs == 'NEW')
            {
                if (confirm("請再次確定填寫e-mail是否正確，帳號申請通過將寄信通知")) {
                } else {
               return false;
                }
            }
        }
        function chknumber(objElement) {
            var chrstring = objElement.value;
            var chr = chrstring.substring(chrstring.length - 1, chrstring.length);
            if ((chr != '.') && (chr != '0') && (chr != '1') && (chr != '2') && (chr != '3') && (chr != '4') && (chr != '5') && (chr != '6') && (chr != '7') && (chr != '8') && (chr != '9')) {
                objElement.value = objElement.value.substring(0, chrstring.length - 1);
            }
        }        
        function chkDateTodayAfter(jToday)
        {
            var Today = new Date();
            var jChkR = 'Y';

            var t = Date.parse(jToday);

            if(isNaN(t)) {
                //alert('請輸入日期格式');
                jChkR = 'N';
            }

            if ((Date.parse(jToday)).valueOf() < (Date.parse(Today)).valueOf()) {
                //alert("ScheduleDate比系統目前時間小");
                jChkR = 'N';
            }
            return jChkR;
        }
        function chkPrivacy(jPrivacy) {
            if (jPrivacy.checked)
            {
                document.getElementById("AddNewAcc").disabled = false;
            } else {
                document.getElementById("AddNewAcc").disabled = true;
            }            
        }
        function sp(test) {
            if (!checkID(test)) {
                alert('請輸入合法身份證號碼');
                    return false;
                }}
    </script>
    <script src="../js/BaseNorl.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"/>

    <div class="wrap-s">
        <div class="header-wrap-s">
            <div class="header header-s clearfix"><a href="SWC001.aspx" class="logo-s"></a>
                <div class="header-menu-s">
                    <ul>
                        <li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
                        <li>|</li>
                        <li><a href="https://swc.taipei/swcinfo/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="true"><li>|&nbsp&nbsp&nbsp&nbsp<a href="http://tslm.swc.taipei/tslmwork/" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
                        <asp:Panel ID="TitleLink01" runat="server" Visible="false" CssClass="last-divLi"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li></asp:Panel>
                    </ul>
                </div>
            </div>

            <div class="header-s-green">
                <div class="header-s-green-nameWrap">
                    <span><asp:Literal ID="TextUserName" runat="server">大雷包，您好</asp:Literal></span><input type="password" style="display:none" />
                </div>
            </div>
        </div>
        
        <div class="content-s">
            <div class="detailsMenu"><asp:Image ID="Image1" runat="server" ImageUrl="../images/title/title-apply.png" /></div>
                <div class="applyGrid">
                    <h2 class="detailsBar_title_basic">基本資料</h2>
          
                    <table>
					 <tr>
                      <td style="width:17.5%;">
                          身分別<span style="color: red;font-family:cursive;">＊</span></td>
                      <td><asp:DropDownList ID="DDLIdentity" runat="server" /></td></tr>
                    <tr><td>姓名<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTETName" runat="server" style="width:200px;" MaxLength="20"/>
                            <span class="gray">（範例：戴怡宛）</span>
                            <asp:TextBox ID="TXTACTION" runat="server" style="width:200px;display:none;" />
                        </td></tr>
                    <tr><td>身分證字號<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTETIDNo" runat="server" MaxLength="10" style="width:200px;" onchange="return sp(this.value);" />
                            <asp:label ID="LBETIDNo" runat="server"/>
                            <span class="gray">（範例：A234567890）</span>
                            <asp:TextBox ID="TXTSYSID" runat="server" Visible="false" />
                            <asp:TextBox ID="JSID" runat="server" MaxLength="10" style="display: none"/>
                            
                    <asp:Panel ID="CHGPW" runat="server" Visible="false">
                            <input type="button" value="修正密碼" class="cgPassBtn"/>                    
                    </asp:Panel>                  
                        </td></tr>
                    </table>

                    <div class="cgPass close">
                    <table >
                    <asp:Panel ID="OLDPWAREA" runat="server">
                    <tr><td><asp:label ID="LBPWOld" runat="server" Text ="舊密碼"/><span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTETPWOLD" runat="server" TextMode="Password" onkeyup="JsChkInputBox(this,'02');" MaxLength="20" />
                        </td></tr>                          
                    </asp:Panel>                  
                    <tr><td><asp:label ID="LBPWNew1" runat="server" Text ="新密碼"/><span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTETPW" runat="server" TextMode="Password" onkeyup="JsChkInputBox(this,'02');" MaxLength="20" /><br />
                            <span class="red">※ 密碼限制 8-16 個字元，設置的密碼必須包含英文及數字，英文大小寫視為不同字。請勿使用空白鍵</span></td></tr>
                    <tr><td><asp:label ID="LBPWNew2" runat="server" Text ="新密碼確認"/><span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTETPWChk" runat="server" TextMode="Password" onkeyup="JsChkInputBox(this,'02');" MaxLength="20" /><br />
                            <span class="red">※ 請再次輸入密碼</span></td></tr>
                    </table>
                    </div>

                    <table class="cgPassn">
                    <tr><td>手機<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTETTel" runat="server" style="width:200px;" MaxLength="10" onkeyup="chknumber(this);"/>
                            <span class="gray">（範例：0912345678）</span>
                        </td></tr>
                    <tr><td>電子信箱<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTETEmail" runat="server" style="width:400px;" MaxLength="30"/>
                            <span class="gray">（範例：abc@gmail.com）</span><br />
                            <span class="red">※ 請確實填寫電子郵件信箱，作為發送電子文件或其他訊息通知之用</span>
                        </td></tr>
                    <tr><td>通訊地址<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTETOrgAddr" runat="server" style="width:400px;" MaxLength="50"/>
                            <span class="gray">（範例：臺北市信義區松德路300號3樓）</span>
                        </td></tr>
                    <tr><td>執業機構名稱<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTETOrgName" runat="server" style="width:400px;" MaxLength="20"/>
                            <span class="gray">（範例：臺北市政府大地工程處）</span>
                        </td></tr>
                    <tr><td>執業機構統一編號<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTETOrgGUINo" runat="server" style="width:200px;" MaxLength="8"/>
                            <span class="gray">（範例：25652041）</span>
                        </td></tr>
                    <tr><td>執業執照字號<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTETOrgIssNo" runat="server" style="width:200px;" MaxLength="20"/>
                            <span class="gray">（範例：技執字第號）</span>
                        </td></tr>
                    <tr><td>執業機構地址<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTETOrgAddr2"  runat="server" style="width:400px;" MaxLength="50"/>
                            <span class="gray">（範例：臺北市信義區松德路300號3樓）</span>
                        </td></tr>
                    <tr><td>執業機構電話<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTETOrgTel" runat="server" style="width:200px;" MaxLength="20"/>
                            <span class="gray">（範例：0227593001）</span>
                        </td></tr>
<%--                    <tr><td>執業執照證號</td>
                        <td><asp:TextBox ID="TXTETCOPC" runat="server" style="width:200px;" MaxLength="20"/>
                            ，到期日
                            <asp:TextBox ID="TXTETCOPCExp" runat="server" width="120px"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTETCOPCExp_CalendarExtender" runat="server" TargetControlID="TXTETCOPCExp" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                        
                        --%>

                        <tr><td>執業執照<span style="color: red;font-family:cursive;">＊</span></td>
                        <td class="applyGrid_imgadd">水土保持技師：
                            <asp:TextBox ID="TXTETTCNo01" runat="server" Visible="false" />
                            <asp:Image ID="TXTETTCNo01_img" runat="server" Width="200" />

                            <div>到期日
                            <asp:TextBox ID="TXTTCNo01ED" runat="server" width="120px"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTTCNo01ED_CalendarExtender" runat="server" TargetControlID="TXTTCNo01ED" Format="yyyy-MM-dd"></asp:CalendarExtender>
                            </div>

                            <asp:FileUpload ID="TXTETTCNo01_FileUpload" runat="server"  />
                            <asp:Button ID="TXTETTCNo01_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTETTCNo01_fileuploadok_Click"  />
                            <asp:Button ID="TXTETTCNo01_fileuploaddel" runat="server" Text="x" ToolTip="上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTETTCNo01_fileuploaddel_Click" />
                            <span style="color:red;">※ 檔案大小請小於 10Mb，請上傳 jpg, png 格式圖檔</span><br/><br/>

                            土木工程技師：<asp:TextBox ID="TXTETTCNo02" runat="server" Visible="false"/>
                            <asp:Image ID="TXTETTCNo02_img" runat="server" Width="200" />

                            <div>到期日
                            <asp:TextBox ID="TXTTCNo02ED" runat="server" width="120px"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTTCNo02ED_CalendarExtender" runat="server" TargetControlID="TXTTCNo02ED" Format="yyyy-MM-dd"></asp:CalendarExtender>
                            </div>

                            <asp:FileUpload ID="TXTETTCNo02_FileUpload" runat="server"  />
                            <asp:Button ID="TXTETTCNo02_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTETTCNo02_fileuploadok_Click" />
                            <asp:Button ID="TXTETTCNo02_fileuploaddel" runat="server" Text="x" ToolTip="上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTETTCNo02_fileuploaddel_Click" />
                            <span style="color:red;">※ 檔案大小請小於 10Mb，請上傳 jpg, png 格式圖檔</span><br/><br/>

                            水利工程技師：<asp:TextBox ID="TXTETTCNo03" runat="server" Visible="false"/>
                            <asp:Image ID="TXTETTCNo03_img" runat="server" Width="200" />
                            
                            <div>到期日
                            <asp:TextBox ID="TXTTCNo03ED" runat="server" width="120px"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTTCNo03ED_CalendarExtender" runat="server" TargetControlID="TXTTCNo03ED" Format="yyyy-MM-dd"></asp:CalendarExtender>
                            </div>

                            <asp:FileUpload ID="TXTETTCNo03_FileUpload" runat="server"  />
                            <asp:Button ID="TXTETTCNo03_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTETTCNo03_fileuploadok_Click" />
                            <asp:Button ID="TXTETTCNo03_fileuploaddel" runat="server" Text="x" ToolTip="上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTETTCNo03_fileuploaddel_Click" />
                            <span style="color:red;">※ 檔案大小請小於 10Mb，請上傳 jpg, png 格式圖檔</span><br/><br/>

                            大地工程技師：<asp:TextBox ID="TXTETTCNo04" runat="server" Visible="false"/>
                            <asp:Image ID="TXTETTCNo04_img" runat="server" Width="200" />
                                
                            <div>到期日
                            <asp:TextBox ID="TXTTCNo04ED" runat="server" width="120px" autocomplete="off"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTTCNo04ED_CalendarExtender" runat="server" TargetControlID="TXTTCNo04ED" Format="yyyy-MM-dd"></asp:CalendarExtender>
                            </div>

                            <asp:FileUpload ID="TXTETTCNo04_FileUpload" runat="server"  />
                            <asp:Button ID="TXTETTCNo04_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTETTCNo04_fileuploadok_Click" style="height: 21px" />
                            <asp:Button ID="TXTETTCNo04_fileuploaddel" runat="server" Text="x" ToolTip="上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTETTCNo04_fileuploaddel_Click" Width="20px" />
                            <span style="color:red;">※ 檔案大小請小於 10Mb，請上傳 jpg, png 格式圖檔</span><br/><br/>

                        </td></tr>
                         <tr style="display:none;">
                            <td>公會會員證書<span style="color: red;font-family:cursive;">＊</span></td>
                            <td><asp:FileUpload ID="TXTETTCNo05_FileUpload" runat="server"  />

                                <asp:Button ID="TXTETTCNo05_fileuploadok" runat="server" Text="上傳照片" OnClick="TXTETTCNo05_fileuploadok_Click" style="height: 21px" />
                                <asp:Button ID="TXTETTCNo05_fileuploaddel" runat="server" Text="x" ToolTip="上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTETTCNo05_fileuploaddel_Click" Width="20px" />
                                <span style="color:red;">※ 檔案大小請小於 10Mb，請上傳 jpg, png 格式圖檔</span><br/>

                                有效日期：
                                <asp:TextBox ID="TXTTCNo05ED" runat="server" width="120px"></asp:TextBox>
                                <asp:CalendarExtender ID="TXTTCNo05ED_CalendarExtender" runat="server" TargetControlID="TXTTCNo05ED" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                <asp:Image ID="TXTETTCNo05_img" runat="server" Width="100" />
                                <asp:TextBox ID="TXTETTCNo05" runat="server" Visible="false"></asp:TextBox>
                            </td></tr>   
                         <tr>
                            <td>公會會員證書<span style="color: red;font-family:cursive;">＊</span></td>
                            <td><asp:DropDownList ID="DDLGuild" runat="server" /><br/><br/>

                                上傳公會會員證書：<asp:Image ID="GuildImage" runat="server" Width="200" /><br/>                                
                                <asp:TextBox ID="TxtGuildFile" runat="server" Visible="false"></asp:TextBox>
                                <asp:FileUpload ID="GuildFile_FileUpload" runat="server" />
                                <asp:Button ID="GuildFile_UpLoad" runat="server" Text="上傳檔案" OnClick="FileUpLoad_Click" style="height: 21px" OnClientClick="return sp(document.getElementById('TXTETIDNo').value);" />
                                <asp:Button ID="GuildFile_FileDel" runat="server" Text="x" ToolTip="上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" Width="20px" OnClick="FileDel_Click" />
                                <span style="color:red;">※ 檔案大小請小於 10Mb，請上傳 jpg, png 格式圖檔</span><br/><br/>

                                上傳技師審查經歷：
                                <asp:HyperLink ID="ExperienceLink" runat="server" Target="_blank"/><br/>                                
                                <asp:TextBox ID="TxtExperience" runat="server" Visible="false"></asp:TextBox>
                                <asp:FileUpload ID="Experience_FileUpload" runat="server"  />
                                <asp:Button ID="Experience_UpLoad" runat="server" Text="上傳檔案" style="height: 21px" OnClientClick="return sp(document.getElementById('TXTETIDNo').value);" OnClick="FileUpLoad_Click" />
                                <asp:Button ID="Experience_FileDel" runat="server" Text="x" ToolTip="上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" Width="20px" OnClick="FileDel_Click" />
                                <span style="color:red;">※ 檔案大小請小於 50Mb，請上傳 pdf 格式檔案</span><br/><br/>

                                有效日期：
                                <asp:TextBox ID="TXTEXP" runat="server" width="120px" autocomplete="off"></asp:TextBox>
                                <asp:CalendarExtender ID="TXTEXP_CalendarExtender" runat="server" TargetControlID="TXTEXP" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                <asp:Button ID="ADDLIST" runat="server" Text="加入清單" OnClick="ADDLIST_Click" />
                                <asp:TextBox ID="GuildCount" Text="0" runat="server" Visible="false"></asp:TextBox>

                                <br /><br />
                                
                                <asp:GridView ID="GuildList" runat="server" CssClass="startskyblue" AutoGenerateColumns="False" Height="50"
                                    OnRowCommand="GuildList_RowCommand" OnRowDataBound="GuildList_RowDataBound">
                                    <Columns>                                        
                                        <asp:BoundField DataField="NI" HeaderText="序號" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="GuildName" HeaderText="公會名稱" HeaderStyle-Width="30%" HtmlEncode="false" />
                                        <asp:HyperLinkField DataNavigateUrlFields="GuildFileLink" DataTextField="GuildText" HeaderText="公會會員證書" Target="_blank" />
                                        <asp:HyperLinkField DataNavigateUrlFields="ExperienceFileLink" DataTextField="ExperienceText" HeaderText="技師審查經歷" Target="_blank" />
                                        <asp:BoundField DataField="EXPDate" HeaderText="有效日期" HeaderStyle-Width="13%" />
                                        <asp:TemplateField Visible="true" >
                                            <ItemTemplate>
                                                <asp:Button id="BtnDel" runat="server" CommandArgument="<%#Container.DataItemIndex %>" CommandName="DelGuild" Text="刪除" OnClientClick="return confirm('確認刪除這筆資料?')" />
                                                <asp:HiddenField ID="GuildID" runat="server" Value='<%# Eval("GuildId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:TextBox ID="TextBoxGD01" Text="" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="TextBoxGD01Chk" Text="" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="TextBoxGD02" Text="" runat="server" Visible="false"></asp:TextBox>

                            </td></tr>   
                            
              <!--             <tr>
              <td>職名章圖像</td>
              <td>
                <input type="file" value="選擇檔案">
                <input type="button" value="上傳">
              </td>
            </tr> -->
          </table>
        </div>

            <br/>
                    <br>
                    <br>
                    <p>
                    <input type="checkbox" id="CheckBoxPrivacy" onclick="chkPrivacy(this);" />
            本人已詳閱並同意 <a href="../PriPage/service_policy.aspx" target="_blank">使用者規則</a> 及 <a href="../PriPage/personal_policy.aspx" target="_blank">隱私權保護政策</a>。 </span>



            <div class="checklist-btn">
                <asp:Button ID="GoHomePage" runat="server" Text="回首頁" OnClick="GoHomePage_Click" />&nbsp&nbsp
                <asp:Button ID="AddNewAcc" runat="server" Text="申請帳號" OnClientClick="return chkInput();" OnClick="AddNewAcc_Click" Enabled="false" />
                <asp:Button ID="SaveAccount" runat="server" Text="存檔" OnClientClick="return chkInput();" OnClick="SaveAccount_Click" />
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

        <asp:Literal ID="error_msg" runat="server"></asp:Literal>

        <script src="../js/jquery-3.1.1.min.js"></script>
        <script src="../js/inner.js"></script>
        <script src="../js/allhref.js"></script>
    </div>
    </form>
</body>
</html>
