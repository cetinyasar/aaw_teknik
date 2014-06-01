angular.module('ada.auth', [])
	.factory('loginKontrol', function () {
		return {
			sayHello: function (text) {
				return "Factory says \"Hello " + text + "\"";
			}
		}
	});
