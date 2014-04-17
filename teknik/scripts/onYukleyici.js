require.config({
	baseUrl: 'scripts',
	paths: {
		'angular': '/lib/angular/angular',
		'angular-route': '/lib/angular/angular-route/angular-route',
		'angular-resource': '/lib/angular/angular-resource/angular-resource',
		'angular-ui': '/lib/angular/ui-bootstrap-tpls-0.10.0'
	},
	shim: {
		'app': {
			deps: ['angular', 'angular-route', 'angular-resource', 'angular-ui']
		},
		'angular-route': {
			deps: ['angular']
		},
		'angular-resource': {
			deps: ['angular']
		},
		'angular-ui': {
			deps: ['angular']
		}
	}
});
require
(
    [
        'app'
    ],
    function (app) {
    	angular.bootstrap(document, ['app']);
    }
);