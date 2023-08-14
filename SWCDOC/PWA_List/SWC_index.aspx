<%@ Page Language="C#" AutoEventWireup="true" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="X-UA-Compatible" content="IE=9,IE=10,IE=11,IE=EDGE, chrome=1">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>臺北市水土保持申請書件管理平台</title>
    <link rel="Shortcut Icon" type="image/x-icon" href="../../images/logo-s.svg">
    <meta name="keywords" content="臺北市水土保持申請書件管理平台, 書件管理平台">
    <meta name="description" content="臺北市水土保持申請書件管理平台">
    <meta name="author" content="dorathy">
    <meta name="copyright" content="© 2017 臺北市水土保持申請書件管理平台">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge, chrome=1">
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    <link rel="stylesheet" type="text/css" href="../../css/reset.css"/>
    <link rel="stylesheet" type="text/css" href="../../css/all.css"/>
    <link rel="stylesheet" type="text/css" href="../../css/ad.css"/>
	<link rel="stylesheet" type="text/css" href="../../css/all_PWA.css?202208230547"/>
    <link rel="stylesheet" type="text/css" href="../../css/all_PWA_2.css?202208230547"/>
    <link rel="stylesheet" type="text/css" href="../../css/fontawesome-free-5.15.4-web/css/all.css"/>
	<link rel="manifest" href="manifest.json" />
	<script type="module">
		import 'https://cdn.jsdelivr.net/npm/@pwabuilder/pwaupdate';
		const el = document.createElement('pwa-update');
		document.body.appendChild(el);
	</script>
	<!--<script type="module" src="https://cdn.jsdelivr.net/npm/@pwabuilder/pwaupdate"></script>-->
	<script src="pwaupdate.js"></script>
	<script>
		if ('serviceWorker' in navigator) {
			navigator.serviceWorker.register('pwabuilder-sw.js')
			.then(reg => console.log('完成 SW 設定!', reg))
			.catch(err => console.log('Error!', err));
		}
	</script>
    <script src="../../js/jquery-3.1.1.min.js"></script>
    <script src="../../js/index.js"></script>
	<script type = "text/javascript">
		
		//原始資料
		var db;
		var db2;
		var db2_1;
		var db3;
		var db3_1;
		var db3_2;
		var db4;
		var db4_1;
		var db6;
		var db6_1;
		var db6_2;
		var indexedDB = window.indexedDB || window.webkitIndexedDB || window.mozIndexedDB || window.msIndexedDB;
		var IDBTransaction = window.IDBTransaction || window.webkitIDBTransaction;
	
		var req = indexedDB.open("mydb123");
	
		req.onsuccess = function (e) {
			db = e.target.result;
			db2 = e.target.result;
			db2_1 = e.target.result;
			db3 = e.target.result;
			db3_1 = e.target.result;
			db3_2 = e.target.result;
			db4 = e.target.result;
			db4_1 = e.target.result;
			db6 = e.target.result;
			db6_1 = e.target.result;
			db6_2 = e.target.result;
		};
	
		req.onerror = function () {
		};
	
		req.onupgradeneeded = function (e) {
			db = e.target.result;
			var store = db.createObjectStore("SWC", {keyPath: "SWCWHO"});
			db2 = e.target.result;
			var store2 = db2.createObjectStore("SWCDT002", {keyPath: "SWCNO"});
			db2_1 = e.target.result;
			var store2_1 = db2_1.createObjectStore("SWCDT002_1", {keyPath: ["SWCNO","DTLNO","NO"]});
			db3 = e.target.result;
			var store3 = db3.createObjectStore("SWCDT003", {keyPath: "SWCNO"});
			db3_1 = e.target.result;
			var store3_1 = db3_1.createObjectStore("SWCDT003_1", {keyPath: ["SWCNO","DTLNO","NO"]});
			db3_2 = e.target.result;
			var store3_2 = db3_2.createObjectStore("SWCDT003_2", {keyPath: ["SWCNO","DTLNO","NO"]});
			db4 = e.target.result;
			var store4 = db4.createObjectStore("SWCDT004", {keyPath: "SWCNO"});
			db4_1 = e.target.result;
			var store4_1 = db4_1.createObjectStore("SWCDT004_1", {keyPath: ["SWCNO","DTLNO","NO"]});
			db6 = e.target.result;
			var store6 = db6.createObjectStore("SWCDT006", {keyPath: "SWCNO"});
			db6_1 = e.target.result;
			var store6_1 = db6_1.createObjectStore("SWCDT006_1", {keyPath: ["SWCNO","DTLNO","NO"]});
			db6_2 = e.target.result;
			var store6_2 = db6_2.createObjectStore("SWCDT006_2", {keyPath: ["SWCNO","DTLNO","NO"]});
		};
		
		function setValue(where) {			
			//save data
			var txn = db.transaction("SWC", "readwrite");
			var store = txn.objectStore("SWC");
			
			var data = { 
				SWCWHO: "Me",
				WHERE: where
			};
			
			var req = store.delete("Me");
			req = store.put(data);
	
			req.onsuccess = function (e) {
				
			};
	
			req.onerror = function () {
			
			};
		};
		
		window.onload = function () {
			//原始資料
			var db;
			var indexedDB = window.indexedDB || window.webkitIndexedDB || window.mozIndexedDB || window.msIndexedDB;
			var IDBTransaction = window.IDBTransaction || window.webkitIDBTransaction;
	
			var req = indexedDB.open("mydb123");
	
			req.onsuccess = function (e) {
				db = e.target.result;
				
				var txn1 = db.transaction("SWC", "readwrite");
				var store1 = txn1.objectStore("SWC");
				
				
				var req1 = store1.get("Me");
				
				req1.onsuccess = function (e) {
					var data1 = e.target.result;
					if(data1){
						if(data1.WHERE == "1"){
							document.getElementById("te_chs").click();
						}
						if(data1.WHERE == "2"){
							document.getElementById("bu_chs").click();
						}
					}
				};
			};		
		};
	</script>
	<style>
		.input_disabled{
			background-color: #f3f3f3;
			border:#aaa 2px solid;
			color:#aaa;
		}
		
		.btn_disabled{
			border: 3px solid #aaa;
			background-color: transparent;
		}
	</style>
