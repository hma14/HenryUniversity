'use strict';

var apiServicesApp = angular.module('apiServicesApp', ['ngResource']);

apiServicesApp.factory('Course', ['$resource', function ($resource) {
    return $resource('api/Courses/:id', {}, {
        query: { method: "GET", isArray: true },
        create: { method: "POST" },
        get: { method: "GET", url: "/api/Courses?id=:id" },
        remove: { method: "DELETE", url: "/api/Courses?id=:id" },
        update: { method: "PUT", url: "/api/Courses?id=:id" }
    })
}]);

apiServicesApp.factory('Department', ['$resource', function ($resource) {
    return $resource('api/Departments/:id', {}, {
        query: { method: "GET", isArray: true },
        create: { method: "POST" },
        get: { method: "GET", url: "/api/Departments?id=:id" },
        remove: { method: "DELETE", url: "/api/Departments?id=:id" },
        update: { method: "PUT", url: "/api/Departments?id=:id" }
    })
}]);

apiServicesApp.factory('Student', ['$resource', function ($resource) {
    return $resource('api/Students/:id', {}, {
        query: { method: "GET", isArray: true },
        create: { method: "POST" },
        get: { method: "GET", url: "/api/Students?id=:id" },
        remove: { method: "DELETE", url: "/api/Students?id=:id" },
        update: { method: "PUT", url: "/api/Students?id=:id" }
    })
}]);
// above will create the following methods:

//Course.query(); // Will return array of  students
//Course.get({ id: 2 }); // Returns Course with id 2
//Course.create(Course); //Adds new Course entry
//Course.update({ id: 2 }, course); //Updates Course 2 info
//Course.remove({ id: 2 }); //Removes Course with id 2



apiServicesApp.service('popupService', function ($window) {
    this.showPopup = function (message) {
        return $window.confirm(message);
    }
});