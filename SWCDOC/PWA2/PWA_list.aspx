<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PWA_list.aspx.cs" Inherits="SWC001_PWA_list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>PWA_list</title>
    <link rel="stylesheet" type="text/css" href="../../css/reset.css" />
    <link rel="stylesheet" type="text/css" href="../../css/all.css?202210140431" />
	<link rel="stylesheet" type="text/css" href="../../css/iris.css" />
    <link rel="stylesheet" type="text/css" href="../../css/all_PWA.css?202210140431"/>
    <link rel="stylesheet" type="text/css" href="../../css/all_PWA_2.css?202210140431"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
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
		var db;
		var indexedDB = window.indexedDB || window.webkitIndexedDB || window.mozIndexedDB || window.msIndexedDB;
		var IDBTransaction = window.IDBTransaction || window.webkitIDBTransaction;
		
		var req = indexedDB.open("mydb123");
		
		req.onsuccess = function (e) {
			db = e.target.result;
			var txn = db.transaction("SWCDT002", "readonly");
			var store = txn.objectStore("SWCDT002");
			var a = "";
			var cursorRequest = store.openCursor();
			var cnt = 1;
			
			//沒有資料文字顯示
			var panel = document.getElementById("PanelTable_Temp");
			var div = document.createElement("div");
			var text = document.createTextNode("目前沒有暫存資料");
			div.appendChild(text);
			panel.appendChild(div);
					
			cursorRequest.addEventListener("success",function(event){
				var result = event.target.result
				//讀不到資料離開此function
				if(!result) return
				
				if(cnt == 1)
					panel.innerHTML = "";
					panel.style.border = "none";
				
				a = cnt+"|"+result.value.SWCNO+"|"+result.value.LBSWC002+"|"+result.value.LBSWC005;
				document.getElementById("TBtempdata").value = a;
				
				tempdata_1(a);
				cnt++;
				//讀下一筆資料
				result.continue();
			});
		};
		
		function tempdata_1(a){
			var data = a.split('|');
			var panel = document.getElementById("PanelTable_Temp");
			var table = document.createElement("table");
			table.setAttribute('class', 'listTB2');
			var tbody = document.createElement("tbody");
			
			//=====================================================
			//1
			var tr = document.createElement('tr');
			var td = document.createElement('td');
			td.setAttribute('colspan', '3');
			var text = document.createTextNode(data[2]);
			td.appendChild(text);
			
			tr.appendChild(td);
			
			tbody.appendChild(tr);
			//=====================================================
			//2
			tr = document.createElement('tr');
			var th = document.createElement('th');
			text = document.createTextNode(data[0] + "、");
			th.appendChild(text);
			
			tr.appendChild(th);
			
			tbody.appendChild(tr);
			
			
			var th = document.createElement('th');
			th.setAttribute('colspan', '2');
			text = document.createTextNode(data[3]);
			th.appendChild(text);
			
			tr.appendChild(th);
			
			tbody.appendChild(tr);
			//=====================================================
			//3
			tr = document.createElement('tr');
			td = document.createElement('td');
			text = document.createTextNode("");
			td.appendChild(text);
			
			tr.appendChild(td);
			
			tbody.appendChild(tr);
			
			
			
			td = document.createElement('td');
			text = document.createTextNode("水土保持施工抽查紀錄");
			td.appendChild(text);
			
			tr.appendChild(td);
			
			tbody.appendChild(tr);
			
			
			
			td = document.createElement('td');
			var a = document.createElement('a');
			a.setAttribute('target', '_blank');
			a.setAttribute('href', '../PWA_SWCDT002/SWCDT002PWA.aspx?SWCNO=' + data[1] + '&DTLNO=AddNew');
			text = document.createTextNode("詳情");
			a.appendChild(text);
			td.appendChild(a);
			
			tr.appendChild(td);
			
			tbody.appendChild(tr);
			
			//=====================================================
			
			
			table.appendChild(tbody);
			panel.appendChild(table);
		}
	</script>
	<style>
		body{
			background:#FFF;
		}
	</style>
	<script>
		function cleardata(){
			document.getElementById("TBSWC000").value = "";
			document.getElementById("TBSWC025").value = "";
			document.getElementById("TBSWC012").value = "";
			document.getElementById("TBSWC008").value = "";
			var x = true;
			var i = 1;
			while(x){
				if (document.getElementById("table_"+i)) {
					document.getElementById("table_"+i).className = "listTB";
				}else{
					x = false;
				}
				i++;
			}
		};
		function searchdata(){
			var x = true;
			var i = 1;
			var cnt = 0;
			while(x){
				if (document.getElementById("table_"+i)) {
					document.getElementById("table_"+i).className = "listTB";
					var table = document.getElementById("table_"+i);
					
					var swc000 = table.rows[3].cells[0].innerHTML;
					var swc025 = table.rows[4].cells[0].innerHTML;
					var swc012 = table.rows[5].cells[0].innerHTML;
					var swc008 = table.rows[6].cells[0].innerHTML;
					
					if(document.getElementById("TBSWC000").value !='' && swc000.indexOf(document.getElementById("TBSWC000").value) == -1){
						document.getElementById("table_"+i).className += " none";
					}
					if(document.getElementById("TBSWC025").value !='' && swc025.indexOf(document.getElementById("TBSWC025").value) == -1){
						document.getElementById("table_"+i).className += " none";
					}
					if(document.getElementById("TBSWC012").value !='' && swc012.indexOf(document.getElementById("TBSWC012").value) == -1){
						document.getElementById("table_"+i).className += " none";
					}
					if(document.getElementById("TBSWC008").value !='' && swc008.indexOf(document.getElementById("TBSWC008").value) == -1){
						document.getElementById("table_"+i).className += " none";
					}
					var cn = document.getElementById("table_"+i).className;
					if(cn.indexOf("none")!=-1){
						cnt ++;
					}
				}
				else{
					if(cnt == i-1)
					{
						alert("查無案件資料");
					}
					x = false;
				}
				i++;
			}
		};
	</script>
</head>
<body>
    <form id="form1" runat="server">
        <<div class="wrap-s">
        <div class="header_PWA">
		  <img class="logo" src="../../img/logo2.svg"/>
            <span>臺北市水土保持申請書件平台</span>
            <%--<img class="download" src="../../img/PWA_icon-08.svg"/>
		  <img class="logo_text" src="../../img/logo_text.svg"/>--%>
		</div>
        <%--<div class="hello">您好</div>  --%>
        <br><br>
		
		
			<div class="tab_css">
               <!-- TAB2 打包區塊 start -->
               <input id="tab2" type="radio" name="tab" checked="checked" class="none"/>
               <label for="tab2" class="tab2_css none">已暫存未送出</label>
                <div class="tab_content">
                  <div class="PWA_noup">
                   <div class="noup_title">已暫存未送出</div>
			   	    <asp:Panel ID="PanelTable_Temp" runat="server">
			   	    </asp:Panel>
                  </div>
               </div>
               <!-- TAB2 打包區塊 end -->
           </div>
		   
        </div>
		<asp:TextBox ID="TBtempdata" runat="server" style="border:none;width:0;" />
    </form>
</body>
</html>
