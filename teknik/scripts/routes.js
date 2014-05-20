define([], function () {
	return {
		defaultRoutePaths: '/',
		routes: {
			'/arama': {
				templateUrl: '/views/arama/ara.html',
				dependencies: [
                    'arama/araController'
				]
			},
			'/ayarlar': {
				templateUrl: '/views/ayarlar/ayar.html',
				dependencies: [
                    'ayarlar/ayarController'
				]
			},
			'/police': {
				templateUrl: '/views/police/pol.html',
				dependencies: [
                    'police/polController'
				]
			}
		}
	};
});