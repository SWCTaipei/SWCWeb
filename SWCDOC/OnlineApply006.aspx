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
                    if (document.getElementById("Link010").href === '') {
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
                    if (document.getElementById("Link014").href === '') {
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
        function UpLoadChk(ObjId)
        {
            if (document.getElementById(ObjId).value == '') { alert('請選擇檔案'); return false;} else { return ChkFileSize(ObjId, '50', '.pdf.doc.docx.odt'); }
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
                        <li><a href="https://swc.taipei/swcinfo/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="http://tslm.swc.taipei/tslmwork/" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
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
                        <asp:Label ID="LBSWC000" runat="server" Visible="false"/>
                        <asp:Label ID="LBSWC025" runat="server" Visible="false" /></td></tr>
                <tr><td>計畫名稱</td>
                    <td><asp:Label ID="LBSWC005" runat="server"/></td></tr>
                <tr><td rowspan="3">變更資訊<span style="color: red;font-family:cursive;">＊</span></td>
                    <td style="line-height:40px; display:none;">
                        義務人名稱：
                        <asp:TextBox ID="TXTONA003" runat="server" autocomplete="off" MaxLength="50"></asp:TextBox>
                        <span class="gray">（ex：王小明）</span>
                        <br/>
                        身分證：
                        <asp:TextBox ID="TXTONA004" runat="server" autocomplete="off" MaxLength="20" style="margin-left:33px;"></asp:TextBox>
                        <span class="gray">（ex：X123456789）</span>
                        <br/>
                        電話：
                        <asp:TextBox ID="TXTONA005" runat="server" autocomplete="off" MaxLength="50" style="margin-left:49px;"></asp:TextBox>
                        <span class="gray">（ex：02-27921234#1234）</span>
                        <br/>
                        地址：
                        <asp:TextBox ID="TXTONA016" runat="server" autocomplete="off" MaxLength="80" style="margin-left:49px; width:400px;"></asp:TextBox>
                        <span class="gray">（ex：臺北市信義區松德路300號3樓）</span>
                        <br/>
						<br/>
					</td>
					<td style="line-height:40px;">
						<asp:CheckBox ID="CHKONA002" runat="server" Text ="變更義務人" /><br/>
						<table class="OAYM">
							<tbody>
								<tr>
									<th>姓名</th>
									<td><asp:TextBox ID="TXTSWCPNAME" runat="server" /></td>
									<th>身分證字號/統一編號</th>
									<td><asp:TextBox ID="TXTSWCPID" runat="server" /></td>
								</tr>
								<tr>
									<th>手機</th>
									<td colspan="3"><asp:TextBox ID="TXTSWCPPHONE" runat="server" /></td>
								</tr>
								<tr>
									<th style="vertical-align:middle;">地址</th>
									<td colspan="3">
										<asp:TextBox ID="TXTSWCPCODE" runat="server" width="60px" Enabled="false" />
										<asp:DropDownList ID="DDLSWCPCITY" AutoPostBack="true" OnSelectedIndexChanged="DDLSWCPCITY_SelectedIndexChanged" runat="server" width="80px" />
										<asp:DropDownList ID="DDLSWCPAREA" AutoPostBack="true" OnSelectedIndexChanged="DDLSWCPAREA_SelectedIndexChanged" runat="server" width="80px" />
										<asp:TextBox ID="TXTSWCPADDRESS" runat="server" width="400px" />
									</td>
									
								</tr>
							</tbody>
						</table>
						<asp:Button ID="AddAddress" runat="server" Text="加入清單" Height="26px" OnClick="AddAddress_Click" />
						<asp:TextBox ID="AddNO" runat="server" Text="0" Visible="false"/>
						<br>
						<asp:GridView ID="GVPEOPLE" runat="server" AutoGenerateColumns="False" CssClass="OAYMTB" OnRowCommand="GVPEOPLE_RowCommand" >
                            <Columns>
                                <asp:BoundField DataField="序號" HeaderText="序號" />
                                <asp:BoundField DataField="姓名" HeaderText="姓名" />
                                <asp:BoundField DataField="身分證字號" HeaderText="身分證字號/統一編號" />
                                <asp:BoundField DataField="手機" HeaderText="手機" />
                                <asp:BoundField DataField="地址ZipCode" HeaderText="地址" Visible="false" />
                                <asp:BoundField DataField="地址City" HeaderText="地址" Visible="false" />
                                <asp:BoundField DataField="地址District" HeaderText="地址" Visible="false" />
                                <asp:BoundField DataField="地址Address" HeaderText="地址" Visible="false" />
                                <asp:BoundField DataField="地址" HeaderText="地址" />
                                <asp:TemplateField Visible="true">
                                    <ItemTemplate>
                                        <asp:Button runat="server" CommandArgument="<%#Container.DataItemIndex %>" CommandName="delfile001" Text="刪除" OnClientClick="return confirm('確認刪除這筆資料?')"  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br/>
                        檢附目的事業主管機關核准變更文件：<br/>
                        <asp:TextBox ID="TXTONA006" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTONA006_fileupload" runat="server" />
                        <asp:Button ID="TXTONA006_fileuploadok" runat="server" Text="上傳檔案" OnClientClick="return UpLoadChk('TXTONA006_fileupload');" OnClick="TXTONA006_fileuploadok_Click" />
                        <asp:Button ID="TXTONA006_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA006_fileuploaddel_Click" />
                        <br/><asp:HyperLink ID="Link006" runat="server" Target="_blank" />
                        <br/><span style="color:red;">※ 上傳格式限定為pdf、odt或doc檔案，大小請於50mb以內</span><br/>
                        檢附水土保持保證金權利轉讓切結書（<asp:HyperLink ID="A" runat="server" Text="範本" Target="_blank" NavigateUrl="~/sysFile/水土保持保證金權利轉讓切結書(範本).doc" />）：<br/>
                        <asp:TextBox ID="TXTONA017" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTONA017_fileupload" runat="server" />
                        <asp:Button ID="TXTONA017_fileuploadok" runat="server" Text="上傳檔案" OnClientClick="return UpLoadChk('TXTONA017_fileupload');" OnClick="UpLoadFileOk_Click" />
                        <asp:Button ID="TXTONA017_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="UpLoadFileDel_Click" />
                        <br/><asp:HyperLink ID="Link017" runat="server" Target="_blank" />
                        <br/><span style="color:red;">※ 上傳格式限定為pdf、odt或doc檔案，大小請於50mb以內</span></td></tr>
                <tr><td style="line-height:40px; font-weight:normal">
                        <asp:CheckBox ID="CHKONA007" runat="server" Text ="變更承辦技師" /><br/>
                        技師名稱：
                        <asp:DropDownList ID="DDLDTL008" runat="server" Height="25px"/>
                        <span style="color:red;">※ 若於清單中找不到所屬技師，請確認技師是否已申請本平台帳號。</span><br/>
                        執業執照字號：
                        <asp:TextBox ID="TXTONA009" runat="server" autocomplete="off" style="margin-left:8px;"></asp:TextBox><br/>
                        委託契約：<br/>
                        <asp:TextBox ID="TXTONA010" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTONA010_fileupload" runat="server" />
                        <asp:Button ID="TXTONA010_fileuploadok" runat="server" Text="上傳檔案" OnClientClick="return UpLoadChk('TXTONA010_fileupload');" OnClick="TXTONA010_fileuploadok_Click" />
                        <asp:Button ID="TXTONA010_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA010_fileuploaddel_Click" />
                        <br/><asp:HyperLink ID="Link010" runat="server" Target="_blank" />
                        <br/><span style="color:red;">※ 上傳格式限定為pdf、odt或doc檔案，大小請於50mb以內</span></td></tr>
                <tr><td style="line-height:40px; font-weight:normal">
                        <asp:CheckBox ID="CHKONA011" runat="server" Text ="變更監造技師" /><br/>
                        技師名稱：
                        <asp:DropDownList ID="DDLDTL012" runat="server" Height="25px"/>
                        <span style="color:red;">※ 若於清單中找不到所屬技師，請確認技師是否已申請本平台帳號。</span><br/>
                        執業執照字號：
                        <asp:TextBox ID="TXTONA013" runat="server" autocomplete="off" style="margin-left:8px;"></asp:TextBox><br/>
                        委託契約：<br/>
                        <asp:TextBox ID="TXTONA014" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTONA014_fileupload" runat="server" />
                        <asp:Button ID="TXTONA014_fileuploadok" runat="server" Text="上傳檔案" OnClientClick="return UpLoadChk('TXTONA014_fileupload');" OnClick="TXTONA014_fileuploadok_Click" />
                        <asp:Button ID="TXTONA014_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA014_fileuploaddel_Click" />
                        <br/><asp:HyperLink ID="Link014" runat="server" Target="_blank" />
                        <br/><span style="color:red;">※ 上傳格式限定為pdf、odt或doc檔案，大小請於50mb以內</span></td></tr> </table>
                
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
        <script src="../js/NorJs.js"></script>
    </div>
    </form>
</body>
</html>
