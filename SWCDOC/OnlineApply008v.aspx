﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineApply008v.aspx.cs" Inherits="SWCDOC_OnlineApply008" %>
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
            if (jChkType == 'DataLock') {
                var r = confirm('確認送出後，即不可修改，請再次確認是否要完成送出。');
                return r;
            }
        }
        function DateDifference(StartDate, EndDate) {
            var myStartDate = new Date(StartDate);
            var myEndDate = new Date(EndDate);

            // 天數，86400000是24*60*60*1000，除以86400000就是有幾天
            return (myEndDate - myStartDate) / 86400000;
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
                    <span><asp:Literal ID="TextUserName" runat="server">多多綠奶茶，您好</asp:Literal></span>
                </div>
            </div>
        </div>
    




        
  <div class="content-s">
    <div class="review form">
      <h1>水土保持計畫停工申請</h1><br />

                <div class="detailsMenu-btn">
                    <asp:ImageButton ID="OutPdf" runat="server" title="輸出PDF" ImageUrl="../images/btn/icon_exportpdf.png" OnClick="OutPdf_Click" Visible="false" />
                </div>
          
                <table class="review-out">
                <tr><td>停工申請編號</td>
                    <td><asp:Label ID="TXTONA001" runat="server" Visible="true"/></td></tr>
                <tr><td>水保局編號</td>
                    <td><asp:Label ID="LBSWC002" runat="server"/>
                        <asp:Label ID="LBSWC000" runat="server" Visible="false"/></td></tr>
                <tr><td>計畫名稱</td>
                    <td><asp:Label ID="LBSWC005" runat="server"/></td></tr>
                <tr><td>開工日期</td>
                    <td><asp:Label ID="TXTONA002" runat="server" width="120px" autocomplete="off"></asp:Label></td></tr>
                <tr><td>預計停工期限</td>
                    <td><asp:Label ID="TXTONA003" runat="server" autocomplete="off"></asp:Label>
                        ~
                        <asp:Label ID="TXTONA004" runat="server" width="120px" autocomplete="off"></asp:Label></td></tr>
                <tr><td>停工理由</td>
                    <td><asp:Label ID="LBONA005" runat="server" Text="辦理目的事業開發許可變更設計<br />" Visible="false"></asp:Label>
                        <asp:Label ID="LBONA006" runat="server" Text="辦理水土保持計畫變更設計<br />" Visible="false"></asp:Label>
                        <asp:Label ID="LBONA007" runat="server" Text="個人或外在因素須停止施作<br />" Visible="false"></asp:Label>
                        <asp:Label ID="LBONA008" runat="server" Text="其他" Visible="false"></asp:Label>
                        <asp:Label ID="TXTONA009" runat="server" /></td></tr>
               <tr><td>是否安全無虞</td>
                   <td><asp:Label ID="TXTONA010" runat="server"/></td></tr>

                <tr><td style="border:none"></td>
                    <td style="border:none"></td></tr>
                    

                <asp:Panel ID="ReviewResults" runat="server" Visible="false">

                <tr><td class="bgcolor">審查結果</td>
                    <td class="bgcolor2" style="line-height:40px;">
                        

                        <asp:Panel ID="Panel1" runat="server">
						    <asp:radiobutton ID="CHKRRc" runat="server" Text="退補正：" value="2" GroupName="CHKRR" />
                            <asp:TextBox ID="ReviewDirections" runat="server" MaxLength="200" style="padding-left:5px; width:580px; height:23px;" /><asp:Label ID="LBV01" runat="server" Text="，改正期限" />
                            <asp:TextBox ID="TXTDeadline" runat="server" width="120px" MaxLength="20" autocomplete="off"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTDeadline_CalendarExtender" runat="server" TargetControlID="TXTDeadline" Format="yyyy-MM-dd"></asp:CalendarExtender>

                        </asp:Panel>
						<asp:radiobutton ID="CHKRRa" runat="server" Text="准" value="1" GroupName="CHKRR" />
                        <asp:radiobutton ID="CHKRRb" runat="server" Text="駁：" value="0" GroupName="CHKRR" />
                        <asp:TextBox ID="CHK" runat="server" Width="70px" Visible="False" />
                        <asp:Label ID="LBRR" runat="server" Visible="false" />
                        <asp:TextBox ID="ResultsExplain" runat="server" Width="580px" />
                        <asp:Label ID="LBResultsExplain" runat="server" Width="300px" />
                        <asp:Panel ID="ReViewUL" runat="server"><br/>
                        上傳公文：
                        <asp:FileUpload ID="TXTReviewDoc_fileupload" runat="server" CssClass="wordtt" />
                        <asp:Button ID="TXTReviewDoc_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTReviewDoc_fileuploadok_Click" />
                        <asp:TextBox ID="TXTReviewDoc" runat="server" CssClass="word" Width="20px" Visible="False"/>
                        <asp:Button ID="TXTReviewDoc_fileclean" runat="server" CssClass="wordttb" Text="X" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTReviewDoc_fileclean_Click" />
                        <br/><span style="color:red;">※ 上傳格式限定為pdf、odt或doc檔案，大小請於50mb以內</span><br/></asp:Panel>
                        <asp:HyperLink ID="LinkReviewDoc" runat="server" CssClass="word" Target="_blank"/><br/>
                        <asp:Label ID="ReviewID" runat="server" Visible="false"/></td></tr>
                </asp:Panel>

      </table>
      
      
                <asp:Label ID="ReqCount" runat="server" Text="" style="display:none;" />
                <asp:Panel ID="SignList" runat="server"><br/><br/>

                <div><span style="background-color: #FFFF99; font-size: 16pt; font-weight: bold; margin-top:1em;">退補正歷程</span></div><br/>
            
                <asp:GridView ID="GVSignList" runat="server" DataSourceID="SqlDataSourceSign" CssClass="retirement" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="TH001n" HeaderText="退文日期" SortExpression="SWC000" ItemStyle-Width="200px"/>
                        <asp:BoundField DataField="TH005n" HeaderText="改正期限" SortExpression="SWC002" ItemStyle-Width="190px"/>
                        <asp:BoundField DataField="THName" HeaderText="退文人員" SortExpression="SWC004" ItemStyle-Width="140px"  Visible="false"/>
                        <asp:BoundField DataField="TH004" HeaderText="說明" SortExpression="SWC005"  ItemStyle-Width="350px"/>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceSign" runat="server" ConnectionString="<%$ ConnectionStrings:SWCConnStr %>" SelectCommand="" OnSelected="SqlDataSourceSign_Selected" />

                </asp:Panel>
      
                    <div class="form-btn">
                        <asp:Button ID="DataLock" runat="server" Text="確認送出" OnClientClick="return chkInput('DataLock');" OnClick="DataLock_Click" Visible="false" />&nbsp&nbsp
                        <asp:Button ID="SaveCase" runat="server" Text="暫時儲存" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" Visible="false" />&nbsp&nbsp
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
    </div>
    </form>
</body>
</html>
