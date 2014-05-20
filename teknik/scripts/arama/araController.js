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
				$scope.aramaYap();
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
		//this.AramaSonuc = aramaSonucu.Sonuc[0];
		this.AramaSonuc = aramaSonucu.Sonuc;
		this.Kriterler = aramaSonucu.Kriterler;
		this.Kriterler.EsSearch = aramaSonucu.EsSearch;
		if (this.Kriterler.SecilebilirKriterler.PoliceGrubu.length == 0) 
			this.ilkCalistirmaIcinKriterleriAyarla();
		else
			this.ikinciCalistirmaIcinKriterleriAyarla();

		this.VeriAliniyor = false;
	};

	this.ilkCalistirmaIcinKriterleriAyarla = function ()
	{
		for (var key in this.AramaSonuc.facets) {
			if (isUndefined(this.AramaSonuc.facets) || this.AramaSonuc.facets == null)
				continue;
			if (!this.AramaSonuc.facets.hasOwnProperty(key))
				continue;

			var value = this.AramaSonuc.facets[key];
			//örnek value.terms == array içinde term = "kko" ve count = 111, term = "trf" ve count = 456 gibi bilgi var
			for (var i = 0; i < value.terms.length; i++) {
				var sk = new Kriter();
				sk.Secili = false;
				sk.Adi = value.terms[i].term;
				sk.Adet = value.terms[i].count;
				if (key == "policeGrubu")
					this.Kriterler.SecilebilirKriterler.PoliceGrubu.push(sk);
				if (key == "brans")
					this.Kriterler.SecilebilirKriterler.Brans.push(sk);
				if (key == "marka")
					this.Kriterler.SecilebilirKriterler.Marka.push(sk);
				if (key == "modelYili")
					this.Kriterler.SecilebilirKriterler.ModelYili.push(sk);
				if (key == "tali")
					this.Kriterler.SecilebilirKriterler.Tali.push(sk);
				if (key == "satici")
					this.Kriterler.SecilebilirKriterler.Satici.push(sk);
				if (key == "sorumlu")
					this.Kriterler.SecilebilirKriterler.Sorumlu.push(sk);

			}
		}
	};

	this.ikinciCalistirmaIcinKriterleriAyarla = function () {
		for (var key in this.AramaSonuc.facets) {
			if (isUndefined(this.AramaSonuc.facets) || this.AramaSonuc.facets == null)
				continue;
			if (!this.AramaSonuc.facets.hasOwnProperty(key))
				continue;

			var value = this.AramaSonuc.facets[key];
			for (var i = 0; i < value.terms.length; i++) {
				var sk = new Kriter();
				sk.Secili = false;
				sk.Adi = value.terms[i].term;
				sk.Adet = value.terms[i].count;
				if (key == "policeGrubu") {
					this.Kriterler.SecilebilirKriterler.PoliceGrubu.push(sk);
					this.Kriterler.SecilebilirKriterler.PoliceGrubu.filter(function(elem) {
						if (elem.Adi == sk.Adi) {
							elem.Adet = sk.Adet;
						}
						}
					);
				}
				if (key == "brans")
					this.Kriterler.SecilebilirKriterler.Brans.push(sk);
				if (key == "marka")
					this.Kriterler.SecilebilirKriterler.Marka.push(sk);
				if (key == "modelYili")
					this.Kriterler.SecilebilirKriterler.ModelYili.push(sk);
				if (key == "tali")
					this.Kriterler.SecilebilirKriterler.Tali.push(sk);
				if (key == "satici")
					this.Kriterler.SecilebilirKriterler.Satici.push(sk);
				if (key == "sorumlu")
					this.Kriterler.SecilebilirKriterler.Sorumlu.push(sk);

				
			}
		}
	};

}

function AramaSonuc() {
	this.hits = new Object();
	this.hits.total = 0;
	this.hits.hits = [];
}

function AramaKriterleri()
{
	this.Query = "İNTEGRAL";
	this.EsSearch = "YOK";
	this.SecilebilirKriterler = new SecilebilirKriterler();
}

function SecilebilirKriterler()
{
	this.PoliceGrubu = [];
	this.Brans = [];
	this.Marka = [];
	this.ModelYili = [];
	this.Tali = [];
	this.Satici = [];
	this.Sorumlu = [];
	
}

function Kriter() {
	this.Secili = false;
	this.Adi = "";
	this.Adet = 0;
}
