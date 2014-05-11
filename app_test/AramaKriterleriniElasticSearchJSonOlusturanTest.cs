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

		//[Test]
		//public void SadeceKKOPoliceGrubuFiltresiOlanKriterGeldiginde_GetFilterAndTermsPoliceGrubuIcindeBirTaneKKOOlanKayitOlmali()
		//{
		//	AramaKriterleri ak = AramaKriterleri.Olustur();
		//	ak.SecilebilirKriterler.PoliceGrubu.Add(new Kriter {Adi = "KKO"});
		//	AramaKriterdenElasticSearchOlusturan olusturan = new AramaKriterdenElasticSearchOlusturan();
		//	ElasticSearchGet esg = olusturan.Olustur(ak);

		//	Assert.AreEqual(1, esg.filter.and.Count);
		//	Assert.AreEqual("KKO", esg.filter.and[0].terms.policeGrubu[0]);
		//}

		//[Test]
		//public void KKOVeTRFPoliceGrubuFiltresiOlanKriterGeldiginde_GetFilterAndTermsPoliceGrubuIcindeKKOVeTRFKayitOlmali()
		//{
		//	AramaKriterleri ak = AramaKriterleri.Olustur();
		//	ak.SecilebilirKriterler.PoliceGrubu.Add(new Kriter { Adi = "KKO" });
		//	ak.SecilebilirKriterler.PoliceGrubu.Add(new Kriter { Adi = "TRF" });
		//	AramaKriterdenElasticSearchOlusturan olusturan = new AramaKriterdenElasticSearchOlusturan();
		//	ElasticSearchGet esg = olusturan.Olustur(ak);

		//	Assert.AreEqual(1, esg.filter.and.Count);
		//	Assert.AreEqual(2, esg.filter.and[0].terms.policeGrubu.Count);
		//	Assert.AreEqual("KKO", esg.filter.and[0].terms.policeGrubu[0]);
		//	Assert.AreEqual("TRF", esg.filter.and[0].terms.policeGrubu[1]);
		//}

		//[Test]
		//public void KKOVeTRFPoliceGrubuVeMercedesMarkaFiltresiOlanKriterGeldiginde_GetFilterAndTermsPoliceGrubuIcindeKKOVeTRFVeMarkaIcindeMercedesKayitOlmali()
		//{
		//	AramaKriterleri ak = AramaKriterleri.Olustur();
		//	ak.SecilebilirKriterler.PoliceGrubu.Add(new Kriter { Adi = "KKO" });
		//	ak.SecilebilirKriterler.PoliceGrubu.Add(new Kriter { Adi = "TRF" });
		//	ak.SecilebilirKriterler.Marka.Add(new Kriter { Adi = "mercedes" });
		//	AramaKriterdenElasticSearchOlusturan olusturan = new AramaKriterdenElasticSearchOlusturan();
		//	ElasticSearchGet esg = olusturan.Olustur(ak);

		//	Assert.AreEqual(1, esg.filter.and.Count);
		//	Assert.AreEqual(2, esg.filter.and[0].terms.policeGrubu.Count);
		//	Assert.AreEqual(1, esg.filter.and[0].terms.marka.Count);
		//	Assert.AreEqual("KKO", esg.filter.and[0].terms.policeGrubu[0]);
		//	Assert.AreEqual("TRF", esg.filter.and[0].terms.policeGrubu[1]);
		//	Assert.AreEqual("mercedes", esg.filter.and[0].terms.marka[0]);
		//}

		//[Test]
		//public void TanzimTarihiOlanKriterGeldiginde_GetFilterAndRangeIcindeTanzimTarihiAraligiDoluOlmali()
		//{
		//	AramaKriterleri ak = AramaKriterleri.Olustur();
		//	ak.SecilebilirKriterler.TanzimTarihAraligi = new BaslangicBitisTarihi { IlkTarih=new DateTime(2014, 1, 1), SonTarih = new DateTime(2014, 2, 2)};
		//	AramaKriterdenElasticSearchOlusturan olusturan = new AramaKriterdenElasticSearchOlusturan();
		//	ElasticSearchGet esg = olusturan.Olustur(ak);

		//	//Assert.AreEqual(1, esg.filter.and[0].range.tanzimTarihi.from.Day);
		//	//Assert.AreEqual(2, esg.filter.and[0].range.tanzimTarihi.to.Day);
		//}

		//[Test]
		//public void SerbestMetinSorgusuKriterGeldiginde_GetQueryDoluOlmali()
		//{
		//	AramaKriterleri ak = AramaKriterleri.Olustur();
		//	ak.Query = "query";
		//	AramaKriterdenElasticSearchOlusturan olusturan = new AramaKriterdenElasticSearchOlusturan();
		//	ElasticSearchGet esg = olusturan.Olustur(ak);
		//	Assert.AreEqual("query", esg.query.query_string.query);
		//}

		//[Test]
		//public void From100Size200KriterGeldiginde_GetFrom100Size200Olmali()
		//{
		//	AramaKriterleri ak = AramaKriterleri.Olustur();
		//	ak.From = 100;
		//	ak.Size = 200;
		//	AramaKriterdenElasticSearchOlusturan olusturan = new AramaKriterdenElasticSearchOlusturan();
		//	ElasticSearchGet esg = olusturan.Olustur(ak);
		//	Assert.AreEqual(100, esg.from);
		//	Assert.AreEqual(200, esg.size);
		//}

		[Test]
		public void From0Size100QueryDenemeKriterGeldiginde_DeserializeSonucuDogurOlmali()
		{
			AramaKriterleri ak = AramaKriterleri.Olustur();
			ak.From = 0;
			ak.Size = 100;
			ak.Query = "renault";
			ak.SecilebilirKriterler.PoliceGrubu.Add(new Kriter { Adi = "kko" });
			ak.SecilebilirKriterler.Marka.Add(new Kriter { Adi = "renault" });
			ak.SecilebilirKriterler.TanzimTarihAraligi = new BaslangicBitisTarihi { IlkTarih = new DateTime(2000, 1, 1), SonTarih = new DateTime(2014, 12, 31) };

			AramaKriterdenElasticSearchOlusturan olusturan = new AramaKriterdenElasticSearchOlusturan();
			ElasticSearchGet esg = olusturan.Olustur(ak);
			JsonSerializerSettings js = new JsonSerializerSettings();
			js.NullValueHandling = NullValueHandling.Ignore;
			string serializeObject = JsonConvert.SerializeObject(esg, js);
			Assert.AreEqual(serializeObject, "121212");
			
		}
	}

	public class AramaKriterdenElasticSearchOlusturan
	{
		public ElasticSearchGet Olustur(AramaKriterleri ak)
		{
			ElasticSearchGet retVal = ElasticSearchGet.Olustur();

			retVal.from = ak.From;
			retVal.size = ak.Size;
			retVal.query.query_string.query = ak.Query;
			retVal.filter.and.AddRange(aramaKriterlerindenTermsFiltreleriAl(ak));

			return retVal;
		}

		private List<EsAnd> aramaKriterlerindenTermsFiltreleriAl(AramaKriterleri ak)
		{
			List<EsAnd> retVal = new List<EsAnd>();
			//IAnd esAnd = EsAnd.Olustur();
			EsAnd esAnd = new EsAnd();//EsAnd.Olustur();
			esAnd.terms = new EsTerms();
			esAnd.terms.policeGrubu = new List<string>();
			foreach (Kriter kr in ak.SecilebilirKriterler.PoliceGrubu)
			{
				esAnd.terms.policeGrubu.Add(kr.Adi);
			}
			retVal.Add(esAnd);
			EsAnd esAnd2 = new EsAnd(); //EsAnd.Olustur();
			esAnd2.terms = new EsTerms();
			esAnd2.terms.marka = new List<string>();
			foreach (Kriter kr in ak.SecilebilirKriterler.Marka)
			{
				esAnd2.terms.marka.Add(kr.Adi);
			}
			retVal.Add(esAnd2);
			//esAnd.range.tanzimTarihi.from = ak.SecilebilirKriterler.TanzimTarihAraligi.IlkTarih;
			//esAnd.range.tanzimTarihi.to = ak.SecilebilirKriterler.TanzimTarihAraligi.SonTarih;
			EsRange range = EsRange.Olustur();
			range.tanzimTarihi.from = ak.SecilebilirKriterler.TanzimTarihAraligi.IlkTarih;
			range.tanzimTarihi.to = ak.SecilebilirKriterler.TanzimTarihAraligi.SonTarih;
			//retVal.Add(range);
			return retVal;
		}
	}

	public class ElasticSearchGet
	{
		public int from { get; set; }
		public int size { get; set; }
		public List<string> fields { get; set; }
		public Query query { get; set; }
		public EsFilter filter { get; set; }
		//public EsFacets facets { get; set; }
		public static ElasticSearchGet Olustur()
		{
			ElasticSearchGet retVal = new ElasticSearchGet {fields = new List<string>(), filter = EsFilter.Olustur(), query = Query.Olustur(), from=0, size=100};
			return retVal;
		}
	}

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

	public class EsTerms
	{
		public List<string> policeGrubu { get; set; }
		public List<string> marka { get; set; }

		public static EsTerms Olustur()
		{
			EsTerms retVal = new EsTerms();
			retVal.policeGrubu = new List<string>();
			retVal.marka = new List<string>();
			return retVal;
		}
	}

	public class EsAnd
	{
		public EsTerms terms { get; set; }
		public EsRange range { get; set; }
		public static EsAnd Olustur()
		{
			EsAnd retVal = new EsAnd();
			retVal.terms = EsTerms.Olustur();
			retVal.range = EsRange.Olustur();
			return retVal;
		}
	}

	public class EsFilter
	{
		//public List<EsAnd> and { get; set; }
		public List<object> and { get; set; }
		public EsRange range { get; set; }
		public static EsFilter Olustur()
		{
			EsFilter retVal = new EsFilter();
			retVal.and = new List<object>();
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

	//public class EsAnd
	//{
	//	public EsRange range { get; set; }
	//	public EsTerms terms { get; set; }

	//	public static EsAnd Olustur()
	//	{
	//		EsAnd retVal = new EsAnd();
	//		retVal.range = EsRange.Olusutur();
	//		retVal.terms = EsTerms.Olustur();
	//		return retVal;
	//	}
	//}

	//public interface IAnd { }
	//public class TanzimTarihi
	//{
	//	public DateTime from { get; set; }
	//	public DateTime to { get; set; }
	//}

	//public class EsRange : IAnd
	//{
	//	public TanzimTarihi tanzimTarihi { get; set; }

	//	public static EsRange Olusutur()
	//	{
	//		EsRange retVal = new EsRange();
	//		retVal.tanzimTarihi = new TanzimTarihi();
	//		return retVal;
	//	}
	//}

	//public class EsTerms : IAnd
	//{
	//	public List<string> policeGrubu { get; set; }
	//	//public List<string> marka { get; set; }

	//	public static EsTerms Olustur()
	//	{
	//		EsTerms retVal = new EsTerms();
	//		retVal.policeGrubu = new List<string>();
	//		//retVal.marka = new List<string>();
	//		return retVal;
	//	}
	//}

	
}
