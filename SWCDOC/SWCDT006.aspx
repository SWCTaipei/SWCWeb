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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCDT006.aspx.cs" Inherits="SWCDOC_SWCDT006" %>
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
        function chkInput(jChkType) {
            jCHKValue01 = document.getElementById("TXTDTL002").value;

            if (jCHKValue01.trim() == '') {
                alert('請輸入檢查日期');
                document.getElementById("TXTDTL002").focus();
                return false;
            } else {
                var t = Date.parse(jCHKValue01);
                if (isNaN(t)) {
                    document.getElementById("TXTDTL002").focus();
                    return false;
                }
            }

            if (jChkType == 'DataLock') {
                var r = confirm('確認送出後，即不可修改，請再次確認是否要完成送出。');
                return r;
            }
        }
        function BigPic() {
            alert('test');
        }
    </script>


    <style type="text/css">
        .auto-style1 {
            height: 37px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"/>

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
            <div class="completed form">
                <h1>水土保持完工檢查紀錄表<br/><br/></h1>

                <table class="completed-out">
                <tr><td>完工檢查表編號</td>
                    <td><asp:Label ID="LBDTL001" runat="server"/>
                        <asp:Label ID="LBSWC000" runat="server" Visible="false"/>
                    </td></tr>
                <tr><td>檢查日期<span style="color: red;font-family:cursive;">＊</span></td>
                    <td><asp:TextBox ID="TXTDTL002" runat="server" width="150px" autocomplete="off"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTDTL002_CalendarExtender" runat="server" TargetControlID="TXTDTL002" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                <tr><td>檢查單位</td>
                    <td><asp:Label ID="TXTDTL004" runat="server" /></td></tr>
                </table>
                
                <br/><br/>

                <table class="completed-verticalText_X">
                <tr><td rowspan="6" class="text_tabletitle">水土保持書件</td>
                    <td>計畫名稱</td>
                    <td style="line-height:1.5;"><asp:Label ID="LBSWC005" runat="server"/>
                        （<asp:Label ID="LBSWC007" runat="server"/>）</td></tr>
                <tr><td class="auto-style1">核定日期文號</td>
                    <td class="auto-style1">臺北市政府
                        <asp:Label ID="LBSWC038" runat="server"/>
                        <asp:Label ID="LBSWC039" runat="server"/>
                        函</td></tr>
                <tr><td>水土保持施工許可證日期文號</td>
                    <td>臺北市政府
                        <asp:Label ID="LBSWC043" runat="server"/>
                        <asp:Label ID="LBSWC044" runat="server"/>
                        函</td></tr>
                <tr><td>開工日期</td>
                    <td><asp:Label ID="LBSWC051" runat="server"/></td></tr>
                <tr><td>核定完工日期</td>
                    <td><asp:Label ID="LBSWC052" runat="server" width="120px"></asp:Label></td></tr>
                <tr><td>申報完工日期</td>
                    <td><asp:Label ID="LBSWC058" runat="server" width="120px"></asp:Label></td></tr>
                <tr><td rowspan="3" class="text_tabletitle">水土保持義務人</td>
                    <td>姓名或名稱</td>
                    <td><asp:Label ID="LBSWC013" runat="server"/></td></tr>
                <tr><td>身分證或營利事業統一編號</td>
                    <td><asp:Label ID="LBSWC013ID" runat="server"/></td></tr>
                <tr><td>住居所或營業所</td>
                    <td><asp:Label ID="LBSWC014" runat="server"/></td></tr>
                <tr><td rowspan="5" class="text_tabletitle">承辦監造技師</td>
                    <td>姓名</td>
                    <td><asp:Label ID="LBSWC021" runat="server"/></td></tr>
                <tr><td>執業機構名稱</td>
                    <td><asp:Label ID="LBSWC021Name" runat="server"/></td></tr>
                <tr><td>執業執照字號</td>
                    <td><asp:Label ID="LBSWC021OrgIssNo" runat="server"/></td></tr>
                <tr><td>營利事業統一編號</td>
                    <td><asp:Label ID="LBSWC021OrgGUINo" runat="server"/></td></tr>
                <tr><td>電話</td>
                    <td><asp:Label ID="LBSWC021OrgTel" runat="server"/></td></tr>
                <tr><td colspan="2">實施地點土地標示</td>
                    <td><asp:TextBox ID="TXTDTL023" runat="server" width="100%" onkeyup="textcount(this,'TXTDTL023_count','200');" MaxLength="200" />
                        <asp:Label ID="TXTDTL023_count" runat="server" Text="(0/200)" ForeColor="Red" /></td></tr>
                </table>

                <table class="completed-excelUpload">
                <tr><td colspan="14" style="border-bottom: none;border-top:none;">
                        一、完工抽驗項目 <asp:HyperLink ID="NewUser" runat="server" Text="excel範本下載" NavigateUrl="../sysFile/完工抽驗項目.xlsx" /></td></tr>
                <tr><td colspan="14" style="border-top: none;">
                        <asp:FileUpload ID="TXTDTL024_fileupload" runat="server" />
                        <asp:Button ID="TXTDTL024_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL024_fileuploadok_Click"/>
                        <asp:TextBox ID="TXTDTL024" runat="server" Width="70px" Visible="false"/>
                        <asp:Button ID="TXTDTL024_fileclean" runat="server" CssClass="wordttb" Text="X" OnClientClick="return confirm('檔案刪除後無法復原，請確認真的要刪除檔案!!!')" OnClick="TXTDTL024_fileclean_Click" /><br/>
                        <asp:HyperLink ID="Link024" runat="server" CssClass="word" Target="_blank"></asp:HyperLink><br/>
                        <span style="color:red;">※ 上傳格式限定為excel，檔案大小請於50mb以內</span></td></tr>
                <tr><td colspan="14">二、實施與計畫或規定不符之限期改正期限 <br/><br/>
                        <asp:TextBox ID="TXTDTL025" runat="server" width="100%" Height="120px" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL025_count','300');" />
                        <asp:Label ID="TXTDTL025_count" runat="server" Text="(0/300)" ForeColor="Red" /></td></tr>
                <tr><td colspan="14">三、其他注意事項<br/><br/>
                        <asp:TextBox ID="TXTDTL026" runat="server" width="100%" Height="120px" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL026_count','300');" />
                        <asp:Label ID="TXTDTL026_count" runat="server" Text="(0/300)" ForeColor="Red" /></td></tr>
                <tr class="completed-checkAnswer">
                    <td colspan="2">四、檢查結果</td>
                    <td colspan="10">
                        <asp:DropDownList ID="DDLDTL027" runat="server">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="已達完工標準" Value="已達完工標準"></asp:ListItem>
					        <asp:ListItem Text="未達完工標準" Value="未達完工標準"></asp:ListItem>
                        </asp:DropDownList></td></tr>
                </table>

                <table class="checkRecord-imgUpload">
                <tr><td style="border-bottom: none;">
                        五、相關單位及人員簽名<br/><br/>
                        <asp:TextBox ID="TXTDTL028" runat="server" width="100%" Height="250px" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL028_count','800');" />
                        <asp:Label ID="TXTDTL028_count" runat="server" Text="(0/800)" ForeColor="Red" /></td>
                    <td style="border-bottom: none;text-align:center;">
                        <asp:Image ID="TXTDTL029_img" runat="server" CssClass="imgUpload" Visible="false"/><br/>
                        <asp:HyperLink ID="HyperLink029" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink>
                        <asp:TextBox ID="TXTDTL029" runat="server" Width="70px" Visible="False" /><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/><br/>
                        <asp:FileUpload ID="TXTDTL029_fileupload" runat="server" />
                        <asp:Button ID="TXTDTL029_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL029_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL029_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請確認真的要刪除照片!!!')" OnClick="TXTDTL029_fileuploaddel_Click" /></td></tr>
                <tr><td colspan="2" style="border-top: none;border-bottom: none;">六、 屬簡易水土保持申報書者，「水土保持施工許可證日期文號」及「承辦監造技師」等二欄，無需填寫。</td></tr>
                <tr><td colspan="2" style="border-top: none;">七、 完工檢查係採抽驗方式，屬未抽驗、隱蔽或工程品質部分，應由水土保持義務人及承辦監造技師負責。</td></tr>
                </table>
                
                <br/>

                <table class="checkRecord-fileUpload">
                <tr><td colspan="2">
                    <asp:Label ID="LBSWC005a" runat="server" CssClass="redn"/></td></tr>
                <tr><td>相片一</td>
                    <td>相片二</td></tr>
                <tr><td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL030" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL030_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL030_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL030_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL030_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL030_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL030_img" runat="server" CssClass="imgUpload" />
                        <br/>
                        <asp:TextBox ID="TXTDTL031" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td>
                    <td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL032" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL032_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL032_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL032_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL032_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL032_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL032_img" runat="server" CssClass="imgUpload" />
                        <br/>
                        <asp:TextBox ID="TXTDTL033" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td></tr>
                        <tr>
                            <td>相片三</td>
                            <td>相片四</td>
                        </tr>
                        <tr>
                            <td>
                        <div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL034" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL034_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL034_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL034_fileuploadok_Click"/>
                            <asp:Button ID="TXTDTL034_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL034_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL034_img" runat="server" CssClass="imgUpload" />
                        <br/>
                        <asp:TextBox ID="TXTDTL035" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td>
                    <td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL036" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL036_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL036_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL036_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL036_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL036_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL036_img" runat="server" CssClass="imgUpload" />
                        <br/>
                        <asp:TextBox ID="TXTDTL037" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td></tr>
                <tr><td>相片五</td>
                    <td>相片六</td></tr>
                <tr><td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL038" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL038_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL038_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL038_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL038_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL038_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL038_img" runat="server" CssClass="imgUpload" />
                        <br/>
                        <asp:TextBox ID="TXTDTL039" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td>
                    <td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL040" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL040_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL040_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL040_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL040_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL040_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL040_img" runat="server" CssClass="imgUpload" />
                        <br/>
                        <asp:TextBox ID="TXTDTL041" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td></tr>
                <tr><td colspan="4">附件</td></tr>
                <tr><td colspan="4">附件檔案上傳：
                        <asp:TextBox ID="TXTDTL042" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTDTL042_fileupload" runat="server" />
                        <asp:Button ID="TXTDTL042_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL042_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL042_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請確認真的要刪除檔案!!!')" OnClick="TXTDTL042_fileuploaddel_Click" />
                        <br/><br/>
                        <span style="color:red;">檔案大小請小於 50 Mb，請上傳 pdf odt word 格式</span>
                        <br/><br/>
                        <asp:HyperLink ID="Link042" runat="server" Text ="其他附件檔案下載" Target="_blank" Visible="false" /></td></tr>
                    </table>





                <div class="form-btn">
                    <asp:Button ID="DataLock" runat="server" Text="確認送出" OnClientClick="return chkInput('DataLock');" OnClick="DataLock_Click" />&nbsp&nbsp
                    <asp:Button ID="SaveCase" runat="server" Text="暫時儲存" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" />&nbsp&nbsp
                    <asp:Button ID="GoHomePage" runat="server" Text="返回編輯案件" OnClick="GoHomePage_Click" />
                </div>
            </div>
        </div>

        <%--<div class="footer-s">
            <div class="footer-s-green"></div>
            <div class="footer-b-brown">
                <p> <span class="span1">臺北市政府工務局大地工程處</span>
                    <br/><span class="span2">110臺北市信義區松德路300號3樓 　(02)27593001   臺北市民當家熱線1999</span>
                    <br/><span class="span2">資料更新：2017.5.19　來訪人數：123456789 </span></p>
            </div>
        </div>--%>
        
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

    </div>
    </form>
</body>
</html>
