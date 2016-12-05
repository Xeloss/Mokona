directives.directive('datePicker', ["$compile", function ($compile) {
    function getIdFor(anElement)
    {
        if (!!anElement[0].id)
            return anElement[0].id;
        else
            return Math.ceil(Math.random() * 100000);
    }

    function getIsOpenAttributFor(anElement)
    {
        return "isOpen_" + getIdFor(anElement);
    }

    function getngModelAttributFor(anElement)
    {   
        return "model_" + getIdFor(anElement);
    }

    return {
        restrict: 'A',
        require: "?ngModel",
        replace: false,
        terminal: true,
        priority: 10000, 
        scope:{
            model: '=ngModel',
            outModel: '='
        },
        compile: function compile(element, attrs, ngModel) {
            
            var isOpen = getIsOpenAttributFor(element);
            var ngModel = getngModelAttributFor(element);

            element.attr('uib-datepicker-popup', '');
            element.attr('on-open-focus', true)
            element.attr('is-open', isOpen);
            element.attr('out-model', attrs['ngModel']);
            element.attr('ng-model', ngModel);
            element.attr('ng-focus', isOpen + ' = true');

            attrs['outModel'] = attrs['ngModel'];
            attrs['ngModel'] = ngModel;

            element.removeAttr("date-picker"); //remove the attribute to avoid indefinite loop

            var calendarIcon = angular.element('<span class="input-group-addon" ng-click="' + isOpen + ' = true"><i class="fa fa-calendar"><i/></span>');
            
            var parent = element.parent();
            parent.prepend(calendarIcon);

            return {
                pre: function preLink(scope, iElement, iAttrs, controller) { },
                post: function postLink(scope, iElement, iAttrs, controller) {
                    $compile(iElement.parent())(scope);

                    scope.$watch('model', function (value) {
                        if (value === undefined)
                            return;

                        if (typeof value.getMonth === 'function') //Check if value is a Date-type object
                            scope.outModel = (value).toISOString();
                        else
                            scope.outModel = value;
                    });

                    scope.$watch('outModel', function (value) {
                        if (value === undefined)
                            return;

                        if (typeof value.getMonth === 'function') //Check if value is a Date-type object
                            scope.model = value;
                        else if (typeof (value) === "string" && !!value)
                            scope.model = new Date(Date.parse(value));
                    });
                }
            };
        }
    };
}]);
