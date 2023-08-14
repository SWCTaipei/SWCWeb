<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineApply007.aspx.cs" Inherits="SWCDOC_OnlineApply007" %>
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
            var jONA003 = document.getElementById("TXTONA003").value;
            var jONA004 = document.getElementById("TXTONA004").value;
            var jONA007 = document.getElementById("TXTONA007").value;

            if (document.getElementById("TXTONA002").value == '') {
                alert('請輸入開工日期');
                return false;
            }
            if (document.getElementById("TXTONA003").value == '') {
                alert('請輸入原核定完工日期');
                return false;
            }
            if (document.getElementById("TXTONA004").value == '') {
                alert('請輸入目的事業主管機關核定(展延)工期文件日期');
                return false;
            }
            /*
            if (typeof Link005 == 'object')
            {
                if (document.getElementById("Link005").innerHTML == '') {
                    alert('請上傳目的事業主管機關核定(展延)工期文件');
                    return false;
                }
            }
            else
            {
                alert('請上傳目的事業主管機關核定(展延)工期文件');
                return false;
            }
            */
            if (document.getElementById("TXTONA006").value == '') {
                alert('請輸入展延次數');
                return false;
            }
            if (document.getElementById("TXTONA007").value == '') {
                alert('請輸入預計展延工期期限');
                return false;
            }
            if (document.getElementById("TXTONA007").value > document.getElementById("TXTONA004").value) {
                alert('預計展延工期期限不得大於目的事業主管機關核定工期');
                return false;
            }
            //alert(addMonthsUTC(jONA007, 0));
            //alert(addMonthsUTC(jONA003, 6));
            if (addMonthsUTC(jONA007, 0) > addMonthsUTC(jONA003, 6)) {
                alert('預計展延工期期限不得超過六個月');
                return false;
            }
            if (document.getElementById("CHKONA009").checked || document.getElementById("CHKONA010").checked || document.getElementById("CHKONA011").checked || document.getElementById("CHKONA012").checked) { }else {
                alert('監造技師簽證之展延工期事實及理由說明');
                return false;
            }

            if (jChkType == 'DataLock') {
                var r = confirm('確認送出後，即不可修改，請再次確認是否要完成送出。');
                return r;
            }
        }
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
                        <li><a href="https://swc.taipei/swcinfo/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="http://tslm.swc.taipei/tslmwork/" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
                        <asp:Panel ID="TitleLink00" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWCBase001.aspx" title="帳號管理">帳號管理</a></li></asp:Panel>
                        <asp:Panel ID="LogOutLink" runat="server" Visible="true"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li></asp:Panel>
                    </ul>
                </div>
            </div>

            <div class="header-s-green">
                <div class="header-s-green-nameWrap">
                    <span><asp:Literal ID="TextUserName" runat="server">王小姐，您好</asp:Literal></span>
                </div>
            </div>
        </div>





        
  <div class="content-s">
    <div class="review form">
      <h1>水土保持計畫工期展延<br>
        <br>
      </h1>
          
        
                <table class="review-out">
                <tr><td>工期展延編號</td>
                    <td><asp:Label ID="TXTONA001" runat="server" Visible="true"/></td></tr>
                <tr><td style="width: 19%">水保局編號</td>
                    <td><asp:Label ID="LBSWC002" runat="server"/>
                        <asp:Label ID="LBSWC000" runat="server" Visible="false"/>
                        <asp:Label ID="LBSWC025" runat="server" Visible="false"/></td></tr>
                <tr><td>計畫名稱</td>
                    <td><asp:Label ID="LBSWC005" runat="server"/></td></tr>
                <tr><td><span style="color: red;font-family:cursive;">＊</span>開工日期</td>
                    <td><asp:TextBox ID="TXTONA002" runat="server" width="120px" autocomplete="off"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTONA002_CalendarExtender" runat="server" TargetControlID="TXTONA002" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                <tr><td><span style="color: red;font-family:cursive;">＊</span>原核定完工日期</td>
                    <td><asp:TextBox ID="TXTONA003" runat="server" width="120px" autocomplete="off"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTONA003_CalendarExtender" runat="server" TargetControlID="TXTONA003" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                <tr><td style="line-height:1.5"><span style="color: red;font-family:cursive;">＊</span>目的事業主管機關核定(展延)完工期限及證明文件</td>
                    <td><asp:TextBox ID="TXTONA004" runat="server" width="120px" autocomplete="off"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTONA004_CalendarExtender" runat="server" TargetControlID="TXTONA004" Format="yyyy-MM-dd"></asp:CalendarExtender><br/>
                        <br/>
                        <asp:TextBox ID="TXTONA005" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTONA005_fileupload" runat="server" />
                        <asp:Button ID="TXTONA005_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA005_fileuploadok_Click" />
                        <asp:Button ID="TXTONA005_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA005_fileuploaddel_Click" />
                        <br/><asp:HyperLink ID="Link005" runat="server" Target="_blank" />
                        <br/><span style="color:red;">
                            ※ 上傳格式限定為pdf、odt或doc檔案，大小請於50mb以內<br/>
                            ※ 前次上傳文件仍屬有效或可於<a href="https://dba.gov.taipei/" target="_blank">臺北市建管處網站</a>查得建照工期者免上傳
                            </span><br/></td></tr>
                <tr><td><span style="color: red;font-family:cursive;">＊</span>展延次數</td>
                    <td>第 <asp:TextBox ID="TXTONA006" runat="server" width="50px" autocomplete="off" style="text-align:right" ></asp:TextBox> 次展延</td></tr>
                <tr><td><span style="color: red;font-family:cursive;">＊</span>預計展延工期期限</td>
                    <td><asp:TextBox ID="TXTONA007" runat="server" width="120px" autocomplete="off"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTONA007_CalendarExtender" runat="server" TargetControlID="TXTONA007" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                <tr><td style="line-height:1.5">監造技師簽證之展延工期事實及理由說明</td>
                    <td><asp:CheckBox ID="CHKONA009" runat="server" Text="配合目的事業工項(如建築工程)調整水土保持工序" /><br/>
                        <asp:CheckBox ID="CHKONA010" runat="server" Text="配合辦理水土保持計畫變更設計" /><br/>
                        <asp:CheckBox ID="CHKONA011" runat="server" Text="因氣候或人為因素無法順利施作" /><br/>
                        <asp:CheckBox ID="CHKONA012" runat="server" Text="其他" />
                        <asp:TextBox ID="TXTONA008" runat="server" MaxLength="100" autocomplete="off" width="800px" /></td></tr></table>
        

                <asp:Label ID="ReqCount" runat="server" Text="" style="display:none;" />
                <asp:Panel ID="SignList" runat="server"><br/><br/>

                <div><span style="background-color: #FFFF99; font-size: 16pt; font-weight: bold; margin-top:1em;">退補正歷程</span></div><br/>
            
                <asp:GridView ID="GVSignList" runat="server" DataSourceID="SqlDataSourceSign" CssClass="retirement" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="TH001n" HeaderText="退文日期" SortExpression="SWC000" ItemStyle-Width="200px"/>
                        <asp:BoundField DataField="TH005n" HeaderText="改正期限" SortExpression="SWC002" ItemStyle-Width="190px"/>
                        <asp:BoundField DataField="THName" HeaderText="退文人員" SortExpression="SWC004" ItemStyle-Width="140px" Visible="false" />
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
        <script src="../js/BaseNorl.js"></script>
    </div>
    </form>
</body>
</html>
