using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			AramaKriterleri ak = new AramaKriterleri();
			AramaKriterdenElasticSearchOlusturan olusturan = new AramaKriterdenElasticSearchOlusturan();
			ElasticSearchGet esg = olusturan.Olustur(ak);


		}
    }

	public class AramaKriterdenElasticSearchOlusturan
	{
		public ElasticSearchGet Olustur(AramaKriterleri ak)
		{
			return new ElasticSearchGet();
		}
	}

	public class ElasticSearchGet
	{
		public int from { get; set; }
		public int size { get; set; }
		public List<string> fields { get; set; }
		public EsFilter filter { get; set; }
		//public EsFacets facets { get; set; }

	}

	public class EsFilter
	{
		public List<EsAnd> and { get; set; }
	}

	public class EsAnd
	{
		public EsRange range { get; set; }
		public EsTerms terms { get; set; }
	}

	public class TanzimTarihi
	{
		public string from { get; set; }
		public string to { get; set; }
	}

	public class EsRange
	{
		public TanzimTarihi tanzimTarihi { get; set; }
	}

	public class EsTerms
	{
		public List<string> policeGrubu { get; set; }
		public List<string> sirketPoliceNo { get; set; }

	}
}
