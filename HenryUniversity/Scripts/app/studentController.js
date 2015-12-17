
universityapp.controller('studentCtrl',
    ['$scope', '$location', '$rootScope', 'popupService', 'Student','$exceptionHandler',
    function ($scope, $location, $rootScope, popupService, Student, $exceptionHandler) {
    var url = 'api/Students/';
    $scope.students = [];
    $scope.sortField = 'EnrollmentDate';
    $scope.reverse = true;

    $scope.getAll = function () {
        var promise = Student.query();
        promise.$promise.then(function (payload) {
            $scope.students = payload;
        },
        function (error) {
            $exceptionHandler(error.data.message, error.status + ' - ' + error.statusText);
        });
    };

    $scope.addStudent = function () {
        var promise = Student.create({
                'FirstMidName': $scope.FName,
                'LastName': $scope.LName,
                'EnrollmentDate': $scope.EnrollDate
            });
        promise.$promise.then(function () {
            $location.path('/students');
        },
        function (error) {
            $exceptionHandler(error.data.message, error.status + ' - ' + error.statusText);
        });
    };

    $scope.deleteStudent = function (student) {
        if (popupService.showPopup('Really want to delete this?')) {
            var promise = Student.remove({ id: student.ID });
            promise.$promise.then(function () {
                $scope.getAll();
                $location.path('/students');
            },
            function (error) {
                $exceptionHandler(error.data.message, error.status + ' - ' + error.statusText);
            });
        }
    };

    $scope.editStudent = function (student) {
        $rootScope.LastName = student.LastName;
        $rootScope.FirstMidName = student.FirstMidName;
        $rootScope.EnrollmentDate = student.EnrollmentDate.split('T')[0]; // convert to yyyy-MM-dd
        saveState = student;
    };

    $scope.updateStudent = function () {
        student = saveState;
        student.FirstMidName = $scope.FirstMidName;
        student.LastName = $scope.LastName;
        student.EnrollmentDate = $scope.EnrollmentDate;
        var promise = Student.update({ id: student.ID }, student);
        promise.$promise.then(function () {
            $location.path('/students');
        },
        function (error) {
            $exceptionHandler(error.data.message, error.status + ' - ' + error.statusText);
        });
    };
}]);