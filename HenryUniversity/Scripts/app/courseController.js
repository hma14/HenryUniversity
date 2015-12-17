universityapp.controller('courseCtrl',
    ['$scope', '$location', '$rootScope', 'popupService', 'Course', '$exceptionHandler',
    function ($scope, $location, $rootScope, popupService, Course, $exceptionHandler) {
        var url = 'api/Courses/';
        $scope.courses = [];
        $scope.course = '';
        $scope.error = '';
        $scope.sortField = 'Title';
        $scope.reverse = true;

        $scope.getAll = function () {
            var promise = Course.query();
            promise.$promise.then(function (payload) {
                $scope.courses = payload;
            },
            function (error) {
                $exceptionHandler(error.data.message, error.status + ' - ' + error.statusText);
            });
        };

        $scope.get = function () {
            var promise = Course.get({ id: $scope.CourseID });
            promise.$promise.then(function (payload) {
                $scope.course = payload;
            },
            function (error) {
                $exceptionHandler(error.data.message, error.status + ' - ' + error.statusText);
            });
        };

        $scope.addCourse = function () {
            var promise = Course.create({
                'CourseID': $scope.CourseID,
                'Title': $scope.Title,
                'Credits': $scope.Credits,
                'DepartmentID': $scope.Department.DepartmentID
            });
            promise.$promise.then(function () {
                $location.path('/courses');
            },
            function (error) {
                $exceptionHandler(error.data.message, error.status + ' - ' + error.statusText);
            });
        };

        $scope.editCourse = function (course) {
            $rootScope.CourseID = course.CourseID;
            $rootScope.Title = course.Title;
            $rootScope.Credits = course.Credits;
            $rootScope.DepartmentID = course.DepartmentID;
            saveState = course;
        };

        $scope.updateCourse = function () {
            course = saveState;
            course.CourseID = $scope.CourseID;
            course.Title = $scope.Title;
            course.Credits = $scope.Credits;
            course.DepartmentID = $scope.Department.DepartmentID;
            course.Department = $scope.Department;

            var promise = Course.update({ id: course.CourseID }, course);
            promise.$promise.then(function () {
                $location.path('/courses');
            },
            function (error) {
                $exceptionHandler(error.data.message, error.status + ' - ' + error.statusText);
            });
        };

        $scope.deleteCourse = function (course) {
            if (popupService.showPopup('Really want to delete this?')) {
                var promise = Course.remove({ id: course.CourseID });
                promise.$promise.then(function () {
                    $scope.getAll();
                    $location.path('/courses');
                },
                function (error) {
                    $exceptionHandler(error.data.message, error.status + ' - ' + error.statusText);
                });
            }
        };
    }]);