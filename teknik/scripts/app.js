define(['routes', 'services/dependencyResolverFor'],
	function (config, dependencyResolverFor) {
		var app = angular.module('app', ['ngRoute', 'ui.bootstrap', 'ada.tarih', 'ada.login', 'ada.kriterPaneli']);

	app.config(
    [
        '$routeProvider',
        '$locationProvider',
        '$controllerProvider',
        '$compileProvider',
        '$filterProvider',
        '$provide',


        function ($routeProvider, $locationProvider, $controllerProvider, $compileProvider, $filterProvider, $provide) {
        	app.controller = $controllerProvider.register;
        	app.directive = $compileProvider.directive;
        	app.filter = $filterProvider.register;
        	app.factory = $provide.factory;
        	app.service = $provide.service;

        	$locationProvider.html5Mode(true);
        	if (config.routes !== undefined) {
        		angular.forEach(config.routes, function (route, path) {
        			$routeProvider.when(path, { templateUrl: route.templateUrl, resolve: dependencyResolverFor(route.dependencies) });
        		});
        	}

        	if (config.defaultRoutePaths !== undefined) {
        		$routeProvider.otherwise({ redirectTo: config.defaultRoutePaths });
        	}

			
        }
    ]);

	////Ana Uygulama Controller
	app.controller('mainController', ['$scope', '$rootScope', function ($scope, $rootScope)
	{
		$scope.Menu = [1];
		//alert($scope.Login.LoginYapilmis);
		//$scope.Login = { "LoginYapilmis": true } ;
		//alert($scope.Login.LoginYapilmis);
	}]);

	return app;
});