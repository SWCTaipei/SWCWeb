<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWC001.aspx.cs" Inherits="SWCDOC_SWC001" %>
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
    <link rel="stylesheet" type="text/css" href="../css/all.css?202208020212"/>
     <link rel="stylesheet" type="text/css" href="../css/iris.css?202103230553"/>
    <script type="text/javascript">
        function SearchChg(e1)
        {
            if (e1.checked) { SwcArea02.style.display = 'none'; List01.style.display = 'none'; alert('提醒您，如欲申請程序變更，請輸入「案件編號」、「案件名稱」或是「地籍」至少一種條件，並請完整輸入並點選查詢按鈕，選擇欲申請之案件後進入填寫表單，謝謝您。'); } else { SwcArea02.style.display = ''; List01.style.display = ''; }
        }
        function InputChk()
        {
            if(document.getElementById('CHKSHTYPE').checked == true )
            {
                var errmsg = "";
                var errcount = 0;

                var jValue01 = document.getElementById('TXTS001').value;
                var jValue02 = document.getElementById('TXTS003').value;
                var jValue03 = $("#DDLDistrict").val();
                var jValue04 = $("#DDLSection").val();
                var jValue05 = $("#DDLSection2").val();
                var jValue06 = document.getElementById('TXTNumber').value;

                if (jValue01 != "")
                {
                    errcount++;
                }
                if (jValue02 != "")
                {
                    errcount++;
                }
                if (jValue03 != "0" || jValue06 != "")
                {
                    if (jValue03 == "0")
                    {
                        alert('請輸入申請案之地籍(區)資料。');
                        return false;
                    }
                    if (jValue04 == "")
                    {
                        alert('請輸入申請案之地籍(段)資料。');
                        return false;
                    }
                    if (jValue05 == "") {
                        alert('請輸入申請案之地籍(小段)資料。');
                        return false;
                    }
                    if (jValue06 == "") {
                        alert('請輸入申請案之地籍(地號)資料。');
                        return false;
                    }
                    errcount++;
                }
                if (errcount == 0)
                {
                    alert('請完整輸入申請案之編號、名稱或地籍資料。');
                    return false;
                }
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


             <div>
                <div class="header-menu-s">
                <ul>
                    <li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
                    <li>|</li>
                    <li><a href="https://swc.taipei/swcinfo/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                    <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp<a href="https://tslm.swc.taipei/tslmwork/" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
                    <asp:Panel ID="TitleLink00" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp<a href="SWCBase001.aspx" title="帳號管理">帳號管理</a></li></asp:Panel>
                    <asp:Panel ID="TitleLink01" runat="server" Visible="false"><li class="none">|&nbsp&nbsp<a href="BasicInfo/BasPg001.aspx" title="權限管理">權限管理</a></li></asp:Panel>
                    <asp:Panel ID="UserBoard00" runat="server" Visible="false"><li class="none">|&nbsp&nbsp<a href="UserBoard.aspx" title="留言板">留言板</a></li></asp:Panel>
                    <asp:Panel ID="GOVMG" runat="server" Visible="false">
						<li class="flip">|&nbsp&nbsp&nbsp<a href="#" title="系統管理">系統管理+</a>
							<ul class="openlist" style="display: none;">
								<%--<li><a href="http://tgeo.swc.taipei/">審查/檢查行事曆</a></li>--%>
								<li><a href="../SWCTCGOV/SWCGOV001.aspx">防災事件通知</a></li>
								<li><a href="../SWCTCGOV/SWCGOV011.aspx">公佈欄</a></li>
								<li><a href="../SWCDOC/UserBoardList.aspx">留言版</a></li>
								<li><a href="http://tgeo.swc.taipei/">T-GEO空間地理資訊平台</a></li>
							</ul>
						</li>
					</asp:Panel>
                    <li>|</li>
                    <li><a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li>
                </ul>
            </div>
        </div></div>

            <div class="header-s-green">
                <div class="header-s-green-nameWrap">
                    <span><asp:Literal ID="TextUserName" runat="server">大雷包，您好</asp:Literal></span>
            <asp:Panel ID="ChgUserType" runat="server" Visible="false">
                    <span style="margin-right:0.8em;">身分別：
                        <asp:DropDownList ID="DDLChange" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DDLChange_SelectedIndexChanged">
                            <asp:ListItem Value="技師">技師</asp:ListItem>
                            <asp:ListItem Value="公會">公會</asp:ListItem>
                        </asp:DropDownList>
                    </span>
            </asp:Panel>
                </div>
            </div>

      </div>  

            <div class="contentFooter">
                <div class="content-s content-s-inquire">
                    <div class="inquireForm date">
                        <span>水保局編號：</span>
                        <asp:TextBox ID="TXTS001" runat="server" MaxLength="20"/>
                        <span>書件類別：</span>
                        <asp:CheckBox ID="CHKS002a" runat="server" Text="水土保持計畫" />
                        <asp:CheckBox ID="CHKS002b" runat="server" Text="簡易水保" />
                        <asp:CheckBox ID="CHKSHTYPE" runat="server" Text="案件變更申請" onclick="SearchChg(this);" Visible="false" Style="margin-left:26%; font-weight:bolder;color:#0000ff;"/>
                        <asp:Button ID="BTNSHTYPE" runat="server" Text="案件變更申請" OnClick="BTNSHTYPE_Click" Visible="false" Style="margin-left:26%; font-weight:bolder;color:#0000ff;"/>
						<br/>
                        <span>書件名稱：</span>
                        <asp:TextBox ID="TXTS003" runat="server" Width="500" />
                        <span>地籍：</span>
                        <asp:DropDownList ID="DDLDistrict" runat="server" Height="25px" AutoPostBack="true" OnSelectedIndexChanged="DDLDistrict_SelectedIndexChanged"/>區
                        <asp:DropDownList ID="DDLSection" runat="server" Height="25px" AutoPostBack="true" OnSelectedIndexChanged="DDLSection_SelectedIndexChanged"/>段
                        <asp:DropDownList ID="DDLSection2" runat="server" Height="25px" AutoPostBack="true" />小段
                        <asp:TextBox ID="TXTNumber" runat="server" Height="25px" MaxLength="8"/>地號
                        <br/>
                        <asp:Panel ID="SwcArea02" runat="server">
                        <span>水保義務人：</span>
                        <asp:TextBox ID="TXTS004" runat="server" MaxLength="10" />
                        <span>承辦技師：</span>
                        <asp:TextBox ID="TXTQQ01" runat="server" MaxLength="10" />
                        <span>審查單位：</span>
                        <asp:DropDownList ID="DDLQQ01" runat="server" Height="25px" /><br/>
                        <span>狀態：</span>
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="申請中" />&nbsp;&nbsp;
                        <asp:CheckBox ID="CHKS003m" runat="server" Text="退補件" />&nbsp;&nbsp;
                        <asp:CheckBox ID="CHKS003a" runat="server" Text="受理中" />&nbsp;&nbsp;
                        <asp:CheckBox ID="CHKS003b" runat="server" Text="審查中" />&nbsp;&nbsp;
                        <asp:CheckBox ID="CHKS003n" runat="server" Text="暫停審查" />&nbsp;&nbsp;
                        <asp:CheckBox ID="CHKS003c" runat="server" Text="已核定" />&nbsp;&nbsp;
                        <asp:CheckBox ID="CHKS003d" runat="server" Text="施工中" />&nbsp;&nbsp;
                        <asp:CheckBox ID="CHKS003e" runat="server" Text="停工中" />&nbsp;&nbsp;
                        <asp:CheckBox ID="CHKS003f" runat="server" Text="已完工" />&nbsp;&nbsp;
                        <asp:CheckBox ID="CHKS003g" runat="server" Text="廢止" />&nbsp;&nbsp;
                        <asp:CheckBox ID="CHKS003h" runat="server" Text="撤銷" />&nbsp;&nbsp;
                        <asp:CheckBox ID="CHKS003i" runat="server" Text="失效" />&nbsp;&nbsp;
                        <asp:CheckBox ID="CHKS003j" runat="server" Text="不予受理" />&nbsp;&nbsp;<br />
                        <asp:CheckBox ID="CHKS003k" runat="server" Text="不予核定" />&nbsp;&nbsp;
                        <asp:CheckBox ID="CHKS003l" runat="server" Text="已變更" />&nbsp;&nbsp;
                        <br/>
                        核定日期：
                        <asp:TextBox ID="TXTS005" runat="server" width="120px" Style="margin-right: 0px;"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTS005_CalendarExtender" runat="server" TargetControlID="TXTS005" Format="yyyy-MM-dd"></asp:CalendarExtender>
                        ~
                        <asp:TextBox ID="TXTS006" runat="server" width="120px"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTS006_CalendarExtender" runat="server" TargetControlID="TXTS006" Format="yyyy-MM-dd"></asp:CalendarExtender>

                        完工日期：
                        <asp:TextBox ID="TXTS007" runat="server" width="120px" Style="margin-right: 0px;"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTS007_CalendarExtender" runat="server" TargetControlID="TXTS007" Format="yyyy-MM-dd"></asp:CalendarExtender>
                        ~
                        <asp:TextBox ID="TXTS008" runat="server" width="120px"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTS008_CalendarExtender" runat="server" TargetControlID="TXTS008" Format="yyyy-MM-dd"></asp:CalendarExtender>
                            </asp:Panel>
                        <div class="inquireForm-btn">
                            <asp:Button ID="ExeQSel" runat="server" Text="查詢" OnClick="ExeQSel_Click" OnClientClick="return InputChk();"/>
                            <asp:Button ID="RemoveSel" runat="server" Text="清除" OnClick="RemoveSel_Click"/>
                        </div>
                    </div>



            <asp:Panel ID="List01" runat="server">

            <div class="inquireGrid">
              <div class="inquireGrid-menu">
                <h3>查詢到件數：<asp:Label ID="CaseCount" runat="server" Text="" />筆</h3>
                  <asp:ImageButton ID="NewSwc" runat="server" OnClick="NewSwc_Click" title="新增案件" ImageUrl="../images/btn/btn-addCase.png" />
                  <asp:ImageButton ID="WriteExcel" runat="server" OnClick="WriteExcel_Click" title="輸出Excel" ImageUrl="../images/btn/btn-excel.png" OnClientClick="return confirm('確認是否要輸出Excel？')"  />
                  <asp:ImageButton ID="WriteOds" runat="server" OnClick="WriteOds_Click" title="輸出ods" ImageUrl="../images/btn/btn-ods.png" />
				  <div class="lookCD" runat="server" id="ToCalendar"><a href="../SWCRD/FPage003.aspx">查看行事曆</a></div>
                  <asp:HyperLink ID="OdsPointDtl3Date" runat="server" NavigateUrl="~/SwcReport/OpenDocXls001.aspx" ImageUrl="~/images/btn/btn-rep.png" ToolTip="匯出施工檢查檢核半年報表" Visible="false" />
              </div>

    
                <asp:GridView ID="GVSWCList" runat="server" DataSourceID="SqlDataSource" CssClass="ADDGridView AutoNewLine" PagerStyle-CssClass="pgr"
                    OnRowCommand="GVSWCList_RowCommand" OnRowDataBound="GVSWCList_RowDataBound" OnPageIndexChanging="GVSWCList_PageIndexChanging" OnSorting="GVSWCList_Sorting"
                    EmptyDataText="沒有符合查詢條件的資料" AllowPaging="true" PageSize="20" AllowSorting="True"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="" HeaderText="" SortExpression=""></asp:BoundField>
                        <asp:BoundField DataField="SWC000" HeaderText="案件編號" SortExpression="SWC000" ItemStyle-Width="200px"/>
                        <asp:BoundField DataField="SWC002" HeaderText="水保局編號" SortExpression="SWC002" ItemStyle-Width="170px"/>
                        <asp:BoundField DataField="SWC004" HeaderText="案件狀態" SortExpression="SWC004" ItemStyle-Width="130px"/>
                        <asp:BoundField DataField="SWC005" HeaderText="水土保持申請書件名稱" SortExpression="SWC005"  ItemStyle-Width="350px"/>
                        <asp:BoundField DataField="SWC088" HeaderText="審查期限" SortExpression="SWC088" ItemStyle-Width="120px"/>
                        <asp:BoundField DataField="SWC013" HeaderText="義務人" SortExpression="SWC013"  ItemStyle-Width="120px"/>    
                        <asp:ButtonField ButtonType="Button" CommandName="detail" Text="詳情"/>
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:Button ID="BtnEdit" runat="server" CommandArgument='<%# Eval("SWC000") %>' OnClick="BtnEdit_Click" Text="編輯" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                    </Columns>
                    <PagerSettings LastPageText="最後一頁" FirstPageText="第一頁" Mode="NumericFirstLast" NextPageText="下一頁" PreviousPageText="上一頁"  />
                    <PagerStyle  CssClass="pgr"  BackColor="#7baac5" Width="100%"/>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SWCConnStr %>" OnSelected="SqlDataSource_Selected"
                    SelectCommand="SELECT * FROM [SWCCASE] ORDER BY [SWC000] DESC"
                    DeleteCommand="DELETE FROM [SWCCASE] WHERE [SWC000] = @SWC000">
                    <DeleteParameters>
                        <asp:Parameter Name="SWC000" Type="String" />
                    </DeleteParameters>
                </asp:SqlDataSource>
</div>

                    
            </asp:Panel>

                <div> 
            <asp:Panel ID="List02" runat="server">

               <div> <h3 class="wrap">查詢到件數:<asp:Label ID="ReqCount" runat="server" Text="" />筆</h3> </div>
             <br>
                <asp:GridView ID="GVReqList" runat="server" DataSourceID="SqlDataSourceReq" CssClass="changerequest AutoNewLine" PagerStyle-CssClass="pgr"
                    OnRowCommand="GVReqList_RowCommand" OnPageIndexChanging="GVSWCList_PageIndexChanging" OnSorting="GVSWCList_Sorting"
                    EmptyDataText="沒有符合查詢條件的資料" AllowPaging="true" PageSize="20" AllowSorting="True"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="" HeaderText="" SortExpression=""></asp:BoundField>
                        <asp:BoundField DataField="SWC000" HeaderText="案件編號" SortExpression="SWC000" ItemStyle-Width="200px"/>
                        <asp:BoundField DataField="SWC002" HeaderText="水保局編號" SortExpression="SWC002" ItemStyle-Width="190px"/>
                        <asp:BoundField DataField="SWC004" HeaderText="案件狀態" SortExpression="SWC004" ItemStyle-Width="140px"/>
                        <asp:BoundField DataField="SWC005" HeaderText="水土保持申請書件名稱" SortExpression="SWC005"  ItemStyle-Width="350px"/>
                        <asp:BoundField DataField="SWC013" HeaderText="義務人" SortExpression="SWC013"  ItemStyle-Width="180px"/>    
                        <asp:ButtonField ButtonType="Button" CommandName="detail" Text="申請"/>
                    </Columns>
                    <PagerSettings LastPageText="最後一頁" FirstPageText="第一頁" Mode="NumericFirstLast" NextPageText="下一頁" PreviousPageText="上一頁"  />
                    <PagerStyle  CssClass="pgr"  BackColor="#7baac5" Width="100%"/>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceReq" runat="server" ConnectionString="<%$ ConnectionStrings:SWCConnStr %>" OnSelected="SqlDataSourceReq_Selected"
                    SelectCommand="SELECT * FROM [SWCCASE] ORDER BY [SWC000] DESC"
                    DeleteCommand="DELETE FROM [SWCCASE] WHERE [SWC000] = @SWC000">
                    <DeleteParameters>
                        <asp:Parameter Name="SWC000" Type="String" />
                    </DeleteParameters>
                </asp:SqlDataSource>
             
            </asp:Panel>


   
       </div>
   </div>

          </div>
                
          <%--  <div class="footer-s">
                <div class="footer-s-green"></div>
                    <div class="footer-b-brown">
                        <p> <span class="span1">臺北市政府工務局大地工程處</span>
                        <br/><span class="span2">110臺北市信義區松德路300號3樓 　(02)27593001   臺北市民當家熱線1999</span>
                               <span class="span2">資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span></p>
                    </div>
            </div>--%>

            <div class="footer">
                 <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                    <span class="span2">110臺北市信義區松德路300號3樓 　服務專線(02)27591109   臺北市民當家熱線1999</span><br/>
                    <span class="span2">建議使用IE11(含)以上，Chrome或Firefox版本瀏覽器　<b>資料更新：</b><asp:Label ID="ToDay" runat="server" Text=""/>　<b>來訪人數：</b><asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                    <span class="span2"><b>客服電話：</b>02-27929328 陳小姐　<b>信箱：</b>tcge7@geovector.com.tw　本系統由多維空間資訊有限公司開發維護 TEL：(02)27929328</span><br/>
			        <span class="span2">※為維護系統服務品質，本平台訂於每周三凌晨AM 4:00-6:30 進行系統維護更新，更新期間偶有瞬斷情形，敬請使用者避開該時段使用。謝謝！</span></p>
            </div>




                <%--<div>
                    <script>
                      (function() {
                        var cx = '016617542573623536061:rhbyy1wlyl8';
                        var gcse = document.createElement('script');
                        gcse.type = 'text/javascript';
                        gcse.async = true;
                        gcse.src = 'https://cse.google.com/cse.js?cx=' + cx;
                        var s = document.getElementsByTagName('script')[0];
                        s.parentNode.insertBefore(gcse, s);
                      })();
                    </script>
                    <gcse:search></gcse:search>
            </div>--%>
        </div>

        </div>
    </div>
        
        <div></div>
        <asp:Literal ID="error_msg" runat="server"></asp:Literal>

        <script src="../js/jquery-3.1.1.min.js"></script>
        <script src="../js/inner.js"></script>
        <script src="../js/allhref.js"></script>
		
		<script>
			$(document).keypress(function(e)
			{
				if(e.keyCode === 13)
				{
					document.getElementById("ExeQSel").click();
					return false;
				}
			});
		</script>
        
        <script type="text/javascript">
            if (document.getElementById('CHKSHTYPE').checked) { SwcArea02.style.display = 'none'; } else { SwcArea02.style.display = ''; }
        </script>
    </form>
</body>
</html>
