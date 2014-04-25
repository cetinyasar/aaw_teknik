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
				sunucuAramaIletisim.veriAl(this).then(function (sonuc) {
					$scope.Arama.ayarla(sonuc[0]);
					
				});
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
	this.TanzimTarihiBaslangic = "01/01/2014";
	this.TanzimTarihiBitis = "21/04/2014";
	this.BransBaslangic = "100";
	this.BransBitis = "999";
	this.PlakaIlKodu = "034";
	this.ModelYili = "2013";
	this.Marka = "MERCEDES";
	this.Tip = "";
	this.SigortaliIlKodu = "016";
}

function Guvenlik() {
	this.TarihSaat = "";
	this.KullaniciAdi = "";
	this.Parola = "";
}
