<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineApply009.aspx.cs" Inherits="SWCDOC_OnlineApply009" %>
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
    <link rel="stylesheet" type="text/css" href="../css/all.css?202210270232"/>
    <link rel="stylesheet" type="text/css" href="../css/OnlineApply001.css"/>
    <link rel="stylesheet" type="text/css" href="../css/iris.css"/>
    
    <script type="text/javascript">
        function chkInput(jChkType) {

            if (document.getElementById("Link003").innerText == '')
            {
                alert('請上傳水土保持竣工書圖及照片');
                return false;
            }
			if (document.getElementById("LBSWC007").innerText == "水土保持計畫") {
                if (document.getElementById('CHKONA016').checked == false && document.getElementById("Link015").innerText == '') {
                    alert('請上傳滯洪沉砂持告示牌竣工照片');
                    return false;
                }
            }
            if (document.getElementById("Link004").innerText == '') 
            {
                alert('請上傳承辦監造技師簽證竣工檢核表');
                return false;
            }
            //if (document.getElementById("Link007").innerText == '')
            //{
            //    alert('請上傳聯外排水屬抽排者，檢附專業技師簽證之查驗成果及後續管理維護計畫');
            //    return false;
            //}
            //if (document.getElementById("TXTONA005").value == '') {
            //    alert('請輸入完工日期');
            //    return false;
            //}
            if (jChkType == 'DataLock') {
                if (document.getElementById('CHKONA009').checked == true) { } else {
                    alert('請確定擋土設施已登錄至台北市山坡地人工邊坡安全資訊系統或無擋土設施');
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
            <div class="header header-s clearfix"><a href="SWC001.aspx" class="logo-s"></a>
                <div class="header-menu-s">
                    <ul>
                        <li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
                        <li>|</li>
                        <li><a href="http://tcgeswc.taipei.gov.tw/index_new.aspx" title="水土保持計畫查詢系統" target="_blank">水土保持計畫查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="https://tslm.swc.taipei/tslmwork/" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
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

        
  <div class="content-t">
    <div class="startReport completed form">
      <h1>水土保持計畫完工申報書<br>
        <br>
      </h1>
     

            <table class="Apply0091">
            <tr><td colspan="2">完工申報編號</td>
                <td colspan="2"><asp:Label ID="TXTONA001" runat="server" Visible="true"/></td></tr>
            <tr><td colspan="2">申報日期</td>
                <td colspan="2"><asp:Label ID="TXTONA002" runat="server" Visible="true"/></td></tr>
            <tr><td rowspan="6" class="text_tabletitle" style="width:170px;">水土保持書件</td>
                <td class="bgcolorb" style="width:20%;">水保局編號</td>
                <td colspan="2" class="bgcolorb">
                    <asp:Label ID="LBSWC002" runat="server"/>
                    <asp:Label ID="LBSWC000" runat="server" Visible="false"/>
                    <asp:Label ID="LBSWC007" runat="server" style="color:#f3feff"/>
                    <asp:Label ID="LBSWC025" runat="server" Visible="false"/>
                    <asp:Label ID="LBSWC117" runat="server" Visible="false"/></td></tr>
            <tr><td class="bgcolorb">計畫名稱</td>
                <td class="bgcolorb" colspan="2">
                    <asp:Label ID="LBSWC005" runat="server"/></td></tr>
            <tr><td class="bgcolorb">核定日期及字號</td>
                <td class="bgcolorb" colspan="2">
                    <asp:Label ID="LBSWC038" runat="server"/>
                    <asp:Label ID="LBSWC039" runat="server"/></td></tr>
            <tr><td class="bgcolorb">實施地點及土地標示</td>
                <td class="bgcolorb" colspan="2"style="line-height:40px;">
                    <asp:GridView ID="GVCadastral" runat="server" AutoGenerateColumns="False" CssClass="ADDCadastral"  PagerStyle-CssClass="pgr" 
                        OnPageIndexChanged="GVCadastral_PageIndexChanged" OnPageIndexChanging="GVCadastral_PageIndexChanging" PageSize="5" AllowPaging="true">
                        <Columns>
                            <asp:BoundField DataField="序號" HeaderText="序號" />
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
            <tr><td class="bgcolorb">水土保持施工許可證日期文號</td>
                <td class="bgcolorb" colspan="2">
                    <asp:Label ID="LBSWC043" runat="server"/>
                    <asp:Label ID="LBSWC044" runat="server"/></td></tr>
            <tr><td class="bgcolorb">開工日期</td>
                <td class="bgcolorb" colspan="2"><asp:Label ID="LBSWC051" runat="server"/></td></tr>
            <tr><td rowspan="3" class="text_tabletitle"> 水土保持義務人</td>
                <td class="bgcolorb">姓名或名稱</td>
                <td class="bgcolorb"colspan="2"><asp:Label ID="LBSWC013" runat="server"/></td></tr>
            <tr><td class="bgcolorb">身分證或營利事業統一編號</td>
                <td class="bgcolorb"colspan="2"><asp:Label ID="LBSWC013ID" runat="server"/></td></tr>
            <tr><td class="bgcolorb">住居所或營業所</td>
                <td class="bgcolorb"colspan="2" style="line-height:40px;">
                    <asp:Label ID="LBSWC014" runat="server"/></td></tr>
            <tr><td rowspan="6" class="text_tabletitle">監造技師</td>
                <td class="bgcolorb">姓名</td>
                <td class="bgcolorb" colspan="2">
                    <asp:Label ID="LBSWC045Name" runat="server"/></td></tr>
            <tr><td class="bgcolorb">執業機構名稱</td>
                <td class="bgcolorb" colspan="2"><asp:Label ID="LBSWC045OrgName" runat="server"/></td></tr>
            <tr><td class="bgcolorb">執業機構地址</td>
                <td class="bgcolorb" style="line-height:40px;" colspan="2">
                    <asp:Label ID="LBSWC045OrgAddr" runat="server"/></td></tr>
            <tr><td class="bgcolorb">執業執照字號</td>
                <td class="bgcolorb" colspan="2">
                    <asp:Label ID="LBSWC045OrgIssNo" runat="server"/></td></tr>
            <tr><td class="bgcolorb">營利事業統一編號</td>
                <td class="bgcolorb" colspan="2"><asp:Label ID="LBSWC045OrgGUINo" runat="server"/></td></tr>
            <tr><td class="bgcolorb">電話</td>
                <td class="bgcolorb" colspan="3"><asp:Label ID="LBSWC045OrgTel" runat="server"/></td></tr>
            <tr><td rowspan="4" class="text_tabletitle"> 檢附文件</td>
                <td class="middle">1.水土保持竣工書圖及照片<span style="color: red;font-family:cursive;">＊</span></td>
                <td colspan="2">
                    <asp:TextBox ID="TXTONA003" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA003_fileupload" runat="server" />
                    <asp:Button ID="TXTONA003_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA003_fileuploadok_Click"/>
                    <asp:Button ID="TXTONA003_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA003_fileuploaddel_Click" />
                    <br/><asp:HyperLink ID="Link003" runat="server" Target="_blank" />
                    <br/><span style="color:red;">※ 上傳格式限定為pdf、odt或doc檔案，大小請於50mb以內</span><br/>
                    <asp:TextBox ID="TXTONA008" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA008_fileupload" runat="server" />
                    <asp:Button ID="TXTONA008_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA008_fileuploadok_Click" />
                    <asp:Button ID="TXTONA008_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA008_fileuploaddel_Click" />
                    <br/><asp:HyperLink ID="Link008" runat="server" Target="_blank" />
                    <br/><span style="color:red;">※ 上傳格式限定為CAD，檔案大小請於50mb以內</span><br/>
                </td></tr>
            <tr><td class="middle">2.滯洪沉砂池告示牌竣工照片<asp:Label ID="Required_02" runat="server" Visible="false"><span style="color: red;font-family:cursive;">＊</span></asp:Label><br/></td>
                <td colspan="2">
                    
					<asp:TextBox ID="TXTONA015" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA015_fileupload" runat="server" />
                    <asp:Button ID="TXTONA015_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA015_fileuploadok_Click"/>
                    <asp:Button ID="TXTONA015_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA015_fileuploaddel_Click" />
                    <br/><asp:HyperLink ID="Link015" runat="server" Target="_blank" />
                    <br/><span style="color:red;">※ 上傳格式限定為pdf、odt、doc或EXCEL檔案，大小請於50mb以內</span>
					<br/><br/>
					<asp:CheckBox ID="CHKONA016" runat="server" Text="本案未規劃滯洪沉砂池" CssClass="ckeckbox" />
					</td></tr>
            <tr><td class="middle">3.承辦監造技師簽證竣工檢核表<span style="color: red;font-family:cursive;">＊</span><br/><asp:HyperLink ID="NewUser" runat="server" Text="excel範本下載" NavigateUrl="../sysFile/計畫竣工檢核表範本.xlsx" /></td>
                <td colspan="2">
                    <asp:TextBox ID="TXTONA004" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA004_fileupload" runat="server" />
                    <asp:Button ID="TXTONA004_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA004_fileuploadok_Click"/>
                    <asp:Button ID="TXTONA004_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA004_fileuploaddel_Click" />
                    <br/><asp:HyperLink ID="Link004" runat="server" Target="_blank" />
                    <br/><span style="color:red;">※ 上傳格式限定為pdf、odt、doc或EXCEL檔案，大小請於50mb以內</span><br/></td></tr>
            <tr><td class="middle" style="line-height:1.4">4.聯外排水屬抽排者，檢附水土保持專業技師簽證之查驗成果及後續管理維護計畫(包含至少3年期專業技師管理檢測委託契約)。<br/></td>
                <td class="middle" colspan="2">
                    <asp:TextBox ID="TXTONA007" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TXTONA007_fileupload" runat="server" />
                    <asp:Button ID="TXTONA007_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA007_fileuploadok_Click"/>
                    <asp:Button ID="TXTONA007_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA007_fileuploaddel_Click" />
                    <br/><asp:HyperLink ID="Link007" runat="server" Target="_blank" />
                    <br/><span style="color:red;">※ 上傳格式限定為pdf、odt或doc檔案，大小請於50mb以內</span><br/></td></tr>
          <tr>
              <td class="middle">5.保證金退款帳戶</td>
              <td colspan="2">
                  
                  戶名：<asp:TextBox ID="TBONA010" runat="server" MaxLength="50"></asp:TextBox><span class="gray">（範例：王小明）</span><br /><br />
                  銀行：<asp:DropDownList ID="DDLONA011" runat="server">
                        <asp:ListItem>請選擇</asp:ListItem>
                        <asp:ListItem>004-臺灣銀行</asp:ListItem>
                        <asp:ListItem>005-臺灣土地銀行</asp:ListItem>
                        <asp:ListItem>006-合作金庫商業銀行</asp:ListItem>
                        <asp:ListItem>007-第一商業銀行</asp:ListItem>
                        <asp:ListItem>008-華南商業銀行</asp:ListItem>
                        <asp:ListItem>009-彰化商業銀行</asp:ListItem>
                        <asp:ListItem>011-上海商業儲蓄銀行</asp:ListItem>
                        <asp:ListItem>012-台北富邦商業銀行</asp:ListItem>
                        <asp:ListItem>013-國泰世華商業銀行</asp:ListItem>
                        <asp:ListItem>015-中國輸出入銀行</asp:ListItem>
                        <asp:ListItem>016-高雄銀行</asp:ListItem>
                        <asp:ListItem>017-兆豐國際商業銀行</asp:ListItem>
                        <asp:ListItem>021-花旗(台灣)商業銀行</asp:ListItem>
                        <asp:ListItem>048-王道商業銀行</asp:ListItem>
                        <asp:ListItem>050-臺灣中小企業銀行</asp:ListItem>
                        <asp:ListItem>052-渣打國際商業銀行</asp:ListItem>
                        <asp:ListItem>053-台中商業銀行</asp:ListItem>
                        <asp:ListItem>054-京城商業銀行</asp:ListItem>
                        <asp:ListItem>081-滙豐(台灣)商業銀行</asp:ListItem>
                        <asp:ListItem>101-瑞興商業銀行</asp:ListItem>
                        <asp:ListItem>102-華泰商業銀行</asp:ListItem>
                        <asp:ListItem>103-臺灣新光商業銀行</asp:ListItem>
                        <asp:ListItem>108-陽信商業銀行</asp:ListItem>
                        <asp:ListItem>118-板信商業銀行</asp:ListItem>
                        <asp:ListItem>147-三信商業銀行</asp:ListItem>
                        <asp:ListItem>700-中華郵政</asp:ListItem>
                        <asp:ListItem>803-聯邦商業銀行</asp:ListItem>
                        <asp:ListItem>805-遠東國際商業銀行</asp:ListItem>
                        <asp:ListItem>806-元大商業銀行</asp:ListItem>
                        <asp:ListItem>807-永豐商業銀行</asp:ListItem>
                        <asp:ListItem>808-玉山商業銀行</asp:ListItem>
                        <asp:ListItem>809-凱基商業銀行</asp:ListItem>
                        <asp:ListItem>810-星展(台灣)商業銀行</asp:ListItem>
                        <asp:ListItem>812-台新國際商業銀行</asp:ListItem>
                        <asp:ListItem>815-日盛國際商業銀行</asp:ListItem>
                        <asp:ListItem>816-安泰商業銀行</asp:ListItem>
                        <asp:ListItem>822-中國信託商業銀行</asp:ListItem>
                    </asp:DropDownList><br /><br />
                  分行：<asp:TextBox ID="TBONA012" runat="server" MaxLength="20"></asp:TextBox><span class="gray">（範例：市府分行）</span><br /><br />
                  帳號：<asp:TextBox ID="TBONA013" runat="server" MaxLength="20"></asp:TextBox><span class="gray">（範例：1076579621330125）</span><br /><br />
                  
                  帳戶影本：
                    <asp:TextBox ID="TBONA014" runat="server" Width="70px" Visible="False" />
                    <asp:FileUpload ID="TBONA014_fileupload" runat="server" />
                    <asp:Button ID="TBONA014_fileuploadok" runat="server" Text="上傳檔案" OnClick="TBONA014_fileuploadok_Click"/>
                    <asp:Button ID="TBONA014_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TBONA014_fileuploaddel_Click" />
                    <br/><asp:HyperLink ID="Link014" runat="server" Target="_blank" />
                    <br/><span style="color:red;">※ 上傳格式限定為jpg、png或pdf檔案，大小請於5mb內</span>
              </td>
          </tr>
                <tr>
                    <td colspan="4">
                        <asp:CheckBox ID="CHKONA009" runat="server" Text ="" />
                        擋土設施已登錄至<a href="https://tpmsis.gov.taipei/TCGEMSIS/" title="台北市山坡地人工邊坡安全資訊系統" target="_blank">台北市山坡地人工邊坡安全資訊系統</a>或無擋土設施。</td>
                </tr>
					
					
					

<%--            <tr><td colspan="5" style="text-align: center;border-bottom: none;">
                上開水土保持計畫訂於
                <asp:TextBox ID="TXTONA005" runat="server" width="120px" autocomplete="off"></asp:TextBox>
                <asp:CalendarExtender ID="TXTONA005_CalendarExtender" runat="server" TargetControlID="TXTONA005" Format="yyyy-MM-dd"></asp:CalendarExtender>
                完工，此致</td></tr>
            <tr><td colspan="5" style="padding-left: 50px; border-top: none;border-bottom:none;">臺北市政府工務局大地工程處 </td></tr>
            <tr><td colspan="3" style="border-top: none;text-align: right;border-right:none;"></td>
                <td colspan="1" style="border-top: none;text-align: left;border-left:none;">
                    水土保持義務人：<asp:Label ID="LBSWC013a" runat="server"/><br/><br/>
                    　承辦監造技師：<asp:Label ID="LBSWC045a" runat="server"/></td></tr>--%>

            </table>
      
                
    </div>
  </div>

              <div class="OFsegG">
			  <div style="float:left"> <img src="../images/btn/btn009-08.png" alt=""/></div>
                <div class="lab">
                   <div class="labcolor1" style="color:#4e7a10;"><div class="icon1" style="background:#f0ffd9;"></div>原核定</div>
                   <div class="labcolor2" style="color:#000;"><div class="icon2" style="background:#fdfff0"></div>現場量測</div>
                </div>

                <asp:Label ID="GVMSG" runat="server" Text="查無資料" Visible="true" CssClass="nodata"/>
				<div style="clear:both;"></div>
                   <div class="detailsGrid">
                      <h2 class="SWCfl openh2">水保設施項目<img src="../images/btn/btn-close.png" alt=""/></h2>

                                <asp:GridView ID="SDIList" runat="server" CssClass="OFcheckG AutoNewLine" AutoGenerateColumns="False" Height="50" EmptyDataText="查無資料"
                                  OnDataBound="SDIList_DataBound" OnRowCommand="SDIList_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="SDIFD003" HeaderText="水土保持設施類別" HeaderStyle-Width="" />
                                        <asp:BoundField DataField="SDIFD004" HeaderText="設施名稱<br>（位置或編號）" HeaderStyle-Width="510px" HtmlEncode="false" />
                                        <asp:BoundField DataField="SDIFD005" HeaderText="設施型式" HeaderStyle-Width="270px" />
                <asp:TemplateField HeaderText="數" HeaderStyle-Width="10px">
                    <ItemTemplate>
                        <asp:Label ID="SDILB006" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDIFD006"))) %>' Visible="true"></asp:Label>
                        <asp:Label ID="SDILB006D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDIFD006D"))) %>' Visible="true"></asp:Label>
                        <asp:TextBox ID="CHK001" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001"))) %>' style="width:40px; text-align:center;" Visible="true" />
                        <asp:Label ID="A1" runat="server" Text='~' Visible="true"></asp:Label>
                        <asp:TextBox ID="CHK001_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001_1"))) %>' style="width:40px; text-align:center;" Visible="true" />
                        <asp:TextBox ID="CHK001D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001D"))) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false" />
                        <asp:TextBox ID="RCH001" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001"))) %>' style="width:40px; text-align:center;" Visible="false" />
                        <asp:TextBox ID="RCH001_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001_1"))) %>' style="width:40px; text-align:center;" Visible="false" />
                        <asp:TextBox ID="RCH001D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001D"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                                        <asp:BoundField DataField="SDIFD007" HeaderText="量" HeaderStyle-Width="10px" />
                <asp:TemplateField HeaderText="數量差異百分比" HeaderStyle-Width="400px">
                    <ItemTemplate>
                        <asp:Label ID="LBCHK002" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK002"))) %>' Visible="true"></asp:Label>
                        <asp:TextBox ID="TXTCHK002" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK002"))) %>' MaxLength="20" style="width:40px; text-align:center;" Visible="true" />
                        <asp:Label ID="LBCHK002pers" runat="server" Text='％' Visible="true"></asp:Label>
                        <asp:Label ID="LBCHK002_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK002_1"))) %>' Visible="true"></asp:Label>
                        <asp:TextBox ID="TXTCHK002_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK002_1"))) %>' MaxLength="20" style="width:40px; text-align:center;" Visible="true" />
						<asp:Label ID="LBCHK002pers_1" runat="server" Text='％' Visible="true"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                                        <asp:BoundField DataField="SDIFD008" HeaderText="檢核項目" HeaderStyle-Width="480px" />
                <asp:TemplateField HeaderText="" HeaderStyle-Width="60px">
                    <ItemTemplate>
                        <asp:Label ID="ITNONE03" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDIFD012"))) %>' Visible="true"></asp:Label>
						<asp:TextBox ID="CHK004" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004"))) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false"/>
						<asp:Label ID="A2" runat="server" Text='~' Visible="true"></asp:Label>
						<asp:TextBox ID="CHK004_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004_1"))) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false"/>
						<asp:TextBox ID="RCH004" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                        <asp:TextBox ID="RCH004_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004_1"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="尺寸" HeaderStyle-Width="260px">
                    <ItemTemplate>
                        <asp:Label ID="LabelX1" runat="server" Text="×" Visible="true" ></asp:Label>
                        <asp:Label ID="ITNONE04" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDIFD013"))) %>' Visible="true" ></asp:Label>
                        <asp:Label ID="LB004D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004D"))) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false" />
                        <asp:TextBox ID="CHK004D" runat="server" Text='<%# Convert.ToString(Eval("SDICHK004D")) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false" />
                        <asp:TextBox ID="CHK005" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK005"))) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false" />
                        <asp:Label ID="A3" runat="server" Text='~' Visible="true"></asp:Label>
						<asp:TextBox ID="CHK005_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK005_1"))) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false"/>
						<asp:TextBox ID="RCH005" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK005"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                        <asp:TextBox ID="RCH005_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK005_1"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                        <asp:TextBox ID="RCH004D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004D"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" HeaderStyle-Width="260px">
                    <ItemTemplate>                        
                        <asp:Label ID="LabelX2" runat="server" Text="×" Visible="true"></asp:Label>
                        <asp:Label ID="ITNONE05" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDIFD014"))) %>' Visible="true"></asp:Label>
                        <asp:TextBox ID="CHK006" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK006"))) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false" />
                        <asp:Label ID="A4" runat="server" Text='~' Visible="true"></asp:Label>
						<asp:TextBox ID="CHK006_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK006_1"))) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false"/>
						<asp:TextBox ID="RCH006" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK006"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                        <asp:TextBox ID="RCH006_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK006_1"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                                        <asp:BoundField DataField="SDIFD015" HeaderText="" HeaderStyle-Width="60px" />
                <asp:TemplateField HeaderText="尺寸差異百分比" HeaderStyle-Width="400px">
                    <ItemTemplate>
                        <asp:Label ID="LBCHK007" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK007"))) %>' Visible="true"></asp:Label>
                        <asp:TextBox ID="TXTCHK007" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK007"))) %>' style="width:40px; text-align:center;" Visible="true" />
                        <asp:Label ID="LBCHK007pers" runat="server" Text='％' Visible="true"></asp:Label>
                        <asp:Label ID="LBCHK007_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK007_1"))) %>' Visible="true"></asp:Label>
                        <asp:TextBox ID="TXTCHK007_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK007_1"))) %>' style="width:40px; text-align:center;" Visible="true" />
                        <asp:Label ID="LBCHK007pers_1" runat="server" Text='％' Visible="true"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                                        <asp:BoundField DataField="SDICHK008" HeaderText="檢查日期" HeaderStyle-Width="8%" />
                <asp:TemplateField HeaderText="施工完成" HeaderStyle-Width="100px">
                    <ItemTemplate>
                        <asp:DropDownList ID="DDLDONE" runat="server" SelectedValue='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK009"))) %>' Visible="false"><asp:ListItem>未完成</asp:ListItem><asp:ListItem>完成</asp:ListItem><asp:ListItem>已由永久設施取代</asp:ListItem></asp:DropDownList>
                        <asp:TextBox ID="RCH009" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK009"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>

                                        <asp:TemplateField Visible="true" HeaderStyle-Width="100px" >
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="MODIFYDATA" CommandArgument="<%#Container.DataItemIndex %>" CommandName="Modify" Text="修改" Visible="false" />
                                                <asp:HiddenField ID="HDSDI001" runat="server" Value='<%# Eval("SDIFD001") %>' />
                                                <asp:HiddenField ID="HDSDI011" runat="server" Value='<%# Eval("SDIFD011") %>' />
                                                <asp:HiddenField ID="HDSDINI" runat="server" Value='<%# Container.DataItemIndex %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
										<asp:TemplateField HeaderText="是否漸變" HeaderStyle-Width="5%">
											<ItemTemplate>
												<asp:Label ID="LB019" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDILB019"))) %>' style="width:40px; text-align:center;" />
											</ItemTemplate>
										</asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
							 </div>
							</div>
        
                <asp:Label ID="ReqCount" runat="server" Text="" style="display:none;" />
                <asp:Panel ID="SignList" runat="server"><br/><br/>

                <div><span style="background-color: #FFFF99; font-size: 16pt; font-weight: bold; margin-top:1em;">退補正歷程</span></div><br/>
            
                <asp:GridView ID="GVSignList" runat="server" DataSourceID="SqlDataSourceSign" CssClass="retirement" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="TH001n" HeaderText="退文日期" SortExpression="SWC000" ItemStyle-Width="200px"/>
                        <asp:BoundField DataField="TH005n" HeaderText="改正期限" SortExpression="SWC002" ItemStyle-Width="190px"/>
                        <asp:BoundField DataField="THName" HeaderText="退文人員" SortExpression="SWC004" Visible="false" ItemStyle-Width="140px"/>
                        <asp:BoundField DataField="TH004" HeaderText="說明" SortExpression="SWC005"  ItemStyle-Width="350px"/>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceSign" runat="server" ConnectionString="<%$ ConnectionStrings:SWCConnStr %>" SelectCommand="" OnSelected="SqlDataSourceSign_Selected" />

                </asp:Panel>

                    <div class="form-btn">
                        <asp:Button ID="DataLock" runat="server" Text="確認送出" OnClientClick="return chkInput('DataLock');" OnClick="DataLock_Click" />&nbsp&nbsp
                        <asp:Button ID="SaveCase" runat="server" Text="計算與暫存" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" />&nbsp&nbsp
                        <asp:Button ID="GoHomePage" runat="server" Text="返回瀏覽案件" OnClick="GoHomePage_Click" />
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
    </div>
    </form>
</body>
</html>
