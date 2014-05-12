using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app_aaw_lib;
using Newtonsoft.Json;
using NUnit.Framework;

namespace app_test
{
    public class AramaKriterleriniElasticSearchJSonOlusturanTest
    {
		[SetUp]
		public void Ayarlar()
		{
		}

		[Test]
		public void HicBirFiltreIcermeyenAramaKriteriGeldiginde_FiltreIcermyenElasticSearchObjesiOlusmali()
		{
			AramaKriterleri ak = AramaKriterleri.Olustur();
			AramaKriterdenElasticSearchOlusturan olusturan = new AramaKriterdenElasticSearchOlusturan();
			ElasticSearchGet esg = olusturan.Olustur(ak);
			Assert.AreEqual(esg.fields.Count, 0);
			Assert.AreEqual(esg.fields.Count, 0);
		}

		[Test]
		public void SadeceKKOPoliceGrubuFiltresiOlanKriterGeldiginde_GetFilterAndTermsPoliceGrubuIcindeBirTaneKKOOlanKayitOlmali()
		{
			AramaKriterleri ak = AramaKriterleri.Olustur();
			ak.SecilebilirKriterler.PoliceGrubu.Add(new Kriter { Adi = "KKO" });
			AramaKriterdenElasticSearchOlusturan olusturan = new AramaKriterdenElasticSearchOlusturan();
			ElasticSearchGet esg = olusturan.Olustur(ak);

			//Assert.AreEqual(1, esg.filter.and.Count);
			//Assert.AreEqual("KKO", esg.filter.and[0].policeGrubu[0]);
		}

		[Test]
		public void KKOVeTRFPoliceGrubuFiltresiOlanKriterGeldiginde_GetFilterAndTermsPoliceGrubuIcindeKKOVeTRFKayitOlmali()
		{
			AramaKriterleri ak = AramaKriterleri.Olustur();
			ak.SecilebilirKriterler.PoliceGrubu.Add(new Kriter { Adi = "KKO" });
			ak.SecilebilirKriterler.PoliceGrubu.Add(new Kriter { Adi = "TRF" });
			AramaKriterdenElasticSearchOlusturan olusturan = new AramaKriterdenElasticSearchOlusturan();
			ElasticSearchGet esg = olusturan.Olustur(ak);

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
			AramaKriterdenElasticSearchOlusturan olusturan = new AramaKriterdenElasticSearchOlusturan();
			ElasticSearchGet esg = olusturan.Olustur(ak);

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
			AramaKriterdenElasticSearchOlusturan olusturan = new AramaKriterdenElasticSearchOlusturan();
			ElasticSearchGet esg = olusturan.Olustur(ak);

			Assert.AreEqual(1, ((EsAnd)(esg.filter.and[0])).range.tanzimTarihi.from.Day);
			Assert.AreEqual(2, ((EsAnd)(esg.filter.and[0])).range.tanzimTarihi.to.Day);
		}

		[Test]
		public void SerbestMetinSorgusuKriterGeldiginde_GetQueryDoluOlmali()
		{
			AramaKriterleri ak = AramaKriterleri.Olustur();
			ak.Query = "query";
			AramaKriterdenElasticSearchOlusturan olusturan = new AramaKriterdenElasticSearchOlusturan();
			ElasticSearchGet esg = olusturan.Olustur(ak);
			Assert.AreEqual("query", esg.query.query_string.query);
		}

		[Test]
		public void From100Size200KriterGeldiginde_GetFrom100Size200Olmali()
		{
			AramaKriterleri ak = AramaKriterleri.Olustur();
			ak.From = 100;
			ak.Size = 200;
			AramaKriterdenElasticSearchOlusturan olusturan = new AramaKriterdenElasticSearchOlusturan();
			ElasticSearchGet esg = olusturan.Olustur(ak);
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

			AramaKriterdenElasticSearchOlusturan olusturan = new AramaKriterdenElasticSearchOlusturan();
			ElasticSearchGet esg = olusturan.Olustur(ak);
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
			ak.SecilebilirKriterler.TanzimTarihAraligi = new BaslangicBitisTarihi { IlkTarih = new DateTime(2000, 1, 1), SonTarih = new DateTime(2014, 12, 31) };

			AramaKriterdenElasticSearchOlusturan olusturan = new AramaKriterdenElasticSearchOlusturan();
			ElasticSearchGet esg = olusturan.Olustur(ak);
			JsonSerializerSettings js = new JsonSerializerSettings();
			js.NullValueHandling = NullValueHandling.Ignore;
			string serializeObject = JsonConvert.SerializeObject(esg, js);
			Assert.AreEqual(serializeObject, "{\"from\":0,\"size\":100,\"query\":{\"query_string\":{\"query\":\"renault\"}},\"filter\":{\"and\":[{\"terms\":{\"policeGrubu\":[\"kko\"]}},{\"terms\":{\"marka\":[\"renault\"]}},{\"range\":{\"tanzimTarihi\":{\"from\":\"2000-01-01T00:00:00\",\"to\":\"2014-12-31T00:00:00\"}}}]}}");
		}

	}

	public class AramaKriterdenElasticSearchOlusturan
	{
		public ElasticSearchGet Olustur(AramaKriterleri ak)
		{
			ElasticSearchGet retVal = new ElasticSearchGet();// ElasticSearchGet.Olustur();

			retVal.from = ak.From;
			retVal.size = ak.Size;
			if (!string.IsNullOrEmpty(ak.Query))
			{
				retVal.query = Query.Olustur();
				retVal.query.query_string.query = ak.Query;
			}

			List<EsAnd> kriterlerindenTermsFiltreleriAl = aramaKriterlerindenTermsFiltreleriAl(ak);
			if (kriterlerindenTermsFiltreleriAl.Count > 0)
			{
				retVal.filter = EsFilter.Olustur();
				retVal.filter.and = kriterlerindenTermsFiltreleriAl;
			}

			retVal.facets = new List<EsFacets>();
			retVal.facets.AddRange(aramaKriterlerindenFacetsAl(ak));
			return retVal;
		}

