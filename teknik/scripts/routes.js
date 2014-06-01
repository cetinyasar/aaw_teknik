define([], function () {
	return {
		defaultRoutePaths: '/ana',
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
			},
			'/ana': {
				templateUrl: '/views/ana.html',
				dependencies: ['ana']
			}

		}
	};
});