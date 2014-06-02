define(['app'], function(app)
{
	app.service('sunucuIletisim',
	[
		'$http', '$q', 'loginKontrol',
		function($http, $q, loginKontrol)
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
					var islenmisVeri = sunucudanAldiginVeriyiIsle(data);
					
					if (islenmisVeri["ExceptionVar"]) {
						if (islenmisVeri.Mesaj.indexOf("Bu handler metodu çalıştırılamaz (Login olmalısınız)") > -1) {
							loginKontrol.loginMi("");
						}
					}
					kilit.resolve(islenmisVeri);
				}).error(function (errorData) { });
				return kilit.promise;
			}
			
		}
	]);
});

