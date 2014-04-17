define([], function ()
{
	return {
		defaultRoutePath: '/',
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
		},
		otherwise:
		{
			redirectTo : '/arama'
		}
	};
});