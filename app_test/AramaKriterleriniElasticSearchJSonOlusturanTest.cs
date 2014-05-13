using System;
using app_aaw_lib;
using app_aaw_lib.EsSearch.Sorgu;
using Newtonsoft.Json;
using NUnit.Framework;

namespace app_test
{
    public class AramaKriterleriniElasticSearchJSonOlusturanTest
    {
		AramaKriterdenElasticSearchOlusturan olusturan;
		[SetUp]
		public void Ayarlar()
		{
		}

		[Test]
		public void HicBirFiltreIcermeyenAramaKriteriGeldiginde_FiltreIcermyenElasticSearchObjesiOlusmali()
		{
			AramaKriterleri ak = AramaKriterleri.Olustur();
			olusturan = new AramaKriterdenElasticSearchOlusturan(ak);
			ElasticSearchGet esg = olusturan.Olustur();
			Assert.AreEqual(esg.filter, null);
			
		}

		[Test]
		public void SadeceKKOPoliceGrubuFiltresiOlanKriterGeldiginde_GetFilterAndTermsPoliceGrubuIcindeBirTaneKKOOlanKayitOlmali()
		{
			AramaKriterleri ak = AramaKriterleri.Olustur();
			ak.SecilebilirKriterler.PoliceGrubu.Add(new Kriter { Adi = "KKO" });
			olusturan = new AramaKriterdenElasticSearchOlusturan(ak);
			ElasticSearchGet esg = olusturan.Olustur();
		}

		[Test]
		public void KKOVeTRFPoliceGrubuFiltresiOlanKriterGeldiginde_GetFilterAndTermsPoliceGrubuIcindeKKOVeTRFKayitOlmali()
		{
			AramaKriterleri ak = AramaKriterleri.Olustur();
			ak.SecilebilirKriterler.PoliceGrubu.Add(new Kriter { Adi = "KKO" });
			ak.SecilebilirKriterler.PoliceGrubu.Add(new Kriter { Adi = "TRF" });
			olusturan = new AramaKriterdenElasticSearchOlusturan(ak);
			ElasticSearchGet esg = olusturan.Olustur();


			Assert.AreEqual(1, esg.filter.and.Count);
			Assert.AreEqual(2, ((TermsPoliceGrubu)(esg.filter.and[0].terms)).policeGrubu.Count);
			Assert.AreEqual("KKO", ((TermsPoliceGrubu)(esg.filter.and[0].terms)).policeGrubu[0]);
			Assert.AreEqual("TRF", ((TermsPoliceGrubu)(esg.filter.and[0].terms)).policeGrubu[1]);
		}

		[Test]
		public void KKOVeTRFPoliceGrubuVeMercedesMarkaFiltresiOlanKriterGeldiginde_GetFilterAndTermsPoliceGrubuIcindeKKOVeTRFVeMarkaIcindeMercedesKayitOlmali()
		{
			AramaKriterleri ak = AramaKriterleri.Olustur();
			ak.SecilebilirKriterler.PoliceGrubu.Add(new Kriter { Adi = "KKO" });
			ak.SecilebilirKriterler.PoliceGrubu.Add(new Kriter { Adi = "TRF" });
			ak.SecilebilirKriterler.Marka.Add(new Kriter { Adi = "mercedes" });
			olusturan = new AramaKriterdenElasticSearchOlusturan(ak);
			ElasticSearchGet esg = olusturan.Olustur();
			
			Assert.AreEqual(2, esg.filter.and.Count);
			Assert.AreEqual(2, ((TermsPoliceGrubu)(esg.filter.and[0].terms)).policeGrubu.Count);
			Assert.AreEqual(1, ((TermsMarka)(esg.filter.and[1].terms)).marka.Count);
			Assert.AreEqual("KKO", ((TermsPoliceGrubu)(esg.filter.and[0].terms)).policeGrubu[0]);
			Assert.AreEqual("TRF", ((TermsPoliceGrubu)(esg.filter.and[0].terms)).policeGrubu[1]);
			Assert.AreEqual("mercedes", ((TermsMarka)(esg.filter.and[1].terms)).marka[0]);
		}

		[Test]
		public void TanzimTarihiOlanKriterGeldiginde_GetFilterAndRangeIcindeTanzimTarihiAraligiDoluOlmali()
		{
			AramaKriterleri ak = AramaKriterleri.Olustur();
			ak.SecilebilirKriterler.TanzimTarihAraligi = new BaslangicBitisTarihi { IlkTarih = new DateTime(2014, 1, 1), SonTarih = new DateTime(2014, 2, 2) };
			olusturan = new AramaKriterdenElasticSearchOlusturan(ak);
			ElasticSearchGet esg = olusturan.Olustur();

			Assert.AreEqual(1, ((EsAnd)(esg.filter.and[0])).range.tanzimTarihi.from.Day);
			Assert.AreEqual(2, ((EsAnd)(esg.filter.and[0])).range.tanzimTarihi.to.Day);
		}

