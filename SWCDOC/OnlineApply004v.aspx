<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineApply004v.aspx.cs" Inherits="SWCDOC_OnlineApply004" %>
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
	<script type="text/javascript">
        function chkInput(jChkType) {

            var iValue03 = document.getElementById("TXTONA003").innerText;
            var iValue16 = $("#TXTONA016").val();
            var iValue17 = $("#TXTONA017").val();

            if (addMonthsUTC(iValue16, 0) > addMonthsUTC(iValue17, 0)) {
                alert('「核准展延完工期限」不得超過「目的事業核定展延完工期限」');
                return false;
            }
            if (addMonthsUTC(iValue16, 0) > addMonthsUTC(iValue03, 12)) {
                alert('「核准展延完工期限」不得超過「預定開工日期」1年');
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
                        <li><a href="https://swc.taipei/swcinfo/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="http://tslm.swc.taipei/tslmwork/" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
                        <asp:Panel ID="TitleLink00" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWCBase001.aspx" title="帳號管理">帳號管理</a></li></asp:Panel>
                        <asp:Panel ID="LogOutLink" runat="server" Visible="true"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li></asp:Panel>
                    </ul>
                </div>
            </div>

            <div class="header-s-green">
                <div class="header-s-green-nameWrap">
                    <span><asp:Literal ID="TextUserName" runat="server">孜然養樂多，您好</asp:Literal></span>
                </div>
            </div>
        </div>
    
        
  <div class="content-s">
    <div class="startReport facilityMaintain form">
      <h1>水土保持計畫<asp:Label ID="LBSWC004_1" runat="server" Text="開工/復工"/>申報書</h1>
<br/>
      <div class="detailsMenu-btn">
             <asp:ImageButton ID="OutPdf" runat="server" title="輸出施工許可證PDF" ImageUrl="../images/btn/btn-pdf.png" OnClick="OutPdf_Click1" Visible="false"  />
      </div>

            <table class="startReport-base ">
            <tr><td colspan="2" style="text-align: left; font-weight:bold; width: 40%;"><asp:Label ID="LBSWC004_2" runat="server" Text="開工/復工"/>申報書編號</td>
                <td colspan="4">
                    <asp:Label ID="TXTONA001" runat="server" Visible="true"/></td></tr>
            <tr><td colspan="2" style="text-align: left; font-weight:bold; width:20%;">水保局編號</td>
                <td colspan="4">
                    <asp:Label ID="LBSWC002" runat="server"/>
                    <asp:Label ID="LBSWC000" runat="server" Visible="false"/>
                    <asp:Label ID="LBSWC004" runat="server" Visible="false" /></td></tr>
                <asp:Panel ID="Panel2" runat="server" Visible="false">
            <tr><td colspan="2" style="text-align: left; font-weight:bold;">申報日期</td>
                <td colspan="4">
                    <asp:Label ID="TXTONA002" runat="server"/></td></tr>
                 </asp:Panel>
            <tr><td rowspan="3" style="font-weight:bold;">水土保持義務人</td>
                <td style=width:20%;>姓名或名稱</td>
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
            <tr><td colspan="2" style="font-weight:bold;">預定<asp:Label ID="LBSWC004_3" runat="server" Text="開工/復工"/>日期</td>
                <td style="width:30%">
                    <asp:Label ID="TXTONA003" runat="server" width="120px" autocomplete="off"></asp:Label></td>
                <td style="width:20%;font-weight:bold;">預定完工日期</td>
                <td style="width:40%">
                    <asp:Label ID="TXTONA004" runat="server" width="120px" autocomplete="off"></asp:Label></td></tr>
            <tr><td colspan="2" style="font-weight:bold;">目的事業主管機關核定(展延)完工期限及證明文件</td>
                <td style="width:30%" colspan="3">
                    <asp:Label ID="TXTONA012" runat="server" width="120px" autocomplete="off"></asp:Label>
                    <br/>
                    <asp:TextBox ID="TXTONA013" runat="server" Width="70px" Visible="False" />
                    <asp:HyperLink ID="Link013" runat="server" Target="_blank" /></td></tr>
            <tr><td rowspan="3" style="font-weight:bold;">監造技師</td>
                <td>姓名</td>
                <td class="bgcolorb">
                    <asp:Label ID="LBSWC045Name" runat="server"/>
                    <asp:HiddenField ID="TXTONA014" runat="server" /></td>
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
            <tr><td colspan="1" rowspan="7" style="font-weight:bold;">檢附文件</td>
                
				<td>1.水土保持保證金繳納證明</td>
                <td colspan="3"><asp:Label ID="LBSWC041" runat="server"/></td>
                <td colspan="3" style="display:none;">
                    <asp:TextBox ID="TXTONA005" runat="server" Width="70px" Visible="False" />
                    <asp:HyperLink ID="Link005" runat="server" Target="_blank" />
                </td></tr>
            <tr>
			
                <td>2.監造契約影本</td>
                <td colspan="3">
                    <asp:TextBox ID="TXTONA006" runat="server" Width="70px" Visible="False" />
                    <asp:HyperLink ID="Link006" runat="server" Target="_blank" /></td></tr>
            <tr>
                <td>3.開發範圍界樁照片及位置標示於圖面</td>
                <td colspan="3">
                    <asp:TextBox ID="TXTONA007" runat="server" Width="70px" Visible="False" />
                    <asp:HyperLink ID="Link007" runat="server" /></td></tr>
            <tr>
                <td>4.開挖整地範圍界樁照片及位置標示於圖面（非保護區免設置），切結書</td>
                <td colspan="3">
                    <asp:TextBox ID="TXTONA008" runat="server" Width="70px" Visible="False" />
                    <asp:HyperLink ID="Link008" runat="server" Target="_blank" /></td></tr>
            <tr>
                <td>5.施工標示牌</td>
                <td colspan="3">
                    <asp:TextBox ID="TXTONA009" runat="server" Width="70px" Visible="False" />
                    <asp:HyperLink ID="Link009" runat="server" Target="_blank" /></td></tr>
            <tr>
                <td>6.災害搶救小組名冊（敘明工地負責人及相關人員行動電話）</td>
                <td colspan="3">
                    <asp:TextBox ID="TXTONA010" runat="server" Width="70px" Visible="False" />
                    <asp:HyperLink ID="Link010" runat="server" Target="_blank" /></td></tr>
			<tr>
                <td>7.廠商聯絡資料</td>
                <td colspan="3">
                    <table class="companydata">
                        <tr>
                            <th>營造單位：</th>
                            <td><asp:Label ID="LBONA018" runat="server" /></td>
                        </tr>
                        <tr>
                            <th>統一編號：</th>
                            <td><asp:Label ID="LBONA019" runat="server" /></td>
                        </tr>
                        <tr>
                            <th>工地負責人員：</th>
                            <td><asp:Label ID="LBONA020" runat="server" /></td>
                        </tr>
                        <tr>
                            <th>工地負責人員手機：</th>
                            <td><asp:Label ID="LBONA021" runat="server" /></td>
                        </tr>
                    </table>
                    
                </td>
            </tr>
			<tr>
			   <td colspan="6"><asp:CheckBox ID="CHKONA015" runat="server" Text ="「已詳閱水土保持計畫監造須知」（水土保持計畫監造須知附件）" Enabled="false" /></td>
			</tr>
					
<%--             <tr><td colspan="5" style="text-align: center;border-bottom: none;">
                上開水土保持計畫訂於
                    <asp:Label ID="TXTONA011" runat="server" width="130px" autocomplete="off"></asp:Label>
                開工，此致</td></tr>
			
				
				
				
				
           <tr><td colspan="3" style="border-top: none;border-right:none;border-bottom:none;text-align: center;vertical-align: top;">臺北市政府工務局大地工程處</td>
                <td colspan="3" style="border-top: none;border-left:none;border-bottom:none;"></td></tr>
            <tr><td colspan="3" style="border-top: none;border-right:none;text-align: center;vertical-align: top;"></td>
                <td colspan="3" style="border-top: none;border-left:none;">
                    水土保持義務人：<asp:Label ID="LBSWC013a" runat="server"/><br/>
                    　承辦監造技師：<asp:Label ID="LBSWC045a" runat="server"/></td></tr>--%>
                <tr><td style="border:none"></td>
                    <td style="border:none"></td></tr>

                <asp:Panel ID="ReviewResults" runat="server" Visible="false">

                <tr><td colspan="1" class="bgcolor">審查結果</td>
                    <td colspan="4" class="bgcolor2" style="line-height:40px;">
                        
						<asp:Panel ID="ChkDateArea" runat="server" Visible="false">
						&nbsp;&nbsp;&nbsp;<span>核准完工期限 
                            <asp:TextBox ID="TXTONA016" runat="server" width="80px" autocomplete="off"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTONA016_CalendarExtender" runat="server" TargetControlID="TXTONA016" Format="yyyy-MM-dd"></asp:CalendarExtender></span>
                        &nbsp;&nbsp;&nbsp;<span>目的事業核定完工期限
                            <asp:TextBox ID="TXTONA017" runat="server" width="80px" autocomplete="off"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTONA017_CalendarExtender" runat="server" TargetControlID="TXTONA017" Format="yyyy-MM-dd"></asp:CalendarExtender></span>
						    <br/>
						</asp:Panel>	
                        <asp:Panel ID="Panel1" runat="server">
						    <asp:radiobutton ID="CHKRRc" runat="server" Text="退補正：" value="2" GroupName="CHKRR" />
                            <asp:TextBox ID="ReviewDirections" runat="server" MaxLength="200" style="padding-left:5px; width:580px; height:23px;" /><asp:Label ID="LBV01" runat="server" Text="，改正期限" />
                            <asp:TextBox ID="TXTDeadline" runat="server" width="120px" MaxLength="20" autocomplete="off"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTDeadline_CalendarExtender" runat="server" TargetControlID="TXTDeadline" Format="yyyy-MM-dd"></asp:CalendarExtender>

                            <div style="margin-left: 6em;">開發範圍界樁請參考範例，依規定格式製作設置並將照片標示於圖說。&nbsp;<input type="button" value="新增" onclick="document.getElementById('ReviewDirections').value += '開發範圍界樁請參考範例，依規定格式製作設置並將照片標示於圖說。；';" /></div>
                            <div style="margin-top: -8px; margin-left: 6em;">請確實豎立水土保持設施之開挖整地範圍界樁並將照片標示於圖說。&nbsp;<input type="button" value="新增" onclick="document.getElementById('ReviewDirections').value += '請確實豎立水土保持設施之開挖整地範圍界樁並將照片標示於圖說。；';" /></div>
                            <div style="margin-top: -8px; margin-left: 6em;">施工告示牌資訊有誤。&nbsp;<input type="button" value="新增" onclick="document.getElementById('ReviewDirections').value += '施工告示牌資訊有誤。；';" /></div>
                        </asp:Panel>	
                        
                        <asp:radiobutton ID="CHKRRa" runat="server" Text="准" value="1" GroupName="CHKRR" />	
                        <asp:radiobutton ID="CHKRRb" runat="server" Text="駁：" value="0" GroupName="CHKRR" />
                        <asp:TextBox ID="CHK" runat="server" Width="70px" Visible="False" />
                        <asp:Label ID="LBRR" runat="server" Visible="false" />
                        <asp:TextBox ID="ResultsExplain" runat="server" Width="580px"  />
                        <asp:Label ID="LBResultsExplain" runat="server" Width="700px" /><br/>
                        <asp:Panel ID="ReViewUL" runat="server">
                        上傳公文：
                        <asp:FileUpload ID="TXTReviewDoc_fileupload" runat="server" CssClass="wordtt" />
                        <asp:Button ID="TXTReviewDoc_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTReviewDoc_fileuploadok_Click" />
                        <asp:TextBox ID="TXTReviewDoc" runat="server" CssClass="word" Width="20px" Visible="False"/>
                        <asp:Button ID="TXTReviewDoc_fileclean" runat="server" CssClass="wordttb" Text="X" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTReviewDoc_fileclean_Click" />
                        <br/><span style="color:red;">※ 上傳格式限定為pdf、odt或doc檔案，大小請於50mb以內</span><br/></asp:Panel>
                        <asp:HyperLink ID="LinkReviewDoc" runat="server" CssClass="word" Target="_blank"/><br/>
                        <asp:Label ID="LBDTLK2" runat="server" Text="存檔人：" Visible="false" /><asp:Label ID="ReviewID" runat="server" Visible="false" /></td></tr>
                </asp:Panel>
            </table>
      
                <asp:Label ID="ReqCount" runat="server" Text="0" style="display:none;" />
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
                        <asp:Button ID="DataLock" runat="server" Text="確認送出" OnClientClick="return chkInput('DataLock');" OnClick="DataLock_Click" Visible="false" />&nbsp&nbsp
                        <asp:Button ID="SaveCase" runat="server" Text="暫時儲存" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" Visible="false" />&nbsp&nbsp
                        <asp:Button ID="Button1" runat="server" Text="輸出施工許可證PDF" OnClientClick="return chkInput('');" OnClick="Button1_Click" Visible="false" />&nbsp&nbsp
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
