directives.directive('errorHandler', ["$compile", function ($compile) {
    return {
        restrict: 'A',
        replace: false,
        terminal: true, //this setting is important, see explanation below
        priority: 1000, //this setting is important, see explanation below
        compile: function compile(element, attrs) {
            element.attr('uib-tooltip', '{{' + attrs["errorHandler"] + '}}');
            element.attr('tooltip-placement', 'top');
            element.attr('tooltip-trigger', 'mouseenter');
            element.attr('tooltip-enable', attrs["errorHandler"]);

            var parent = element.parent();
            parent.attr("ng-class", "{ 'has-error': " + attrs["errorHandler"] + ", 'input-group': " + attrs["errorHandler"] + " }");

            element.removeAttr("error-handler"); //remove the attribute to avoid indefinite loop
            element.removeAttr("data-error-handler"); //also remove the same attribute with data- prefix in case users specify data-common-things in the html

            var errorIcon = angular.element('<span class="input-group-addon fa fa-remove" ng-show="' + attrs["errorHandler"] + '"></span>');

            parent.append(errorIcon);

            return {
                pre: function preLink(scope, iElement, iAttrs, controller) { },
                post: function postLink(scope, iElement, iAttrs, controller) {
                    $compile(iElement.parent())(scope);
                }
            };
        }
    };
}]);
