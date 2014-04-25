define([], function () {
	return {
		defaultRoutePath: '/index.html',
		routes: {
			'/arama': {
				templateUrl: '/views/arama/ara.html',
				dependencies: [
                    'arama/araController'
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