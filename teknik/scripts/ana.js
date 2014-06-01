define(['app'], function (app) {
	app.controller('anaController',
    [
		'$scope', 'loginKontrol',
		function ($scope, loginKontrol) {
			//alert("ana");
			var tmp = loginKontrol.sayHello("asasa");
			alert(tmp);
		}
    ]);
});



//aawClientBootstrap();
//function aawClientBootstrap() {
//	ayarlanmadiysaAdaSessionCookieAyarla();
//}

//function ayarlanmadiysaAdaSessionCookieAyarla() {
//	if (docCookies.getItem("adaSessionId") != null) {
//		//session zaten varsa sunucudan login olup olmadığını kontrol et
//		$.post("/SSOHttpHandler.LoginKontrol.teknik", function (response) {
//			var nesne = JSON.parse(response);
//			if (nesne.LoginYapilmis) {
//				window.location = "/arama";
//			}
//			else
//				window.location = nesne.LoginUrl;
//		});
//	}
//	else {
//		$.post("/SSOHttpHandler.SessionBootstrapAyarlariniAl.teknik", function (response) {
//			var nesne = JSON.parse(response);
//			docCookies.setItem("adaSessionId", nesne.SessionKey, false, null, nesne.CookieDomain);
//			window.location = nesne.LoginUrl;//session yeni başlıyorsa login'e yönlendirilmesi gerektiği kesin (beni hatırla varsa bile implementasyonu login sayfasında)
//		});
//	}
//}