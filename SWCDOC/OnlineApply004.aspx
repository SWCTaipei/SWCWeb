<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineApply004.aspx.cs" Inherits="SWCDOC_OnlineApply004" %>
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
    <link rel="stylesheet" type="text/css" href="../css/iris.css"/>
	<link href="../css/search.css" rel="stylesheet">
    
    <script type="text/javascript">
        function chkInput(jChkType) {
            var iValue01 = $("#TXTONA003").val();
            var iValue02 = $("#TXTONA004").val();
            var iValue03 = $("#TXTONA012").val(); 

            if (iValue01.trim() == "") {
                alert('請輸入預定'+document.getElementById("LBSWC004_1").innerText+'日期');
				$("#TXTONA003").focus();
                return false;
            }
            if (iValue02.trim() == "") {
                alert('請輸入預定完工日期');
				$("#TXTONA004").focus();
                return false;
            }
            if (addMonthsUTC(iValue02, 0) > addMonthsUTC(iValue01, 12)) {
                alert('「預定完工日期」不得超過「預訂'+document.getElementById("LBSWC004_1").innerText+'日期」1年');
				$("#TXTONA004").focus();
                return false;
            }
            if (iValue03.trim() == "") {
                alert('請輸入目的事業主管機關核定(展延)完工期限');
				$("#TXTONA012").focus();
                return false;
            }
            if (iValue02.trim() > iValue03.trim()) {
                alert('「預計完工日期」不得超過「目的事業主管機關核定(展延)完工期限」');
				$("#TXTONA004").focus();
                return false;
            }
            //if (typeof Link013 == 'object') { } else
            //{
            //    alert('請上傳目的事業主管機關核定(展延)證明文件附件');
            //    return false;
            //}
			
			if (jChkType == 'DataLock') {
				if (document.getElementById("Link006").href === '')
				{
					alert('請上傳監造契約影本');
					$("#TXTONA006_fileupload").focus();
					return false;
				}
				if (document.getElementById("Link007").href === '')
				{
					alert('請上傳開發範圍界樁照片及位置標示於圖面');
					$("#TXTONA007_fileupload").focus();
					return false;
				}
				if (document.getElementById("Link008").href === '')
				{
					alert('請上傳開挖整地範圍界樁照片及位置標示於圖面(非保護區免設置)，切結書');
					$("#TXTONA008_fileupload").focus();
					return false;
				}
				if (document.getElementById("Link009").href === '')
				{
					alert('請上傳施工標示牌');
					$("#TXTONA009_fileupload").focus();
					return false;
				}
				if (document.getElementById("Link010").href === '')
				{
					alert('請上傳災害搶救小組名冊（敘明工地負責人及相關人員行動電話）');
					$("#TXTONA010_fileupload").focus();
					return false;
				}
				if (document.getElementById("LBSWC142").innerText.trim() == '是')
				{
					if (document.getElementById("Link026").href === '')
					{
						alert('請上傳道路挖掘許可申請文件');
						$("#TXTONA026_fileupload").focus();
						return false;
					}
				}
				
				
				if (document.getElementById("TextBox1").value == "") {
					alert('請輸入施工廠商');
					$("#TextBox1").focus();
					return false;
				}
				if (document.getElementById("TextBox2").value == "") {
					alert('請輸入統一編號');
					$("#TextBox2").focus();
					return false;
				}
				if (document.getElementById("TextBox2").value.length !== 8) {
					alert('統一編號長度需為8碼');
					$("#TextBox2").focus();
					return false;
				}
				if (document.getElementById("TextBox3").value == "") {
					alert('請輸入工地負責人員');
					$("#TextBox3").focus();
					return false;
				}
				if (document.getElementById("TextBox4").value == "") {
					alert('請輸入工地負責人員手機');
					$("#TextBox4").focus();
					return false;
				}
				if (document.getElementById("TextBox4").value.length !== 10) {
					alert('工地負責人員手機長度需為10碼');
					$("#TextBox4").focus();
					return false;
				}
				if (document.getElementById("TextBox4").value.substr(0, 2) !== "09") {
					alert('請輸入有效的工地負責人員手機');
					$("#TextBox4").focus();
					return false;
				}
				if (document.getElementById("LBSWC041").innerText.trim() == '') 
				{ 
					alert('請確認保證金是否繳交 或無需繳納保證金。');
					$("#LBSWC041").focus();
					return false; 
				}
                if (document.getElementById("CHKONA015").checked == true) 
				{ }
				else 
				{
					alert('請詳閱水土保持計畫監造須知。');
					$("#CHKONA015").focus();
					return false; 
				}
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
            <div class="header header-s clearfix"><a href="HaloPage001.aspx" class="logo-s"></a>
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
                    <span><asp:Literal ID="TextUserName" runat="server">先生，您好</asp:Literal></span>
                </div>
            </div>
        </div>
    
        
  <div class="content-s">
    <div class="startReport facilityMaintain form">
      <h1>水土保持計畫<asp:Label ID="LBSWC004_1" runat="server" Text="開工/復工"/>申報書<br>
        <br>
      </h1>

            <table class="startReport-base">
            <tr><td colspan="2" style="text-align:left; font-weight:bold; width:40%;"><asp:Label ID="LBSWC004_2" runat="server" Text="開工/復工"/>申報書編號</td>
                <td colspan="4">
                    <asp:Label ID="TXTONA001" runat="server" Visible="true"/></td></tr>
            <tr><td colspan="2" style="text-align: left; font-weight:bold; width:20%;">水保局編號</td>
                <td colspan="4">
                    <asp:Label ID="LBSWC002" runat="server"/>
                    <asp:Label ID="LBSWC000" runat="server" Visible="false" />
                    <asp:Label ID="LBSWC004" runat="server" Visible="false" />
                    <asp:Label ID="LBSWC025" runat="server" Visible="false" /></td></tr>
            <tr><td colspan="2" style="text-align: left; font-weight:bold;">申報日期</td>
                <td colspan="4">
                    <asp:Label ID="TXTONA002" runat="server"/></td></tr>
            <tr><td rowspan="3" style="font-weight:bold; width:18%;">水土保持義務人</td>
                <td style="width:20%;">姓名或名稱</td>
                <td class="bgcolorb" colspan="3" >
                    <asp:Label ID="LBSWC013" runat="server"/></td></tr>
            <tr><td>身分證或營利事業統一編號</td>
                <td class="bgcolorb" colspan="3" >
                    <asp:Label ID="LBSWC013ID" runat="server"/></td></tr>
            <tr><td>住居所或營業所</td>
                <td class="bgcolorb" colspan="3">
                    <asp:Label ID="LBSWC014" runat="server"/></td></tr>
            <tr><td rowspan="3" style="font-weight:bold;">水土保持計畫(核定本)</td>
                <td>計畫名稱</td>
                <td class="bgcolorb" colspan="3">
                    <asp:Label ID="LBSWC005" runat="server"/></td></tr>
            <tr><td>核定日期及字號</td>
                <td class="bgcolorb" colspan="3">
                    <asp:Label ID="LBSWC038" runat="server"/>
                    <asp:Label ID="LBSWC039" runat="server"/></td></tr>
            <tr><td>實施地點及土地標示</td>
                <td class="bgcolorb" colspan="3">
                    <asp:GridView ID="GVCadastral" runat="server" AutoGenerateColumns="False" CssClass="ADDCadastral"  PagerStyle-CssClass="pgr" 
                        OnPageIndexChanged="GVCadastral_PageIndexChanged" OnPageIndexChanging="GVCadastral_PageIndexChanging" PageSize="5" AllowPaging="true">
                        <Columns>
                            <asp:BoundField DataField="序號" HeaderText="序號"  />
                            <asp:BoundField DataField="區" HeaderText="區" />
                            <asp:BoundField DataField="段" HeaderText="段" />
                            <asp:BoundField DataField="小段" HeaderText="小段" />
                            <asp:BoundField DataField="地號" HeaderText="地號" />
                            <asp:BoundField DataField="土地使用分區" HeaderText="使用分區" />
                            <asp:BoundField DataField="土地可利用限度" HeaderText="可利用限度" />
                            <asp:BoundField DataField="林地類別" HeaderText="林地類別" />
                            <asp:BoundField DataField="地質敏感區" HeaderText="地質敏感區" />
                        </Columns>
                    </asp:GridView></td></tr>
            <tr><td colspan="2" style="font-weight:bold;">預定<asp:Label ID="LBSWC004_3" runat="server" Text="開工/復工"/>日期<span style="color: red;font-family:cursive;">＊</span></td>
                <td style="width:30%">
                    <asp:TextBox ID="TXTONA003" runat="server" width="120px" autocomplete="off"></asp:TextBox>
                    <asp:CalendarExtender ID="TXTONA003_CalendarExtender" runat="server" TargetControlID="TXTONA003" Format="yyyy-MM-dd"></asp:CalendarExtender>
                    <span class="gray">（範例：2020-01-02）</span>
                </td>
                <td style="width:20%;font-weight:bold;">預定完工日期<span style="color: red;font-family:cursive;">＊</span></td>
                <td style="width:40%">
                    <asp:TextBox ID="TXTONA004" runat="server" width="120px" autocomplete="off"></asp:TextBox>
                    <asp:CalendarExtender ID="TXTONA004_CalendarExtender" runat="server" TargetControlID="TXTONA004" Format="yyyy-MM-dd"></asp:CalendarExtender>
                    <br/>
                    <span class="gray">（範例：2020-01-02）</span>
                </td></tr>
            <tr><td colspan="2" style="font-weight:bold;">目的事業主管機關核定(展延)完工期限及證明文件<span style="color: red;font-family:cursive;">＊</span></td>
                <td style="width:30%" colspan="3">
                    <asp:TextBox ID="TXTONA012" runat="server" width="120px" autocomplete="off"></asp:TextBox>
                    <asp:CalendarExtender ID="TXTONA012_CalendarExtender" runat="server" TargetControlID="TXTONA012" Format="yyyy-MM-dd"></asp:CalendarExtender>
                    <span class="gray">（範例：2020-01-02）</span>
                    <br/>
                    <br/>
                    <asp:TextBox ID="TXTONA013" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA013_fileupload" runat="server" />
                    <asp:Button ID="TXTONA013_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA013_fileuploadok_Click" />
                    <asp:Button ID="TXTONA013_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA013_fileuploaddel_Click" />
                    <br/><asp:HyperLink ID="Link013" runat="server" Target="_blank" />
                    <br/><span style="color:red;">※ 可於<a href="https://dba.gov.taipei/" target="_blank">臺北市建管處網站</a>查得建照工期者免上傳</span>                   
                    <br/><span style="color:red;">※ 上傳格式限定為pdf、odt或doc檔案，大小請於50mb以內</span><br/></td></tr>
            <tr><td rowspan="3" style="font-weight:bold;">監造技師</td>
                <td>姓名</td>
                <td class="bgcolorb">
                    <asp:Label ID="LBSWC045Name" runat="server"/>
                    <asp:HiddenField ID="TXTONA014" runat="server" />
                </td>
                <td>執業執照字號</td>
                <td class="bgcolorb">
                    <asp:Label ID="LBSWC045OrgIssNo" runat="server"/></td></tr>
            <tr><td>事務所或公司名稱</td>
                <td class="bgcolorb">
                    <asp:Label ID="LBSWC045OrgName" runat="server"/></td>
                <td>營利事業統一編號</td>
                <td class="bgcolorb">
                    <asp:Label ID="LBSWC045OrgGUINo" runat="server"/></td></tr>
            <tr><td>事務所或公司地址</td>
                <td colspan="3" class="bgcolorb">
                    <asp:Label ID="LBSWC045OrgAddr" runat="server"/></td></tr>
            <tr><td style="font-weight:bold;">監造建築師</td>
                <td>姓名</td>
                <td colspan="3">
                    <asp:DropDownList ID="DDLSWC135" runat="server" CssClass="select" />
                </td></tr>
			<tr><td colspan="1" rowspan="8" style="font-weight:bold;">檢附文件</td>
                
                <td>1.水土保持保證金繳納證明</td>
				<td colspan="3"><asp:Label ID="LBSWC041" runat="server"/></td>
                <td colspan="3" style="display:none;"> 
                    <asp:TextBox ID="TXTONA005" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA005_fileupload" runat="server" />
                    <asp:Button ID="TXTONA005_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA005_fileuploadok_Click" />
                    <asp:Button ID="TXTONA005_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA005_fileuploaddel_Click" />
                    <br/><asp:HyperLink ID="Link005" runat="server" Target="_blank" />
                </td>
			</tr>
            <tr>
			
                <td>2.監造契約影本<span style="color: red;font-family:cursive;">＊</span></td>
                <td colspan="3">
                    <asp:TextBox ID="TXTONA006" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA006_fileupload" runat="server" />
                    <asp:Button ID="TXTONA006_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA006_fileuploadok_Click" />
                    <asp:Button ID="TXTONA006_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA006_fileuploaddel_Click" />
                    <br/><asp:HyperLink ID="Link006" runat="server" Target="_blank" /></td></tr>
            <tr>
                <td>3.開發範圍界樁照片及位置標示於圖面<a href="../sysFile/水保計畫界樁格式與範本.pdf" target="_blank">（範例）</a><span style="color: red;font-family:cursive;">＊</span></td>
                <td colspan="3">
                    <asp:TextBox ID="TXTONA007" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA007_fileupload" runat="server" />
                    <asp:Button ID="TXTONA007_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA007_fileuploadok_Click" />
                    <asp:Button ID="TXTONA007_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA007_fileuploaddel_Click" />
                    <br/><asp:HyperLink ID="Link007" runat="server" Target="_blank" /></td></tr>
            <tr>
                <td>4.開挖整地範圍界樁照片及位置標示於圖面（非保護區免設置），切結書<a href="../sysFile/範例.ZIP" target="_blank">（範例）</a><span style="color: red;font-family:cursive;">＊</span></td>
                <td colspan="3">
                    <asp:TextBox ID="TXTONA008" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA008_fileupload" runat="server" />
                    <asp:Button ID="TXTONA008_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA008_fileuploadok_Click" />
                    <asp:Button ID="TXTONA008_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA008_fileuploaddel_Click"/>
                    <br/><asp:HyperLink ID="Link008" runat="server" Target="_blank" /></td></tr>
            <tr>
                <td>5.施工標示牌<span style="color: red;font-family:cursive;">＊</span></td>
                <td colspan="3">
                    <asp:TextBox ID="TXTONA009" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA009_fileupload" runat="server" />
                    <asp:Button ID="TXTONA009_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA009_fileuploadok_Click"/>
                    <asp:Button ID="TXTONA009_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA009_fileuploaddel_Click"/>
                    <br/><asp:HyperLink ID="Link009" runat="server" Target="_blank" /></td></tr>
            <tr>
                <td>6.災害搶救小組名冊（敘明工地負責人及相關人員行動電話）<span style="color: red;font-family:cursive;">＊</span></td>
                <td colspan="3">
                    <asp:TextBox ID="TXTONA010" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA010_fileupload" runat="server" />
                    <asp:Button ID="TXTONA010_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA010_fileuploadok_Click" />
                    <asp:Button ID="TXTONA010_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA010_fileuploaddel_Click"/>
                    <br/><asp:HyperLink ID="Link010" runat="server" Target="_blank" /></td></tr>
            <tr>
                <td>7.本案聯外排水如涉及道路挖掘許可申請，應檢附許可文件<span ID="sp142" runat="server" style="color: red;font-family:cursive;">＊</span></td>
                <td colspan="3">
					<asp:Label ID="LBSWC142" runat="server" class="none"/>
					<asp:Panel ID="PSWC142Y" runat="server" Visible="false">
						<asp:TextBox ID="TXTONA026" runat="server" Width="70px" Visible="False" />
						<asp:FileUpload ID="TXTONA026_fileupload" runat="server" />
						<asp:Button ID="TXTONA026_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA026_fileuploadok_Click" />
						<asp:Button ID="TXTONA026_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA026_fileuploaddel_Click"/>
						<br/><asp:HyperLink ID="Link026" runat="server" Target="_blank" />
					</asp:Panel>
					<asp:Panel ID="PSWC142N" runat="server" Visible="false">
						<asp:Label ID="TXTONA027" runat="server" Text=""/>
					</asp:Panel>
				</td></tr>
            <tr>
                <td>8.廠商聯絡資料<span style="color: red;font-family:cursive;">＊</span></td>
                <td colspan="3">
                    <table class="companydata">
                        <tr>
                            <th>營造單位：</th>
                            <td><asp:TextBox ID="TextBox1" runat="server" MaxLength="20" /></td>
                        </tr>
                        <tr>
                            <th>統一編號：</th>
                            <td><asp:TextBox ID="TextBox2" runat="server" MaxLength="10" /></td>
                        </tr>
                        <tr>
                            <th>工地負責人員：</th>
                            <td><asp:TextBox ID="TextBox3" runat="server" MaxLength="10" /></td>
                        </tr>
                        <tr>
                            <th>工地負責人員手機：</th>
                            <td><asp:TextBox ID="TextBox4" runat="server" MaxLength="10" /></td>
                        </tr>
                    </table>
                    
                </td>
            </tr>
            <tr><td colspan="5" style="color:red; text-align:center" > ※ 上傳格式限定為pdf、odt或doc檔案，大小請於50mb以內</td></tr>
			<tr>
			   <td colspan="6"><asp:CheckBox ID="CHKONA015" runat="server" />「<a href="../sysFile/水土保持計畫監造須知.docx" target="_blank">已詳閱水土保持計畫監造須知</a>」</td>
			</tr>
			
			 <asp:Panel ID="hidden" runat="server" Visible="false">
			
        <tr><td colspan="5" style="text-align: center;border-bottom: none;">
                上開水土保持計畫訂於
                    <asp:TextBox ID="TXTONA011" runat="server" width="120px" autocomplete="off"></asp:TextBox>
                    <asp:CalendarExtender ID="TXTONA011＿CalendarExtender" runat="server" TargetControlID="TXTONA011" Format="yyyy-MM-dd"></asp:CalendarExtender>
                開工，此致</td></tr>
            <tr><td colspan="3" style="border-top: none;border-right:none;border-bottom:none;text-align: center;vertical-align: top;">臺北市政府工務局大地工程處</td>
                <td colspan="3" style="border-top: none;border-left:none;border-bottom:none;"></td></tr>
            <tr><td colspan="3" style="border-top: none;border-right:none;text-align: center;vertical-align: top;"></td>
                <td colspan="3" style="border-top: none;border-left:none;">
                    水土保持義務人：<asp:Label ID="LBSWC013a" runat="server"/><br/>
                    　承辦監造技師：<asp:Label ID="LBSWC045a" runat="server"/></td></tr></asp:Panel>
            </table>
      
                <asp:Label ID="ReqCount" runat="server" Text="" style="display:none;" />
                <asp:Panel ID="SignList" runat="server"><br/><br/>

                <div><span style="background-color: #FFFF99; font-size: 16pt; font-weight: bold; margin-top:1em;">退補正歷程</span></div><br/>
            
                <asp:GridView ID="GVSignList" runat="server" DataSourceID="SqlDataSourceSign" CssClass="retirement" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="TH001n" HeaderText="退文日期" SortExpression="SWC000" ItemStyle-Width="200px"/>
                        <asp:BoundField DataField="TH005n" HeaderText="改正期限" SortExpression="SWC002" ItemStyle-Width="190px"/>
                        <asp:BoundField DataField="THName" HeaderText="退文人員" SortExpression="SWC004" ItemStyle-Width="140px" Visible="false"/>
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
		<script type="text/javascript" src="../js/search.js"></script>
		<script>
			$(function (){
				$(".select").select2({
					language: 'zh-TW',
					width: '20%',
					maximumInputLength: 10,
					minimumInputLength: 0,
					tags: false,
					placeholder: "請選擇",
				});
			})
		</script>
    </div>
    </form>
</body>
</html>
