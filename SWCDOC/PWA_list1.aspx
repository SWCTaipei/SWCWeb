<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PWA_list1.aspx.cs" Inherits="SWCDOC_PWA_list1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>PWA_list</title>
    <link type="text/css" rel="stylesheet" href="../css/all.css?202202181203"/>
	<link type="text/css" rel="stylesheet" href="../css/all_PWA.css?202202181203"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
	<style>
		body{
			background:#FFF;
			margin:0;
		}
		
		.tab_css input[type="radio"]{
        	display:none;
        }
        .tab_css label{
        	margin: 0 15px 25px 0;
        	padding: 7px 10px;
        	cursor: pointer;
        	border-radius: 5px;
        	background: #1C930F ;
        	color: #fff;
        	opacity: 0.3;
        	font-size:17px;
        }
        .tab_css{
        	display:flex;
        	flex-wrap:wrap;
        	justify-content:center;
			margin-top:90px;
        }
        
        .tab_content{
        	order:1;
        	display: none;
        	width:100%;
        	border-bottom: 3px solid #ddd;
        	line-height: 1.6; 
        	font-size: .9em; 
        	padding: 15px; 
        	border: 1px solid #ddd; 
        	border-radius: 5px;
			margin-top:0px;
        }
        .tab_css  input:checked + label,
        .tab_css label:hover{opacity: 1; font-weight:bold;}
        .tab_css input:checked + label + .tab_content{display: initial;}
		
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
					
					var swc000 = table.rows[2].cells[0].innerHTML;
					var swc025 = table.rows[3].cells[0].innerHTML;
					var swc012 = table.rows[4].cells[0].innerHTML;
					var swc008 = table.rows[5].cells[0].innerHTML;
					
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
        <div class="wrap-s">
        <div class="header_PWA">
		  <img class="logo" src="../img/logo2.svg"/>
            <span>臺北市水土保持申請書件平台</span>
            <%--<img class="download" src="../../img/PWA_icon-08.svg"/>
		  <img class="logo_text" src="../../img/logo_text.svg"/>--%>
		</div>
        <%--<div class="hello">您好</div>  --%>
            
			
				  
			<div class="tab_css">
               <!-- TAB1 打包區塊 start -->
               <input id="tab1" type="radio" name="tab" checked="checked"/>
               <%--<label for="tab1">建立表單</label>--%>
                <div class="tab_content0">
				  <div class="pwa_search" style="display:none;">    
					<table class="pwa_searchTB">
						<tr>
							<th>案件編號:</th>
							<td><asp:TextBox ID="TBSWC000" runat="server" /></td>
						</tr>
						<tr>
							<th>承辦人員:</th>
							<td><asp:TextBox ID="TBSWC025" runat="server" /></td>
						</tr>
						<tr>
							<th>轄區:</th>
							<td>
								<asp:DropDownList ID="TBSWC012" runat="server">
									<asp:ListItem Value=""></asp:ListItem>
									<asp:ListItem Value="北一區">北一區</asp:ListItem>
									<asp:ListItem Value="北二區">北二區</asp:ListItem>
									<asp:ListItem Value="北三區">北三區</asp:ListItem>
									<asp:ListItem Value="北四區">北四區</asp:ListItem>
									<asp:ListItem Value="北五區">北五區</asp:ListItem>
									<asp:ListItem Value="北六區">北六區</asp:ListItem>
									<asp:ListItem Value="北七區">北七區</asp:ListItem>
									<asp:ListItem Value="北八區">北八區</asp:ListItem>
									<asp:ListItem Value="南一區">南一區</asp:ListItem>
									<asp:ListItem Value="南二區">南二區</asp:ListItem>
									<asp:ListItem Value="南三區">南三區</asp:ListItem>
									<asp:ListItem Value="南四區">南四區</asp:ListItem>
									<asp:ListItem Value="南五區">南五區</asp:ListItem>
									<asp:ListItem Value="南六區">南六區</asp:ListItem>
									<asp:ListItem Value="南七區">南七區</asp:ListItem>
									<asp:ListItem Value="南八區">南八區</asp:ListItem>
								</asp:DropDownList>
							</td>
						</tr>
						<tr>
							<th>行政區:</th>
							<td>
								<asp:DropDownList ID="TBSWC008" runat="server">
									<asp:ListItem Value=""></asp:ListItem>
									<asp:ListItem Value="北投">北投</asp:ListItem>
									<asp:ListItem Value="士林">士林</asp:ListItem>
									<asp:ListItem Value="內湖">內湖</asp:ListItem>
									<asp:ListItem Value="中山">中山</asp:ListItem>
									<asp:ListItem Value="中正">中正</asp:ListItem>
									<asp:ListItem Value="信義">信義</asp:ListItem>
									<asp:ListItem Value="大安">大安</asp:ListItem>
									<asp:ListItem Value="南港">南港</asp:ListItem>
									<asp:ListItem Value="文山">文山</asp:ListItem>
								</asp:DropDownList>
							</td>
						</tr>
					</table>
				    <div class="pwacenter"> 
					  <asp:Button ID="btn_search" runat="server" Text="查詢" UseSubmitBehavior="false" onclientclick="return searchdata()" />
				      <asp:Button ID="btn_clear" runat="server" Text="清除" UseSubmitBehavior="false" onclientclick="return cleardata()" />
				    </div>
				   </div>

				  <div class="PWA_now">
                    <div class="now_title">建立表單</div>
				    <asp:Panel ID="PanelTable" runat="server">
				    </asp:Panel>
                 </div>
                </div>
               <!-- TAB1 打包區塊 end -->
           </div>
		   
        </div>
		<asp:TextBox ID="TBtempdata" runat="server" style="border:none;width:0;" />
    </form>
</body>
</html>
