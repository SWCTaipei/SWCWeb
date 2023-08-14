<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCDT003PWA.aspx.cs" Inherits="PWA_SWCDT003_SWCDT003PWA" %>
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
    <link rel="stylesheet" type="text/css" href="../../css/all.css?202208230246" />
	<link rel="stylesheet" type="text/css" href="../../css/iris.css" />
    <link rel="stylesheet" type="text/css" href="../../css/all_PWA.css?202210240336"/>
    <link rel="stylesheet" type="text/css" href="../../css/all_PWA_2.css?202210240336"/>
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600' rel='stylesheet' type='text/css' />
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
        function addtext(unitelement, peopleelement, listelement) {
            listelement.value = listelement.value + "\r\n" + unitelement.value + " " + peopleelement.value;
            return false;
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
			var DDLDTL071 = document.getElementById("DDLDTL071").value;
			
			var DDLDTL006 = document.getElementById("DDLDTL006").value;
			var TXTDTL007 = document.getElementById("TXTDTL007").value;
			
			var DDLDTL008 = document.getElementById("DDLDTL008").value;
			var TXTDTL009 = document.getElementById("TXTDTL009").value;
			
			var DDLDTL010 = document.getElementById("DDLDTL010").value;
			var TXTDTL011 = document.getElementById("TXTDTL011").value;
			
			var DDLDTL012 = document.getElementById("DDLDTL012").value;
			var TXTDTL013 = document.getElementById("TXTDTL013").value;
			
			var DDLDTL014 = document.getElementById("DDLDTL014").value;
			var TXTDTL015 = document.getElementById("TXTDTL015").value;
			
			var DDLDTL016 = document.getElementById("DDLDTL016").value;
			var TXTDTL017 = document.getElementById("TXTDTL017").value;
			
			var DDLDTL018 = document.getElementById("DDLDTL018").value;
			var TXTDTL019 = document.getElementById("TXTDTL019").value;
			
			var DDLDTL020 = document.getElementById("DDLDTL020").value;
			var TXTDTL021 = document.getElementById("TXTDTL021").value;
			
			var DDLDTL022 = document.getElementById("DDLDTL022").value;
			var TXTDTL023 = document.getElementById("TXTDTL023").value;
			
			var DDLDTL072 = document.getElementById("DDLDTL072").value;
			var TXTDTL073 = document.getElementById("TXTDTL073").value;
			
			var TXTDTL025 = document.getElementById("TXTDTL025").value;
			
			var DDLDTL026 = document.getElementById("DDLDTL026").value;
			var TXTDTL027 = document.getElementById("TXTDTL027").value;
			
			var DDLDTL028 = document.getElementById("DDLDTL028").value;
			var TXTDTL029 = document.getElementById("TXTDTL029").value;
			
			var DDLDTL030 = document.getElementById("DDLDTL030").value;
			var TXTDTL031 = document.getElementById("TXTDTL031").value;
			
			var DDLDTL032 = document.getElementById("DDLDTL032").value;
			var TXTDTL033 = document.getElementById("TXTDTL033").value;
			
			var DDLDTL034 = document.getElementById("DDLDTL034").value;
			var TXTDTL035 = document.getElementById("TXTDTL035").value;
			
			var DDLDTL036 = document.getElementById("DDLDTL036").value;
			var TXTDTL037 = document.getElementById("TXTDTL037").value;
			
			var DDLDTL038 = document.getElementById("DDLDTL038").value;
			var TXTDTL039 = document.getElementById("TXTDTL039").value;
			
			var TXTDTL041 = document.getElementById("TXTDTL041").value;
			
			var DDLDTL042 = document.getElementById("DDLDTL042").value;
			var TXTDTL043 = document.getElementById("TXTDTL043").value;
			
			var DDLDTL044 = document.getElementById("DDLDTL044").value;
			
			var TXTDTL049 = document.getElementById("TXTDTL049").value;
			
			var TXTDTL074 = document.getElementById("TXTDTL074").value;
			
			var TXTDTL050 = document.getElementById("TXTDTL050").value;
			
			var TXTDTL055 = document.getElementById("TXTDTL055").value;
			
			
			var url_string = location.href;
			var url = new URL(url_string);
			
            var txn = db.transaction("SWCDT003", "readwrite");
            var store = txn.objectStore("SWCDT003");
			
            var data = { 
				SWCNO: url.searchParams.get("SWCNO"),
				LBSWC005: LBSWC005,
				LBSWC002: LBSWC002,
				LBDTL001: LBDTL001,
				
				TXTDTL002:TXTDTL002,
				DDLDTL071:DDLDTL071,
				
				DDLDTL006:DDLDTL006,
				TXTDTL007:TXTDTL007,
				
				DDLDTL008:DDLDTL008,
				TXTDTL009:TXTDTL009,
				
				DDLDTL010:DDLDTL010,
				TXTDTL011:TXTDTL011,
				
				DDLDTL012:DDLDTL012,
				TXTDTL013:TXTDTL013,
				
				DDLDTL014:DDLDTL014,
				TXTDTL015:TXTDTL015,
				
				DDLDTL016:DDLDTL016,
				TXTDTL017:TXTDTL017,
				
				DDLDTL018:DDLDTL018,
				TXTDTL019:TXTDTL019,
				
				DDLDTL020:DDLDTL020,
				TXTDTL021:TXTDTL021,
				
				DDLDTL022:DDLDTL022,
				TXTDTL023:TXTDTL023,
				
				DDLDTL072:DDLDTL072,
				TXTDTL073:TXTDTL073,
				
				TXTDTL025:TXTDTL025,
				
				DDLDTL026:DDLDTL026,
				TXTDTL027:TXTDTL027,
				
				DDLDTL028:DDLDTL028,
				TXTDTL029:TXTDTL029,
				
				DDLDTL030:DDLDTL030,
				TXTDTL031:TXTDTL031,
				
				DDLDTL032:DDLDTL032,
				TXTDTL033:TXTDTL033,
				
				DDLDTL034:DDLDTL034,
				TXTDTL035:TXTDTL035,
				
				DDLDTL036:DDLDTL036,
				TXTDTL037:TXTDTL037,
				
				DDLDTL038:DDLDTL038,
				TXTDTL039:TXTDTL039,
				
				TXTDTL041:TXTDTL041,
				
				DDLDTL042:DDLDTL042,
				TXTDTL043:TXTDTL043,
				
				DDLDTL044:DDLDTL044,
				
				TXTDTL049:TXTDTL049,
				
				TXTDTL074:TXTDTL074,
				
				TXTDTL050:TXTDTL050,
				
				TXTDTL055:TXTDTL055
			};
			

            var req = store.put(data);
			//儲存圖片列表
			var table = document.getElementById("Table_Temp1");
			
			var txn = db.transaction("SWCDT003_1", "readwrite");
			var store = txn.objectStore("SWCDT003_1");
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
            var txn = db.transaction("SWCDT003_2", "readwrite");
            var store = txn.objectStore("SWCDT003_2");
			
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
            var txn = db.transaction("SWCDT003", "readwrite");
            var store = txn.objectStore("SWCDT003");
			
			var url_string = location.href;
			var url = new URL(url_string);
			
            var req = store.get(url.searchParams.get("SWCNO"));
			
            req.onsuccess = function (e) {
				var data1 = e.target.result;
				
				$("[id=TXTDTL002]").val(data1.TXTDTL002);
				$("[id=DDLDTL071]").val(data1.DDLDTL071);
				$("[id=DDLDTL006]").val(data1.DDLDTL006);
				$("[id=TXTDTL007]").val(data1.TXTDTL007);
				$("[id=DDLDTL008]").val(data1.DDLDTL008);
				$("[id=TXTDTL009]").val(data1.TXTDTL009);
				$("[id=DDLDTL010]").val(data1.DDLDTL010);
				$("[id=TXTDTL011]").val(data1.TXTDTL011);
				$("[id=DDLDTL012]").val(data1.DDLDTL012);
				$("[id=TXTDTL013]").val(data1.TXTDTL013);
				$("[id=DDLDTL014]").val(data1.DDLDTL014);
				$("[id=TXTDTL015]").val(data1.TXTDTL015);
				$("[id=DDLDTL016]").val(data1.DDLDTL016);
				$("[id=TXTDTL017]").val(data1.TXTDTL017);
				$("[id=DDLDTL018]").val(data1.DDLDTL018);
				$("[id=TXTDTL019]").val(data1.TXTDTL019);
				$("[id=DDLDTL020]").val(data1.DDLDTL020);
				$("[id=TXTDTL021]").val(data1.TXTDTL021);
				$("[id=DDLDTL022]").val(data1.DDLDTL022);
				$("[id=TXTDTL023]").val(data1.TXTDTL023);
				$("[id=DDLDTL072]").val(data1.DDLDTL072);
				$("[id=TXTDTL073]").val(data1.TXTDTL073);
				$("[id=TXTDTL025]").val(data1.TXTDTL025);
				$("[id=DDLDTL026]").val(data1.DDLDTL026);
				$("[id=TXTDTL027]").val(data1.TXTDTL027);
				$("[id=DDLDTL028]").val(data1.DDLDTL028);
				$("[id=TXTDTL029]").val(data1.TXTDTL029);
				$("[id=DDLDTL030]").val(data1.DDLDTL030);
				$("[id=TXTDTL031]").val(data1.TXTDTL031);
				$("[id=DDLDTL032]").val(data1.DDLDTL032);
				$("[id=TXTDTL033]").val(data1.TXTDTL033);
				$("[id=DDLDTL034]").val(data1.DDLDTL034);
				$("[id=TXTDTL035]").val(data1.TXTDTL035);
				$("[id=DDLDTL036]").val(data1.DDLDTL036);
				$("[id=TXTDTL037]").val(data1.TXTDTL037);
				$("[id=DDLDTL038]").val(data1.DDLDTL038);
				$("[id=TXTDTL039]").val(data1.TXTDTL039);
				$("[id=TXTDTL041]").val(data1.TXTDTL041);
				$("[id=DDLDTL042]").val(data1.DDLDTL042);
				$("[id=TXTDTL043]").val(data1.TXTDTL043);
				$("[id=DDLDTL044]").val(data1.DDLDTL044);
				$("[id=TXTDTL049]").val(data1.TXTDTL049);
				$("[id=TXTDTL074]").val(data1.TXTDTL074);
				$("[id=TXTDTL050]").val(data1.TXTDTL050);
				$("[id=TXTDTL055]").val(data1.TXTDTL055);
				LoadImageList();
				LoadFacilityList();
				alert("讀取成功");
			};
		};
		
		//讀取圖片列表
		function LoadImageList(){
			var txn = db.transaction("SWCDT003_1", "readwrite");
            var store = txn.objectStore("SWCDT003_1");
			
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
            var txn = db.transaction("SWCDT003", "readwrite");
            var store = txn.objectStore("SWCDT003");
			
			var url_string = location.href;
			var url = new URL(url_string);

            var req = store.delete(url.searchParams.get("SWCNO"));
			
			var txn = db.transaction("SWCDT003_1", "readwrite");
            var store = txn.objectStore("SWCDT003_1");

			var req = store.delete(IDBKeyRange.bound([url.searchParams.get("SWCNO"),document.getElementById("LBDTL001").innerHTML,1],[url.searchParams.get("SWCNO"),document.getElementById("LBDTL001").innerHTML,100]), 'next');
			
			var txn = db.transaction("SWCDT003_2", "readwrite");
            var store = txn.objectStore("SWCDT003_2");

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
            var txn = db.transaction("SWCDT003_2", "readonly");
            var store = txn.objectStore("SWCDT003_2");
			
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"/>
    <div class="wrap-s">
        <div class="header_PWA">
		  <img class="logo" src="../../img/logo2.svg"/>
            <span>水土保持施工監督檢查紀錄</span>
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
               <%-- <h1>水土保持施工監督檢查紀錄</h1><br/>--%>
                <asp:Button ID="GoOnline" runat="server" Text="讀取上次資料" UseSubmitBehavior="false" CssClass="read_data none" Visible="false" onclientclick="return getValue(this);" /><br/>
				<div class="detailsMenu-btn none">
                    <asp:ImageButton ID="OutPdf" runat="server" title="輸出PDF" ImageUrl="../images/btn/icon_exportpdf.png" OnClick="OutPdf_Click" Visible="false" />
                </div>

                <%--tab--%>
                  <div class="tab_content">
				    <!-- TAB1 打包區塊 end -->
                    <!--<input id="tab1" type="radio" name="tab" checked="checked"/-->
                    <!--<label for="tab1" class="fixed1">案件資訊</label-->
					<asp:Panel ID="tab1" runat="server">
                    <div class="tab_main"> <!--class="tab_content"-->
					<table class="swcchg-outA tdpadding">
                       <tr><th>施工監督表編號</th>
                           <td><asp:Label ID="LBDTL001" runat="server"/>
                               <asp:Label ID="LBSWC000" runat="server" Visible="false" />
                               <asp:Label ID="LBSWC012" runat="server" Visible="false" />
                               <asp:Label ID="LBSWC025" runat="server" Visible="false" />
							   <asp:TextBox ID="TXTIMAGE" runat="server" style="display:none;"/></td></tr></td></tr>
					   <tr><th>水保局編號</th>
					   	<td><asp:Label ID="LBSWC002" runat="server"/></td></tr>
                       <tr><th>檢查日期<span style="color: red;font-family:cursive;">＊</span></th>
                           <td><asp:TextBox ID="TXTDTL002" runat="server" autocomplete="off" ></asp:TextBox>
                               <asp:CalendarExtender ID="TXTDTL002_CalendarExtender" runat="server" TargetControlID="TXTDTL002" Format="yyyy-MM-dd"></asp:CalendarExtender>
                               <span class="gray">（範例：2020-01-02）</span>
                           </td></tr>
                       
                       <tr><th>檢查單位</th>
                           <td><asp:Label ID="TXTDTL004" runat="server" width="100%" /></td></tr>
                       <tr><th>面積</th>
                           <td><asp:Label ID="LBSWC023" runat="server" /> 公頃</td></tr>

                        <%--222--%>
                        <tr><%--<td rowspan="4">水土保持書件</td>--%>
                            <th>計畫名稱</th>
                            <td><asp:Label ID="LBSWC005" runat="server"/>
                                （<asp:Label ID="LBSWC007" runat="server"/>）</td></tr>
                        <tr style="display:none;"><td class="CRTT">核定日期文號</td>
                            <td>臺北市政府
                                <asp:Label ID="LBSWC038" runat="server"/>
                                <asp:Label ID="LBSWC039" runat="server"/>
                                函</td></tr>
                        <tr style="display:none;"><td class="CRTT">水土保持施工許可證日期文號</td>
                            <td>臺北市政府
                                <asp:Label ID="LBSWC043" runat="server"/>
                                <asp:Label ID="LBSWC044" runat="server"/>
                                函</td></tr>
                        <tr style="display:none;"><td class="CRTT">開工日期</td>
                            <td><asp:Label ID="LBSWC051" runat="server"/></td></tr>
                        <tr><th>預定完工日期</th>
                            <td><asp:Label ID="LBSWC052" runat="server"/></td></tr>
                        <tr><th>工程進度</th>
                            <td><asp:Label ID="LBSWCO01" runat="server"/></td></tr>
                        <tr><th>檢查結果</th>
                            <td><asp:Label ID="LBSWC071" runat="server"/></td></tr>
				        	
				        	
                        <tr><%--<td rowspan="1" colspan="2" class="CR01">水土保持義務人姓名或名稱</td>--%>
                            <th>義務人姓名或名稱</th>
                            <td><asp:Label ID="LBSWC013" runat="server"/></td></tr>
                        <tr style="display:none;"><td class="CRTT">住居所或營業所</td>
                            <td><asp:Label ID="LBSWC014" runat="server"/></td></tr>
                        <tr style="display:none;"><td class="CRTT">電話</td>
                            <td><asp:Label ID="LBSWC013TEL" runat="server"/></td></tr>
				        	
				        	
                        <tr><th>承辦監造技師姓名</th>
                            <td style="display:none;" class="CRTT">姓名</td>
                            <td><asp:Label ID="LBSWC021" runat="server"/></td></tr>
                        <tr style="display:none;"><td class="CRTT">執業機構名稱</td>
                            <td><asp:Label ID="LBSWC021Name" runat="server"/></td></tr>
                        <tr style="display:none;"><td class="CRTT">執業機構地址</td>
                            <td><asp:Label ID="LBSWC021OrgAddr" runat="server"/></td></tr>
                        <tr style="display:none;"><td class="CRTT">電話</td>
                            <td><asp:Label ID="LBSWC021OrgTel" runat="server"/></td></tr>
                   </table>
                        <div class="flex">
							<input id="btn_next1" type="button" value="下一頁" runat="server" class="flex1" />
						</div>
                    </div>
					</asp:Panel>
                    <!-- TAB1 打包區塊 end -->
                    
                    <!-- TAB2 打包區塊 start -->
                    <!--<input id="tab2" type="radio" name="tab"/-->
                    <!--<label for="tab2" class="fixed2">檢查項目</label-->
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
                      <li class="nav-item">
                        <a id="B4" class="nav-link" href="#A4" role="tab" data-toggle="tab">4</a>
                      </li>
                        <li class="nav-item">
                        <a id="B5" class="nav-link" href="#A5" role="tab" data-toggle="tab">5</a>
                      </li>
                    </ul>
                   
              
                   <!-- Tab panes -->
                   <div class="tab-content">
                     <div role="tabpanel" class="tab-pane fade in active" id="A1">
                         <table class="swcchg-out checknonecss opentxt">
                                 <%--<tr><th class="firstw">一、檢查項目</th>
                                     <td style="text-align: center;">檢查結果</td></tr>--%>
                                 <tr><th>（一）水土保持施工告示牌</th><!--20211018修改同步下方選項-->
                                     <td><asp:DropDownList ID="DDLDTL006" runat="server">
                                             <%--<asp:ListItem Value=""></asp:ListItem>--%>
                                             <%--<asp:ListItem Value="已設立">已設立</asp:ListItem>--%>
                                             <%--<asp:ListItem Value="資訊缺漏">資訊缺漏</asp:ListItem>--%>
                                             <%--<asp:ListItem Value="未設立">未設立</asp:ListItem>--%>
				                 			<asp:ListItem Value=""></asp:ListItem>
                                             <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                                             <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                                             <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                                             <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                                             <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                                             <asp:ListItem Value="無此項">無此項</asp:ListItem>
                                         </asp:DropDownList><br/>
                                             <asp:TextBox ID="TXTDTL007" runat="server" MaxLength="100"  onkeyup="textcount(this,'TXTDTL007_count','100');" placeholder="備註說明" />
                                             <asp:Label ID="TXTDTL007_count" runat="server" Text="(0/100)" ForeColor="Red" />
                                     </td></tr>
                                 <tr><th>（二）開發範圍界樁</th><!--20211018修改同步下方選項-->
                                     <td><asp:DropDownList ID="DDLDTL008" runat="server" >
                                             <%--<asp:ListItem Value=""></asp:ListItem>--%>
                                             <%--<asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>--%>
                                             <%--<asp:ListItem Value="有越界施工缺失應改善">有越界施工缺失應改善</asp:ListItem>--%>
                                             <%--<asp:ListItem Value="缺失應改未改或未依計畫施作，越界整地，通知大地處查處">缺失應改未改或未依計畫施作，越界整地，通知大地處查處</asp:ListItem>--%>
                                             <%--<asp:ListItem Value="應補立界樁">應補立界樁</asp:ListItem>--%>
				                 			<asp:ListItem Value=""></asp:ListItem>
                                             <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                                             <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                                             <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                                             <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                                             <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                                             <asp:ListItem Value="無此項">無此項</asp:ListItem>
                                         </asp:DropDownList><br/>
                                             <asp:TextBox ID="TXTDTL009" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL009_count','100');" placeholder="備註說明"  />
                                             <asp:Label ID="TXTDTL009_count" runat="server" Text="(0/100)" ForeColor="Red" />
                                     </td></tr>
                                 <tr><th>（三）開挖整地範圍界樁</th><!--20211018修改同步下方選項-->
                                     <td><asp:DropDownList ID="DDLDTL010" runat="server" >
                                             <%--<asp:ListItem Value=""></asp:ListItem>--%>
                                             <%--<asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>--%>
                                             <%--<asp:ListItem Value="應補立界樁">應補立界樁</asp:ListItem>--%>
                                             <%--<asp:ListItem Value="無此項">無此項</asp:ListItem>--%>
				                 			<asp:ListItem Value=""></asp:ListItem>
                                             <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                                             <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                                             <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                                             <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                                             <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                                             <asp:ListItem Value="無此項">無此項</asp:ListItem>
                                         </asp:DropDownList><br/>
                                             <asp:TextBox ID="TXTDTL011" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL011_count','100');" placeholder="備註說明"  />
                                             <asp:Label ID="TXTDTL011_count" runat="server" Text="(0/100)" ForeColor="Red" />
                                     </td></tr>
                                </table>
                                <div class="flex">
                                  <input id="Button8" type="button" value="繼續填寫" runat="server" class="flex2" />
                               </div>
                     </div>


                     <div role="tabpanel" class="tab-pane fade" id="A2">
                         <table class="swcchg-out checknonecss opentxt">
                                        <tr><th>（四）臨時性防災措施</th>
                                  <%--<td></td>--%></tr>
                              <tr><td class="td-padding">1.排水設施</td>
                                  <td><asp:DropDownList ID="DDLDTL012" runat="server" >
                                          <asp:ListItem Value=""></asp:ListItem>
                                          <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                                          <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                                          <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                                          <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                                          <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                                          <asp:ListItem Value="無此項">無此項</asp:ListItem>
                                      </asp:DropDownList><br/>
                                          <asp:TextBox ID="TXTDTL013" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL013_count','100');" placeholder="備註說明"  />
                                          <asp:Label ID="TXTDTL013_count" runat="server" Text="(0/100)" ForeColor="Red" />
                                  </td></tr>
                              <tr><td class="td-padding">2.沉砂設施</td>
                                  <td><asp:DropDownList ID="DDLDTL014" runat="server" >
                                          <asp:ListItem Value=""></asp:ListItem>
                                          <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                                          <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                                          <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                                          <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                                          <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                                          <asp:ListItem Value="無此項">無此項</asp:ListItem>
                                      </asp:DropDownList><br/>
                                          <asp:TextBox ID="TXTDTL015" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL015_count','100');" placeholder="備註說明"  />
                                          <asp:Label ID="TXTDTL015_count" runat="server" Text="(0/100)" ForeColor="Red" />
                                  </td></tr>
                              <tr><td class="td-padding">3.滯洪設施</td>
                                  <td><asp:DropDownList ID="DDLDTL016" runat="server" >
                                          <asp:ListItem Value=""></asp:ListItem>
                                          <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                                          <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                                          <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                                          <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                                          <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                                          <asp:ListItem Value="無此項">無此項</asp:ListItem>
                                      </asp:DropDownList><br/>
                                          <asp:TextBox ID="TXTDTL017" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL017_count','100');" placeholder="備註說明"  />
                                          <asp:Label ID="TXTDTL017_count" runat="server" Text="(0/100)" ForeColor="Red" />
                                  </td></tr>
                              <tr><td class="td-padding">4.土方暫置</td>
                                  <td><asp:DropDownList ID="DDLDTL018" runat="server" >
                                          <asp:ListItem Value=""></asp:ListItem>
                                          <asp:ListItem Value="尚無土方暫置須求">尚無土方暫置須求</asp:ListItem>
                                          <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                                          <%--<asp:ListItem Value="暫置方式有缺失應改善">暫置方式有缺失應改善</asp:ListItem>--%>
                                          <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                                          <%--<asp:ListItem Value="未依計畫施作，通知大地處查處">未依計畫施作，通知大地處查處</asp:ListItem>--%>
                                          <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                                          <asp:ListItem Value="無此項">無此項</asp:ListItem>
                                      </asp:DropDownList><br/>
                                          <asp:TextBox ID="TXTDTL019" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL019_count','100');" placeholder="備註說明"  />
                                          <asp:Label ID="TXTDTL019_count" runat="server" Text="(0/100)" ForeColor="Red" />
                                  </td></tr>
                              <tr><td class="td-padding">5.邊坡保護措施</td>
                                  <td><asp:DropDownList ID="DDLDTL020" runat="server" >
                                          <asp:ListItem Value=""></asp:ListItem>
                                          <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                                          <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                                          <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                                          <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                                          <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                                          <asp:ListItem Value="無此項">無此項</asp:ListItem>
                                      </asp:DropDownList><br/>
                                          <asp:TextBox ID="TXTDTL021" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL021_count','100');" placeholder="備註說明" />
                                          <asp:Label ID="TXTDTL021_count" runat="server" Text="(0/100)" ForeColor="Red" />
                                  </td></tr>
                              <tr><td class="td-padding">6.施工便道</td>
                                  <td><asp:DropDownList ID="DDLDTL022" runat="server" >
                                          <asp:ListItem Value=""></asp:ListItem>
                                          <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                                          <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                                          <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                                          <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                                          <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                                          <asp:ListItem Value="無此項">無此項</asp:ListItem>
                                      </asp:DropDownList><br/>
                                          <asp:TextBox ID="TXTDTL023" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL023_count','100');" placeholder="備註說明"  />
                                          <asp:Label ID="TXTDTL023_count" runat="server" Text="(0/100)" ForeColor="Red" />
                                  </td></tr>
                              <tr><td class="td-padding">7.臨時攔砂設施(如砂包、防溢座等)</td>
                                  <td><asp:DropDownList ID="DDLDTL072" runat="server" >
                                          <asp:ListItem Value=""></asp:ListItem>
                                          <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                                          <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                                          <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                                          <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                                          <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                                          <asp:ListItem Value="無此項">無此項</asp:ListItem>
                                      </asp:DropDownList><br/>
                                          <asp:TextBox ID="TXTDTL073" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL073_count','100');" placeholder="備註說明"  />
                                          <asp:Label ID="TXTDTL073_count" runat="server" Text="(0/100)" ForeColor="Red" />
                                  </td></tr>
                              <tr><td class="td-padding">8.其他</td>
                                  <td>
                                          <asp:TextBox ID="TXTDTL025" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL025_count','100');" placeholder="備註說明"  />
                                          <asp:Label ID="TXTDTL025_count" runat="server" Text="(0/100)" ForeColor="Red" />
                                      
                                      <div style="display:none;">
                                          <asp:DropDownList ID="DDLDTL024" runat="server" />
                                      </div>
                                  </td></tr>
                               </table>
                               <div class="flex">
                                  <%--<input type="button" value="回案件資訊" class="flex1" />--%>
                                  <input id="Button4" type="button" value="回前一頁" runat="server" class="flex2" />
                                  <input id="Button5" type="button" value="繼續填寫" runat="server" class="flex2" />
                                  <%--<input type="button" value="線上簽名" class="flex1" />--%>
                               </div>

                     </div>
                     <div role="tabpanel" class="tab-pane fade" id="A3">
                         <table class="swcchg-out checknonecss opentxt">
                                   <tr><th>（五）永久性防災措施</th>
                                       <%--<td></td>--%></tr>
                                   <tr><td class="td-padding">1.排水設施</td>
                                       <td><asp:DropDownList ID="DDLDTL026" runat="server" >
                                               <asp:ListItem Value=""></asp:ListItem>
                                               <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                                               <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                                               <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                                               <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                                               <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                                               <asp:ListItem Value="無此項">無此項</asp:ListItem>
                                           </asp:DropDownList><br/>
                                               <asp:TextBox ID="TXTDTL027" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL027_count','100');" placeholder="備註說明"  />
                                               <asp:Label ID="TXTDTL027_count" runat="server" Text="(0/100)" ForeColor="Red" />
                                       </td></tr>
                                   <tr><td class="td-padding">2.沉砂設施</td>
                                       <td><asp:DropDownList ID="DDLDTL028" runat="server" >
                                               <asp:ListItem Value=""></asp:ListItem>
                                               <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                                               <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                                               <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                                               <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                                               <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                                               <asp:ListItem Value="無此項">無此項</asp:ListItem>
                                           </asp:DropDownList><br/>
                                               <asp:TextBox ID="TXTDTL029" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL029_count','100');" placeholder="備註說明"  />
                                               <asp:Label ID="TXTDTL029_count" runat="server" Text="(0/100)" ForeColor="Red" /><br/>
                                       </td></tr>
                                   <tr><td class="td-padding">3.滯洪設施</td>
                                       <td><asp:DropDownList ID="DDLDTL030" runat="server" >
                                               <asp:ListItem Value=""></asp:ListItem>
                                               <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                                               <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                                               <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                                               <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                                               <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                                               <asp:ListItem Value="無此項">無此項</asp:ListItem>
                                           </asp:DropDownList><br/>
                                               <asp:TextBox ID="TXTDTL031" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL031_count','100');" placeholder="備註說明"  />
                                               <asp:Label ID="TXTDTL031_count" runat="server" Text="(0/100)" ForeColor="Red" /><br/>
                                       </td></tr>
                                   <tr><td class="td-padding">4.聯外排水</td>
                                       <td><asp:DropDownList ID="DDLDTL032" runat="server" >
                                               <asp:ListItem Value=""></asp:ListItem>
                                               <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                                               <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                                               <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                                               <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                                               <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                                               <asp:ListItem Value="無此項">無此項</asp:ListItem>
                                           </asp:DropDownList><br/>
                                               <asp:TextBox ID="TXTDTL033" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL033_count','100');" placeholder="備註說明"   />
                                               <asp:Label ID="TXTDTL033_count" runat="server" Text="(0/100)" ForeColor="Red" />
                                       </td></tr>
                                   <tr><td class="td-padding">5.擋土設施</td>
                                       <td><asp:DropDownList ID="DDLDTL034" runat="server" >
                                               <asp:ListItem Value=""></asp:ListItem>
                                               <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                                               <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                                               <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                                               <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                                               <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                                               <asp:ListItem Value="無此項">無此項</asp:ListItem>
                                           </asp:DropDownList><br/>
                                               <asp:TextBox ID="TXTDTL035" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL035_count','100');" placeholder="備註說明"   />
                                               <asp:Label ID="TXTDTL035_count" runat="server" Text="(0/100)" ForeColor="Red" />
                                       </td></tr>
                                   <tr><td class="td-padding">6.植生工程</td>
                                       <td><asp:DropDownList ID="DDLDTL036" runat="server" >
                                               <asp:ListItem Value=""></asp:ListItem>
                                               <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                                               <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                                               <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                                               <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                                               <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                                               <asp:ListItem Value="無此項">無此項</asp:ListItem>
                                           </asp:DropDownList><br/>
                                               <asp:TextBox ID="TXTDTL037" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL037_count','100');" placeholder="備註說明"   />
                                               <asp:Label ID="TXTDTL037_count" runat="server" Text="(0/100)" ForeColor="Red" />
                                       </td></tr>
                                   <tr><td class="td-padding">7.邊坡穩定措施</td>
                                       <td><asp:DropDownList ID="DDLDTL038" runat="server" >
                                               <asp:ListItem Value=""></asp:ListItem>
                                               <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                                               <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                                               <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                                               <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                                               <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                                               <asp:ListItem Value="無此項">無此項</asp:ListItem>
                                           </asp:DropDownList><br />
                                               <asp:TextBox ID="TXTDTL039" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL039_count','100');" placeholder="備註說明"   />
                                               <asp:Label ID="TXTDTL039_count" runat="server" Text="(0/100)" ForeColor="Red" />
                                       </td></tr>
                                   <tr><td class="td-padding">8.其他</td>
                                       <td><asp:DropDownList ID="DDLDTL040" runat="server" Visible="false" CssClass="none">
                                               <asp:ListItem Value=""></asp:ListItem>
                                               <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                                               <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                                               <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                                               <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                                               <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                                               <asp:ListItem Value="無此項">無此項</asp:ListItem>
                                           </asp:DropDownList>
                                               <asp:TextBox ID="TXTDTL041" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL041_count','100');" placeholder="備註說明"   />
                                               <asp:Label ID="TXTDTL041_count" runat="server" Text="(0/100)" ForeColor="Red" />
                                       </td></tr>
                               </table>
                               <div class="flex">
                                  <%--<input type="button" value="回案件資訊" class="flex1" />--%>
                                  <input id="Button2" type="button" value="回前一頁" runat="server" class="flex2" />
                                  <input id="Button3" type="button" value="繼續填寫" runat="server" class="flex2" />
                                  <%--<input type="button" value="線上簽名" class="flex1" />--%>
                               </div>


                     </div>
                       <div role="tabpanel" class="tab-pane fade" id="A4">

                           <table class="swcchg-out checknonecss opentxt">
                                    <tr><th>（六）承辦監造技師是否在場</th>
                                    <td><asp:DropDownList ID="DDLDTL042" runat="server" >
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="是">是</asp:ListItem>
                                            <asp:ListItem Value="技師代理">技師代理</asp:ListItem>
                                            <asp:ListItem Value="未出席，限期說明並通知大地處">未出席，限期說明並通知大地處</asp:ListItem>
                                        </asp:DropDownList><br/>
                                            <asp:TextBox ID="TXTDTL043" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL043_count','100');" placeholder="備註說明"   />
                                            <asp:Label ID="TXTDTL043_count" runat="server" Text="(0/100)" ForeColor="Red" />
                                    </td></tr>
                                    <tr><th>（七）是否備妥監造紀錄</th>
                                    <td><asp:DropDownList ID="DDLDTL044" runat="server" >
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="是">是</asp:ListItem>
                                            <asp:ListItem Value="否">否</asp:ListItem>
                                        </asp:DropDownList>
                                        <div style="display:none;">
                                            <asp:TextBox ID="TXTDTL045" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL045_count','100');" placeholder="備註說明"   />
                                            <asp:Label ID="TXTDTL045_count" runat="server" Text="(0/100)" ForeColor="Red" />
                                        </div>
                                  
                                        <asp:DropDownList ID="DDLDTL046" runat="server" Visible="false" >
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                                            <asp:ListItem Value="應補立界樁">應補立界樁</asp:ListItem>
                                            <asp:ListItem Value="無此項">無此項</asp:ListItem>
                                        </asp:DropDownList>
                                        <div style="display:none;">
                                            （八）災害搶救小組是否成立
                                            <asp:TextBox ID="TXTDTL047" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL047_count','100');" placeholder="備註說明" />
                                            <asp:Label ID="TXTDTL047_count" runat="server" Text="(0/100)" ForeColor="Red" />
                                        </div>
                                   </td></tr>
                              </table>
                             <div class="flex">
                                  <%--<input type="button" value="回案件資訊" class="flex1" />--%>
                                  <input id="Button6" type="button" value="回前一頁" runat="server" class="flex2" />
                                  <input id="Button7" type="button" value="繼續填寫" runat="server" class="flex2" />
                                  <%--<input type="button" value="線上簽名" class="flex1" />--%>
                               </div>
                       </div>
                       <div role="tabpanel" class="tab-pane fade" id="A5">
                           <table class="swcchg-out checknonecss opentxt">
                                    <tr><th>二、通知水土保持義務人及營造單位施工缺失改正事項</th>
                                    <td> <asp:Label ID="TXTDTL048" runat="server" TextMode="MultiLine"  onkeyup="textcount(this,'TXTDTL048_count','500');" MaxLength="500" />
                                             改正期限：
                                             <asp:TextBox ID="TXTDTL049" runat="server"  AUTOCOMPLETE="off"></asp:TextBox>
                                             <asp:CalendarExtender ID="TXTDTL049_CalendarExtender" runat="server" TargetControlID="TXTDTL049" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                             <span class="gray">（範例：2020-01-02）</span>
                                             </td></tr>
                                 
                                     <asp:Panel ID="PNDA3a" runat="server">
                                     <tr><th>
                                 三、未依計畫施作事項及改正期限：</th>
                                      <td>
                                     <asp:Label ID="LBDTL051" runat="server" TextMode="MultiLine" Text="" />
                                             <br />改正期限：
                                             <asp:TextBox ID="TXTDTL074" runat="server" width="120px" AUTOCOMPLETE="off"></asp:TextBox>
                                             <asp:CalendarExtender ID="TXTDTL074_CalendarExtender" runat="server" TargetControlID="TXTDTL074" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                             <span class="gray">（範例：2020-01-02）</span>
                                             </td></tr>
                                     </asp:Panel>
                                         
                                     <asp:Panel ID="PNDA3b" runat="server" Visible="false">
                                  <tr class="input-ML0">
                                  <th>
                                     三、前次施工檢查之改正事項及限期改正情形</th>
                                     <td>前次施工檢查注意事項
                                     <asp:TextBox ID="TXTDTL051" runat="server" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL051_count','1000');" />
                                     <asp:Label ID="TXTDTL051_count" runat="server" Text="(0/1000)" ForeColor="Red" /><br/><br/>
                                     現場改正情形<br/><br/>
                                     <asp:TextBox ID="TXTDTL052" runat="server" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL052_count','500');" />
                                     <asp:Label ID="TXTDTL052_count" runat="server" Text="(0/500)" ForeColor="Red" /><br/><br/>
                                     前次監督檢查缺失之複查&nbsp&nbsp&nbsp(&nbsp&nbsp&nbsp是否已改正：
                                     <asp:DropDownList ID="DDLDTL053" runat="server" />
                                     <asp:TextBox ID="TXTDTL054" runat="server" placeholder="其他說明" MaxLength="50" />&nbsp&nbsp&nbsp)</td></tr>
                                     </asp:Panel>
                                     <tr>
                                         <th>四、其他注意事項</th>
                                         <td>
                                     <asp:TextBox ID="TXTDTL050" runat="server" TextMode="MultiLine"  onkeyup="textcount(this,'TXTDTL050_count','500');" />
                                     <asp:Label ID="TXTDTL050_count" runat="server" Text="(0/500)" ForeColor="Red" />
                                         </td>
                                     </tr>
                                     <tr><th>五、檢查結果<span style="color: red;font-family:cursive;">＊</span></th>
                                         <td><asp:DropDownList ID="DDLDTL071" runat="server" CssClass="option">
                                              <asp:ListItem Value=""></asp:ListItem>
                                              <asp:ListItem Value="尚未施工">尚未施工</asp:ListItem>
                                              <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                                              <asp:ListItem Value="有施工缺失改善中">有施工缺失改善中</asp:ListItem>
                                              <asp:ListItem Value="缺失應改未改或未依計畫施工，通知大地處查處">缺失應改未改或未依計畫施工，通知大地處查處</asp:ListItem>
                                              <asp:ListItem Value="完工">完工</asp:ListItem>
                                              </asp:DropDownList>
                                         </td></tr>
                                </table>
                               <div class="flex">
                                  <%--<input type="button" value="回案件資訊" class="flex1" />--%>
                                  <input id="Button1" type="button" value="回前一頁" runat="server" class="flex2" />
                                  <%--<input id="Button9" type="button" value="繼續填寫" runat="server" class="flex2" />--%>
                                  <%--<input type="button" value="線上簽名" class="flex1" />--%>
                               </div>
                       </div>

                   </div>
                    <!-- Tab panes -->

                    

                    <div class="form-btnPWA">
                        <div class="tagbtn">
                          <span></span>
                          <span></span>
                          <span></span>
                          <span></span>
                        </div>
					</div>
                </div>
				</asp:Panel>
                 <!-- TAB2 打包區塊 end -->
                    
                 <!-- TAB3 打包區塊 start -->
                    <!--<input id="tab3" type="radio" name="tab"/-->
                    <!--<label for="tab3" class="fixed3">線上簽名</label>-->
                    <asp:Panel ID="tab3" runat="server" style="display:none;">
					<div class="tab_main"> <!--class="tab_content"-->
                       <table class="mbborder">
                         <tr><td colspan="2">
                             <b style="margin-bottom:5px; display:block;">二、相關單位及人員簽名</b>
                        <span class="box1"><asp:TextBox ID="TXTDTL055" runat="server" TextMode="MultiLine" style="display:none;" />
                        <asp:Button ID="TXTDTL056_changepanel" runat="server" Text="切換簽名方式" OnClientClick="ChangePanel(); return false" Visible="false"/>
                        </span>					
					    
                        <span class="box2" style="display:none;">
                        <asp:Image ID="TXTDTL056_img" runat="server" CssClass="imgUpload-l80" Visible="false" /><br/>
                        <asp:HyperLink ID="HyperLink056" runat="server" CssClass="imgUpload imgTP" Target="_blank"></asp:HyperLink><br/>
                        <asp:Panel ID="Panel5601" runat="server">
                        <br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/><br/>
                        <asp:TextBox ID="TXTDTL056" runat="server" Width="70px" Visible="false" />
                        <asp:FileUpload ID="TXTDTL056_fileupload" runat="server" />
                        <asp:Button ID="TXTDTL056_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL056_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL056_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL056_fileuploaddel_Click" />
						</asp:Panel>
						</span>
                        <asp:Panel ID="Panel5602_" runat="server">
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
                            <asp:HiddenField ID="hfImageData" runat="server" />
                            <div class="center">
								<!--<asp:Button ID="TXTDTL05602_Clear" runat="server" Text="重新簽名" OnClientClick="clearCanvas()" CssClass="retun" />&nbsp;&nbsp;&nbsp;
								<asp:Button ID="TXTDTL05602_Signsture" runat="server" Text="確認簽名並加入清單" OnClick="TXTDTL05602_Signsture_Click" CssClass="signok" UseSubmitBehavior="false" />-->
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
                        </asp:Panel>
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
						<div class="OFseg">
							<div class="lab">
							<div class="labcolor1"><div class="icon1"></div>原核定</div>
							<div class="labcolor2"><div class="icon2"></div>現場量測</div>
							</div>
							
							<asp:Label ID="GVMSG" runat="server" Text="查無資料" Visible="true" class="nodata"/>
							<div style="clear:both;"></div>
							<div class="detailsGrid">
								<h2 class="SWCfO openh2">水保設施項目<img src="../images/btn/btn-close.png" alt=""/></h2>
			
							<asp:GridView ID="SDIList" runat="server" CssClass="OFcheck AutoNewLine swcfacility_mb" AutoGenerateColumns="False" EmptyDataText="查無資料" ShowHeaderWhenEmpty="true"
								OnRowCommand="SDIList_RowCommand" OnDataBound="SDIList_DataBound">
								<Columns>
									<asp:BoundField DataField="SDIFD017" HeaderText="技師報備<br>施工完成" HeaderStyle-Width="7%" HtmlEncode="false" />
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
							<asp:TemplateField HeaderText="數量差異<br>百分比" HeaderStyle-Width="400px">
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
									<asp:TextBox ID="CHK004D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004D"))) %>' MaxLength="100" style="width:40px; text-align:center;" Visible="true" Enabled="false" />
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
							<asp:TemplateField HeaderText="尺寸差異<br>百分比" HeaderStyle-Width="400px">
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
				 
				 
               </div>
                <div style="display:none;">
                    檢查類型：<asp:Label ID="TXTDTL003" runat="server" text="施工監督檢查" Visible="false" />
                    身分證或營利事業統一編號：<asp:Label ID="LBSWC013ID" runat="server" Visible="false" />
                    執業執照字號：<asp:Label ID="LBSWC021OrgIssNo" runat="server" Visible="false" />
                    營利事業統一編號：<asp:Label ID="LBSWC021OrgGUINo" Visible="false" runat="server"/>
                    實施地點土地標示：<asp:TextBox ID="TXTDTL005" runat="server" width="100%" MaxLength="200" Visible="false" />
                </div>

                <table class="checkRecord-imgUpload_X none" style="border-top:none;">
                        <tr>
                            <td colspan="2">
                               六、說明：
                                <table class="txtbox">
                                  <tr>
                                      <th>（一）</th>
                                      <td>本項檢查係屬行政監督檢查，檢查困難、隱蔽或不影響水保設施正常功能者（如圖面未標示之尺寸、水溝蓋板之型式、滯洪沉沙池之告示牌…等）得免查驗，應由水土保持義務人及承辦監造技師負責。</td>
                                  </tr>
                                    <tr>
                                      <th>（二）</th>
                                      <td>承辦監造技師未能到場時，應以書面方式委任符合水土保持法規定之技師代理之。並檢附，不克出席之佐證。</td>
                                  </tr>
                                </table>

                               <br /><br />
                                 七、填表注意事項：<br />
                                <table class="txtbox">
                                  
                                  <tr>
                                      <th>（一）</th>
                                      <td>如有未依核定計畫施作情形，應說明與計畫不符事項。</td>
                                  </tr>
                                  <tr>
                                      <th>（二）</th>
                                      <td>前次監督檢查缺失及應注意事項之複查，請註明辦理情形及是否同意結案（或持續列管）。</td>
                                  </tr>
                                  <tr>
                                      <th>（三）</th>
                                      <td>滯洪、沉砂池檢查項目及標準如下：<br />
                                          <b style="font-size:20pt; vertical-align:middle;">◆</b><span>滯洪、沉砂量體：增減不得逾20%，且不得小於所須最小滯洪、沉砂量。</span><br />
                                          <b style="font-size:20pt; vertical-align:middle;">◆</b><span>放流口及溢洪口通水斷面積：增加不超過20%或減少不超過10%。</span>
                                      </td>
                                  </tr>
                                  <tr>
                                      <th>（四）</th>
                                      <td>植生工程檢查項目及標準如下：<br />
                                          <b style="font-size:20pt; vertical-align:middle;">◆</b><span>植生面積：增減不得逾20%。</span><br />
                                          <b style="font-size:20pt; vertical-align:middle;">◆</b><span>覆蓋率：</span><br />
                                          <span style="padding-left:20px; line-height:2;">a.以種子撒播及草皮鋪植等方式直接栽植者，以植被生長後之覆蓋率審認。</span><br />
                                          <span style="padding-left:20px; line-height:2;">b.以噴植、植生帶、土袋植生及草袋等配合資材方式栽植者，以資材覆蓋率審認。</span>
                                      </td>
                                  </tr>
                                    <tr>
                                        <th>（五）</th>
                                        <td>相片說明應與紀錄文字勾稽。</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                   
           
                <div class="inbox" style="display:none;">
                  <div class="leftimg">
                   <div class="imgtxt">現場相片一</div>
                    <div>
                      <div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL057" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL057_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL057_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL057_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL057_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL057_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:HyperLink ID="HyperLink057" runat="server" CssClass="imgUpload" Target="_blank" />
                        <asp:Image ID="TXTDTL057_img" runat="server" CssClass="imgUpload-l80" Visible="false" />
                        <br/>
                        <asp:TextBox ID="TXTDTL058" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/>
                    </div>
                </div>
                    
                <div class="rightimg">
                  <div class="imgtxt">現場相片二</div>
                   <div>        
                     <div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL059" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL059_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL059_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL059_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL059_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL059_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:HyperLink ID="HyperLink059" runat="server" CssClass="imgUpload" Target="_blank" />
                        <asp:Image ID="TXTDTL059_img" runat="server" CssClass="imgUpload-l80" Visible="false" />
                        <br/>
                        <asp:TextBox ID="TXTDTL060" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/>
                       </div>
                     </div>
                  </div>


                <div class="inbox" style="display:none;">
                  <div class="leftimg">
                     <div class="imgtxt">現場相片三</div>
                      <div>
                        <div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL061" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL061_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL061_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL061_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL061_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL061_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:HyperLink ID="HyperLink061" runat="server" CssClass="imgUpload" Target="_blank" />
                        <asp:Image ID="TXTDTL061_img" runat="server" CssClass="imgUpload-l80" Visible="false" />
                        <br/>
                        <asp:TextBox ID="TXTDTL062" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/>
                      </div>
                </div>


                 <div class="rightimg">
                   <div class="imgtxt">現場相片四</div>
                     <div>
                      <div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL063" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL063_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL063_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL063_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL063_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL063_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:HyperLink ID="HyperLink063" runat="server" CssClass="imgUpload" Target="_blank" />
                        <asp:Image ID="TXTDTL063_img" runat="server" CssClass="imgUpload-l80" Visible="false" />
                        <br/>
                        <asp:TextBox ID="TXTDTL064" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/>
                         </div>
                      </div>
                  </div>


                 <div class="inbox" style="display:none;">
                   <div class="leftimg">
                    <div class="imgtxt">現場相片五</div>
                    <div>
                    <div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL065" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL065_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL065_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL065_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL065_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL065_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:HyperLink ID="HyperLink065" runat="server" CssClass="imgUpload" Target="_blank" />
                        <asp:Image ID="TXTDTL065_img" runat="server" CssClass="imgUpload-l80" Visible="false" />
                        <br/>
                        <asp:TextBox ID="TXTDTL066" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/>
                   </div>
                </div>

                <div class="rightimg">
                  <div class="imgtxt">現場相片六</div>
                    <div>
                     <div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL067" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL067_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL067_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL067_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL067_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL067_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:HyperLink ID="HyperLink067" runat="server" CssClass="imgUpload" Target="_blank" />
                        <asp:Image ID="TXTDTL067_img" runat="server" CssClass="imgUpload-l80" Visible="false" />
                        <br/>
                        <asp:TextBox ID="TXTDTL068" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/>
                        </div>
                      </div>
                 </div>

                <table class="checkRecord-fileUpload" style="display:none;">
                <tr><td colspan="2">
                    <asp:Label ID="LBSWC005a" runat="server" CssClass="redn" Visible="false" />檢查現場照片 </td></tr>
                <tr><td colspan="4">附件</td></tr>
                <tr><td colspan="4">附件檔案上傳：
                        <asp:TextBox ID="TXTDTL070" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTDTL070_fileupload" runat="server" />
                        <asp:Button ID="TXTDTL070_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL070_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL070_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('檔案刪除後無法復原，請確認真的要刪除檔案!!!')" OnClick="TXTDTL070_fileuploaddel_Click" />
                        <br/><br/>
                        <span style="color:red;">檔案大小請小於 50 Mb，請上傳 pdf, odt, word 格式</span>
                        <br/><br/>
                        <asp:HyperLink ID="Link070" runat="server" Text ="其他附件檔案下載" Target="_blank" Visible="false" /></td></tr>
                    </table>
            </div>
        </div>

                <div class="PWAbottombtn">
                    <asp:ImageButton runat="server" UseSubmitBehavior="false" src="../../img/PWA_icon-09.svg" onclientclick="setValue();"/>
					<asp:ImageButton ID="SaveCase" runat="server" UseSubmitBehavior="false" src="../../img/PWA_icon-10.svg" OnClientClick="return chkInput('');" OnClick="SaveCase_Click"/>
				</div>
                <!--<br/><br/><br/>
                <div class="PWAbottombtn none">
					<asp:Button ID="GoOffline1" runat="server" Text="儲存草稿" UseSubmitBehavior="false" onclientclick="setValue();" class="PWAffline flexbtn1" />
					<asp:Button ID="SaveCase1" runat="server" Text="上傳至系統" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" class="PWAffline flexbtn2" />
                </div>-->

           <div class="go-top1">TOP</div>   

           <%--<div class="footer-s"></div>
           <div class="footer-s-green"></div>
           <div class="pwa_footer">
               <b>臺北市政府工務局大地工程處</b>
               <span>110臺北市信義區松德路300號3樓</span>
               <span>臺北市民當家熱線1999</span>
               <span>客服電話：02-27929328 陳小姐</span>
               <span>本系統由多維空間資訊有限公司開發維護</span>
           </div>
        --%>

           <div class="footer-b-brown none">
               <b><span class="span1">臺北市政府工務局大地工程處</span><br/>
                  <span class="span2">110臺北市信義區松德路300號3樓<br/> 　服務專線(02)27591109 <br/> 　  臺北市民當家熱線1999</span><br/>
                  <span class="span2">建議使用IE11(含)以上，Chrome或Firefox版本瀏覽器 資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                  <span class="span2">客服電話：02-27593001#3729 許先生 本系統由多維空間資訊有限公司開發維護 TEL：(02)27929328</span><br/>
				  <span class="span2">※為維護系統服務品質，本平台訂於每周三凌晨AM 4:00-6:30 進行系統維護更新，更新期間偶有瞬斷情形，敬請使用者避開該時段使用。謝謝！</span>

               </b>

           </div>

          
        <asp:Literal ID="error_msg" runat="server"></asp:Literal>

        <%--<script src="../../js/jquery-3.1.1.min.js"></script>--%>
        
        <%--<script src="../../js/inner.js"></script>--%>
        <%--<script src="../../js/allhref.js"></script>--%>
        

    </div>
     </div>
    </form>
    <script src="../../js/jquery-3.1.1.min.js"></script>
    <script src="../../js/boostrap.js"></script>
    <script src="../../js/inner.js?20220217"></script>
    
    </body>
</html>
