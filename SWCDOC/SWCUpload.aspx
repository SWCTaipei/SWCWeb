<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCUpload.aspx.cs" Inherits="SWCDOC_SWCUpload" %>
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
    <link rel="stylesheet" type="text/css" href="../css/reset.css" />
    <link rel="stylesheet" type="text/css" href="../css/all.css?202108020225" />
</head>
<body>
    <form id="form1" runat="server">
    <div>

    <div class="wrap-s">
        <div class="header-wrap-s">
            <div class="header header-s clearfix"><a href="HaloPage001.aspx" class="logo-s"></a>
                <div class="header-menu-s">
                    <ul>
                        <li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
                        <li>|</li>
                        <li><a href="http://www.swc.taipei/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="https://tslm.swc.taipei/tslmwork/NF000/PG000.aspx" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
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
			 <div class="checkhref">
                <h1>上傳修正/檢視本<br/><br/></h1>
                <asp:Label ID="LBNO" runat="server" Visible="false"/>
                <asp:Label ID="LBMAX" runat="server" Visible="false"/>
                <asp:Label ID="LBCASEID" runat="server" Visible="false"/>
				<asp:Panel ID="pdata" runat="server">
				</asp:Panel>
			</div>
				<br/><br/>
                <span class="R_title RT3">上傳修正本</span>
                <span class="br"></span>
                                    
                 <table class="grownTB">
                    <tr><td style="width:200px; text-align: center;">修正本</td>
                        <td><asp:FileUpload ID="SFFileXX_FileUpload" runat="server" Style="height: 25px;" />
							<asp:Button ID="Btn_UPSFileXX" runat="server" Text="上傳檔案" Style="height: 25px;" OnClick="Btn_UPSFile_Click" />
							<asp:Button ID="Btn_DelSFileXX" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" Style="height: 25px;" OnClick="Btn_DelSFile_Click" />
                            <asp:HyperLink ID="SFLINKXX" runat="server" Target="_blank" Visible="false"/>
							<asp:TextBox ID="TXTSFileXX" runat="server" Width="70px" Visible="False" />
							<span class="red" style="font-weight:bold; display:block; margin:10px 0;">※ 上傳格式限定為PDF，檔案大小請於150mb以內</span>
						</td></tr>
                    <tr><td style="width:200px; text-align: center;">第一次上傳日期</td>
                        <td><asp:Label ID="LBDATE1" runat="server"/></td></tr>
                    <tr><td style="width:200px; text-align: center;">最後上傳日期</td>
                        <td><asp:Label ID="LBDATE2" runat="server"/></td></tr>
                    <tr><td style="width:200px; text-align: center;">最後更新人員</td>
                        <td><asp:Label ID="LBPEOPLE" runat="server"/></td></tr>
                </table>

                <br/><br/>
				
                <span class="R_title RT2">上傳檢視本</span>
                <span class="br"></span>
				<table class="">
                    <tr><td style="width:200px; text-align: center;">檢視本上傳</td>
                        <td><asp:FileUpload ID="SFFile99_FileUpload" runat="server" Style="height: 25px;" />
							<asp:Button ID="Btn_UPSFile99" runat="server" Text="上傳檔案" Style="height: 25px;" OnClick="Btn_UPSFile_Click" />
							<asp:Button ID="Btn_DelSFile99" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" Style="height: 25px;" OnClick="Btn_DelSFile_Click" />
                            <asp:HyperLink ID="SFLINK99" runat="server" Target="_blank" Visible="false"/>
							<asp:TextBox ID="TXTSFile99" runat="server" Width="70px" Visible="False" />
							<span class="red" style="font-weight:bold; display:block; margin:10px 0 0;">※ 上傳格式限定為PDF，檔案大小請於150mb以內</span>
							<span style="font-size: 16px; color: #0D7E0F; display: block; font-weight: bold !important;">※ 本欄位於審查單位確認無審查意見後，由承辦技師上傳。</span>
						</td></tr>
                </table>
				
				<asp:Label ID="UP1" runat="server" Text="" Visible="false"/>
				<asp:Label ID="UP2" runat="server" Text="" Visible="false"/>
				
                <div class="form-btn">
					<asp:Button ID="Save" runat="server" Text="儲存" OnClick="Save_Click" />
                    <asp:Button ID="GoHomePage" runat="server" Text="返回案件" OnClick="GoHomePage_Click" />
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

        <script src="../js/jquery-3.1.1.min.js"></script>
        <script src="../js/inner.js"></script>
        <script src="../js/allhref.js"></script>
    </div>
    </form>
</body>
</html>
