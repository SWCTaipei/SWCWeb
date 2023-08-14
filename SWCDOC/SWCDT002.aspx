<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCDT002.aspx.cs" Inherits="SWCDOC_SWCDT002" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>臺北市水土保持申請書件管理平台</title>
    <link rel="Shortcut Icon" type="image/x-icon" href="../images/logo-s.ico">
    <meta name="keywords" content="臺北市水土保持申請書件管理平台, 書件管理平台">
    <meta name="description" content="臺北市水土保持申請書件管理平台">
    <meta name="author" content="dorathy">
    <meta name="copyright" content="© 2017 臺北市水土保持申請書件管理平台">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge, chrome=1">
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    <link rel="stylesheet" type="text/css" href="../css/reset.css"/>
    <link rel="stylesheet" type="text/css" href="../css/all.css"/>
	<link rel="stylesheet" type="text/css" href="../css/all_PWA.css?202205240338"/>

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
            //jCHKValue01 = document.getElementById("TXTDTL002").value;

            //if (jCHKValue01.trim() == '') {
            //    alert('請輸入檢查日期');
            //    document.getElementById("TXTDTL002").focus();
            //    return false;
            //} else {
            //    if (!dateValidationCheck(jCHKValue01)) {
            //        document.getElementById("TXTDTL002").focus();
            //        return false;
            //    }
            //}
            if (jChkType == 'DataLock') {
                var r = confirm('確認送出後，即不可修改，請再次確認是否要完成送出。');
                return r;
            }
        }
    </script>
	<script type = "text/javascript">
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
      <div class="header-wrap-s">
        <div class="header header-s clearfix"><a href="HaloPage001.aspx" class="logo-s"></a>
          <div class="header-menu-s">
            <ul>
				<li><a href="#" title="系統操作手冊">系統操作手冊</a></li>
                <li>|</li>
                <li><a href="http://www.swc.taipei/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                <li>|</li>
                <asp:Panel ID="TitleLink01" runat="server" CssClass="last-divLi"><li><a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li></asp:Panel>
            </ul>
          </div>
        </div>

            <div class="header-s-green">
                <div class="header-s-green-nameWrap">
                    <span><asp:Literal ID="TextUserName" runat="server">您好</asp:Literal></span>
                </div>
            </div>
        </div>

        <div class="content-s">
            <div class="swcchg form">
                <h1>水土保持施工抽查紀錄</h1><br/>
                <table class="swcchg-outA">
                    <tr><th>施工監督表編號</th>
                        <td><asp:Label ID="LBDTL001" runat="server"/>
                            <asp:Label ID="LBSWC000" runat="server" Visible="false"/></td></tr>
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

                <br /><br />

                <table class="swcchg-out">
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
                <tr><td colspan="2">（四）其他注意事項：<br/><br/>
                        <asp:TextBox ID="TXTDTL020" runat="server" width="100%" Height="100px" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL020_count','500');" />
                        <asp:Label ID="TXTDTL020_count" runat="server" Text="(0/500)" ForeColor="Red" /></td></tr>
                </table>

              <table class="swcchg-imgUpload0">
                <tr><td style="border-right:1px #000 solid; border-left:1px #000 solid;" >二、相關單位及人員簽名<br/><br/>
                        <asp:TextBox ID="TXTDTL021" runat="server" width="100%" Height="100px" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL021_count','500');" />
                        <asp:Label ID="TXTDTL021_count" runat="server" Text="(0/500)" ForeColor="Red" />
						<asp:Image ID="TXTDTL022_img" runat="server" CssClass="imgUpload-l80" Visible="false" /></td></tr>
                <tr><td style="width:70% !impprtant;" id="id_pCanvas">
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
				<tr style="margin-bottom:none;"><td style="width:30%; border-top:none; border-bottom:none;"><span style="color:red;">※ 檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span>
						<br/><span style="color:red;">※ 上傳檔案請勿使用+、空格、/、\、?、%、#、&、=、!...等特殊符號(包含全形符號)</span><br/><br/>
                        <asp:HyperLink ID="Link022" runat="server" Visible="false"/><br/>
						<asp:TextBox ID="TXTDTL022" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTDTL022_fileupload" runat="server" />
                        <asp:Button ID="TXTDTL022_fileuploadok" runat="server" Text="上傳檔案並加入清單" OnClick="TXTDTL022_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL022_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請確認真的要刪除照片!!!')" OnClick="TXTDTL022_fileuploaddel_Click" Visible="false"/></td></tr>
				<tr style="border-top:none;"><td style="border-top:none;">
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
                <tr><td colspan="2">三、上傳檔案<br/><br/>
                        <asp:HyperLink ID="Link023" runat="server" Target="_blank" />
                        <asp:TextBox ID="TXTDTL023" runat="server" Width="70px" Visible="False" /><br/><br/>
                        <asp:FileUpload ID="TXTDTL023_fileupload" runat="server" />
                        <asp:Button ID="TXTDTL023_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL023_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL023_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('資料刪除後無法復原，確認是否刪除這筆資料?')" OnClick="TXTDTL023_fileuploaddel_Click" />
                        <br><span style="color:red;">※ 上傳格式限定為PDF檔案大小請於50mb以內</span>
						<br/><span style="color:red;">※ 上傳檔案請勿使用+、空格、/、\、?、%、#、&、=、!...等特殊符號(包含全形符號)</span></td></tr>
                </table>
				
				  </div>
      </div>

                <div class="PWAbottombtn1 btncenter">
					<asp:>
                    <asp:Button ID="DataLock" runat="server" Text="確認送出" OnClientClick="return chkInput('DataLock');" OnClick="DataLock_Click" class="PWAsavecase flexbtn1" />&nbsp&nbsp
                    <asp:Button ID="SaveCase" runat="server" Text="暫時儲存" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" class="PWAffline flexbtn2"/>
					<asp:Button ID="GoHomePage" runat="server" Text="返回瀏覽案件" OnClick="GoHomePage_Click" />
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
    </div>
    </form>
</body>
</html>
