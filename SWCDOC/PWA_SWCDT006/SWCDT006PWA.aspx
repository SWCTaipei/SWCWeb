<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCDT006PWA.aspx.cs" Inherits="PWA_SWCDT006_SWCDT006PWA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>臺北市水土保持申請書件管理平台</title>
    <link rel="Shortcut Icon" type="image/x-icon" href="../../images/logo-s.ico" />
    <meta name="keywords" content="臺北市水土保持申請書件管理平台, 書件管理平台" />
    <meta name="description" content="臺北市水土保持申請書件管理平台" />
    <meta name="author" content="dobubu" />
    <meta name="copyright" content="© 2017 臺北市水土保持申請書件管理平台" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge, chrome=1" />
    <meta name="viewport" content="width=device-width" />
    <link rel="stylesheet" type="text/css" href="../../css/reset.css" />
    <link rel="stylesheet" type="text/css" href="../../css/all.css?202108230131" />
    <link rel="stylesheet" type="text/css" href="../../css/iris.css?202106180341" />

    <link rel="stylesheet" type="text/css" href="../../css/all_PWA.css?202210240336"/>
    <link rel="stylesheet" type="text/css" href="../../css/all_PWA_2.css?202210240336"/>
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
		function chk_Panel(tab) {
            if(tab==1) 
			{
				document.getElementById("tab1").style.display = "block";
				document.getElementById("tab2").style.display = "none";
				document.getElementById("tab3").style.display = "none";
				document.getElementById("tab4").style.display = "none";
				document.getElementById("Btn_T1").className = "tabcss_active";
				document.getElementById("Btn_T2").className = "tabcss";
				document.getElementById("Btn_T3").className = "tabcss";
				document.getElementById("Btn_T4").className = "tabcss";
			}
			else if(tab==2) 
			{
				document.getElementById("tab1").style.display = "none";
				document.getElementById("tab2").style.display = "block";
				document.getElementById("tab3").style.display = "none";
				document.getElementById("tab4").style.display = "none";
				document.getElementById("Btn_T1").className = "tabcss";
				document.getElementById("Btn_T2").className = "tabcss_active";
				document.getElementById("Btn_T3").className = "tabcss";
				document.getElementById("Btn_T4").className = "tabcss";
			}
			else if(tab==3) 
			{
				document.getElementById("tab1").style.display = "none";
				document.getElementById("tab2").style.display = "none";
				document.getElementById("tab3").style.display = "block";
				document.getElementById("tab4").style.display = "none";
				document.getElementById("Btn_T1").className = "tabcss";
				document.getElementById("Btn_T2").className = "tabcss";
				document.getElementById("Btn_T3").className = "tabcss_active";
				document.getElementById("Btn_T4").className = "tabcss";
			}
			else if(tab==4) 
			{
				document.getElementById("tab1").style.display = "none";
				document.getElementById("tab2").style.display = "none";
				document.getElementById("tab3").style.display = "none";
				document.getElementById("tab4").style.display = "block";
				document.getElementById("Btn_T1").className = "tabcss";
				document.getElementById("Btn_T2").className = "tabcss";
				document.getElementById("Btn_T3").className = "tabcss";
				document.getElementById("Btn_T4").className = "tabcss_active";
			}
			return false;
        }
    </script>
	<script>
		//PWA
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
            var LBSWC005 = document.getElementById("LBSWC005").innerHTML;
            var LBSWC002 = document.getElementById("LBSWC002").innerHTML;
			var LBDTL001 = document.getElementById("LBDTL001").innerHTML;
			
			
            var TXTDTL002 = document.getElementById("TXTDTL002").value;
			var TXTDTL023 = document.getElementById("TXTDTL023").value;
			var TXTDTL025 = document.getElementById("TXTDTL025").value;
			var TXTDTL026 = document.getElementById("TXTDTL026").value;
			var DDLDTL027 = document.getElementById("DDLDTL027").value;
			
			
			var url_string = location.href;
			var url = new URL(url_string);
			
			
            var txn = db.transaction("SWCDT006", "readwrite");
            var store = txn.objectStore("SWCDT006");
			
            var data = { 
				SWCNO: url.searchParams.get("SWCNO"),
				LBSWC005: LBSWC005,
				LBSWC002: LBSWC002,
				LBDTL001: LBDTL001,
				
				TXTDTL002: TXTDTL002,
				TXTDTL023: TXTDTL023,
				TXTDTL025: TXTDTL025,
				TXTDTL026: TXTDTL026,
				DDLDTL027: DDLDTL027
				
			};
			

            var req = store.put(data);
			//儲存圖片列表
			var table = document.getElementById("Table_Temp1");
			
			var txn = db.transaction("SWCDT006_1", "readwrite");
			var store = txn.objectStore("SWCDT006_1");
			var LBDTL001 = document.getElementById("LBDTL001").innerHTML;
			
			var req = store.delete(IDBKeyRange.bound([url.searchParams.get("SWCNO"),document.getElementById("LBDTL001").innerHTML,1],[url.searchParams.get("SWCNO"),document.getElementById("LBDTL001").innerHTML,100]), 'next');
			
			for(var i = 1; i < table.rows.length; i++)
			{
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
			
			
			//儲存設施
            var txn = db.transaction("SWCDT006_2", "readwrite");
            var store = txn.objectStore("SWCDT006_2");
			
            if(document.getElementById("SDIList"))
			{
				var table = document.getElementById("SDIList");
				
				var CHK001 = ""; var CHK001D = "";
				var TXTCHK002 = ""; var TXTCHK002_1 = "";
				var CHK004 = ""; var CHK004_1 = "";
				var CHK004D = ""; var CHK005 = ""; var CHK005_1 = "";
				var CHK006 = ""; var CHK006_1 = "";
				var TXTCHK007 = ""; var TXTCHK007_1 = "";
				var DDLPASS = "";
				var j = 1;
				
				for(var i = 1; i < table.rows.length; i++)
				{
					if(i%2==0)
					{
						if(document.getElementById("SDIList_CHK001_"+(i-1))) CHK001 = document.getElementById("SDIList_CHK001_"+(i-1)).value;
						else CHK001 = "";
						
						if(document.getElementById("SDIList_CHK001D_"+(i-1))) CHK001D = document.getElementById("SDIList_CHK001D_"+(i-1)).value;
						else CHK001D = "";
						
						if(document.getElementById("SDIList_TXTCHK002_"+(i-1))) TXTCHK002 = document.getElementById("SDIList_TXTCHK002_"+(i-1)).value;
						else TXTCHK002 = "";
						
						if(document.getElementById("SDIList_TXTCHK002_1_"+(i-1))) TXTCHK002_1 = document.getElementById("SDIList_TXTCHK002_1_"+(i-1)).value;
						else TXTCHK002_1 = "";
						
						if(document.getElementById("SDIList_CHK004_"+(i-1))) CHK004 = document.getElementById("SDIList_CHK004_"+(i-1)).value;
						else CHK004 = "";
						
						if(document.getElementById("SDIList_CHK004_1_"+(i-1))) CHK004_1 = document.getElementById("SDIList_CHK004_1_"+(i-1)).value;
						else CHK004_1 = "";
						
						if(document.getElementById("SDIList_CHK004D_"+(i-1))) CHK004D = document.getElementById("SDIList_CHK004D_"+(i-1)).value;
						else CHK004D = "";
						
						if(document.getElementById("SDIList_CHK005_"+(i-1))) CHK005 = document.getElementById("SDIList_CHK005_"+(i-1)).value;
						else CHK005 = "";
						
						if(document.getElementById("SDIList_CHK005_1_"+(i-1))) CHK005_1 = document.getElementById("SDIList_CHK005_1_"+(i-1)).value;
						else CHK005_1 = "";
						
						if(document.getElementById("SDIList_CHK006_"+(i-1))) CHK006 = document.getElementById("SDIList_CHK006_"+(i-1)).value;
						else CHK006 = "";
						
						if(document.getElementById("SDIList_CHK006_1_"+(i-1))) CHK006_1 = document.getElementById("SDIList_CHK006_1_"+(i-1)).value;
						else CHK006_1 = "";
						
						if(document.getElementById("SDIList_TXTCHK007_"+(i-1))) TXTCHK007 = document.getElementById("SDIList_TXTCHK007_"+(i-1)).value;
						else TXTCHK007 = "";
						
						if(document.getElementById("SDIList_TXTCHK007_1_"+(i-1))) TXTCHK007_1 = document.getElementById("SDIList_TXTCHK007_1_"+(i-1)).value;
						else TXTCHK007_1 = "";
						
						if(document.getElementById("SDIList_DDLPASS_"+(i-1))) DDLPASS = document.getElementById("SDIList_DDLPASS_"+(i-1)).value;
						else DDLPASS = "";
						
						var data = { 
							SWCNO: url.searchParams.get("SWCNO"),
							DTLNO: LBDTL001,
							NO: j,
							CHK001: CHK001,
							CHK001D: CHK001D,
							TXTCHK002: TXTCHK002,
							TXTCHK002_1: TXTCHK002_1,
							CHK004: CHK004,
							CHK004_1: CHK004_1,
							CHK004D: CHK004D,
							CHK005: CHK005,
							CHK005_1: CHK005_1,
							CHK006: CHK006,
							CHK006_1: CHK006_1,
							TXTCHK007: TXTCHK007,
							TXTCHK007_1: TXTCHK007_1,
							DDLPASS: DDLPASS
						};
						var req = store.put(data);
						j++;
					}
				}
			}
			

            req.onsuccess = function (e) {
				alert("已存檔");
            };

            req.onerror = function () {
			
            };
        }
		
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
			//get data			
            var txn = db.transaction("SWCDT006", "readwrite");
            var store = txn.objectStore("SWCDT006");
			
			
			var url_string = location.href;
			var url = new URL(url_string);
			
            var req = store.get(url.searchParams.get("SWCNO"));
			
            req.onsuccess = function (e) {
				var data1 = e.target.result;
				
				$("[id=TXTDTL002]").val(data1.TXTDTL002);
				$("[id=TXTDTL023]").val(data1.TXTDTL023);
				$("[id=TXTDTL025]").val(data1.TXTDTL025);
				$("[id=TXTDTL026]").val(data1.TXTDTL026);
				$("[id=DDLDTL027]").val(data1.DDLDTL027);
				LoadImageList();
				LoadFacilityList();
				alert("讀取成功");
			};
		};
		
		//讀取圖片列表
		function LoadImageList(){
			var txn = db.transaction("SWCDT006_1", "readwrite");
            var store = txn.objectStore("SWCDT006_1");
			
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
            var txn = db.transaction("SWCDT006", "readwrite");
            var store = txn.objectStore("SWCDT006");
			
			var url_string = location.href;
			var url = new URL(url_string);

            var req = store.delete(url.searchParams.get("SWCNO"));
			
			var txn = db.transaction("SWCDT006_1", "readwrite");
            var store = txn.objectStore("SWCDT006_1");

			var req = store.delete(IDBKeyRange.bound([url.searchParams.get("SWCNO"),document.getElementById("LBDTL001").innerHTML,1],[url.searchParams.get("SWCNO"),document.getElementById("LBDTL001").innerHTML,100]), 'next');
			
			var txn = db.transaction("SWCDT006_2", "readwrite");
            var store = txn.objectStore("SWCDT006_2");

			var req = store.delete(IDBKeyRange.bound([url.searchParams.get("SWCNO"),document.getElementById("LBDTL001").innerHTML,1],[url.searchParams.get("SWCNO"),document.getElementById("LBDTL001").innerHTML,100]), 'next');
			
			
			req.onsuccess = function (e) {
				var req = store.openCursor();
				req.onsuccess = function (e) {
					var cursor = e.target.result;
					if (!cursor)
					{
						alert("資料已上傳至系統");
						window.open('', '_self', '');
						window.close();
					}
					//SWCNO 1 2 3 4 5 ...
					//ID 1 3 5 7 9 ...
					store.delete([url.searchParams.get("SWCNO"),cursor.value.NO]);

					cursor.continue();
				};
			};

            req.onerror = function () {
			
            };
        };
		
		//讀取設施
		function LoadFacilityList(){
            var txn = db.transaction("SWCDT006_2", "readonly");
            var store = txn.objectStore("SWCDT006_2");
			
			var url_string = location.href;
			var url = new URL(url_string);
			
			var req = store.openCursor(IDBKeyRange.bound([url.searchParams.get("SWCNO"),document.getElementById("LBDTL001").innerHTML,1],[url.searchParams.get("SWCNO"),document.getElementById("LBDTL001").innerHTML,100]), 'next');
			
			req.onsuccess = function (e) {
				var cursor = e.target.result;
				if (!cursor)
				{
					return;
				}
				//SWCNO 1 2 3 4 5 ...
				//ID 1 3 5 7 9 ...
				if(document.getElementById("SDIList_CHK001_"+(2*(cursor.value.NO)-1))) document.getElementById("SDIList_CHK001_"+(2*(cursor.value.NO)-1)).value = cursor.value.CHK001;
				
				if(document.getElementById("SDIList_CHK001D_"+(2*(cursor.value.NO)-1))) document.getElementById("SDIList_CHK001D_"+(2*(cursor.value.NO)-1)).value = cursor.value.CHK001D;
				
				if(document.getElementById("SDIList_TXTCHK002_"+(2*(cursor.value.NO)-1))) document.getElementById("SDIList_TXTCHK002_"+(2*(cursor.value.NO)-1)).value = cursor.value.TXTCHK002;
				
				if(document.getElementById("SDIList_TXTCHK002_1_"+(2*(cursor.value.NO)-1))) document.getElementById("SDIList_TXTCHK002_1_"+(2*(cursor.value.NO)-1)).value = cursor.value.TXTCHK002_1;
				
				if(document.getElementById("SDIList_CHK004_"+(2*(cursor.value.NO)-1))) document.getElementById("SDIList_CHK004_"+(2*(cursor.value.NO)-1)).value = cursor.value.CHK004;
				
				if(document.getElementById("SDIList_CHK004_1_"+(2*(cursor.value.NO)-1))) document.getElementById("SDIList_CHK004_1_"+(2*(cursor.value.NO)-1)).value = cursor.value.CHK004_1;
				
				if(document.getElementById("SDIList_CHK004D_"+(2*(cursor.value.NO)-1))) document.getElementById("SDIList_CHK004D_"+(2*(cursor.value.NO)-1)).value = cursor.value.CHK004D;
				
				if(document.getElementById("SDIList_CHK005_"+(2*(cursor.value.NO)-1))) document.getElementById("SDIList_CHK005_"+(2*(cursor.value.NO)-1)).value = cursor.value.CHK005;
				
				if(document.getElementById("SDIList_CHK005_1_"+(2*(cursor.value.NO)-1))) document.getElementById("SDIList_CHK005_1_"+(2*(cursor.value.NO)-1)).value = cursor.value.CHK005_1;
				
				if(document.getElementById("SDIList_CHK006_"+(2*(cursor.value.NO)-1))) document.getElementById("SDIList_CHK006_"+(2*(cursor.value.NO)-1)).value = cursor.value.CHK006;
				
				if(document.getElementById("SDIList_CHK006_1_"+(2*(cursor.value.NO)-1))) document.getElementById("SDIList_CHK006_1_"+(2*(cursor.value.NO)-1)).value = cursor.value.CHK006_1;
				
				if(document.getElementById("SDIList_TXTCHK007_"+(2*(cursor.value.NO)-1))) document.getElementById("SDIList_TXTCHK007_"+(2*(cursor.value.NO)-1)).value = cursor.value.TXTCHK007;
				
				if(document.getElementById("SDIList_TXTCHK007_1_"+(2*(cursor.value.NO)-1))) document.getElementById("SDIList_TXTCHK007_1_"+(2*(cursor.value.NO)-1)).value = cursor.value.TXTCHK007_1;
				
				if(document.getElementById("SDIList_DDLPASS_"+(2*(cursor.value.NO)-1))) document.getElementById("SDIList_DDLPASS_"+(2*(cursor.value.NO)-1)).value = cursor.value.DDLPASS;

				cursor.continue();
			};
			
            req.onerror = function () {
			
            };
		}
	</script>
    <style type="text/css">
        .auto-style3 {
            font-size: xx-large;
            color: #FF3300;
        }

        .auto-style4 {
            font-size: xx-large;
            color: #0011FF;
        }
    </style>

    <style type="text/css">
        .auto-style1 {
            height: 37px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <%--<div>--%>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"/>

    <div class="wrap-s">
        <div class="header_PWA">
		  <img class="logo" src="../../img/logo2.svg"/>
            <span>水土保持完工檢查紀錄表</span>
			<asp:ImageButton runat="server" class="download" src="../../img/PWA_icon-08.svg" onclientclick="getValue(''); return false;"/>
		</div>
        <div class="tabbg">
            <asp:Button ID="Btn_T1" runat="server" Text="案件資訊" OnClientClick="return chk_Panel(1);" UseSubmitBehavior="false" class="tabcss_active"/>
            <asp:Button ID="Btn_T2" runat="server" Text="檢查項目" OnClientClick="return chk_Panel(2);" UseSubmitBehavior="false" class="tabcss"/>
            <asp:Button ID="Btn_T3" runat="server" Text="線上簽名" OnClientClick="return chk_Panel(3);" UseSubmitBehavior="false" class="tabcss"/>
			<asp:Button ID="Btn_T4" runat="server" Text="水保設施" OnClientClick="return chk_Panel(4);" UseSubmitBehavior="false" class="tabcss"/>
        </div>
        <div class="b-blue none">
                <span><asp:Literal ID="TextUserName" runat="server">大雷包，您好</asp:Literal></span>
        </div>



        <div class="content-t PWA_wrap">
            <div class="checkRecord pwa_font pwah1">
				<asp:Button ID="GoOnline" runat="server" Text="讀取上次資料" UseSubmitBehavior="false" onclientclick="return getValue(this);" /><br/>

                <%--tab--%>
                 <div class="tab_content">
			    <!-- TAB1 打包區塊 end -->
                   <!--<input id="tab1" type="radio" name="tab" checked="checked"/-->
                   <!--<label for="tab1" class="fixed1">案件資訊</label-->
				<asp:Panel ID="tab1" runat="server">
                <div class="tab_main"> <!--class="tab_content"-->
				<table class="swcchg-outA tdpadding">
                <tr><th style="width:16%;">完工檢查表編號</th>
                    <td><asp:Label ID="LBDTL001" runat="server"/>
                        <asp:Label ID="LBSWC000" runat="server" Visible="false"/>
                        <asp:Label ID="LBSWC025" runat="server" Visible="false"/>
						<asp:TextBox ID="TXTIMAGE" runat="server" style="display:none;"/>
					</td></tr>
                <tr><th>水保局編號</th>
						<td><asp:Label ID="LBSWC002" runat="server"/></td></tr>
                <tr><th>檢查日期<span style="color: red;font-family:cursive;">＊</span></th>
                    <td><asp:TextBox ID="TXTDTL002" runat="server" width="150px" autocomplete="off"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTDTL002_CalendarExtender" runat="server" TargetControlID="TXTDTL002" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                <tr><th>檢查單位</th>
                    <td><asp:Label ID="TXTDTL004" runat="server" /></td></tr>
                </table>
                <div class="flex">
					<input id="btn_next1" type="button" value="下一頁" onclick="next_page()" runat="server" class="flex1" />
				</div>
                </div>
				</asp:Panel>
                <!-- TAB1 打包區塊 end -->

                <!-- TAB2 打包區塊 star -->
                <asp:Panel ID="tab2" runat="server" style="display:none;">
                <div class="tab_main"> <!--class="tab_content"-->
                    <ul class="nav nav-tabs" role="tablist">
                      <li class="nav-item">
                        <a id="B1" class="nav-link active" href="#A1a" role="tab" data-toggle="tab">1</a>
                      </li>
                      <li class="nav-item">
                        <a id="B2" class="nav-link" href="#A2" role="tab" data-toggle="tab">2</a>
                      </li>
                      <li class="nav-item">
                        <a id="B3" class="nav-link" href="#A3a" role="tab" data-toggle="tab">3</a>
                      </li>
                      <li class="nav-item">
                        <a id="B4" class="nav-link" href="#A4" role="tab" data-toggle="tab">4</a>
                      </li>
                      <%--<li class="nav-item">
                        <a id="B5" class="nav-link" href="#A5" role="tab" data-toggle="tab">5</a>
                      </li>--%>
                    </ul>

                    <div class="tab-content">
                     <div role="tabpanel" class="tab-pane fade in active" id="A1a">
                         <div class="PWA_ultitle">水土保持書件</div>
                         <table class="swcchg-outA tdpadding">
                         <tr>
                             <th style="width:16%; vertical-align:middle;">計畫名稱</th>
                             <td style="line-height:1.5;"><asp:Label ID="LBSWC005" runat="server"/>
                                 （<asp:Label ID="LBSWC007" runat="server"/>）</td></tr>
                         <tr><th class="auto-style1">核定日期文號</th>
                             <td class="auto-style1">臺北市政府
                                 <asp:Label ID="LBSWC038" runat="server"/>
                                 <asp:Label ID="LBSWC039" runat="server"/>
                                 函</td></tr>
                         <tr><th>水土保持施工許可證日期文號</th> 
                             <td>臺北市政府
                                 <asp:Label ID="LBSWC043" runat="server"/>
                                 <asp:Label ID="LBSWC044" runat="server"/>
                                 函</td></tr>
                         <tr><th>開工日期</th>
                             <td><asp:Label ID="LBSWC051" runat="server"/></td></tr>
                         <tr><th>核定完工日期</th>
                             <td><asp:Label ID="LBSWC052" runat="server" width="120px"></asp:Label></td></tr>
                         <tr><th>申報完工日期</th>
                             <td><asp:Label ID="LBSWC058" runat="server" width="120px"></asp:Label></td></tr>
                        </table>
                     </div>
                     <div role="tabpanel" class="tab-pane fade" id="A2">
                         <div class="PWA_ultitle">水土保持義務人</div>
                         <table class="swcchg-outA tdpadding">
                         <tr>
                              <th style="width:16%;">姓名或名稱</th>
                              <td><asp:Label ID="LBSWC013" runat="server"/></td></tr>
                          <tr><th>身分證或營利事業統一編號</th>
                              <td><asp:Label ID="LBSWC013ID" runat="server"/></td></tr>
                          <tr><th>住居所或營業所</th>
                              <td><asp:Label ID="LBSWC014" runat="server"/></td></tr>
                         </table>
                     </div>
                     <div role="tabpanel" class="tab-pane fade" id="A3a">
                         <div class="PWA_ultitle">承辦監造技師</div>
                         <table class="swcchg-outA tdpadding">
                         <tr>
                              <th style="width:16%;">姓名</th>
                              <td><asp:Label ID="LBSWC021" runat="server"/></td></tr>
                          <tr><th>執業機構名稱</th>
                              <td><asp:Label ID="LBSWC021Name" runat="server"/></td></tr>
                          <tr><th>執業執照字號</th>
                              <td><asp:Label ID="LBSWC021OrgIssNo" runat="server"/></td></tr>
                          <tr><th>營利事業統一編號</th>
                              <td><asp:Label ID="LBSWC021OrgGUINo" runat="server"/></td></tr>
                          <tr><th>電話</th>
                              <td><asp:Label ID="LBSWC021OrgTel" runat="server"/></td></tr>
                          </table>
                     </div>
                    <div role="tabpanel" class="tab-pane fade" id="A4">
                         <table class="swcchg-outA tdpadding">
                         <tr><th colspan="14">實施地點土地標示</th>
                              <td><asp:TextBox ID="TXTDTL023" runat="server" width="100%" onkeyup="textcount(this,'TXTDTL023_count','200');" MaxLength="200" />
                                  <asp:Label ID="TXTDTL023_count" runat="server" Text="(0/200)" ForeColor="Red" /></td></tr>
                          <tr><th colspan="14" style="border-bottom: none;border-top:none;">
                                  一、完工抽驗項目 <%--asp:HyperLink ID="NewUser" runat="server" Text="excel範本下載" NavigateUrl="../sysFile/完工抽驗項目.xlsx" /--%></th>
                                  <td><asp:FileUpload ID="TXTDTL024_fileupload" runat="server" Visible="false" />
                                  <asp:Button ID="TXTDTL024_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL024_fileuploadok_Click" Visible="false" />
                                  <asp:TextBox ID="TXTDTL024" runat="server" Width="70px" Visible="false"/>
                                  <asp:Button ID="TXTDTL024_fileclean" runat="server" CssClass="wordttb" Text="X" OnClientClick="return confirm('檔案刪除後無法復原，請確認真的要刪除檔案!!!')" OnClick="TXTDTL024_fileclean_Click" Visible="false" /><br/>
                                  <asp:HyperLink ID="Link024" runat="server" CssClass="word" Target="_blank"></asp:HyperLink>
                                  <%--span style="color:red;">※ 上傳格式限定為excel，檔案大小請於50mb以內</span--%></th>
								  </td></tr>
                          <tr><th colspan="14">二、實施與計畫或規定不符之限期改正期限</th>
                                  <td><asp:TextBox ID="TXTDTL025" runat="server" width="100%" Height="120px" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL025_count','300');" />
                                  <asp:Label ID="TXTDTL025_count" runat="server" Text="(0/300)" ForeColor="Red" /></td></tr>
                          <tr><th colspan="14">三、其他注意事項</th>
                                 <td><asp:TextBox ID="TXTDTL026" runat="server" width="100%" Height="120px" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL026_count','300');" />
                                  <asp:Label ID="TXTDTL026_count" runat="server" Text="(0/300)" ForeColor="Red" /></td></tr>
                          <tr><th colspan="14">四、檢查結果</th>
                                 <td> <asp:DropDownList ID="DDLDTL027" runat="server">
                                      <asp:ListItem Text="" Value=""></asp:ListItem>
                                      <asp:ListItem Text="已達完工標準" Value="已達完工標準"></asp:ListItem>
				          	          <asp:ListItem Text="未達完工標準" Value="未達完工標準"></asp:ListItem>
                                  </asp:DropDownList>
				              <asp:Panel runat="server" Visible="false">
                                  <span style="color:red;">※ 若竣工圖說有更動，請上傳新版本竣工圖說</span>
                              
                              水土保持竣工書圖及照片：<br/>
                              <asp:TextBox ID="TXTONA003" runat="server" Width="70px" Visible="False" />
                              <asp:FileUpload ID="TXTONA003_fileupload" runat="server" />
                              <asp:Button ID="TXTONA003_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA003_fileuploadok_Click" />
                              <asp:Button ID="TXTONA003_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA003_fileuploaddel_Click" />
                              <br/><asp:HyperLink ID="Link003" runat="server" Target="_blank" />
                              <br/><span style="color:red;">※ 上傳格式限定為pdf、odt或doc檔案，大小請於50mb以內</span><br/>
                              <asp:TextBox ID="TXTONA008" runat="server" Width="70px" Visible="False" />
                              <asp:FileUpload ID="TXTONA008_fileupload" runat="server" />
                              <asp:Button ID="TXTONA008_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTONA008_fileuploadok_Click" />
                              <asp:Button ID="TXTONA008_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTONA008_fileuploaddel_Click" />
                              <br/><asp:HyperLink ID="Link008" runat="server" Target="_blank" />
                              <br/><span style="color:red;">※ 上傳格式限定為CAD，檔案大小請於50mb以內</span><br/>
				          	  </asp:Panel>
                              </td></tr>
                          </table>
                     </div>
                    </div>
                
                </div>
                </asp:Panel>
                <!-- TAB2 打包區塊 end -->

                <!-- TAB3 打包區塊 end -->
                <asp:Panel ID="tab3" runat="server" style="display:none;"><%--1--%>
                <div class="tab_main"> <!--class="tab_content"-->
                <table class="mbborder" style="border:none !important;">
                    <tr>
                        <td><b>五、相關單位及人員簽名</b><br/><br/>
						  <asp:Panel runat="server" Visible="false">
                            <span class="box1"><asp:TextBox ID="TXTDTL028" runat="server" Width="100%" Height="250px" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL028_count','800');" />
                            <asp:Label ID="TXTDTL028_count" runat="server" Text="(0/800)" ForeColor="Red" /><br />
							<asp:Button ID="TXTDTL029_changepanel" runat="server" Text="切換簽名方式" /></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <span class="box2">
                            <asp:Image ID="TXTDTL029_img" runat="server" CssClass="imgUpload" Visible="false" /><br />
                            <asp:HyperLink ID="HyperLink029" runat="server" CssClass="imgUpload imgTP" Target="_blank"></asp:HyperLink>
                            <br />
                            <span style="color: red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br />
                            <br />
                            <asp:TextBox ID="TXTDTL029" runat="server" Width="70px" Visible="false" />
                            <asp:FileUpload ID="TXTDTL029_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL029_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL029_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL029_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL029_fileuploaddel_Click" />
                          </asp:Panel>
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
                                style="cursor:pointer;background:white;border:1px #CCC solid;padding-left: 0;  padding-right: 0;margin-left: auto;margin-right: auto;display: block;">
                            </canvas>
                            <br />
                            <asp:HiddenField ID="hfImageData" runat="server" />
							<div class="center">
								<!--<asp:Button ID="TXTDTL02902_Clear" runat="server" Text="重新簽名" OnClientClick="clearCanvas()" />&nbsp;&nbsp;&nbsp;
								<asp:Button ID="TXTDTL02902_Signsture" runat="server" Text="簽名" OnClick="TXTDTL02902_Signsture_Click" UseSubmitBehavior="false" /><br />-->
								<asp:Button ID="btn1" runat="server" UseSubmitBehavior="false" onclientclick="return clearCanvas();" Text="重新簽名" class="retunsign" />
								<asp:Button ID="btn2" runat="server" UseSubmitBehavior="false" Text="確認簽名並加入清單" CssClass="signok" onclientclick="InsertImageList(); return false;" />
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
                        </td>
                    </tr>
                 </table>
                </div>
                </asp:Panel>
                <!-- TAB3 打包區塊 end -->
				
				<!-- TAB4 打包區塊 start -->
                  <!--<input id="tab4" type="radio" name="tab"/-->
                  <!--<label for="tab4" class="fixed3">水保設施</label>-->
                  <asp:Panel ID="tab4" runat="server" style="display:none;">
				  <div class="tab_main">  <!--class="tab_content"-->
				  
					<div class="OFsegG">
                <div style="float:left"> <img src="../images/btn/btn006-07.png" alt="" /></div>
                <div class="lab">
                   <div class="labcolor1" style="color:#4e7a10;"><div class="icon1" style="background:#f0ffd9;"></div>原核定</div>
                   <div class="labcolor2" style="color:#000;"><div class="icon2" style="background:#fdfff0"></div>現場量測</div>
                </div>
                <asp:Label ID="GVMSG" runat="server" Text="查無資料" Visible="true" class="nodata"/>
				<div style="clear:both;"></div>
                   <div class="detailsGrid">
                      <h2 class="SWCfl openh2">水保設施項目<img src="../images/btn/btn-close.png" alt=""/></h2>

                                <asp:GridView ID="SDIList" runat="server" CssClass="OFcheckG AutoNewLine swcfacility_mb" AutoGenerateColumns="False" EmptyDataText="查無資料"
                                    OnRowCommand="SDIList_RowCommand" OnDataBound="SDIList_DataBound">
                                    <Columns>
                                        <asp:BoundField DataField="SDIFD017" HeaderText="技師報備<br>施工完成" HeaderStyle-Width="5%" HtmlEncode="false" />
                                        <asp:BoundField DataField="SDIFD003" HeaderText="水土保持設施類別" HeaderStyle-Width="5%" />
                                        <asp:BoundField DataField="SDIFD004" HeaderText="設施名稱<br>（位置或編號）" HeaderStyle-Width="980px" HtmlEncode="false" />
                                        <asp:BoundField DataField="SDIFD005" HeaderText="設施型式" HeaderStyle-Width="10%" />
                <asp:TemplateField HeaderText="數" HeaderStyle-Width="10px">
                    <ItemTemplate>
                        <asp:Label ID="SDILB006" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDIFD006"))) %>' Visible="true"></asp:Label>
                        <asp:Label ID="SDILB006D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDIFD006D"))) %>' Visible="true"></asp:Label>
                        <asp:TextBox ID="CHK001" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001"))) %>' style="width:40px; text-align:center;" Visible="true" />
                        <asp:Label ID="A1" runat="server" Text='~' Visible="true"></asp:Label>
                        <asp:TextBox ID="CHK001_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001_1"))) %>' style="width:40px; text-align:center;" Visible="true" />
                        <asp:TextBox ID="CHK001D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001D"))) %>' MaxLength="20" style="width:40px; text-align:center;" Visible="true" Enabled="false" />
                        <asp:TextBox ID="RCH001" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001"))) %>' style="width:40px; text-align:center;" Visible="false" />
                        <asp:TextBox ID="RCH001_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001_1"))) %>' style="width:40px; text-align:center;" Visible="false" />
                        <asp:TextBox ID="RCH001D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001D"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SDIFD007" HeaderText="量" HeaderStyle-Width="10px" />
                <asp:TemplateField HeaderText="數量差異百分比" HeaderStyle-Width="400px">
                    <ItemTemplate>
                        <asp:Label ID="LBCHK002" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK002"))) %>' Visible="true"></asp:Label>
                        <asp:TextBox ID="TXTCHK002" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK002"))) %>' style="width:40px; text-align:center;" Visible="true" />
                        <asp:Label ID="LBCHK002pers" runat="server" Text='％' Visible="true"></asp:Label>
                        <asp:Label ID="LBCHK002_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK002_1"))) %>' Visible="true"></asp:Label>
                        <asp:TextBox ID="TXTCHK002_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK002_1"))) %>' MaxLength="20" style="width:40px; text-align:center;" Visible="true" />
						<asp:Label ID="LBCHK002pers_1" runat="server" Text='％' Visible="true"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SDIFD008" HeaderText="檢核項目" HeaderStyle-Width="580px" />
                <asp:TemplateField HeaderText="" HeaderStyle-Width="60px">
                    <ItemTemplate>
                        <asp:Label ID="ITNONE03" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDIFD012"))) %>' Visible="true"></asp:Label>
                        <asp:TextBox ID="CHK004" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004"))) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false" />
                        <asp:Label ID="A2" runat="server" Text='~' Visible="true"></asp:Label>
						<asp:TextBox ID="RCH004" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                        <asp:TextBox ID="CHK004_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004_1"))) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false"/>
						<asp:TextBox ID="RCH004_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004_1"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="尺寸" HeaderStyle-Width="260px">
                    <ItemTemplate>
                        <asp:Label ID="LabelX1" runat="server" Text="×" Visible="true"></asp:Label>
                        <asp:Label ID="ITNONE04" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDIFD013"))) %>' Visible="true"></asp:Label>
                        <asp:Label ID="LB004D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004D"))) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false" />
                        <asp:TextBox ID="CHK004D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004D"))) %>' MaxLength="100" style="width:150px; text-align:center;" Visible="true" Enabled="false" />
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
                <asp:BoundField DataField="SDIFD015" HeaderText="" HeaderStyle-Width="0.5%" />
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
                <asp:TemplateField HeaderText="檢查結果" HeaderStyle-Width="6%">
                    <ItemTemplate>
                        <asp:DropDownList ID="DDLPASS" runat="server" SelectedValue='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK010"))) %>' Visible="false"><asp:ListItem></asp:ListItem><asp:ListItem>通過</asp:ListItem><asp:ListItem>未通過</asp:ListItem><asp:ListItem>隱蔽無法量測</asp:ListItem><asp:ListItem>已由永久設施取代</asp:ListItem></asp:DropDownList>
                        <asp:TextBox ID="RCH10" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK010"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>

                                        <asp:TemplateField Visible="true" >
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="MODIFYDATA" CommandArgument="<%#Container.DataItemIndex %>" CommandName="Modify" Text="修改" Visible="false" />
                                                <asp:HiddenField ID="HDSDI001" runat="server" Value='<%# Eval("SDIFD001") %>' />
                                                <asp:HiddenField ID="HDSDINI" runat="server" Value='<%# Container.DataItemIndex %>' />
                                                <asp:HiddenField ID="HDSDI011" runat="server" Value='<%# Eval("SDIFD011") %>' />
												<asp:HiddenField ID="HDSDI008" runat="server" Value='<%# Eval("SDICHK008") %>' />
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
				  
				  </div>
                </asp:Panel>
				<!-- TAB4 打包區塊 end -->




                
                
                <br/><br/>
                
                <br/>
                <table class="checkRecord-fileUpload none">
                <tr><td colspan="2">
                    <asp:Label ID="Label1" runat="server" CssClass="redn"/></td></tr>
                </table>
                <div class="inbox" style="display:none;">
                  <div class="leftimg">
                   <div class="imgtxt">現場相片一</div>
                    <div>
                      <div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL030" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL030_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL030_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL030_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL030_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL030_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL030_img" runat="server" CssClass="imgUpload" />
                        <br/>
                        <asp:TextBox ID="TXTDTL031" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/>
                        </div>
                   </div>
                    <div class="rightimg">
                     <div class="imgtxt">現場相片二</div>
                   <div>        
                     <div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL032" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL032_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL032_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL032_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL032_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL032_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL032_img" runat="server" CssClass="imgUpload" />
                        <br/>
                        <asp:TextBox ID="TXTDTL033" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/>
                        </div>
                     </div>
                  </div>

                 <div class="inbox" style="display:none;">
                  <div class="leftimg">
                     <div class="imgtxt">現場相片三</div>
                      <div>
                        <div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL034" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL034_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL034_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL034_fileuploadok_Click"/>
                            <asp:Button ID="TXTDTL034_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL034_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL034_img" runat="server" CssClass="imgUpload" />
                        <br/>
                        <asp:TextBox ID="TXTDTL035" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/>
                      </div>
                </div>


                 <div class="rightimg">
                   <div class="imgtxt">現場相片四</div>
                     <div>
                     <div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL036" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL036_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL036_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL036_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL036_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL036_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL036_img" runat="server" CssClass="imgUpload" />
                        <br/>
                        <asp:TextBox ID="TXTDTL037" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/>
                         </div>
                      </div>
                  </div>

                  <div class="inbox" style="display:none;">
                   <div class="leftimg">
                    <div class="imgtxt">現場相片五</div>
                    <div>
                    <div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL038" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL038_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL038_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL038_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL038_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL038_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL038_img" runat="server" CssClass="imgUpload" />
                        <br/>
                        <asp:TextBox ID="TXTDTL039" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/>
                        </div>
                   </div>

                    <div class="rightimg">
                  <div class="imgtxt">現場相片六</div>
                    <div>
                     <div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL040" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL040_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL040_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL040_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL040_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL040_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL040_img" runat="server" CssClass="imgUpload" />
                        <br/>
                        <asp:TextBox ID="TXTDTL041" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/>
                        </div>
                     </div>
                  </div>





                <table class="checkRecord-fileUpload" style="display:none;">
                <tr><td colspan="2">
                    <asp:Label ID="LBSWC005a" runat="server" CssClass="redn"/></td></tr>
                
                <tr><td colspan="4">附件</td></tr>
                <tr><td colspan="4">附件檔案上傳：
                        <asp:TextBox ID="TXTDTL042" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTDTL042_fileupload" runat="server" />
                        <asp:Button ID="TXTDTL042_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL042_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL042_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請確認真的要刪除檔案!!!')" OnClick="TXTDTL042_fileuploaddel_Click" />
                        <br/><br/>
                        <span style="color:red;">檔案大小請小於 50 Mb，請上傳 pdf odt word 格式</span>
                        <br/><br/>
                        <asp:HyperLink ID="Link042" runat="server" Text ="其他附件檔案下載" Target="_blank" Visible="false" /></td></tr>
                    </table>
					
					<asp:Label ID="ReqCount" runat="server" Text="" style="display:none;" />
                <asp:Panel ID="SignList" runat="server"><br/><br/>

                <div><span style="background-color: #FFFF99; font-size: 16pt; font-weight: bold; margin-top:1em;">退補正歷程</span></div><br/>
            
                <asp:GridView ID="GVSignList" runat="server" DataSourceID="SqlDataSourceSign" CssClass="retirement bigW" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="TH001n" HeaderText="退文日期" SortExpression="SWC000" ItemStyle-Width="200px"/>
                        <asp:BoundField DataField="TH005n" HeaderText="改正期限" SortExpression="SWC002" ItemStyle-Width="190px"/>
                        <asp:BoundField DataField="THName" HeaderText="退文人員" SortExpression="SWC004" ItemStyle-Width="140px"/>
                        <asp:BoundField DataField="TH004" HeaderText="說明" SortExpression="SWC005"  ItemStyle-Width="350px"/>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceSign" runat="server" ConnectionString="<%$ ConnectionStrings:SWCConnStr %>" SelectCommand="" OnSelected="SqlDataSourceSign_Selected" />
                    
                </asp:Panel>
                 </div>
            </div>
        </div>
                
                <br/><br/><br/>
				
				<div class="PWAbottombtn">
					<asp:ImageButton runat="server" UseSubmitBehavior="false" src="../../img/PWA_icon-09.svg" onclientclick="setValue();"/>
					<asp:ImageButton ID="SaveCase" runat="server" UseSubmitBehavior="false" src="../../img/PWA_icon-10.svg" OnClientClick="return chkInput('');" OnClick="SaveCase_Click"/>
				</div>
                <!--<div class="form-btn none">
					<asp:Panel runat="server" Visible="false">
						<asp:Button ID="DataLock" runat="server" Text="確認送出" OnClientClick="return chkInput('DataLock');" OnClick="DataLock_Click" />&nbsp&nbsp
						<asp:Button ID="GoHomePage" runat="server" Text="返回編輯案件" OnClick="GoHomePage_Click" />
					</asp:Panel>
					<asp:Button ID="GoOffline" runat="server" Text="離線暫存" UseSubmitBehavior="false" onclientclick="setValue();" />
					<asp:Button ID="SaveCase1" runat="server" Text="上傳至系統" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" />&nbsp&nbsp
                </div>-->
                <div class="go-top1">TOP</div>
           

        <%--<div class="footer-s">
            <div class="footer-s-green"></div>
            <div class="footer-b-brown">
                <p> <span class="span1">臺北市政府工務局大地工程處</span>
                    <br/><span class="span2">110臺北市信義區松德路300號3樓 　(02)27593001   臺北市民當家熱線1999</span>
                    <br/><span class="span2">資料更新：2017.5.19　來訪人數：123456789 </span></p>
            </div>
        </div>--%>
        
        
                



            <div class="footer none">
                <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                           <span class="span2">110臺北市信義區松德路300號3樓 　服務專線(02)27591109   臺北市民當家熱線1999</span><br/>
                            <span class="span2">建議使用IE11(含)以上，Chrome或Firefox版本瀏覽器 資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                            <span class="span2">客服電話：02-27593001#3729 許先生 本系統由多維空間資訊有限公司開發維護 TEL：(02)27929328</span><br/>
							<span class="span2">※為維護系統服務品質，本平台訂於每周三凌晨AM 4:00-6:30 進行系統維護更新，更新期間偶有瞬斷情形，敬請使用者避開該時段使用。謝謝！</span></p>
            </div>
    




        <asp:Literal ID="error_msg" runat="server"></asp:Literal>
        
    <script type="text/javascript">
        if (document.getElementById("ReqCount").innerText == '0') { SignList.style.display = "none"; }
    </script>
    <script src="../../js/jquery-3.1.1.min.js"></script>
    <script src="../../js/boostrap.js"></script>
    <script src="../../js/inner.js?20220217"></script>

    
    </form>
</body>
</html>
