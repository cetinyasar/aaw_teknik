define(['app'], function (app) {
	app.controller('anaController',
    [
		'$scope', 'loginKontrol',
		function ($scope, loginKontrol)
		{
			loginKontrol.loginMi("/arama");

		}
    ]);
});