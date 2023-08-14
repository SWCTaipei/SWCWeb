<%@ Page Title="首頁" Language="C#" MasterPageFile="~/PriPage/MasterPageA.master" AutoEventWireup="true" CodeFile="HaloPage001.aspx.cs" Inherits="SWCDOC_HaloPage001" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<script>
		function get_YN()
		{
			if(document.getElementById('<%=LB_YN.ClientID%>').innerHTML == "T")
			{
				alert("提醒您，您所填登之執業執照到期日已逾期，無法使用水保計畫案件申請相關權限，若需恢復權限，請至「帳號管理」中更新「執業執照及到期日」欄位資訊。");
				return false;
			}
			return true;
		}
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
<%--義務人--%>
    <asp:Panel ID="Panel1" runat="server" Visible="false" CssClass="btnbg">
      <div class="NIMGwrap">
        <div class="Grid Grid--gutters Grid--cols-4 u-textCenter">
            
            <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <asp:HyperLink ID="HyperLink3" ImageUrl="../images/IMGbtn/06.jpg" runat="server" NavigateUrl="~/SWCDOC/SWC001.aspx" Text="我的案件列表" />
              <p>我的案件列表</p>
              </div>
           </div></div>

            <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <asp:HyperLink ID="HyperLink11" ImageUrl="../images/IMGbtn/05.jpg" runat="server" NavigateUrl="~/SWCRD/USDate002.aspx" Text="基本資料更新" />
              <p>基本資料更新</p>
              </div>
           </div></div>

            <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <asp:HyperLink ID="HyperLink8" ImageUrl="../images/IMGbtn/08.jpg" runat="server" NavigateUrl="~/SWCDOC/OnlineApply001.aspx" Text="已完工設施維護" />
              <p>已完工設施維護</p>
              </div>
           </div></div>
		  </div>
       </asp:Panel>
<%--義務人--%>
 
<%--技師--%>
    <asp:Panel ID="Panel2" runat="server" Visible="false" CssClass="btnbg">
        <div class="NIMGwrap">
            <asp:Label ID="LB_YN" runat="server" style="display:none;" />
        <div class="Grid Grid--gutters Grid--cols-4 u-textCenter">
            
            <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <asp:HyperLink ID="HyperLink1" ImageUrl="../images/IMGbtn/06.jpg" runat="server" onclick="return get_YN();" NavigateUrl="~/SWCDOC/SWC001.aspx" Text="我的案件列表" />
              <p>我的案件列表</p>
              </div>
           </div></div>

            <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <asp:HyperLink ID="HyperLink9" ImageUrl="../images/IMGbtn/02.jpg" runat="server" NavigateUrl="~/SWCRD/CGLNTYPE.aspx" Text="受託審查/檢查案件" />
              <p>受託審查/檢查案件</p>
              </div>
           </div></div>

            <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <asp:HyperLink ID="HyperLink10" ImageUrl="../images/IMGbtn/11.jpg" runat="server" onclick="return get_YN();" NavigateUrl="~/SWCRD/Dlist001.aspx" Text="待辦報表" />
              <p>待辦報表</p>
              </div>
           </div></div>
        </div>
      </div>


        <div class="NIMGwrap">
        <div class="Grid Grid--gutters Grid--cols-4 u-textCenter">

            <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <asp:HyperLink ID="HyperLink6" ImageUrl="../images/IMGbtn/12.jpg" runat="server" onclick="return get_YN();" NavigateUrl="~/SWCDOC/LIST024.aspx" Text="代辦案件" />
              <p>代辦案件</p>
              </div>
           </div></div>


            <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <asp:HyperLink ID="HyperLink5" ImageUrl="../images/IMGbtn/05.jpg" runat="server" NavigateUrl="~/SWCDOC/SWCBase001.aspx" Text="帳號管理" />
              <p>帳號管理</p>
              </div>
           </div></div>

            <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <a href="sample.aspx"><span><img src="../images/IMGbtn/09.jpg" /></span>
              <p>範例文件</p></a>
              </div>
           </div></div>

            <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <asp:HyperLink ImageUrl="../images/IMGbtn/13.jpg" runat="server" NavigateUrl="~/SWCDOC/SWCProxy001.aspx" Text="設定代理人" />
              <p>設定代理人</p>
              </div>
           </div></div>
        </div>
       </div>
