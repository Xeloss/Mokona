
(function () {

    app.controller('languageController', ["$scope", "$cookies", "$window", LanguageController]);

    function LanguageController($scope, $cookies, $window) {

        $scope.state = {
            currentLanguage: 'en'
        }

        $scope.events = {
            changeLanguage: changeLanguage
        };

        //Events
        function changeLanguage(language) {
            $cookies.put("_culture", language, { path: '/' });
            location.reload();
        }

        //Private Methods
        function checkCurrentLanguage() {
            var lang = $cookies.get("_culture");
            if (!!lang) {
                $scope.state.currentLanguage = lang;
                return;
            }

            lang = $window.navigator.language || $window.navigator.userLanguage;
            if (!!lang)
                lang = lang.split("-")[0];

            if (lang == 'es' || lang == 'en') {
                $cookies.put("_culture", lang, { path: '/' });
                $scope.state.currentLanguage = lang;
            }
        }

        //Page Setup
        checkCurrentLanguage();
    }

})();