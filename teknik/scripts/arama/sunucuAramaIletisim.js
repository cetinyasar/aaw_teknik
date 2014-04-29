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
				return this.istekGonder("raporAl.ada", obj);
			}

			this.kriterleriAl = function (obj) {
				return this.istekGonder("kriterleriAl.ada", obj);
			}

			this.istekGonder = function(istekTipi, veri) {
				var kilit = erteleme.defer();
				this.http.post(istekTipi, sunucuyaGondermedenOnceIsle(veri)).success(function (data) {
					kilit.resolve(sunucudanAldiginVeriyiIsle(data));
				}).error(function (errorData) { });
				return kilit.promise;
			}
			
		}
	]);
});

