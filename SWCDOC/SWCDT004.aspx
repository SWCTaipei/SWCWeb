﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCDT004.aspx.cs" Inherits="SWCDOC_SWCDT004" %>
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
    <meta name="viewport" content="width=device-width">
    <link rel="stylesheet" type="text/css" href="../css/reset.css"/>
    <link rel="stylesheet" type="text/css" href="../css/all.css"/>
    <link rel="stylesheet" type="text/css" href="../css/iris.css"/>

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
        function chkInput(jChkType) {
            jCHKValue01 = document.getElementById("TXTDTL002").value;

            if (jCHKValue01.trim() == '') {
                alert('請輸入檢查日期');
                document.getElementById("TXTDTL002").focus();
                return false;
            } else {
                var t = Date.parse(jCHKValue01);
                if (isNaN(t)) {
                    document.getElementById("TXTDTL002").focus();
                    return false;
                }
            }
            if (jChkType == 'DataLock') {
                var r = confirm('確認送出後，即不可修改，請再次確認是否要完成送出。');
                return r;
            }
        }
    </script>
	<script>
		//Canvas
        var paintpoints = new Array();
        var paintpoints_content = new Array();
        var canvas;
        var context;
        var paint = false;
        var lastPoint;
		
		var cPushArray = new Array();
		var cStep = -1;
		
		window.onload = function () {
            canvas = document.getElementById("pCanvas");
            context = canvas.getContext("2d");
			
			canvas.width = $(id_pCanvas).width();
			//if($(window).width() > 1580){
			//	canvas.width = 800;
			//}
			//else{
			//	canvas.width = $(window).width() - 100;
			//}
			canvas.height = 200;

            canvas.addEventListener("touchstart", touchstart, false);
            canvas.addEventListener("touchend", touchend, false);
            canvas.addEventListener("touchmove", touchmove, false);
        };
		
		function cPush() {
			cStep++;
			if (cStep < cPushArray.length) { cPushArray.length = cStep; }
			cPushArray.push(document.getElementById('pCanvas').toDataURL());
		}
		
		window.onresize = function () {
			canvas = document.getElementById("pCanvas");
            canvas.width = $(id_pCanvas).width();
        };
		
		var getMousePos = function (cv, e) {
            var rect = cv.getBoundingClientRect();
            return {
                x: e.clientX - rect.left,//相對於Canvas左上角的X座標
                y: e.clientY - rect.top,//相對於Canvas左上角的Y座標
                rectLeft: rect.left,
                rectTop: rect.top,
                clientX: e.clientX,
                clientY: e.clientY
            }
        }
		
		var getTouchPos = function (cv, e) {
            var rect = cv.getBoundingClientRect();
            return {
                x: e.touches[0].clientX - rect.left,//相對於Canvas左上角的X座標
                y: e.touches[0].clientY - rect.top,//相對於Canvas左上角的Y座標                
            }
        }
		
		function mousedownandler(e) {
            var pos = getMousePos(canvas, e);
            var x = pos.x;
            var y = pos.y;
            context.beginPath();
            context.lineWidth = 2;
            context.strokeStyle = 'blue';
            context.moveTo(x, y);
            paint = true;
        }
		
		function mousemoveandler(e) {
            if (paint) {
                var pos = getMousePos(canvas, e);
                var x = pos.x;
                var y = pos.y;
                context.lineTo(x, y);
                context.stroke();
            }
        }
		
		function mouseupHandler(e) {
            paint = false;
			cPush();
        }
		
		function clearCanvas() {
            context.clearRect(0, 0, canvas.width, canvas.height);
        }
		
		function touchmove(e) {
            e.preventDefault();
            var pos = getTouchPos(canvas, e);
            var x = pos.x;
            var y = pos.y;
            if (!paint) { return }

            context.lineTo(x, y);
            context.stroke();
        }
		
		function touchstart(e) {
            var pos = getTouchPos(canvas, e);
            var x = pos.x;
            var y = pos.y;
            paint = true;

            context.beginPath();
            context.lineWidth = 2;
            context.strokeStyle = 'blue';
            context.moveTo(x, y);
        }
		
		function touchend(e) {
            paint = false;
        }
		
		function CanvasToImage() {
			var base64 = $('#pCanvas')[0].toDataURL();
			var base64_str = String(base64);
			document.getElementById("hfImageData").value = base64_str;
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
            <div class="checklist form">
            <h1>颱風豪雨設施自主檢查表</h1><br/><br/><br/>
          
            <table class="checklist-out">
                <tr><td>自主檢查表編號</td>
                    <td><asp:Label ID="LBDTL001" runat="server"/>
                        <asp:Label ID="LBSWC000" runat="server" Visible="false"/></td></tr>
                <tr><td>案件名稱</td>
                    <td><asp:Label ID="LBSWC005" runat="server"/></td></tr>
                <tr><td>檢查日期<span style="color: red;font-family:cursive;">＊</span></td>
                    <td><asp:TextBox ID="TXTDTL002" runat="server" width="150px" autocomplete="off"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTDTL002_CalendarExtender" runat="server" TargetControlID="TXTDTL002" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                <tr><td>防災標的<span style="color: red;font-family:cursive;">＊</span></td>
                    <td><asp:DropDownList ID="DDLDTL003" runat="server" Height="25px"/>
                        <asp:DropDownList ID="DDLDTL085" runat="server" Height="25px"/></td></tr>
                <tr><td>自主檢查結果<span style="color: red;font-family:cursive;">＊</span></td>
                    <td><asp:DropDownList ID="DDLDTL004" runat="server" Height="25px"/></td></tr>
            </table>

            <br/><br/>
                
            <table class="checklist-orig">
            <tr><th>一、檢查項目</th>
                <th colspan="4">檢查情形-設施功能是否完善<br/>(填"否"者，請填寫因應措施)</th>
                <th>因應措施</th>
                <th>備註</th></tr>
            <tr><td colspan="7">（一）水土保持施工告示牌</td></tr>
            <tr><td class="td-padding">
                    <asp:CheckBox ID="CHKDTL005" runat="server" Text="告示牌資訊" /></td>
                <td colspan="4">
                    <asp:DropDownList ID="DDLDTL006" runat="server">
                        <asp:ListItem Text="是" Value="是" Selected="True"></asp:ListItem>
					    <asp:ListItem Text="否" Value="否"></asp:ListItem>
                    </asp:DropDownList></td>
                <td><asp:TextBox ID="TXTDTL007" runat="server" onkeyup="textcount(this,'TXTDTL007_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL007_count" runat="server" Text="(0/50)" ForeColor="Red" /></td>
                <td><asp:TextBox ID="TXTDTL008" runat="server" onkeyup="textcount(this,'TXTDTL008_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL008_count" runat="server" Text="(0/50)" ForeColor="Red" /></td></tr>
            <tr><td class="td-padding">
                    <asp:CheckBox ID="CHKDTL009" runat="server" Text="防災小組正常運作" /></td>
                <td colspan="4">
                    <asp:DropDownList ID="DDLDTL010" runat="server">
                        <asp:ListItem Text="是" Value="是" Selected="True"></asp:ListItem>
					    <asp:ListItem Text="否" Value="否"></asp:ListItem>
                    </asp:DropDownList></td>
                <td><asp:TextBox ID="TXTDTL011" runat="server" onkeyup="textcount(this,'TXTDTL011_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL011_count" runat="server" Text="(0/50)" ForeColor="Red" /></td>
                <td><asp:TextBox ID="TXTDTL012" runat="server" onkeyup="textcount(this,'TXTDTL012_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL012_count" runat="server" Text="(0/50)" ForeColor="Red" /></td></tr>
            <tr><td class="td-padding">
                    <asp:CheckBox ID="CHKDTL013" runat="server" Text="行動電話保持暢通" /></td>
                <td colspan="4">
                    <asp:DropDownList ID="DDLDTL014" runat="server">
                        <asp:ListItem Text="是" Value="是" Selected="True"></asp:ListItem>
					    <asp:ListItem Text="否" Value="否"></asp:ListItem>
                    </asp:DropDownList></td>
                <td><asp:TextBox ID="TXTDTL015" runat="server" onkeyup="textcount(this,'TXTDTL015_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL015_count" runat="server" Text="(0/50)" ForeColor="Red" /></td>
                <td><asp:TextBox ID="TXTDTL016" runat="server" onkeyup="textcount(this,'TXTDTL016_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL016_count" runat="server" Text="(0/50)" ForeColor="Red" /></td></tr>
            <tr><td colspan="7">（二）臨時性防災措施</td></tr>
            <tr><td class="td-padding">
                    <asp:CheckBox ID="CHKDTL017" runat="server" Text="排水設施" /></td>
                <td colspan="4">
                    <asp:DropDownList ID="DDLDTL018" runat="server">
                        <asp:ListItem Text="是" Value="是" Selected="True"></asp:ListItem>
					    <asp:ListItem Text="否" Value="否"></asp:ListItem>
                    </asp:DropDownList></td>
                <td><asp:TextBox ID="TXTDTL019" runat="server" onkeyup="textcount(this,'TXTDTL019_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL019_count" runat="server" Text="(0/50)" ForeColor="Red" /></td>
                <td><asp:TextBox ID="TXTDTL020" runat="server" onkeyup="textcount(this,'TXTDTL020_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL020_count" runat="server" Text="(0/50)" ForeColor="Red" /></td></tr>
            <tr><td class="td-padding">
                    <asp:CheckBox ID="CHKDTL021" runat="server" Text="沉砂設施" /></td>
                <td colspan="4">
                    <asp:DropDownList ID="DDLDTL022" runat="server">
                        <asp:ListItem Text="是" Value="是" Selected="True"></asp:ListItem>
					    <asp:ListItem Text="否" Value="否"></asp:ListItem>
                    </asp:DropDownList></td>
                <td><asp:TextBox ID="TXTDTL023" runat="server" onkeyup="textcount(this,'TXTDTL023_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL023_count" runat="server" Text="(0/50)" ForeColor="Red" /></td>
                <td><asp:TextBox ID="TXTDTL024" runat="server" onkeyup="textcount(this,'TXTDTL024_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL024_count" runat="server" Text="(0/50)" ForeColor="Red" /></td></tr>
            <tr><td class="td-padding">
                    <asp:CheckBox ID="CHKDTL025" runat="server" Text="滯洪設施" /></td>
                <td colspan="4">
                    <asp:DropDownList ID="DDLDTL026" runat="server">
                        <asp:ListItem Text="是" Value="是" Selected="True"></asp:ListItem>
					    <asp:ListItem Text="否" Value="否"></asp:ListItem>
                    </asp:DropDownList></td>
                <td><asp:TextBox ID="TXTDTL027" runat="server" onkeyup="textcount(this,'TXTDTL027_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL027_count" runat="server" Text="(0/50)" ForeColor="Red" /></td>
                <td><asp:TextBox ID="TXTDTL028" runat="server" onkeyup="textcount(this,'TXTDTL028_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL028_count" runat="server" Text="(0/50)" ForeColor="Red" /></td></tr>
            <tr><td class="td-padding">
                    <asp:CheckBox ID="CHKDTL029" runat="server" Text="土方暫置" /></td>
                <td colspan="4">
                    <asp:DropDownList ID="DDLDTL030" runat="server">
                        <asp:ListItem Text="是" Value="是" Selected="True"></asp:ListItem>
					    <asp:ListItem Text="否" Value="否"></asp:ListItem>
                    </asp:DropDownList></td>
                <td><asp:TextBox ID="TXTDTL031" runat="server" onkeyup="textcount(this,'TXTDTL031_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL031_count" runat="server" Text="(0/50)" ForeColor="Red" /></td>
                <td><asp:TextBox ID="TXTDTL032" runat="server" onkeyup="textcount(this,'TXTDTL032_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL032_count" runat="server" Text="(0/50)" ForeColor="Red" /></td></tr>
            <tr><td class="td-padding">
                    <asp:CheckBox ID="CHKDTL033" runat="server" Text="邊坡保護措施" /></td>
                <td colspan="4">
                    <asp:DropDownList ID="DDLDTL034" runat="server">
                        <asp:ListItem Text="是" Value="是" Selected="True"></asp:ListItem>
					    <asp:ListItem Text="否" Value="否"></asp:ListItem>
                    </asp:DropDownList></td>
                <td><asp:TextBox ID="TXTDTL035" runat="server" onkeyup="textcount(this,'TXTDTL035_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL035_count" runat="server" Text="(0/50)" ForeColor="Red" /></td>
                <td><asp:TextBox ID="TXTDTL036" runat="server" onkeyup="textcount(this,'TXTDTL036_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL036_count" runat="server" Text="(0/50)" ForeColor="Red" /></td></tr>
            <tr><td class="td-padding">
                    <asp:CheckBox ID="CHKDTL037" runat="server" Text="施工便道" /></td>
                <td colspan="4">
                    <asp:DropDownList ID="DDLDTL038" runat="server">
                        <asp:ListItem Text="是" Value="是" Selected="True"></asp:ListItem>
					    <asp:ListItem Text="否" Value="否"></asp:ListItem>
                    </asp:DropDownList></td>
                <td><asp:TextBox ID="TXTDTL039" runat="server" onkeyup="textcount(this,'TXTDTL039_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL039_count" runat="server" Text="(0/50)" ForeColor="Red" /></td>
                <td><asp:TextBox ID="TXTDTL040" runat="server" onkeyup="textcount(this,'TXTDTL040_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL040_count" runat="server" Text="(0/50)" ForeColor="Red" /></td></tr>
            <tr><td class="td-padding">
                    <asp:CheckBox ID="CHKDTL041" runat="server" Text="其他（如防災砂包）" /></td>
                <td colspan="4">
                    <asp:DropDownList ID="DDLDTL042" runat="server">
                        <asp:ListItem Text="是" Value="是" Selected="True"></asp:ListItem>
					    <asp:ListItem Text="否" Value="否"></asp:ListItem>
                    </asp:DropDownList></td>
                <td><asp:TextBox ID="TXTDTL043" runat="server" onkeyup="textcount(this,'TXTDTL043_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL043_count" runat="server" Text="(0/50)" ForeColor="Red" /></td>
                <td><asp:TextBox ID="TXTDTL044" runat="server" onkeyup="textcount(this,'TXTDTL044_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL044_count" runat="server" Text="(0/50)" ForeColor="Red" /></td></tr>
            <tr><td colspan="7">（三）永久性防災措施</td></tr>
            <tr><td class="td-padding">
                    <asp:CheckBox ID="CHKDTL045" runat="server" Text="排水設施" /></td>
                <td colspan="4">
                    <asp:DropDownList ID="DDLDTL046" runat="server">
                        <asp:ListItem Text="是" Value="是" Selected="True"></asp:ListItem>
					    <asp:ListItem Text="否" Value="否"></asp:ListItem>
                    </asp:DropDownList></td>
                <td><asp:TextBox ID="TXTDTL047" runat="server" onkeyup="textcount(this,'TXTDTL047_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL047_count" runat="server" Text="(0/50)" ForeColor="Red" /></td>
                <td><asp:TextBox ID="TXTDTL048" runat="server" onkeyup="textcount(this,'TXTDTL048_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL048_count" runat="server" Text="(0/50)" ForeColor="Red" /></td></tr>
            <tr><td class="td-padding">
                    <asp:CheckBox ID="CHKDTL049" runat="server" Text="沉砂設施" /></td>
                <td colspan="4">
                    <asp:DropDownList ID="DDLDTL050" runat="server">
                        <asp:ListItem Text="是" Value="是" Selected="True"></asp:ListItem>
					    <asp:ListItem Text="否" Value="否"></asp:ListItem>
                    </asp:DropDownList></td>
                <td><asp:TextBox ID="TXTDTL051" runat="server" onkeyup="textcount(this,'TXTDTL051_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL051_count" runat="server" Text="(0/50)" ForeColor="Red" /></td>
                <td><asp:TextBox ID="TXTDTL052" runat="server" onkeyup="textcount(this,'TXTDTL052_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL052_count" runat="server" Text="(0/50)" ForeColor="Red" /></td></tr>
            <tr><td class="td-padding">
                    <asp:CheckBox ID="CHKDTL053" runat="server" Text="滯洪設施" /></td>
                <td colspan="4">
                    <asp:DropDownList ID="DDLDTL054" runat="server">
                        <asp:ListItem Text="是" Value="是" Selected="True"></asp:ListItem>
					    <asp:ListItem Text="否" Value="否"></asp:ListItem>
                    </asp:DropDownList></td>
                <td><asp:TextBox ID="TXTDTL055" runat="server" onkeyup="textcount(this,'TXTDTL055_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL055_count" runat="server" Text="(0/50)" ForeColor="Red" /></td>
                <td><asp:TextBox ID="TXTDTL056" runat="server" onkeyup="textcount(this,'TXTDTL056_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL056_count" runat="server" Text="(0/50)" ForeColor="Red" /></td></tr>
            <tr><td class="td-padding">
                    <asp:CheckBox ID="CHKDTL057" runat="server" Text="聯外排水" /></td>
                <td colspan="4">
                    <asp:DropDownList ID="DDLDTL058" runat="server">
                        <asp:ListItem Text="是" Value="是" Selected="True"></asp:ListItem>
					    <asp:ListItem Text="否" Value="否"></asp:ListItem>
                    </asp:DropDownList></td>
                <td><asp:TextBox ID="TXTDTL059" runat="server" onkeyup="textcount(this,'TXTDTL059_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL059_count" runat="server" Text="(0/50)" ForeColor="Red" /></td>
                <td><asp:TextBox ID="TXTDTL060" runat="server" onkeyup="textcount(this,'TXTDTL060_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL060_count" runat="server" Text="(0/50)" ForeColor="Red" /></td></tr>
            <tr><td class="td-padding">
                    <asp:CheckBox ID="CHKDTL061" runat="server" Text="擋土設施" /></td>
                <td colspan="4">
                    <asp:DropDownList ID="DDLDTL062" runat="server">
                        <asp:ListItem Text="是" Value="是" Selected="True"></asp:ListItem>
					    <asp:ListItem Text="否" Value="否"></asp:ListItem>
                    </asp:DropDownList></td>
                <td><asp:TextBox ID="TXTDTL063" runat="server" onkeyup="textcount(this,'TXTDTL063_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL063_count" runat="server" Text="(0/50)" ForeColor="Red" /></td>
                <td><asp:TextBox ID="TXTDTL064" runat="server" onkeyup="textcount(this,'TXTDTL064_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL064_count" runat="server" Text="(0/50)" ForeColor="Red" /></td></tr>
            <tr><td class="td-padding">
                    <asp:CheckBox ID="CHKDTL065" runat="server" Text="植生工程" /></td>
                <td colspan="4">
                    <asp:DropDownList ID="DDLDTL066" runat="server">
                        <asp:ListItem Text="是" Value="是" Selected="True"></asp:ListItem>
					    <asp:ListItem Text="否" Value="否"></asp:ListItem>
                    </asp:DropDownList></td>
                <td><asp:TextBox ID="TXTDTL067" runat="server" onkeyup="textcount(this,'TXTDTL067_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL067_count" runat="server" Text="(0/50)" ForeColor="Red" /></td>
                <td><asp:TextBox ID="TXTDTL068" runat="server" onkeyup="textcount(this,'TXTDTL068_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL068_count" runat="server" Text="(0/50)" ForeColor="Red" /></td></tr>
            <tr><td class="td-padding">
                    <asp:CheckBox ID="CHKDTL069" runat="server" Text="邊坡穩定措施" /></td>
                <td colspan="4">
                    <asp:DropDownList ID="DDLDTL070" runat="server">
                        <asp:ListItem Text="是" Value="是" Selected="True"></asp:ListItem>
					    <asp:ListItem Text="否" Value="否"></asp:ListItem>
                    </asp:DropDownList></td>
                <td><asp:TextBox ID="TXTDTL071" runat="server" onkeyup="textcount(this,'TXTDTL071_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL071_count" runat="server" Text="(0/50)" ForeColor="Red" /></td>
                <td><asp:TextBox ID="TXTDTL072" runat="server" onkeyup="textcount(this,'TXTDTL072_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL072_count" runat="server" Text="(0/50)" ForeColor="Red" /></td></tr>
            <tr><td class="td-padding">
                    <asp:CheckBox ID="CHKDTL073" runat="server" Text="其他" /></td>
                <td colspan="4">
                    <asp:DropDownList ID="DDLDTL074" runat="server">
                        <asp:ListItem Text="是" Value="是" Selected="True"></asp:ListItem>
					    <asp:ListItem Text="否" Value="否"></asp:ListItem>
                    </asp:DropDownList></td>
                <td><asp:TextBox ID="TXTDTL075" runat="server" onkeyup="textcount(this,'TXTDTL075_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL075_count" runat="server" Text="(0/50)" ForeColor="Red" /></td>
                <td><asp:TextBox ID="TXTDTL076" runat="server" onkeyup="textcount(this,'TXTDTL076_count','50');" MaxLength="50" />
                    <asp:Label ID="TXTDTL076_count" runat="server" Text="(0/50)" ForeColor="Red" /></td></tr>
            <tr><td colspan="7" style="border-bottom:none;">其他事項：<br/><br/>
                    <asp:TextBox ID="TXTDTL077" runat="server" width="100%" Height="60px" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL077_count','800');" style="width:100%;height:250px;" />
                    <asp:Label ID="TXTDTL077_count" runat="server" Text="(0/800)" ForeColor="Red" /></td></tr>
                <tr><td colspan="7" style="text-align:right;border-top:none;"><%--檢查日期：--%>
                        <asp:TextBox ID="TXTDTL078" runat="server" width="120px" Visible="false"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTDTL078_CalendarExtender" runat="server" TargetControlID="TXTDTL078" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                <tr><td colspan="7">二、相關單位及人員簽名<br/><br/>
					<asp:TextBox ID="TXTDTL079" runat="server" width="100%" Height="100px" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL079_count','800');" style="width:100%;height:250px;" /><br/>
					<asp:Label ID="TXTDTL079_count" runat="server" Text="(0/800)" ForeColor="Red" />
					<p style="line-height:2.5;">註:1.本表格請依所核水土保持設施項目填報，無此項或尚未施作項目請於備註欄說明。並視所需自行增減欄位。<br/></p>
					<p style="padding-left:20px;margin-top:10px;">2.請水土保持義務人會同承辦監造技師及施工廠商，於中央氣象局發布海上颱風警報或豪雨特報後填列本表。</p></td></tr>
			<tr><td colspan="7" style="width:70% !impprtant; border-bottom:none;" id="id_pCanvas">
					<div>
						<b>相關單位：</b>
						<asp:DropDownList ID="DDL_Sign" runat="server">
							<asp:ListItem Value="檢查單位及人員">檢查單位及人員</asp:ListItem>
							<asp:ListItem Value="承辦監造技師">承辦監造技師</asp:ListItem>
							<asp:ListItem Value="水土保持義務人">水土保持義務人</asp:ListItem>
						</asp:DropDownList>
					</div>
					<br />
					<div>
						<b>人&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;員：</b>
						<asp:TextBox ID="TB_Sign" runat="server" />
					</div>
					<br />
					<canvas id="pCanvas"  
				    	onmousedown="mousedownandler(event)" 
				    	onmousemove="mousemoveandler(event)"
				    	onmouseup="mouseupHandler(event)"        
				    	style="cursor:pointer;background:white;border:1px #CCC solid;padding-left: 0;padding-right: 0;margin-left: auto;margin-right: auto;">
				    </canvas>
					<asp:Button ID="btn1" runat="server" UseSubmitBehavior="false" OnClientClick="return clearCanvas();" Text="重新簽名" class="retunsign" />
					<asp:Button ID="btn2" runat="server" Text="確認簽名並加入清單" OnClientClick="CanvasToImage();" OnClick="btn2_Click" />
					<asp:TextBox ID="hfImageData" runat="server" style="display:none;"/></td></tr>
            <tr style="margin-bottom:none;"><td colspan="7" style="width:30%; border-top:none; border-bottom:none;"><span style="color:red;">※ 檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span>
					<br/><span style="color:red;">※ 上傳檔案請勿使用+、空格、/、\、?、%、#、&、=、!...等特殊符號(包含全形符號)</span><br/><br/>
                    <asp:Image ID="TXTDTL084_img" runat="server" CssClass="upimg" Visible="false" />
					<asp:HyperLink ID="HyperLink084" runat="server" CssClass="imgUpload" Target="_blank" Visible="false"></asp:HyperLink>
					<asp:TextBox ID="TXTDTL084" runat="server" Width="70px" Visible="false" />
					<asp:FileUpload ID="TXTDTL084_fileupload" runat="server" />
					<asp:Button ID="TXTDTL084_fileuploadok" runat="server" Text="上傳檔案並加入清單" OnClick="TXTDTL084_fileuploadok_Click" />
					<asp:Button ID="TXTDTL084_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL084_fileuploaddel_Click" Visible="false" /></td></tr>
			<tr style="border-top:none;"><td colspan="7" style="border-top:none;">
					<asp:GridView ID="GVIMAGE" runat="server" AutoGenerateColumns="False" OnRowDataBound="GVIMAGE_RowDataBound" OnRowDeleting="GVIMAGE_RowDeleting" class="signTB">
                        <Columns>
                            <asp:BoundField DataField="NO" HeaderText="序號" />
							<asp:BoundField DataField="IDENTITY" HeaderText="相關單位" />
							<asp:BoundField DataField="NAME" HeaderText="人員" />
                            <asp:TemplateField HeaderText="圖片" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
								<ItemTemplate>
									<asp:HyperLink ID="link" runat="server" Text='<%# Eval("IMAGENAME") %>' />
								</ItemTemplate>
							</asp:TemplateField>
							<asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
					</td></tr>
			</table>
				
				<br><br>
				<div class="inbox">
                  <div class="leftimg bottombd">
                   <div class="imgtxt">現場相片一</div>
                    <div>
                      <div class="imgUpload-btn">
                        <asp:TextBox ID="TXTDTL080" runat="server" Width="50px" Visible="False" />
                        <asp:FileUpload ID="TXTDTL080_fileupload" runat="server" />
                        <asp:Button ID="TXTDTL080_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL080_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL080_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請確認真的要刪除照片!!!')" OnClick="TXTDTL080_fileuploaddel_Click" />
                    </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL080_img" runat="server" CssClass="imgUpload" Visible="false" />
                        <asp:HyperLink ID="HyperLink080" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink>
                    <br/>
                        <asp:TextBox ID="TXTDTL081" runat="server" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL081_count','200');" placeholder="照片說明文字" CssClass="imgUploadText" /><br/>
                        <asp:Label ID="TXTDTL081_count" runat="server" Text="(0/200)" ForeColor="Red" />
				    </div>
				</div>
				
				<div class="rightimg bottombd">
                  <div class="imgtxt">現場相片二</div>
                   <div>        
                     <div class="imgUpload-btn">
					 <div class="imgUpload-btn">
                        <asp:TextBox ID="TXTDTL082" runat="server" Width="50px" Visible="False" />
                        <asp:FileUpload ID="TXTDTL082_fileupload" runat="server" />
                        <asp:Button ID="TXTDTL082_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL082_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL082_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請確認真的要刪除照片!!!')" OnClick="TXTDTL082_fileuploaddel_Click" />
                    </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL082_img" runat="server" CssClass="imgUpload" Visible="false" />
                        <asp:HyperLink ID="HyperLink082" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink>
                    <br/>
                        <asp:TextBox ID="TXTDTL083" runat="server" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL083_count','200');" placeholder="照片說明文字" CssClass="imgUploadText" /><br/>
                        <asp:Label ID="TXTDTL083_count" runat="server" Text="(0/200)" ForeColor="Red" />
					</div>
				  </div>
				</div>
			</div>
                <div class="form-btn">
                    <asp:Button ID="DataLock" runat="server" Text="確認送出" OnClientClick="return chkInput('DataLock');" OnClick="DataLock_Click" />&nbsp&nbsp
                    <asp:Button ID="SaveCase" runat="server" Text="暫時儲存" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" />&nbsp&nbsp
                    <asp:Button ID="GoHomePage" runat="server" Text="返回編輯案件" OnClick="GoHomePage_Click" />
                </div>

            </div>
        </div>

<%--        <div class="footer-s">
            <div class="footer-s-green"></div>
                <div class="footer-b-brown">
                    <p> <span class="span1">臺北市政府工務局大地工程處</span><br><span class="span2">110臺北市信義區松德路300號3樓 　(02)27593001   臺北市民當家熱線1999</span><br><span class="span2">資料更新：2017.5.19　來訪人數：123456789 </span></p>
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

    </div>
    </form>
</body>
</html>