</head>

<body>

    <!--==== header =======================================-->
		<div class="header_PWA">
		  <img class="logo" src="../../img/logo2.svg"/>
		  <img class="logo_text" src="../../img/logo_text.svg"/>
		</div>

    <!--==== choose_identity ==============================-->
    <div class="choose_identity">
      <h1>請選擇身份</h1>
      <div class="choose_all te_color_all">
        <input class="choose_input te_input" type="radio" name="type" id="te_chs" onclick="setValue('1');"/>
        <label class="choose_label" for="te_chs">
          <span class="choose_button te_color"></span>
          技師/審查檢查委員
        </label>
      </div>
      <div class="choose_all bu_color_all">
        <input class="choose_input bu_input" type="radio" name="type" id="bu_chs" onclick="setValue('2');"/>
        <label class="choose_label" for="bu_chs">
          <span class="choose_button bu_color"></span>
          業務單位
        </label>
      </div>
    </div>

    <!--==== choose_wifi ===========================================-->
    <div class="wifi_wrap wifi_te">
      <div class="choose_wifi">
        <div class="wifi_title">
          <h1 class="wifi_title_yes active">有網路</h1>
          <h1 class="wifi_title_no">無網路</h1>
        </div>
        <div class="wifi_container wifi_yes">
          <img src="../../images/btn/wifi_te.svg" alt="wifi圖示" />
          <span>使用需有網路<br />第一次使用請由此進入</span>
          <div class="btn">
            <input type="button" class="login_btn te" value="登入" title="技師/審查檢查委員（初次登入）" onclick="window.open('https://swc.taipei/Default.aspx?PWA=1', '_blank')">
          </div>
        </div>
        <div class="wifi_container wifi_no">
          <img src="../../images/btn/wifi_no.svg" alt="wifi圖示" />
          <span>已登入過並有暫存案件<br />無網路時使用</span>
          <div class="btn">
            <input type="button" class="login_btn no" value="登入" title="技師/審查檢查委員（已登入過）" onclick="window.open('https://swc.taipei/SWCDOC/PWA1/PWA_list.aspx', '_blank')">
          </div>
        </div>
      </div>
    </div>
    <div class="wifi_wrap wifi_bu">
      <div class="choose_wifi">
        <div class="wifi_title">
          <h1 class="wifi_title_yes bu active">有網路</h1>
          <h1 class="wifi_title_no">無網路</h1>
        </div>
        <div class="wifi_container wifi_yes">
          <img src="../../images/btn/wifi_bu.svg" alt="wifi圖示" />
          <span>使用需有網路<br />第一次使用請由此進入</span>
          <div class="btn">
            <input type="button" class="login_btn bu" value="登入" title="業務單位（初次登入）" onclick="window.open('https://swc.taipei/Default.aspx?PWA=2', '_blank')">
          </div>
        </div>
        <div class="wifi_container wifi_no">
          <img src="../../images/btn/wifi_no.svg" alt="wifi圖示" />
          <span>已登入過並有暫存案件<br />無網路時使用</span>
          <div class="btn">
            <input type="button" class="login_btn no" value="登入" title="業務單位（已登入過）"onclick="window.open('https://swc.taipei/SWCDOC/PWA2/PWA_list.aspx', '_blank')">
          </div>
        </div>
      </div>
    </div>
</body>
  <script src="../../js/jquery-3.1.1.min.js"></script>
  <script src="../../js/alljq2.js"></script>
</html>

