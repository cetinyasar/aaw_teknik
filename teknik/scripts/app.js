define(['routes', 'services/dependencyResolverFor'],
	function (config, dependencyResolverFor) {
		var app = angular.module('app', ['ngRoute', 'ui.bootstrap', 'ada.tarih', 'ada.auth']);

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

        	//loginKontrol.test();
        }
    ]);

	////Ana Uygulama Controller
	app.controller('mainController', ['$scope', '$rootScope', function ($scope) {
		//Menü
		//alert("main2");
		//$scope.Menu = [];
		//$rootScope.$on('$routeChangeStart', function (event) {
		//    alert("routeChangeStart");
		//    //if (!Auth.isLoggedIn()) {
		//    //	console.log('DENY');
		//    //	event.preventDefault();
		//    //	$location.path('/login');
		//    //}
		//    //else {
		//    //	console.log('ALLOW');
		//    //	$location.path('/home');
		//    //}
		//});


		////Her sayfa deðþitiðinde çalýþýr
		//$scope.$on('$routeChangeSuccess', function () {
		//	$scope.sayfaDegisti();
		//});

		//$scope.sayfaDegisti = function () {
		//	aawClientBootstrap();
		//};

		//var cookieNull = docCookies.getItem("adaSessionId") == null;
		//if (cookieNull) {
		//	alert("cookieNull");
		//	aawClientBootstrap();
		//}
		//else {
		//	alert("not cookieNull");
		//	$("#divMain").show();
		//}

	}]);

	return app;
});