</asp:Panel>
 <%--技師--%>
 
<%--公會--%>
    <asp:Panel ID="Panel3" runat="server" Visible="false" CssClass="btnbg">

      <div class="NIMGwrap">
        <div class="Grid Grid--gutters Grid--cols-4 u-textCenter">
           
            <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <asp:HyperLink ID="HyperLink4" ImageUrl="../images/IMGbtn/01.jpg" runat="server" NavigateUrl="~/SWCDOC/SWC001.aspx" Text="我的案件列表" />
              <p>我的案件列表</p>
              </div>
           </div></div>

           <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <asp:HyperLink ID="HyperLink61" ImageUrl="../images/IMGbtn/02.jpg" runat="server" NavigateUrl="~/SWCRD/FPage001.aspx" Text="指派案件委員" />
              <p>指派案件委員</p>
              </div>
           </div></div>

            <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <asp:HyperLink ID="HyperLink7" ImageUrl="../images/IMGbtn/03.jpg" runat="server" NavigateUrl="~/SWCDOC/BasicInfo/BasPg001.aspx" Text="子帳號權限管理" />
              <p>子帳號權限管理</p>
              </div>
           </div></div>


            <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <asp:HyperLink ImageUrl="../images/IMGbtn/06.jpg" runat="server" NavigateUrl="~/SWCDOC/SWCProxy002.aspx" Text="設定代理人" />
              <p>設定代理人</p>
              </div>
           </div></div>
          
        </div>
       </div>
</asp:Panel>
 <%--公會--%>

<%--其他機關--%>
    <asp:Panel ID="Panel4" runat="server" Visible="false" CssClass="btnbg">
    
      <div class="NIMGwrap">
        <div class="Grid Grid--gutters Grid--cols-4 u-textCenter">

            <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <asp:HyperLink ImageUrl="../images/IMGbtn/06.jpg" runat="server" NavigateUrl="~/SWCDOC/SWC001.aspx" Text="我的案件列表" />
              <p>我的案件列表</p>
              </div>
           </div></div>

            <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <asp:HyperLink ImageUrl="../images/IMGbtn/05.jpg" runat="server" NavigateUrl="~/SWCDOC/SWCBase002.aspx" Text="帳號管理" />
              <p>帳號管理</p>
              </div>
           </div></div>

            <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <asp:HyperLink ID="HyperLink12" ImageUrl="../images/IMGbtn/02.jpg" runat="server" NavigateUrl="~/SWCDOC/SWCOrganList.aspx" Text="審查帳號" />
            <p>帳號審核</p>
              </div>
           </div></div>

        </div>
       </div>
</asp:Panel>
 <%--其他機關--%>

 <%--建築師--%>
    <asp:Panel ID="Panel5" runat="server" Visible="false" CssClass="btnbg">
      <div class="NIMGwrap">
        <div class="Grid Grid--gutters Grid--cols-4 u-textCenter">
            <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <asp:HyperLink ImageUrl="../images/IMGbtn/06.jpg" runat="server" NavigateUrl="~/SWCDOC/SWC001.aspx" Text="我的案件列表" />
              <p>我的案件列表</p>
              </div>
           </div></div>
        </div>
       </div>
</asp:Panel>
 <%--建築師--%>

 <%--營造單位--%>
	<asp:Panel ID="Panel6" runat="server" Visible="false" CssClass="btnbg">
      <div class="NIMGwrap">
        <div class="Grid Grid--gutters Grid--cols-4 u-textCenter">

            <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <asp:HyperLink ImageUrl="../images/IMGbtn/06.jpg" runat="server" NavigateUrl="~/SWCDOC/SWC001.aspx" Text="我的案件列表" />
            <p>我的案件列表</p>
              </div>
           </div></div>

        </div>
       </div>
