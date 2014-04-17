define(['app', 'arama/sunucuAramaIletisim'], function (app)
{
	app.controller('araController',
    [
		'$scope', '$modal', 'sunucuAramaIletisim',
		function ($scope, $modal, sunucuAramaIletisim, filtreServis) {
			$scope.Arama = new Arama();

			$scope.aramaYap = function () {
				sunucuAramaIletisim.veriAl(this).then(function (sonuc) {
					$scope.Arama.ayarla(sonuc[0]);
				});
			}

			$scope.kriterAl = function ()
			{

				var modalInstance = $modal.open({
					templateUrl: '/views/arama/filtre.html',
					scope: $scope
				});
				console.log('modal opened');
				modalInstance.result.then(function () {
					console.log($scope.selected);
				}, function () {
					console.log('Modal dismissed at: ' + new Date());
				});

				//var modalInstance = $modal.open({
				//	templateUrl: '/views/arama/filtre.html',
				//	controller: filtreServis,
				//	resolve: {
				//		items: function () {
				//			return $scope.items;
				//		}
				//	}
				//});

				//modalInstance.result.then(function (selectedItem) {
				//    alert(selectedItem);
			    //}, function () {
				//    alert("dismissed");
			    //});
			}

		}
    ]);

	app.controller('filtreServis',
	[
		'$scope', '$modalInstance',
		function ($scope, $modalInstance) {

			$scope.ok = function () {
				$modalInstance.close("close");
			};

			$scope.cancel = function () {
				$modalInstance.dismiss('cancel');
			};
		}
	]);

});

function Arama()
{

	this.Baslik = "Poliçe Ara";
	this.AramaKriterleri = new AramaKriterleri();
	this.AramaSonuc = new AramaSonuc();
	this.ayarla = function (sonuc)
	{
		this.AramaSonuc = sonuc;
	};
}

function AramaSonuc()
{
	this.hits = new Object();
	this.hits.total = 0;
	this.hits.hits = [];
}

function AramaKriterleri() {
	this.Guvenlik = new Guvenlik();
	this.Tipi = "PoliceArama";
	this.Sorgu = "";
}

function Guvenlik() {
	this.TarihSaat = "";
	this.KullaniciAdi = "";
	this.Parola = "";
}
