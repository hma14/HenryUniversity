'use strict';

var universityapp = angular.module('universityApp', ['ngRoute', 'apiServicesApp']);

universityapp.factory('Department', ['$resource', function ($resource) {
    return $resource('api/Departments/:id', {}, {
        query: { method: "GET", isArray: true },
        create: { method: "POST" },
        get: { method: "GET", url: "/api/Departments?id=:id" },
        remove: { method: "DELETE", url: "/api/Departments?id=:id" },
        update: { method: "PUT", url: "/api/Departments?id=:id" }
    })
}]);


universityapp.config(function ($routeProvider) {
    $routeProvider.
        when('/students', {
            templateUrl: 'Html/Students.html',
            controller: 'studentCtrl'
        }).
        when('/addstudent', {
            templateUrl: 'Html/AddStudent.html',
            controller: 'studentCtrl'
        }).
        when('/editstudent', {
            templateUrl: 'Html/EditStudent.html',
            controller: 'studentCtrl'
        }).
        when('/courses', {
            templateUrl: 'Html/Courses.html',
            controller: 'courseCtrl'
        }).
        when('/addcourse', {
            templateUrl: 'Html/AddCourse.html',
            controller: 'courseCtrl'
        }).
        when('/editcourse', {
            templateUrl: 'Html/EditCourse.html',
            controller: 'courseCtrl'
        }).
        otherwise({
            redirectTo: '/'
        });

});


universityapp.run(function ($rootScope, Department) {
    var promise = Department.query();
    promise.$promise.then(function (payload) {
        $rootScope.departments = payload;
    },
    function (errPayload) {
        $rootScope.error = "Oops... GET Departments went wrong: " + errPayload;
    });

    var saveState = {};
});

universityapp.factory('$exceptionHandler', function ($injector) {
    return function (exception, cause) {
        var $rootScope = $injector.get('$rootScope');
        $rootScope.errors = $rootScope.errors || [];
        $rootScope.errors.push('Cause: ' + cause + ', Details: ' + exception);
        console.log($rootScope.errors);
    }
})