		private List<EsFacets> aramaKriterlerindenFacetsAl(AramaKriterleri aramaKriterleri)
		{
			List<EsFacets> retVal = new List<EsFacets>();
			EsFacets facet = new EsFacets();
			facet.policeGrubu = new FacetPoliceGrubu();
			facet.policeGrubu.terms = new FacetTerms();
			facet.policeGrubu.terms.field = "policeGrubu";
			facet.policeGrubu.terms.size = 10;

			facet.policeGrubu.facet_filter = new FacetFacetFilter();
			facet.policeGrubu.facet_filter.and = new FacetAnd();
			facet.policeGrubu.facet_filter.and.filters = new List<FacetFilters>();

			FacetFilters fff = new FacetFilters();
			fff.
			
			facet.policeGrubu.facet_filter.and.filters.Add(fff);

			retVal.Add(facet);

			return retVal;
		}

		private List<EsAnd> aramaKriterlerindenTermsFiltreleriAl(AramaKriterleri ak)
		{
			List<EsAnd> esAnd = new List<EsAnd>();

			EsAnd and = new EsAnd() { terms = new TermsPoliceGrubu() { policeGrubu = new List<string>() } };
			foreach (Kriter kr in ak.SecilebilirKriterler.PoliceGrubu)
			{
				((TermsPoliceGrubu)(and.terms)).policeGrubu.Add(kr.Adi);
			}
			if (ak.SecilebilirKriterler.PoliceGrubu.Count > 0)
				esAnd.Add(and);

			EsAnd item = new EsAnd() { terms = new TermsMarka() { marka = new List<string>() } };
			foreach (Kriter kr in ak.SecilebilirKriterler.Marka)
			{
				((TermsMarka)(item.terms)).marka.Add(kr.Adi);
			}

			if (ak.SecilebilirKriterler.Marka.Count > 0)
				esAnd.Add(item);

			if (ak.SecilebilirKriterler.TanzimTarihAraligi.IlkTarih.Year != 1)
			{
				EsAnd rangeAnd = new EsAnd() {range = EsRange.Olustur()};
				rangeAnd.range.tanzimTarihi.from = ak.SecilebilirKriterler.TanzimTarihAraligi.IlkTarih;
				rangeAnd.range.tanzimTarihi.to = ak.SecilebilirKriterler.TanzimTarihAraligi.SonTarih;
				esAnd.Add(rangeAnd);
			}




			return esAnd;
		}

	}

	public class ElasticSearchGet
	{
		public int from { get; set; }
		public int size { get; set; }
		public List<string> fields { get; set; }
		public Query query { get; set; }
		public EsFilter filter { get; set; }
		public List<EsFacets> facets { get; set; }
	}

	#region Facets
	public class EsFacets
	{
		public FacetPoliceGrubu policeGrubu { get; set; }
		public FacetMarka marka { get; set; }
	}

	public class FacetPoliceGrubu
	{
		public FacetTerms terms { get; set; }
		public FacetFacetFilter facet_filter { get; set; }
	}

	public class FacetMarka
	{
		public FacetTerms terms { get; set; }
		public FacetFacetFilter facet_filter { get; set; }
	}

	public class FacetTerms
	{
		public string field { get; set; }
		public int size { get; set; }
	}

	public class FacetFacetFilter
	{
		public FacetAnd and { get; set; }
	}

	public class FacetAnd
	{
		public List<FacetFilters> filters { get; set; }
	}

	public class FacetFilters
	{
		public object terms { get; set; }
	}

	#endregion

	public class QueryString
	{
		public string query { get; set; }

		public static QueryString Olustur()
		{
			QueryString retVal = new QueryString();
			retVal.query = "";
			return retVal;
		}
	}

	public class Query
	{
		public QueryString query_string { get; set; }

		public static Query Olustur()
		{
			Query retVal = new Query();
			retVal.query_string = QueryString.Olustur();
			return retVal;
		}
	}

	public class EsAnd
	{
		public object terms { get; set; }
		public EsRange range { get; set; }
	}

	public abstract class Terms
	{
		public List<string> policeGrubu { get; set; }
		public List<string> marka { get; set; }
	}

	public class TermsPoliceGrubu : Terms
	{
		public static TermsPoliceGrubu Olustur()
		{
			TermsPoliceGrubu retVal = new TermsPoliceGrubu();
			retVal.policeGrubu = new List<string>();
			return retVal;
		}
	}
	public class TermsMarka : Terms
	{
		public static TermsMarka Olustur()
		{
			TermsMarka retVal = new TermsMarka();
			retVal.marka = new List<string>();
			return retVal;
		}
	}

	public class EsFilter
	{
		public List<EsAnd> and { get; set; }
		
		public static EsFilter Olustur()
		{
			EsFilter retVal = new EsFilter();
			retVal.and = new List<EsAnd>();
			return retVal;
		}
	}

	public class EsTanzimTarihi
	{
		public DateTime from { get; set; }
		public DateTime to { get; set; }
	}

	public class EsRange
	{
		public EsTanzimTarihi tanzimTarihi { get; set; }

		public static EsRange Olustur()
		{
			EsRange retVal = new EsRange();
			retVal.tanzimTarihi = new EsTanzimTarihi();
			return retVal;
		}
	}
}
