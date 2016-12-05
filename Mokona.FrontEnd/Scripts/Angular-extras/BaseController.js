function BaseController($scope) {

    var self = this;

    $scope.errors = null;
    $scope.hasError = function (propertyName)
    {
        return !!$scope.errors
            && !!$scope.errors[propertyName];
    }

    this.addError = function(propertyName, message)
    {
        $scope.errors = $scope.errors || {};
        $scope.errors[propertyName] = message;
    }

    this.showErrors = function (validationResult) {

        $scope.errors = {};
        validationResult.errors.forEach(function (e) {
            var propertyName = toCamelCase(e.propertyName);
            self.addError(propertyName, e.errorMessage);
        });
    }

    this.cleanErrors = function ()
    {
        $scope.errors = null;
    }

    function toCamelCase(propertyName)
    {
        return propertyName.replace(/^\w/gi, function myFunction(x) { return x.toLowerCase(); });
    }
}
