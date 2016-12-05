directives.directive('expenseDetail', ['eMokonaervice', 'costCenterService', 'expenseTypeService', 'documentTypeService',
    function (eMokonaervice, costCenterService, expenseTypeService, documentTypeService) {
    
    function link(scope)    
    {
        scope.data = {};
        scope.data.costCenters = [];
        scope.data.expenseTypes = [];
        scope.data.documentTypes = [];

        // Events
        scope.events = {
            onSave: onSave
        };
        function onSave(expense)
        {
            eMokonaervice.put(expense)
                          .then();
        }

        // Private Methods
        function fetchCostCenters()
        {
            costCenterService.get()
                             .then(function (costCenters) {
                                 scope.data.costCenters = costCenters.values;
                             })
        }
        function fetchEMokonaTypes()
        {
            expenseTypeService.get()
                              .then(function (expenseTypes) {
                                  scope.data.expenseTypes = expenseTypes.values;
                              });
        }
        function fetchDocumentTypes()
        {
            documentTypeService.get()
                               .then(function (documentTypes) {
                                   scope.data.documentTypes = documentTypes.values;
                               });
        }

        fetchCostCenters();
        fetchEMokonaTypes();
        fetchDocumentTypes();
    }

    return {
        restrict: 'E',
        require: "?ngModel",
        scope:{
            expense: '=ngModel'
        },
        templateUrl: '/View/Expense/Detail',
        link: link
    };
}]);
