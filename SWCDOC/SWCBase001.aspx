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
    
    <script type="text/javascript">
        function chkInput(jChkType) {
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

            jCHKValueExpDate = document.getElementById("TXTETCOPCExp").value;

            if (jCHKValueExpDate.trim() != "") {
                vvv = chkDateTodayAfter(jCHKValueExpDate);

                if (vvv == 'N') {
                    alert('執照日期已過期，請輸入有效之執照日期');
                    return false;
                }
            }
            
            //jCHKValueDate01 = document.getElementById("TXTTCNo01ED").value;
            //jCHKValueDate02 = document.getElementById("TXTTCNo02ED").value;
            //jCHKValueDate03 = document.getElementById("TXTTCNo03ED").value;
            //jCHKValueDate04 = document.getElementById("TXTTCNo04ED").value;

            //if (jCHKValueDate01.trim() != "") {
            //    vvv = chkDateTodayAfter(jCHKValueDate01);

            //    if (vvv == 'N') {
            //        alert('證書日期已過期，請輸入有效之證書日期');
            //        return false;
            //    }
            //}
            //if (jCHKValueDate02.trim() != "")
            //{
            //    vvv = chkDateTodayAfter(jCHKValueDate02);

            //    if (vvv == 'N') {
            //        alert('證書日期已過期，請輸入有效之證書日期');
            //        return false;
            //    }
            //}
            //if (jCHKValueDate03.trim() != "") {
            //    vvv = chkDateTodayAfter(jCHKValueDate03);

            //    if (vvv == 'N') {
            //        alert('證書日期已過期，請輸入有效之證書日期');
            //        return false;
            //    }
            //}
            //if (jCHKValueDate04.trim() != "") {
            //    vvv = chkDateTodayAfter(jCHKValueDate04);

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
                        <li><a href="http://tcgeswc.taipei.gov.tw/index_new.aspx" title="水土保持計畫查詢系統" target="_blank">水土保持計畫查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="true"><li>|&nbsp&nbsp&nbsp&nbsp<a href="http://172.28.100.55/TSLM" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
                        <asp:Panel ID="TitleLink01" runat="server" Visible="false" CssClass="last-divLi"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li></asp:Panel>
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
            <div class="detailsMenu"><asp:Image ID="Image1" runat="server" ImageUrl="../images/title/title-apply.png" /></div>
                <div class="applyGrid">
                    <h2 class="detailsBar_title_basic">基本資料</h2>
          
                    <table>
                    <tr><td>姓名<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTETName" runat="server" style="width:200px;" MaxLength="20"/>
                            <asp:TextBox ID="TXTACTION" runat="server" style="width:200px;display:none;" />
                        </td></tr>
                    <tr><td>身分證字號<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTETIDNo" runat="server" MaxLength="10" style="width:200px;"/>
                            <asp:label ID="LBETIDNo" runat="server"/>
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
                        <td><asp:TextBox ID="TXTETPWOLD" runat="server" TextMode="Password" onkeyup="JsChkInputBox(this,'02');" MaxLength="20" /></td></tr>                          
                    </asp:Panel>                  
                    <tr><td><asp:label ID="LBPWNew1" runat="server" Text ="新密碼"/><span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTETPW" runat="server" TextMode="Password" onkeyup="JsChkInputBox(this,'02');" MaxLength="20" /></td></tr>
                    <tr><td><asp:label ID="LBPWNew2" runat="server" Text ="新密碼確認"/><span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTETPWChk" runat="server" TextMode="Password" onkeyup="JsChkInputBox(this,'02');" MaxLength="20" /></td></tr>
                    </table>
                    </div>

                    <table class="cgPassn">
                    <tr><td>手機<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTETTel" runat="server" style="width:200px;" MaxLength="10" onkeyup="chknumber(this);"/></td></tr>
                    <tr><td>電子信箱<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTETEmail" runat="server" style="width:200px;" MaxLength="30"/></td></tr>
                    <tr><td>通訊地址<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTETOrgAddr" runat="server" style="width:400px;" MaxLength="50"/></td></tr>
                    <tr><td>執業機構名稱<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTETOrgName" runat="server" style="width:200px;" MaxLength="20"/></td></tr>
                    <tr><td>執業機構統一編號</td>
                        <td><asp:TextBox ID="TXTETOrgGUINo" runat="server" style="width:200px;" MaxLength="8"/></td></tr>
                    <tr><td>執業執照字號</td>
                        <td><asp:TextBox ID="TXTETOrgIssNo" runat="server" style="width:200px;" MaxLength="20"/></td></tr>
                    <tr><td>執業機構電話</td>
                        <td><asp:TextBox ID="TXTETOrgTel" runat="server" style="width:200px;" MaxLength="20"/></td></tr>
                    <tr><td>執業執照證號</td>
                        <td><asp:TextBox ID="TXTETCOPC" runat="server" style="width:200px;" MaxLength="20"/>
                            ，到期日
                            <asp:TextBox ID="TXTETCOPCExp" runat="server" width="120px"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTETCOPCExp_CalendarExtender" runat="server" TargetControlID="TXTETCOPCExp" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                    <tr><td>技師證書證號</td>
                        <td class="applyGrid_imgadd">水土保持技師：
                            <asp:TextBox ID="TXTETTCNo01" runat="server" Visible="false" />
                            <asp:Image ID="TXTETTCNo01_img" runat="server" Width="200" />

                            <div style="display:none;">到期日
                            <asp:TextBox ID="TXTTCNo01ED" runat="server" width="120px"></asp:TextBox>，
                            <asp:CalendarExtender ID="TXTTCNo01ED_CalendarExtender" runat="server" TargetControlID="TXTTCNo01ED" Format="yyyy-MM-dd"></asp:CalendarExtender>
                            </div>

                            <asp:FileUpload ID="TXTETTCNo01_FileUpload" runat="server"  />
                            <asp:Button ID="TXTETTCNo01_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTETTCNo01_fileuploadok_Click" />
                            <asp:Button ID="TXTETTCNo01_fileuploaddel" runat="server" Text="x" ToolTip="上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTETTCNo01_fileuploaddel_Click" />
                            <span style="color:red;">※ 檔案大小請小於 10Mb，請上傳 jpg, png 格式圖檔</span><br/><br/>

                            土木工程技師：<asp:TextBox ID="TXTETTCNo02" runat="server" Visible="false"/>
                            <asp:Image ID="TXTETTCNo02_img" runat="server" Width="200" />

                            <div style="display:none;">到期日
                            <asp:TextBox ID="TXTTCNo02ED" runat="server" width="120px"></asp:TextBox>，
                            <asp:CalendarExtender ID="TXTTCNo02ED_CalendarExtender" runat="server" TargetControlID="TXTTCNo02ED" Format="yyyy-MM-dd"></asp:CalendarExtender>
                            </div>

                            <asp:FileUpload ID="TXTETTCNo02_FileUpload" runat="server"  />
                            <asp:Button ID="TXTETTCNo02_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTETTCNo02_fileuploadok_Click" />
                            <asp:Button ID="TXTETTCNo02_fileuploaddel" runat="server" Text="x" ToolTip="上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTETTCNo02_fileuploaddel_Click" />
                            <span style="color:red;">※ 檔案大小請小於 10Mb，請上傳 jpg, png 格式圖檔</span><br/><br/>

                            水利工程技師：<asp:TextBox ID="TXTETTCNo03" runat="server" Visible="false"/>
                            <asp:Image ID="TXTETTCNo03_img" runat="server" Width="200" />
                            
                            <div style="display:none;">到期日
                            <asp:TextBox ID="TXTTCNo03ED" runat="server" width="120px"></asp:TextBox>，
                            <asp:CalendarExtender ID="TXTTCNo03ED_CalendarExtender" runat="server" TargetControlID="TXTTCNo03ED" Format="yyyy-MM-dd"></asp:CalendarExtender>
                            </div>

                            <asp:FileUpload ID="TXTETTCNo03_FileUpload" runat="server"  />
                            <asp:Button ID="TXTETTCNo03_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTETTCNo03_fileuploadok_Click" />
                            <asp:Button ID="TXTETTCNo03_fileuploaddel" runat="server" Text="x" ToolTip="上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTETTCNo03_fileuploaddel_Click" />
                            <span style="color:red;">※ 檔案大小請小於 10Mb，請上傳 jpg, png 格式圖檔</span><br/><br/>

                            大地工程技師：<asp:TextBox ID="TXTETTCNo04" runat="server" Visible="false"/>
                            <asp:Image ID="TXTETTCNo04_img" runat="server" Width="200" />
                                
                            <div style="display:none;">到期日
                            <asp:TextBox ID="TXTTCNo04ED" runat="server" width="120px"></asp:TextBox>，
                            <asp:CalendarExtender ID="TXTTCNo04ED_CalendarExtender" runat="server" TargetControlID="TXTTCNo04ED" Format="yyyy-MM-dd"></asp:CalendarExtender>
                            </div>

                            <asp:FileUpload ID="TXTETTCNo04_FileUpload" runat="server"  />
                            <asp:Button ID="TXTETTCNo04_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTETTCNo04_fileuploadok_Click" />
                            <asp:Button ID="TXTETTCNo04_fileuploaddel" runat="server" Text="x" ToolTip="上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTETTCNo04_fileuploaddel_Click" />
                            <span style="color:red;">※ 檔案大小請小於 10Mb，請上傳 jpg, png 格式圖檔</span><br/><br/>

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
            本人已詳閱並同意 <a href="../PriPage/personal_policy.aspx" target="_blank">使用者規則</a> 及 <a href="../PriPage/service_policy.aspx" target="_blank">隱私權保護政策</a>。 </span>




            

            <div class="checklist-btn">
                <asp:Button ID="GoHomePage" runat="server" Text="回首頁" OnClick="GoHomePage_Click" />&nbsp&nbsp
                <asp:Button ID="AddNewAcc" runat="server" Text="申請帳號" OnClientClick="return chkInput('');" OnClick="AddNewAcc_Click" Enabled="false" />
                <asp:Button ID="SaveAccount" runat="server" Text="存檔" OnClientClick="return chkInput('');" OnClick="SaveAccount_Click" />
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
                    <span class="span2">建議使用IE11(含)以上，Chrome或Firefox版本瀏覽器 資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                    <span class="span2">客服電話：02-27593001#3718 許先生 本系統由多維空間資訊有限公司開發維護 TEL:(02)27929328</span></p>
            </div>

        <asp:Literal ID="error_msg" runat="server"></asp:Literal>

        <script src="../js/jquery-3.1.1.min.js"></script>
        <script src="../js/inner.js"></script>
        <script src="../js/allhref.js"></script>
    </div>
    </form>
</body>
</html>
