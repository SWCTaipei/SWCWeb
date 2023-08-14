<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCDT005.aspx.cs" Inherits="SWCDOC_SWCDT005" %>
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
    <link rel="stylesheet" type="text/css" href="../css/all.css?202108240202" />
	<link rel="stylesheet" type="text/css" href="../css/iris.css?202108240202" />
    
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
        function chknumber(objElement) {
            var chrstring = objElement.value;
            var chr = chrstring.substring(chrstring.length - 1, chrstring.length);
            if ((chr != '0') && (chr != '1') && (chr != '2') && (chr != '3') && (chr != '4') && (chr != '5') && (chr != '6') && (chr != '7') && (chr != '8') && (chr != '9')) {
                objElement.value = objElement.value.substring(0, chrstring.length - 1);
            }
        }
        function chkInput(jChkType) {
            jCHKValue01 = document.getElementById("TXTDTL002").value;
            jCHKValue02 = document.getElementById("TXTDTL003").value;
            jCHKValue03 = document.getElementById("TXTDTL004").value;
            //jCHKValue04 = document.getElementById("TXTSWC015").value;
            //jCHKValue05 = document.getElementById("TXTSWC016").value;
            /*
            if (jCHKValue01.trim() == '') {
                alert('請輸入檢查日期');
                document.getElementById("TXTDTL002").focus();
                return false;
            } else
            {
                if (!dateValidationCheck(jCHKValue01)) {
                    document.getElementById("TXTDTL002").focus();
                    return false;
                }
            }*/
            if (jCHKValue02.trim() == '') {
                alert('請輸入工程進度');
                document.getElementById("TXTDTL003").focus();
                return false;
            }
            if (jCHKValue03.trim() == '') {
                alert('請輸入監造結果');
                document.getElementById("TXTDTL004").focus();
                return false;
            }
            if (jChkType == 'DataLock') {
                var r = confirm('確認送出後，即不可修改，請再次確認是否要完成送出。');
                return r;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    
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
                        <li>|</li>
                        <li><a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li>
                    </ul>
                </div>
            </div>
            
            <div class="header-s-green">
                <div class="header-s-green-nameWrap">
                    <span><asp:Literal ID="TextUserName" runat="server">許先生，您好</asp:Literal></span>
                </div>
            </div>
        </div>

        <div class="content-t">
           <div class="supervision form">
                <h1>臺北市水土保持計畫監造紀錄<br/><br/></h1>

                <table class="checklist-out">
                <tr><td>監造紀錄表編號</td>
                    <td><asp:Label ID="LBDTL001" runat="server"/>
                        <asp:Label ID="LBSWC000" runat="server" Visible="false"/></td></tr>
                <tr style="display:none;"><td>案件名稱</td>
                    <td><asp:Label ID="LBSWC005" runat="server"/></td></tr>
                <tr style="display:none;"><td>檢查日期<span style="color: red;font-family:cursive;">＊</span></td>
                    <td><asp:TextBox ID="TXTDTL002" runat="server" width="150px" autocomplete="off"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTDTL002_CalendarExtender" runat="server" TargetControlID="TXTDTL002" Format="yyyy-MM-dd"></asp:CalendarExtender>
                        <asp:TextBox ID="TXTDTL005" runat="server" width="150px" Visible="false"></asp:TextBox>
                        <span class="gray">（範例：2020-01-02）</span>
                    </td></tr>
                <tr><td>監造日期<span style="color: red;font-family:cursive;">＊</span></td>
                    <td><asp:DropDownList ID="DDLDTL088" runat="server"/></td></tr>
                <tr><td>工程狀態<span style="color: red;font-family:cursive;">＊</span></td>
                    <td><asp:DropDownList ID="DDLDTL089" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="尚未施工">尚未施工</asp:ListItem>
                            <asp:ListItem Value="施作臨時設施">施作臨時設施</asp:ListItem>
                            <asp:ListItem Value="施作臨時＆永久設施">施作臨時＆永久設施</asp:ListItem>
                            <asp:ListItem Value="施作永久設施">施作永久設施</asp:ListItem>
                            <asp:ListItem Value="完工">完工</asp:ListItem>
                        </asp:DropDownList></td></tr>
                <tr><td style="vertical-align:middle;">工程進度<span style="color: red;font-family:cursive;">＊</span></td>
                    <td>累計進度百分比：
                        <asp:TextBox ID="TXTDTL003" runat="server" Width="50" onkeyup="chknumber(this);" MaxLength="3" />％
                        <span class="gray">(範例：70)</span></td></tr>
                <tr><td style="vertical-align:middle;">監造結果<span style="color: red;font-family:cursive;">＊</span></td>
                    <td><asp:DropDownList ID="DDLDTL004" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="尚未施工">尚未施工</asp:ListItem>
                            <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                            <asp:ListItem Value="有施工缺失改善中">有施工缺失改善中</asp:ListItem>
                            <asp:ListItem Value="缺失應改未改或未依計畫施工，通知大地處查處">缺失應改未改或未依計畫施工，通知大地處查處</asp:ListItem>
                            <asp:ListItem Value="完工">完工</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TXTDTL004" runat="server" width="100%" onkeyup="textcount(this,'TXTDTL004_count','200');" MaxLength="200" Height="100" TextMode="MultiLine" Visible="false" /></td></tr>
                </table>

                <br/><br/>

                <table>
                <tr><td>日期</td>
                    <td><asp:Label ID="LBDTL004" runat="server"/></td></tr>
                <tr><td >水土保持計畫名稱</td>
                    <td><asp:Label ID="LBSWC005_01" runat="server"/></td></tr>
                <tr><td>水土保持義務人</td>
                    <td><asp:Label ID="LBSWC013" runat="server"/></td></tr>
                <tr><td>主管機關</td>
                    <td><asp:Label ID="LBSWC017" runat="server"/></td></tr>
                <tr><td>水土保持施工許可證日期文號</td>
                    <td>臺北市政府
                        <asp:Label ID="LBSWC043" runat="server"/>
                        <asp:Label ID="LBSWC044" runat="server"/>
                        號函</td></tr>
                <tr><td>承辦監造技師</td>
                    <td><asp:Label ID="LBSWC045" runat="server"/></td></tr>
                <tr><td>營造單位</td>
                    <td><asp:Label ID="LBSWC048" runat="server"/></td></tr>
                <tr><td>預定完工日期</td>
                    <td><asp:Label ID="LBSWC052" runat="server"/></td></tr>
          </table>
            
           <br /><br /> 
           <div class="RWDfrom RWD_3">          
              <ul class="mbnone">  
                  <li style="text-align:center;"> 監造項目</li>
                  <li style="text-align:center;"> 是否與計畫或規定相符</li>
                  <li style="text-align:center;"> 備註</li>
              </ul>
              <ul>
                <li class="bold">（一）水土保持施工告示牌</li><!--20211018修改同步下方選項-->
                    <li>
                        <asp:DropDownList ID="DDLDTL016" runat="server">
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
                        </asp:DropDownList>
                        <div style="display:none;">
                        不符部分：
                        <asp:TextBox ID="TXTDTL017" runat="server" MaxLength="50" /></div></li>
                    <li><asp:TextBox ID="TXTDTL018" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL018_count','50');" />
                        <br/><asp:Label ID="TXTDTL018_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </li>
                </ul> 

                <ul>
                    <li>（二）開發範圍界樁</li><!--20211018修改同步下方選項-->
                    <li>
                        <asp:DropDownList ID="DDLDTL019" runat="server">
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
                        </asp:DropDownList>
                        <div style="display:none;">
                        不符部分：
                        <asp:TextBox ID="TXTDTL020" runat="server" MaxLength="50" /></div></li>
                    <li><asp:TextBox ID="TXTDTL021" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL021_count','50');" />
                        <br/><asp:Label ID="TXTDTL021_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </li>
                </ul>
                <ul>
                   <li>（三）開挖整地範圍界樁</li><!--20211018修改同步下方選項-->
                   <li>
                        <asp:DropDownList ID="DDLDTL022" runat="server">
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
                        </asp:DropDownList>
                        <div style="display:none;">
                        不符部分：
                        <asp:TextBox ID="TXTDTL023" runat="server" MaxLength="50" /></div></li>
                    <li><asp:TextBox ID="TXTDTL024" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL024_count','50');" />
                        <br/><asp:Label ID="TXTDTL024_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </li>
                 </ul>

            <asp:Panel ID="HD01" runat="server" Visible="false">
                 <ul>
                    <li>（四）災害搶救小組是否成立</li>
                    <li>
                        <asp:DropDownList ID="DDLDTL025" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <div style="display:none;">
                        不符部分：
                        <asp:TextBox ID="TXTDTL026" runat="server" MaxLength="50" /></div></li>
                   <li><asp:TextBox ID="TXTDTL027" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL027_count','50');" />
                        <br/><asp:Label ID="TXTDTL027_count" runat="server" Text="(0/50)" ForeColor="Red" /></li>
                    </ul>
            </asp:Panel>
               <ul> 
                  <li>（四）臨時性防災措施</li>
                  <li class="nonepd">
                        <div style="display:none;">
                        <asp:DropDownList ID="DDLDTL028" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL029" runat="server" MaxLength="50" /></div></li>
                    <li class="nonepd"><div style="display:none;">
                            <asp:TextBox ID="TXTDTL030" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL030_count','50');" />
                        <br/><asp:Label ID="TXTDTL030_count" runat="server" Text="(0/50)" ForeColor="Red" /></div>
                    </li>
                </ul>
                <ul>
                   <li class="li-padding"> 1.排水設施</li>
                   <li class="li-paddingmb">
                        <asp:DropDownList ID="DDLDTL031" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                            <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                            <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                            <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                            <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                            <asp:ListItem Value="無此項">無此項</asp:ListItem>
                        </asp:DropDownList>
                        <div style="display:none;">
                        不符部分：
                        <asp:TextBox ID="TXTDTL032" runat="server" MaxLength="50" /></div></li>
                    <li class="li-paddingmb"><asp:TextBox ID="TXTDTL033" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL033_count','50');" />
                        <br/><asp:Label ID="TXTDTL033_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </li>
                 </ul>
                 <ul>
                   <li class="li-padding">2.沉砂設施</li>
                   <li class="li-paddingmb">
                        <asp:DropDownList ID="DDLDTL034" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                            <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                            <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                            <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                            <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                            <asp:ListItem Value="無此項">無此項</asp:ListItem>
                        </asp:DropDownList>
                        <div style="display:none;">
                        不符部分：
                        <asp:TextBox ID="TXTDTL035" runat="server" MaxLength="50" /></div></li>
                    <li class="li-paddingmb"><asp:TextBox ID="TXTDTL036" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL036_count','50');" />
                        <br/><asp:Label ID="TXTDTL036_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </li>
                </ul>
               <ul>
                   <li class="li-padding">3.滯洪設施</li>
                    <li class="li-paddingmb"><asp:DropDownList ID="DDLDTL037" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                            <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                            <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                            <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                            <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                            <asp:ListItem Value="無此項">無此項</asp:ListItem>
                        </asp:DropDownList>
                        <div style="display:none;">
                        不符部分：
                        <asp:TextBox ID="TXTDTL038" runat="server" MaxLength="50" /></div></li>
                    <li class="li-paddingmb"><asp:TextBox ID="TXTDTL039" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL039_count','50');" />
                        <br/><asp:Label ID="TXTDTL039_count" runat="server" Text="(0/50)" ForeColor="Red" /></li>
                </ul>
               <ul>
                   <li class="li-padding">4.土方暫置</li>
                   <li class="li-paddingmb"><asp:DropDownList ID="DDLDTL040" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="尚無土方暫置須求">尚無土方暫置須求</asp:ListItem>
                            <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                            <asp:ListItem Value="暫置方式有缺失應改善">暫置方式有缺失應改善</asp:ListItem>
                            <asp:ListItem Value="未依計畫施作，通知大地處查處">未依計畫施作，通知大地處查處</asp:ListItem>
                            <asp:ListItem Value="無此項">無此項</asp:ListItem>
                        </asp:DropDownList>
                        <div style="display:none;">
                        不符部分：
                        <asp:TextBox ID="TXTDTL041" runat="server" MaxLength="50" /></div></li>
                    <li class="li-paddingmb"><asp:TextBox ID="TXTDTL042" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL042_count','50');" />
                        <br/><asp:Label ID="TXTDTL042_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </li>
                </ul>
                <ul>
                    <li class="li-padding">5.邊坡保護措施</li>
                    <li class="li-paddingmb"><asp:DropDownList ID="DDLDTL043" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                            <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                            <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                            <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                            <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                            <asp:ListItem Value="無此項">無此項</asp:ListItem>
                        </asp:DropDownList>
                        <div style="display:none;">
                        不符部分：
                        <asp:TextBox ID="TXTDTL044" runat="server" MaxLength="50" /></div></li>
                    <li class="li-paddingmb"><asp:TextBox ID="TXTDTL045" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL045_count','50');" />
                        <br/><asp:Label ID="TXTDTL045_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </li>
                </ul>
               <ul>
                   <li class="li-padding">6.施工便道</li>
                   <li class="li-paddingmb"><asp:DropDownList ID="DDLDTL046" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                            <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                            <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                            <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                            <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                            <asp:ListItem Value="無此項">無此項</asp:ListItem>
                        </asp:DropDownList>
                        <div style="display:none;">
                        不符部分：
                        <asp:TextBox ID="TXTDTL047" runat="server" MaxLength="50" /></div></li>
                    <li class="li-paddingmb"><asp:TextBox ID="TXTDTL048" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL048_count','50');" />
                        <br/><asp:Label ID="TXTDTL048_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </li>
                </ul>
                <ul>
                    <li class="li-padding">7.臨時攔砂設施(如砂包、防溢座等)</li>
                    <li class="li-paddingmb"><asp:DropDownList ID="DDLDTL090" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                            <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                            <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                            <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                            <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                            <asp:ListItem Value="無此項">無此項</asp:ListItem>
                        </asp:DropDownList>
                        <div style="display:none;">
                        不符部分：
                        <asp:TextBox ID="TextBox1" runat="server" MaxLength="50" /></div></li>
                    <li class="li-paddingmb"><asp:TextBox ID="TXTDTL091" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL091_count','50');" />
                        <br/><asp:Label ID="TXTDTL091_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </li>
                </ul>
                <ul>
                    <li class="li-padding">8.其他</li>
                    <li class="li-paddingmb">
                        <div style="display:none;">
                        <asp:DropDownList ID="DDLDTL049" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                            <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                            <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                            <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                            <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                            <asp:ListItem Value="無此項">無此項</asp:ListItem>
                        </asp:DropDownList>
                        不符部分：
                        <asp:TextBox ID="TXTDTL050" runat="server" MaxLength="50" /></div>
                        <asp:TextBox ID="TXTDTL051" runat="server" size="50" MaxLength="50" onkeyup="textcount(this,'TXTDTL051_count','50');" />
                        <br/><asp:Label ID="TXTDTL051_count" runat="server" Text="(0/50)" ForeColor="Red" /></li>
                </ul>
                <ul>
                   <li>（五）永久性防災措施</li>
                    <li class="nonepd"><div style="display:none;">
                        <asp:DropDownList ID="DDLDTL052" runat="server" />
                        不符部分：
                        <asp:TextBox ID="TXTDTL053" runat="server" MaxLength="50" /></div></li>
                    <li class="nonepd"><div style="display:none;">
                        <asp:TextBox ID="TXTDTL054" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL054_count','50');" />
                        <br/><asp:Label ID="TXTDTL054_count" runat="server" Text="(0/50)" ForeColor="Red" /></div></li>
                </ul>
                <ul>
                    <li class="li-padding">1.排水設施</li>
                    <li class="li-paddingmb">
                        <asp:DropDownList ID="DDLDTL055" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                            <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                            <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                            <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                            <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                            <asp:ListItem Value="無此項">無此項</asp:ListItem>
                        </asp:DropDownList>
                        <div style="display:none;">
                        不符部分：
                        <asp:TextBox ID="TXTDTL056" runat="server" MaxLength="50" /></div></li>
                    <li class="li-paddingmb"><asp:TextBox ID="TXTDTL057" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL057_count','50');" />
                        <br/><asp:Label ID="TXTDTL057_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </li>
                </ul>
                <ul>
                    <li class="li-padding">2.沉砂設施</li>
                    <li class="li-paddingmb">
                        <asp:DropDownList ID="DDLDTL058" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                            <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                            <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                            <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                            <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                            <asp:ListItem Value="無此項">無此項</asp:ListItem>
                        </asp:DropDownList>
                        <div style="display:none;">
                        不符部分：
                        <asp:TextBox ID="TXTDTL059" runat="server" MaxLength="50" /></div></li>
                    <li class="li-paddingmb"><asp:TextBox ID="TXTDTL060" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL060_count','50');" />
                        <br/><asp:Label ID="TXTDTL060_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </li></ul>
                <ul>
                    <li class="li-padding">3.滯洪設施</li>
                    <li class="li-paddingmb">
                        <asp:DropDownList ID="DDLDTL061" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                            <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                            <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                            <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                            <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                            <asp:ListItem Value="無此項">無此項</asp:ListItem>
                        </asp:DropDownList>
                        <div style="display:none;">
                        不符部分：
                        <asp:TextBox ID="TXTDTL062" runat="server" MaxLength="50" /></div></li>
                    <li class="li-paddingmb"><asp:TextBox ID="TXTDTL063" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL063_count','50');" />
                        <br/><asp:Label ID="TXTDTL063_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </li>
                </ul>
                <ul>
                    <li class="li-padding">4.聯外排水</li>
                    <li class="li-paddingmb">
                        <asp:DropDownList ID="DDLDTL064" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                            <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                            <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                            <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                            <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                            <asp:ListItem Value="無此項">無此項</asp:ListItem>
                        </asp:DropDownList>
                        <div style="display:none;">
                        不符部分：
                        <asp:TextBox ID="TXTDTL065" runat="server" MaxLength="50" /></div></li>
                    <li class="li-paddingmb"><asp:TextBox ID="TXTDTL066" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL066_count','50');" />
                        <br/><asp:Label ID="TXTDTL066_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </li>
               </ul>
                <ul>
                    <li class="li-padding">5.擋土設施</li>
                    <li class="li-paddingmb">
                        <asp:DropDownList ID="DDLDTL067" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                            <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                            <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                            <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                            <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                            <asp:ListItem Value="無此項">無此項</asp:ListItem>
                        </asp:DropDownList>
                        <div style="display:none;">
                        不符部分：
                        <asp:TextBox ID="TXTDTL068" runat="server" MaxLength="50" /></div></li>
                    <li class="li-paddingmb"><asp:TextBox ID="TXTDTL069" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL069_count','50');" />
                        <br/><asp:Label ID="TXTDTL069_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </li></ul>
                <ul>
                    <li class="li-padding">6.植生工程</li>
                    <li class="li-paddingmb">
                        <asp:DropDownList ID="DDLDTL070" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                            <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                            <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                            <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                            <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                            <asp:ListItem Value="無此項">無此項</asp:ListItem>
                        </asp:DropDownList>
                        <div style="display:none;">
                        不符部分：
                        <asp:TextBox ID="TXTDTL071" runat="server" MaxLength="50" /></div></li>
                    <li class="li-paddingmb"><asp:TextBox ID="TXTDTL072" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL072_count','50');" />
                        <br/><asp:Label ID="TXTDTL072_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </li>
                </ul>
                <ul>
                    <li class="li-padding">7.邊坡穩定措施</li>
                    <li class="li-paddingmb">
                        <asp:DropDownList ID="DDLDTL073" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                            <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                            <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                            <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                            <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                            <asp:ListItem Value="無此項">無此項</asp:ListItem>
                        </asp:DropDownList>
                        <div style="display:none;">
                        不符部分：
                        <asp:TextBox ID="TXTDTL074" runat="server" MaxLength="50" /></div></li>
                    <li class="li-paddingmb"><asp:TextBox ID="TXTDTL075" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL075_count','50');" />
                        <br/><asp:Label ID="TXTDTL075_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </li>
                </ul>
                <ul>
                    <li class="li-padding ">8.其他</li>
                    <li class="li-paddingmb nonepd2">
                        <div style="display:none;">
                        <asp:DropDownList ID="DDLDTL076" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="尚未施作">尚未施作</asp:ListItem>
                            <asp:ListItem Value="依計畫施作">依計畫施作</asp:ListItem>
                            <asp:ListItem Value="有缺失應改善">有缺失應改善</asp:ListItem>
                            <asp:ListItem Value="缺失應改未改或未依計畫施作，通知大地處查處">缺失應改未改或未依計畫施作，通知大地處查處</asp:ListItem>
                            <asp:ListItem Value="施作完成">施作完成</asp:ListItem>
                            <asp:ListItem Value="無此項">無此項</asp:ListItem>
                        </asp:DropDownList>
                        不符部分：
                        <asp:TextBox ID="TXTDTL077" runat="server" MaxLength="50" /></div>
                        <asp:TextBox ID="TXTDTL078" runat="server" MaxLength="50" onkeyup="textcount(this,'TXTDTL078_count','50');" />
                        <br/><asp:Label ID="TXTDTL078_count" runat="server" Text="(0/50)" ForeColor="Red" />
                    </li></ul>
                <ul><li>（六）當週重大事件</li>
                    <li class="nonepd2">
                        <asp:TextBox ID="TXTDTL079" runat="server" width="100%" onkeyup="textcount(this,'TXTDTL079_count','200');" MaxLength="200" Height="100" TextMode="MultiLine" />
                        <asp:Label ID="TXTDTL079_count" runat="server" Text="(0/200)" ForeColor="Red" />
                        
                    </li>
                 </ul>
                </div>

              <div class="RWDfrom RWD_3" style="border-top:none;">
                <ul>
                    <li style="vertical-align:middle;">（七）通知水土保持義務人及營造單位改正事項</li>
                    <li class="nonepd2">
                        <asp:Label ID="TXTDTL080" runat="server" width="100%" onkeyup="textcount(this,'TXTDTL080_count','200');" MaxLength="200" Height="100" TextMode="MultiLine" />
                        <asp:Label ID="TXTDTL080_count" runat="server" Text="(0/200)" ForeColor="Red" Visible="false" />
                        <asp:TextBox ID="TXTDTL093" runat="server" width="120px"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTDTL093_CalendarExtender" runat="server" TargetControlID="TXTDTL093" Format="yyyy-MM-dd"></asp:CalendarExtender>
                        <span class="gray">（範例：2020-01-02）</span></li>
                </ul>
                <ul>
                    <li>（八）未依計畫施作事項及改正期限</li>
                    <li class="nonepd2">
                        <asp:Label ID="LBDTL092" runat="server" /><br/>
                        <asp:TextBox ID="TXTDTL081" runat="server" width="120px"></asp:TextBox>
                        <asp:CalendarExtender ID="TXTDTL081_CalendarExtender" runat="server" TargetControlID="TXTDTL081" Format="yyyy-MM-dd"></asp:CalendarExtender>
                        <span class="gray">（範例：2020-01-02）</span>
                    </li>
                </ul>
                <ul>
                    <li>（九）累計進度百分比</li>
                    <li class="nonepd2">
                        <asp:Label ID="TXTDTL082" runat="server" onkeyup="chknumber(this);"/> %
                        <span class="gray" style="display:none;">（範例：60）</span><br />
                    </li>
                </ul>
                </div>
        
                <table class="supervision-innerTable" style="display:none;">
                <tr><td colspan="12" style="text-align: left;">
                    （十一）臨時水保設施完成項目（如尚未施作完成則免）
                    <asp:HyperLink ID="DLExcel01" runat="server" Text="excel範本下載" NavigateUrl="../sysFile/臨時水保設施完成項目.xlsx" /></td></tr>
                <tr><td colspan="12">
                        <asp:FileUpload ID="TXTDTL083_FileUpload" runat="server" />
                        <asp:Button ID="TXTDTL083_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL083_fileuploadok_Click"/>
                        <asp:TextBox ID="TXTDTL083" runat="server" Width="70px" Visible="false"/>
                        <asp:Button ID="TXTDTL083_fileclean" runat="server" CssClass="wordttb" Text="X" OnClientClick="return confirm('檔案刪除後無法復原，請確認真的要刪除檔案!!!')" OnClick="TXTDTL083_fileclean_Click" /><br />
                        <asp:HyperLink ID="Link083" runat="server" CssClass="word" Target="_blank"></asp:HyperLink><br />
                        <span>※ 上傳格式限定為excel，檔案大小請於50mb以內</span></td></tr>
                <tr><td colspan="12">
                    （十二）永久水保設施完成項目（如尚未施作完成則免）
                    <asp:HyperLink ID="DLExcel02" runat="server" Text="excel範本下載" NavigateUrl="../sysFile/永久水保設施完成項目.xlsx" /></td></tr>
                <tr><td colspan="12">
                        <asp:FileUpload ID="TXTDTL084_FileUpload" runat="server" />
                        <asp:Button ID="TXTDTL084_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTDTL084_fileuploadok_Click"/>
                        <asp:TextBox ID="TXTDTL084" runat="server" Width="70px" Visible="false"/>
                        <asp:Button ID="TXTDTL084_fileclean" runat="server" CssClass="wordttb" Text="X" OnClientClick="return confirm('檔案刪除後無法復原，請確認真的要刪除檔案!!!')" /><br/>
                        <asp:HyperLink ID="Link084" runat="server" CssClass="word" Target="_blank"></asp:HyperLink><br/>
                        <span>※ 上傳格式限定為excel，檔案大小請於50mb以內</span></td></tr>

                <tr><td colspan="2">承辦監造技師：
                        <asp:HyperLink ID="HyperLink085" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink>
                        <asp:Image ID="TXTDTL085_img" runat="server" Visible="False" /></td>
                    <td colspan="10">(簽章)<asp:TextBox ID="TXTDTL085" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTDTL085_FileUpload" runat="server"  />
                        <asp:Button ID="TXTDTL085_fileuploadok" runat="server" Text="上傳簽章" OnClick="TXTDTL085_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL085_fileuploaddel" runat="server" Text="x" ToolTip="刪除簽章" OnClientClick="return confirm('簽章後無法復原，請確認真的要簽章!!!')" OnClick="TXTDTL085_fileuploaddel_Click" />
                        <span>檔案大小請小於 10Mb，請上傳 jpg, png 格式圖檔</span></td></tr>
                <tr><td colspan="2">營造單位：
                        <asp:HyperLink ID="HyperLink086" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink>
                        <asp:Image ID="TXTDTL086_img" runat="server" Visible="False" /></td>
                    <td colspan="10">(簽章)<asp:TextBox ID="TXTDTL086" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTDTL086_FileUpload" runat="server"  />
                        <asp:Button ID="TXTDTL086_fileuploadok" runat="server" Text="上傳簽章" OnClick="TXTDTL086_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL086_fileuploaddel" runat="server" Text="x" ToolTip="刪除簽章" OnClientClick="return confirm('簽章後無法復原，請確認真的要簽章!!!')" OnClick="TXTDTL086_fileuploaddel_Click" />
                        <span>檔案大小請小於 10Mb，請上傳 jpg, png 格式圖檔</span></td></tr>
                <tr><td colspan="2">水土保持義務人：
                        <asp:HyperLink ID="HyperLink087" runat="server" CssClass="imgUpload" Target="_blank"></asp:HyperLink>
                        <asp:Image ID="TXTDTL087_img" runat="server" Visible="False" /></td>
                    <td colspan="10">(簽章)<asp:TextBox ID="TXTDTL087" runat="server" Width="70px" Visible="False" />
                        <asp:FileUpload ID="TXTDTL087_FileUpload" runat="server"  />
                        <asp:Button ID="TXTDTL087_fileuploadok" runat="server" Text="上傳簽章" OnClick="TXTDTL087_fileuploadok_Click" />
                        <asp:Button ID="TXTDTL087_fileuploaddel" runat="server" Text="x" ToolTip="刪除簽章" OnClientClick="return confirm('簽章後無法復原，請確認真的要簽章!!!')" OnClick="TXTDTL087_fileuploaddel_Click" />
                        <span>檔案大小請小於 10Mb，請上傳 jpg, png 格式圖檔</span></td></tr>

                </table>
               </div>
            </div>
			
			<div class="OFsegG">
                <div style="float:left"> <img src="../images/btn/btn005-06.png?202108160528" alt=""></div>
                <div class="lab">
                   <div class="labcolor1" style="color:#4e7a10;"><div class="icon1" style="background:#f0ffd9;"></div>原核定</div>
                   <div class="labcolor2" style="color:#000;"><div class="icon2" style="background:#fdfff0"></div>現場量測</div>
                </div>

                <asp:Label ID="GVMSG" runat="server" Text="查無資料" Visible="true" class="nodata"/>
				<div style="clear:both;"></div>
                   <div class="detailsGrid">
                      <h2 class="SWCfl openh2">水保設施項目<img src="../images/btn/btn-close.png" alt=""/></h2>
                                <asp:GridView ID="SDIList" runat="server" CssClass="OFcheckG AutoNewLine" AutoGenerateColumns="False" Height="50"
                                  OnDataBound="SDIList_DataBound" OnRowCommand="SDIList_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="SDIFD003" HeaderText="水土保持設施類別" HeaderStyle-Width="" />
                                        <asp:BoundField DataField="SDIFD004" HeaderText="設施名稱<br>（位置或編號）" HeaderStyle-Width="510px" HtmlEncode="false" />
                                        <asp:BoundField DataField="SDIFD005" HeaderText="設施型式" HeaderStyle-Width="270px" />
                <asp:TemplateField HeaderText="數" HeaderStyle-Width="10px">
                    <ItemTemplate>
                        <asp:Label ID="SDILB006" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDIFD006"))) %>' style="width:100px;" Visible="true"></asp:Label>
                        <asp:Label ID="SDILB006D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDIFD006D"))) %>' style="width:80px;"  Visible="true"></asp:Label>
                        <asp:TextBox ID="CHK001" runat="server" onkeyup="chkdecimal(this)" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001"))) %>' style="width:40px; text-align:center;" Visible="true" />
                        <asp:Label ID="A1" runat="server" Text='~' Visible="true"></asp:Label>
                        <asp:TextBox ID="CHK001_1" runat="server" onkeyup="chkdecimal(this)" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001_1"))) %>' style="width:40px; text-align:center;" Visible="true" />
                        <asp:TextBox ID="CHK001D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001D"))) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false" />
                        <asp:TextBox ID="RCH001" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001"))) %>' style="width:40px; text-align:center;" Visible="false" />
                        <asp:TextBox ID="RCH001_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001_1"))) %>' style="width:40px; text-align:center;" Visible="false" />
                        <asp:TextBox ID="RCH001D" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK001D"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                                        <asp:BoundField DataField="SDIFD007" HeaderText="量" HeaderStyle-Width="10px" />
                <asp:TemplateField HeaderText="數量差異百分比" HeaderStyle-Width="400px">
                    <ItemTemplate>
                        <asp:Label ID="LBCHK002" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK002"))) %>' Visible="true"></asp:Label>
                        <asp:TextBox ID="TXTCHK002" runat="server" onkeyup="chkdecimal(this)" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK002"))) %>' MaxLength="20" style="width:40px; text-align:center;" Visible="true" />
                        <asp:Label ID="LBCHK002pers" runat="server" Text='％' Visible="true"></asp:Label>
                        <asp:Label ID="LBCHK002_1" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK002_1"))) %>' Visible="true"></asp:Label>
                        <asp:TextBox ID="TXTCHK002_1" runat="server" onkeyup="chkdecimal(this)" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK002_1"))) %>' MaxLength="20" style="width:40px; text-align:center;" Visible="true" />
						<asp:Label ID="LBCHK002pers_1" runat="server" Text='％' Visible="true"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                                        <asp:BoundField DataField="SDIFD008" HeaderText="檢核項目" HeaderStyle-Width="480px" />
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
                        <asp:Label ID="LB004D" runat="server" Text='<%# Convert.ToString(Eval("SDICHK004D")) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false" />
                        <asp:TextBox ID="CHK004D" runat="server" Text='<%# Convert.ToString(Eval("SDICHK004D")) %>' style="width:40px; text-align:center;" Visible="true" Enabled="false" />
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
                                        <asp:BoundField DataField="SDIFD015" HeaderText="" HeaderStyle-Width="60px" />
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
                <asp:TemplateField HeaderText="施工完成" HeaderStyle-Width="100px">
                    <ItemTemplate>
						<asp:DropDownList ID="DDLDONE" runat="server" SelectedValue='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK009"))) %>' Visible="false"><asp:ListItem>未完成</asp:ListItem><asp:ListItem>完成</asp:ListItem><asp:ListItem>已由永久設施取代</asp:ListItem></asp:DropDownList>
                        <asp:TextBox ID="RCH009" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDICHK009"))) %>' style="width:40px; text-align:center;" Visible="false" Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>

                                        <asp:TemplateField Visible="true" HeaderStyle-Width="100px" >
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="MODIFYDATA" CommandArgument="<%#Container.DataItemIndex %>" CommandName="Modify" Text="修改" Visible="false" />
                                                <asp:HiddenField ID="HDSDI001" runat="server" Value='<%# Eval("SDIFD001") %>' />
                                                <asp:HiddenField ID="HDSDI011" runat="server" Value='<%# Eval("SDIFD011") %>' />
                                                <asp:HiddenField ID="HDSDINI" runat="server" Value='<%# Container.DataItemIndex %>' />
												<asp:HiddenField ID="HDSDI008" runat="server" Value='<%# Eval("SDICHK008") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
										<asp:TemplateField HeaderText="是否漸變" HeaderStyle-Width="5%">
											<ItemTemplate>
												<asp:Label ID="LB019" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("SDILB019"))) %>' style="width:40px; text-align:center;" />
											</ItemTemplate>
										</asp:TemplateField>
                                    </Columns>
                                </asp:GridView></div>
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
                    <br><span class="span2">110臺北市信義區松德路300號3樓 　(02)27593001   臺北市民當家熱線1999</span>
                    <br><span class="span2">資料更新：2017.5.19　來訪人數：123456789 </span></p>
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
