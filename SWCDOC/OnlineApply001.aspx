<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineApply001.aspx.cs" Inherits="SWCDOC_OnlineApply001" %>
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
        function SetRadioSel()
        {
            //08：09、10
            if(document.all.RaONA008b.checked)
            {
                document.getElementById("RaONA009a").disabled = true;
                document.getElementById("TXTONA009aD").disabled = true;
                document.getElementById("RaONA009b").disabled = true;
                document.getElementById("TXTONA009bD").disabled = true;
                document.getElementById("RaONA010a").disabled = true;
                document.getElementById("TXTONA010aD").disabled = true;
                document.getElementById("RaONA010b").disabled = true;
                document.getElementById("TXTONA010bD").disabled = true;

                document.getElementById("RaONA009a").checked = false;
                document.getElementById("RaONA009b").checked = false;
                document.getElementById("RaONA010a").checked = false;
                document.getElementById("RaONA010b").checked = false;
            }
            else
            {
                document.getElementById("RaONA009a").disabled = false;
                document.getElementById("TXTONA009aD").disabled = false;
                document.getElementById("RaONA009b").disabled = false;
                document.getElementById("TXTONA009bD").disabled = false;
                document.getElementById("RaONA010a").disabled = false;
                document.getElementById("TXTONA010aD").disabled = false;
                document.getElementById("RaONA010b").disabled = false;
                document.getElementById("TXTONA010bD").disabled = false;
                
            }

            //11：12、13     ************************************************************
            if (document.all.RaONA011b.checked) {
                document.getElementById("RaONA012a").disabled = true;
                document.getElementById("TXTONA012aD").disabled = true;
                document.getElementById("RaONA012b").disabled = true;
                document.getElementById("TXTONA012bD").disabled = true;
                document.getElementById("RaONA013a").disabled = true;
                document.getElementById("TXTONA013aD").disabled = true;
                document.getElementById("RaONA013b").disabled = true;
                document.getElementById("TXTONA013bD").disabled = true;

                document.getElementById("RaONA012a").checked = false;
                document.getElementById("RaONA012b").checked = false;
                document.getElementById("RaONA013a").checked = false;
                document.getElementById("RaONA013b").checked = false;
            }
            else {
                document.getElementById("RaONA012a").disabled = false;
                document.getElementById("TXTONA012aD").disabled = false;
                document.getElementById("RaONA012b").disabled = false;
                document.getElementById("TXTONA012bD").disabled = false;
                document.getElementById("RaONA013a").disabled = false;
                document.getElementById("TXTONA013aD").disabled = false;
                document.getElementById("RaONA013b").disabled = false;
                document.getElementById("TXTONA013bD").disabled = false;
            }

            //14：15、16、17 ************************************************************
            if (document.all.RaONA014b.checked) {
                document.getElementById("RaONA015a").disabled = true;
                document.getElementById("TXTONA015aD").disabled = true;
                document.getElementById("RaONA015b").disabled = true;
                document.getElementById("TXTONA015bD").disabled = true;
                document.getElementById("RaONA016a").disabled = true;
                document.getElementById("TXTONA016aD").disabled = true;
                document.getElementById("RaONA016b").disabled = true;
                document.getElementById("TXTONA016bD").disabled = true;
                document.getElementById("RaONA017a").disabled = true;
                document.getElementById("TXTONA017aD").disabled = true;
                document.getElementById("RaONA017b").disabled = true;
                document.getElementById("TXTONA017bD").disabled = true;
                document.getElementById("RaONA017c").disabled = true;

                document.getElementById("RaONA015a").checked = false;
                document.getElementById("RaONA015b").checked = false;
                document.getElementById("RaONA016a").checked = false;
                document.getElementById("RaONA016b").checked = false;
                document.getElementById("RaONA017a").checked = false;
                document.getElementById("RaONA017b").checked = false;
                document.getElementById("RaONA017c").checked = false;
            }
            else {
                document.getElementById("RaONA015a").disabled = false;
                document.getElementById("TXTONA015aD").disabled = false;
                document.getElementById("RaONA015b").disabled = false;
                document.getElementById("TXTONA015bD").disabled = false;
                document.getElementById("RaONA016a").disabled = false;
                document.getElementById("TXTONA016aD").disabled = false;
                document.getElementById("RaONA016b").disabled = false;
                document.getElementById("TXTONA016bD").disabled = false;
                document.getElementById("RaONA017a").disabled = false;
                document.getElementById("TXTONA017aD").disabled = false;
                document.getElementById("RaONA017b").disabled = false;
                document.getElementById("TXTONA017bD").disabled = false;
                document.getElementById("RaONA017c").disabled = false;
            }

            //18：19、20     ************************************************************
            if (document.all.RaONA018b.checked) {
                document.getElementById("RaONA019a").disabled = true;
                document.getElementById("TXTONA019aD").disabled = true;
                document.getElementById("RaONA019b").disabled = true;
                document.getElementById("TXTONA019bD").disabled = true;
                document.getElementById("RaONA020a").disabled = true;
                document.getElementById("TXTONA020aD").disabled = true;
                document.getElementById("RaONA020b").disabled = true;
                document.getElementById("TXTONA020bD").disabled = true;

                document.getElementById("RaONA019a").checked = false;
                document.getElementById("RaONA019b").checked = false;
                document.getElementById("RaONA020a").checked = false;
                document.getElementById("RaONA020b").checked = false;
            }
            else {
                document.getElementById("RaONA019a").disabled = false;
                document.getElementById("TXTONA019aD").disabled = false;
                document.getElementById("RaONA019b").disabled = false;
                document.getElementById("TXTONA019bD").disabled = false;
                document.getElementById("RaONA020a").disabled = false;
                document.getElementById("TXTONA020aD").disabled = false;
                document.getElementById("RaONA020b").disabled = false;
                document.getElementById("TXTONA020bD").disabled = false;
            }
        }
        function chkInput(jChkType) {
            var vData01 = $("#TXTSWC002").val();
            var vData02 = $("#TXTONA002").val();
            var vData03 = $("#TXTONA003").val();
            var vData04 = $("#TXTONA004").val();
            var vData05 = $("#TXTONA005").val();
            var vData06 = $("#TXTONA006").val();
            var vData07 = $("#TXTONA007").val();

            if (typeof TXTSWC002 == 'object')
            {
                if (vData01.trim() == "") {
                    alert('請輸入水保局編號');
                    return false;
                }
            }

            if (vData02.trim() == "") {
                alert('請輸入檢查日期');
                return false;
            }
            if (vData03.trim() == "") {
                alert('請輸入社區(設施)地址');
                return false;
            }
            if (vData04.trim() == "") {
                alert('請輸入義務人(聯絡人)');
                return false;
            }
            if (vData05.trim() == "") {
                alert('請輸入聯絡地址');
                return false;
            }
            if (vData06.trim() == "") {
                alert('請輸入聯絡電話');
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
            <div class="header header-s clearfix"><a href="SWC001.aspx" class="logo-s"></a>
                <div class="header-menu-s">
                    <ul>
                        <li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
                        <li>|</li>
                        <li><a href="http://swc.taipei/swcinfo/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="http://tslm.swc.taipei/tslmwork/" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
                        <asp:Panel ID="TitleLink00" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWCBase001.aspx" title="帳號管理">帳號管理</a></li></asp:Panel>
                        <asp:Panel ID="LogOutLink" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li></asp:Panel>
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
            <div class="facilityMaintain form">
                <h1>臺北市山坡地水土保持設施安全自主檢查表<br/><br/></h1>
                
                <table class="facilityMaintain-out">
                    <tr><td>安全自主檢查表編號</td>
                        <td><asp:Label ID="LBONA001" runat="server" />
                            <asp:Label ID="LBSWC000" runat="server" Visible="false"/>
                        </td></tr>
                    <tr><td>項次編號</td>
                        <td><asp:TextBox ID="TXTONA027" runat="server" MaxLength="10" width="100px"/>
						<span class="gray">（範例：250）</span>
                        </td></tr>
                    <tr><td>水保局編號<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTSWC002" runat="server" MaxLength="14" width="200px"/>
                            <%--<asp:TextBox ID="TXTSWC002" runat="server" MaxLength="12" placeholder="(如不知可由市府填寫)" width="200px"/>--%>
                            <asp:Label ID="LBSWC002" runat="server" Visible="false" />
                        </td></tr>
                    <tr><td>檢查日期<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTONA002" runat="server" width="120px" autocomplete="off" ReadOnly="true"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTONA002_CalendarExtender" runat="server" TargetControlID="TXTONA002" Format="yyyy-MM-dd"></asp:CalendarExtender>
                            <span class="gray">（範例：2020-01-02）</span>
                        </td></tr>
                    <tr><td>社區(設施)地址<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTONA003" runat="server" width="100%" onkeyup="textcount(this,'TXTONA003_count','100');" MaxLength="100" /><br />
                            <span class="gray">（範例：110臺北市信義區松德路300號3樓）</span><br />
                            <asp:Label ID="TXTONA003_count" runat="server" Text="(0/100)" ForeColor="Red"  /></td></tr>
                    <tr><td>水土保持義務人<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTONA004" runat="server" width="100%" onkeyup="textcount(this,'TXTONA004_count','100');" MaxLength="100" /><br />
                            <span class="gray">（範例：王小明）</span><br />
                            <asp:Label ID="TXTONA004_count" runat="server" Text="(0/100)" ForeColor="Red"  /></td></tr>
                    <tr><td>聯絡人</td>
                        <td><asp:TextBox ID="TXTONA028" runat="server" width="100%" onkeyup="textcount(this,'TXTONA028_count','100');" MaxLength="100" /><br />
                            <span class="gray">（範例：王小明）</span><br />
                            <asp:Label ID="TXTONA028_count" runat="server" Text="(0/100)" ForeColor="Red"  /></td></tr>
                    <tr><td>聯絡地址<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTONA005" runat="server" width="100%" onkeyup="textcount(this,'TXTONA005_count','100');" MaxLength="100" /><br />
                            <span class="gray">（範例：110臺北市信義區松德路300號3樓）</span><br />
                            <asp:Label ID="TXTONA005_count" runat="server" Text="(0/100)" ForeColor="Red"  /></td></tr>
                    <tr><td>聯絡電話（市話）<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTONA006" runat="server" width="100%" onkeyup="textcount(this,'TXTONA006_count','100');" MaxLength="100" /><br />
                            <span class="gray">（範例：02-27936512#123）</span><br />
                            <asp:Label ID="TXTONA006_count" runat="server" Text="(0/100)" ForeColor="Red"  />
                        </td></tr>
                    <tr><td>行動電話<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTONA007" runat="server" width="100%" onkeyup="textcount(this,'TXTONA007_count','100');" MaxLength="100" /><br />
                            <span class="gray">（範例：0912345678）</span><br />
                            <asp:Label ID="TXTONA007_count" runat="server" Text="(0/100)" ForeColor="Red"  />
                        </td></tr>
                </table>

                <br/><br/>
                
                <table class="OA01-T2">
                <tr><th colspan="3">檢查項目與名稱</th>
                    <th colspan="2">檢查及改善結果</th></tr>
                <tr class="tr-title">
                    <td>一、滯洪沉砂設施</td>
                    <td colspan="2"><asp:RadioButton ID="RaONA008a" GroupName="ONA008" runat="server" Text="有此設施" onchange="SetRadioSel();" />（請接續填寫）</td>
                    <td colspan="3"> <asp:RadioButton ID="RaONA008b" GroupName="ONA008" runat="server" Text="無此設施" onchange="SetRadioSel();" />（請跳至二、排水設施繼續填寫）</td>
                    
                </tr>
                    <%--<tr><td></td><td></td><td></td><td style="width:200px;"></td><td></td></tr>--%>
                <tr><td class="td-padding" colspan="3">1.池內是否淤積？</td>
                    <td style="text-align:left;" colspan="2">
                        <asp:RadioButton ID="RaONA009a" GroupName="ONA009" runat="server" Text="是" />，
                        <asp:TextBox ID="TXTONA009aD" runat="server" width="100%" onkeyup="textcount(this,'TXTONA009aD_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTONA009aD_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" />
                        <br/>
                    <asp:RadioButton ID="RaONA009b" GroupName="ONA009" runat="server" Text="否" />，
                        <asp:TextBox ID="TXTONA009bD" runat="server" width="100%" onkeyup="textcount(this,'TXTONA009bD_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTONA009bD_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /></td></tr>
                <tr><td class="td-padding" colspan="3" >2.進水及排放處是否堵塞？</td>
                    <td style="text-align:left;" colspan="2">
                    <asp:RadioButton ID="RaONA010a" GroupName="ONA010" runat="server" Text="是" />，
                        <asp:TextBox ID="TXTONA010aD" runat="server" width="100%" onkeyup="textcount(this,'TXTONA010aD_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTONA010aD_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /><br/>
                    <asp:RadioButton ID="RaONA010b" GroupName="ONA010" runat="server" Text="否" />，
                        <asp:TextBox ID="TXTONA010bD" runat="server" width="100%" onkeyup="textcount(this,'TXTONA010bD_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTONA010bD_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /></td></tr>
                <tr class="tr-title">
                    <td>二、排水設施</td>
                    <td colspan="2"><asp:RadioButton ID="RaONA011a" GroupName="ONA011" runat="server" Text="有此設施" onchange="SetRadioSel();" />（請接續填寫）</td>
                    <td colspan="2"><asp:RadioButton ID="RaONA011b" GroupName="ONA011" runat="server" Text="無此設施" onchange="SetRadioSel();" />（請跳至三、邊坡保護設施繼續填寫）</td>
                    
                </tr>
                <tr><td class="td-padding" colspan="3">1.排水溝是否損壞？</td>
                    <td style="text-align:left;" colspan="2">
                    <asp:RadioButton ID="RaONA012a" GroupName="ONA012" runat="server" Text="是" />，
                        <asp:TextBox ID="TXTONA012aD" runat="server" width="100%" onkeyup="textcount(this,'TXTONA012aD_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTONA012aD_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /><br/>
                    <asp:RadioButton ID="RaONA012b" GroupName="ONA012" runat="server" Text="否" />，
                        <asp:TextBox ID="TXTONA012bD" runat="server" width="100%" onkeyup="textcount(this,'TXTONA012bD_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTONA012bD_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /></td></tr>
                <tr><td class="td-padding" colspan="3">2.排水溝是否雜物淤積？</td>
                    <td style="text-align:left;" colspan="2">
                    <asp:RadioButton ID="RaONA013a" GroupName="ONA013" runat="server" Text="是" />，
                        <asp:TextBox ID="TXTONA013aD" runat="server" width="100%" onkeyup="textcount(this,'TXTONA013aD_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTONA013aD_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /><br/>
                    <asp:RadioButton ID="RaONA013b" GroupName="ONA013" runat="server" Text="否" />，
                        <asp:TextBox ID="TXTONA013bD" runat="server" width="100%" onkeyup="textcount(this,'TXTONA013bD_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTONA013bD_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /></td></tr>
                <tr class="tr-title">
                    <td>三、邊坡保護設施 </td>
                    <td colspan="2"><asp:RadioButton ID="RaONA014a" GroupName="ONA014" runat="server" Text="有此設施" onchange="SetRadioSel();" />（請接續填寫）</td>
                    <td colspan="2"><asp:RadioButton ID="RaONA014b" GroupName="ONA014" runat="server" Text="無此設施" onchange="SetRadioSel();" />（請跳至四、抽水設施繼續填寫）</td>
                    
                </tr>
                <tr><td class="td-padding" colspan="3">1.是否外凸變形？</td>
                    <td style="text-align:left;" colspan="2">
                    <asp:RadioButton ID="RaONA015a" GroupName="ONA015" runat="server" Text="是" />，
                        <asp:TextBox ID="TXTONA015aD" runat="server" width="100%" onkeyup="textcount(this,'TXTONA015aD_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTONA015aD_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /><br/>
                    <asp:RadioButton ID="RaONA015b" GroupName="ONA015" runat="server" Text="否" />，
                        <asp:TextBox ID="TXTONA015bD" runat="server" width="100%" onkeyup="textcount(this,'TXTONA015bD_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTONA015bD_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /></td></tr>
                <tr><td class="td-padding" colspan="3">2.是否龜裂？</td>
                    <td style="text-align:left;" colspan="2">
                    <asp:RadioButton ID="RaONA016a" GroupName="ONA016" runat="server" Text="是" />，
                        <asp:TextBox ID="TXTONA016aD" runat="server" width="100%" onkeyup="textcount(this,'TXTONA016aD_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTONA016aD_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /><br/>
                    <asp:RadioButton ID="RaONA016b" GroupName="ONA016" runat="server" Text="否" />，
                        <asp:TextBox ID="TXTONA016bD" runat="server" width="100%" onkeyup="textcount(this,'TXTONA016bD_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTONA016bD_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /></td></tr>
                <tr><td class="td-padding" colspan="3">3.排水孔是否堵塞？</td>
                    <td style="text-align:left;" colspan="2">
                    <asp:RadioButton ID="RaONA017a" GroupName="ONA017" runat="server" Text="是" />，
                        <asp:TextBox ID="TXTONA017aD" runat="server" width="100%" onkeyup="textcount(this,'TXTONA017aD_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTONA017aD_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /><br/>
                    <asp:RadioButton ID="RaONA017b" GroupName="ONA017" runat="server" Text="否" />，
                        <asp:TextBox ID="TXTONA017bD" runat="server" width="100%" onkeyup="textcount(this,'TXTONA017bD_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTONA017bD_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /><br/>
                    <asp:RadioButton ID="RaONA017c" GroupName="ONA017" runat="server" Text="無排水孔" /></td></tr>
                <tr class="tr-title">
                    <td>四、抽水設施</td>
                    <td colspan="2"><asp:RadioButton ID="RaONA018a" GroupName="ONA018" runat="server" Text="有此設施" onchange="SetRadioSel();" />（請接續填寫）</td>
                    <td colspan="2"><asp:RadioButton ID="RaONA018b" GroupName="ONA018" runat="server" Text="無此設施" onchange="SetRadioSel();" />（請跳至五、其他繼續填寫）</td>
                    
                </tr>
                <tr><td class="td-padding" colspan="3">1.是否功能正常？</td>
                    <td style="text-align:left;" colspan="2">
                    <asp:RadioButton ID="RaONA019a" GroupName="ONA019" runat="server" Text="是" />，
                        <asp:TextBox ID="TXTONA019aD" runat="server" width="100%" onkeyup="textcount(this,'TXTONA019aD_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTONA019aD_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /><br/>
                    <asp:RadioButton ID="RaONA019b" GroupName="ONA019" runat="server" Text="否" />，
                        <asp:TextBox ID="TXTONA019bD" runat="server" width="100%" onkeyup="textcount(this,'TXTONA019bD_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTONA019bD_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /></td></tr>
                <tr><td class="td-padding" colspan="3">2.是否有定期維修保養檢查及記錄？</td>
                    <td style="text-align:left;" colspan="2">
                    <asp:RadioButton ID="RaONA020a" GroupName="ONA020" runat="server" Text="是" />，
                        <asp:TextBox ID="TXTONA020aD" runat="server" width="100%" onkeyup="textcount(this,'TXTONA020aD_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTONA020aD_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /><br/>
                    <asp:RadioButton ID="RaONA020b" GroupName="ONA020" runat="server" Text="否" />，
                        <asp:TextBox ID="TXTONA020bD" runat="server" width="100%" onkeyup="textcount(this,'TXTONA020bD_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTONA020bD_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /></td></tr>
                <tr class="tr-title">
                    <td>五、其他</td>
                    <td></td>
                    <td></td>
                    <td></td><td></td>
                </tr>
                <tr><td class="td-padding" colspan="3">1.是否需要專業技師現場指導</td>
                    <td style="text-align:left;" colspan="2">
                    <asp:RadioButton ID="RaONA021a" GroupName="ONA021" runat="server" Text="是" />，說明：
                    <asp:TextBox ID="TXTONA022" runat="server" Width="150px" MaxLength="50" /><br/>
                    <asp:RadioButton ID="RaONA021b" GroupName="ONA021" runat="server" Text="否" /></td></tr>
                <tr><td class="td-padding" colspan="3">2.設施淤積、堵塞與龜裂…等異常徵兆是否規劃進行改善?</td>
                    <td style="text-align:left;" colspan="2">
                    <asp:RadioButton ID="RaONA023a" GroupName="ONA024" runat="server" Text="是" />，
                    規劃於
                    <asp:TextBox ID="TXTONA024" runat="server" width="120px" ReadOnly="true"></asp:TextBox>
                    <asp:CalendarExtender ID="TXTONA024_CalendarExtender" runat="server" TargetControlID="TXTONA024" Format="yyyy-MM-dd"></asp:CalendarExtender>
                    改善完成<br/>
                    <asp:RadioButton ID="RaONA023b" GroupName="ONA024" runat="server" Text="否" />，說明：
                        <asp:TextBox ID="TXTONA023bD" runat="server" width="100%" onkeyup="textcount(this,'TXTONA023bD_count','100');" MaxLength="100" />
                        <asp:Label ID="TXTONA023bD_count" runat="server" Text="(0/100)" ForeColor="Red" Visible="false" /></td></tr>
                <tr>
                    <td>六、建議事項</td>
                    <td colspan="4">
                        <asp:TextBox ID="TXTONA026" runat="server" TextMode="MultiLine"  onkeyup="textcount(this,'TXTONA026_count','250');" MaxLength="250" style="width:100%; height:80px; vertical-align:middle;" />
                        <asp:Label ID="TXTONA026_count" runat="server" Text="(0/250)" ForeColor="Red" /></td>
                </tr>
                <tr><td>檢查人員</td>
                    <td colspan="4"><asp:TextBox ID="TXTONA025" runat="server" Width="100%" MaxLength="100" /></td>
                </tr>
                </table>
                
            <br/>

      <div style="color:red";> 備註：<br/>
        <br/>
        1.	檢查時機<br/>
        <br/>
        <span style="padding-left:15px;">(1)	每年於五月一日前至少1次維修、檢查，並維持正常運作，若有損壞或阻塞，應立即修繕及清淤。<br></span>
        <br/>
        <span style="padding-left:15px;">(2)	於中央氣象局發布北部區域列入海上颱風警報警戒範圍或豪雨警報以上等級後，應自行檢查清淤以維持功能。<br></span>
        <br/>
        2.	前項之維修、檢查應製成紀錄並保存五年，供市府抽查。 </div>





                <div class="form-btn">
                    <asp:Button ID="DataLock" runat="server" Text="送出" OnClientClick="return chkInput('DataLock');" Visible="false" OnClick="DataLock_Click" />&nbsp&nbsp
                    <asp:Button ID="SaveCase" runat="server" Text="暫時存檔" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" />&nbsp&nbsp
                    <asp:Button ID="GoHomePage" runat="server" Text="返回案件列表" Visible="false" OnClick="GoHomePage_Click" />
                </div>
            </div>
        </div>

<%--        <div class="footer-s">
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
                    <span class="span2">建議使用IE11(含)以上，Chrome或Firefox版本瀏覽器　<b>資料更新：</b><asp:Label ID="ToDay" runat="server" Text=""/>　<b>來訪人數：</b><asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                    <span class="span2"><b>客服電話：</b>02-27929328 陳小姐　<b>信箱：</b>tcge7@geovector.com.tw　本系統由多維空間資訊有限公司開發維護 TEL：(02)27929328</span><br/>
			        <span class="span2">※為維護系統服務品質，本平台訂於每周三凌晨AM 4:00-6:30 進行系統維護更新，更新期間偶有瞬斷情形，敬請使用者避開該時段使用。謝謝！</span></p>
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
