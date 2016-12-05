var sevices = angular.module('services', []);
var directives = angular.module('directive', []);
var app = angular.module('MokonaApp', ['services', 'directive', 'ngRoute', 'ngCookies', 'ui.bootstrap', 'angular-loading-bar', 'ngAnimate']);