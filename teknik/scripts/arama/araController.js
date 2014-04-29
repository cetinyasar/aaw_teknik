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
	this.tar = "15.04.2014".OnHanelidenTarihAl();
	this.TanzimTarihiBaslangic = "01.01.2000".OnHanelidenTarihAl();
	this.TanzimTarihiBitis = "21/04/2014".OnHanelidenTarihAl();
	this.BransBaslangic = "100";
	this.BransBitis = "999";
	this.PlakaIlKodu = "";
	this.ModelYili = "";
	this.Marka = "";
	this.Tip = "";
	this.SigortaliIlKodu = "";
}

function Guvenlik() {
	this.TarihSaat = "";
	this.KullaniciAdi = "";
	this.Parola = "";
}
