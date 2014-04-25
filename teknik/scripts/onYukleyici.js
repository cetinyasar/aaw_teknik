require.config({
	baseUrl: 'scripts',
	paths: {
		'angular': '/lib/angular/angular',
		'angular-route': '/lib/angular/angular-route/angular-route',
		'angular-resource': '/lib/angular/angular-resource/angular-resource',
		'angular-ui': '/lib/angular/ui-bootstrap-tpls-0.10.0',
		//clipone tema için gerekli olanlar
		'clipone-jquery': '/assets/plugins/jquery/2.1.0/jquery.min',
		'clipone-jquery-ui': '/assets/plugins/jquery-ui/jquery-ui-1.10.2.custom.min',
		'clipone-bootstrap': '/assets/plugins/bootstrap/js/bootstrap.min',
		'clipone-bootstrap-hover-dropdown': '/assets/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min',
		'clipone-jquery-blockUI': '/assets/plugins/blockUI/jquery.blockUI',
		'clipone-jquery-iCheck': '/assets/plugins/iCheck/jquery.icheck.min',
		'clipone-jquery-perfect-scrollbar-mousewheel': '/assets/plugins/perfect-scrollbar/src/jquery.mousewheel',
		'clipone-jquery-perfect-scrollbar': '/assets/plugins/perfect-scrollbar/src/perfect-scrollbar',
		//'clipone-less': '/assets/plugins/less/less-1.5.0.min',
		'clipone-jquery-cookie': '/assets/plugins/jquery-cookie/jquery.cookie',
		'clipone-bootstrap-colorpalette': '/assets/plugins/bootstrap-colorpalette/js/bootstrap-colorpalette',
		'clipone-main': '/assets/js/main'

	},
	shim: {
		'app': {
			deps: ['angular', 'angular-route', 'angular-resource', 'angular-ui', 'clipone-main']
		},
		'angular-route': {
			deps: ['angular']
		},
		'angular-resource': {
			deps: ['angular']
		},
		'angular-ui': {
			deps: ['angular']
		},
		'clipone-main': {
			deps: ['angular', 'clipone-jquery', 'clipone-jquery-ui', 'clipone-bootstrap', 'clipone-bootstrap-hover-dropdown', 'clipone-jquery-blockUI', 'clipone-jquery-iCheck',
			'clipone-jquery-perfect-scrollbar-mousewheel', 'clipone-jquery-perfect-scrollbar', 'clipone-jquery-cookie', 'clipone-bootstrap-colorpalette']
			
		},
		//'clipone-main': {
		//	deps: ['angular', 'clipone-jquery', 'clipone-jquery-ui', 'clipone-bootstrap', 'clipone-bootstrap-hover-dropdown', 'clipone-jquery-blockUI', 'clipone-jquery-iCheck',
		//	'clipone-jquery-perfect-scrollbar-mousewheel', 'clipone-jquery-perfect-scrollbar', 'clipone-jquery-cookie', 'clipone-bootstrap-colorpalette']
		//},
		'clipone-jquery-ui': {
			deps: ['clipone-jquery']
		},
		'clipone-bootstrap': {
			deps: ['clipone-jquery']
		},
		'clipone-bootstrap-hover-dropdown': {
			deps: ['clipone-jquery']
		},
		'clipone-jquery-iCheck': {
			deps: ['clipone-jquery']
		},
		'clipone-jquery-perfect-scrollbar': {
			deps: ['clipone-jquery']
		},
		'clipone-bootstrap-colorpalette': {
			deps: ['clipone-jquery']
		},
		'clipone-jquery-perfect-scrollbar-mousewheel': {
			deps: ['clipone-jquery']
		},
		'clipone-jquery-blockUI': {
			deps: ['clipone-jquery']
		},
		'clipone-jquery-cookie': {
			deps: ['clipone-jquery']
		}
		
	}
});

//require.config({
//	baseUrl: 'scripts',
//	paths: {
//		'angular': '/lib/angular/angular',
//		'angular-route': '/lib/angular/angular-route/angular-route',
//		'angular-resource': '/lib/angular/angular-resource/angular-resource',
//		'angular-ui': '/lib/angular/ui-bootstrap-tpls-0.10.0'
//	},
//	shim: {
//		'app': {
//			deps: ['angular', 'angular-route', 'angular-resource', 'angular-ui']
//		},
//		'angular-route': {
//			deps: ['angular']
//		},
//		'angular-resource': {
//			deps: ['angular']
//		},
//		'angular-ui': {
//			deps: ['angular']
//		}
//	}
//});

require
(
    [
        'app'
    ],
    function (app) {
    	angular.bootstrap(document, ['app']);
    	jQuery(document).ready(function () {
    		Main.init();
    	});

    }
);