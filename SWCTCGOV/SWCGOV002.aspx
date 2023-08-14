<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCGOV002.aspx.cs" Inherits="SWCDOC_SWCBase001" %>
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
    <link rel="stylesheet" type="text/css" href="../css/iris.css"/>
    
    <script type="text/javascript">
        function chkInput(jChkType) {
            jCHKValue01 = document.getElementById("TXTDENAME").value;
            //jCHKValue02 = document.getElementById("TXTDEDATE").value;
            jCHKValue03 = document.getElementById("TXTDENTTEXT").value;
			jCHKValue04 = $("#DDLDETYPE option:selected").text();
			var chkBoxList = document.getElementById("CHKDISTRICT");
			var chkBoxCount = chkBoxList.getElementsByTagName("input");

            if (jCHKValue01.trim() == '') {
                alert('請輸入事件名稱');
                return false;
            }
            //if (jCHKValue02.trim() == '') {
            //    alert('請輸入發送日期');
            //    return false;
            //} else {
            //    return dateValidationCheck(jCHKValue02);
            //}

            if (jCHKValue03.trim() == '') {
                alert('請輸入通知內容');
                return false;
            }
			
			var re = false;
			for(var i=0;i<chkBoxCount.length;i++)
			{   
				if(chkBoxCount[i].checked)
				{
					re = true;
				}
			}
			if(!re){
				alert('請勾選行政區');
				return re;
			}
			
			
			if(jCHKValue04 == '颱風豪雨通知回傳自主檢查表')
			{
				var chkBoxSWCList = document.getElementById("CHKSWCSTATUS");
				var chkBoxSWCCount = chkBoxSWCList.getElementsByTagName("input");
				var chkBoxSWCList1 = document.getElementById("CHKSENDMBR");
				var chkBoxSWCCount1 = chkBoxSWCList1.getElementsByTagName("input");
				var chkBoxSWCList2 = document.getElementById("CHKSENDFUN");
				var chkBoxSWCCount2 = chkBoxSWCList2.getElementsByTagName("input");
				
				re = false;
				for(var i=0;i<chkBoxSWCCount.length;i++)
				{   
					if(chkBoxSWCCount[i].checked)
					{
						re = true;
					}
				}
				if(!re){
					alert('請勾選案件狀態');
					return re;
				}
				
				re = false;
				for(var i=0;i<chkBoxSWCCount1.length;i++)
				{   
					if(chkBoxSWCCount1[i].checked)
					{
						re = true;
					}
				}
				if(!re){
					alert('請勾選通知對象');
					return re;
				}
				
				re = false;
				for(var i=0;i<chkBoxSWCCount2.length;i++)
				{   
					if(chkBoxSWCCount2[i].checked)
					{
						re = true;
					}
				}
				if(!re){
					alert('請勾選通知管道');
					return re;
				}
			}else{
				var chkBoxILGList = document.getElementById("CHKSWCSTATUS_ILG");
				var chkBoxILGCount = chkBoxILGList.getElementsByTagName("input");
				var chkBoxILGList1 = document.getElementById("CHKSENDMBR_ILG");
				var chkBoxILGCount1 = chkBoxILGList1.getElementsByTagName("input");
				var chkBoxILGList2 = document.getElementById("CHKSENDFUN_ILG");
				var chkBoxILGCount2 = chkBoxILGList2.getElementsByTagName("input");
				
				re = false;
				for(var i=0;i<chkBoxILGCount.length;i++)
				{   
					if(chkBoxILGCount[i].checked)
					{
						re = true;
					}
				}
				if(!re){
					alert('請勾選案件狀態');
					return re;
				}
				
				re = false;
				for(var i=0;i<chkBoxILGCount1.length;i++)
				{   
					if(chkBoxILGCount1[i].checked)
					{
						re = true;
					}
				}
				if(!re){
					alert('請勾選通知對象');
					return re;
				}
				
				re = false;
				for(var i=0;i<chkBoxILGCount2.length;i++)
				{
					if(chkBoxILGCount2[i].checked)
					{
						re = true;
					}
				}
				if(!re){
					alert('請勾選通知管道');
					return re;
				}
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
		function CheckALL(){
			var chkBoxList = document.getElementById("CHKDISTRICT");
			var chkBoxCount= chkBoxList.getElementsByTagName("input");
			var temp = false;
			for(var i=0;i<chkBoxCount.length;i++)
			{   
				if(chkBoxCount[i].checked)
					temp = true;
			}
			
			for(var i=0;i<chkBoxCount.length;i++)
			{
				chkBoxCount[i].checked = !temp;
			}
			return false; 
		}
    </script>
    <script src="../js/BaseNorl.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"/>

    <div class="wrap-s">
        <div class="header-wrap-s">
            <div class="header header-s clearfix"><a href="../SWCDOC/SWC001.aspx" class="logo-s"></a>
                <div class="header-menu-s">
                    <ul>
                        <li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
                        <li>|</li>
                        <li><a href="https://swc.taipei/swcinfo" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="https://tslm.swc.taipei/tslmwork" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
                        <asp:Panel ID="TitleLink00" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="../SWCDOC/SWCBase001.aspx" title="帳號管理">帳號管理</a></li></asp:Panel>
                        <asp:Panel ID="GOVMG" runat="server" Visible="false"><li class="system">|&nbsp&nbsp&nbsp&nbsp<a href="../SWCTCGOV/SWCGOV001.aspx" title="系統管理">系統管理</a><ul><li><a href="../SWCTCGOV/SWCGOV001.aspx">防災事件通知</a></li><li><a href="../SWCTCGOV/SWCGOV011.aspx">公佈欄</a></li></ul></li></asp:Panel>
                        <li>|</li>
                        <li><a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li>
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
            <div class="detailsMenu"><asp:Image ID="Image1" runat="server" ImageUrl="../images/title/btn-87.png" /></div>
                 <div class="ph-applyGrid">
                       <h2 class="detailsBar_title_basic ">基本資料</h2>

                       <div class="detailsGrid_wrap">
                        <table class="ph-table">
                    <tr><th>流水號</th>
                        <td><asp:Label ID="TXTDENO" runat="server" /></td></tr>
                    <tr><th>通知事件<span style="color: red;font-family:cursive;">＊</span></th>
                        <td><asp:DropDownList ID="DDLDETYPE" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDLDETYPE_SelectedIndexChanged"/></td></tr>
					<tr><th>行政區<span style="color: red;font-family:cursive;">＊</span></th>
                        <td><asp:Button ID="BTN_CHKDISTRICT_ALL" runat="server" OnClientClick="return CheckALL()" Text="全選/全不選" />
                            <asp:CheckBoxList ID="CHKDISTRICT" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" /></td></tr>
                    <tr><th style="vertical-align:middle">水土保持申請案件<span style="color: red;font-family:cursive;">＊</span></th>
                        <td><span class="ittype">案件狀態</span>
							<asp:CheckBoxList ID="CHKSWCSTATUS" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" RepeatColumns="10">
								<asp:ListItem>退補件</asp:ListItem>
								<asp:ListItem>不予受理</asp:ListItem>
								<asp:ListItem>受理中</asp:ListItem>
								<asp:ListItem>審查中</asp:ListItem>
								<asp:ListItem>暫停審查</asp:ListItem>
								<asp:ListItem>撤銷</asp:ListItem>
								<asp:ListItem>不予核定</asp:ListItem>
								<asp:ListItem>已核定</asp:ListItem>
								<asp:ListItem>施工中</asp:ListItem>
								<asp:ListItem>停工中</asp:ListItem>
								<asp:ListItem>已完工</asp:ListItem>
								<asp:ListItem>廢止</asp:ListItem>
								<asp:ListItem>失效</asp:ListItem>
								<asp:ListItem>已變更</asp:ListItem>
							</asp:CheckBoxList>
							<br/>
							<span class="ittype">通知對象</span><asp:CheckBoxList ID="CHKSENDMBR" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"/><br/><br/>
							<span class="ittype">通知管道</span><asp:CheckBoxList ID="CHKSENDFUN" runat="server" style="border:none;" RepeatDirection="Horizontal" RepeatLayout="Flow"/></td></tr>
                    <tr><th style="vertical-align:middle">水土保持違規案件<span style="color: red;font-family:cursive;">＊</span></th>
                        <td><span class="ittype2">案件狀態</span><asp:CheckBoxList ID="CHKSWCSTATUS_ILG" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"/><br/><br/>
							<span class="ittype2">通知對象</span><asp:CheckBoxList ID="CHKSENDMBR_ILG" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"/><br/><br/>
							<span class="ittype2">通知管道</span><asp:CheckBoxList ID="CHKSENDFUN_ILG" runat="server" style="border:none;" RepeatDirection="Horizontal" RepeatLayout="Flow"/></td></tr>
					<tr><th>事件名稱<span style="color: red;font-family:cursive;">＊</span></th>
                        <td><asp:TextBox ID="TXTDENAME" runat="server" MaxLength="50" style="width:99%;"/></td></tr>
						
					<tr><th>通知內容<span style="color: red;font-family:cursive; " >＊</span></th>
                        <td><asp:TextBox ID="TXTDENTTEXT" runat="server" TextMode="MultiLine" Height="80" Width="99%" onkeyup="textcount(this,'TXTDENTTEXT_count','300');" /><br/>
                            <asp:Label ID="TXTDENTTEXT_count" runat="server" Text="(0/300)" ForeColor="Red" style="padding:10px;"/></td></tr>
                    </table>

                    <table style="border:1px solid #000000; border-top:none;  line-height:1.5; width:1200px;">
                    <%--<tr><td>發送日期<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTDEDATE" runat="server" width="120px" Style="margin-right: 0px;"></asp:TextBox>
                            <asp:CalendarExtender ID="TXTDEDATE_CalendarExtender" runat="server" TargetControlID="TXTDEDATE" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>--%>
          </table>
        </div>
                     </div>

            <br/>


            

            <div class="btncenter">
                <asp:Button ID="DataLock" runat="server" Text="確認送出" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" />
                <asp:Button ID="SaveCase" runat="server" Text="暫時儲存" OnClick="SaveCase_Click" />
                <asp:Button ID="GoHomePage" runat="server" Text="返回瀏覽案件" OnClick="GoHomePage_Click" />
            </div>
        </div>
        


            <%--<div class="footer-s">
                <div class="footer-s-green"></div>
                <div class="footer-b-brown">
                    <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                       <span class="span2">110臺北市信義區松德路300號3樓 　(02)27593001   臺北市民當家熱線1999</span><br/>
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

        <asp:Literal ID="error_msg" runat="server"></asp:Literal>

        <script src="../js/jquery-3.1.1.min.js"></script>
        <script src="../js/inner.js"></script>
        <script src="../js/allhref.js"></script>
    </div>
    </form>
</body>
</html>
