define(['app', 'arama/sunucuIletisim'], function (app) {
	app.controller('ayarController',
    [
		'$scope', 'sunucuIletisim', 'loginKontrol',
		function ($scope, sunucuIletisim, loginKontrol) {
			loginKontrol.loginMi("");

			$scope.SunucuIletisimiYapiliyor = false;
			$scope.Ayarlar = new Ayarlar();
			$scope.IndexOlusturmaKriterleri = new IndexOlusturmaKriterleri();

			$scope.aramaAyarlariKaydet = function () {
				$scope.SunucuIletisimiYapiliyor = true;
				sunucuIletisim.ayarlariKaydet(this.Ayarlar).then(function (indexlemeSonucu) {
					$scope.SunucuIletisimiYapiliyor = false;
					alert(indexlemeSonucu);
				});
			}

			$scope.indexleriYenidenOlustur = function() {
				$scope.SunucuIletisimiYapiliyor = true;
				sunucuIletisim.indexOlustur(this.IndexOlusturmaKriterleri).then(function (indexlemeSonucu) {
					$scope.SunucuIletisimiYapiliyor = false;
					alert(indexlemeSonucu);
				});

			}

			$scope.tumIndexleriSil = function () {
				$scope.SunucuIletisimiYapiliyor = true;
				sunucuIletisim.tumIndexSil(this.IndexOlusturmaKriterleri).then(function (indexlemeSonucu) {
					$scope.SunucuIletisimiYapiliyor = false;
					alert(JSON.parse(indexlemeSonucu));
				});
			}
		}

    ]);
});

function IndexOlusturmaKriterleri() {
	this.BaslangicTanzimTarihi = "01.01.2014";
	this.BitisTanzimTarihi = "31.01.2014";
}

function Ayarlar() {
	
	this.AramaSunucuAdresi = "http://localhost:9200";
	this.SayfadaGosterilecekSonucAdedi = 200;
	this.ListelenecekKolonlar = "Poliçe No; Tanzim Tarihi";
	this.GruplardaGosterilecekAdet = 100;
}
