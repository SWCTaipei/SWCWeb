var app = angular.module('myApp', ['ui.bootstrap', 'ui.calendar']);

app.controller('Ctrl', function($scope, $uibModal, $log, $compile, uiCalendarConfig) {

    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();

    //$scope.events = [{
    //    title: 'All Day Event',
    //    start: '2021-08-31 09:00:00',
	//	end: '2021-08-31 19:00:00'
    //}];

    /* event source that calls a function on every view switch */
    $scope.eventSources = [$scope.events];

    /* Render Tooltip */
    $scope.eventRender = function(event, element, view) {
        element.attr({
            'uib-tooltip': event.title,
            'tooltip-append-to-body': true
        });
        $compile(element)($scope);
    };

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
            editable: true,
            eventTextColor: '#fff',
            eventBorderColor: '#9987B5',
            eventBackgroundColor: '#9987B5',
            header: {
                left: 'title',
                center: 'month,agendaWeek,agendaDay',
                right: 'today prev,next'
            },
            eventClick: $scope.alertOnEventClick,
            eventDrop: $scope.alertOnDrop,
            eventResize: $scope.alertOnResize,
            eventRender: $scope.eventRender
        }
    };

});

app.controller('ModalInstanceCtrl', function($scope, $uibModalInstance, items) {
	
		$scope.nome = items.title;
		$scope.ted = items.start;
		$scope.ted_end = items.end;
		$scope.date = moment($scope.ted).format("YYYY-MM-DDThh:mm:ss");
		$scope.date_end = moment($scope.ted_end).format("YYYY-MM-DDThh:mm:ss");
		$scope.gfExtenso = moment(items.start).format("YYYY 年 MMMM Do dddd hh:mm");
		$scope.gfExtenso_end = moment(items.end).format("YYYY 年 MMMM Do dddd hh:mm");

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