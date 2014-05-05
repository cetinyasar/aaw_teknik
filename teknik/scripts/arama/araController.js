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

			$scope.yeniSecilen = function(secilen) {
				secilen.secili = !secil.secili;
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
		this.Kriterler = new AramaKriterleri();

		//örnek: key == "policeGrubu"    
		for (var key in sonuc.facets)
		{
			if (isUndefined(sonuc.facets) || sonuc.facets == null)
				continue;
			if (!sonuc.facets.hasOwnProperty(key))
				continue;
			var value = sonuc.facets[key];
			//örnek value.terms == array içinde term = "kko" ve count = 111, term = "trf" ve count = 456 gibi bilgi var
			for (var i = 0; i < value.terms.length; i++)
			{
				if (key == "policeGrubu")
				{
					var sk = new SecilebilirKriter();
					sk.secili = false;
					sk.adi = value.terms[i].term;
					sk.adet = value.terms[i].count;
					this.Kriterler.policeGrubu.push(sk);
				}
			}
		}
		this.VeriAliniyor = false;
	};
}

function AramaSonuc()
{
	this.hits = new Object();
	this.hits.total = 0;
	this.hits.hits = [];
}

function AramaKriterleri()
{
	this.policeGrubu = [];
}

function SecilebilirKriter()
{
	this.secili = false;
	this.adi = "";
	this.adet = 0;
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
