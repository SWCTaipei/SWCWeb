function cc(QQ){
	var obj = new Array();
	obj = QQ.split('/');
	for (var x = 0; x < obj.length; x++) {
		var objj = new Array();
		objj = obj[x].split('|');
	
		//書件;起;迄;義務人;案件編號;地點;地區;狀態;顏色;監造技師;檢查公會
		var Q = '{ "case": "' + objj[0] + '", "title": "' + objj[3] + ',' + objj[6] + '" , "start": "' + objj[1] + '",  "end": "' + objj[2] + '" , "obligor": "' + objj[3] + '", "caseno": "' + objj[4] + '", "place": "' + objj[5] + '", "area": "' + objj[6] + '", "casestatus": "' + objj[7] + '", "backgroundColor": "' + objj[8] + '", "people": "' + objj[9] + '", "guild": "' + objj[10] + '"}';
		//var Q = '{ "case": "' + objj[0] + '", "title": "' + objj[3] + ',' + objj[6] + '" , "start": "' + objj[1] + '",  "end": "' + objj[2] + '" , "obligor": "' + objj[3] + '", "caseno": "' + objj[4] + '", "place": "' + objj[5] + '", "area": "' + objj[6] + '", "casestatus": "' + objj[7] + '"}';
		var Qjson = JSON.parse(Q);
		obj[x] = Qjson;
	}
	
	var events = new Array();
	for (var x = 0; x < obj.length; x++) {
		events[x] = obj[x];
	}
	
	
	var app = angular.module('myApp', ['ui.bootstrap', 'ui.calendar']);
	
	app.controller('Ctrl', function($scope, $uibModal, $log, $compile, uiCalendarConfig) {
	
		var date = new Date();
		var d = date.getDate();
		var m = date.getMonth();
		var y = date.getFullYear();
		
		
		$scope.events = events;
		
		
		/* event source that calls a function on every view switch */
		$scope.eventSources = [$scope.events];
	
		/* Render Tooltip */
		//$scope.eventRender = function(event, element, view) {
		//	element.attr({
		//		'uib-tooltip': event.title,
		//		'tooltip-append-to-body': true
		//	});
		//	$compile(element)($scope);
		//};
	
		$scope.alertOnEventClick = function(date, jsEvent, view, size) {
			$scope.selected = date;
			// Modal
	
			$scope.animationsEnabled = true;
	
			var modalInstance = $uibModal.open({
				animation: $scope.animationsEnabled,
				templateUrl: 'myModalContent.html',
				controller: 'ModalInstanceCtrl',
				size: size,
				resolve: {
					items: function() {
						return $scope.selected;
					}
				}
			});
	
			$scope.toggleAnimation = function() {
				$scope.animationsEnabled = !$scope.animationsEnabled;
			};
			// FIM Modal
	
		};
	
		/* Change View */
		$scope.changeView = function(view, calendar) {
			uiCalendarConfig.calendars[calendar].fullCalendar('changeView', view);
		};
		$scope.renderCalender = function(calendar) {
			if (uiCalendarConfig.calendars[calendar]) {
				uiCalendarConfig.calendars[calendar].fullCalendar('render');
			}
		};
	
		var currentLangCode = 'zh-tw';
		$scope.uiConfig = {
			calendar: {
				defaultView: 'month',
				height: "auto",
				lang: currentLangCode,
				editable: false,
				eventTextColor: '#fff',
				eventBorderColor: '#9987B5',
				eventBackgroundColor: '#9987B5',
				header: {
					left: 'title',
					center: 'month,agendaWeek,agendaDay',
					right: 'today prev,next'
				},
				nowIndicator : true,
				eventClick: $scope.alertOnEventClick,
				displayEventTime : false,
				//eventDrop: $scope.alertOnDrop,
				eventResize: $scope.alertOnResize,
				eventRender: $scope.eventRender
			}
		};
	
	});
	
	
	app.controller('ModalInstanceCtrl', function($scope, $uibModalInstance, items) {
	
		$scope.nome = items.title;
		$scope.ted = items.start;
		$scope.ted_end = items.end;
		$scope.obligor = items.obligor;
		$scope.caseno = items.caseno;
		$scope.case = items.case;
		$scope.area = items.area;
		$scope.place = items.place;
		$scope.date = moment($scope.ted).format("YYYY-MM-DDThh:mm:ss");
		$scope.date_end = moment($scope.ted_end).format("YYYY-MM-DDThh:mm:ss");
		$scope.gfExtenso = moment(items.start).format("YYYY 年 MMMM Do dddd hh:mm");
		$scope.gfExtenso_end = moment(items.end).format("YYYY 年 MMMM Do dddd hh:mm");
		$scope.casestatus = items.casestatus;
		$scope.people = items.people;
		$scope.guild = items.guild;
	
		$scope.items = items;
	
		$scope.selected = {
			item: $scope.items[0]
		};
	
		$scope.ok = function() {
			$uibModalInstance.close($scope.selected.item);
		};
	
		$scope.cancel = function() {
			$uibModalInstance.dismiss('cancel');
		};
	});
	
}