		[Test]
		public void SerbestMetinSorgusuKriterGeldiginde_GetQueryDoluOlmali()
		{
			AramaKriterleri ak = AramaKriterleri.Olustur();
			ak.Query = "query";
			olusturan = new AramaKriterdenElasticSearchOlusturan(ak);
			ElasticSearchGet esg = olusturan.Olustur();

			Assert.AreEqual("query", esg.query.query_string.query);
		}

		[Test]
		public void From100Size200KriterGeldiginde_GetFrom100Size200Olmali()
		{
			AramaKriterleri ak = AramaKriterleri.Olustur();
			ak.From = 100;
			ak.Size = 200;
			olusturan = new AramaKriterdenElasticSearchOlusturan(ak);
			ElasticSearchGet esg = olusturan.Olustur();

			Assert.AreEqual(100, esg.from);
			Assert.AreEqual(200, esg.size);
		}

		[Test]
		public void From0Size100QueryrenaultKriterGeldiginde_DeserializeSonucuDogurOlmali()
		{
			AramaKriterleri ak = AramaKriterleri.Olustur();
			ak.From = 0;
			ak.Size = 200;
			ak.Query = "renault";
			ak.SecilebilirKriterler.PoliceGrubu.Add(new Kriter { Adi = "kko" });
			ak.SecilebilirKriterler.Marka.Add(new Kriter { Adi = "renault" });
			ak.SecilebilirKriterler.TanzimTarihAraligi = new BaslangicBitisTarihi { IlkTarih = new DateTime(2000, 1, 1), SonTarih = new DateTime(2014, 12, 31) };

			olusturan = new AramaKriterdenElasticSearchOlusturan(ak);
			ElasticSearchGet esg = olusturan.Olustur();

			JsonSerializerSettings js = new JsonSerializerSettings();
			js.NullValueHandling = NullValueHandling.Ignore;
			string serializeObject = JsonConvert.SerializeObject(esg, js);
			Assert.AreEqual(serializeObject, "{\"from\":0,\"size\":200,\"query\":{\"query_string\":{\"query\":\"renault\"}},\"filter\":{\"and\":[{\"terms\":{\"policeGrubu\":[\"kko\"]}},{\"terms\":{\"marka\":[\"renault\"]}},{\"range\":{\"tanzimTarihi\":{\"from\":\"2000-01-01T00:00:00\",\"to\":\"2014-12-31T00:00:00\"}}}]}}");
		}

		[Test]
		public void FromSizeGonderilmedigindeQueryrenaultKriterGeldiginde_DeserializeSonucuDogurOlmali()
		{
			AramaKriterleri ak = AramaKriterleri.Olustur();
			ak.Query = "renault";
			ak.SecilebilirKriterler.PoliceGrubu.Add(new Kriter { Adi = "kko" });
			ak.SecilebilirKriterler.Marka.Add(new Kriter { Adi = "renault" });
			ak.SecilebilirKriterler.Marka.Add(new Kriter { Adi = "bmc" });
			ak.SecilebilirKriterler.Brans.Add(new Kriter { Adi = "100" });
			ak.SecilebilirKriterler.Brans.Add(new Kriter { Adi = "200" });
			ak.SecilebilirKriterler.Brans.Add(new Kriter { Adi = "300" });
			ak.SecilebilirKriterler.TanzimTarihAraligi = new BaslangicBitisTarihi { IlkTarih = new DateTime(2000, 1, 1), SonTarih = new DateTime(2014, 12, 31) };

			olusturan = new AramaKriterdenElasticSearchOlusturan(ak);
			ElasticSearchGet esg = olusturan.Olustur();

			JsonSerializerSettings js = new JsonSerializerSettings();
			js.NullValueHandling = NullValueHandling.Ignore;
			string serializeObject = JsonConvert.SerializeObject(esg, js);
			Assert.AreEqual(serializeObject, "{\"from\":0,\"size\":100,\"query\":{\"query_string\":{\"query\":\"renault\"}},\"filter\":{\"and\":[{\"terms\":{\"policeGrubu\":[\"kko\"]}},{\"terms\":{\"marka\":[\"renault\",\"bmc\"]}},{\"terms\":{\"brans\":[\"100\",\"200\",\"300\"]}},{\"range\":{\"tanzimTarihi\":{\"from\":\"2000-01-01T00:00:00\",\"to\":\"2014-12-31T00:00:00\"}}}]},\"facets\":{\"policeGrubu\":{\"terms\":{\"field\":\"policeGrubu\",\"size\":10},\"facet_filter\":{\"and\":{\"filters\":[{\"range\":{\"tanzimTarihi\":{\"from\":\"2000-01-01T00:00:00\",\"to\":\"2014-12-31T00:00:00\"}}},{\"terms\":{\"marka\":[\"renault\",\"bmc\"]}},{\"terms\":{\"brans\":[\"100\",\"200\",\"300\"]}}]}}},\"marka\":{\"terms\":{\"field\":\"marka\",\"size\":10},\"facet_filter\":{\"and\":{\"filters\":[{\"range\":{\"tanzimTarihi\":{\"from\":\"2000-01-01T00:00:00\",\"to\":\"2014-12-31T00:00:00\"}}},{\"terms\":{\"policeGrubu\":[\"kko\"]}},{\"terms\":{\"brans\":[\"100\",\"200\",\"300\"]}}]}}},\"brans\":{\"terms\":{\"field\":\"brans\",\"size\":10},\"facet_filter\":{\"and\":{\"filters\":[{\"range\":{\"tanzimTarihi\":{\"from\":\"2000-01-01T00:00:00\",\"to\":\"2014-12-31T00:00:00\"}}},{\"terms\":{\"marka\":[\"renault\",\"bmc\"]}},{\"terms\":{\"policeGrubu\":[\"kko\"]}}]}}}}}");
		}

