define(['app'], function (app) {

	app.service('loginKontrol',
	[
		'$http', '$q',
		function ($http, $q) {
			this.http = $http;
			var erteleme = $q;
			this.veriAl = function (obj) {
				return this.istekGonder("AramaHttpHandler.Ara.teknik", obj);
			}

			this.kriterleriAl = function (obj) {
				return this.istekGonder("Arama.Deneme.teknik", obj);
			}

		}
	]);

	//app.factory('loginKontrol',
    //[
	//	function () {
	//		return {
	//			deneme : function() {
	//				alert("deneme");
	//			},
	//			deneme2 : function() {
	//				alert("deneme2");
	//			}
	//		};

	//	}
    //]);
});
