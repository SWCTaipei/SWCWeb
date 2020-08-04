<!--
    Soil and Water Conservation Platform Project is a web applicant tracking system which allows citizen can search, view and manage their SWC applicant case.
    Copyright (C) <2020>  <Geotechnical Engineering Office, Public Works Department, Taipei City Government>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or any later version.
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
-->

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="swc_104.aspx.vb" Inherits="swc_104" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>水土保持申請案件</title>
        <script type="text/javascript">

            function radcancel(radElement) {
                var radtemp = document.getElementById(radElement);
                radtemp.checked = false;
            }
            
            function textboxmode(chkElement, txtElement) {
                var chktemp = document.getElementById(chkElement);
                var textboxtemp = document.getElementById(txtElement);
                if (chktemp.checked == true) {
                    textboxtemp.removeAttribute('readOnly'); 
                }
                else {
                    textboxtemp.setAttribute('readOnly', 'readOnly');
                }
            }

            function chknumber09comm(objElement) {
                var chrstring = objElement.value;
                var chr = chrstring.substring(chrstring.length - 1, chrstring.length);
                if ((chr != ';') && (chr != '0') && (chr != '1') && (chr != '2') && (chr != '3') && (chr != '4') && (chr != '5') && (chr != '6') && (chr != '7') && (chr != '8') && (chr != '9')) {
                    objElement.value = objElement.value.substring(0, chrstring.length - 1);
                }
            }
            function chknumber09(objElement) {
                var chrstring = objElement.value;
                var chr = chrstring.substring(chrstring.length - 1, chrstring.length);
                if ((chr != '0') && (chr != '1') && (chr != '2') && (chr != '3') && (chr != '4') && (chr != '5') && (chr != '6') && (chr != '7') && (chr != '8') && (chr != '9')) {
                    objElement.value = objElement.value.substring(0, chrstring.length - 1);
                }
            }
            function chknumber(objElement) {
                var chrstring = objElement.value;
                var chr = chrstring.substring(chrstring.length - 1, chrstring.length);
                if ((chr != '.') && (chr != '0') && (chr != '1') && (chr != '2') && (chr != '3') && (chr != '4') && (chr != '5') && (chr != '6') && (chr != '7') && (chr != '8') && (chr != '9')) {
                    objElement.value = objElement.value.substring(0, chrstring.length - 1);
                }
            }
            function textcount(txtobj, labobj, txtcount) {

                var textboxtemp = document.getElementById(txtobj);
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
            }  //end function textcount()
            
            function chknull(jTxtBox) {
                var jChkValue = document.getElementById(jTxtBox).value;
                var jUpLoadFile = document.getElementById('VrFileUpload_hyperlink').innerText;

                if (jChkValue.trim() == '') {
                    alert('請輸入環景照片名稱');
                    return false;
                }
                if (jUpLoadFile.trim() == '') {
                    alert('請先上傳環景照片');
                    return false;
                }
            }
    </script>
    <style type="text/css">
        /*tr:nth-of-type(even){background-color:#FFF;}
tr:nth-of-type(odd){background-color:#E8E8E8;}
table {
	border-collapse:collapse;
} */

.title {
	font-family:Arial, "微軟正黑體", "標楷體", "新細明體";
	font-size: 20px;
	font-weight: bold;
	text-align:center;
	line-height:40px;
}
.wordtt {
	font-family:Arial, "標楷體", "微軟正黑體", "新細明體";
	font-size: 16px;
	line-height:30px;
	text-align:left;
	text-indent:5px;	
}
.wordred {
	font-family:Arial, "標楷體", "微軟正黑體", "新細明體";
	font-size: 16px;
	line-height: 30px;
	text-align: left;
	text-indent: 5px;
	color: #F00;
}
.word {
	font-family: Arial,"標楷體", "微軟正黑體", "新細明體";
	font-size: 16px;
	line-height:20px;
	text-align:left;
}
.wordright {
	font-family: Arial,"標楷體", "微軟正黑體", "新細明體";
	font-size: 16px;
	line-height:20px;
	text-align:right;
}
.wordtb {
	font-family: Arial, "標楷體", "微軟正黑體", "新細明體";
	font-size: 14px;
	line-height: 18px;
	text-align: left;
}
.wordtbcenter {
	font-family: Arial, "標楷體", "微軟正黑體", "新細明體";
	font-size: 14px;
	line-height: 18px;
	text-align: center;
}

.ps {
	font-size: 12px;
	color: #F00;
}
	body
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
			background-image: url(image/ttbg.png);
         	background-repeat: repeat-x;
        }
        .body
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
			background-image: url(image/ttbg.png);
         	background-repeat: repeat-x;
        }
        .ttdcenter
        {
               text-align:center;
               font-family :標楷體;
               font-size:40px;
        }
        .tdcenter
        {
               text-align:center;
               font-family :標楷體;
               font-size:larger;
        }
        .tdleft
        {
               text-align:left;
               font-family :標楷體;
               font-size:larger;
        }
        .tdright
        {
               text-align:right;
               font-family :標楷體;
               font-size:larger;
        }
        .mailtext
        {
               text-align:left;
               font-family :標楷體;
               font-size:larger;
        }
        .mailps
        {
               text-align:center;
               font-family :標楷體;
               font-size:20px;
            height: 32px;
        }
                
.alltitle {
	font-family: "微軟正黑體", "標楷體", "新細明體";
	font-size: 30px;
	font-weight: bold;
	text-align:center;
	line-height:50px;
}
.title {
	font-family: "微軟正黑體", "標楷體", "新細明體";
	font-size: 20px;
	font-weight: bold;
	line-height:40px;
}
.wordtt {
	font-family: "微軟正黑體", "標楷體", "新細明體";
	font-size: 16px;
	line-height:35px;
	text-align:center;
	
}
.wordttleft {
	font-family: "微軟正黑體", "標楷體", "新細明體";
	font-size: 16px;
	line-height:35px;
	text-align:left;
	
}
.wordttb {
	font-family: "微軟正黑體", "標楷體", "新細明體";
	font-size: 16px;
	line-height:16px;
	text-align:center;
	
}

.wordred {
	font-family: "微軟正黑體", "標楷體", "新細明體";
	font-size: 16px;
	line-height: 30px;
	text-align: center;
	color: #F00;
}
.wordblack {
	font-family: "微軟正黑體", "標楷體", "新細明體";
	font-size: 16px;
	line-height: 30px;
	text-align: center;
	
}
.word {
	font-family: "微軟正黑體", "標楷體", "新細明體";
	font-size: 16px;
	line-height:20px;
	text-align:left;
}

.wordtb {
	font-family:  "標楷體", "微軟正黑體", "新細明體";
	font-size: 14px;
	line-height: 18px;
	text-align: left;
}


.ps {
	font-size: 12px;
	color: #F00;
}
        
