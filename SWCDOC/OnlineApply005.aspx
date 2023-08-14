<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineApply005.aspx.cs" Inherits="SWCDOC_OnlineApply005" %>

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
            var i = 0;

            if (document.getElementById("CHKBOXONA003").checked) { i++; }
            if (document.getElementById("CHKBOXONA004").checked) { i++; }
            if (document.getElementById("CHKBOXONA005").checked) { i++; }
            if (document.getElementById("CHKBOXONA006").checked) { i++; }
            if (document.getElementById("CHKBOXONA007").checked) { i++; }
            if (document.getElementById("CHKBOXONA008").checked) { i++; }
            if (document.getElementById("CHKBOXONA009").checked) { i++; }
            if (document.getElementById("CHKBOXONA010").checked) { i++; }

            if (i < 1)
            {
                alert('請勾選申請調整項目');
                return false;
            }
            //if (typeof Link011 == 'object')
            //{
            //    if (document.getElementById("Link011").innerText == '') {
            //        alert('請上傳承辦監造技師確認安全無虞之簽證說明書');
            //        return false;
            //    }
            //}
            //else
            //{
            //    alert('請上傳承辦監造技師確認安全無虞之簽證說明書');
            //    return false;
            //}
            if (document.getElementById("CHKBOXONA012").checked == true) { } else { alert('請確認符合水土保持計畫審核監督辦法第19條第2項各款情形之一者免辦理變更設計且安全無虞。'); return false; }
            if (jChkType == 'DataLock') {
                var r = confirm('確認送出後，即不可修改，請再次確認是否要完成送出。');
                return r;
            }
        }
        function UpLoadChk(ObjId) {
            if (document.getElementById(ObjId).value == '') { alert('請選擇檔案'); return false; } else { return ChkFileSize(ObjId, '50', '.xlsx.xls.doc.docx.png.jpg.pdf'); }
        }
        function UpLoadChk1(ObjId) {
            if (document.getElementById(ObjId).value == '') { alert('請選擇檔案'); return false; } else { return ChkFileSize(ObjId, '50', '.dwg'); }
        }
        function UpLoadChk2(ObjId) {
            if (document.getElementById(ObjId).value == '') { alert('請選擇檔案'); return false; } else { return ChkFileSize(ObjId, '50', '.pdf'); }
        }
    </script>
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
                        <li><a href="http://www.swc.taipei/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="https://tslm.swc.taipei/tslmwork/" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
                        <asp:Panel ID="TitleLink00" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWCBase001.aspx" title="帳號管理">帳號管理</a></li></asp:Panel>
                        <asp:Panel ID="LogOutLink" runat="server" Visible="true"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li></asp:Panel>
                    </ul>
                </div>
            </div>

            <div class="header-s-green">
                <div class="header-s-green-nameWrap">
                    <span><asp:Literal ID="TextUserName" runat="server">草莓枇杷膏挫冰，您好</asp:Literal></span>
                </div>
            </div>
        </div>


        <div class="content-s">
            <div class="review form">
                <h1>水土保持計畫設施調整及其他報備事項<br><br>
                </h1>
          
                <table class="review-out">
                <tr><td>設施調整報備編號</td>
                    <td><asp:Label ID="TXTONA001" runat="server" Visible="true"/></td></tr>
                <tr><td>水保局編號</td>
                    <td><asp:Label ID="LBSWC002" runat="server"/>
                        <asp:Label ID="LBSWC000" runat="server" Visible="false"/>
                        <asp:Label ID="LBSWC025" runat="server" Visible="false"/></td></tr>
                <tr><td>計畫名稱</td>
                    <td><asp:Label ID="LBSWC005" runat="server"/></td></tr>
                    <asp:Panel ID="HD01" runat="server" Visible="false">
                        <tr><td>調整報備項目是否屬永久設施</td>
                            <td><asp:DropDownList ID="DDLONA002" runat="server" Height="25px" />
                                <span style="color: red; padding: 10px;">※(是者須經目的事業主管機關核轉)</span></td></tr>
                    </asp:Panel>
                <tr><td>申請調整說明</td>
                    <td style="line-height:40px;">
                        <asp:TextBox ID="TXTONA017" runat="server" MaxLength="250" TextMode="MultiLine" CssClass="width-90"></asp:TextBox>
						<br /><span class="red">（0/250）</span>
                    </td></tr>
                <tr><td>申請調整項目<span style="color: red;font-family:cursive;">＊</span></td>
                    <td style="line-height:40px;">
                        <asp:CheckBox ID="CHKBOXONA003" runat="server" Text=" 一、道路開發面積增減未超過原計畫10%" /><br/>
                        <asp:CheckBox ID="CHKBOXONA004" runat="server" Text=" 二、目的事業開發配置調整，未涉及計畫面積變更且無變更開挖整地位置及水保設施" /><br/>
                        <asp:CheckBox ID="CHKBOXONA005" runat="server" Text=" 三、各單項水保設施，其計量單位之數量增減不 超過 20%，其為滯洪沉砂池者，量體不得小於所須最小滯洪、沉砂量" /><br/>
                        <asp:CheckBox ID="CHKBOXONA006" runat="server" Text=" 四、地形、地質與原設計不符，原水保設施仍可發揮正常功能" /><br/>
                        <asp:CheckBox ID="CHKBOXONA007" runat="server" Text=" 五、變更水保設施位置者，原水保設施仍可發揮正常功能" /><br/>
                        <asp:CheckBox ID="CHKBOXONA008" runat="server" Text=" 六、變更水保設施之構造物斷面、通水斷面(含排水及滯洪沉砂設施變更形狀或加【去】蓋 等)，面積增加不超過 20%或減少不超過 10%，且不影響原構造物正常功能" /><br/>
                        <asp:CheckBox ID="CHKBOXONA009" runat="server" Text=" 七、因應實際需要，依水土保持技術規範增設、 調整必要臨時防災措施或新增臨時防災階段" /><br/>
                        <asp:CheckBox ID="CHKBOXONA010" runat="server" Text=" 八、非屬前七點之其它未涉及變更水保開挖整地位置及水保設施之調整報備事項，如純屬建築配置變更、二樓板勘驗前完工限制解列等" Font-Bold="true" /><br/>
                    </td></tr>
                <tr><td> 變更後水土保持設施配置圖<br><span style="color:red;">（請併上傳CAD及PDF檔，調整處以雲狀圖標繪）</span></td>
                    <td  style="line-height:30px;">
                        <asp:TextBox ID="TXTONA013" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTONA013_fileupload" runat="server" />
                        <asp:Button ID="TXTONA013_fileuploadok" runat="server" Text="上傳檔案" OnClick="UpLoadFileOk_Click" OnClientClick="return UpLoadChk1('TXTONA013_fileupload');" />
                        <asp:Button ID="TXTONA013_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="UpLoadFileDel_Click" />
                        <br/><asp:HyperLink ID="Link013" runat="server" Target="_blank" /><br/>
                        <span style="color:red;">※ 上傳格式限定為CAD，檔案大小請於50mb以內</span><br/>
                        <asp:TextBox ID="TXTONA014" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTONA014_fileupload" runat="server" />
                        <asp:Button ID="TXTONA014_fileuploadok" runat="server" Text="上傳檔案" OnClick="UpLoadFileOk_Click" OnClientClick="return UpLoadChk2('TXTONA014_fileupload');" />
                        <asp:Button ID="TXTONA014_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="UpLoadFileDel_Click" />
                        <br/><asp:HyperLink ID="Link014" runat="server" Target="_blank" /><br/>
                        <span style="color:red;">※ 上傳格式限定為PDF，檔案大小請於50mb以內</span></td></tr>
                <tr><td>變更後臨時性防災設施配置圖<br><span style="color:red;">（請併上傳CAD及PDF檔，調整處以雲狀圖標繪）</span></td>
                    <td style="line-height:30px;">
                        <asp:TextBox ID="TXTONA015" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTONA015_fileupload" runat="server" />
                        <asp:Button ID="TXTONA015_fileuploadok" runat="server" Text="上傳檔案" OnClick="UpLoadFileOk_Click" OnClientClick="return UpLoadChk1('TXTONA015_fileupload');" />
                        <asp:Button ID="TXTONA015_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="UpLoadFileDel_Click" />
                        <br/><asp:HyperLink ID="Link015" runat="server" Target="_blank" /><br/>
                        <span style="color:red;">※ 上傳格式限定為CAD，檔案大小請於50mb以內</span><br/>
                        <asp:TextBox ID="TXTONA016" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTONA016_fileupload" runat="server" />
                        <asp:Button ID="TXTONA016_fileuploadok" runat="server" Text="上傳檔案" OnClientClick="return UpLoadChk2('TXTONA016_fileupload');" OnClick="UpLoadFileOk_Click" />
                        <asp:Button ID="TXTONA016_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="UpLoadFileDel_Click" />
                        <br/><asp:HyperLink ID="Link016" runat="server" Target="_blank" /><br/>
                        <span style="color:red;">※ 上傳格式限定為PDF，檔案大小請於50mb以內</span>
                    </td></tr>
                <tr><td> 相關附件 </td>
                    <td style="line-height:30px;">
                        <asp:TextBox ID="TXTONA011" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTONA011_fileupload" runat="server" />
                        <asp:Button ID="TXTONA011_fileuploadok" runat="server" Text="上傳檔案" OnClientClick="return UpLoadChk('TXTONA011_fileupload');" OnClick="UpLoadFileOk_Click" />
                        <asp:Button ID="TXTONA011_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="UpLoadFileDel_Click" />
                        <br/><asp:HyperLink ID="Link011" runat="server" Target="_blank" /><br/>
                        <span style="color:red;">※ 上傳格式限定為 .xls .xlsx .doc .docx .png .jpg .pdf 檔案，大小請於50mb以內</span></td></tr>
                <tr><td colspan="2">
                        <span style="color: red;font-family:cursive;">＊</span><asp:CheckBox ID="CHKBOXONA012" runat="server" Text="經確認符合水土保持計畫審核監督辦法第19條第2項各款情形之一者免辦理變更設計且安全無虞。" /></td></tr>
                                    </table>


                <asp:Label ID="ReqCount" runat="server" Text="" style="display:none;" />
                <asp:Panel ID="SignList" runat="server"><br/><br/>

                <div><span style="background-color: #FFFF99; font-size: 16pt; font-weight: bold; margin-top:1em;">退補正歷程</span></div><br/>
            
                <asp:GridView ID="GVSignList" runat="server" DataSourceID="SqlDataSourceSign" CssClass="retirement" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="TH001n" HeaderText="退文日期" SortExpression="SWC000" ItemStyle-Width="200px"/>
                        <asp:BoundField DataField="TH005n" HeaderText="改正期限" SortExpression="SWC002" ItemStyle-Width="190px"/>
                        <asp:BoundField DataField="THName" HeaderText="退文人員" SortExpression="THName" Visible="false" ItemStyle-Width="140px"/>
                        <asp:BoundField DataField="TH004" HeaderText="說明" SortExpression="SWC005"  ItemStyle-Width="350px"/>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceSign" runat="server" ConnectionString="<%$ ConnectionStrings:SWCConnStr %>" SelectCommand="" OnSelected="SqlDataSourceSign_Selected" />

                </asp:Panel>





      
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
                    <span class="span2">建議使用IE11(含)以上，Chrome或Firefox版本瀏覽器　<b>資料更新：</b><asp:Label ID="ToDay" runat="server" Text=""/>　<b>來訪人數：</b><asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                    <span class="span2"><b>客服電話：</b>02-27929328 陳小姐　<b>信箱：</b>tcge7@geovector.com.tw　本系統由多維空間資訊有限公司開發維護 TEL：(02)27929328</span><br/>
			        <span class="span2">※為維護系統服務品質，本平台訂於每周三凌晨AM 4:00-6:30 進行系統維護更新，更新期間偶有瞬斷情形，敬請使用者避開該時段使用。謝謝！</span></p>
            </div>

        </div>

        <asp:Literal ID="error_msg" runat="server"></asp:Literal>














        
    <script type="text/javascript">
        if (document.getElementById("ReqCount").innerText == '0') { SignList.style.display = "none"; }
    </script>
        <script src="../js/jquery-3.1.1.min.js"></script>
        <script src="../js/inner.js"></script>
        <script src="../js/allhref.js"></script>
        <script src="../js/NorJs.js"></script>

    </div>
    </form>
</body>
</html>
