<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="SWCDOC_SWC000" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<%-- <meta http-equiv="X-UA-Compatible" content="IE=9,IE=10,IE=11,IE=EDGE, chrome=1">--%>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>臺北市水土保持申請書件管理平台</title>
    <link rel="Shortcut Icon" type="image/x-icon" href="images/logo-s.ico">
    <meta name="keywords" content="臺北市水土保持申請書件管理平台, 書件管理平台">
    <meta name="description" content="臺北市水土保持申請書件管理平台">
    <meta name="author" content="dorathy">
    <meta name="copyright" content="© 2017 臺北市水土保持申請書件管理平台">
    <%--<meta http-equiv="X-UA-Compatible" content="IE=Edge, chrome=1">--%>
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0" />
    
	<meta name="Soil and Water Conservation Platform Project is a web applicant tracking system which allows citizen can search, view and manage their SWC applicantcase. Copyright (C) <2020> <Geotechnical Engineering Office, Public Works Department, Taipei City Government> "/>
    <meta name="This program is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public License as published by the Free
        Software Foundation, either version 3 of the License, or any later version. This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
        without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more details. " />
    <meta name="You should have received a copy of the GNU Affero General Public License along with this program. If not, see <https://www.gnu.org/licenses/>." />
	
	<link rel="stylesheet" type="text/css" href="css/reset.css"/>
    <link rel="stylesheet" type="text/css" href="css/all.css?202210281233"/>
    <link rel="stylesheet" type="text/css" href="css/ad.css?202210281233"/>
    
    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/index.js"></script>
    <script type="text/javascript">
        function chkInput(jChkType) {
			var jInputID = document.getElementById('TXTID1').value;
            var jInputPW = document.getElementById('TXTPW1').value;
			if (jInputID == '')
            {
                alert('請輸入身份證字號');
                return false;
            }
            if (jInputPW == '') {
                alert('請輸入手機');
                return false;
            }
            if (jInputPW.length != 10 || isNaN(jInputPW)) {
                alert('手機格式不正確，請輸入10位數字號碼');
                return false;
            }
			return true;
        }
		
		function chkInput2(jChkType) {
			var jInputID = document.getElementById('TXTID2').value;
            var jInputPW = document.getElementById('TXTPW2').value;
			if (jInputID == '')
            {
                alert('請輸入身分證字號或統一編號');
                return false;
            }
            if (jInputPW == '') {
                alert('請輸入密碼');
                return false;
            }
			return true;
        }
		
		var timer;
        $(function () {
           timer = setTimeout(function () {
               $("#div").css('display', 'none');
           }, 5000);
        })
        function stop() {
           clearTimeout(timer);
        }
		
		$(function () {
            var $modal = $('.spacebgfix');
            var HIDE_CLASS = 'is-hide';

            $('#js-startbtn').on('click', function () {
                $modal.removeClass(HIDE_CLASS);
            });

            $('.js-modal-close').on('click', function () {
                $modal.addClass(HIDE_CLASS);
            });
        });
		
		$(function () {
            var $modal = $('.spacebgfix2');
            var HIDE_CLASS = 'is-hide2';

            $('#jsstartbtn2').on('click', function () {
                $modal.removeClass(HIDE_CLASS);
            });

            $('.js-modal-close2').on('click', function () {
                $modal.addClass(HIDE_CLASS);
            });
        });
		$(function () {
            var $modal = $('.spacebgfix3');
            var HIDE_CLASS = 'is-hide3';

            $('#jsstartbtn3').on('click', function () {
                $modal.removeClass(HIDE_CLASS);
            });

            $('.js-modal-close3').on('click', function () {
                $modal.addClass(HIDE_CLASS);
            });
        });
    </script>

    <style>
        .swclogin{
            color:#364250;
            outline:none;
            background:#f7e26d;
            border:2px solid #FFF;
            cursor:pointer;
            font-size:16px;
            font-weight:bold;
            padding:0.5px 10px;
            font-family:微軟正黑體;
            letter-spacing:2px;
            vertical-align:central;
			
        }
        .swclogin:hover{
            background:#364250;
            border:2px solid #FFF;
            color:#FFF;
        }
        /*燈箱效果*/