</asp:Panel>
 <%--營造單位--%>

 <%--代理技師--%>
	<asp:Panel ID="Panel7" runat="server" Visible="false" CssClass="btnbg">
      <div class="NIMGwrap">
        <div class="Grid Grid--gutters Grid--cols-4 u-textCenter">

            <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <asp:HyperLink ImageUrl="../images/IMGbtn/06.jpg" runat="server" onclick="return get_YN();" NavigateUrl="~/SWCDOC/SWC001.aspx" Text="我的案件列表" />
              <p>我的案件列表</p>
              </div>
           </div></div>

            <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <asp:HyperLink ImageUrl="../images/IMGbtn/12.jpg" runat="server" onclick="return get_YN();" NavigateUrl="~/SWCDOC/LIST024.aspx" Text="代辦案件" />
              <p>代辦案件</p>
              </div>
           </div></div>

            <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <a href="sample.aspx"><span><img src="../images/IMGbtn/09.jpg" /></span>
                  <p>範例文件</p></a>
              </div>
           </div></div>
        </div>
       </div>
</asp:Panel>
 <%--代理技師--%>


 <%--代理公會--%>
	<asp:Panel ID="Panel8" runat="server" Visible="false" CssClass="btnbg">
      <div class="NIMGwrap">
        <div class="Grid Grid--gutters Grid--cols-4 u-textCenter">

            <div class="Grid-cell"><div class="Demo">
              <div class="IMGbtn">
              <asp:HyperLink ImageUrl="../images/IMGbtn/06.jpg" runat="server" NavigateUrl="~/SWCDOC/SWC001.aspx" Text="我的案件列表" />
            <p>我的案件列表</p>
              </div>
           </div></div>
            
        </div>
       </div>
</asp:Panel>
 <%--代理公會--%>

        
     <div class="HLnews">
        <div class="newsiner">
         <!--<span class="newstitle">系統公告</span>-->
         <div id="tab-demo">
    <ul class="tab-title">
        <li><a href="#tab01">系統公告</a></li>
        <li><a href="#tab02">快速連結</a></li>
        <!--<li><a href="#tab03">tab03</a></li>-->
    </ul>
    <div id="tab01" class="tab-inner">
                 <asp:GridView ID="GVNews" runat="server" DataSourceID="SqlDataSource" CssClass="newstb" AutoGenerateColumns="false" PagerStyle-CssClass="pgr" AllowPaging="true" AllowSorting="True" >
                    <Columns>
                        <asp:BoundField DataField="BBDateStart" HeaderText="公告日期" SortExpression="BBDateStart"/>
                        <asp:BoundField DataField="BBUnit" HeaderText="公佈單位" SortExpression="BBUnit"/>
                        <asp:HyperLinkField DataNavigateUrlFields="BBURL" DataTextField="BBTitle" HeaderText="公佈主旨" Target="_blank" />
                    </Columns>
                </asp:GridView>
				<PagerSettings LastPageText="最後一頁" FirstPageText="第一頁" Mode="NumericFirstLast" NextPageText="下一頁" PreviousPageText="上一頁" />
				<PagerStyle CssClass="pgr" BackColor="#7baac5" Width="100%" />
				<asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SWCConnStr %>"
                            SelectCommand=" select *,'../SWCTCGOV/SWCGOV013.aspx?BillBID='+BBNo as BBURL from BillBoard where left(convert(varchar, getdate(), 120), 10) >= BBDateStart and left(convert(varchar, getdate(), 120), 10) <= BBDateEnd and BBShow = '是' order by BBDateStart DESC "></asp:SqlDataSource>

    </div>
    <div id="tab02" class="tab-inner">
        <div class="linkwrap">
            <span><asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/SWCAPPLY/SWCDT007L.aspx" Text="已完工管理介面" Visible="false" /></span>
            <span><a href="../SWCRD/shiftsList001.aspx"" target="_blank">派案歷程</a></span>
			<span><a href="../DouCode/GuildUsers.aspx" target="_blank">共同供應契約審查委員合格名冊</a></span>
			<span><asp:LinkButton ID="LB1" OnClick="LB1_Click" runat="server" Text="T-GEO空間地理資訊平台"></asp:LinkButton></span>
        </div>
    </div>
    <!--<div id="tab03" class="tab-inner">
        <p>tab03的內容</p>
    </div>-->
</div> 
      
          
        </div>
     </div>
            
      <script>
          function myFunction() {
              alert("網頁施工中...");
          }
      </script>    
      
     <script src="../js/jquery-3.1.1.min.js"></script>
     <script src="../js/allhref.js"></script>
     <script src="../js/alljq.js"></script>

</asp:Content>

