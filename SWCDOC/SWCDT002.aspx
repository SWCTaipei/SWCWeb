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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCDT002.aspx.cs" Inherits="SWCDOC_SWCDT002" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
        function chkInput(jChkType) {
            jCHKValue01 = document.getElementById("TXTDTL002").value;
            jCHKValue02 = document.getElementById("TXTDTL003").value;
            jCHKValue03 = document.getElementById("TXTDTL004").value;
            //jCHKValue04 = document.getElementById("TXTSWC015").value;
            //jCHKValue05 = document.getElementById("TXTSWC016").value;

            if (jCHKValue01.trim() == '') {
                alert('請輸入審查日期');
                document.getElementById("TXTDTL002").focus();
                return false;
            }
            if (jCHKValue02.trim() == '') {
                alert('請輸入補正期限');
                document.getElementById("TXTDTL003").focus();
                return false;
            }
            if (jCHKValue03.trim() == '') {
                alert('請輸入主旨');
                document.getElementById("TXTDTL004").focus();
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
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"/>

    <div class="wrap-s">
      <div class="header-wrap-s">
        <div class="header header-s clearfix"><a href="SWC000.aspx" class="logo-s"></a>
          <div class="header-menu-s">
            <ul>
                    <li><a href="#" title="系統操作手冊">系統操作手冊</a></li>
                    <li>|</li>
                    <li><a href="http://tcgeswc.taipei.gov.tw/index_new.aspx" title="水土保持計畫查詢系統" target="_blank">水土保持計畫查詢系統 </a></li>
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
            <div class="swcchg form">
                <h1>水土保持施工抽查紀錄</h1><br/>

                <table class="swcchg-out">
                    <tr><td>施工監督表編號</td>
                        <td><asp:Label ID="LBDTL001" runat="server"/>
                            <asp:Label ID="LBSWC000" runat="server" Visible="false"/></td></tr>
                    <tr><td>檢查日期<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTDTL002" runat="server" width="120px"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTDTL002_CalendarExtender" runat="server" TargetControlID="TXTDTL002" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                    <tr><td>檢查類型</td>
                        <td><asp:Label ID="TXTDTL003" runat="server" Text="施工抽查紀錄"/></td></tr>
                    <tr><td>檢查單位</td>
                        <td><asp:Label ID="TXTDTL004" runat="server" width="100%" MaxLength="200" Text="臺北市政府工務局大地工程處"/></td></tr>
                </table>

                <br><br>

                <table>
                <tr><th>水土保持書件名稱</th>
                    <th><asp:Label ID="LBSWC005" runat="server"/></th></tr>
                <tr><td>一、檢查項目</td>
                    <td style="text-align:center;">現場情形</td></tr>
                <tr><td>（一）水土保持施工告示牌</td>
                    <td><asp:CheckBox ID="CHKDTL007" runat="server" Text="已設立無誤" /><br/>
                        <asp:CheckBox ID="CHKDTL008" runat="server" Text="未設立" /><br/>
                        <asp:CheckBox ID="CHKDTL009" runat="server" Text="資訊缺漏" />：
                        <asp:TextBox ID="TXTDTL010" runat="server" width="120px"></asp:TextBox></td></tr>
                <tr><td>（二）臨時性防災措施</td>
                    <td></td></tr>
                <tr><td class="td-padding">1.泥水外流未疏導</td>
                    <td><asp:DropDownList ID="DropList011" runat="server">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="是" Value="是"></asp:ListItem>
					        <asp:ListItem Text="否" Value="否"></asp:ListItem>
                        </asp:DropDownList></td></tr>
                <tr><td class="td-padding">2.開挖邊坡裸露未覆蓋</td>
                    <td><asp:DropDownList ID="DropList012" runat="server">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="是" Value="是"></asp:ListItem>
					        <asp:ListItem Text="否" Value="否"></asp:ListItem>
                        </asp:DropDownList></td></tr>
                <tr><td class="td-padding">3.土方暫置未覆蓋</td>
                    <td><asp:DropDownList ID="DropList013" runat="server">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="是" Value="是"></asp:ListItem>
					        <asp:ListItem Text="否" Value="否"></asp:ListItem>
                        </asp:DropDownList></td></tr>
                <tr><td class="td-padding">4.其他</td>
                    <td><asp:TextBox ID="TXTDTL014" runat="server" width="100%" Height="100px" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL014_count','300');" />
                        <asp:Label ID="TXTDTL014_count" runat="server" Text="(0/300)" ForeColor="Red" /></td></tr>
                <tr><td>（三）災害搶救小組測試</td>
                    <td><asp:CheckBox ID="CHKBOX015" runat="server" Text="正常運作" />
                        <asp:CheckBox ID="CHKBOX016" runat="server" Text="電話無人接聽" />
                        <asp:CheckBox ID="CHKBOX017" runat="server" Text="電話號碼錯誤" />
                        <asp:CheckBox ID="CHKBOX018" runat="server" Text="其他" />：
                        <asp:TextBox ID="TXTDTL019" runat="server" width="120px"></asp:TextBox></td></tr>
                <tr><td colspan="2">（四）其他注意事項：<br/><br/>
                        <asp:TextBox ID="TXTDTL020" runat="server" width="100%" Height="100px" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL020_count','500');" />
                        <asp:Label ID="TXTDTL020_count" runat="server" Text="(0/500)" ForeColor="Red" /></td></tr>
                </table>

                <table class="swcchg-imgUpload">
                <tr><td>二、相關單位及人員簽名<br/><br/>
                        <asp:TextBox ID="TXTDTL021" runat="server" width="100%" Height="100px" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL021_count','500');" style="height:250px;width:100%;"/>
                        <asp:Label ID="TXTDTL021_count" runat="server" Text="(0/500)" ForeColor="Red" /></td>
                    <td><asp:Image ID="TXTDTL022_img" runat="server" CssClass="imgUpload-l80" /><br/><br/><span>檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/><br/>
                        <asp:TextBox ID="TXTDTL022" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTDTL022_fileupload" runat="server" />
                        <asp:Button ID="TXTDTL022_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL022_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL022_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請確認真的要刪除照片!!!')" /></td></tr>
                <tr><td colspan="2">三、上傳檔案<br/><br/>
                        <asp:HyperLink ID="Link023" runat="server" /><br/>
                        <asp:TextBox ID="TXTDTL023" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTDTL023_fileupload" runat="server" />
                        <asp:Button ID="TXTDTL023_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL023_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL023_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請確認真的要刪除照片!!!')" />
                        <span style="color:red;">※ 上傳格式限定為PDF檔案大小請於50mb以內</span></td></tr>
                </table>

                <div class="form-btn">
                    <asp:Button ID="DataLock" runat="server" Text="送出" OnClientClick="return chkInput('DataLock');" OnClick="DataLock_Click" Visible="false" />&nbsp&nbsp
                    <asp:Button ID="SaveCase" runat="server" Text="存檔" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" />&nbsp&nbsp
                    <asp:Button ID="GoHomePage" runat="server" Text="關閉" OnClick="GoHomePage_Click" />
                </div>
        </div>
      </div>
        
            <div class="footer-s">
                <div class="footer-s-green"></div>
                <div class="footer-b-brown">
                    <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                       <span class="span2">110臺北市信義區松德路300號3樓 　服務專線(02)27593001   臺北市民當家熱線1999</span><br/>
                       <span class="span2">資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span></p>
                </div>
            </div>
        </div>

        <asp:Literal ID="error_msg" runat="server"></asp:Literal>

        <script src="../js/jquery-3.1.1.min.js"></script>
        <script src="../js/inner.js"></script>

    </div>
    </form>
</body>
</html>
