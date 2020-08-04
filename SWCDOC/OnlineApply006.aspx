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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineApply006.aspx.cs" Inherits="SWCDOC_OnlineApply006" %>
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
    <link rel="stylesheet" type="text/css" href="../css/OnlineApply001.css"/>
    
    <script type="text/javascript">
        function chkInput(jChkType) {
            if (document.getElementById('CHKONA002').checked == true || document.getElementById('CHKONA007').checked == true || document.getElementById('CHKONA011').checked == true)
            {
                if (document.getElementById('CHKONA002').checked)
                {
                    if (document.getElementById("TXTONA003").value == '')
                    {
                        alert('請輸入義務人名稱');
                        return false;
                    }
                    if (document.getElementById("TXTONA004").value == '') {
                        alert('請輸入義務人身分證');
                        return false;
                    }
                    if (document.getElementById("TXTONA005").value == '') {
                        alert('請輸入義務人電話');
                        return false;
                    }
                    if (document.getElementById("TXTONA006").value == '') {
                        alert('請上傳目的事業主管機關核准變更文件');
                        return false;
                    }
                }
                if (document.getElementById('CHKONA007').checked)
                {
                    if ($('#DDLDTL008').val() == '')
                    {
                        alert('請選擇技師名稱');
                        return false;
                    }
                    if (document.getElementById("TXTONA009").value == '') {
                        alert('請輸入職業證照');
                        return false;
                    }
                    if (typeof Link010 == 'object') { } else
                    {
                        alert('請上傳監造契約等影本');
                        return false;
                    }
                }
                if (document.getElementById('CHKONA011').checked)
                {
                    if ($('#DDLDTL012').val() == '') {
                        alert('請選擇技師名稱');
                        return false;
                    }
                    if (document.getElementById("TXTONA013").value == '') {
                        alert('請輸入職業證照');
                        return false;
                    }
                    if (typeof Link014 == 'object') { } else
                    {
                        alert('請上傳監造契約等影本');
                        return false;
                    }
                }
            }
            else
            {
                alert('請選擇變更義務人或變更技師');
                return false;
            }

            if (jChkType == 'DataLock') {
                var r = confirm('確認送出後，即不可修改，請再次確認是否要完成送出。');
                return r;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
    <div class="wrap-s">
        <div class="header-wrap-s">
            <div class="header header-s clearfix"><a href="SWC001.aspx" class="logo-s"></a>
                <div class="header-menu-s">
                    <ul>
                        <li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
                        <li>|</li>
                        <li><a href="http://tcgeswc.taipei.gov.tw/index_new.aspx" title="水土保持計畫查詢系統" target="_blank">水土保持計畫查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="http://172.28.100.55/TSLM" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
                        <asp:Panel ID="TitleLink00" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWCBase001.aspx" title="帳號管理">帳號管理</a></li></asp:Panel>
                        <asp:Panel ID="LogOutLink" runat="server" Visible="true"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li></asp:Panel>
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
            <div class="review form">
                <h1>水土保持計畫義務人及技師變更報備<br/><br/></h1>

                <table class="review-out">
                <tr><td>義務人及技師變更報備編號</td>
                    <td><asp:Label ID="TXTONA001" runat="server" Visible="true"/></td></tr>
                <tr><td style="width: 19%">水保局編號</td>
                    <td><asp:Label ID="LBSWC002" runat="server"/>
                        <asp:Label ID="LBSWC000" runat="server" Visible="false"/></td></tr>
                <tr><td>計畫名稱</td>
                    <td><asp:Label ID="LBSWC005" runat="server"/></td></tr>
                <tr><td rowspan="3">變更資訊</td>
                    <td style="line-height:40px;">
                        <asp:CheckBox ID="CHKONA002" runat="server" Text ="變更義務人" /><br/>
                        義務人名稱：
                        <asp:TextBox ID="TXTONA003" runat="server" autocomplete="off"></asp:TextBox><br/>
                        身分證：
                        <asp:TextBox ID="TXTONA004" runat="server" autocomplete="off" style="margin-left:33px;"></asp:TextBox><br/>
                        電話：
                        <asp:TextBox ID="TXTONA005" runat="server" autocomplete="off" style="margin-left:49px;"></asp:TextBox><br/>
                        檢附目的事業主管機關核准變更文件：<br/>
                        <asp:TextBox ID="TXTONA006" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTONA006_fileupload" runat="server" />
                        <asp:Button ID="TXTONA006_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA006_fileuploadok_Click" />
                        <asp:Button ID="TXTONA006_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA006_fileuploaddel_Click" />
                        <br/><asp:HyperLink ID="Link006" runat="server" />
                        <br/><span style="color:red;">※ 上傳格式限定為pdf、odt或doc檔案，大小請於50mb以內</span></td></tr>
                <tr><td style="line-height:40px; font-weight:normal">
                        <asp:CheckBox ID="CHKONA007" runat="server" Text ="變更承辦技師" /><br/>
                        技師名稱：
                        <asp:DropDownList ID="DDLDTL008" runat="server" Height="25px"/>
                        <span style="color:red;">※ 若於清單中找不到所屬技師，請確認技師是否已申請本平台帳號。</span><br/>
                        執業執照字號：
                        <asp:TextBox ID="TXTONA009" runat="server" autocomplete="off" style="margin-left:8px;"></asp:TextBox><br/>
                        監造契約：<br/>
                        <asp:TextBox ID="TXTONA010" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTONA010_fileupload" runat="server" />
                        <asp:Button ID="TXTONA010_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA010_fileuploadok_Click" />
                        <asp:Button ID="TXTONA010_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA010_fileuploaddel_Click" />
                        <br/><asp:HyperLink ID="Link010" runat="server" />
                        <br/><span style="color:red;">※ 上傳格式限定為pdf、odt或doc檔案，大小請於50mb以內</span></td></tr>
                <tr><td style="line-height:40px; font-weight:normal">
                        <asp:CheckBox ID="CHKONA011" runat="server" Text ="變更監造技師" /><br/>
                        技師名稱：
                        <asp:DropDownList ID="DDLDTL012" runat="server" Height="25px"/>
                        <span style="color:red;">※ 若於清單中找不到所屬技師，請確認技師是否已申請本平台帳號。</span><br/>
                        執業執照字號：
                        <asp:TextBox ID="TXTONA013" runat="server" autocomplete="off" style="margin-left:8px;"></asp:TextBox><br/>
                        監造契約：<br/>
                        <asp:TextBox ID="TXTONA014" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTONA014_fileupload" runat="server" />
                        <asp:Button ID="TXTONA014_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA014_fileuploadok_Click" />
                        <asp:Button ID="TXTONA014_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA014_fileuploaddel_Click" />
                        <br/><asp:HyperLink ID="Link014" runat="server" />
                        <br/><span style="color:red;">※ 上傳格式限定為pdf、odt或doc檔案，大小請於50mb以內</span></td></tr> </table>

                    <div class="form-btn">
                        <asp:Button ID="DataLock" runat="server" Text="確認送出" OnClientClick="return chkInput('DataLock');" OnClick="DataLock_Click" />&nbsp&nbsp
                        <asp:Button ID="SaveCase" runat="server" Text="暫時儲存" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" />&nbsp&nbsp
                        <asp:Button ID="GoHomePage" runat="server" Text="返回瀏覽案件" OnClick="GoHomePage_Click" />
                    </div>
                </div>
            </div>

            <div class="footer">
                 <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                           <span class="span2">110臺北市信義區松德路300號3樓 　服務專線(02)27591109   臺北市民當家熱線1999</span><br/>
                            <span class="span2">建議使用IE11(含)以上，Chrome或Firefox版本瀏覽器 資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                           <span class="span2">客服電話：02-27593001#3718 許先生 本系統由多維空間資訊有限公司開發維護 TEL:(02)27929328</span></p>
            </div>

        </div>

        <asp:Literal ID="error_msg" runat="server"></asp:Literal>


        <script src="../js/jquery-3.1.1.min.js"></script>
        <script src="../js/inner.js"></script>
        <script src="../js/allhref.js"></script>
    </div>
    </form>
</body>
</html>
