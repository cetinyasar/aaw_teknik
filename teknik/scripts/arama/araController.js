define(['app', 'arama/sunucuAramaIletisim'], function (app)
{
	app.controller('araController',
    [
		'$scope', '$modal', 'sunucuAramaIletisim', 
		function ($scope, $modal, sunucuAramaIletisim) {
			$scope.Arama = new Arama();

			$scope.aramaYap = function ()
			{
				$scope.Arama.VeriAliniyor = true;
				sunucuAramaIletisim.veriAl(this.Arama).then(function (sonuc) {
					$scope.Arama.ayarla(sonuc[0]);
				});
			}

			$scope.yeniSecilen = function(secimAlani, secilen) {
				$scope.Arama.Kriterler.SeciliKriter.policeGrubu.push(secilen);
			}
		}
    ]);
});

function Arama()
{

	this.Baslik = "Poliçe Ara";
	this.VeriAliniyor = false;
	this.Kriterler = new AramaKriterleri();
	this.AramaSonuc = new AramaSonuc();
	this.ayarla = function (sonuc)
	{
		this.AramaSonuc = sonuc;
		this.VeriAliniyor = false;
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
	this.SeciliKriter = new SeciliKriter();
}

function SeciliKriter() {
	this.policeGrubu = [];
	this.brans = [];
	this.taliAdiAcik = [];
	this.saticiAdiAcik = [];
	this.marka = [];
}


function Guvenlik() {
	this.TarihSaat = "";
	this.KullaniciAdi = "";
	this.Parola = "";
}