.style3 {
	font-family: "微軟正黑體", "標楷體", "新細明體";
	font-size: 20px;
	line-height:22px;
} 
.style4 {
	font-family: "微軟正黑體", "標楷體", "新細明體";
	font-size: 24px;
	line-height:29px;
}        
        .style5
        {
            font-family: 微軟正黑體, 標楷體, 新細明體;
            font-size: 16px;
            line-height: 35px;
            text-align: center;
            text-indent: 5px;
            height: 37px;
        }
        .style6
        {
            font-family: 微軟正黑體, 標楷體, 新細明體;
            font-size: 16px;
            line-height: 20px;
            text-align: left;
            height: 37px;
        }
    </style>
    <link rel="stylesheet" href="css/swc-sceneEdit.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true"></asp:ToolkitScriptManager>
        <div align="center">
            <table id="maintable" runat="server" align="center" style="margin: 0px; border-style: none; border-width: 0px; width: 995px;" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <iframe id="menutitle" runat="server" src="menutitle.aspx" width="995" height="125" frameborder="0" style="border-width: 0px; border-style: none;" align="middle"></iframe>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="title" style="padding: 0px; text-align: center; margin: 0px; border-style: none; border-width: 0px; width: 995px; border-spacing: 0px;" cellspacing="0" align="center">
                            <tr>
                                <td style="width: 705px; font-weight: bold;" class="tdleft">現在位置：<a href="http://172.28.100.55/tslm/" style="text-decoration: none" target="_top">首頁</a>&gt; <a style="text-decoration: none" href="http://172.28.100.55/tcge/swc_104.aspx" target="_self">水土保持案件</a> > <span style="color: #CC3300;">水保申請案件</span>
                                </td>
                                <td style="width: 290px; font-weight: bold;" class="tdright">
                                    <asp:Label ID="labname" runat="server" Text="Label" Font-Names="標楷體"></asp:Label>
                                    <asp:Button ID="btnlogout" runat="server" Text="登出" Height="22px" UseSubmitBehavior="False" OnClick="btnlogout_Click" Font-Names="標楷體" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="tdleft">
                                    <div style="float: left;width: 348px;">
                                        <img src="image/title_swc.png" alt="" />
                                    </div>
                                    <asp:ImageButton ID="btswc" runat="server" Width="121" Height="51" AlternateText="水保申請案件" ImageUrl="~/image/menus_swc.png" OnClick="btswc_Click" Enabled="false" />
                                    <asp:ImageButton ID="btswcdp" runat="server" Width="121" Height="51" AlternateText="災害防治" ImageUrl="~/image/menus_swcdp.png" OnClick="btswcdp_Click" />
                                    <asp:ImageButton ID="btswcnn" runat="server" Width="121" Height="51" AlternateText="免擬具" ImageUrl="~/image/menus_swcnn.png" OnClick="btswcnn_Click" />
                                    <asp:ImageButton ID="btswcg" runat="server" Width="121" Height="51" AlternateText="水保輔導案件" ImageUrl="~/image/menus_swcg.png" OnClick="btswcg_Click" />
                                    <asp:ImageButton ID="btswcgm" runat="server" Width="121" Height="51" AlternateText="輔導預約管理" ImageUrl="~/image/menus_reserve.png" OnClick="btswcgm_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Gridpanel" runat="server" HorizontalAlign="Center">
                            <asp:Panel ID="Querypane" runat="server">
                                <table id="querytable" runat="server" style="padding: 0px; text-align: center; margin: 0px; border-style: none; border-width: 0px; width: 960px; border-spacing: 0px;" cellspacing="0" align="center">
                                    <tr>
                                        <td style="width: 477px;">
                                            <table id="tablequery" style="border: 1px solid; width: 477px; text-align: left">
                                                <tr>
                                                    <td class="style2">
                                                        <asp:Label ID="Label1" runat="server" Font-Names="標楷體" Font-Size="Small" Text="狀態："></asp:Label>
                                                        <asp:CheckBox ID="CheckBoxq1" runat="server" Font-Names="標楷體" Font-Size="Small" Text="受理中" />&nbsp;
                                                        <asp:CheckBox ID="CheckBoxq2" runat="server" Font-Names="標楷體" Font-Size="Small" Text="審查中" />&nbsp;
                                                        <asp:CheckBox ID="CheckBoxq3" runat="server" Font-Names="標楷體" Font-Size="Small" Text="已核定" />&nbsp;
                                                        <asp:CheckBox ID="CheckBoxq4" runat="server" Font-Names="標楷體" Font-Size="Small" Text="施工中" />&nbsp;
                                                        <asp:CheckBox ID="CheckBoxq5" runat="server" Font-Names="標楷體" Font-Size="Small" Text="停工中" />&nbsp;
                                                        <asp:CheckBox ID="CheckBoxq6" runat="server" Font-Names="標楷體" Font-Size="Small" Text="已完工" />&nbsp;
                                                        <br />
                                                        <asp:Label ID="Label144" runat="server" Text="　　　" Font-Names="標楷體" Font-Size="Small"></asp:Label>
                                                        <asp:CheckBox ID="CheckBoxq7" runat="server" Font-Names="標楷體" Font-Size="Small" Text="廢止" />&nbsp
                                                        <asp:CheckBox ID="CheckBoxq8" runat="server" Font-Names="標楷體" Font-Size="Small" Text="撤銷" />&nbsp;
                                                        <asp:CheckBox ID="CheckBoxq9" runat="server" Font-Names="標楷體" Font-Size="Small" Text="失效" />&nbsp;
                                                        <asp:CheckBox ID="CheckBoxq10" runat="server" Font-Names="標楷體" Font-Size="Small" Text="不予受理" />&nbsp;
                                                        <asp:CheckBox ID="CheckBoxq11" runat="server" Font-Names="標楷體" Font-Size="Small" Text="不予核定" />&nbsp;
                                                        <asp:CheckBox ID="CheckBoxq14" runat="server" Font-Names="標楷體" Font-Size="Small" Text="已變更" />&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style2">
                                                        <asp:Label ID="Label2" runat="server" Text="水土保持書件類別：" Font-Names="標楷體" Font-Size="Small"></asp:Label>
                                                        <asp:CheckBox ID="CheckBoxq12" runat="server" Font-Names="標楷體" Font-Size="Small" Text="水土保持計畫" />
                                                        <asp:CheckBox ID="CheckBoxq13" runat="server" Font-Names="標楷體" Font-Size="Small" Text="簡易水保" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style2">
                                                        <asp:Label ID="Label3" runat="server" Text="水土保持申請書件名稱：" Font-Names="標楷體" Font-Size="Small"></asp:Label>
                                                        <asp:TextBox ID="TextBoxq1" runat="server" Width="280" Font-Names="標楷體" Font-Size="Small"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style2">
                                                        <asp:Label ID="Label17" runat="server" Text="行政審查編號：" Font-Names="標楷體" Font-Size="Small"></asp:Label>
                                                        <asp:TextBox ID="TextBoxq2" runat="server" Width="110" Font-Names="標楷體" Font-Size="Small"></asp:TextBox>
                                                        &nbsp;
                                                        <asp:Label ID="Labe48" runat="server" Text="監督管理編號：" Font-Names="標楷體" Font-Size="Small"></asp:Label>
                                                        <asp:TextBox ID="TextBoxq3" runat="server" Width="110" Font-Names="標楷體" Font-Size="Small"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style2">
                                                        <asp:Label ID="Labelland" runat="server" Text="地段：" Font-Names="標楷體" Font-Size="Small"></asp:Label>
                                                        <asp:DropDownList ID="Dorplistq3" runat="server" OnSelectedIndexChanged="Dorplistq3_SelectedIndexChanged" AutoPostBack="True" Width="55px">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem Value="16">北投</asp:ListItem>
                                                            <asp:ListItem Value="15">士林</asp:ListItem>
                                                            <asp:ListItem Value="14">內湖</asp:ListItem>
                                                            <asp:ListItem Value="10">中山</asp:ListItem>
                                                            <asp:ListItem Value="03">中正</asp:ListItem>
                                                            <asp:ListItem Value="17">信義</asp:ListItem>
                                                            <asp:ListItem Value="02">大安</asp:ListItem>
                                                            <asp:ListItem Value="13">南港</asp:ListItem>
                                                            <asp:ListItem Value="11">文山</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Label ID="Labellandarea" runat="server" Text="區" Font-Names="標楷體" Font-Size="Small"></asp:Label>&nbsp;
                                                        <asp:DropDownList ID="Dorplistq4" runat="server" Width="55px" AutoPostBack="True" OnSelectedIndexChanged="Dorplistq4_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="Labellandsection" runat="server" Text="段" Font-Names="標楷體" Font-Size="Small"></asp:Label>&nbsp;
                                                        <asp:DropDownList ID="Dorplistq5" runat="server" Width="45px">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="Labellandsubsection" runat="server" Text="小段" Font-Names="標楷體" Font-Size="Small"></asp:Label>&nbsp;
                                                        <asp:TextBox ID="TextBoxq15" runat="server" Width="50px"></asp:TextBox>
                                                        <asp:Label ID="Labellandno" runat="server" Text="地號" Font-Names="標楷體" Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style2">
                                                        <asp:Label ID="Label8" runat="server" Text="水保義務人：" Font-Names="標楷體" Font-Size="Small"></asp:Label>
                                                        <asp:TextBox ID="TextBoxq4" runat="server" Width="130" Font-Names="標楷體" Font-Size="Small"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="Label9" runat="server" Text="承辦技師：" Font-Names="標楷體" Font-Size="Small"></asp:Label>
                                                        <asp:TextBox ID="TextBoxq5" runat="server" Width="130" Font-Names="標楷體" Font-Size="Small"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style2">
                                                        <asp:Label ID="Label10" runat="server" Text="審查單位(受委託單位)：" Font-Names="標楷體" Font-Size="Small"></asp:Label>
                                                        <asp:TextBox ID="TextBoxq6" runat="server" Width="280" Font-Names="標楷體" Font-Size="Small"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style7">
                                                        <asp:Label ID="Label11" runat="server" Text="核定日期：" Font-Names="標楷體" Font-Size="Small"></asp:Label>
                                                        <asp:TextBox ID="TextBoxq7" runat="server" Width="130" Font-Names="標楷體" Font-Size="Small"></asp:TextBox>
                                                        <asp:CalendarExtender ID="TextBoxq7_CalendarExtender" runat="server" Enabled="True" TargetControlID="TextBoxq7" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                                        <asp:Label ID="Label12" runat="server" Text="～" Font-Names="標楷體" Font-Size="Small"></asp:Label>
                                                        <asp:TextBox ID="TextBoxq8" runat="server" Width="130" Font-Names="標楷體" Font-Size="Small"></asp:TextBox>
                                                        <asp:CalendarExtender ID="TextBoxq8_CalendarExtender" runat="server" Enabled="True" TargetControlID="TextBoxq8" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style2">
                                                        <asp:Label ID="Label13" runat="server" Text="開工日期：" Font-Names="標楷體" Font-Size="Small"></asp:Label>
                                                        <asp:TextBox ID="TextBoxq9" runat="server" Width="130" Font-Names="標楷體" Font-Size="Small"></asp:TextBox>
                                                        <asp:CalendarExtender ID="TextBoxq9_CalendarExtender" runat="server" Enabled="True" TargetControlID="TextBoxq9" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                                        <asp:Label ID="Label14" runat="server" Text="～" Font-Names="標楷體" Font-Size="Small"></asp:Label>
                                                        <asp:TextBox ID="TextBoxq10" runat="server" Width="130" Font-Names="標楷體" Font-Size="Small"></asp:TextBox>
                                                        <asp:CalendarExtender ID="TextBoxq10_CalendarExtender" runat="server" Enabled="True" TargetControlID="TextBoxq10" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style2">
                                                        <asp:Label ID="Label15" runat="server" Text="監造技師：" Font-Names="標楷體" Font-Size="Small"></asp:Label>
                                                        <asp:TextBox ID="TextBoxq11" runat="server" Width="130" Font-Names="標楷體" Font-Size="Small"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="Labe47" runat="server" Text="轄區別：" Font-Names="標楷體" Font-Size="Small"></asp:Label>
                                                        <asp:DropDownList ID="Dorplistq1" runat="server" Font-Names="標楷體" Font-Size="Medium" Width="100px">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem Value="北一區">北一區</asp:ListItem>
                                                            <asp:ListItem Value="北二區">北二區</asp:ListItem>
                                                            <asp:ListItem Value="北三區">北三區</asp:ListItem>
                                                            <asp:ListItem Value="北四區">北四區</asp:ListItem>
                                                            <asp:ListItem Value="北五區">北五區</asp:ListItem>
                                                            <asp:ListItem Value="北六區">北六區</asp:ListItem>
                                                            <asp:ListItem Value="北七區">北七區</asp:ListItem>
                                                            <asp:ListItem Value="北八區">北八區</asp:ListItem>
                                                            <asp:ListItem Value="北九區" Enabled="false">北九區</asp:ListItem>
                                                            <asp:ListItem Value="南一區">南一區</asp:ListItem>
                                                            <asp:ListItem Value="南二區">南二區</asp:ListItem>
                                                            <asp:ListItem Value="南三區">南三區</asp:ListItem>
                                                            <asp:ListItem Value="南四區">南四區</asp:ListItem>
                                                            <asp:ListItem Value="南五區">南五區</asp:ListItem>
                                                            <asp:ListItem Value="南六區">南六區</asp:ListItem>
                                                            <asp:ListItem Value="南七區">南七區</asp:ListItem>
                                                            <asp:ListItem Value="南八區">南八區</asp:ListItem>
                                                            <asp:ListItem Value="南九區" Enabled="false">南九區</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style2">
                                                        <asp:Label ID="Label16" runat="server" Text="委外檢查公會：" Font-Names="標楷體" Font-Size="Small"></asp:Label>
                                                        <asp:TextBox ID="TextBoxq12" runat="server" Width="95" Font-Names="標楷體" Font-Size="Small"></asp:TextBox>&nbsp&nbsp&nbsp&nbsp
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style2">
                                                        <asp:Label ID="Label18" runat="server" Text="完工日期：" Font-Names="標楷體" Font-Size="Small"></asp:Label>
                                                        <asp:TextBox ID="TextBoxq13" runat="server" Width="130" Font-Names="標楷體" Font-Size="Small"></asp:TextBox>
                                                        <asp:CalendarExtender ID="TextBoxq13_CalendarExtender" runat="server" Enabled="True" TargetControlID="TextBoxq13" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                                        <asp:Label ID="Label19" runat="server" Text="～" Font-Names="標楷體" Font-Size="Small"></asp:Label>
                                                        <asp:TextBox ID="TextBoxq14" runat="server" Width="130" Font-Names="標楷體" Font-Size="Small"></asp:TextBox>
                                                        <asp:CalendarExtender ID="TextBoxq14_CalendarExtender" runat="server" Enabled="True" TargetControlID="TextBoxq14" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center; height: 30px" class="style2">
                                                        <asp:TextBox ID="swcquereytext" runat="server" Font-Names="標楷體" Height="25px"
                                                            TextMode="MultiLine" Width="288px" Visible="false"></asp:TextBox>
                                                        <asp:Button ID="qButton1" runat="server" Text="查詢" OnClick="qButton1_Click" Font-Names="標楷體" />&nbsp;
                                                        <asp:Button ID="qButton2" runat="server" Text="清除" OnClick="qButton2_Click" Font-Names="標楷體" />
                                                        <asp:Button ID="Button1" runat="server" Text="倒資料到OPENSWC" Visible="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="text-align: center; width: 6px"></td>
                                        <td style="text-align: center; width: 477px">
                                            <asp:Panel ID="Panelstatic" runat="server" Width="477">
                                                <table id="table1" width="477" align="center" style="border: 1px solid">
                                                    <tr>
                                                        <td class="style4" colspan="8">
                                                            <asp:Label ID="Label38" runat="server" Text="水保申請案件行政區案件狀態統計表" Font-Names="標楷體" Font-Size="Medium"></asp:Label>&nbsp;
                                                            <asp:ImageButton ID="exportexcel" runat="server" Height="20px" ImageUrl="image/excel.png" Width="20px" ToolTip="輸出統計表" />
                                                            <br />
                                                            <asp:Label ID="Label20" runat="server" Text="(85年至103年10月29日)" Font-Names="標楷體" Font-Size="Medium"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style3" bgcolor="#0099FF"></td>
                                                        <td class="style3" bgcolor="#0099FF">
                                                            <asp:Label ID="Label21" runat="server" Text="受理中" Font-Names="標楷體" Font-Size="Small"></asp:Label></td>
                                                        <td class="style3" bgcolor="#0099FF">
                                                            <asp:Label ID="Label22" runat="server" Text="審查中" Font-Names="標楷體" Font-Size="Small"></asp:Label></td>
                                                        <td class="style3" bgcolor="#0099FF">
                                                            <asp:Label ID="Label23" runat="server" Text="已核定" Font-Names="標楷體" Font-Size="Small"></asp:Label></td>
                                                        <td class="style3" bgcolor="#0099FF">
                                                            <asp:Label ID="Label24" runat="server" Text="施工中" Font-Names="標楷體" Font-Size="Small"></asp:Label></td>
                                                        <td class="style3" bgcolor="#0099FF">
                                                            <asp:Label ID="Label25" runat="server" Text="已完工" Font-Names="標楷體" Font-Size="Small"></asp:Label></td>
                                                        <td class="style3" bgcolor="#0099FF">
                                                            <asp:Label ID="Label26" runat="server" Text="已廢止" Font-Names="標楷體" Font-Size="Small"></asp:Label></td>
                                                        <td class="style3" bgcolor="#0099FF">
                                                            <asp:Label ID="Label27" runat="server" Text="小計" Font-Names="標楷體" Font-Size="Small"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style3">
                                                            <asp:Label ID="Label28" runat="server" Text="北投區" Font-Names="標楷體" Font-Size="Small"></asp:Label></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton11" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton12" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton13" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton14" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton15" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton16" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton17" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style3">
                                                            <asp:Label ID="Label29" runat="server" Text="士林區" Font-Names="標楷體" Font-Size="Small"></asp:Label></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton21" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton22" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton23" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton24" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton25" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton26" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton27" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style3">
                                                            <asp:Label ID="Label30" runat="server" Text="大安區" Font-Names="標楷體" Font-Size="Small"></asp:Label></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton31" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton32" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton33" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton34" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton35" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton36" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton37" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style3">
                                                            <asp:Label ID="Label31" runat="server" Text="中山區" Font-Names="標楷體" Font-Size="Small"></asp:Label></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton41" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton42" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton43" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton44" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton45" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton46" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton47" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style3">
                                                            <asp:Label ID="Label32" runat="server" Text="內湖區" Font-Names="標楷體" Font-Size="Small"></asp:Label></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton51" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton52" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton53" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton54" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton55" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton56" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton57" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style3">
                                                            <asp:Label ID="Label33" runat="server" Text="文山區" Font-Names="標楷體" Font-Size="Small"></asp:Label></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton61" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton62" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton63" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton64" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton65" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton66" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton67" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style3">
                                                            <asp:Label ID="Label34" runat="server" Text="中正區" Font-Names="標楷體" Font-Size="Small"></asp:Label></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton71" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton72" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton73" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton74" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton75" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton76" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton77" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style3">
                                                            <asp:Label ID="Label35" runat="server" Text="信義區" Font-Names="標楷體" Font-Size="Small"></asp:Label></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton81" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton82" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton83" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton84" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton85" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton86" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton87" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style3">
                                                            <asp:Label ID="Label36" runat="server" Text="南港區" Font-Names="標楷體" Font-Size="Small"></asp:Label></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton91" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton92" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton93" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton94" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton95" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton96" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton97" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style3">
                                                            <asp:Label ID="Label37" runat="server" Text="小計" Font-Names="標楷體" Font-Size="Small"></asp:Label></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton01" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton02" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton03" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton04" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton05" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton06" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                        <td class="style3">
                                                            <asp:LinkButton ID="LinkButton07" runat="server" Text="0" Font-Size="Small"></asp:LinkButton></td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <br />
                            <asp:Panel ID="AddNewCase" runat="server" CssClass="tdleft" Width="960px" align="center">
                                查詢到件數：<asp:Label ID="qLabel1" runat="server" Text=""></asp:Label>&nbsp&nbsp&nbsp
                                <asp:ImageButton ID="icon_newcase" runat="server" Width="126" Height="42" alt="back" ImageUrl="image/icon_newcase.png" />
                                <asp:ImageButton ID="icon_excelexport" runat="server" Width="126" Height="42" alt="export excel" ImageUrl="image/icon_exportexcel.png" />
                                <asp:ImageButton ID="icon_mexcelexport" runat="server" Width="126" Height="42" alt="export month excel" ImageUrl="image/icon_exportmexcel.png" />
                                <asp:ImageButton ID="icon_lightps" runat="server" Width="126" Height="42" AlternateText="狀態有疑慮" ImageUrl="~/image/icon_redimage.png" />
                            </asp:Panel>
                            <asp:Panel ID="GridViewpanel" runat="server" align="center">

                                <asp:GridView ID="GridView2" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                                    Width="960px" AllowSorting="True" OnSorting="GridView2_Sorting" AutoGenerateColumns="False"
                                    DataKeyNames="SWC00" EmptyDataText="查無資料"
                                    DataSourceID="SqlDataSource2" OnDataBound="GridView2_DataBound"
                                    OnRowCreated="GridView2_RowCreated" OnRowDeleting="GridView2_RowDeleting"
                                    AllowPaging="True" RowStyle-Font-Names="標楷體"
                                    RowStyle-HorizontalAlign="Center" PageSize="20" HeaderStyle-Font-Names="標楷體"
                                    EnableModelValidation="True">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="SWC00" HeaderText="流水號" SortExpression="SWC00" ItemStyle-Width="50px" />
                                        <asp:BoundField DataField="SWC01" HeaderText="核定編號" SortExpression="SWC01" ItemStyle-Width="80px" />
                                        <asp:BoundField DataField="SWC02" HeaderText="行政審查案件編號" SortExpression="SWC02" ItemStyle-Width="160px" />
                                        <asp:BoundField DataField="SWC04" HeaderText="案件狀態" SortExpression="SWC04" ItemStyle-Width="80px" />
                                        <asp:BoundField DataField="SWC05" HeaderText="水土保持申請書件名稱" SortExpression="SWC05" ItemStyle-Width="250px" />
                                        <asp:BoundField DataField="SWC07" HeaderText="類別" SortExpression="SWC07" ItemStyle-Width="100px" />
                                        <asp:BoundField DataField="SWC08" HeaderText="區" SortExpression="SWC08" ItemStyle-Width="50px" Visible="false" />
                                        <asp:BoundField DataField="SWC09" HeaderText="段" SortExpression="SWC09" ItemStyle-Width="50px" Visible="false" />
                                        <asp:BoundField DataField="SWC10" HeaderText="小段" SortExpression="SWC10" ItemStyle-Width="50px" Visible="false" />
                                        <asp:BoundField DataField="SWC11" HeaderText="地號" SortExpression="SWC11" ItemStyle-Width="50px" Visible="false" />
                                        <asp:BoundField DataField="SWC13" HeaderText="義務人" SortExpression="SWC13" ItemStyle-Width="100px" />
                                        <asp:ButtonField ButtonType="Button" CommandName="detail" Text="詳情" ItemStyle-Width="40px" ControlStyle-Font-Names="標楷體" />
                                        <asp:TemplateField ShowHeader="False" Visible="false">
                                            <ItemTemplate>
                                                <asp:Button ID="delbutton" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('確認刪除這筆資料?')" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:ImageField DataImageUrlField="light" HeaderText="檢核">
                                        </asp:ImageField>
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <EmptyDataRowStyle Font-Size="Larger" ForeColor="Red" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerSettings LastPageText="最後一頁" FirstPageText="第一頁" Mode="NumericFirstLast" NextPageText="下一頁" PreviousPageText="上一頁" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:tslmConnectionString2 %>"
                                    SelectCommand="SELECT [SWC00], [SWC01], [SWC02], [SWC04], [SWC05], [SWC07], [SWC08], [SWC09], [SWC10], [SWC11], [SWC13], [light] FROM [SWCSWC] ORDER BY [SWC00] DESC"
                                    DeleteCommand="DELETE FROM [SWCSWC] WHERE [SWC00] = @SWC00">
                                    <DeleteParameters>
                                        <asp:Parameter Name="SWC00" Type="String" />
                                    </DeleteParameters>
                                </asp:SqlDataSource>
                            </asp:Panel>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Detailpanel" runat="server" Width="960px" align="center">
                            <table width="960" border="0" cellpadding="0" cellspacing="0" align="center">
                                <tr>
                                    <td width="160" height="50" align="left" valign="middle">
                                        <asp:ImageButton ID="icon_goback" runat="server" Width="126" Height="42" alt="back" ImageUrl="image/icon_goback.png" />
                                    </td>
                                    <td width="160" align="left" valign="middle">
                                        <asp:ImageButton ID="icon_editcase" runat="server" Width="126" Height="42" alt="editcase" ImageUrl="image/icon_editcase.png" />
                                    </td>
                                    <td width="160" align="left" valign="middle">
                                        <asp:ImageButton ID="icon_webmap" runat="server" Width="126" Height="42" alt="webmap" ImageUrl="image/icon_webmap.png" />
                                    </td>
                                    <td width="160" align="left" valign="middle">
                                        <asp:ImageButton ID="icon_copycase" runat="server" Width="126" Height="42" alt="copycase" ImageUrl="image/icon_copyswc.png" />
                                    </td>
                                    <td width="160" align="left" valign="middle">
                                        <asp:ImageButton ID="icon_exportpdf" runat="server" Width="126" Height="42" alt="exportpdf" ImageUrl="image/icon_exportpdf.png" />
                                    </td>

                                    <td width="160" align="left" valign="middle">&nbsp;</td>
                                </tr>
                            </table>
                            <table width="960" border="1" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="2" align="left">
                                        <asp:Label ID="lightps" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" bgcolor="#99CCFF" class="title">基本資料
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#E1FFFF" class="wordtt">核定編號</td>
                                    <td class="wordttleft">
                                        <asp:TextBox ID="SWC01" runat="server" CssClass="word" Width="200px" BackColor="Silver"></asp:TextBox>
                                        <!-- 自動編號，水保計畫編法 民國年-流水號，簡易水保編法 A民國年-流水號 -->
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#E1FFFF" class="wordtt">水保局編號 <span class="wordred">*</span></td>
                                    <td class="wordttleft">
                                        <asp:TextBox ID="SWC02" runat="server" CssClass="word" Width="200px" BackColor="#FFFF99"></asp:TextBox>
                                        <asp:Label ID="SWC02PS" CssClass="wordtb" runat="server" Text="(此欄位只能修正一次，請注意!!!!)" ForeColor="Red"></asp:Label>
                                        <asp:TextBox ID="tempswc02" runat="server" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#E1FFFF" class="wordtt">狀態</td>
                                    <td class="wordttleft">
                                        <label for="select"></label>
                                        <asp:DropDownList ID="SWC04" runat="server" CssClass="word">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>退補件</asp:ListItem>
                                            <asp:ListItem>不予受理</asp:ListItem>
                                            <asp:ListItem>受理中</asp:ListItem>
                                            <asp:ListItem>審查中</asp:ListItem>
                                            <asp:ListItem>暫停審查</asp:ListItem>
                                            <asp:ListItem>撤銷</asp:ListItem>
                                            <asp:ListItem>不予核定</asp:ListItem>
                                            <asp:ListItem>已核定</asp:ListItem>
                                            <asp:ListItem>施工中</asp:ListItem>
                                            <asp:ListItem>停工中</asp:ListItem>
                                            <asp:ListItem>已完工</asp:ListItem>
                                            <asp:ListItem>廢止</asp:ListItem>
                                            <asp:ListItem>失效</asp:ListItem>
                                            <asp:ListItem>已變更</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="SWC04Label" runat="server" Text="" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#E1FFFF" class="wordtt">書件名稱 <span class="wordred">*</span></td>
                                    <td class="wordttleft">
                                        <asp:TextBox ID="SWC05" runat="server" CssClass="word" Width="740px"
                                            oninput="textcount('SWC05','SWC05_count','255');" Height="125px"
                                            MaxLength="255" TextMode="MultiLine" ReadOnly="False"></asp:TextBox><br />
                                        <asp:Label ID="SWC05_count" CssClass="wordtb" runat="server" Text="(0/255)" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#E1FFFF" class="wordtt">書件類別 <span class="wordred">*</span></td>
                                    <td class="wordttleft">
                                        <label for="select2"></label>
                                        <asp:DropDownList ID="SWC07" runat="server" CssClass="word">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>水土保持計畫</asp:ListItem>
                                            <asp:ListItem>簡易水保</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="SWC07Label" runat="server" Text="" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" height="100" bgcolor="#E1FFFF" class="wordtt">地籍</td>
                                    <td valign="top" class="wordttleft">
                                        <p>
                                            <asp:TextBox ID="SWC08" runat="server" CssClass="word" Width="100px" BackColor="Silver"></asp:TextBox>
                                            <asp:TextBox ID="SWC09" runat="server" CssClass="word" Width="200px" BackColor="Silver"></asp:TextBox>
                                            <asp:TextBox ID="SWC10" runat="server" CssClass="word" Width="200px" BackColor="Silver"></asp:TextBox>
                                            <asp:TextBox ID="SWC11" runat="server" CssClass="word" Width="200px" BackColor="Silver"></asp:TextBox>
                                        </p>
                                        <asp:Panel ID="Panel15" runat="server">
                                            <asp:Panel ID="LandAdd" runat="server">
                                                <asp:DropDownList ID="LandAddarea" runat="server" Height="22px" Width="68px" OnSelectedIndexChanged="LandAddarea_SelectedIndexChanged" AutoPostBack="True" BackColor="Pink">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value="16">北投</asp:ListItem>
                                                    <asp:ListItem Value="15">士林</asp:ListItem>
                                                    <asp:ListItem Value="14">內湖</asp:ListItem>
                                                    <asp:ListItem Value="10">中山</asp:ListItem>
                                                    <asp:ListItem Value="03">中正</asp:ListItem>
                                                    <asp:ListItem Value="17">信義</asp:ListItem>
                                                    <asp:ListItem Value="02">大安</asp:ListItem>
                                                    <asp:ListItem Value="13">南港</asp:ListItem>
                                                    <asp:ListItem Value="11">文山</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Label ID="Label39" runat="server" Text="區" CssClass="ttextleft"></asp:Label>&nbsp
                                            <asp:DropDownList ID="LandAddsection" runat="server" Height="22px" Width="75px" AutoPostBack="True" OnSelectedIndexChanged="LandAddsection_SelectedIndexChanged" BackColor="Pink">
                                            </asp:DropDownList>
                                                <asp:Label ID="Label40" runat="server" Text="段" CssClass="ttextleft"></asp:Label>&nbsp
                                            <asp:DropDownList ID="LandAddsubsection" runat="server" Height="22px" BackColor="Pink">
                                            </asp:DropDownList>
                                                <asp:Label ID="Label41" runat="server" Text="小段" CssClass="ttextleft"></asp:Label>&nbsp
                                            <asp:TextBox ID="LandAddlandno" runat="server" Width="110px" MaxLength="15" BackColor="Pink"></asp:TextBox>
                                                <asp:Label ID="Label42" runat="server" Text="地號" CssClass="ttextleft"></asp:Label>&nbsp
                                            <br />
                                                <asp:Label ID="Label4n" runat="server" Text="使用分區：" CssClass="ttextleft"></asp:Label>
                                                <asp:DropDownList ID="Landuse" runat="server" Height="22px" Width="68px" BackColor="Pink">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                    <asp:ListItem Value="保護區">保護區</asp:ListItem>
                                                    <asp:ListItem Value="住宅區">住宅區</asp:ListItem>
                                                    <asp:ListItem Value="陽明山國家公園">陽明山國家公園</asp:ListItem>
                                                    <asp:ListItem Value="保變住">保變住</asp:ListItem>
                                                    <asp:ListItem Value="風景區">風景區</asp:ListItem>
                                                    <asp:ListItem Value="商業區">商業區</asp:ListItem>
                                                    <asp:ListItem Value="工業區">工業區</asp:ListItem>
                                                    <asp:ListItem Value="行政區">行政區</asp:ListItem>
                                                    <asp:ListItem Value="文教區">文教區</asp:ListItem>
                                                    <asp:ListItem Value="倉庫區">倉庫區</asp:ListItem>
                                                    <asp:ListItem Value="農業區">農業區</asp:ListItem>
                                                    <asp:ListItem Value="行水區">行水區</asp:ListItem>
                                                    <asp:ListItem Value="保存區">保存區</asp:ListItem>
                                                    <asp:ListItem Value="特定專用區">特定專用區</asp:ListItem>
                                                    <asp:ListItem Value="公共設施用地">公共設施用地</asp:ListItem>
                                                </asp:DropDownList>&nbsp;
                                            <asp:Label ID="Label5n" runat="server" Text="可利用限度：" CssClass="ttextleft"></asp:Label>
                                                <asp:DropDownList ID="Landlimite" runat="server" Height="22px" Width="68px" BackColor="Pink">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value="尚未公告">尚未公告</asp:ListItem>
                                                    <asp:ListItem Value="宜農牧地">宜農牧地</asp:ListItem>
                                                    <asp:ListItem Value="宜林地">宜林地</asp:ListItem>
                                                    <asp:ListItem Value="加強保育地">加強保育地</asp:ListItem>
                                                    <asp:ListItem Value="不屬查定">不屬查定</asp:ListItem>
                                                </asp:DropDownList>&nbsp;
                                            <asp:Label ID="Label6n" runat="server" Text="林地類別：" CssClass="ttextleft"></asp:Label>
                                                <asp:DropDownList ID="Landforest" runat="server" Height="22px" Width="68px" BackColor="Pink">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value="本市林地">本市林地</asp:ListItem>
                                                    <asp:ListItem Value="保安林">保安林</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp;
                                            <asp:Label ID="Label14n" runat="server" Text="地質敏感區：" CssClass="ttextleft"></asp:Label>
                                                <asp:DropDownList ID="Landsensative" runat="server" Height="22px" Width="68px" BackColor="Pink">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value="山崩與地滑">山崩與地滑</asp:ListItem>
                                                    <asp:ListItem Value="地質遺跡">地質遺跡</asp:ListItem>
                                                    <asp:ListItem Value="地下水補注">地下水補注</asp:ListItem>
                                                    <asp:ListItem Value="活動斷層">活動斷層</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp;
                                            <asp:Button ID="landaddok" runat="server" Text="新增" Style="height: 21px" Font-Names="標楷體" />
                                                <asp:Label ID="lbno" runat="server" Text="0" Visible="false"></asp:Label><br />
                                            </asp:Panel>
                                            <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Left" AutoGenerateColumns="False" Font-Names="標楷體"
                                                RowStyle-HorizontalAlign="Center" EmptyDataText="無紀錄" AllowPaging="True" PageSize="5" DataKeyNames="序號" CssClass="wordtbcenter">
                                                <AlternatingRowStyle BackColor="White" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:BoundField DataField="序號" HeaderText="序號">
                                                        <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="區" HeaderText="區">
                                                        <HeaderStyle Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="段" HeaderText="段">
                                                        <HeaderStyle Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="小段" HeaderText="小段">
                                                        <HeaderStyle Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="地號" HeaderText="地號">
                                                        <HeaderStyle Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="土地使用分區" HeaderText="使用分區">
                                                        <HeaderStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="土地可利用限度" HeaderText="可利用限度">
                                                        <HeaderStyle Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="林地類別" HeaderText="林地類別">
                                                        <HeaderStyle Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="地質敏感區" HeaderText="地質敏感區">
                                                        <HeaderStyle Width="90px" />
                                                    </asp:BoundField>
                                                    <asp:CommandField ShowDeleteButton="True" />
                                                </Columns>
                                                <EmptyDataRowStyle ForeColor="Red" />
                                                <PagerSettings LastPageText="最後一頁" FirstPageText="第一頁" Mode="NumericFirstLast"
                                                    NextPageText="下一頁" PreviousPageText="上一頁" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#E1FFFF" class="wordtt">轄區</td>
                                    <td class="wordttleft">
                                        <label for="select3"></label>
                                        <asp:DropDownList ID="SWC12" runat="server" CssClass="word" AutoPostBack="True">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value="北一區">北一區</asp:ListItem>
                                            <asp:ListItem Value="北二區">北二區</asp:ListItem>
                                            <asp:ListItem Value="北三區">北三區</asp:ListItem>
                                            <asp:ListItem Value="北四區">北四區</asp:ListItem>
                                            <asp:ListItem Value="北五區">北五區</asp:ListItem>
                                            <asp:ListItem Value="北六區">北六區</asp:ListItem>
                                            <asp:ListItem Value="北七區">北七區</asp:ListItem>
                                            <asp:ListItem Value="北八區">北八區</asp:ListItem>
                                            <asp:ListItem Value="北九區" Enabled="false">北九區</asp:ListItem>
                                            <asp:ListItem Value="南一區">南一區</asp:ListItem>
                                            <asp:ListItem Value="南二區">南二區</asp:ListItem>
                                            <asp:ListItem Value="南三區">南三區</asp:ListItem>
                                            <asp:ListItem Value="南四區">南四區</asp:ListItem>
                                            <asp:ListItem Value="南五區">南五區</asp:ListItem>
                                            <asp:ListItem Value="南六區">南六區</asp:ListItem>
                                            <asp:ListItem Value="南七區">南七區</asp:ListItem>
                                            <asp:ListItem Value="南八區">南八區</asp:ListItem>
                                            <asp:ListItem Value="南九區" Enabled="false">南九區</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="SWC12Label" runat="server" Text="" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#E1FFFF" class="wordtt">義務人</td>
                                    <td class="wordttleft">
                                        <asp:TextBox ID="SWC13" runat="server" CssClass="word" Width="740px" oninput="textcount('SWC13','SWC13_count','100');" Height="45px" MaxLength="100" TextMode="MultiLine"></asp:TextBox><br />
                                        <asp:Label ID="SWC13_count" CssClass="wordtb" runat="server" Text="(0/100)" ForeColor="Red"></asp:Label><asp:Label ID="SWC13PS" CssClass="wordtb" runat="server" Text="範例：王大明、陳小華（用、分開）"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#E1FFFF" class="wordtt">義務人地址</td>
                                    <td class="wordttleft">
                                        <asp:TextBox ID="SWC14" runat="server" CssClass="word" Width="740px" oninput="textcount('SWC14','SWC14_count','255');" Height="45px" MaxLength="255" TextMode="MultiLine"></asp:TextBox><br />
                                        <asp:Label ID="SWC14_count" CssClass="wordtb" runat="server" Text="(0/255)" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#E1FFFF" class="wordtt">聯絡人</td>
                                    <td class="wordttleft">
                                        <asp:TextBox ID="SWC15" runat="server" CssClass="word" Width="740px"
                                            oninput="textcount('SWC15','SWC15_count','50');" MaxLength="50"></asp:TextBox><br />
                                        <asp:Label ID="SWC15_count" CssClass="wordtb" runat="server" Text="(0/50)" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#E1FFFF" class="wordtt">聯絡人手機</td>
                                    <td class="wordttleft">
                                        <asp:TextBox ID="SWC16" runat="server" CssClass="word" Width="740px"
                                            oninput="chknumber09comm(this);textcount('SWC16','SWC16_count','50');" MaxLength="50"></asp:TextBox><br />
                                        <asp:Label ID="SWC16_count" CssClass="wordtb" runat="server" Text="(0/50)" ForeColor="Red"></asp:Label>
                                        <asp:Label ID="SWC16PS" CssClass="wordtb" runat="server" Text="範例：0928123456&nbsp;&nbsp;分隔請用 &quot;;&quot;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#E1FFFF" class="wordtt">目的事業主管機關</td>
                                    <td class="word">
                                        <label for="select5"></label>
                                        <asp:DropDownList ID="SWC17" runat="server" CssClass="word">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>都市發展局</asp:ListItem>
                                            <asp:ListItem>陽明山國家公園管理處</asp:ListItem>
                                            <asp:ListItem>新建工程處</asp:ListItem>
                                            <asp:ListItem>大地工程處</asp:ListItem>
                                            <asp:ListItem>殯葬管理處</asp:ListItem>
                                            <asp:ListItem>經濟部</asp:ListItem>
                                            <asp:ListItem>產業發展局</asp:ListItem>
                                            <asp:ListItem>公園路燈工程管理處</asp:ListItem>
                                            <asp:ListItem>臺北自來水事業處</asp:ListItem>
                                            <asp:ListItem>環境保護局</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="SWC17Label" runat="server" Text="" Visible="false"></asp:Label>
                                        &nbsp;&nbsp;其他：<asp:TextBox ID="SWC18" runat="server" CssClass="word" Width="340px"
                                            oninput="textcount('SWC18','SWC18_count','50');" MaxLength="50"></asp:TextBox>
                                        <asp:Label ID="SWC18_count" CssClass="wordtb" runat="server" Text="(0/50)" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#E1FFFF" class="wordtt">計畫面積(公頃)</td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC23" runat="server" CssClass="wordright" Width="200px" onkeyup="chknumber(this);"></asp:TextBox>&nbsp;&nbsp;公頃
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#E1FFFF" class="wordtt">承辦技師</td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC21" runat="server" CssClass="word" Width="340px"
                                            MaxLength="50" oninput="textcount('SWC21','SWC21_count','50');"></asp:TextBox>
                                        <asp:Label ID="SWC21_count" runat="server" CssClass="wordtb" ForeColor="Red"
                                            Text="(0/50)"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#E1FFFF" class="wordtt">承辦技師Email</td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC85" runat="server" CssClass="word" Width="340px"
                                            MaxLength="50" oninput="textcount('SWC85','SWC85_count','75');"></asp:TextBox>
                                        <asp:Label ID="SWC85_count" runat="server" CssClass="wordtb" ForeColor="Red"
                                            Text="(0/75)"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td bgcolor="#E1FFFF" class="wordtt">承辦人員</td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC25" runat="server" CssClass="word" Width="200px" BackColor="Silver"></asp:TextBox>&nbsp;<-&nbsp;
                                    <asp:DropDownList ID="geouser" runat="server" CssClass="word" Width="200px"
                                        DataSourceID="SqlDataSourceuser" DataTextField="姓名" AutoPostBack="True" BackColor="Pink">
                                    </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSourceuser" runat="server" ConnectionString="<%$ ConnectionStrings:tslmConnectionString2 %>"
                                            SelectCommand="SELECT [姓名] FROM [user] where [狀態]='正常' and 單位='大地工程處' and 職稱<>'技工' and [科室]='審查管理科'"></asp:SqlDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#E1FFFF" class="wordtt">座標</td>
                                    <td class="word">X:
                                    <asp:TextBox ID="SWC27" runat="server" CssClass="word" Width="150px"
                                        onkeyup="chknumber(this);" MaxLength="6"></asp:TextBox>
                                        &nbsp;&nbsp;Y:
                                    <asp:TextBox ID="SWC28" runat="server" CssClass="word" Width="150px"
                                        onkeyup="chknumber(this);" MaxLength="7"></asp:TextBox>
                                        <asp:Label ID="SWC28PS" CssClass="wordtb" runat="server" Text="&nbsp;&nbsp;範例：X：300580 Y：2778810（請輸入67座標系）" ForeColor="Red"></asp:Label>

                                    </td>
                                </tr>


                            </table>
                            <table width="960" border="1" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td bgcolor="#CCCCFF" colspan="3" class="title">受理
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#ECECFF" class="wordtt">補正期限
                                    </td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC32" runat="server" CssClass="word" Width="200px" Format="yyyy-MM-dd"></asp:TextBox>
                                        <asp:CalendarExtender ID="SWC32_CalendarExtender" runat="server" Enabled="True" TargetControlID="SWC32"
                                            Format="yyyy-MM-dd">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td width="250" rowspan="4" valign="top">
                                        <asp:Label ID="Label15n" runat="server" Text="受理附件清單" Font-Names="標楷體" Font-Size="24px"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Panel ID="receivefileuploadpanel" runat="server" Width="250px">
                                            <asp:FileUpload ID="receivefileupload" runat="server" Font-Names="標楷體" Font-Size="14px" />
                                            <asp:Button ID="receivefileuploadok" runat="server" Text="開始上傳附件" Font-Names="標楷體"
                                                Font-Size="Large" />
                                        </asp:Panel>
                                        <asp:Panel ID="receivefilepanel" runat="server" Width="250px">
                                            <asp:GridView ID="receivefileGridView" runat="server" HorizontalAlign="Left" AutoGenerateColumns="False"
                                                Font-Names="標楷體" RowStyle-HorizontalAlign="Center" EmptyDataText="無紀錄" DataKeyNames="序號"
                                                CssClass="wordtb">
                                                <AlternatingRowStyle BackColor="White" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:BoundField DataField="序號" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                                        <HeaderStyle Width="15px" />
                                                        <ItemStyle CssClass="hiddencol" />
                                                    </asp:BoundField>
                                                    <asp:HyperLinkField HeaderText="附件" DataNavigateUrlFields="附件URL" DataTextField="附件檔名"
                                                        Target="_blank" ItemStyle-CssClass="tdleft">
                                                        <HeaderStyle Width="210px" />
                                                        <ItemStyle CssClass="tdleft" />
                                                    </asp:HyperLinkField>
                                                    <asp:CommandField ShowDeleteButton="True" DeleteText="X">
                                                        <HeaderStyle Width="15px" />
                                                    </asp:CommandField>
                                                </Columns>
                                                <EmptyDataRowStyle ForeColor="Red" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#ECECFF" class="wordtt">審查費金額
                                    </td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC35" runat="server" CssClass="wordright" Width="200px" onkeyup="chknumber09(this);"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#ECECFF" class="wordtt">審查費繳納期限
                                    </td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC31" runat="server" CssClass="word" Width="200px" Format="yyyy-MM-dd"></asp:TextBox>
                                        <asp:CalendarExtender ID="SWC31_CalendarExtender" runat="server" Enabled="True" TargetControlID="SWC31"
                                            Format="yyyy-MM-dd">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#ECECFF" class="wordtt">審查費繳納日期
                                    </td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC33" runat="server" CssClass="word" Width="200px" Format="yyyy-MM-dd"></asp:TextBox>
                                        <asp:CalendarExtender ID="SWC33_CalendarExtender" runat="server" Enabled="True" TargetControlID="SWC33"
                                            Format="yyyy-MM-dd">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                            <table width="960" border="1" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td bgcolor="#BAFF75" colspan="3" class="title">審查</td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#DFFFBF" class="wordtt">受理日期</td>
                                    <td width="500" class="word">
                                        <asp:TextBox ID="SWC34" runat="server" CssClass="word" Width="200px" Format="yyyy-MM-dd"></asp:TextBox>
                                        <asp:CalendarExtender ID="SWC34_CalendarExtender" runat="server" Enabled="True" TargetControlID="SWC34"
                                            Format="yyyy-MM-dd">
                                        </asp:CalendarExtender>
                                        <br />
                                        <asp:Label ID="SWC34PS" CssClass="wordtb" runat="server" Text="※ 外審為委外日期，內審為第一次審查會紀錄發文日期" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td width="250" rowspan="13" valign="top">
                                        <asp:Label ID="Labelcheckfile" runat="server" Text="審查附件清單" Font-Names="標楷體" Font-Size="24px"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Panel ID="CheckfileuploadPanel" runat="server" Width="250px">

                                            <asp:FileUpload ID="checkfileupload" runat="server" Font-Names="標楷體" Font-Size="14px" />
                                            <asp:Button ID="checkfileuploadok" runat="server" Text="開始上傳附件" Font-Names="標楷體" Font-Size="Large" />
                                        </asp:Panel>
                                        <asp:Panel ID="CheckfilePanel" runat="server" Width="250px">

                                            <asp:GridView ID="CheckfileGridView" runat="server" HorizontalAlign="Left" AutoGenerateColumns="False" Font-Names="標楷體"
                                                RowStyle-HorizontalAlign="Center" EmptyDataText="無紀錄" DataKeyNames="序號" CssClass="wordtb">
                                                <AlternatingRowStyle BackColor="White" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:BoundField DataField="序號" ItemStyle-CssClass="hiddencol"
                                                        HeaderStyle-CssClass="hiddencol">
                                                        <HeaderStyle Width="15px" />
                                                        <ItemStyle CssClass="hiddencol" />
                                                    </asp:BoundField>
                                                    <asp:HyperLinkField HeaderText="附件" DataNavigateUrlFields="附件URL"
                                                        DataTextField="附件檔名" Target="_blank" ItemStyle-CssClass="tdleft">
                                                        <HeaderStyle Width="210px" />
                                                        <ItemStyle CssClass="tdleft" />
                                                    </asp:HyperLinkField>
                                                    <asp:CommandField ShowDeleteButton="True" DeleteText="X">
                                                        <HeaderStyle Width="15px" />
                                                    </asp:CommandField>
                                                </Columns>
                                                <EmptyDataRowStyle ForeColor="Red" />
                                            </asp:GridView>
                                        </asp:Panel>

                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#DFFFBF" class="wordtt">審查單位</td>
                                    <td class="word">
                                        <asp:DropDownList ID="SWC22" runat="server" CssClass="word">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>社團法人桃園市土木技師公會</asp:ListItem>
                                            <asp:ListItem>社團法人新北市土木技師公會</asp:ListItem>
                                            <asp:ListItem>社團法人台灣省土木技師公會</asp:ListItem>
                                            <asp:ListItem>社團法人台北市土木技師公會</asp:ListItem>
                                            <asp:ListItem>社團法人台北市水利技師公會</asp:ListItem>
                                            <asp:ListItem>社團法人中華民國大地工程技師公會</asp:ListItem>
                                            <asp:ListItem>社團法人台灣省水利技師公會</asp:ListItem>
                                            <asp:ListItem>社團法人臺北市水土保持技師公會</asp:ListItem>
                                            <asp:ListItem>社團法人台灣省水土保持技師公會</asp:ListItem>
                                            <asp:ListItem>社團法人新北市水土保持技師公會</asp:ListItem>
                                            <asp:ListItem>----非現行廠商----</asp:ListItem>
                                            <asp:ListItem>中華水土保持學會</asp:ListItem>
                                            <asp:ListItem>國立中興大學</asp:ListItem>
                                            <asp:ListItem>國立屏東科技大學</asp:ListItem>
                                            <asp:ListItem>臺北市水土保持技師公會</asp:ListItem>
                                            <asp:ListItem>台北市土木技師公會</asp:ListItem>
                                            <asp:ListItem>台北市水利技師公會</asp:ListItem>
                                            <asp:ListItem>臺灣省水土保持技師公會</asp:ListItem>
                                            <asp:ListItem>台灣省土木技師公會</asp:ListItem>
                                            <asp:ListItem>臺灣省水利技師公會</asp:ListItem>
                                            <asp:ListItem>新北市土木技師公會</asp:ListItem>
                                            <asp:ListItem>新北市水土保持技師公會</asp:ListItem>
                                            <asp:ListItem>中華民國大地工程技師公會</asp:ListItem>
                                            <asp:ListItem>臺北市政府工務局大地工程處</asp:ListItem>

                                        </asp:DropDownList>
                                        <asp:Label ID="SWC22Label" runat="server" Text="" Visible="false"></asp:Label>
                                        <br />
                                        其他：<asp:TextBox ID="SWC86" runat="server" CssClass="word" Width="340px"
                                            oninput="textcount('SWC86','SWC86_count','100');" MaxLength="100"></asp:TextBox>
                                        <asp:Label ID="SWC86_count" CssClass="wordtb" runat="server" Text="(0/100)" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#DFFFBF" class="wordtt">審查委員</td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC87" runat="server" CssClass="word" Width="467px"
                                            oninput="textcount('SWC87','SWC87_count','255');" Height="45px" MaxLength="255" TextMode="MultiLine"></asp:TextBox>
                                        <br />
                                        <asp:Label ID="SWC87_count" CssClass="wordtb" runat="server" Text="(0/255)" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#DFFFBF" class="wordtt">審查紀錄</td>
                                    <td class="word">

                                        <asp:Panel ID="Panelcheckrecord" runat="server">
                                            <table style="margin: 0px; padding: 0px; border: 1px solid #000000; width: 100%; text-align: center;">
                                                <tr>
                                                    <td style="padding: 0px; margin: 0px; border: 1px solid #000000; width: 9%;">
                                                        <!--
                                                審查次數
                                                -->
                                                    </td>
                                                    <td style="border: 1px solid #000000; width: 44%;">審查日期
                                                    </td>
                                                    <td style="border: 1px solid #000000; width: 44%;">補正期限
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border: 1px solid #000000; width: 9%;">

                                                        <asp:TextBox ID="SWC_r_no" runat="server" CssClass="word" Width="25px" onkeyup="chknumber09(this);" Visible="false"></asp:TextBox>

                                                    </td>
                                                    <td style="border: 1px solid #000000; width: 44%;">
                                                        <asp:TextBox ID="SWC_r_c" runat="server" CssClass="word" Width="98%" Format="yyyy-MM-dd" BackColor="Pink"></asp:TextBox>
                                                        <asp:CalendarExtender ID="SWC_r_c_CalendarExtender" runat="server" Enabled="True" TargetControlID="SWC_r_c" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                                    </td>
                                                    <td style="border: 1px solid #000000; width: 44%;">
                                                        <asp:TextBox ID="SWC_r_f" runat="server" CssClass="word" Width="98%" Format="yyyy-MM-dd" BackColor="Pink"></asp:TextBox>
                                                        <asp:CalendarExtender ID="SWC_r_f_CalendarExtender" runat="server" Enabled="True" TargetControlID="SWC_r_f" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                                    </td>
                                                </tr>
                                            </table>

                                            <asp:Label ID="Label7a" runat="server" Text="意見函檔:"></asp:Label>

                                            <asp:TextBox ID="SWC_r_file" runat="server" CssClass="word" Width="20px" Visible="false"></asp:TextBox>
                                            <asp:FileUpload ID="SWC_r_file_fileupload" runat="server" CssClass="wordtt" />
                                            <asp:Button ID="SWC_r_file_fileuploadok" runat="server" Text="上傳檔案"
                                                CssClass="wordttb" OnClientClick="divBlock.style.display='';" Height="26px" />
                                            <asp:Label ID="swcchreportlbno" runat="server" Text="0" Visible="false"></asp:Label>
                                            <br />
                                            <asp:HyperLink ID="SWC_r_file_hyperlink" runat="server" CssClass="word"></asp:HyperLink>
                                            <asp:Button ID="SWC_r_file_fileclean" runat="server" CssClass="wordttb" Text="X" /><br />
                                            <asp:Label ID="Label43" runat="server" Text="※ 上傳格式限定為PDF、JPG，檔案大小請於50mb以內" CssClass="wordred"></asp:Label><br />
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Button ID="SWC_r_updata" runat="server" Text="加入資料" /><asp:Button ID="SWC_r_cancel" runat="server" Text="取消編修" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Label ID="Label44" runat="server" Text="※ 上傳文件內容請注意個資" CssClass="wordred"></asp:Label>
                                        </asp:Panel>
                                        <br />

                                        <asp:GridView ID="swccheckGridView" runat="server" HorizontalAlign="Left"
                                            AutoGenerateColumns="False" Font-Names="標楷體" RowStyle-HorizontalAlign="Center"
                                            EmptyDataText="無紀錄" DataKeyNames="序號" CssClass="wordtb"
                                            EnableModelValidation="True">
                                            <AlternatingRowStyle BackColor="White" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <Columns>
                                                <asp:BoundField DataField="序號" HeaderText="序號" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                                    <HeaderStyle Width="20px" BackColor="#99CCFF" />
                                                    <ItemStyle CssClass="hiddencol" Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="審查次數" HeaderText="審查次數" Visible="False">
                                                    <HeaderStyle Width="30px" BackColor="#99CCFF" />
                                                    <ItemStyle Width="30px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="審查日期" HeaderText="審查日期">
                                                    <HeaderStyle Width="70px" BackColor="#99CCFF" />
                                                    <ItemStyle Width="70px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="補正期限" HeaderText="補正期限">
                                                    <HeaderStyle Width="70px" BackColor="#99CCFF" />
                                                </asp:BoundField>
                                                <asp:HyperLinkField DataNavigateUrlFields="備註" DataTextField="意見函檔"
                                                    HeaderText="意見函檔" Target="_blank">
                                                    <HeaderStyle BackColor="#99CCFF" Width="260px" />
                                                    <ItemStyle Width="260px" HorizontalAlign="Left" />
                                                </asp:HyperLinkField>
                                                <asp:CommandField ShowDeleteButton="True" DeleteText="X">
                                                    <HeaderStyle Width="15px" BackColor="#99CCFF" />
                                                    <ItemStyle Width="15px" />
                                                </asp:CommandField>
                                                <asp:CommandField ShowEditButton="True" Visible="false">
                                                    <HeaderStyle Width="30px" BackColor="#99CCFF" />
                                                    <ItemStyle Width="30px" />
                                                </asp:CommandField>
                                            </Columns>
                                            <EmptyDataRowStyle ForeColor="Red" />
                                        </asp:GridView>


                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#DFFFBF" class="wordtt">審查期限</td>
                                    <td width="500" class="word">
                                        <asp:TextBox ID="SWC88" runat="server" CssClass="word" Width="200px" Format="yyyy-MM-dd"></asp:TextBox>
                                        <asp:CalendarExtender ID="SWC88_CalendarExtender" runat="server" Enabled="True" TargetControlID="SWC88" Format="yyyy-MM-dd">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#DFFFBF" class="wordtt">暫停審查期限</td>
                                    <td width="500" class="word">
                                        <asp:TextBox ID="SWC89" runat="server" CssClass="word" Width="200px" Format="yyyy-MM-dd"></asp:TextBox>
                                        <asp:CalendarExtender ID="SWC89_CalendarExtender" runat="server" Enabled="True" TargetControlID="SWC89" Format="yyyy-MM-dd">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#DFFFBF" class="wordtt">核定日期</td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC38" runat="server" CssClass="word" Width="200px" Format="yyyy-MM-dd"></asp:TextBox>
                                        <asp:CalendarExtender ID="SWC38_CalendarExtender" runat="server" Enabled="True" TargetControlID="SWC38" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#DFFFBF" class="wordtt">核定文號</td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC39" runat="server" CssClass="word" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#DFFFBF" class="wordtt">核定本
                                    </td>
                                    <td class="word">
                                        <asp:Panel ID="Panelscane" runat="server">
                                            <asp:FileUpload ID="SWC80_fileupload" runat="server" CssClass="wordtt" />
                                            <asp:Button ID="SWC80_fileuploadok" runat="server" Text="上傳檔案" CssClass="wordttb" OnClientClick="divBlock.style.display='';" />
                                            <br />
                                            <span class="wordred">※ 上傳格式限定為PDF、JPG，檔案大小請於50mb以內</span><br />
                                        </asp:Panel>
                                        <asp:TextBox ID="SWC80" runat="server" CssClass="word" Width="20px" Visible="False"></asp:TextBox>
                                        <asp:HyperLink ID="SWC80_hyperlink" runat="server" CssClass="word" 
                                            Target="_blank">[SWC80_hyperlink]</asp:HyperLink>
                                        <asp:Button ID="SWC80_fileclean" runat="server" CssClass="wordttb" Text="X" />
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#DFFFBF" class="wordtt">水土保持設施配置圖<br />
                                        (圖6-1)</td>
                                    <td class="word">
                                        <asp:Panel ID="Panel61" runat="server">
                                            <asp:FileUpload ID="SWC29_fileupload" runat="server" CssClass="wordtt" />
                                            <asp:Button ID="SWC29_fileuploadok" runat="server" Text="上傳檔案"
                                                CssClass="wordttb" OnClientClick="divBlock.style.display='';" Height="26px" />
                                            <br />
                                            <span class="wordred">※ 上傳格式限定為PDF、JPG，檔案大小請於50mb以內</span><br />
                                        </asp:Panel>
                                        <asp:TextBox ID="SWC29" runat="server" CssClass="word" Width="20px" Visible="False"></asp:TextBox>
                                        <asp:HyperLink ID="SWC29_hyperlink" runat="server" CssClass="word"
                                            Target="_blank">[SWC29_hyperlink]</asp:HyperLink>
                                        <asp:Button ID="SWC29_fileclean" runat="server" CssClass="wordttb" Text="X" />
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#DFFFBF" class="wordtt">臨時性防災設施配置圖<br />
                                        (圖7-1)</td>
                                    <td align="left" class="word">
                                        <asp:Panel ID="Panel71" runat="server">
                                            <asp:FileUpload ID="SWC30_fileupload" runat="server" CssClass="wordtt" />
                                            <asp:Button ID="SWC30_fileuploadok" runat="server" Text="上傳檔案"
                                                CssClass="wordttb" OnClientClick="divBlock.style.display='';" Height="26px" />
                                            <br />
                                            <span class="wordred">※ 上傳格式限定為PDF、JPG，檔案大小請於50mb以內</span><br />
                                        </asp:Panel>
                                        <asp:TextBox ID="SWC30" runat="server" CssClass="word" Width="20px" Visible="False"></asp:TextBox>
                                        <asp:HyperLink ID="SWC30_hyperlink" runat="server" CssClass="word"
                                            Target="_blank">[SWC30_hyperlink]</asp:HyperLink>
                                        <asp:Button ID="SWC30_fileclean" runat="server" CssClass="wordttb" Text="X" />
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#DFFFBF" class="style5">保證金金額</td>
                                    <td class="style6">
                                        <asp:TextBox ID="SWC40" runat="server" CssClass="wordright" Width="200px" onkeyup="chknumber09(this);"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td width="200" bgcolor="#DFFFBF" class="wordtt">審查費核銷
                                    </td>
                                    <td class="word">
                                        <asp:RadioButton ID="SWC3601" runat="server" GroupName="SWC36R" CssClass="word" Text="已核銷" />&nbsp;&nbsp;
                                    <asp:RadioButton ID="SWC3602" runat="server" GroupName="SWC36R" CssClass="word" Text="無審查費" />&nbsp;&nbsp;
                                    <asp:Button ID="SWC36" runat="server" CssClass="wordttb" Text="X" OnClientClick="radcancel('SWC3601');radcancel('SWC3602');return false;" />
                                    </td>
                                </tr>
                                </table>
                            <table width="960" border="1" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="3" bgcolor="#D2BAD8" class="title">施工</td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#F2E7F1" class="wordtt">開工期限
                                    </td>
                                    <td width="500" class="word">
                                        <asp:TextBox ID="SWC82" runat="server" CssClass="word" Width="200px"
                                            Format="yyyy-MM-dd" ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox><br />
                                        <asp:CalendarExtender ID="SWC82_CalendarExtender" runat="server" Enabled="True" TargetControlID="SWC82"
                                            Format="yyyy-MM-dd">
                                        </asp:CalendarExtender>
                                        <asp:Label ID="SWC82PS" class="wordred" runat="server" Text="※依審核監督辦法§22、§31-1及§35-1，簡水核定+1年 水計核定+3年(103/12/25前核定的皆為107/12/25)，逾期書件失效"></asp:Label>
                                    </td>
                                    <td width="250" rowspan="24" valign="top">
                                        <asp:Label ID="Labeldofile" runat="server" Text="施工中附件清單" Font-Names="標楷體" Font-Size="24px"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Panel ID="douploadpanel" runat="server" Width="250px">
                                            <asp:FileUpload ID="dofileupload" runat="server" Font-Names="標楷體" Font-Size="14px" />
                                            <asp:Button ID="dofileuploadok" runat="server" Text="開始上傳附件" Font-Names="標楷體" Font-Size="Large" />
                                        </asp:Panel>
                                        <asp:Panel ID="doPanel" runat="server" Width="250px">

                                            <asp:GridView ID="doGridView" runat="server" HorizontalAlign="Left" AutoGenerateColumns="False" Font-Names="標楷體"
                                                RowStyle-HorizontalAlign="Center" EmptyDataText="無紀錄" DataKeyNames="序號" CssClass="wordtb">
                                                <AlternatingRowStyle BackColor="White" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:BoundField DataField="序號" ItemStyle-CssClass="hiddencol"
                                                        HeaderStyle-CssClass="hiddencol">
                                                        <HeaderStyle Width="15px" />
                                                        <ItemStyle CssClass="hiddencol" />
                                                    </asp:BoundField>
                                                    <asp:HyperLinkField HeaderText="附件" DataNavigateUrlFields="附件URL"
                                                        DataTextField="附件檔名" Target="_blank" ItemStyle-CssClass="tdleft">
                                                        <HeaderStyle Width="210px" />
                                                        <ItemStyle CssClass="tdleft" />
                                                    </asp:HyperLinkField>
                                                    <asp:CommandField ShowDeleteButton="True" DeleteText="X">
                                                        <HeaderStyle Width="15px" />
                                                    </asp:CommandField>
                                                </Columns>
                                                <EmptyDataRowStyle ForeColor="Red" />
                                            </asp:GridView>
                                        </asp:Panel>

                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#F2E7F1" class="wordtt">開工展延次數
                                    </td>
                                    <td class="style6">
                                        <asp:DropDownList ID="SWC83" runat="server" CssClass="word">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="SWC83Label" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="SWC83PS" class="wordred" runat="server" Text="※依審核監督辦法§22，以2次為限，每次不得超過6個月"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#F2E7F1" class="wordtt">保證金繳納</td>
                                    <td width="500" class="word">
                                        <asp:RadioButton ID="SWC4101" runat="server" GroupName="SWC41R" CssClass="word" Text="已繳交" />&nbsp;&nbsp;
                                    <asp:RadioButton ID="SWC4102" runat="server" GroupName="SWC41R" CssClass="word" Text="無保證金" />&nbsp;&nbsp;
                                    <asp:Button ID="SWC41" runat="server" CssClass="wordttb" Text="X" OnClientClick="radcancel('SWC4101');radcancel('SWC4102');return false;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2E7F1" class="wordtt">施工許可證核發日期
                                    </td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC43" runat="server" CssClass="word" Width="200px" Format="yyyy-MM-dd"></asp:TextBox>
                                        <asp:CalendarExtender ID="SWC43_CalendarExtender" runat="server" Enabled="True" TargetControlID="SWC43"
                                            Format="yyyy-MM-dd">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2E7F1" class="wordtt">施工許可證核發文號</td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC44" runat="server" CssClass="word" Width="200px"
                                            MaxLength="20"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2E7F1" class="wordtt">開工日期</td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC51" runat="server" CssClass="word" Width="200px" Format="yyyy-MM-dd"></asp:TextBox>
                                        <asp:CalendarExtender ID="SWC51_CalendarExtender" runat="server" Enabled="True" TargetControlID="SWC51" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2E7F1" class="wordtt">預定完工日期</td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC52" runat="server" CssClass="word" Width="200px" Format="yyyy-MM-dd"></asp:TextBox>
                                        <asp:CalendarExtender ID="SWC52_CalendarExtender" runat="server" Enabled="True" TargetControlID="SWC52" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                        <br />
                                        <asp:Label ID="SWC52PS" class="wordred" runat="server" Text="※依審核監督辦法§31-1及§34，逾期書件及施工許可證皆失效"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2E7F1" class="wordtt">監造技師</td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC45" runat="server" CssClass="word" Width="340px"
                                            MaxLength="50" oninput="textcount('SWC45','SWC45_count','50');"></asp:TextBox>
                                        <asp:Label ID="SWC45_count" runat="server" CssClass="wordtb" ForeColor="Red"
                                            Text="(0/50)"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2E7F1" class="wordtt">監造技師Email</td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC90" runat="server" CssClass="word" Width="340px"
                                            MaxLength="75" oninput="textcount('SWC90','SWC90_count','75');"></asp:TextBox>
                                        <asp:Label ID="SWC90_count" runat="server" CssClass="wordtb" ForeColor="Red"
                                            Text="(0/75)"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2E7F1" class="wordtt">監造技師地址</td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC46" runat="server" CssClass="word" Width="475px"
                                            MaxLength="255" oninput="textcount('SWC46','SWC46_count','255');"
                                            Height="64px" TextMode="MultiLine"></asp:TextBox><br />
                                        <asp:Label ID="SWC46_count" runat="server" CssClass="wordtb" ForeColor="Red"
                                            Text="(0/255)"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2E7F1" class="wordtt">監造技師手機</td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC47" runat="server" CssClass="word" Width="475px"
                                            oninput="chknumber09comm(this);textcount('SWC47','SWC47_count','50');" MaxLength="50"></asp:TextBox><br />
                                        <asp:Label ID="SWC47_count" CssClass="wordtb" runat="server" Text="(0/50)" ForeColor="Red"></asp:Label>
                                        <asp:Label ID="SWC47PS" CssClass="wordtb" runat="server" Text="範例：0928123456&nbsp;&nbsp;分隔請用 &quot;;&quot;" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2E7F1" class="wordtt">施工廠商</td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC48" runat="server" CssClass="word" Width="475px"
                                            MaxLength="255" oninput="textcount('SWC48','SWC48_count','255');"
                                            Height="45px" TextMode="MultiLine"></asp:TextBox><br />
                                        <asp:Label ID="SWC48_count" runat="server" CssClass="wordtb" ForeColor="Red"
                                            Text="(0/255)"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2E7F1" class="wordtt">工地負責人員</td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC49" runat="server" CssClass="word" Width="340px"
                                            MaxLength="50" oninput="textcount('SWC49','SWC49_count','50');"></asp:TextBox>
                                        <asp:Label ID="SWC49_count" runat="server" CssClass="wordtb" ForeColor="Red"
                                            Text="(0/50)"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2E7F1" class="wordtt">工地負責人員手機</td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC50" runat="server" CssClass="word" Width="475px"
                                            MaxLength="50"
                                            oninput="chknumber09comm(this);textcount('SWC50','SWC50_count','50');"></asp:TextBox>
                                        <br />
                                        <asp:Label ID="SWC50_count" runat="server" CssClass="wordtb" ForeColor="Red"
                                            Text="(0/50)"></asp:Label>
                                        <asp:Label ID="SWC50PS" CssClass="wordtb" runat="server" Text="範例：0928123456&nbsp;&nbsp;分隔請用 &quot;;&quot;" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2E7F1" class="wordtt">檢查單位</td>
                                    <td class="word">
                                        <asp:DropDownList ID="SWC24" runat="server" CssClass="word">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>社團法人台灣省水土保持技師公會</asp:ListItem>
                                            <asp:ListItem>社團法人臺北市水土保持技師公會</asp:ListItem>
                                            <asp:ListItem>社團法人中華民國大地工程技師公會</asp:ListItem>
                                            <asp:ListItem>社團法人台北市土木技師公會</asp:ListItem>
                                            <asp:ListItem>----非現行廠商----</asp:ListItem>
                                            <asp:ListItem>臺北市水土保持技師公會</asp:ListItem>
                                            <asp:ListItem>台北市土木技師公會</asp:ListItem>
                                            <asp:ListItem>台北市水利技師公會</asp:ListItem>
                                            <asp:ListItem>中華民國大地工程技師公會</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="SWC24Label" runat="server" Text="" Visible="false"></asp:Label>
                                        <br />
                                        其他：<asp:TextBox ID="SWC99" runat="server" CssClass="word" Width="340px"
                                            oninput="textcount('SWC99','SWC99_count','255');" MaxLength="255"></asp:TextBox>
                                        <asp:Label ID="SWC99_count" CssClass="wordtb" runat="server" Text="(0/255)" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2E7F1" class="wordtt">施工中監督檢查紀錄
                                    </td>
                                    <td class="word">
                                        <asp:Label ID="LabelSWC54" runat="server" Width="475px"></asp:Label><br />
                                        <asp:TextBox ID="SWC54" runat="server" CssClass="word" Width="475px" MaxLength="255"
                                            oninput="textcount('SWC54','SWC54_count','255');" Height="45px" TextMode="MultiLine"
                                            Visible="False" ReadOnly="True"></asp:TextBox><br />
                                        <asp:Label ID="SWC54_count" runat="server" CssClass="wordtb" ForeColor="Red" Text="(0/255)"
                                            Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2E7F1" class="wordtt">工程進度</td>
                                    <td class="word">
                                        <asp:DropDownList ID="SWC91" runat="server" CssClass="word">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>尚未動工</asp:ListItem>
                                            <asp:ListItem>施作臨時設施</asp:ListItem>
                                            <asp:ListItem>施作臨時＆永久設施</asp:ListItem>
                                            <asp:ListItem>施作永久設施</asp:ListItem>
                                            <asp:ListItem>施作完成</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="SWC91Label" runat="server" Text="" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2E7F1" class="wordtt">停工日期</td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC53" runat="server" CssClass="word" Width="200px" Format="yyyy-MM-dd"></asp:TextBox>
                                        <asp:CalendarExtender ID="SWC53_CalendarExtender" runat="server" Enabled="True" TargetControlID="SWC53" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2E7F1" class="style5">停工期限
                                    </td>
                                    <td class="style6">
                                        <asp:TextBox ID="SWC84" runat="server" CssClass="word" Width="200px" Format="yyyy-MM-dd"
                                            ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox><br />
                                        <asp:CalendarExtender ID="SWC84_CalendarExtender" runat="server" Enabled="True" TargetControlID="SWC84"
                                            Format="yyyy-MM-dd">
                                        </asp:CalendarExtender>
                                        <asp:Label ID="SWC84PS" class="wordred" runat="server" Text="※依審核監督辦法§22-1及§31-1，逾期書件失效"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#F2E7F1" class="wordtt">停工展延次數
                                    </td>
                                    <td class="style6">
                                        <asp:DropDownList ID="SWC92" runat="server" CssClass="word">
                                            <asp:ListItem>0</asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="SWC92Label" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="SWC92PS" class="wordred" runat="server" Text="※依審核監督辦法§22-1，以2次為限，每次不得超過6個月"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#F2E7F1" class="wordtt">完工日期</td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC58" runat="server" CssClass="word" Width="200px" Format="yyyy-MM-dd"></asp:TextBox>
                                        <asp:CalendarExtender ID="SWC58_CalendarExtender" runat="server" Enabled="True" TargetControlID="SWC58" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                        <asp:Label ID="SWC58PS" class="wordred" runat="server" Text="※完工申報書填的完工日期"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#F2E7F1" class="wordtt">完工證明書核發日期</td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC59" runat="server" CssClass="word" Width="200px" Format="yyyy-MM-dd"></asp:TextBox>
                                        <asp:CalendarExtender ID="SWC59_CalendarExtender" runat="server" Enabled="True" TargetControlID="SWC59" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#F2E7F1" class="wordtt">水保設施</td>
                                    <td>
                                        <table width="500" border="0" cellpadding="0" cellspacing="0" class="word">
                                            <tr>
                                                <td colspan="2" class="word">
                                                    <asp:CheckBox ID="SWC67" runat="server" Text="滯洪沉砂設施" CssClass="word" onclick="textboxmode('SWC67','SWC68');textboxmode('SWC67','SWC69');textboxmode('SWC67','SWC70');" />
                                                    <asp:TextBox ID="SWC68" runat="server" CssClass="wordright" Width="50px" oninput="chknumber09(this);"></asp:TextBox>
                                                    座，滯洪量
                                                <asp:TextBox ID="SWC69" runat="server" CssClass="wordright" Width="50px" oninput="chknumber09(this);"></asp:TextBox>
                                                    m³，沉砂量
                                                <asp:TextBox ID="SWC70" runat="server" CssClass="wordright" Width="50px" oninput="chknumber09(this);"></asp:TextBox>
                                                    m³
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="250" class="word">
                                                    <asp:CheckBox ID="SWC71" runat="server" Text="排水設施" CssClass="word" onclick="textboxmode('SWC71','SWC72');" />
                                                    <asp:TextBox ID="SWC72" runat="server" CssClass="wordright" Width="50px" oninput="chknumber09(this);"></asp:TextBox>
                                                    條
                                                </td>
                                                <td width="250" class="word">
                                                    <asp:CheckBox ID="SWC73" runat="server" Text="擋土設施" CssClass="word" onclick="textboxmode('SWC73','SWC74');" />
                                                    <asp:TextBox ID="SWC74" runat="server" CssClass="wordright" Width="50px" oninput="chknumber09(this);"></asp:TextBox>
                                                    道
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="word">
                                                    <asp:CheckBox ID="SWC75" runat="server" Text="植生工程" CssClass="word" />
                                                </td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#F2E7F1" class="wordtt">保證金退還
                                    </td>
                                    <td class="word">
                                        <asp:RadioButton ID="SWC5601" runat="server" GroupName="SWC56R" CssClass="word" Text="已退還" />&nbsp;&nbsp;
                                    <asp:RadioButton ID="SWC5602" runat="server" GroupName="SWC56R" CssClass="word" Text="無保證金" />&nbsp;&nbsp;
                                    <asp:Button ID="SWC56" runat="server" CssClass="wordttb" Text="X" OnClientClick="radcancel('SWC5601');radcancel('SWC5602');return false;" />
                                    </td>
                                </tr>
                            </table>
                            <table width="960" border="1" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="3" bgcolor="#F1AA69" class="title">已完工</td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#FCE2CA" class="wordtt">維護管理人</td>
                                    <td width="500" class="word">
                                        <asp:TextBox ID="SWC93" runat="server" CssClass="word" Width="340px"
                                            MaxLength="255" oninput="textcount('SWC93','SWC93_count','255');"></asp:TextBox>
                                        <asp:Label ID="SWC93_count" runat="server" CssClass="wordtb" ForeColor="Red"
                                            Text="(0/255)"></asp:Label>
                                    </td>
                                    <td width="250" rowspan="5" valign="top">
                                        <asp:Label ID="Labelfinish" runat="server" Text="已完工附件清單" Font-Names="標楷體" Font-Size="24px"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Panel ID="finishuploadpanel" runat="server" Width="250px">
                                            <asp:FileUpload ID="finishfileupload" runat="server" Font-Names="標楷體" Font-Size="14px" />
                                            <asp:Button ID="finishfileuploadok" runat="server" Text="開始上傳附件" Font-Names="標楷體" Font-Size="Large" />
                                        </asp:Panel>
                                        <asp:Panel ID="finishPanel" runat="server" Width="250px">

                                            <asp:GridView ID="finishGridView" runat="server" HorizontalAlign="Left" AutoGenerateColumns="False" Font-Names="標楷體"
                                                RowStyle-HorizontalAlign="Center" EmptyDataText="無紀錄" DataKeyNames="序號" CssClass="wordtb">
                                                <AlternatingRowStyle BackColor="White" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:BoundField DataField="序號" ItemStyle-CssClass="hiddencol"
                                                        HeaderStyle-CssClass="hiddencol">
                                                        <HeaderStyle Width="15px" />
                                                        <ItemStyle CssClass="hiddencol" />
                                                    </asp:BoundField>
                                                    <asp:HyperLinkField HeaderText="附件" DataNavigateUrlFields="附件URL"
                                                        DataTextField="附件檔名" Target="_blank" ItemStyle-CssClass="tdleft">
                                                        <HeaderStyle Width="210px" />
                                                        <ItemStyle CssClass="tdleft" />
                                                    </asp:HyperLinkField>
                                                    <asp:CommandField ShowDeleteButton="True" DeleteText="X">
                                                        <HeaderStyle Width="15px" />
                                                    </asp:CommandField>
                                                </Columns>
                                                <EmptyDataRowStyle ForeColor="Red" />
                                            </asp:GridView>
                                        </asp:Panel>

                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FCE2CA" class="wordtt">維護管理人地址</td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC94" runat="server" CssClass="word" Width="475px"
                                            MaxLength="255" oninput="textcount('SWC94','SWC94_count','255');"
                                            Height="64px" TextMode="MultiLine"></asp:TextBox><br />
                                        <asp:Label ID="SWC94_count" runat="server" CssClass="wordtb" ForeColor="Red"
                                            Text="(0/255)"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FCE2CA" class="wordtt">維護管理人手機</td>
                                    <td class="word">
                                        <asp:TextBox ID="SWC95" runat="server" CssClass="word" Width="475px"
                                            oninput="chknumber09comm(this);textcount('SWC95','SWC95_count','255');" MaxLength="255"></asp:TextBox><br />
                                        <asp:Label ID="SWC95_count" CssClass="wordtb" runat="server" Text="(0/255)" ForeColor="Red"></asp:Label>
                                        <asp:Label ID="SWC95PS" CssClass="wordtb" runat="server" Text="範例：0928123456&nbsp;&nbsp;分隔請用 &quot;;&quot;" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FCE2CA" class="wordtt">基地概況</td>
                                    <td>
                                        <table width="500" border="0" cellpadding="0" cellspacing="0" class="word">
                                            <tr>
                                                <td height="30" class="word">
                                                    <asp:CheckBox ID="SWC61" runat="server" Text="建物" CssClass="word" onclick="textboxmode('SWC61','SWC62');" />
                                                    <asp:TextBox ID="SWC62" runat="server" CssClass="wordright" Width="50px" oninput="chknumber09(this);"></asp:TextBox>
                                                    戶
                                                </td>
                                                <td width="250">
                                                    <asp:CheckBox ID="SWC63" runat="server" Text="道路" CssClass="word" onclick="textboxmode('SWC63','SWC64');" />
                                                    <asp:TextBox ID="SWC64" runat="server" CssClass="wordright" Width="50px" oninput="chknumber09(this);"></asp:TextBox>
                                                    條
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" height="30" class="word">
                                                    <asp:CheckBox ID="SWC65" runat="server" Text="其他" CssClass="word" onclick="textboxmode('SWC65','SWC66');" /><br />
                                                    <asp:TextBox ID="SWC66" runat="server" CssClass="word" Width="475px"
                                                        MaxLength="255" oninput="textcount('SWC66','SWC66_count','255');"
                                                        Height="45px" TextMode="MultiLine"></asp:TextBox><br />
                                                    <asp:Label ID="SWC66_count" runat="server" CssClass="wordtb" ForeColor="Red"
                                                        Text="(0/255)"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#FCE2CA" class="wordtt">設施維護檢查及輔導紀錄表</td>
                                    <td class="word">
                                        <asp:Label ID="LabelSWC76" runat="server" Width="475px"></asp:Label><br />
                                        <asp:TextBox ID="SWC76" runat="server" CssClass="word" Width="475px"
                                            Height="45px" MaxLength="255" oninput="textcount('SWC76','SWC76_count','255');"
                                            TextMode="MultiLine" ReadOnly="True" Visible="False"></asp:TextBox>
                                        <br />
                                        <asp:Label ID="SWC76_count" runat="server" CssClass="wordtb" ForeColor="Red"
                                            Text="(0/255)" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table width="960" border="1" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="3" bgcolor="#FF7DC5" class="title">不予受理/撤銷/不予核定/廢止/失效</td>
                                </tr>
                                <tr>
                                    <td width="200" bgcolor="#FBE9F1" class="wordtt">日期</td>
                                    <td width="500" class="word">
                                        <asp:TextBox ID="SWC78" runat="server" CssClass="word" Width="200px" Format="yyyy-MM-dd"></asp:TextBox>
                                        <asp:CalendarExtender ID="SWC78_CalendarExtender" runat="server" Enabled="True" TargetControlID="SWC78" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                    </td>
                                    <td width="250" rowspan="1" valign="top">
                                        <asp:Label ID="Labelelse" runat="server" Text="其他附件清單" Font-Names="標楷體" Font-Size="24px"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Panel ID="elseuploadPanel" runat="server" Width="250px">
                                            <asp:FileUpload ID="elsefileupload" runat="server" Font-Names="標楷體" Font-Size="14px" />
                                            <asp:Button ID="elsefileuploadok" runat="server" Text="開始上傳附件" Font-Names="標楷體" Font-Size="Large" />
                                        </asp:Panel>
                                        <asp:Panel ID="elsePanel" runat="server" Width="250px">

                                            <asp:GridView ID="elseGridView" runat="server" HorizontalAlign="Left" AutoGenerateColumns="False" Font-Names="標楷體"
                                                RowStyle-HorizontalAlign="Center" EmptyDataText="無紀錄" DataKeyNames="序號" CssClass="wordtb">
                                                <AlternatingRowStyle BackColor="White" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:BoundField DataField="序號" ItemStyle-CssClass="hiddencol"
                                                        HeaderStyle-CssClass="hiddencol">
                                                        <HeaderStyle Width="15px" />
                                                        <ItemStyle CssClass="hiddencol" />
                                                    </asp:BoundField>
                                                    <asp:HyperLinkField HeaderText="附件" DataNavigateUrlFields="附件URL"
                                                        DataTextField="附件檔名" Target="_blank" ItemStyle-CssClass="tdleft">
                                                        <HeaderStyle Width="210px" />
                                                        <ItemStyle CssClass="tdleft" />
                                                    </asp:HyperLinkField>
                                                    <asp:CommandField ShowDeleteButton="True" DeleteText="X">
                                                        <HeaderStyle Width="15px" />
                                                    </asp:CommandField>
                                                </Columns>
                                                <EmptyDataRowStyle ForeColor="Red" />
                                            </asp:GridView>
                                        </asp:Panel>

                                    </td>
                                </tr>
                                </table>
                            <table width="960" border="1" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="3" bgcolor="#E8E04F" class="title">備註</td>
                                </tr>
                                <tr>
                                    <td width="200" height="100" bgcolor="#FFFAC4" class="wordtt">其他公開資訊</td>
                                    <td width="500" class="word">
                                        <asp:Panel ID="Panelotheropen1" runat="server">
                                            <asp:FileUpload ID="otheropen1_fileupload" runat="server" CssClass="wordtt" /><asp:Button
                                                ID="otheropen1_fileuploadok" runat="server" Text="上傳檔案" CssClass="wordttb" OnClientClick="divBlock.style.display='';" />
                                            <br />
                                            <span class="wordred">※ 上傳格式限定為PDF、JPG，檔案大小請於10mb以內</span><br />
                                        </asp:Panel>
                                        <asp:TextBox ID="otheropen1" runat="server" CssClass="word" Width="20px" Visible="False"></asp:TextBox>
                                        <asp:HyperLink ID="otheropen1_hyperlink" runat="server" CssClass="word"></asp:HyperLink>
                                        <asp:Button ID="otheropen1_fileclean" runat="server" CssClass="wordttb" Text="X" />
                                        <br />
                                        <br />
                                        <asp:Panel ID="Panelotheropen2" runat="server">
                                            <asp:FileUpload ID="otheropen2_fileupload" runat="server" CssClass="wordtt" /><asp:Button
                                                ID="otheropen2_fileuploadok" runat="server" Text="上傳檔案" CssClass="wordttb" OnClientClick="divBlock.style.display='';" />
                                            <br />
                                            <span class="wordred">※ 上傳格式限定為PDF、JPG，檔案大小請於10mb以內</span><br />
                                        </asp:Panel>
                                        <asp:TextBox ID="otheropen2" runat="server" CssClass="word" Width="20px" Visible="False"></asp:TextBox>
                                        <asp:HyperLink ID="otheropen2_hyperlink" runat="server" CssClass="word"></asp:HyperLink>
                                        <asp:Button ID="otheropen2_fileclean" runat="server" CssClass="wordttb" Text="X" />
                                        <br />
                                        <br />
                                        <asp:Panel ID="Panelotheropen3" runat="server">
                                            <asp:FileUpload ID="otheropen3_fileupload" runat="server" CssClass="wordtt" /><asp:Button
                                                ID="otheropen3_fileuploadok" runat="server" Text="上傳檔案" CssClass="wordttb" OnClientClick="divBlock.style.display='';" />
                                            <br />
                                            <span class="wordred">※ 上傳格式限定為PDF、JPG，檔案大小請於10mb以內</span><br />
                                        </asp:Panel>
                                        <asp:TextBox ID="otheropen3" runat="server" CssClass="word" Width="20px" Visible="False"></asp:TextBox>
                                        <asp:HyperLink ID="otheropen3_hyperlink" runat="server" CssClass="word"></asp:HyperLink>
                                        <asp:Button ID="otheropen3_fileclean" runat="server" CssClass="wordttb" Text="X" />
                                    </td>
                                    <td width="250" rowspan="4" valign="top">
                                        <asp:Panel ID="SWChistorypanel" runat="server" Width="100%">
                                            <asp:Label ID="Label4b" runat="server" Text="案件歷程清單" Font-Names="標楷體" Font-Size="24px"></asp:Label>
                                            <br />
                                            <asp:GridView ID="SWChistroygridview" runat="server" HorizontalAlign="Left" AutoGenerateColumns="False" Font-Names="標楷體"
                                                RowStyle-HorizontalAlign="Center" EmptyDataText="無紀錄" DataKeyNames="行政審查案件編號" CssClass="wordtb" Width="100%">
                                                <AlternatingRowStyle BackColor="White" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:HyperLinkField HeaderText="行政審查案件編號" DataNavigateUrlFields="行政審查案件編號URL"
                                                        DataTextField="行政審查案件編號" Target="_blank" ItemStyle-CssClass="tdleft">
                                                        <HeaderStyle Width="210px" />
                                                        <ItemStyle CssClass="tdleft" />
                                                    </asp:HyperLinkField>
                                                </Columns>
                                                <EmptyDataRowStyle ForeColor="Red" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <br />
                                        <asp:Panel ID="SWCPSpanel" runat="server" Width="100%" Visible="false">
                                            <asp:Label ID="Labelps" runat="server" Text="備註附件清單" Font-Names="標楷體" Font-Size="24px"></asp:Label>
                                            <br />
                                            <br />
                                            <asp:FileUpload ID="psfileupload" runat="server" Font-Names="標楷體" Font-Size="14px" />
                                            <asp:Button ID="psfileuploadok" runat="server" Text="開始上傳附件" Font-Names="標楷體" Font-Size="Large" />
                                            <asp:Panel ID="psPanel" runat="server" Width="250px">

                                                <asp:GridView ID="psGridView" runat="server" HorizontalAlign="Left" AutoGenerateColumns="False" Font-Names="標楷體"
                                                    RowStyle-HorizontalAlign="Center" EmptyDataText="無紀錄" DataKeyNames="序號" CssClass="wordtb">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <Columns>
                                                        <asp:BoundField DataField="序號" ItemStyle-CssClass="hiddencol"
                                                            HeaderStyle-CssClass="hiddencol">
                                                            <HeaderStyle Width="15px" />
                                                            <ItemStyle CssClass="hiddencol" />
                                                        </asp:BoundField>
                                                        <asp:HyperLinkField HeaderText="附件" DataNavigateUrlFields="附件URL"
                                                            DataTextField="附件檔名" Target="_blank" ItemStyle-CssClass="tdleft">
                                                            <HeaderStyle Width="210px" />
                                                            <ItemStyle CssClass="tdleft" />
                                                        </asp:HyperLinkField>
                                                        <asp:CommandField ShowDeleteButton="True" DeleteText="X">
                                                            <HeaderStyle Width="15px" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                    <EmptyDataRowStyle ForeColor="Red" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" height="100" bgcolor="#FFFAC4" class="wordtt">其他公開資訊</td>
                                    <td width="500" class="word">
                                        <asp:TextBox ID="SWC96" runat="server" CssClass="word" Width="475px"
                                            MaxLength="255" oninput="textcount('SWC96','SWC96_count','255');"
                                            Height="45px" TextMode="MultiLine"></asp:TextBox><br />
                                        <asp:Label ID="SWC96_count" runat="server" CssClass="wordtb" ForeColor="Red"
                                            Text="(0/255)"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:TextBox ID="SWC97" runat="server" CssClass="word" Width="475px"
                                            MaxLength="255" oninput="textcount('SWC97','SWC97_count','255');"
                                            Height="45px" TextMode="MultiLine"></asp:TextBox><br />
                                        <asp:Label ID="SWC97_count" runat="server" CssClass="wordtb" ForeColor="Red"
                                            Text="(0/255)"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:TextBox ID="SWC98" runat="server" CssClass="word" Width="475px"
                                            MaxLength="255" oninput="textcount('SWC98','SWC98_count','255');"
                                            Height="45px" TextMode="MultiLine"></asp:TextBox><br />
                                        <asp:Label ID="SWC98_count" runat="server" CssClass="wordtb" ForeColor="Red"
                                            Text="(0/255)"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FFFAC4" class="wordtt">違規編號</td>
                                    <td class="word">
                                        <asp:Label ID="LabelSWC81" runat="server" Width="475px"></asp:Label><br />
                                        <asp:TextBox ID="SWC81" runat="server" CssClass="word" Width="475px"
                                            MaxLength="255" oninput="textcount('SWC81','SWC81_count','255');"
                                            Height="68px" TextMode="MultiLine"></asp:TextBox><br />
                                        <asp:Label ID="SWC81_count" runat="server" CssClass="wordtb" ForeColor="Red"
                                            Text="(0/255)"></asp:Label>
                                        <asp:Label ID="SWC81PS" CssClass="wordtb" runat="server" Text="範例：CA09312006,CA09312007（用,分開）" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200" height="100" bgcolor="#FFFAC4" class="wordtt">備註</td>
                                    <td width="500" class="word">
                                        <asp:TextBox ID="SWC79" runat="server" CssClass="word" Width="475px"
                                            MaxLength="255" oninput="textcount('SWC79','SWC79_count','255');"
                                            Height="64px" TextMode="MultiLine"></asp:TextBox><br />
                                        <asp:Label ID="SWC79_count" runat="server" CssClass="wordtb" ForeColor="Red"
                                            Text="(0/255)"></asp:Label>
                                        <br />
                                        <asp:Label ID="SWC26Label" runat="server" Text="檔號：" class="word"></asp:Label>
                                        <asp:TextBox ID="SWC26" runat="server" CssClass="word" Width="200px"
                                            MaxLength="32"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>



                        <!-- 環景照片 -->
                        <table width="960" border="1" align="center" cellpadding="0" cellspacing="0">
                        <tbody><tr><td colspan="3" bgcolor="#00D3CD" class="title">環景照片</td></tr>
                               <tr><td colspan="2">
                                   
                                <asp:Panel ID="VrImgUpload" runat="server">

                                   <div class="editWrap">
                                        <div class="editLeft">選擇環景照片檔案：<br />
                                            <asp:FileUpload ID="VrFileUploadBar" runat="server" CssClass="wordttb" BackColor="Pink" Width="200px" />
                                            <asp:Button ID="VrFileUploadOk" runat="server" Text="上傳檔案" CssClass="wordttb" OnClientClick="divBlock.style.display='';" Height="26px" />
                                            <asp:Label ID="VrFilesNo" runat="server" Text="0" Visible="false"></asp:Label>
                                                     
                                            <asp:TextBox ID="VrFileUploadTxt" runat="server" CssClass="word" Width="20px" Visible="False"></asp:TextBox>
                                            <asp:TextBox ID="VrFileUploadPathTxt" runat="server" CssClass="word" Width="20px" Visible="False"></asp:TextBox>
                                            <asp:HyperLink ID="VrFileUpload_hyperlink" runat="server" CssClass="word" Target="_blank"></asp:HyperLink>   
                                            
                                            <asp:Button ID="VrFileClean" runat="server" Text="X" CssClass="wordttb" /><br />
                                            <span class="wordred word2">※ 上傳格式限定為JPG，檔案大小請於50mb以內</span>
                                            
                                            <br /> 輸入環景照片名稱：<br />
                                            <asp:TextBox ID="TextVrDesc" runat="server" Width="200px"></asp:TextBox>
                                                
                                            <asp:Button ID="AddVrFileList" runat="server" Text="加入清單" CssClass="wordttb" OnClientClick="return chknull('TextVrDesc');" Height="26px" />
                                  
                                        </div>
                                        <div class="editRight">

                                            <asp:Image ID="VrFile_img" runat="server" width="334px" Height="167px" />

                                        </div>
                                       
                                        <br />
                                        <%--<span class="wordred word2">※ 上傳格式限定為JPG，檔案大小請於50mb以內</span><br />--%>
                                    </div>
                            
                                </asp:Panel>
                                   
                                <asp:Panel ID="VrImgDemo" runat="server">
                                    <div class="photoView">
                                        <!-- 顯示圖片區域 -->
                                        <asp:Literal ID="VrViewArea" runat="server" />
                                        <%--<iframe id="VrViewArea" width="571px" height="405px" frameborder="0" runat="server" scrolling="no" src="" title=''></iframe>--%>
                                        <%-- <asp:Image ID="Image1" runat="server" width="571px" Height="405px" />--%>
                                        <br/><br/>
                                        (本功能請使用 Google Chrome 或 Microsoft Edge 觀看及操作）
                                    </div>
                                </asp:Panel>  

                                   
                                        <asp:GridView ID="VrGridView" runat="server" AutoGenerateColumns="False" RowStyle-HorizontalAlign="Center"
                                            EmptyDataText="無紀錄" DataKeyNames="序號" CssClass="scenePhoto"
                                            EnableModelValidation="True">
                                            <AlternatingRowStyle BackColor="White" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <Columns>
                                                <asp:BoundField DataField="序號" HeaderText="序號" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                                    <HeaderStyle BackColor="#2461bf" ForeColor="White" Height="30" BorderWidth="2px" BorderColor="gray"/>
                                                    <ItemStyle CssClass="hiddencol" Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="實體檔案名稱" HeaderText="實體檔案名稱" Visible="false">
                                                     <HeaderStyle Width="70px" BackColor="#2461bf" ForeColor="White" BorderWidth="2px" BorderColor="gray"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="環景照片名稱" HeaderText="環景照片名稱">
                                                     <HeaderStyle Width="50px" BackColor="#2461bf" ForeColor="White" BorderWidth="2px" BorderColor="gray"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="實體檔案路徑" HeaderText="實體檔案路徑" Visible="false">
                                                     <HeaderStyle Width="70px" BackColor="#2461bf" ForeColor="White" BorderWidth="2px" BorderColor="gray"/>
                                                </asp:BoundField>

                                        <asp:TemplateField ShowHeader="False" Visible="true" HeaderText="" >
                                            <ItemTemplate>
                                                <asp:Button ID="Viewbutton" runat="server" CausesValidation="False" CommandArgument="<%#Container.DataItemIndex %>" CommandName="ViewVr" Text="顯示" OnClientClick = "NewWindow();"/>
                                                <asp:Button ID="Fullbutton" runat="server" CausesValidation="False" CommandArgument="<%#Container.DataItemIndex %>" CommandName="ViewVrBig" Text="放大" OnClientClick="return openfull(this);" />
                                            </ItemTemplate>
                                                     <HeaderStyle Width="30px" BackColor="#2461bf" ForeColor="White" BorderWidth="2px" BorderColor="gray"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="False" Visible="true" HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="delbutton" runat="server" CausesValidation="False" CommandArgument="<%#Container.DataItemIndex %>" CommandName="DeleteVr" Text="刪除" />
                                            </ItemTemplate>
                                                     <HeaderStyle Width="30px" BackColor="#2461bf" ForeColor="White" BorderWidth="2px" BorderColor="gray"/>
                                        </asp:TemplateField>                                                
                                                <asp:BoundField DataField="vrid" HeaderText="id">
                                                     <HeaderStyle Width="70px" BackColor="#2461bf" ForeColor="White" BorderWidth="2px" />
                                                </asp:BoundField>


                                            </Columns>
                                            <EmptyDataRowStyle ForeColor="Red" />
                                        </asp:GridView>

                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <p>&nbsp;</p>
                        </asp:Panel>
                        
                        

                                                    
                                                    <br />



                    </td>
                </tr>

                <tr>
                    <td align="center" class="wordttcenter" valign="middle">
                        <asp:Panel ID="Buttonpanel" runat="server">
                            <asp:Button ID="AddNewOK" runat="server" Text="確定新增資料" Font-Names="標楷體" Font-Size="Medium" />
                            <asp:Button ID="UpdatecaseOK" runat="server" Text="確定更新資料" Font-Names="標楷體" Font-Size="Medium" Style="height: 26px" />
                            <asp:Button ID="CancelOK" runat="server" Text="放棄編修，返回總表" Font-Names="標楷體" Font-Size="Medium" Style="height: 26px" />
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <p>&nbsp;</p>
        </div>
        <div id="divBlock" style="border-style: solid; border-width: 1px; z-index: 1; position: fixed; top: 250px; left: 500px; height: 250px; width: 450px; background-color: #FEE387; filter: Alpha(Ppacity=80); display: none;">
            <font color="#006600" size="7">圖片上傳中......</font><br />
            <p>
            </p>
            <br />
            <font color="#006600" size="7">請勿進行任何動作</font><br />
        </div>
        <div id="divblock2" style="border-style: solid; border-width: 1px; z-index: 1; position: fixed; top: 250px; left: 500px; height: 250px; width: 450px; background-color: #808080; filter: Alpha(Ppacity=80); display: none;">
            <font color="yellow" size="7">資料傳送中......</font><br />
            <p>
            </p>
            <br />
            <font color="yellow" size="7">請勿進行任何動作</font><br />
        </div>
    </form>
</body>
</html>
