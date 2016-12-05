(function () {
    app.config(["$routeProvider", function ($routeProvider) {

        var root = '/View/';

        $routeProvider.when('/', {
            templateUrl: root + 'Dashboard',
            controller: 'dashboardIndexController',
            reloadOnSearch: false
        })
        .when('/Settings/Company', {
            templateUrl: root + 'Company',
            controller: 'companyController'
        })
        .when('/Settings/Users', {
            templateUrl: root + 'User',
            controller: 'userIndexController'
        });
    }]);
})();