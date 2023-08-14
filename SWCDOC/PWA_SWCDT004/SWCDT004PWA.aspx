<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCDT004PWA.aspx.cs" Inherits="PWA_SWCDT004_SWCDT004PWA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>臺北市水土保持申請書件管理平台</title>
    <link rel="Shortcut Icon" type="image/x-icon" href="../../images/logo-s.ico">
    <meta name="keywords" content="臺北市水土保持申請書件管理平台, 書件管理平台">
    <meta name="description" content="臺北市水土保持申請書件管理平台">
    <meta name="author" content="dobubu">
    <meta name="copyright" content="© 2017 臺北市水土保持申請書件管理平台">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge, chrome=1">
    <meta name="viewport" content="width=device-width">
    <link rel="stylesheet" type="text/css" href="../../css/reset.css"/>
    <link rel="stylesheet" type="text/css" href="../../css/all.css"/>
    <link rel="stylesheet" type="text/css" href="../../css/iris.css"/>
    <link rel="stylesheet" type="text/css" href="../../css/all_PWA.css?202210240336"/>
    <link rel="stylesheet" type="text/css" href="../../css/all_PWA_2.css?202210240336"/>
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
            if (tab == 1) {
                document.getElementById("tab1").style.display = "block";
                document.getElementById("tab2").style.display = "none";
                document.getElementById("tab3").style.display = "none";
                document.getElementById("Btn_T1").className = "tabcss_active";
                document.getElementById("Btn_T2").className = "tabcss";
                document.getElementById("Btn_T3").className = "tabcss";
            }
            else if (tab == 2) {
                document.getElementById("tab1").style.display = "none";
                document.getElementById("tab2").style.display = "block";
                document.getElementById("tab3").style.display = "none";
                document.getElementById("Btn_T1").className = "tabcss";
                document.getElementById("Btn_T2").className = "tabcss_active";
                document.getElementById("Btn_T3").className = "tabcss";
            }
            else if (tab == 3) {
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
			var DDLDTL003 = document.getElementById("DDLDTL003").value;
			var DDLDTL085 = document.getElementById("DDLDTL085").value;
			var DDLDTL004 = document.getElementById("DDLDTL004").value;
			//5 6 7 8 . 9 10 11 12 . . . 73 74 75 76
			var CHKDTL005 = document.getElementById("CHKDTL005").checked;
			var DDLDTL006 = document.getElementById("DDLDTL006").value;
			var TXTDTL007 = document.getElementById("TXTDTL007").value;
			var TXTDTL008 = document.getElementById("TXTDTL008").value;
			
			var CHKDTL009 = document.getElementById("CHKDTL009").checked;
			var DDLDTL010 = document.getElementById("DDLDTL010").value;
			var TXTDTL011 = document.getElementById("TXTDTL011").value;
			var TXTDTL012 = document.getElementById("TXTDTL012").value;
			
			var CHKDTL013 = document.getElementById("CHKDTL013").checked;
			var DDLDTL014 = document.getElementById("DDLDTL014").value;
			var TXTDTL015 = document.getElementById("TXTDTL015").value;
			var TXTDTL016 = document.getElementById("TXTDTL016").value;
			
			var CHKDTL017 = document.getElementById("CHKDTL017").checked;
			var DDLDTL018 = document.getElementById("DDLDTL018").value;
			var TXTDTL019 = document.getElementById("TXTDTL019").value;
			var TXTDTL020 = document.getElementById("TXTDTL020").value;
			
			var CHKDTL021 = document.getElementById("CHKDTL021").checked;
			var DDLDTL022 = document.getElementById("DDLDTL022").value;
			var TXTDTL023 = document.getElementById("TXTDTL023").value;
			var TXTDTL024 = document.getElementById("TXTDTL024").value;
			
			var CHKDTL025 = document.getElementById("CHKDTL025").checked;
			var DDLDTL026 = document.getElementById("DDLDTL026").value;
			var TXTDTL027 = document.getElementById("TXTDTL027").value;
			var TXTDTL028 = document.getElementById("TXTDTL028").value;
			
			var CHKDTL029 = document.getElementById("CHKDTL029").checked;
			var DDLDTL030 = document.getElementById("DDLDTL030").value;
			var TXTDTL031 = document.getElementById("TXTDTL031").value;
			var TXTDTL032 = document.getElementById("TXTDTL032").value;
			
			var CHKDTL033 = document.getElementById("CHKDTL033").checked;
			var DDLDTL034 = document.getElementById("DDLDTL034").value;
			var TXTDTL035 = document.getElementById("TXTDTL035").value;
			var TXTDTL036 = document.getElementById("TXTDTL036").value;
			
			var CHKDTL037 = document.getElementById("CHKDTL037").checked;
			var DDLDTL038 = document.getElementById("DDLDTL038").value;
			var TXTDTL039 = document.getElementById("TXTDTL039").value;
			var TXTDTL040 = document.getElementById("TXTDTL040").value;
			
			var CHKDTL041 = document.getElementById("CHKDTL041").checked;
			var DDLDTL042 = document.getElementById("DDLDTL042").value;
			var TXTDTL043 = document.getElementById("TXTDTL043").value;
			var TXTDTL044 = document.getElementById("TXTDTL044").value;
			
			var CHKDTL045 = document.getElementById("CHKDTL045").checked;
			var DDLDTL046 = document.getElementById("DDLDTL046").value;
			var TXTDTL047 = document.getElementById("TXTDTL047").value;
			var TXTDTL048 = document.getElementById("TXTDTL048").value;
			
			var CHKDTL049 = document.getElementById("CHKDTL049").checked;
			var DDLDTL050 = document.getElementById("DDLDTL050").value;
			var TXTDTL051 = document.getElementById("TXTDTL051").value;
			var TXTDTL052 = document.getElementById("TXTDTL052").value;
			
			var CHKDTL053 = document.getElementById("CHKDTL053").checked;
			var DDLDTL054 = document.getElementById("DDLDTL054").value;
			var TXTDTL055 = document.getElementById("TXTDTL055").value;
			var TXTDTL056 = document.getElementById("TXTDTL056").value;
			
			var CHKDTL057 = document.getElementById("CHKDTL057").checked;
			var DDLDTL058 = document.getElementById("DDLDTL058").value;
			var TXTDTL059 = document.getElementById("TXTDTL059").value;
			var TXTDTL060 = document.getElementById("TXTDTL060").value;
			
			var CHKDTL061 = document.getElementById("CHKDTL061").checked;
			var DDLDTL062 = document.getElementById("DDLDTL062").value;
			var TXTDTL063 = document.getElementById("TXTDTL063").value;
			var TXTDTL064 = document.getElementById("TXTDTL064").value;
			
			var CHKDTL065 = document.getElementById("CHKDTL065").checked;
			var DDLDTL066 = document.getElementById("DDLDTL066").value;
			var TXTDTL067 = document.getElementById("TXTDTL067").value;
			var TXTDTL068 = document.getElementById("TXTDTL068").value;
			
			var CHKDTL069 = document.getElementById("CHKDTL069").checked;
			var DDLDTL070 = document.getElementById("DDLDTL070").value;
			var TXTDTL071 = document.getElementById("TXTDTL071").value;
			var TXTDTL072 = document.getElementById("TXTDTL072").value;
			
			var CHKDTL073 = document.getElementById("CHKDTL073").checked;
			var DDLDTL074 = document.getElementById("DDLDTL074").value;
			var TXTDTL075 = document.getElementById("TXTDTL075").value;
			var TXTDTL076 = document.getElementById("TXTDTL076").value;
			
			var TXTDTL077 = document.getElementById("TXTDTL077").value;
			//var TXTDTL079 = document.getElementById("TXTDTL079").value;
			
			var url_string = location.href;
			var url = new URL(url_string);
			
            var txn = db.transaction("SWCDT004", "readwrite");
            var store = txn.objectStore("SWCDT004");
			
			var url_string = location.href;
			var url = new URL(url_string);
            var data = { 
				SWCNO: url.searchParams.get("SWCNO"),
				LBSWC005: LBSWC005,
				LBSWC002: LBSWC002,
				
				LBDTL001: LBDTL001,
				
				TXTDTL002:TXTDTL002,
				DDLDTL003:DDLDTL003,
				DDLDTL085:DDLDTL085,
				DDLDTL004:DDLDTL004,
				
				CHKDTL005:CHKDTL005,
				DDLDTL006:DDLDTL006,
				TXTDTL007:TXTDTL007,
				TXTDTL008:TXTDTL008,
				
				CHKDTL009:CHKDTL009,
				DDLDTL010:DDLDTL010,
				TXTDTL011:TXTDTL011,
				TXTDTL012:TXTDTL012,
				
				CHKDTL013:CHKDTL013,
				DDLDTL014:DDLDTL014,
				TXTDTL015:TXTDTL015,
				TXTDTL016:TXTDTL016,
				
				CHKDTL017:CHKDTL017,
				DDLDTL018:DDLDTL018,
				TXTDTL019:TXTDTL019,
				TXTDTL020:TXTDTL020,
				
				CHKDTL021:CHKDTL021,
				DDLDTL022:DDLDTL022,
				TXTDTL023:TXTDTL023,
				TXTDTL024:TXTDTL024,
				
				CHKDTL025:CHKDTL025,
				DDLDTL026:DDLDTL026,
				TXTDTL027:TXTDTL027,
				TXTDTL028:TXTDTL028,
				
				CHKDTL029:CHKDTL029,
				DDLDTL030:DDLDTL030,
				TXTDTL031:TXTDTL031,
				TXTDTL032:TXTDTL032,
				
				CHKDTL033:CHKDTL033,
				DDLDTL034:DDLDTL034,
				TXTDTL035:TXTDTL035,
				TXTDTL036:TXTDTL036,
				
				CHKDTL037:CHKDTL037,
				DDLDTL038:DDLDTL038,
				TXTDTL039:TXTDTL039,
				TXTDTL040:TXTDTL040,
				
				CHKDTL041:CHKDTL041,
				DDLDTL042:DDLDTL042,
				TXTDTL043:TXTDTL043,
				TXTDTL044:TXTDTL044,
				
				CHKDTL045:CHKDTL045,
				DDLDTL046:DDLDTL046,
				TXTDTL047:TXTDTL047,
				TXTDTL048:TXTDTL048,
				
				CHKDTL049:CHKDTL049,
				DDLDTL050:DDLDTL050,
				TXTDTL051:TXTDTL051,
				TXTDTL052:TXTDTL052,
				
				CHKDTL053:CHKDTL053,
				DDLDTL054:DDLDTL054,
				TXTDTL055:TXTDTL055,
				TXTDTL056:TXTDTL056,
				
				CHKDTL057:CHKDTL057,
				DDLDTL058:DDLDTL058,
				TXTDTL059:TXTDTL059,
				TXTDTL060:TXTDTL060,
				
				CHKDTL061:CHKDTL061,
				DDLDTL062:DDLDTL062,
				TXTDTL063:TXTDTL063,
				TXTDTL064:TXTDTL064,
				
				CHKDTL065:CHKDTL065,
				DDLDTL066:DDLDTL066,
				TXTDTL067:TXTDTL067,
				TXTDTL068:TXTDTL068,
				
				CHKDTL069:CHKDTL069,
				DDLDTL070:DDLDTL070,
				TXTDTL071:TXTDTL071,
				TXTDTL072:TXTDTL072,
				
				CHKDTL073:CHKDTL073,
				DDLDTL074:DDLDTL074,
				TXTDTL075:TXTDTL075,
				TXTDTL076:TXTDTL076,
				
				TXTDTL077:TXTDTL077//,
				//TXTDTL079:TXTDTL079
			};
            var req = store.put(data);
			//儲存圖片列表
			var table = document.getElementById("Table_Temp1");
			
			var txn = db.transaction("SWCDT004_1", "readwrite");
			var store = txn.objectStore("SWCDT004_1");
			var url_string = location.href;
			var url = new URL(url_string);
			var LBDTL001 = document.getElementById("LBDTL001").innerHTML;
			
			var req = store.delete(IDBKeyRange.bound([url.searchParams.get("SWCNO"),document.getElementById("LBDTL001").innerHTML,1],[url.searchParams.get("SWCNO"),document.getElementById("LBDTL001").innerHTML,100]), 'next');
			
			for(var i = 1; i < table.rows.length; i++)
			{
				var txn = db.transaction("SWCDT004_1", "readwrite");
				var store = txn.objectStore("SWCDT004_1");
				
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
            var txn = db.transaction("SWCDT004", "readwrite");
            var store = txn.objectStore("SWCDT004");
			
			
			var url_string = location.href;
			var url = new URL(url_string);

            var req = store.get(url.searchParams.get("SWCNO"));

            req.onsuccess = function (e) {
				var data1 = e.target.result;
				
				$("[id=TXTDTL002]").val(data1.TXTDTL002);
				$("[id=DDLDTL003]").val(data1.DDLDTL003);
				$("[id=DDLDTL085]").val(data1.DDLDTL085);
				$("[id=DDLDTL004]").val(data1.DDLDTL004);
				
				$("[id=CHKDTL005]").prop('checked',data1.CHKDTL005);
				$("[id=DDLDTL006]").val(data1.DDLDTL006);
				$("[id=TXTDTL007]").val(data1.TXTDTL007);
				$("[id=TXTDTL008]").val(data1.TXTDTL008);
				
				$("[id=CHKDTL009]").prop('checked',data1.CHKDTL009);
				$("[id=DDLDTL010]").val(data1.DDLDTL010);
				$("[id=TXTDTL011]").val(data1.TXTDTL011);
				$("[id=TXTDTL012]").val(data1.TXTDTL012);
				
				$("[id=CHKDTL013]").prop('checked',data1.CHKDTL013);
				$("[id=DDLDTL014]").val(data1.DDLDTL014);
				$("[id=TXTDTL015]").val(data1.TXTDTL015);
				$("[id=TXTDTL016]").val(data1.TXTDTL016);
				
				$("[id=CHKDTL017]").prop('checked',data1.CHKDTL017);
				$("[id=DDLDTL018]").val(data1.DDLDTL018);
				$("[id=TXTDTL019]").val(data1.TXTDTL019);
				$("[id=TXTDTL020]").val(data1.TXTDTL020);
				
				$("[id=CHKDTL021]").prop('checked',data1.CHKDTL021);
				$("[id=DDLDTL022]").val(data1.DDLDTL022);
				$("[id=TXTDTL023]").val(data1.TXTDTL023);
				$("[id=TXTDTL024]").val(data1.TXTDTL024);
				
				$("[id=CHKDTL025]").prop('checked',data1.CHKDTL025);
				$("[id=DDLDTL026]").val(data1.DDLDTL026);
				$("[id=TXTDTL027]").val(data1.TXTDTL027);
				$("[id=TXTDTL028]").val(data1.TXTDTL028);
				
				$("[id=CHKDTL029]").prop('checked',data1.CHKDTL029);
				$("[id=DDLDTL030]").val(data1.DDLDTL030);
				$("[id=TXTDTL031]").val(data1.TXTDTL031);
				$("[id=TXTDTL032]").val(data1.TXTDTL032);
				
				$("[id=CHKDTL033]").prop('checked',data1.CHKDTL033);
				$("[id=DDLDTL034]").val(data1.DDLDTL034);
				$("[id=TXTDTL035]").val(data1.TXTDTL035);
				$("[id=TXTDTL036]").val(data1.TXTDTL036);
				
				$("[id=CHKDTL037]").prop('checked',data1.CHKDTL037);
				$("[id=DDLDTL038]").val(data1.DDLDTL038);
				$("[id=TXTDTL039]").val(data1.TXTDTL039);
				$("[id=TXTDTL040]").val(data1.TXTDTL040);
				
				$("[id=CHKDTL041]").prop('checked',data1.CHKDTL041);
				$("[id=DDLDTL042]").val(data1.DDLDTL042);
				$("[id=TXTDTL043]").val(data1.TXTDTL043);
				$("[id=TXTDTL044]").val(data1.TXTDTL044);
				
				$("[id=CHKDTL045]").prop('checked',data1.CHKDTL045);
				$("[id=DDLDTL046]").val(data1.DDLDTL046);
				$("[id=TXTDTL047]").val(data1.TXTDTL047);
				$("[id=TXTDTL048]").val(data1.TXTDTL048);
				
				$("[id=CHKDTL049]").prop('checked',data1.CHKDTL049);
				$("[id=DDLDTL050]").val(data1.DDLDTL050);
				$("[id=TXTDTL051]").val(data1.TXTDTL051);
				$("[id=TXTDTL052]").val(data1.TXTDTL052);
				
				$("[id=CHKDTL053]").prop('checked',data1.CHKDTL053);
				$("[id=DDLDTL054]").val(data1.DDLDTL054);
				$("[id=TXTDTL055]").val(data1.TXTDTL055);
				$("[id=TXTDTL056]").val(data1.TXTDTL056);
				
				$("[id=CHKDTL057]").prop('checked',data1.CHKDTL057);
				$("[id=DDLDTL058]").val(data1.DDLDTL058);
				$("[id=TXTDTL059]").val(data1.TXTDTL059);
				$("[id=TXTDTL060]").val(data1.TXTDTL060);
				
				$("[id=CHKDTL061]").prop('checked',data1.CHKDTL061);
				$("[id=DDLDTL062]").val(data1.DDLDTL062);
				$("[id=TXTDTL063]").val(data1.TXTDTL063);
				$("[id=TXTDTL064]").val(data1.TXTDTL064);
				
				$("[id=CHKDTL065]").prop('checked',data1.CHKDTL065);
				$("[id=DDLDTL066]").val(data1.DDLDTL066);
				$("[id=TXTDTL067]").val(data1.TXTDTL067);
				$("[id=TXTDTL068]").val(data1.TXTDTL068);
				
				$("[id=CHKDTL069]").prop('checked',data1.CHKDTL069);
				$("[id=DDLDTL070]").val(data1.DDLDTL070);
				$("[id=TXTDTL071]").val(data1.TXTDTL071);
				$("[id=TXTDTL072]").val(data1.TXTDTL072);
				
				$("[id=CHKDTL073]").prop('checked',data1.CHKDTL073);
				$("[id=DDLDTL074]").val(data1.DDLDTL074);
				$("[id=TXTDTL075]").val(data1.TXTDTL075);
				$("[id=TXTDTL076]").val(data1.TXTDTL076);
				
				$("[id=TXTDTL077]").val(data1.TXTDTL077);
				//$("[id=TXTDTL079]").val(data1.TXTDTL079);
				//讀取圖片列表
				LoadImageList();
				alert("讀取成功");
			};

            req.onerror = function () {
			
            };
		};
		
		//讀取圖片列表
		function LoadImageList(){
			var txn = db.transaction("SWCDT004_1", "readwrite");
            var store = txn.objectStore("SWCDT004_1");
			
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
			//delete data
            var txn = db.transaction("SWCDT004", "readwrite");
            var store = txn.objectStore("SWCDT004");
			
			var url_string = location.href;
			var url = new URL(url_string);

            var req = store.delete(url.searchParams.get("SWCNO"));
			
			var txn = db.transaction("SWCDT004_1", "readwrite");
            var store = txn.objectStore("SWCDT004_1");

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
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"/>
    <div class="wrap-s">
        <div class="header_PWA">
		  <img class="logo" src="../../img/logo2.svg"/>
            <span>颱風豪雨設施自主檢查表</span>
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
               <%-- <h1>水土保持施工監督檢查紀錄</h1><br/>--%>
                <asp:Button ID="GoOnline" runat="server" Text="讀取上次資料" UseSubmitBehavior="false" onclientclick="return getValue(this);" CssClass="none" /><br />
				

            <%--tab--%>
            <div class="tab_content">
			    <!-- TAB1 打包區塊 star -->
				<asp:Panel ID="tab1" runat="server">
                   <div class="tab_main"> <!--class="tab_content"-->
				   <table class="swcchg-outA tdpadding">
                        <tr><th>自主檢查表編號</th>
                            <td><asp:Label ID="LBDTL001" runat="server"/>
                                <asp:Label ID="LBSWC000" runat="server" Visible="false"/>
								<asp:TextBox ID="TXTIMAGE" runat="server" style="display:none;"/></td></tr>
			        	<tr><th>水保局編號</th>
			        		<td><asp:Label ID="LBSWC002" runat="server" /></td></tr>
                        <tr><th>案件名稱</th>
                            <td><asp:Label ID="LBSWC005" runat="server"/></td></tr>
                        <tr><th>檢查日期<span style="color: red;font-family:cursive;">＊</span></th>
                            <td><asp:TextBox ID="TXTDTL002" runat="server" width="150px" autocomplete="off"></asp:TextBox>
                                <asp:CalendarExtender ID="TXTDTL002_CalendarExtender" runat="server" TargetControlID="TXTDTL002" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                        <tr><th>防災標的<span style="color: red;font-family:cursive;">＊</span></th>
                            <td><asp:DropDownList ID="DDLDTL003" runat="server" Height="25px"/>
                                <asp:DropDownList ID="DDLDTL085" runat="server" Height="25px"/></td></tr>
                        <tr><th>自主檢查結果<span style="color: red;font-family:cursive;">＊</span></th>
                            <td><asp:DropDownList ID="DDLDTL004" runat="server" Height="25px"/></td></tr>
                    </table> 
                   </div>
                  </asp:Panel>
                <!-- TAB1 打包區塊 end -->

                <!-- TAB2 打包區塊 star -->
                 <asp:Panel ID="tab2" runat="server" style="display:none;">
                 <div class="tab_main"> <!--class="tab_content"-->
                     <ul class="nav nav-tabs" role="tablist">
                      <li class="nav-item">
                        <a id="B1" class="nav-link active" href="#A1" role="tab" data-toggle="tab">1</a>
                      </li>
                      <li class="nav-item">
                        <a id="B2" class="nav-link" href="#A2" role="tab" data-toggle="tab">2</a>
                      </li>
                      <li class="nav-item">
                        <a id="B3" class="nav-link" href="#A3" role="tab" data-toggle="tab">3</a>
                      </li>
                      <%--<li class="nav-item">
                        <a id="B4" class="nav-link" href="#A4" role="tab" data-toggle="tab">4</a>
                      </li>
                        <li class="nav-item">
                        <a id="B5" class="nav-link" href="#A5" role="tab" data-toggle="tab">5</a>
                      </li>--%>
                    </ul>


                 <!-- Tab panes 1-->
                 <div class="tab-content">
                 <div role="tabpanel" class="tab-pane fade in active" id="A1">
                 <table class="swcchg-out checknonecss opentxt">
                        <%--<tr><th>一、檢查項目</th>
                            <th colspan="4">檢查情形-設施功能是否完善<br/>(填"否"者，請填寫因應措施)</th>
                            <th>因應措施</th>
                            <th>備註</th></tr>--%>
                        <tr><th colspan="7">（一）水土保持施工告示牌</th></tr>
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
                     </table>
                    </div>
                    <div role="tabpanel" class="tab-pane fade" id="A2">
                     <table class="swcchg-out checknonecss opentxt">
                        <tr><th colspan="7">（二）臨時性防災措施</th></tr>
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
                        </table>
                        </div>
                        <div role="tabpanel" class="tab-pane fade" id="A3">
                        <table class="swcchg-out checknonecss opentxt">
                        <tr><th colspan="7">（三）永久性防災措施</th></tr>
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
                        <tr><th>其他事項：</th>
                             <td><asp:TextBox ID="TXTDTL077" runat="server" width="100%" Height="60px" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL077_count','800');"  />
                                <asp:Label ID="TXTDTL077_count" runat="server" Text="(0/800)" ForeColor="Red" /></td></tr>
                            <tr><td colspan="7" style="text-align:right;border-top:none;"><%--檢查日期：--%>
                        <asp:TextBox ID="TXTDTL078" runat="server" width="120px" Visible="false"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTDTL078_CalendarExtender" runat="server" TargetControlID="TXTDTL078" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                </table>
                </div>
                </div>
                </div>
                </asp:Panel>
                <!-- TAB2 打包區塊 end -->
                <!-- TAB3 打包區塊 start -->
                 <asp:Panel ID="tab3" runat="server" style="display:none;">
				 <div class="tab_main"> <!--class="tab_content"-->
                       <table class="mbborder">
                         <tr><td colspan="2">
                             <b style="margin-bottom:5px; display:block;">二、相關單位及人員簽名</b><br/>
                              <asp:TextBox ID="TXTDTL079" runat="server" width="100%" Height="100px" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL079_count','800');" style="width:100%;height:250px;" Visible="false" />
                              <asp:Label ID="TXTDTL079_count" runat="server" Text="(0/800)" ForeColor="Red" Visible="false" />
                              <asp:Image ID="TXTDTL084_img" runat="server" CssClass="upimg" Visible="false" />
							  
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
							  
							  
							  
							  <asp:HyperLink ID="HyperLink084" runat="server" CssClass="imgUpload" Target="_blank" Visible="false"></asp:HyperLink>
							<asp:Panel runat="server" Visible="false">
							  <br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/><br/>
							  <asp:TextBox ID="TXTDTL084" runat="server" Width="70px" Visible="false" />
							  <asp:FileUpload ID="TXTDTL084_fileupload" runat="server" />
							  <asp:Button ID="TXTDTL084_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL084_fileuploadok_Click" />
							  <asp:Button ID="TXTDTL084_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL084_fileuploaddel_Click" /></td>
							</asp:Panel>
						 </tr>
                       </table>
				 </div>
                 </asp:Panel>
                <!-- TAB3 打包區塊 end -->
               
          
            

            <br/><br/>
                
            
                <%--<tr><td colspan="7">二、相關單位及人員簽名<br/><br/>
                    
                    
                        <p style="line-height:2.5;">註:1.本表格請依所核水土保持設施項目填報，無此項或尚未施作項目請於備註欄說明。並視所需自行增減欄位。<br/></p>
                        <p style="padding-left:20px;margin-top:10px;">2.請水土保持義務人會同承辦監造技師及施工廠商，於中央氣象局發布海上颱風警報或豪雨特報後填列本表。</p>
                            </td></tr>
                </table>--%>
				
				
				
				<div class="inbox" style="display:none;">
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
                
                <!--<div class="form-btn none">
					<asp:Panel runat="server" Visible="false">
						<asp:Button ID="DataLock" runat="server" Text="確認送出" OnClientClick="return chkInput('DataLock');" OnClick="DataLock_Click" />&nbsp&nbsp
						<asp:Button ID="GoHomePage" runat="server" Text="返回編輯案件" OnClick="GoHomePage_Click" />
					</asp:Panel>
					<asp:Button ID="GoOffline" runat="server" Text="離線暫存" UseSubmitBehavior="false" onclientclick="setValue();" />
					<asp:Button ID="SaveCase1" runat="server" Text="上傳至系統" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" />
                </div>-->
              </div>
            </div>
        </div>
        
		<br/><br/><br/>
	  <div class="PWAbottombtn">
		  <asp:ImageButton runat="server" UseSubmitBehavior="false" src="../../img/PWA_icon-09.svg" onclientclick="setValue();"/>
		  <asp:ImageButton ID="SaveCase" runat="server" UseSubmitBehavior="false" src="../../img/PWA_icon-10.svg" OnClientClick="return chkInput('');" OnClick="SaveCase_Click"/>
      </div>
                
            <div class="footer-b-brown none">
                 <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                 <span class="span2">110臺北市信義區松德路300號3樓 　服務專線(02)27591109   臺北市民當家熱線1999</span><br/>
                 <span class="span2">建議使用IE11(含)以上，Chrome或Firefox版本瀏覽器 資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                 <span class="span2">客服電話：02-27593001#3729 許先生 本系統由多維空間資訊有限公司開發維護 TEL：(02)27929328</span><br/>
				 <span class="span2">※為維護系統服務品質，本平台訂於每周三凌晨AM 4:00-6:30 進行系統維護更新，更新期間偶有瞬斷情形，敬請使用者避開該時段使用。謝謝！</span></p>
            </div>

        </div>
        
        <asp:Literal ID="error_msg" runat="server"></asp:Literal>

        <script src="../../js/jquery-3.1.1.min.js"></script>
        <script src="../../js/boostrap.js"></script>
        <script src="../../js/inner.js?20220217"></script>

    </div>
    </form>
</body>
</html>
