angular.module('ada.login', [])
	.service('loginKontrol', [
		'$http', '$q', '$location', '$rootScope', function ($http, $q, $location, $rootScope)
		{
			this.http = $http;
			var erteleme = $q;
			return {
				loginMi: function(yonlenecekUrl)
				{
					if (docCookies.getItem("adaSessionId") != null)
					{
						this.istekGonder("/SSOHttpHandler.LoginKontrol.teknik").then(function(loginSonuc)
						{
							if (loginSonuc.LoginYapilmis)
							{
								//alert();
								$rootScope.Login = loginSonuc;
								$("#divMain").show();
								if (yonlenecekUrl != "")
									$location.path(yonlenecekUrl);
								else
									$location.path();
							}
							else
								window.location = loginSonuc.LoginUrl + $location.path();
						});
					} else
					{
						this.istekGonder("/SSOHttpHandler.SessionBootstrapAyarlariniAl.teknik").then(function (loginSonuc) {
							docCookies.setItem("adaSessionId", loginSonuc.SessionKey, false, null, loginSonuc.CookieDomain);
							window.location = loginSonuc.LoginUrl + $location.path();
						});
					}
					return "";
				},

				istekGonder: function(istekTipi, veri)
				{
					var kilit = erteleme.defer();
					$http.post(istekTipi, sunucuyaGondermedenOnceIsle(veri)).success(function(data)
					{
						kilit.resolve(sunucudanAldiginVeriyiIsle(data));
					}).error(function(errorData) {});
					return kilit.promise;
				}

			}
		}
	]);