.spacebgfix {
    position: fixed;
    left: 0;
    top: 0;
    height: 100%;
    width: 100%;
    margin: 0;
    padding: 0;
    background-color: rgba(107, 107, 107, 0.6);
	z-index:999999999;
}
.spacebgfix2 {
    position: fixed;
    left: 0;
    top: 0;
    height: 100%;
    width: 100%;
    margin: 0;
    padding: 0;
    background-color: rgba(107, 107, 107, 0.6);
	z-index:999999999;
}
.spacebgfix3 {
    position: fixed;
    left: 0;
    top: 0;
    height: 100%;
    width: 100%;
    margin: 0;
    padding: 0;
    background-color: rgba(107, 107, 107, 0.6);
	z-index:999999999;
}
.spacelibox {
    position: absolute;
    padding: 20px 20px 20px;
    left: 50%;
    top: 50%;
    transform: translate(-50%, -50%);
    border-radius:8px;
    background: #FFF;
	box-shadow:0px 0px  6px #666;
	max-width:680px;
	width:100%;
	height:470px;
}
.spacelibox2 {
    position: absolute;
    padding: 20px 20px 20px;
    left: 50%;
    top: 50%;
    transform: translate(-50%, -50%);
    border-radius:8px;
    background: #FFF;
	box-shadow:0px 0px  6px #666;
	max-width:460px;
	width:95%;
	height:auto;
}
.spacelibox3 {
    position: absolute;
    padding: 20px 20px 20px;
    left: 50%;
    top: 50%;
    transform: translate(-50%, -50%);
    border-radius:8px;
    background: #FFF;
	box-shadow:0px 0px  6px #666;
	max-width:480px;
	width:100%;
	height:auto;
}
.is-hide {
    display: none;
}
.is-hide2 {
    display: none;
}
.is-hide3 {
    display: none;
}

.peoplelist select{
	margin:10px;
	padding:3px;
	border:1px solid #666;
}

/*燈箱結束*/

.look{
    line-height:3;
    color:#666;
    font-size:14pt;
    text-align:left !important;
}
.TPtitle{
	font-size:18px;
	font-weight:bold;
	float:left;
	color:#2a4f2c;
	display:block;
	padding:7px 10px 7px 12px;
	border:2px dashed #2a4f2c;
	/*border-left:8px solid #eeda6d;*/
	margin-right:15px;
	clear:both;
}
@media screen and (max-width:996px) {
  .TPtitle{
       border:none;
	   border-bottom:5px solid #2a4f2c;
	   font-size:20px !important;
  }
}
.TPtitle2{
	font-size:18px;
	font-weight:bold;
	float:center;
	background:#eeda6d;
	color:#364250;
	display:block;
	padding:7px 10px 7px 12px;
	border-radius:5px;
	margin-right:15px;
}
.lsp{
	letter-spacing:4px;
}
.TPclose{
	float:right;
	font-size:20px;
	border:none;
	background:none;
	outline:none;
	color:rgba(127,127,127,1.00);
	cursor:pointer;
	display:block;
	margin-bottom:20px;
}
.TPTB th{
	border-right:4px solid #2A8226;
}
.TPTB{
	line-height:1.5;
	font-size:17px; 
	font-weight:bold;
	margin-top:8px;
	display:block;
	clear:both;
    padding-left:10px;
    padding-top:10px;
}
.TPTB b{
	color:#2eb5c7;
	font-weight:bold;
	padding:0 5px;
}
.TPTB label{
    color:#f1373c;
	font-weight:bold;
	padding:0 5px;
    text-decoration:underline;
}
@media screen and (max-width:996px) {
  .TPTB{
  display:block;
  float:none;
  }
  .ssp{
  margin-top:55px;
  display:block;
  }
}
.TPmemo{
	margin-top:15px;
	line-height:2;
	font-weight:bold;
	font-size:17px;
	color:#666;
    padding-left:10px;
}
.TPmemo a{
	color:#2eb5c7;
}
.TPlogin{
	background: #eeda6d;
	color:#364250;
	border:none;
	outline:none;
	text-align:center;
	margin:0 auto;
	display:block;
	font-size:18px;
	padding:8px 15px 7px 21px;
	border-radius:5px;
	letter-spacing:3px;
	font-weight:bold;
	font-family:微軟正黑體;
	cursor:pointer;
	border-bottom:4px solid #eac428;
	margin-top:15px;
}
.TPlogin:hover{
    background: #eac428;
}
@media screen and (max-width:996px) {
   .TPlogin{
       padding:8px 45px 7px 51px;
	   font-size:20px !important;
   }
}

