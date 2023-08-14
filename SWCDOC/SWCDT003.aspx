<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCDT003.aspx.cs" Inherits="SWCDOC_SWCDT003" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>臺北市水土保持申請書件管理平台</title>
    <link rel="Shortcut Icon" type="image/x-icon" href="../images/logo-s.ico" />
    <meta name="keywords" content="臺北市水土保持申請書件管理平台, 書件管理平台" />
    <meta name="description" content="臺北市水土保持申請書件管理平台" />
    <meta name="author" content="dobubu" />
    <meta name="copyright" content="© 2017 臺北市水土保持申請書件管理平台" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge, chrome=1" />
    <meta name="viewport" content="width=device-width" />
    <link rel="stylesheet" type="text/css" href="../css/reset.css" />
    <link rel="stylesheet" type="text/css" href="../css/all.css" />
	<link rel="stylesheet" type="text/css" href="../css/iris.css" />

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
            jCHKValue01 = document.getElementById("TXTDTL002").value;

            if (jCHKValue01.trim() == '') {
                alert('請輸入檢查日期');
                document.getElementById("TXTDTL002").focus();
                return false;
            }
            if (jChkType == 'DataLock') {
                var r = confirm('確認送出後，即不可修改，請再次確認是否要完成送出。');
                return r;
            }
        }
    </script>
	
    <!--網頁線上簽名start-->
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://cdn.rawgit.com/mobomo/sketch.js/master/lib/sketch.min.js" type="text/javascript"></script>
    <script>
        var paintpoints = new Array();
        var paintpoints_content = new Array();
        var canvas;
        var context;
        var paint = false;
        var lastPoint;

        window.onload = function () {
            canvas = document.getElementById("pCanvas");
            context = canvas.getContext("2d");
			
			canvas.width = $(id_pCanvas).width();
			canvas.height = 200;

            canvas.addEventListener("touchstart", touchstart, false);
            canvas.addEventListener("touchend", touchend, false);
            canvas.addEventListener("touchmove", touchmove, false);
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
        }
        function clearCanvas() {
            context.clearRect(0, 0, canvas.width, canvas.height);
        }

        function sendData() {
            var image = document.getElementById("pCanvas").toDataURL("image/png");
            image = image.replace('data:image/png;base64,', '');
            $.ajax({
                type: 'POST',
                url: 'WebService.asmx/UploadImage',
                data: '{ "imageData" : "' + image + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {
                    alert('Image saved successfully !');
                }
            });
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
        .auto-style3 {
            font-size: xx-large;
            color: #FF3300;
        }

        .auto-style4 {
            font-size: xx-large;
            color: #0011FF;
        }
    </style>
    <script type="text/javascript">
        function ChangePanel() {
            //var panel5601 = document.getElementById("Panel5601");
            //var panel5602 = document.getElementById("Panel5602");
            //var hyperlink056 = document.getElementById("HyperLink056");
			//
            //if (panel5601.style.display == "none") {
            //    panel5601.style.display = "";
            //    panel5602.style.display = "none";
            //    hyperlink056.style.display = "";
            //}
            //else {
            //    panel5601.style.display = "none";
            //    panel5602.style.display = "";
            //    hyperlink056.style.display = "none";
            //}
        }
    </script>
	<script>
        //$(document).ready(function redirectPage() {
        //    var panel5601 = document.getElementById("Panel5601");
        //    var panel5602 = document.getElementById("Panel5602");
		//
        //    var W = document.body.clientWidth;
        //    var H = document.body.clientHeight;
        //    if (W >= 996) {
        //        panel5601.style.display = "";
        //        panel5602.style.display = "none";
        //    }
        //    else {
        //        panel5601.style.display = "none";
        //        panel5602.style.display = "";
        //    }
        //});
    </script>
    <!--網頁線上簽名end-->
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
                        <asp:Panel ID="LogOutLink" runat="server" Visible="true"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li></asp:Panel>
                    </ul>
                </div>
            </div>
            
            <div class="header-s-green">
                <div class="header-s-green-nameWrap">
                    <span><asp:Literal ID="TextUserName" runat="server">大雷包，您好</asp:Literal></span>
                </div>
            </div>

        </div>
        <div class="content-t">
            <div class="checkRecord form">
                <h1>水土保持施工監督檢查紀錄</h1>

                <div class="detailsMenu-btn">
                    <asp:ImageButton ID="OutPdf" runat="server" title="輸出PDF" ImageUrl="../images/btn/icon_exportpdf.png" OnClick="OutPdf_Click" Visible="false" />
                </div>

                <br/><br/>

                <table class="checkRecord-out">
                    <tr><td>施工監督表編號</td>
                        <td><asp:Label ID="LBDTL001" runat="server"/>
                            <asp:Label ID="LBSWC000" runat="server" Visible="false" />
                            <asp:Label ID="LBSWC002" runat="server" Visible="false" />
                            <asp:Label ID="LBSWC012" runat="server" Visible="false" />
                            <asp:Label ID="LBSWC025" runat="server" Visible="false" /></td></tr>
                    <tr><td>檢查日期<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:TextBox ID="TXTDTL002" runat="server" width="120px" autocomplete="off" ></asp:TextBox>
                            <asp:CalendarExtender ID="TXTDTL002_CalendarExtender" runat="server" TargetControlID="TXTDTL002" Format="yyyy-MM-dd"></asp:CalendarExtender>
                            <span class="gray">（範例：2020-01-02）</span>
                        </td></tr>
                    <tr><td>檢查結果<span style="color: red;font-family:cursive;">＊</span></td>
                        <td><asp:DropDownList ID="DDLDTL071" runat="server">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="尚未施工">尚未施工</asp:ListItem>
                                <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                                <asp:ListItem Value="有施工缺失改善中">有施工缺失改善中</asp:ListItem>
                                <asp:ListItem Value="缺失應改未改或未依計畫施工，通知大地處查處">缺失應改未改或未依計畫施工，通知大地處查處</asp:ListItem>
                                <asp:ListItem Value="完工">完工</asp:ListItem>
                            </asp:DropDownList></td></tr>
                    <tr><td>檢查單位</td>
                        <td><asp:Label ID="TXTDTL004" runat="server" width="100%" /></td></tr>
                    <tr><td>面積</td>
                        <td><asp:Label ID="LBSWC023" runat="server" /> 公頃</td></tr>
                </table>

                <br/><br/>

                <table class="checkRecord_sarea">
                <tr><td rowspan="7" class="CR01">水土保持書件</td>
                    <td class="CRTT">計畫名稱</td>
                    <td><asp:Label ID="LBSWC005" runat="server"/>
                        （<asp:Label ID="LBSWC007" runat="server"/>）</td></tr>
                <tr><td class="CRTT">核定日期文號</td>
                    <td>臺北市政府
                        <asp:Label ID="LBSWC038" runat="server"/>
                        <asp:Label ID="LBSWC039" runat="server"/>
                        函</td></tr>
                <tr><td class="CRTT">水土保持施工許可證日期文號</td>
                    <td>臺北市政府
                        <asp:Label ID="LBSWC043" runat="server"/>
                        <asp:Label ID="LBSWC044" runat="server"/>
                        函</td></tr>
                <tr><td class="CRTT">開工日期</td>
                    <td><asp:Label ID="LBSWC051" runat="server"/></td></tr>
                <tr><td class="CRTT">預定完工日期</td>
                    <td><asp:Label ID="LBSWC052" runat="server"/></td></tr>
                <tr><td class="CRTT">工程進度</td>
                    <td><asp:Label ID="LBSWCO01" runat="server"/></td></tr>
                <tr><td class="CRTT">檢查結果</td>
                    <td><asp:Label ID="LBSWC071" runat="server"/></td></tr>
                <tr><td rowspan="3" class="CR01">水土保持義務人</td>
                    <td class="CRTT">姓名或名稱</td>
                    <td><asp:Label ID="LBSWC013" runat="server"/></td></tr>
                <tr><td class="CRTT">住居所或營業所</td>
                    <td><asp:Label ID="LBSWC014" runat="server"/></td></tr>
                <tr><td class="CRTT">電話</td>
                    <td><asp:Label ID="LBSWC013TEL" runat="server"/></td></tr>
                <tr><td rowspan="4" class="CR01">承辦監造技師</td>
                    <td class="CRTT">姓名</td>
                    <td><asp:Label ID="LBSWC021" runat="server"/></td></tr>
                <tr><td class="CRTT">執業機構名稱</td>
                    <td><asp:Label ID="LBSWC021Name" runat="server"/></td></tr>
                <tr><td class="CRTT">執業機構地址</td>
                    <td><asp:Label ID="LBSWC021OrgAddr" runat="server"/></td></tr>
                <tr><td class="CRTT">電話</td>
                    <td><asp:Label ID="LBSWC021OrgTel" runat="server"/></td></tr>
                </table>
                
                <div style="display:none;">
                    檢查類型：<asp:Label ID="TXTDTL003" runat="server" text="施工監督檢查" Visible="false" />
                    身分證或營利事業統一編號：<asp:Label ID="LBSWC013ID" runat="server" Visible="false" />
                    執業執照字號：<asp:Label ID="LBSWC021OrgIssNo" runat="server" Visible="false" />
                    營利事業統一編號：<asp:Label ID="LBSWC021OrgGUINo" Visible="false" runat="server"/>
                    實施地點土地標示：<asp:TextBox ID="TXTDTL005" runat="server" width="100%" MaxLength="200" Visible="false" />
                </div>

                <table class="checkRecord_TB">
                <tr><td class="firstw">一、檢查項目</td>
                    <td style="text-align: center;">檢查結果</td></tr>
                <tr><td>（一）水土保持施工告示牌</td><!--20211018修改同步下方選項-->
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
                            <asp:TextBox ID="TXTDTL007" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL007_count','100');" />
                            <br/><asp:Label ID="TXTDTL007_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td>（二）開發範圍界樁</td><!--20211018修改同步下方選項-->
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
                            <asp:TextBox ID="TXTDTL009" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL009_count','100');" />
                            <br/><asp:Label ID="TXTDTL009_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td>（三）開挖整地範圍界樁</td><!--20211018修改同步下方選項-->
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
                            <asp:TextBox ID="TXTDTL011" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL011_count','100');" />
                            <br/><asp:Label ID="TXTDTL011_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td>（四）臨時性防災措施</td>
                    <td></td></tr>
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
                            <asp:TextBox ID="TXTDTL013" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL013_count','100');" />
                            <br/><asp:Label ID="TXTDTL013_count" runat="server" Text="(0/100)" ForeColor="Red" />
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
                            <asp:TextBox ID="TXTDTL015" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL015_count','100');" />
                            <br/><asp:Label ID="TXTDTL015_count" runat="server" Text="(0/100)" ForeColor="Red" />
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
                            <asp:TextBox ID="TXTDTL017" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL017_count','100');" />
                            <br/><asp:Label ID="TXTDTL017_count" runat="server" Text="(0/100)" ForeColor="Red" />
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
                            <asp:TextBox ID="TXTDTL019" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL019_count','100');" />
                            <br/><asp:Label ID="TXTDTL019_count" runat="server" Text="(0/100)" ForeColor="Red" />
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
                            <asp:TextBox ID="TXTDTL021" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL021_count','100');" />
                            <br/><asp:Label ID="TXTDTL021_count" runat="server" Text="(0/100)" ForeColor="Red" />
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
                            <asp:TextBox ID="TXTDTL023" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL023_count','100');" />
                            <br/><asp:Label ID="TXTDTL023_count" runat="server" Text="(0/100)" ForeColor="Red" />
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
                            <asp:TextBox ID="TXTDTL073" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL073_count','100');" />
                            <br/><asp:Label ID="TXTDTL073_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">8.其他</td>
                    <td>
                            <asp:TextBox ID="TXTDTL025" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL025_count','100');" />
                            <br/><asp:Label ID="TXTDTL025_count" runat="server" Text="(0/100)" ForeColor="Red" />
                        
                        <div style="display:none;">
                            <asp:DropDownList ID="DDLDTL024" runat="server" />
                        </div>
                    </td></tr>
                <tr><td>（五）永久性防災措施</td>
                    <td></td></tr>
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
                            <asp:TextBox ID="TXTDTL027" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL027_count','100');" />
                            <br/><asp:Label ID="TXTDTL027_count" runat="server" Text="(0/100)" ForeColor="Red" />
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
                            <asp:TextBox ID="TXTDTL029" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL029_count','100');" />
                            <br/><asp:Label ID="TXTDTL029_count" runat="server" Text="(0/100)" ForeColor="Red" /><br/>
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
                            <asp:TextBox ID="TXTDTL031" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL031_count','100');" />
                            <br/><asp:Label ID="TXTDTL031_count" runat="server" Text="(0/100)" ForeColor="Red" /><br/>
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
                            <asp:TextBox ID="TXTDTL033" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL033_count','100');"  />
                            <br/><asp:Label ID="TXTDTL033_count" runat="server" Text="(0/100)" ForeColor="Red" />
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
                            <asp:TextBox ID="TXTDTL035" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL035_count','100');"  />
                            <br/><asp:Label ID="TXTDTL035_count" runat="server" Text="(0/100)" ForeColor="Red" />
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
                            <asp:TextBox ID="TXTDTL037" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL037_count','100');"  />
                            <br/><asp:Label ID="TXTDTL037_count" runat="server" Text="(0/100)" ForeColor="Red" />
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
                        </asp:DropDownList><br/>
                            <asp:TextBox ID="TXTDTL039" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL039_count','100');"  />
                            <br/><asp:Label ID="TXTDTL039_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td class="td-padding">8.其他</td>
                    <td><asp:DropDownList ID="DDLDTL040" runat="server" Visible="false" >
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                            <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                            <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                            <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                            <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                            <asp:ListItem Value="無此項">無此項</asp:ListItem>
                        </asp:DropDownList><br/>
                            <asp:TextBox ID="TXTDTL041" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL041_count','100');"  />
                            <br/><asp:Label ID="TXTDTL041_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td>（六）承辦監造技師是否在場</td>
                    <td><asp:DropDownList ID="DDLDTL042" runat="server" >
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="是">是</asp:ListItem>
                            <asp:ListItem Value="技師代理">技師代理</asp:ListItem>
                            <asp:ListItem Value="未出席，限期說明並通知大地處">未出席，限期說明並通知大地處</asp:ListItem>
                        </asp:DropDownList><br/>
                            <asp:TextBox ID="TXTDTL043" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL043_count','100');"  />
                            <br/><asp:Label ID="TXTDTL043_count" runat="server" Text="(0/100)" ForeColor="Red" />
                    </td></tr>
                <tr><td>（七）是否備妥監造紀錄</td>
                    <td><asp:DropDownList ID="DDLDTL044" runat="server" >
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="是">是</asp:ListItem>
                            <asp:ListItem Value="否">否</asp:ListItem>
                        </asp:DropDownList>
                        <div style="display:none;">
                            <asp:TextBox ID="TXTDTL045" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL045_count','100');"  />
                            <br/><asp:Label ID="TXTDTL045_count" runat="server" Text="(0/100)" ForeColor="Red" />
                        </div>

                        <asp:DropDownList ID="DDLDTL046" runat="server" Visible="false" >
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                            <asp:ListItem Value="應補立界樁">應補立界樁</asp:ListItem>
                            <asp:ListItem Value="無此項">無此項</asp:ListItem>
                        </asp:DropDownList>
                        <div style="display:none;">
                            （八）災害搶救小組是否成立
                            <asp:TextBox ID="TXTDTL047" runat="server" MaxLength="100" onkeyup="textcount(this,'TXTDTL047_count','100');"/>
                            <br/><asp:Label ID="TXTDTL047_count" runat="server" Text="(0/100)" ForeColor="Red" />
                        </div>
                    </td></tr>
                </table>
                <table class="CR02">
                <tr><td colspan="3">
                    二、通知水土保持義務人及營造單位施工缺失改正事項<br/><br/>
                        <asp:Label ID="TXTDTL048" runat="server" TextMode="MultiLine" Height="100" Width="100%" onkeyup="textcount(this,'TXTDTL048_count','500');" MaxLength="500" />
                                <br/><br/>改正期限：
                                <asp:TextBox ID="TXTDTL049" runat="server" width="120px" AUTOCOMPLETE="off"></asp:TextBox>
                                <asp:CalendarExtender ID="TXTDTL049_CalendarExtender" runat="server" TargetControlID="TXTDTL049" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                <span class="gray">（範例：2020-01-02）</span>
                                </td></tr>
                    
                        <asp:Panel ID="PNDA3a" runat="server">
                <tr><td colspan="3">
                    三、未依計畫施作事項及改正期限：<br/><br/>
                        <asp:Label ID="LBDTL051" runat="server" TextMode="MultiLine" Text="" Height="100" Width="100%" />
                                <br/><br/>改正期限：
                                <asp:TextBox ID="TXTDTL074" runat="server" width="120px" AUTOCOMPLETE="off"></asp:TextBox>
                                <asp:CalendarExtender ID="TXTDTL074_CalendarExtender" runat="server" TargetControlID="TXTDTL074" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                <span class="gray">（範例：2020-01-02）</span>
                                </td></tr>
                        </asp:Panel>
                            
                        <asp:Panel ID="PNDA3b" runat="server" Visible="false">
                <tr class="input-ML0">
                    <td colspan="3">
                        三、前次施工檢查之改正事項及限期改正情形<br/><br/>
                        前次施工檢查注意事項<br/><br/>
                        <asp:TextBox ID="TXTDTL051" runat="server" TextMode="MultiLine" Height="100" Width="100%" onkeyup="textcount(this,'TXTDTL051_count','1000');" />
                        <asp:Label ID="TXTDTL051_count" runat="server" Text="(0/1000)" ForeColor="Red" /><br/><br/>
                        現場改正情形<br/><br/>
                        <asp:TextBox ID="TXTDTL052" runat="server" TextMode="MultiLine" Height="100" Width="100%" onkeyup="textcount(this,'TXTDTL052_count','500');" />
                        <asp:Label ID="TXTDTL052_count" runat="server" Text="(0/500)" ForeColor="Red" /><br/><br/>
                        前次監督檢查缺失之複查&nbsp&nbsp&nbsp(&nbsp&nbsp&nbsp是否已改正：
                        <asp:DropDownList ID="DDLDTL053" runat="server" />
                        <asp:TextBox ID="TXTDTL054" runat="server" placeholder="其他說明" MaxLength="50" />&nbsp&nbsp&nbsp)</td></tr>
                        </asp:Panel>
                        <tr>
                            <td colspan="3">四、其他注意事項
                                <br />
                                <br />
                        <asp:TextBox ID="TXTDTL050" runat="server" TextMode="MultiLine" Height="100" Width="100%" onkeyup="textcount(this,'TXTDTL050_count','500');" />
                        <asp:Label ID="TXTDTL050_count" runat="server" Text="(0/500)" ForeColor="Red" />
                            </td>
                        </tr>
                </table>

                <table class="checkRecord-imgUpload_X" style="border-top:none;">
                <tr><td style="border-top:none;">五、相關單位及人員簽名<br/><br/>
                        <span class="box1">
							<asp:TextBox ID="TXTDTL055" runat="server" TextMode="MultiLine" />
							<asp:Button ID="TXTDTL056_changepanel" runat="server" Text="切換簽名方式" OnClientClick="ChangePanel(); return false" Visible="false" />
                        </span></td></tr>
				<tr><td style="width:70% !impprtant; border-bottom:none;" id="id_pCanvas">						
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
						<asp:Button ID="btn1" runat="server" UseSubmitBehavior="false" onclientclick="return clearCanvas();" Text="重新簽名" class="retunsign" />
						<asp:Button ID="btn2" runat="server" Text="確認簽名並加入清單" OnClientClick="CanvasToImage();" OnClick="btn2_Click" />
						<asp:TextBox ID="hfImageData" runat="server" style="display:none;"/></td></tr>
				
				<tr><td style="border-top:none; border-bottom:none;"><span class="box2">
                        <asp:Image ID="TXTDTL056_img" runat="server" CssClass="imgUpload-l80" Visible="false" />
                        <asp:HyperLink ID="HyperLink056" runat="server" CssClass="imgUpload imgTP" Target="_blank" Visible="false"></asp:HyperLink>
                        <br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span>
						<br/><span style="color:red;">※ 上傳檔案請勿使用+、空格、/、\、?、%、#、&、=、!...等特殊符號(包含全形符號)</span><br/><br/>
                        <asp:TextBox ID="TXTDTL056" runat="server" Width="70px" Visible="false" />
                        <asp:FileUpload ID="TXTDTL056_fileupload" runat="server" />
                        <asp:Button ID="TXTDTL056_fileuploadok" runat="server" Text="上傳檔案並加入清單" OnClick="TXTDTL056_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL056_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL056_fileuploaddel_Click" Visible="false" />
						</span></td></tr>
						
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
                                      <td>承辦監造技師未能到場時，應以書面方式委任符合水土保持法規定之技師代理之。</td>
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
                    <br />
                    <br />
           
                <div class="inbox">
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


                <div class="inbox">
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


                 <div class="inbox">
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

                <table class="checkRecord-fileUpload">
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
              <div class="OFseg">   
               <div style="float:left"> <img src="../images/btn/btn005-17.png?2021" alt="" /></div>
                <div class="lab">
                   <div class="labcolor1"><div class="icon1"></div>原核定</div>
                   <div class="labcolor2"><div class="icon2"></div>現場量測</div>
                </div>
                
                <asp:Label ID="GVMSG" runat="server" Text="查無資料" Visible="true" class="nodata"/>
				<div style="clear:both;"></div>
                   <div class="detailsGrid SWCfOh2">
                      <h2 class="SWCfO openh2">水保設施項目<img src="../images/btn/btn-close.png" alt=""/></h2>

                <asp:GridView ID="SDIList" runat="server" CssClass="OFcheck AutoNewLine" AutoGenerateColumns="False" Height="50" EmptyDataText="查無資料" ShowHeaderWhenEmpty="true"
                    OnRowCommand="SDIList_RowCommand" OnDataBound="SDIList_DataBound">
                    <Columns>
                        <asp:BoundField DataField="SDIFD017" HeaderText="技師報備<br>施工完成" HeaderStyle-Width="20%" HtmlEncode="false" />
                        <asp:BoundField DataField="SDIFD003" HeaderText="水土保持設施類別" HeaderStyle-Width="5%" />
                        <asp:BoundField DataField="SDIFD004" HeaderText="設施名稱<br>（位置或編號）"  HtmlEncode="false" />
                        <asp:BoundField DataField="SDIFD005" HeaderText="設施型式" HeaderStyle-Width="10%" />
                <asp:TemplateField HeaderText="數" HeaderStyle-Width="10px">
                    <ItemTemplate>
                        <asp:Label ID="SDILB006" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDIFD006"))) %>' Visible="true"></asp:Label>
                        <asp:Label ID="SDILB006D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDIFD006D"))) %>' Visible="true"></asp:Label>
                        <asp:TextBox ID="CHK001" runat="server" onkeyup="chkdecimal(this)" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001"))) %>' style="width:40px; text-align:center;" Visible="true" />
                        <asp:Label ID="A1" runat="server" Text='~' Visible="true"></asp:Label>
                        <asp:TextBox ID="CHK001_1" runat="server" onkeyup="chkdecimal(this)" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001_1"))) %>' style="width:40px; text-align:center;" Visible="true" />
                        <asp:TextBox ID="CHK001D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001D"))) %>' MaxLength="20" style="width:40px; text-align:center;" Visible="true" Enabled="false" />
                        <asp:TextBox ID="RCH001" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001"))) %>' style="width:40px; text-align:center;" Visible="false" />
                        <asp:TextBox ID="RCH001_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001_1"))) %>' style="width:40px; text-align:center;" Visible="false" />
                        <asp:TextBox ID="RCH001D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001D"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
				<asp:BoundField DataField="SDIFD007" HeaderText="量" HeaderStyle-Width="10px" />
                <asp:TemplateField HeaderText="數量差異百分比" >
                    <ItemTemplate>
                        <asp:Label ID="LBCHK002" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK002"))) %>' Visible="true"></asp:Label>
                        <asp:TextBox ID="TXTCHK002" runat="server" onkeyup="chkdecimal(this)" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK002"))) %>' style="width:40px; text-align:center;" Visible="true" />
                        <asp:Label ID="LBCHK002pers" runat="server" Text='％' Visible="true"></asp:Label>
                        <asp:Label ID="LBCHK002_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK002_1"))) %>' Visible="true"></asp:Label>
                        <asp:TextBox ID="TXTCHK002_1" runat="server" onkeyup="chkdecimal(this)" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK002_1"))) %>' MaxLength="20" style="width:40px; text-align:center;" Visible="true" />
						<asp:Label ID="LBCHK002pers_1" runat="server" Text='％' Visible="true"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
				<asp:BoundField DataField="SDIFD008" HeaderText="檢核項目" HeaderStyle-Width="580px" />
                <asp:TemplateField HeaderText="" HeaderStyle-Width="60px">
                    <ItemTemplate>
                        <asp:Label ID="ITNONE03" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDIFD012"))) %>' Visible="true"></asp:Label>
                        <asp:TextBox ID="CHK004" runat="server" onkeyup="chkdecimal(this)" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004"))) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false" />
                        <asp:Label ID="A2" runat="server" Text='~' Visible="true"></asp:Label>
						<asp:TextBox ID="RCH004" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                        <asp:TextBox ID="CHK004_1" runat="server" onkeyup="chkdecimal(this)" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004_1"))) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false"/>
						<asp:TextBox ID="RCH004_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004_1"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="尺寸" HeaderStyle-Width="60px">
                    <ItemTemplate>
                        <asp:Label ID="LabelX1" runat="server" Text="×" Visible="true"></asp:Label>
                        <asp:Label ID="ITNONE04" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDIFD013"))) %>' Visible="true"></asp:Label>
                        <asp:Label ID="LB004D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004D"))) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false" />
                        <asp:TextBox ID="CHK004D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004D"))) %>' MaxLength="100" style="width:40px; text-align:center;" Visible="true" Enabled="false" />
                        <asp:TextBox ID="CHK005" runat="server" onkeyup="chkdecimal(this)" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK005"))) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false" />
                        <asp:Label ID="A3" runat="server" Text='~' Visible="true"></asp:Label>
						<asp:TextBox ID="CHK005_1" runat="server" onkeyup="chkdecimal(this)" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK005_1"))) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false"/>
						<asp:TextBox ID="RCH005" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK005"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                        <asp:TextBox ID="RCH005_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK005_1"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                        <asp:TextBox ID="RCH004D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004D"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" >
                    <ItemTemplate>                        
                        <asp:Label ID="LabelX2" runat="server" Text="×" Visible="true"></asp:Label>
                        <asp:Label ID="ITNONE05" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDIFD014"))) %>' Visible="true"></asp:Label>
                        <asp:TextBox ID="CHK006" runat="server" onkeyup="chkdecimal(this)" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK006"))) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false" />
                        <asp:Label ID="A4" runat="server" Text='~' Visible="true"></asp:Label>
						<asp:TextBox ID="CHK006_1" runat="server" onkeyup="chkdecimal(this)" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK006_1"))) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false"/>
						<asp:TextBox ID="RCH006" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK006"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                        <asp:TextBox ID="RCH006_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK006_1"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
				<asp:BoundField DataField="SDIFD015" HeaderText="" HeaderStyle-Width="0.5%" />
                <asp:TemplateField HeaderText="尺寸差異百分比" >
                    <ItemTemplate>
                        <asp:Label ID="LBCHK007" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK007"))) %>' Visible="true"></asp:Label>
                        <asp:TextBox ID="TXTCHK007" runat="server" onkeyup="chkdecimal(this)" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK007"))) %>' style="width:40px; text-align:center;" Visible="true" />
                        <asp:Label ID="LBCHK007pers" runat="server" Text='％' Visible="true"></asp:Label>
                        <asp:Label ID="LBCHK007_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK007_1"))) %>' Visible="true"></asp:Label>
                        <asp:TextBox ID="TXTCHK007_1" runat="server" onkeyup="chkdecimal(this)" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK007_1"))) %>' style="width:40px; text-align:center;" Visible="true" />
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












                <div class="form-btn">
                    <asp:Button ID="DataLock" runat="server" Text="確認送出" OnClientClick="return chkInput('DataLock');" OnClick="DataLock_Click" />&nbsp&nbsp
                    <asp:Button ID="SaveCase" runat="server" Text="計算與暫存" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" />&nbsp&nbsp
                    <asp:Button ID="GoHomePage" runat="server" Text="返回編輯案件" OnClick="GoHomePage_Click" />
                </div>

            
<%--        <div class="footer-s">
            <div class="footer-s-green"></div>
            <div class="footer-b-brown">
                <p> <span class="span1">臺北市政府工務局大地工程處</span>
                    <br><span class="span2">110臺北市信義區松德路300號3樓 　(02)27593001   臺北市民當家熱線1999</span>
                    <br><span class="span2">資料更新：2017.5.19　來訪人數：2378 </span></p>
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
        <script src="../js/allhref.js"></script>
        <script src="../js/BaseNorl.js"></script>

    </div>
    </form>
</body>
</html>
