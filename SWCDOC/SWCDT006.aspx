<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCDT006.aspx.cs" Inherits="SWCDOC_SWCDT006" %>
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
    <link rel="stylesheet" type="text/css" href="../css/reset.css" />
    <link rel="stylesheet" type="text/css" href="../css/all.css?202108240134" />
    <link rel="stylesheet" type="text/css" href="../css/iris.css?202108240136" />
    
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
        function BigPic() {
            alert('test');
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
            var panel2901 = document.getElementById("Panel2901");
            var panel2902 = document.getElementById("Panel2902");
            var hyperlink029 = document.getElementById("HyperLink029");

            if (panel2901.style.display == "none") {
                panel2901.style.display = "";
                panel2902.style.display = "none";
                hyperlink029.style.display = "";
            }
            else {
                panel2901.style.display = "none";
                panel2902.style.display = "";
                hyperlink029.style.display = "none";
            }
        }
    </script>
    <script>
        $(document).ready(function redirectPage() {
            var panel2901 = document.getElementById("Panel2901");
            var panel2902 = document.getElementById("Panel2902");

            var W = document.body.clientWidth;
            var H = document.body.clientHeight;
            if (W >= 996) {
                panel2901.style.display = "";
                panel2902.style.display = "none";
            }
            else {
                panel2901.style.display = "none";
                panel2902.style.display = "";
            }
        });
    </script>
    <!--網頁線上簽名end-->

    <style type="text/css">
        .auto-style1 {
            height: 37px;
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
                        <li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
                        <li>|</li>
                        <li><a href="http://www.swc.taipei/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="https://tslm.swc.taipei/tslmwork/" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
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
            <div class="completed form">
                <h1>水土保持完工檢查紀錄表<br/><br/></h1>

                <table class="completed-out">
                <tr><td>完工檢查表編號</td>
                    <td><asp:Label ID="LBDTL001" runat="server"/>
                        <asp:Label ID="LBSWC000" runat="server" Visible="false"/>
                        <asp:Label ID="LBSWC002" runat="server" Visible="false"/>
                        <asp:Label ID="LBSWC025" runat="server" Visible="false"/>
                    </td></tr>
                <tr><td>檢查日期<span style="color: red;font-family:cursive;">＊</span></td>
                    <td><asp:TextBox ID="TXTDTL002" runat="server" width="150px" autocomplete="off"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTDTL002_CalendarExtender" runat="server" TargetControlID="TXTDTL002" Format="yyyy-MM-dd"></asp:CalendarExtender></td></tr>
                <tr><td>檢查單位</td>
                    <td><asp:Label ID="TXTDTL004" runat="server" /></td></tr>
                </table>
                
                <br/><br/>

                <table class="completed-verticalText_X LH">
                <tr><td rowspan="6" class="text_tabletitle" style="width:12%;">水土保持書件</td>
                    <td style="width:16%; vertical-align:middle;">計畫名稱</td>
                    <td style="line-height:1.5;"><asp:Label ID="LBSWC005" runat="server"/>
                        （<asp:Label ID="LBSWC007" runat="server"/>）</td></tr>
                <tr><td class="auto-style1">核定日期文號</td>
                    <td class="auto-style1">臺北市政府
                        <asp:Label ID="LBSWC038" runat="server"/>
                        <asp:Label ID="LBSWC039" runat="server"/>
                        函</td></tr>
                <tr><td>水土保持施工許可證日期文號</td>
                    <td>臺北市政府
                        <asp:Label ID="LBSWC043" runat="server"/>
                        <asp:Label ID="LBSWC044" runat="server"/>
                        函</td></tr>
                <tr><td>開工日期</td>
                    <td><asp:Label ID="LBSWC051" runat="server"/></td></tr>
                <tr><td>核定完工日期</td>
                    <td><asp:Label ID="LBSWC052" runat="server" width="120px"></asp:Label></td></tr>
                <tr><td>申報完工日期</td>
                    <td><asp:Label ID="LBSWC058" runat="server" width="120px"></asp:Label></td></tr>
                <tr><td rowspan="3" class="text_tabletitle">水土保持義務人</td>
                    <td>姓名或名稱</td>
                    <td><asp:Label ID="LBSWC013" runat="server"/></td></tr>
                <tr><td>身分證或營利事業統一編號</td>
                    <td><asp:Label ID="LBSWC013ID" runat="server"/></td></tr>
                <tr><td>住居所或營業所</td>
                    <td><asp:Label ID="LBSWC014" runat="server"/></td></tr>
                <tr><td rowspan="5" class="text_tabletitle">承辦監造技師</td>
                    <td>姓名</td>
                    <td><asp:Label ID="LBSWC021" runat="server"/></td></tr>
                <tr><td>執業機構名稱</td>
                    <td><asp:Label ID="LBSWC021Name" runat="server"/></td></tr>
                <tr><td>執業執照字號</td>
                    <td><asp:Label ID="LBSWC021OrgIssNo" runat="server"/></td></tr>
                <tr><td>營利事業統一編號</td>
                    <td><asp:Label ID="LBSWC021OrgGUINo" runat="server"/></td></tr>
                <tr><td>電話</td>
                    <td><asp:Label ID="LBSWC021OrgTel" runat="server"/></td></tr>
                <tr><td colspan="2">實施地點土地標示</td>
                    <td><asp:TextBox ID="TXTDTL023" runat="server" width="100%" onkeyup="textcount(this,'TXTDTL023_count','200');" MaxLength="200" />
                        <asp:Label ID="TXTDTL023_count" runat="server" Text="(0/200)" ForeColor="Red" /></td></tr>
                </table>

                <table class="completed-excelUpload">
                <tr><td colspan="14" style="border-bottom: none;border-top:none;">
                        一、完工抽驗項目(請至下方水保設施項目表或EXCLE表查看) <%--asp:HyperLink ID="NewUser" runat="server" Text="excel範本下載" NavigateUrl="../sysFile/完工抽驗項目.xlsx" /--%></td></tr>
                <tr><td colspan="14" style="border-top: none;">
                        <asp:FileUpload ID="TXTDTL024_fileupload" Visible="false" runat="server" />
                        <asp:Button ID="TXTDTL024_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL024_fileuploadok_Click" Visible="false"/>
                        <asp:TextBox ID="TXTDTL024" runat="server" Width="70px" Visible="false"/>
                        <asp:Button ID="TXTDTL024_fileclean" runat="server" CssClass="wordttb" Text="X" OnClientClick="return confirm('檔案刪除後無法復原，請確認真的要刪除檔案!!!')" OnClick="TXTDTL024_fileclean_Click" Visible="false" /><br/>
                        <asp:HyperLink ID="Link024" runat="server" CssClass="word" Target="_blank"></asp:HyperLink><br/>
                        <%--span style="color:red;">※ 上傳格式限定為excel，檔案大小請於50mb以內</span--%></td></tr>
                <tr><td colspan="14">二、實施與計畫或規定不符之限期改正期限 <br/><br/>
                        <asp:TextBox ID="TXTDTL025" runat="server" width="100%" Height="120px" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL025_count','300');" />
                        <asp:Label ID="TXTDTL025_count" runat="server" Text="(0/300)" ForeColor="Red" /></td></tr>
                <tr><td colspan="14">三、其他注意事項<br/><br/>
                        <asp:TextBox ID="TXTDTL026" runat="server" width="100%" Height="120px" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL026_count','300');" />
                        <asp:Label ID="TXTDTL026_count" runat="server" Text="(0/300)" ForeColor="Red" /></td></tr>
                <tr><td colspan="14">四、檢查結果
                        <asp:DropDownList ID="DDLDTL027" runat="server">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="已達完工標準" Value="已達完工標準"></asp:ListItem>
					        <asp:ListItem Text="未達完工標準" Value="未達完工標準"></asp:ListItem>
                        </asp:DropDownList>　
                        <span style="color:red;">※ 若竣工圖說有更動，請上傳新版本竣工圖說</span><BR/><BR/>
                    
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

                    </td></tr>
                </table>

                <table class="checkRecord-imgUpload_X" style="border-top:none !important;">
                    <tr><td style="border-bottom: none; border-top:none !important;">五、相關單位及人員簽名<br />
                            <br />
                              <span class="box1"><asp:TextBox ID="TXTDTL028" runat="server" Width="100%" Height="250px" TextMode="MultiLine" onkeyup="textcount(this,'TXTDTL028_count','800');" />
                             <asp:Label ID="TXTDTL028_count" runat="server" Text="(0/800)" ForeColor="Red" /><br />
                             <asp:Button ID="TXTDTL029_changepanel" runat="server" Text="切換簽名方式" OnClientClick="ChangePanel(); return false" Visible="false" />
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
                        <asp:Image ID="TXTDTL029_img" runat="server" CssClass="imgUpload" Visible="false" /><br />
                        <asp:HyperLink ID="HyperLink029" runat="server" CssClass="imgUpload imgTP" Target="_blank" Visible="false"></asp:HyperLink>
                        <br/><span style="color:red;">※ 檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span>
						<br/><span style="color:red;">※ 上傳檔案請勿使用+、空格、/、\、?、%、#、&、=、!...等特殊符號(包含全形符號)</span><br/><br/>
                        <asp:TextBox ID="TXTDTL029" runat="server" Width="70px" Visible="false" />
                        <asp:FileUpload ID="TXTDTL029_fileupload" runat="server" />
                        <asp:Button ID="TXTDTL029_fileuploadok" runat="server" Text="上傳檔案並加入清單" OnClick="TXTDTL029_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL029_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL029_fileuploaddel_Click" Visible="false" /></td></tr>
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
                
				<%--<tr><td colspan="2" style="border-top: none;border-bottom: none;">六、 屬簡易水土保持申報書者，「水土保持施工許可證日期文號」及「承辦監造技師」等二欄，無需填寫。</td></tr>
                <tr><td colspan="2" style="border-top: none;">七、 完工檢查係採抽驗方式，屬未抽驗、隱蔽或工程品質部分，應由水土保持義務人及承辦監造技師負責。</td></tr>--%>
                </table>
                
                <br/>

                <table class="checkRecord-fileUpload">
                <tr><td colspan="2">
                    <asp:Label ID="LBSWC005a" runat="server" CssClass="redn"/></td></tr>
                <tr><td>相片一</td>
                    <td>相片二</td></tr>
                <tr><td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL030" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL030_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL030_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL030_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL030_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL030_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL030_img" runat="server" CssClass="imgUpload" />
                        <br/>
                        <asp:TextBox ID="TXTDTL031" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td>
                    <td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL032" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL032_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL032_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL032_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL032_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL032_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL032_img" runat="server" CssClass="imgUpload" />
                        <br/>
                        <asp:TextBox ID="TXTDTL033" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td></tr>
                        <tr>
                            <td>相片三</td>
                            <td>相片四</td>
                        </tr>
                        <tr>
                            <td>
                        <div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL034" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL034_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL034_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL034_fileuploadok_Click"/>
                            <asp:Button ID="TXTDTL034_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL034_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL034_img" runat="server" CssClass="imgUpload" />
                        <br/>
                        <asp:TextBox ID="TXTDTL035" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td>
                    <td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL036" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL036_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL036_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL036_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL036_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL036_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL036_img" runat="server" CssClass="imgUpload" />
                        <br/>
                        <asp:TextBox ID="TXTDTL037" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td></tr>
                <tr><td>相片五</td>
                    <td>相片六</td></tr>
                <tr><td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL038" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL038_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL038_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL038_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL038_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL038_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL038_img" runat="server" CssClass="imgUpload" />
                        <br/>
                        <asp:TextBox ID="TXTDTL039" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td>
                    <td><div class="imgUpload-btn">
                            <asp:TextBox ID="TXTDTL040" runat="server" Width="70px" Visible="False" />
                            <asp:FileUpload ID="TXTDTL040_fileupload" runat="server" />
                            <asp:Button ID="TXTDTL040_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL040_fileuploadok_Click" />
                            <asp:Button ID="TXTDTL040_fileuploaddel" runat="server" Text="x" ToolTip="刪除上傳照片" OnClientClick="return confirm('照片刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTDTL040_fileuploaddel_Click" />
                        </div><br/><span style="color:red;">檔案大小請小於 10 Mb，請上傳 jpg, png 格式圖檔</span><br/>
                        <asp:Image ID="TXTDTL040_img" runat="server" CssClass="imgUpload" />
                        <br/>
                        <asp:TextBox ID="TXTDTL041" runat="server" TextMode="MultiLine" placeholder="照片說明文字" class="imgUploadText"/></td></tr>
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
            
                <asp:GridView ID="GVSignList" runat="server" DataSourceID="SqlDataSourceSign" CssClass="retirement" AutoGenerateColumns="false">
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
		
		<div class="OFseg">
		<div style="float:left"> <img src="../images/btn/btn006-07.png" alt=""></div>
                <div class="lab">
                   <div class="labcolor1"><div class="icon1"></div>原核定</div>
                   <div class="labcolor2"><div class="icon2"></div>現場量測</div>
                </div>

                <asp:Label ID="GVMSG" runat="server" Text="查無資料" Visible="true" class="nodata"/>
				<div style="clear:both;"></div>
                   <div class="detailsGrid">
                      <h2 class="SWCfO openh2">水保設施項目<img src="../images/btn/btn-close.png" alt=""/></h2>

                                <asp:GridView ID="SDIList" runat="server" CssClass="OFcheck AutoNewLine" AutoGenerateColumns="False" Height="50" EmptyDataText="查無資料"
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
                <asp:TemplateField HeaderText="數量差異百分比" HeaderStyle-Width="400px">
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
                <asp:TemplateField HeaderText="尺寸" HeaderStyle-Width="260px">
                    <ItemTemplate>
                        <asp:Label ID="LabelX1" runat="server" Text="×" Visible="true"></asp:Label>
                        <asp:Label ID="ITNONE04" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDIFD013"))) %>' Visible="true"></asp:Label>
                        <asp:Label ID="LB004D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004D"))) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false" />
                        <asp:TextBox ID="CHK004D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004D"))) %>' MaxLength="100" style="width:150px; text-align:center;" Visible="true" Enabled="false" />
                        <asp:TextBox ID="CHK005" runat="server" onkeyup="chkdecimal(this)" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK005"))) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false" />
                        <asp:Label ID="A3" runat="server" Text='~' Visible="true"></asp:Label>
						<asp:TextBox ID="CHK005_1" runat="server" onkeyup="chkdecimal(this)" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK005_1"))) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false"/>
						<asp:TextBox ID="RCH005" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK005"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                        <asp:TextBox ID="RCH005_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK005_1"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                        <asp:TextBox ID="RCH004D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK004D"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" HeaderStyle-Width="260px">
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
                <asp:TemplateField HeaderText="尺寸差異百分比" HeaderStyle-Width="400px">
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


        <%--<div class="footer-s">
            <div class="footer-s-green"></div>
            <div class="footer-b-brown">
                <p> <span class="span1">臺北市政府工務局大地工程處</span>
                    <br/><span class="span2">110臺北市信義區松德路300號3樓 　(02)27593001   臺北市民當家熱線1999</span>
                    <br/><span class="span2">資料更新：2017.5.19　來訪人數：123456789 </span></p>
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
        
    <script type="text/javascript">
        if (document.getElementById("ReqCount").innerText == '0') { SignList.style.display = "none"; }
    </script>
        <script src="../js/jquery-3.1.1.min.js"></script>
        <script src="../js/inner.js"></script>
        <script src="../js/BaseNorl.js"></script>

    </div>
    </form>
</body>
</html>