.spacelibox a{
	text-decoration:none;
}
.num{
	margin:10px 0 20px;
	font-weight:bold;
	font-size:18px;
    clear:both;
}
.num select{
	border:1px solid #666;
	height:30px;
	border-radius:3px;
	margin-top:2px;
}
@media only screen and (max-width: 996px) {
    .spacelibox{
		max-height: 580px;
        height: 95%;
	    overflow-y: scroll;
        width:95%;
    }
     .TPtitle{
         font-size:15px;
		 float:none;
		 display:block;
		 text-align:center;
     }   
     .TPmemo {
         font-size:16px;
         line-height:1.2;
     }
     
}

    </style>
</head>

<body>
	<!-- Google Tag Manager (noscript) -->
	<noscript><iframe src="https://www.googletagmanager.com/ns.html?id=GTM-MSP6KWH"
	height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
	<!-- End Google Tag Manager (noscript) -->
	
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    
    
    <div class="wrap-b">
      <div class="header1 header-b1"><a href="#" class="logo-b"></a></div>
        <div class="header-menu-b1 clearfix1 indexul">
          <ul>
            <li><a href="SWCTCGOV/SWCGOV011p.aspx" title="公佈欄" target="_blank">公佈欄</a></li>
            <li>|</li>
			<li><a href="SWCDOC/sample.aspx" title="申請文件範例" target="_blank">申請文件範例</a></li>
            <li class="DMnone">|</li>
			<li><a href="sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
            <li class="DMnone">|</li>
            <li class="flip"><a href="#" title="相關連結">相關連結+</a>
		       <ul>
		         <li><a href="https://swc.taipei/swcinfo/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統</a></li>
                 <li class="dashed"></li>
		         <li><a href="http://www.geo.gov.taipei/" title="臺北市政府工務局大地工程處" target="_blank">臺北市政府工務局大地工程處</a></li>
				 <li class="dashed"></li>
				 <li><a href="https://swc.taipei/DouCode/GuildUsers.aspx" title="共同供應契約審查委員合格名冊" target="_blank">共同供應契約審查委員合格名冊</a></li>
                 <li class="dashed"></li>
                 <li><a href="http://tgeo.swc.taipei/" target="_blank">T-GEO空間地理資訊平台</a></li>
				 <li class="dashed"></li>
                 <li><a href="https://swctaipei.github.io/SWCWeb/" target="_blank">臺北市水土保持申請書件管理平台開源專案</a></li>
				 <li class="dashed"></li>
                 <li><a href="https://docs.google.com/forms/d/e/1FAIpQLSeMdLImqVSjU697WxcepqeX_ijkJSCroOYgjT5kHL69Scjw-w/viewform" target="_blank">水土保持管理滿意度調查表</a></li>
		       </ul>
            </li>
			<li class="DMnone">|</li>
			<li><a href="#"><input type="button" value="登入" class="swclogin" id="js-startbtn" /></a></li>
          </ul>
		  </div>
      
	   <div class="spacebgfix is-hide">
       <div class="spacelibox">
       <input type="button" value="X" class="js-modal-close TPclose" /><br />
           <div class="num">
				<span class="TPtitle lsp">請選擇身份</span>
				<asp:DropDownList ID="DropDownList1" runat="server">
					<asp:ListItem Value="技師/各類委員">技師/各類委員</asp:ListItem>
					<asp:ListItem Value="審查/檢查單位">審查/檢查單位</asp:ListItem>
					<asp:ListItem Value="水土保持義務人">水土保持義務人</asp:ListItem>
					<asp:ListItem Value="工務局大地工程處">工務局大地工程處</asp:ListItem>
					<asp:ListItem Value="其他機關">其他機關</asp:ListItem>
					<asp:ListItem Value="建築師">建築師</asp:ListItem>
					<asp:ListItem Value="營造單位/工地負責人">營造單位/工地負責人</asp:ListItem>
					<asp:ListItem Value="代理技師">代理技師</asp:ListItem>
                    <asp:ListItem Value="代理公會">代理公會</asp:ListItem>
				</asp:DropDownList>
				<!--<input type="button" value="義務人身分驗證" id="js-startbtn2" class="obligee" />
				<input type="button" value="其他機關驗證" id="js-startbtn3" class="obligee" />-->
				&nbsp;
				<asp:Button Text="義務人身分驗證" ID="jsstartbtn2" class="obligee" runat="server" onclientclick="return false" />&nbsp;&nbsp;
				<asp:Button Text="其他機關驗證" ID="jsstartbtn3" class="obligee" runat="server" onclientclick="return false" />
				
				
           </div> 
           <br />
           <span class="TPtitle ssp">登入注意事項</span>
           
           <span class="TPTB">
             為配合台北市政府單一身分驗證入口政策，本網站可點選下方登入，透過「台北通」、「行動自然人憑證」、「實體自然人憑證」及「健保卡」等方式登入。
             <p style="margin-top:10px;">若使用 <b>台北通3.0</b> 、系統提供帳密驗證服務，需經<label>實名驗證成為金質會員</label></>才能進行線上申請服務。</p>
           </span>
		   
           <div class="TPmemo"> 
            <span>若您未有帳號，可至台北通3.0官方網站申請&nbsp;→ <a href="https://id.taipei/tpcd/" target="_blank">台北通官網申請</a></span><br />
            <span>若有自然人憑證，可透過電腦成為金質會員&nbsp;→ <a href="https://id.taipei/tpcd/registration" target="_blank">升級金質會員說明</a></span>
           </div>
           
           <div><asp:Button ID="TaipeiLogin" runat="server" Text="登入" CssClass="TPlogin" OnClick="TaipeiLogin_Click" /></div>
       </div>
     </div>
	 <div class="spacebgfix2 is-hide2">
		<div class="spacelibox2 obligeewrap">
		<input type="button" value="X" class="js-modal-close2 TPclose" /><br />
			<span style="text-align: center; display:block;">
				<br />
				<span class="obligeeTT">義務人身分驗證</span>
					<table class="obligeeTB">
						<tr>
							<th>
								<span style="text-align: center;">身分證字號</span>
							</th>
							<td>
								<asp:TextBox ID="TXTID1" runat="server" maxlength="10" PlaceHolder="請輸入身分證字號" />
							</td>
						</tr>
						<tr>
							<th>
								<span style="text-align: center;">手機</span>
							</th>
							<td>
								<asp:TextBox ID="TXTPW1" runat="server" maxlength="10" PlaceHolder="請輸入手機" />
							</td>
						</tr>
					</table>
					<asp:Button ID="BTN_Login" runat="server" Text="登入" OnClientClick="return chkInput('');" OnClick="BTN_Login_Click" class="Blogin" />
				<br /><br />
				
			</span>
		</div>
	</div>
	
	<div class="spacebgfix3 is-hide3">
		<div class="spacelibox3 obligeewrap">
		<input type="button" value="X" class="js-modal-close3 TPclose" /><br />
			<span style="text-align: center; display:block;">
				<br />
				<span class="obligeeTT">其他機關驗證</span>
					<table class="obligeeTB">
						<tr>
							<th>
								<span style="text-align: center;">身分證字號或統一編號</span>
							</th>
							<td>
								<asp:TextBox ID="TXTID2" runat="server" PlaceHolder="請輸入身分證字號/統一編號" />
							</td>
						</tr>
						<tr>
							<th>
								<span style="text-align: center;">手機</span>
							</th>
							<td>
								<asp:TextBox ID="TXTPW2" runat="server" PlaceHolder="請輸入手機" />
							</td>
						</tr>
					</table>
					<asp:Button ID="BTN_Login2" runat="server" Text="登入" OnClientClick="return chkInput2('');" OnClick="BTN_Login2_Click" class="Blogin" />
				<br /><br />
				
			</span>
		</div>
	</div>
	
      <div style="clear:both"></div>
        <div id="Marquee" title="" class ="news newstop1" style="display:none;">
            <asp:UpdatePanel ID="MarqueeUpdatePanel" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <asp:Label ID="Marqueeclick" runat="server" AutoPostBack="True" style="display:none ;"></asp:Label>
                    <asp:Button ID="MarqueeButton" runat="server" Text="跑馬燈在這邊顯示" OnClientClick="MarqueeMessageDiv.style.display='';document.getElementById('Marqueeclick').innerText='ShowDtl';" Class="news_buttom" BorderStyle="None" />
                    <asp:Timer ID="MarqueeTimer" runat="server" Interval="5000" OnTick="MarqueeTimer_Tick"></asp:Timer>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

       <div style="text-align:center;" class="powerbiw">
            <!--iframe  class="powerbiw"  src="https://app.powerbi.com/view?r=eyJrIjoiNzQ4YWQ5MDQtZmRhNS00MDQ1LWJiNjQtZDYyODRkNTc0NTlhIiwidCI6IjViNjRlZThjLTkwYjctNDUwNy04ZDJhLTdhNDE5MWM1NzQ2MCIsImMiOjEwfQ%3D%3D"
                frameborder="0" ></iframe-->
			<iframe class="powerbiw"   src="https://app.powerbi.com/view?r=eyJrIjoiMzMwZTY5ZmItNTk1Ni00NjNlLWEzNTktMmVjY2QxYzE5OGYzIiwidCI6IjViNjRlZThjLTkwYjctNDUwNy04ZDJhLTdhNDE5MWM1NzQ2MCIsImMiOjEwfQ%3D%3D" frameborder="0" ></iframe>
         </div>



     <div class="powerbimessage" id="div">
            <span>儀錶板載入中請稍後...如需登入請按右上方登入按鈕</span>
     </div>



      <div>

           


        <div class="login" style="display:none;"><span id="login1">身份</span>
            <asp:DropDownList ID="loginChange" runat="server" onchange="showChange(this.value);" /><br/><br/>
            <span id="login2">請輸入</span>
            <asp:TextBox ID="TXTID" runat="server" placeholder="身分證字號" MaxLength="10" /><br/><br/>
            <span id="login3">密碼</span>
            <asp:TextBox ID="TXTPW" runat="server" placeholder="手機" MaxLength="15" /><br/><br/><br/>
            <asp:HyperLink ID="NewUser" runat="server" Text="首次登入" />

            <asp:ImageButton ID="SwcLogin" runat="server" OnClientClick="return chkInput('');" OnClick="SwcLogin_Click" title="登入" ImageUrl="../images/btn/btn-login.png" CssClass="imgLogin"/>
        </div>
      </div>
      <div id="MarqueeMessageDiv" class="model newshref"  style="filter: Alpha(Ppacity=80); display:none;">

            <span  class="newstitleD">最新消息</span><br /><br /><br />
        <p>
            <asp:UpdatePanel ID="MarqueeMessageUpdatePanel" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <asp:Label ID="MarqueeMessageLabel" runat="server" Text="跑馬燈在這邊顯示" CssClass="newsp"></asp:Label> <br/>
                    <asp:HyperLink ID="LinkFile" runat="server" Target="_blank" />
                    
                    <asp:Button ID="MarqueeMessageButton" runat="server" Text="X" CssClass="close-s" OnClientClick="MarqueeMessageDiv.style.display='none';document.getElementById('Marqueeclick').value='NoDtl';"/>
                    
                </ContentTemplate>
            </asp:UpdatePanel>
        </p>
	</div>

            <div class="footer-b">
                <div class="footer-b-green"></div>
                    <div class="footer-bb-brown">
                        <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                     <span class="span2">110臺北市信義區松德路300號3樓 　服務專線(02)27591109   臺北市民當家熱線1999</span><br/>
                      <span class="span2">建議使用IE11(含)以上，Chrome或Firefox版本瀏覽器　<b>資料更新：</b><asp:Label ID="ToDay" runat="server" Text=""/>　<b>來訪人數：</b><asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                      <span class="span2"><b>客服電話：</b>02-27929328 陳小姐　<b>信箱：</b>tcge7@geovector.com.tw　本系統由多維空間資訊有限公司開發維護 TEL：(02)27929328</span><br/>
			          <span class="span2">※為維護系統服務品質，本平台訂於每周三凌晨AM 4:00-6:30 進行系統維護更新，更新期間偶有瞬斷情形，敬請使用者避開該時段使用。謝謝！</span></p>
                    </div>
                </div>
                
            </div>

        

         

    

       <div style="clear:both"></</div>

        


        
            <script type="text/javascript">
                var x = document.getElementById("loginChange");
                var str = x.options[x.selectedIndex].text;   
                showChange(str);
                //MarqueeMessageDiv.style.display = 'none';
                document.getElementById('Marqueeclick').value = 'NoDtl';

                if (document.getElementById('MarqueeButton').value == '跑馬燈在這邊顯示') {}else{
                    document.getElementById("Marquee").style.display = '';
                    //document.getElementById("MarqueeMessageDiv").style.display = '';
                }
            </script>

            <asp:Literal ID="error_msg" runat="server"></asp:Literal>

        </div>
        <script src="js/allhref.js"></script>
        <script src="../js/jquery-3.1.1.min.js"></script>
        <script src="../js/inner.js"></script>
        <script src="../js/inmenu.js"></script>
    </form>
    
</body>
</html>

