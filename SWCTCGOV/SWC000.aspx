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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWC000.aspx.cs" Inherits="SWCDOC_SWC000" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>臺北市水土保持申請書件管理平台</title>
    <link rel="Shortcut Icon" type="image/x-icon" href="../images/logo-s.ico">
    <meta name="keywords" content="臺北市水土保持申請書件管理平台, 書件管理平台">
    <meta name="description" content="臺北市水土保持申請書件管理平台">
    <meta name="author" content="dorathy">
    <meta name="copyright" content="© 2017 臺北市水土保持申請書件管理平台">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge, chrome=1">
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    <link rel="stylesheet" type="text/css" href="../css/reset.css"/>
    <link rel="stylesheet" type="text/css" href="../css/all.css"/>
    <link rel="stylesheet" type="text/css" href="../css/ad.css"/>
    
    <script src="../js/jquery-3.1.1.min.js"></script>
    <script src="../js/index.js"></script>
    <script type="text/javascript">
        function chkInput(jChkType) {
            var x = document.getElementById("loginChange");
            var jUserType = (x.options[x.selectedIndex].text);

            var jInputID = document.getElementById('TXTID').value;
            var jInputPW = document.getElementById('TXTPW').value;

            switch (jUserType) {
                case "水土保持義務人":

                    if (jInputID == '')
                    {
                        alert('請輸入身份證字號');
                        return false;
                    }
                    if (jInputPW == '') {
                        alert('請輸入手機');
                        return false;
                    }
                    if (jInputPW.length != 10 || isNaN(jInputPW)) {
                        alert('手機格式不正確，請輸入10位數字號碼');
                        return false;
                    }
                    break;

                case "承辦/監造技師":
                    document.getElementById('TXTID').value = jInputID.toUpperCase();

            }
        }
    </script>
</head>

<body>
    <form id="form1" runat="server">



        
   <div class="modal modal__overlay">

  <div class="modal__window">

     <div class="modal__label">
            <p>公佈欄</p>
     </div>
     <div class="modal__content">
      <span>公告內容：</span><br /><textarea class="bbd"></textarea>
     </div>

     <div class="modal__btn">
       <button type="button" class="btn closeModal"> 關閉進入頁面
       </button>
     </div>

     <button type="button" class="modal__close close-s btn-s closeModal">
       <span class="sr-only">Close</span>
       <i class="icon icon--close"></i>
     </button>

  </div> <!-- .modal__window-->

</div>


    <div>
    <div class="wrap-b">
      <div class="header header-b"><a href="SWC000.aspx" class="logo-b"></a>
        <div class="header-menu-b clearfix">
          <ul>
            <li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
            <li>|</li>
            <li><a href="http://tcgeswc.taipei.gov.tw/index_new.aspx" title="臺北市水土保持計畫查詢系統" target="_blank">臺北市水土保持計畫查詢系統</a></li>
            <li>|</li>
            <li><a href="http://www.geo.gov.taipei/" title="臺北市政府工務局大地工程處" target="_blank">臺北市政府工務局大地工程處</a></li>
          </ul>
        </div>
      </div>

      <div class="content-b">
        <div class="login"><span id="login1">身份</span>
            <asp:DropDownList ID="loginChange" runat="server" onchange="showChange(this.value);" /><br/><br/>
            <span id="login2">請輸入</span>
            <asp:TextBox ID="TXTID" runat="server" placeholder="身分證字號" MaxLength="10" /><br/><br/>
            <span id="login3">密碼</span>
            <asp:TextBox ID="TXTPW" runat="server" placeholder="手機" MaxLength="15" /><br/><br/><br/>
            <asp:HyperLink ID="NewUser" runat="server" Text="首次登入" />

            <asp:ImageButton ID="SwcLogin" runat="server" OnClientClick="return chkInput('');" OnClick="SwcLogin_Click" title="登入" ImageUrl="../images/btn/btn-login.png" CssClass="imgLogin"/>
        </div>
      </div>


            <div class="footer-b">
                <div class="footer-b-green"></div>
                    <div class="footer-bb-brown">
                        <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                           <span class="span2">110臺北市信義區松德路300號3樓 　服務專線(02)27591109   臺北市民當家熱線1999</span><br/>
                            <span class="span2">建議使用IE11(含)以上，Chrome或Firefox版本瀏覽器 資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                           <span class="span2">客服電話：02-27593001#3718 許先生 本系統由多維空間資訊有限公司開發維護 TEL:(02)27929328</span></p>
                    </div>
                </div>
                
            </div>
        
            <script type="text/javascript">
                var x = document.getElementById("loginChange");
                var str = x.options[x.selectedIndex].text;        
                
                showChange(str);
            </script>

            <asp:Literal ID="error_msg" runat="server"></asp:Literal>

        </div>
        <script src="../js/allhref.js"></script>

         <script src="../js/ad.js"></script>
        <script src="../js/ad-2.js"></script>
    </form>
    
</body>
</html>

