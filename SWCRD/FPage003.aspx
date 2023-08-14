<%@ Page Title="線上行事曆" Language="C#" MasterPageFile="~/PriPage/MasterPageA.master" AutoEventWireup="true" CodeFile="FPage003.aspx.cs" Inherits="SWCRD_FPage003" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<link rel="stylesheet" type="text/css" href="../css/calendar/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="../css/calendar/calendar_test.css">
    <script src="../js/calendar/jquery-2.2.3.min.js"></script>
    <script src="../js/calendar/angular.min.js"></script>
    <script src="../js/calendar/moment.js"></script>
    <script src="../js/calendar/fullcalendar.min.js"></script>
    <script src="../js/calendar/bootstrap.min.js"></script>
    <script src="../js/calendar/ui-bootstrap-tpls.js"></script>
    <script src="../js/calendar/calendar.js"></script>
    <script src="../js/calendar/lang-all.js"></script>
    <script src="https://apis.google.com/js/api.js"></script>
    <script src="../js/calendar/calendar_test.js"></script>
    <script src="../js/calendar/calendar_test2.js?20220330"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"/>
	<div class="content-s content-s-inquire">
        <div class="content-s">
		</div>
		<div class="CDtitle"><img src="../images/icon/calendar.png" id="LCD">會勘行事曆</div>
        <div  class="rentdiv">
              <span class="rentbox"></span>審查&nbsp;&nbsp;&nbsp;
              <span class="rentbox2"></span>檢查
        </div>		
		<!--<a href="https://calendar.google.com/calendar/r/month/2021/8/31?tab=rc" target="_blank">Google行事曆</a>-->
				<br>
				<div ng-app="myApp">
					<div ng-controller="Ctrl">
						<p></p>
						<div class="container">
							<script type="text/ng-template" id="myModalContent.html">
								<div class="modal-header">
									<button type="button" class="close" data-dismiss="modal" aria-label="Close" ng-click="cancel()"><span aria-hidden="true">&times;</span></button>
									<h3 class="modal-title">詳細資訊</h3>
								</div>
								<div class="modal-body">
									<div class="text-center well">
									<table class="wellD">
									  <tr>
									    <th><label for="nome">案件編號：</label></th>
										<td>{{    caseno   }}</td>
									  </tr>
									  <tr>
									    <th><label for="nome">案件名稱：</label></th>
										<td>{{    case   }}</td>
									  </tr>
									  <tr>
									    <th><label for="nome">案件狀態：</label></th>
										<td>{{    casestatus   }}</td>
									  </tr>
									  <tr>
									    <th><label for="nome">義務人：</label></th>
										<td>{{    obligor   }}</td>
									  </tr>
									  <tr>
									    <th><label for="nome">行政區：</label></th>
										<td>{{    area   }}</td>
									  </tr>
									  <tr>
									    <th><label for="nome">會議日期(起)：</label></th>
										<td>{{   gfExtenso    }}</td>
									  </tr>
									  <tr>
									    <th><label for="nome">會議日期(迄)：</label></th>
										<td>{{   gfExtenso_end    }}</td>
									  </tr>
									  <tr>
									    <th><label for="nome">會議地點：</label></th>
										<td>{{    place   }}</td>
									  </tr>
									  <tr>
									    <th><label for="nome">技師：</label></th>
										<td>{{    people   }}</td>
									  </tr>
									  <tr>
									    <th><label for="nome">單位：</label></th>
										<td>{{    guild   }}</td>
									  </tr>
									</table>
									
								</div>
								<div style="display:none;">
									<div class="form-group">
										<label class="control-label">名稱</label>
										<input type="text" disabled="false" class="form-control" id="title" ng-model="nome">
									</div>
									<div class="form-group">
										<label class="control-label">時間(起)</label>
										<input type="text" disabled="false" class="form-control" id="date" ng-model="date">
									</div>
									<div class="form-group">
										<label class="control-label">時間(迄)</label>
										<input type="text" disabled="false" class="form-control" id="date_end" ng-model="date_end">
									</div>
								</div>
								<div class="modal-footer">
									<button class="btn btn-default" type="button" ng-click="cancel()">取消</button>
									<button class="btn btn-primary" type="button" onclick="login()">登入</button>
									<button class="btn btn-primary" type="button" disabled id="addgoogle" onclick="execute()">新增到google行事曆</button>
								</div>
							</script>
							<div class="alert-success calAlert ng-scope" ng-show="alertMessage != undefined &amp;&amp; alertMessage != ''">
								<h4 class="ng-binding">{{alertMessage}}</h4>
							</div>
							<div>
								<div></div>
								<div select="renderCalender('myCalendar1');">
									<div class="calendar" ng-model="eventSources" calendar="myCalendar1" ui-calendar="uiConfig.calendar"></div>
								</div>
							</div>
						</div>
						<p></p>
					</div>
				</div>
</div>
	<script type="text/javascript">
        var CLIENT_ID = '272620897647-f8ckjg7gh332v2ekrq4cc42fqcabbqfl.apps.googleusercontent.com';
        var API_KEY = 'AIzaSyCtbLShVfKYagm_4MaU8TgH6RIUdRVZtFA';

        var authParams = {
            'response_type': 'token', // Retrieves an access token only
            'client_id': CLIENT_ID, // Client ID from Cloud Console
            'immediate': false, // For the demo, force the auth window every time
            'scope': ['https://www.googleapis.com/auth/calendar https://www.googleapis.com/auth/calendar.events']  // Array of scopes
        };

        //init
        gapi.load("client:auth2", function () {
            gapi.auth2.init({ client_id: CLIENT_ID });
        });

        //登入結果,設定token
        function myCallback(authResult) {
            //if (authResult && authResult['access_token']) {
                gapi.auth.setToken(authResult);
                //console.log(authResult);
                loadClient();
            //} else {
                // Authorization failed or user declined
            //}
        }

        //連API
        function loadClient() {
            gapi.client.setApiKey(API_KEY);
            return gapi.client.load("https://content.googleapis.com/discovery/v1/apis/calendar/v3/rest")
                .then(function () { console.log("GAPI client loaded for API"); },
                    function (err) { console.error("Error loading GAPI client for API", err); });
        }

		function login() {
			gapi.auth.authorize(authParams, myCallback).then(function(){
				document.getElementById('addgoogle').disabled = false;
			});
		}

        // 新增事件
        function execute() {
            var events = [{
                'summary': document.getElementById('title').value,
                'description': document.getElementById('title').value,
                'start': {
                    'dateTime': document.getElementById('date').value,
                    'timeZone': 'Asia/Taipei'
                },
                'end': {
                    'dateTime': document.getElementById('date_end').value,
                    'timeZone': 'Asia/Taipei',
                }
            }];
            var request = gapi.client.calendar.events.insert({
				calendarId: 'primary',
				resource: events[0]
			});
			request.execute();
			alert('已新增至google行事曆');
        }
    </script>
	
</asp:Content>