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
				return this.istekGonder("AramaHttpHandler.Ara.teknik", obj);
			}

			this.kriterleriAl = function (obj) {
				return this.istekGonder("Arama.Deneme.teknik", obj);
			}

			this.ayarlariKaydet = function (obj) {
				return this.istekGonder("AyarHttpHandler.AyarlariKaydet.teknik", obj);
			}

			this.indexOlustur = function (obj) {
				return this.istekGonder("AyarHttpHandler.IndexOlustur.teknik", obj);
			}

			this.tumIndexSil = function (obj) {
				return this.istekGonder("AyarHttpHandler.TumIndexSil.teknik", obj);
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

