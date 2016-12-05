sevices.service('companyService', ['$http', '$q',
    function ($http, $q) {
        BaseResourceService.call(this, 'Company', $http, $q);
    }
]);
