define(['app'], function(app)
{
	app.service('sunucuAramaIletisim',
	[
		'$http', '$q',
		function($http, $q)
		{
			this.http = $http;
			var erteleme = $q;
			this.veriAl = function(obj)
			{
				var kilit = erteleme.defer();
				this.http.post("raporAl.ada", sunucuyaGondermedenOnceIsle(obj)).success(function(data)
				{
					kilit.resolve(sunucudanAldiginVeriyiIsledata);
				}).error(function (errorData) { });
				return kilit.promise;
			}
		}
	]);
});

