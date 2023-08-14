<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCDT002PWA.aspx.cs" Inherits="SWCDOC_SWCDT002PWA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>臺北市水土保持申請書件管理平台</title>
    <link rel="Shortcut Icon" type="image/x-icon" href="../../images/logo-s.ico">
    <meta name="keywords" content="臺北市水土保持申請書件管理平台, 書件管理平台">
    <meta name="description" content="臺北市水土保持申請書件管理平台">
    <meta name="author" content="dorathy">
    <meta name="copyright" content="© 2017 臺北市水土保持申請書件管理平台">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge, chrome=1">
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    <link rel="stylesheet" type="text/css" href="../../css/reset.css"/>
    <link rel="stylesheet" type="text/css" href="../../css/all.css?202208240353"/>
    <link rel="stylesheet" type="text/css" href="../../css/all_PWA.css?202210240334"/>
	<link rel="manifest" href="manifest.json" />
	<script type="module">
		import 'https://cdn.jsdelivr.net/npm/@pwabuilder/pwaupdate';
		const el = document.createElement('pwa-update');
		document.body.appendChild(el);
	</script>
	<script src="pwaupdate.js"></script>
	<script>
		if ('serviceWorker' in navigator) {
			navigator.serviceWorker.register('pwabuilder-sw.js')
			.then(reg => console.log('完成 SW 設定!', reg))
			.catch(err => console.log('Error!', err));
		}
	</script>
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
			var r = confirm('確認送出後，資料會暫存在系統上。');
			var table = document.getElementById("Table_Temp1");
			var txt_image = "";
			for(var i = 1; i < table.rows.length; i++)
			{
				if(i==table.rows.length-1)
				{
					txt_image += table.rows[i].cells[0].innerHTML + ";;;;;"
					txt_image += table.rows[i].cells[1].innerHTML + ";;;;;"
					txt_image += table.rows[i].cells[2].innerHTML + ";;;;;"
					txt_image += table.rows[i].cells[5].innerHTML
				}
				else
				{
					txt_image += table.rows[i].cells[0].innerHTML + ";;;;;"
					txt_image += table.rows[i].cells[1].innerHTML + ";;;;;"
					txt_image += table.rows[i].cells[2].innerHTML + ";;;;;"
					txt_image += table.rows[i].cells[5].innerHTML + "/////"
				}
			}
			document.getElementById("TXTIMAGE").value = txt_image;
			return r;
        }
		//切換頁簽
		function chk_Panel(tab){
			if(tab==1) 
			{
				document.getElementById("tab1").style.display = "block";
				document.getElementById("tab2").style.display = "none";
				document.getElementById("tab3").style.display = "none";
				document.getElementById("Btn_T1").className = "tabcss_active";
				document.getElementById("Btn_T2").className = "tabcss";
				document.getElementById("Btn_T3").className = "tabcss";
			}
			else if(tab==2) 
			{
				document.getElementById("tab1").style.display = "none";
				document.getElementById("tab2").style.display = "block";
				document.getElementById("tab3").style.display = "none";
				document.getElementById("Btn_T1").className = "tabcss";
				document.getElementById("Btn_T2").className = "tabcss_active";
				document.getElementById("Btn_T3").className = "tabcss";
			}
			else if(tab==3) 
			{
				document.getElementById("tab1").style.display = "none";
				document.getElementById("tab2").style.display = "none";
				document.getElementById("tab3").style.display = "block";
				document.getElementById("Btn_T1").className = "tabcss";
				document.getElementById("Btn_T2").className = "tabcss";
				document.getElementById("Btn_T3").className = "tabcss_active";
			}
			return false;
		}
    </script>
	<script type = "text/javascript">
		//原始資料
        var db;
        var indexedDB = window.indexedDB || window.webkitIndexedDB || window.mozIndexedDB || window.msIndexedDB;
        var IDBTransaction = window.IDBTransaction || window.webkitIDBTransaction;

        var req = indexedDB.open("mydb123");

        req.onsuccess = function (e) {
            db = e.target.result;
        };

        req.onerror = function () {
        };

        req.onupgradeneeded = function (e) {
        }
		
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
			
			if($(window).width() > 1580){
				canvas.width = 1500;
			}
			else{
				canvas.width = $(window).width() - 50;
			}
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
		
		function cUndo() {
			if (cStep > 0) {
				clearCanvas();
				cStep--;
				var canvasPic = new Image();
				canvasPic.src = cPushArray[cStep];
				canvasPic.onload = function () { context.drawImage(canvasPic, 0, 0); }
			}
		}

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
		
		//表單存值
		function setValue() {
            var LBDTL001 = document.getElementById("LBDTL001").innerHTML;
			var LBSWC002 = document.getElementById("LBSWC002").innerHTML;
            var TXTDTL002 = document.getElementById("TXTDTL002").value;
			var LBSWC005 = document.getElementById("LBSWC005").innerHTML;
			var CHKDTL007 = document.getElementById("CHKDTL007").checked;
			var CHKDTL008 = document.getElementById("CHKDTL008").checked;
			var CHKDTL009 = document.getElementById("CHKDTL009").checked;
			var TXTDTL010 = document.getElementById("TXTDTL010").value;
			var DropList011 = document.getElementById("DropList011").value;
			var DropList012 = document.getElementById("DropList012").value;
			var DropList013 = document.getElementById("DropList013").value;
			var TXTDTL014 = document.getElementById("TXTDTL014").value;
			
			var CHKBOX015 = document.getElementById("CHKBOX015").checked;
			var CHKBOX016 = document.getElementById("CHKBOX016").checked;
			var CHKBOX017 = document.getElementById("CHKBOX017").checked;
			var CHKBOX018 = document.getElementById("CHKBOX018").checked;
			
			var TXTDTL019 = document.getElementById("TXTDTL019").value;
			var TXTDTL020 = document.getElementById("TXTDTL020").value;
			
			var url_string = location.href;
			var url = new URL(url_string);
			
            var txn = db.transaction("SWCDT002", "readwrite");
            var store = txn.objectStore("SWCDT002");
			
			var url_string = location.href;
			var url = new URL(url_string);
            var data = { 
				SWCNO: url.searchParams.get("SWCNO"),
				LBDTL001: LBDTL001,
				LBSWC002: LBSWC002,
				TXTDTL002: TXTDTL002,
				LBSWC005: LBSWC005,
				CHKDTL007: CHKDTL007,
				CHKDTL008: CHKDTL008,
				CHKDTL009: CHKDTL009,
				TXTDTL010: TXTDTL010,
				DropList011: DropList011,
				DropList012: DropList012,
				DropList013: DropList013,
				TXTDTL014: TXTDTL014,
				CHKBOX015: CHKBOX015,
				CHKBOX016: CHKBOX016,
				CHKBOX017: CHKBOX017,
				CHKBOX018: CHKBOX018,
				TXTDTL019: TXTDTL019,
				TXTDTL020: TXTDTL020
			};
            var req = store.put(data);
			//儲存圖片列表
			var table = document.getElementById("Table_Temp1");
			
			var txn = db.transaction("SWCDT002_1", "readwrite");
			var store = txn.objectStore("SWCDT002_1");
			var url_string = location.href;
			var url = new URL(url_string);
			var LBDTL001 = document.getElementById("LBDTL001").innerHTML;
					
			var req = store.delete(IDBKeyRange.bound([url.searchParams.get("SWCNO"),document.getElementById("LBDTL001").innerHTML,1],[url.searchParams.get("SWCNO"),document.getElementById("LBDTL001").innerHTML,100]), 'next');
			
			for(var i = 1; i < table.rows.length; i++)
			{
				var txn = db.transaction("SWCDT002_1", "readwrite");
				var store = txn.objectStore("SWCDT002_1");
				
				var data = { 
					SWCNO: url.searchParams.get("SWCNO"),
					DTLNO: LBDTL001,
					NO: parseInt(table.rows[i].cells[0].innerHTML),
					IDENTITY: table.rows[i].cells[1].innerHTML,
					NAME: table.rows[i].cells[2].innerHTML,
					IMAGE: table.rows[i].cells[5].innerHTML
				};
				var req = store.put(data);
			}

            req.onsuccess = function (e) {
				alert("已存檔");
            };

            req.onerror = function () {
			
            };
        };
		
		//新增圖片到列表
		function InsertImageList() {
			var base64 = $('#pCanvas')[0].toDataURL();
            var base64_str = String(base64);
            const txtElement = base64_str;
			
			var url_string = location.href;
			var url = new URL(url_string);
			
			//序號取最大+1
			var table = document.getElementById("Table_Temp1");
			var no = 1;
			if(table.rows.length > 1)
			{
				for(var i = 1;i<table.rows.length;i++)
				{
					no = table.rows[i].cells[0].innerHTML;
				}
				no = parseInt(no, 10) + 1;
			}			
			
			var tr = document.createElement('tr');
			//1 序號
			var td = document.createElement('td');
			var text = document.createTextNode(no);
			td.appendChild(text);
			tr.appendChild(td);
			//2 身分
			td = document.createElement('td');
			text = document.createTextNode(document.getElementById("DDL_Sign").value);
			td.appendChild(text);
			tr.appendChild(td);
			//3 姓名
			td = document.createElement('td');
			text = document.createTextNode(document.getElementById("TB_Sign").value);
			td.appendChild(text);
			tr.appendChild(td);
			//4 瀏覽
			td = document.createElement('td');
			var button = document.createElement('input');
			
			button.setAttribute('value', "瀏覽圖片");
			button.type = "button";
			button.id = "btnView_" + no;
			button.onclick = function(){
				LoadImage();
				alert('讀取完成');
			};
			td.appendChild(button);
			tr.appendChild(td);
			//5 刪除
			td = document.createElement('td');
			var button = document.createElement('input');
			
			button.setAttribute('value', "刪除");
			button.type = "button";
			button.id = "btnDelete_" + no;
			button.onclick = function(){
				DeleteImageList();
				alert('刪除完成');
			};
			td.appendChild(button);
			tr.appendChild(td);
			//6 圖片
			var td = document.createElement('td');
			var text = document.createTextNode(txtElement);
			td.appendChild(text);
			td.style.display = "none";
			tr.appendChild(td);
			
			
			table.appendChild(tr);
			
            req.onsuccess = function (e) {
				alert("已存檔");
            };

            req.onerror = function () {
			
            };
        };
		
		//刪除單筆圖片列表
		function DeleteImageList(){
			var no = (event.target.id).replace("btnDelete_","");
			var table = document.getElementById("Table_Temp1");
			for(var i = 1; i < table.rows.length; i++)
			{
				if(table.rows[i].cells[0].innerHTML == no)
				{
					table.deleteRow(i);
				}
			}
		}
		
		//刪除全部圖片列表
		function DeleteAllImageList(){
			var table = document.getElementById("Table_Temp1");
			for(var i = table.rows.length - 1; i > 0; i--)
			{
				table.deleteRow(i);
			}
		}
		
		//表單取值
		function getValue(btn) {
            var txn = db.transaction("SWCDT002", "readwrite");
            var store = txn.objectStore("SWCDT002");
			
			
			var url_string = location.href;
			var url = new URL(url_string);

            var req = store.get(url.searchParams.get("SWCNO"));
        
            req.onsuccess = function (e) {
				var data1 = e.target.result;
				
				$("[id=TXTDTL002]").val(data1.TXTDTL002);
				$("[id=CHKDTL007]").prop('checked',data1.CHKDTL007);
				$("[id=CHKDTL008]").prop('checked',data1.CHKDTL008);
				$("[id=CHKDTL009]").prop('checked',data1.CHKDTL009);
				$("[id=TXTDTL010]").val(data1.TXTDTL010);
				$("[id=DropList011]").val(data1.DropList011);
				$("[id=DropList012]").val(data1.DropList012);
				$("[id=DropList013]").val(data1.DropList013);
				$("[id=TXTDTL014]").val(data1.TXTDTL014);
				$("[id=CHKBOX015]").prop('checked',data1.CHKBOX015);
				$("[id=CHKBOX016]").prop('checked',data1.CHKBOX016);
				$("[id=CHKBOX017]").prop('checked',data1.CHKBOX017);
				$("[id=CHKBOX018]").prop('checked',data1.CHKBOX018);
				$("[id=TXTDTL019]").val(data1.TXTDTL019);
				$("[id=TXTDTL020]").val(data1.TXTDTL020);
				//讀取圖片列表
				LoadImageList();
				alert("讀取成功");
			};

            req.onerror = function () {
			
            };
		};
		
		//讀取圖片列表
		function LoadImageList(){
			var txn = db.transaction("SWCDT002_1", "readwrite");
            var store = txn.objectStore("SWCDT002_1");
			
			var url_string = location.href;
			var url = new URL(url_string);
			
			var req = store.openCursor(IDBKeyRange.bound([url.searchParams.get("SWCNO"),document.getElementById("LBDTL001").innerHTML,1],[url.searchParams.get("SWCNO"),document.getElementById("LBDTL001").innerHTML,100]), 'next');
			
			DeleteAllImageList();
			
			req.onsuccess = function (e) {
				var cursor = e.target.result;
				if(cursor)
				{
					var table = document.getElementById("Table_Temp1");
					
					var tr = document.createElement('tr');
					//1 序號
					var td = document.createElement('td');
					var text = document.createTextNode(cursor.value.NO);
					td.appendChild(text);
					tr.appendChild(td);
					//2 身分
					td = document.createElement('td');
					text = document.createTextNode(cursor.value.IDENTITY);
					td.appendChild(text);
					tr.appendChild(td);
					//3 姓名
					td = document.createElement('td');
					text = document.createTextNode(cursor.value.NAME);
					td.appendChild(text);
					tr.appendChild(td);
					//4 瀏覽
					td = document.createElement('td');
					var button = document.createElement('input');
					
					button.setAttribute('value', "瀏覽圖片");
					button.type = "button";
					button.id = "btnView_" + cursor.value.NO;
					button.onclick = function(){
						LoadImage();
						alert('讀取完成');
					};
					td.appendChild(button);
					tr.appendChild(td);
					//5 刪除
					td = document.createElement('td');
					var button = document.createElement('input');
					
					button.setAttribute('value', "刪除");
					button.type = "button";
					button.id = "btnDelete_" + cursor.value.NO;
					button.onclick = function(){
						DeleteImageList(cursor.value.NO);
						alert('刪除完成');
					};
					td.appendChild(button);
					tr.appendChild(td);
					//6 圖片
					var td = document.createElement('td');
					var text = document.createTextNode(cursor.value.IMAGE);
					td.appendChild(text);
					td.style.display = "none";
					tr.appendChild(td);
					
					
					table.appendChild(tr);
					
					cursor.continue();
				}
			};

            req.onerror = function () {
			
            };
		}
		
		//瀏覽圖片
		function LoadImage(){
			var no = (event.target.id).replace("btnView_","");
			var IMAGE = "";
			var table = document.getElementById("Table_Temp1");
			for(var i = 1; i < table.rows.length; i++)
			{
				if(table.rows[i].cells[0].innerHTML == no)
				{
					IMAGE = table.rows[i].cells[5].innerHTML
				}
			}
			var canvas = document.getElementById("pCanvas");
			var ctx = canvas.getContext("2d");
			
			var image = new Image();
			image.onload = function () {
				clearCanvas();
				ctx.drawImage(image, 0, 0);
			};
			image.src = IMAGE;
		}
		
		//清除indexeddb(給後端呼叫的)
		function delValue() {
            var txn = db.transaction("SWCDT002", "readwrite");
            var store = txn.objectStore("SWCDT002");
			
			var url_string = location.href;
			var url = new URL(url_string);

            var req = store.delete(url.searchParams.get("SWCNO"));
			
			var txn = db.transaction("SWCDT002_1", "readwrite");
            var store = txn.objectStore("SWCDT002_1");

			var req = store.delete(IDBKeyRange.bound([url.searchParams.get("SWCNO"),document.getElementById("LBDTL001").innerHTML,1],[url.searchParams.get("SWCNO"),document.getElementById("LBDTL001").innerHTML,100]), 'next');
			

            req.onsuccess = function (e) {
				alert("資料已上傳至系統");
				window.open('', '_self', '');
				window.close();
			};

            req.onerror = function () {
			
            };
		};
	</script>
	<style type="text/css">
         
        .auto-style1 {
            font-size: xx-large;
            color: #FF0000;
        }
        .auto-style3 {
            font-size: xx-large;
            color: #FF3300;
        }
        .auto-style4 {
            font-size: xx-large;
            color: #0011FF;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"/>
     
	 
    <div class="wrap-s">
      <div class="header_PWA">
		  <img class="logo" src="../../img/logo2.svg"/>
            <span>水土保持施工抽查紀錄</span>
            <asp:ImageButton runat="server" class="download" src="../../img/PWA_icon-08.svg" onclientclick="getValue(''); return false;"/>
		</div>
		<div class="tabbg">
	    	<asp:Button ID="Btn_T1" runat="server" Text="案件資訊" OnClientClick="return chk_Panel(1);" UseSubmitBehavior="false" class="tabcss_active"/>
	    	<asp:Button ID="Btn_T2" runat="server" Text="檢查項目" OnClientClick="return chk_Panel(2);" UseSubmitBehavior="false" class="tabcss"/>
	    	<asp:Button ID="Btn_T3" runat="server" Text="線上簽名" OnClientClick="return chk_Panel(3);" UseSubmitBehavior="false" class="tabcss"/>
	    </div>

            <div class="b-blue none">
                    <span><asp:Literal ID="TextUserName" runat="server">大雷包，您好</asp:Literal></span>
            </div>

        <div class="content-t PWA_wrap">
            <div class="checkRecord pwa_font pwah1">
               
				<asp:Button ID="GoOnline" runat="server" Text="讀取上次資料" UseSubmitBehavior="false" CssClass="read_data none" Visible="false" onclientclick="return getValue(this);" />
				<asp:Button ID="deletedata" runat="server" Text="刪除離線資料" UseSubmitBehavior="false" onclientclick="return delValue();" Visible="false" /><br/><br/>
                
				
				<%--tab--%>
                  <div class="tab_content">
				    <!-- TAB1 打包區塊 end -->
                    <!--<input id="tab1" type="radio" name="tab" checked="checked"/-->
                    <!--<label for="tab1">案件資訊</label-->
                    <div class="tab_main">
						<asp:Panel ID="tab1" runat="server">
					   <table class="swcchg-outA tdpadding">
                           <tr><th>施工監督表編號</th>
                               <td><asp:Label ID="LBDTL001" runat="server"/>
                                   <asp:Label ID="LBSWC000" runat="server" Visible="false"/>
								   <asp:TextBox ID="TXTIMAGE" runat="server" style="display:none;"/></td></tr>
				       	<tr><th>水保案件編號</th>
				       		<td><asp:Label ID="LBSWC002" runat="server"/></td></tr>
                           <tr><th>檢查日期<span style="color: red;font-family:cursive;">＊</span></th>
                               <td><asp:TextBox ID="TXTDTL002" runat="server" width="120px"></asp:TextBox>
                                   <asp:CalendarExtender ID="TXTDTL002_CalendarExtender" runat="server" TargetControlID="TXTDTL002" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                           <tr><th>檢查類型</th>
                               <td><asp:Label ID="TXTDTL003" runat="server" Text="施工抽查紀錄"/></td></tr>
                           <tr><th>檢查單位</th>
                               <td><asp:Label ID="TXTDTL004" runat="server" width="100%" MaxLength="200" Text="臺北市政府工務局大地工程處"/></td></tr>
                       </table>
						<div class="flex">
							<input id="btn_next1" type="button" value="下一頁" onclick="chk_Panel(2)" class="flex1" runat="server"/>
						</div>
						</asp:Panel>
                    <!-- TAB1 打包區塊 end -->
                    
                    <!-- TAB2 打包區塊 start -->
                    <!--<input id="tab2" type="radio" name="tab"/-->
                    <!--<label for="tab2">檢查項目</label-->
					<asp:Panel ID="tab2" runat="server" style="display:none;">
                    <div> <!--class="tab_content"-->
                      <table class="swcchg-out checknonecss opentxt">
                        <tr><th>水土保持書件名稱</th>
                            <td><asp:Label ID="LBSWC005" runat="server"/></td></tr>
                        <tr><th>一、檢查項目</th>
                            <td style="text-align:center;">現場情形</td></tr>
                        <tr><th>（一）水土保持施工告示牌</th>
                            <td><asp:CheckBox ID="CHKDTL007" runat="server" Text="已設立無誤" /><br/>
                                <asp:CheckBox ID="CHKDTL008" runat="server" Text="未設立" /><br/>
                                <asp:CheckBox ID="CHKDTL009" runat="server" Text="資訊缺漏" />：
                                <asp:TextBox ID="TXTDTL010" runat="server" width="120px"></asp:TextBox></td></tr>
                        <tr><th>（二）臨時性防災措施</th>
                            <td class="mbnone"></td></tr>
                        <tr><td class="td-padding">1.泥水外流未疏導</td>
                            <td><asp:DropDownList ID="DropList011" runat="server"/></td></tr>
                        <tr><td class="td-padding">2.開挖邊坡裸露未覆蓋</td>
                            <td><asp:DropDownList ID="DropList012" runat="server"/></td></tr>
                        <tr><td class="td-padding">3.土方暫置未覆蓋</td>
                            <td><asp:DropDownList ID="DropList013" runat="server"/></td></tr>
                        <tr><td class="td-padding">4.其他</td>
                            <td><asp:TextBox ID="TXTDTL014" runat="server" width="100%" Height="100px" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL014_count','300');" />
                                <asp:Label ID="TXTDTL014_count" runat="server" Text="(0/300)" ForeColor="Red" /></td></tr>
                        <tr><td>（三）災害搶救小組測試</td>
                            <td><asp:CheckBox ID="CHKBOX015" runat="server" Text="正常運作" />
                                <asp:CheckBox ID="CHKBOX016" runat="server" Text="電話無人接聽" />
                                <asp:CheckBox ID="CHKBOX017" runat="server" Text="電話號碼錯誤" />
                                <asp:CheckBox ID="CHKBOX018" runat="server" Text="其他" />：
                                <asp:TextBox ID="TXTDTL019" runat="server" width="120px"></asp:TextBox></td></tr>
                        <tr><td>（四）其他注意事項：</td>
				            <td>
                                <asp:TextBox ID="TXTDTL020" runat="server" width="100%" Height="100px" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL020_count','500');" />
                                <asp:Label ID="TXTDTL020_count" runat="server" Text="(0/500)" ForeColor="Red" /></td></tr>
                      </table>
						
						<div class="flex">
							<input id="btn_previous2" type="button" value="上一頁" onclick="chk_Panel(1)" runat="server" class="flex1"/>&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="btn_next2" type="button" value="下一頁" onclick="chk_Panel(3)" runat="server" class="flex1"/>
						</div>
					
                    </div>
					</asp:Panel>
                    <!-- TAB2 打包區塊 end -->
                    
                    <!-- TAB3 打包區塊 start -->
                    <!--<input id="tab3" type="radio" name="tab"/-->
                    <!--<label for="tab3">線上簽名</label-->
					<asp:Panel ID="tab3" runat="server" style="display:none;">
                    <div>
                       <table class="mbborder">
                         <tr><td colspan="2">
                             <b style="margin-bottom:5px; display:block;">二、相關單位及人員簽名</b><br/>
                                 <asp:TextBox ID="TXTDTL021" runat="server" width="100%" Height="100px" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL021_count','500');" style="height:250px;width:100%;" Visible="false" />
                                 <asp:Label ID="TXTDTL021_count" runat="server" Text="(0/500)" ForeColor="Red" Visible="false" />
				         		 <table class="PWA_signperson">
							      <tr>
							 	   <th>相關人員：</th>
							 	   <td>
							 	    <asp:DropDownList ID="DDL_Sign" runat="server">
										<asp:ListItem Value="檢查單位及人員">檢查單位及人員</asp:ListItem>
										<asp:ListItem Value="承辦監造技師">承辦監造技師</asp:ListItem>
										<asp:ListItem Value="水土保持義務人">水土保持義務人</asp:ListItem>
									</asp:DropDownList>
							 	  </td>
							 	</tr>
							 	<tr>
							 	  <th>人員姓名：</th>
							 	  <td>
							 	     <asp:TextBox ID="TB_Sign" runat="server" />
							 	  </td>
							 	</tr>
							   </table>
								
				         		<canvas id="pCanvas"  width="340" height="200"
				         			onmousedown="mousedownandler(event)" 
				         			onmousemove="mousemoveandler(event)"
				         			onmouseup="mouseupHandler(event)"        
				         			style="cursor:pointer;background:white;border:1px #CCC solid;padding-left: 0;padding-right: 0;margin-left: auto;margin-right: auto;display: block;">
				         		</canvas>
				         		<br />
				         			<div class="center">
				         			   <asp:Button ID="TXTDTL022_fileuploadok" runat="server" Text="上傳簽名檔" class="sendsign" OnClick="TXTDTL022_fileuploadok_Click" Visible="false" />
                                       <asp:Button ID="btn1" runat="server" UseSubmitBehavior="false" onclientclick="return clearCanvas();" Text="重新簽名" class="retunsign" />
									   <asp:Button ID="btn2" runat="server" UseSubmitBehavior="false" Text="確認簽名並加入清單" CssClass="signok" onclientclick="InsertImageList(); return false;" />
									   <!--<asp:Button ID="btn3" runat="server" UseSubmitBehavior="false" Text="上一步" CssClass="signok" onclientclick="cUndo(); return false;" />-->
				         			</div>
									<table class="signTB" ID="Table_Temp1" runat="server">
										<tr>
											<th>序號</th>
											<th>相關單位</th>
											<th>人員</th>
											<th>瀏覽</th>
											<th>刪除</th>
										</tr>
									</table>
				         			<asp:HyperLink ID="HyperLink022" runat="server" Visible="false" CssClass="imgUpload-l80" Target="_blank"></asp:HyperLink>
                                 
				         			<asp:Image ID="TXTDTL022_img" runat="server" CssClass="imgUpload-l80" Visible="false" />
				         		<asp:Panel runat="server" Visible="false">
				         			<br/><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/><br/>
				         			<asp:FileUpload ID="TXTDTL022_fileupload" runat="server" />
				         			<asp:Button ID="TXTDTL022_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，確認是否刪除照片?')" OnClick="TXTDTL022_fileuploaddel_Click" />
				         		</asp:Panel>
				         	</td></tr>
                         <tr style="display:none;"><td colspan="2">三、上傳檔案<br/><br/>
                                 <asp:HyperLink ID="Link023" runat="server" Target="_blank" />
                                 <asp:TextBox ID="TXTDTL023" runat="server" Width="70px" Visible="False" /><br/><br/>
                                 <asp:FileUpload ID="TXTDTL023_fileupload" runat="server" />
                                 <asp:Button ID="TXTDTL023_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL023_fileuploadok_Click" />
                                 <asp:Button ID="TXTDTL023_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('資料刪除後無法復原，確認是否刪除這筆資料?')" OnClick="TXTDTL023_fileuploaddel_Click" />
                                 <span style="color:red;">※ 上傳格式限定為PDF檔案大小請於50mb以內</span></td></tr>
                   </table>
						
						<div class="flex">
							<input id="btn_previous3" type="button" value="上一頁" onclick="chk_Panel(2)" runat="server" class="flex1"/><br/>
				         	<asp:TextBox ID="TXTDTL022" runat="server" Width="100px" CssClass="" Visible="false" /><!--noshow-->
				         	<asp:TextBox id="result_image" runat="server" Width="100px" CssClass="" Visible="false" /><!--noshow-->
						</div>
                    </div>
					</asp:Panel>
                    <!-- TAB3 打包區塊 end -->
                    </div>
                  </div>

                <%--tab_end--%>

                <%--<div class="form-btn">
					
					
                </div>--%>
        </div>
      </div>
	  <br/><br/><br/>
	  <div class="PWAbottombtn">
		  <asp:ImageButton runat="server" UseSubmitBehavior="false" src="../../img/PWA_icon-09.svg" onclientclick="setValue();"/>
		  <asp:ImageButton ID="SaveCase" runat="server" UseSubmitBehavior="false" src="../../img/PWA_icon-10.svg" OnClientClick="return chkInput('');" OnClick="SaveCase_Click"/>
      </div>
      <!--<div class="PWAbottombtn none">
        <asp:Button ID="GoOffline1" runat="server" Text="離線暫存" UseSubmitBehavior="false" onclientclick="setValue();" class="PWAsavecase flexbtn1" />&nbsp&nbsp
        <asp:Button ID="SaveCase1" runat="server" Text="上傳至系統" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" class="PWAffline flexbtn2" />
      </div>-->
		<div class="go-top1">TOP</div>
            <div class="footer none">
                <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                <span class="span2">110臺北市信義區松德路300號3樓 　服務專線(02)27591109   臺北市民當家熱線1999</span><br/>
                <span class="span2">建議使用IE11.0(含)以上，Chrome或Firefox版本瀏覽器 資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                <span class="span2">客服電話：02-27593001#3718 許先生 本系統由多維空間資訊有限公司開發維護 TEL:(02)27929328</span></p>
            </div>
        </div>

        <asp:Literal ID="error_msg" runat="server"></asp:Literal>

        <script src="../../js/jquery-3.1.1.min.js"></script>
        <script src="../../js/inner.js?20220217"></script>
    </div>
    </form>
</body>
</html>
