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
				sunucuAramaIletisim.veriAl(this.Arama.Kriterler).then(function (aramaSonucu) {
					$scope.Arama.ayarla(aramaSonucu);
				});
			}

			$scope.yeniSecilen = function(secilen) {
				//secilen.secili = !secilen.secili;
				var sk = new Kriter();
				sk.Secili = secilen.Secili;
				sk.Adi = secilen.Adi;
				sk.Adet = secilen.Adet;
				//$scope.Arama.Kriterler.SeciliKriterler.PoliceGrubu[sk.Adi] = sk;
				$scope.Arama.Kriterler.SeciliKriterler.PoliceGrubu.push(sk);
			}
		}
    ]);
});

function Arama()
{
	this.VeriAliniyor = false;
	this.Kriterler = new AramaKriterleri();
	this.AramaSonuc = new AramaSonuc();
	
	this.ayarla = function (aramaSonucu) {
		
		this.AramaSonuc = aramaSonucu.Sonuc[0];
		this.Kriterler = aramaSonucu.Kriterler;
		//örnek: key == "policeGrubu"
		this.Kriterler.SecilebilirKriterler.PoliceGrubu = [];
		for (var key in this.AramaSonuc.facets)
		{
			
			if (isUndefined(this.AramaSonuc.facets) || this.AramaSonuc.facets == null)
				continue;
			if (!this.AramaSonuc.facets.hasOwnProperty(key))
				continue;
			
			var value = this.AramaSonuc.facets[key];
			//örnek value.terms == array içinde term = "kko" ve count = 111, term = "trf" ve count = 456 gibi bilgi var
			
			for (var i = 0; i < value.terms.length; i++)
			{
				var sk = new Kriter();
				sk.Secili = false;
				sk.Adi = value.terms[i].term;
				sk.Adet = value.terms[i].count;
				if (key == "policeGrubu")
				{
					//this.Kriterler.SecilebilirKriterler.PoliceGrubu[sk.Adi] = sk;
					this.Kriterler.SecilebilirKriterler.PoliceGrubu.push(sk);
				}
			}
		}
		debugger;
		this.VeriAliniyor = false;
	};
}

function AramaSonuc() {
	this.hits = new Object();
	this.hits.total = 0;
	this.hits.hits = [];
}

function AramaKriterleri()
{
	this.Query = "";
	this.SecilebilirKriterler = new SecilebilirKriterler();
	this.SeciliKriterler = new SecilebilirKriterler();
}

function SecilebilirKriterler()
{
	this.PoliceGrubu = [];

	this.Brans = [];
	this.TaliAdiAcik = [];
	this.SaticiAdiAcik = [];
	this.Marka = [];
}

function Kriter() {
	this.Secili = false;
	this.Adi = "";
	this.Adet = 0;
}


function Guvenlik() {
	this.TarihSaat = "";
	this.KullaniciAdi = "";
	this.Parola = "";
}
