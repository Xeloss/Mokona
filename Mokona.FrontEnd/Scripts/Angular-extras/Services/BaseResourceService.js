//This is a base "class" intended to be inherited by an entity service.
function BaseResourceService(resourceName, $http, $q) {
    var self = this;

    this.resourceName = resourceName;
    this.apiRoute = '/api/' + this.resourceName + '/';

    this.get = function (id) {
        var deferred = $q.defer();
        var entityId = !!id ? id : '';

        $http.get(this.apiRoute + entityId)
             .then(function onSuccess(response) {

                 self.onAfterGet(response.data);

                 deferred.resolve(response.data);
             }, function error(response) {
                 deferred.reject(response);
             });

        return deferred.promise;
    }

    this.getWhere = function (query) {
        var deferred = $q.defer();
        var inlineCount = !!query.inlineCount ? "&$inlinecount=AllPages" : '';
        var filter = !!query.filter ? "&$filter=" + query.filter : '';
        var order = !!query.orderBy ? "&$orderby=" + query.orderBy : '';
        var skip = !!query.skip ? "&$skip=" + query.skip : '';
        var top = (query.top == 0 || !!query.top) ? "&$top=" + query.top : '';
        var expand = !!query.expand ? "&$expand=" + query.expand : '';

        $http.get(this.apiRoute + "?n" + inlineCount + filter + order + skip + top + expand)
             .then(function onSuccess(response) {

                 self.onAfterGet(response.data);

                 deferred.resolve(response.data);
             }, function error(response) {
                 deferred.reject(response);
             });

        return deferred.promise;
    }

    this.post = function (entity) {
        var deferred = $q.defer();

        $http.post(this.apiRoute, entity)
             .then(function success(response) {
                 deferred.resolve(response.data);
             }, function error(response) {
                 deferred.reject(response);
             });

        return deferred.promise;
    }

    this.put = function (entity) {
        var deferred = $q.defer();

        $http.put(this.apiRoute, entity)
             .then(function success(response) {
                 deferred.resolve(response.data);
             }, function error(response) {
                 deferred.reject(response);
             });

        return deferred.promise;
    }

    this.delete = function (id) {
        var deferred = $q.defer();
        var entityId = !!id ? id : '';

        $http.delete(this.apiRoute + entityId)
             .then(function onSuccess(response) {
                 deferred.resolve(response.data);
             }, function error(response) {
                 deferred.reject(response);
             });

        return deferred.promise;
    }

    //EL proposito de este metodo es ser sobreescrito en los service para
    //para transformar los datos que devuelve el servidor antes de que se
    //los devuelva al que invoco el get.
    this.onAfterGet = function (data) { }
}
