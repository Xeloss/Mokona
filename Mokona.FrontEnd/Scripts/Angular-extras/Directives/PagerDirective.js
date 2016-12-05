directives.directive('pager', function () {
    
    function link(scope)
    {
        function onPageClicked(page) {
            if (page < 0 || page >= scope.state.pageCount || page == scope.state.currentPage)
                return;

            scope.state.currentPage = page;
            scope.onPageChange(page);
        }

        function getPageNumbers(currentPage) {
            var result = [];
            for (var i = -4; i <= 4; i++) {
                var pageNumber = currentPage + i;

                if (pageNumber >= scope.state.pageCount)
                    break;

                if (pageNumber >= 0)
                    result.push(pageNumber);
            }

            return result;
        }

        scope.methods = {
            getPageNumbers: getPageNumbers
        };
        scope.events = {
            onPageClicked: onPageClicked
        };
    }


    return {
        restrict: 'E',
        require: "?ngModel",
        scope:{
            state: '=ngModel',
            onPageChange: '='
        },
        templateUrl: '/View/Shared/Pager',
        link: link
    };
});