		[Test]
		public void tmp()
		{
			AramaKriterleri ak = AramaKriterleri.Olustur();
			ak.Query = "";
			ak.SecilebilirKriterler.PoliceGrubu.Add(new Kriter { Adi = "kko" });
			ak.SecilebilirKriterler.PoliceGrubu.Add(new Kriter { Adi = "trf" });
			ak.SecilebilirKriterler.Marka.Add(new Kriter { Adi = "renault" });
			ak.SecilebilirKriterler.TanzimTarihAraligi = new BaslangicBitisTarihi { IlkTarih = new DateTime(2000, 1, 1), SonTarih = new DateTime(2014, 12, 31) };

			olusturan = new AramaKriterdenElasticSearchOlusturan(ak);
			ElasticSearchGet esg = olusturan.Olustur();

			JsonSerializerSettings js = new JsonSerializerSettings();
			js.NullValueHandling = NullValueHandling.Ignore;
			string serializeObject = JsonConvert.SerializeObject(esg, js);
			Assert.AreEqual(serializeObject, "{\"from\":0,\"size\":100,\"query\":{\"query_string\":{\"query\":\"renault\"}},\"filter\":{\"and\":[{\"terms\":{\"policeGrubu\":[\"kko\"]}},{\"terms\":{\"marka\":[\"renault\",\"bmc\"]}},{\"terms\":{\"brans\":[\"100\",\"200\",\"300\"]}},{\"range\":{\"tanzimTarihi\":{\"from\":\"2000-01-01T00:00:00\",\"to\":\"2014-12-31T00:00:00\"}}}]},\"facets\":{\"policeGrubu\":{\"terms\":{\"field\":\"policeGrubu\",\"size\":10},\"facet_filter\":{\"and\":{\"filters\":[{\"range\":{\"tanzimTarihi\":{\"from\":\"2000-01-01T00:00:00\",\"to\":\"2014-12-31T00:00:00\"}}},{\"terms\":{\"marka\":[\"renault\",\"bmc\"]}},{\"terms\":{\"brans\":[\"100\",\"200\",\"300\"]}}]}}},\"marka\":{\"terms\":{\"field\":\"marka\",\"size\":10},\"facet_filter\":{\"and\":{\"filters\":[{\"range\":{\"tanzimTarihi\":{\"from\":\"2000-01-01T00:00:00\",\"to\":\"2014-12-31T00:00:00\"}}},{\"terms\":{\"policeGrubu\":[\"kko\"]}},{\"terms\":{\"brans\":[\"100\",\"200\",\"300\"]}}]}}},\"brans\":{\"terms\":{\"field\":\"brans\",\"size\":10},\"facet_filter\":{\"and\":{\"filters\":[{\"range\":{\"tanzimTarihi\":{\"from\":\"2000-01-01T00:00:00\",\"to\":\"2014-12-31T00:00:00\"}}},{\"terms\":{\"marka\":[\"renault\",\"bmc\"]}},{\"terms\":{\"policeGrubu\":[\"kko\"]}}]}}}}}");
		}

	}

	#region Facets

	#endregion
}


//private FacetPoliceGrubu policeGrubuFacetOlustur(AramaKriterleri ak)
//{
//	FacetPoliceGrubu facet = new FacetPoliceGrubu();

//	facet.terms = new FacetTerms();
//	facet.terms.field = "policeGrubu";
//	facet.terms.size = 10;

//	facet.facet_filter = new FacetFacetFilter();
//	facet.facet_filter.and = new FacetAnd();
//	facet.facet_filter.and.filters = new List<object>();

//	if (ak.SecilebilirKriterler.TanzimTarihAraligi.IlkTarih.Year != 1)
//		facet.facet_filter.and.filters.Add(tanzimTarihiRangeEkle(ak));

//	facet.facet_filter.and.filters.Add(markaKriterleriniEkle(ak));

//	return facet;
//}