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
				sunucuAramaIletisim.veriAl(this.Arama).then(function (aramaSonucu) {
					$scope.Arama.ayarla(aramaSonucu);
				});
			}

			$scope.yeniSecilen = function(secilen) {
				secilen.secili = !secilen.secili;
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
				var sk = new SecilebilirKriter();
				sk.secili = false;
				sk.adi = value.terms[i].term;
				sk.adet = value.terms[i].count;

				if (key == "policeGrubu")
					this.Kriterler.SecilebilirKriterler.PoliceGrubu.push(sk);
			}
		}
		
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
	this.PoliceGrubu = [];
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
