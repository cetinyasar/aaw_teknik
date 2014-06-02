using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AdaHttpHandler;
using AdaHttpHandler.Olay;
using app_aaw_lib.EsSearch.Sorgu;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace app_aaw_lib.EsSearch
{
	[AdaHttpHandlerClass("AramaHttpHandler")]
	public class AramaHttpHandler
	{
		[AdaHttpHandlerMethod("Ara")]
		[LoginGerekli]
		public string Ara(AramaKriterleri ak)
		{
			AramaKriterdenElasticSearchOlusturan akeso = new AramaKriterdenElasticSearchOlusturan(ak);
			ElasticSearchGet elasticSearchGet = akeso.Olustur();

			JsonSerializerSettings jss = new JsonSerializerSettings();
			jss.NullValueHandling = NullValueHandling.Ignore;
			string elasticSearchQuery = JsonConvert.SerializeObject(elasticSearchGet, jss);

			PoliceAramaMotoru pam = new PoliceAramaMotoru();
			string aramaSonuc = pam.AramaYap(elasticSearchQuery);

			return "{ \"Sonuc\" : " + aramaSonuc + ", \"Kriterler\" : " + JsonConvert.SerializeObject(ak) + ", \"EsSearch\" : \"" + elasticSearchQuery.Substring(1, elasticSearchQuery.Length - 1).Replace("\"", "'") + "\" }";
		}

		protected void ResponseAyarla(HttpContext context, object responseNesnesi)
		{
			context.Response.Write(JsonConvert.SerializeObject(responseNesnesi, new IsoDateTimeConverter()));
		}

		protected void ResponseAyarla(HttpContext context, string responseNesnesi)
		{
			context.Response.Write(responseNesnesi);
		}

		protected T RequestNesneOlustur<T>(HttpContext context)
		{
			string readToEnd = new StreamReader(context.Request.InputStream).ReadToEnd();
			return JsonConvert.DeserializeObject<T>(readToEnd);
		}
	}

	public class PoliceAramaMotoru
	{
		public string AramaYap(string json)
		{
			//string policeAraJson = JsonOlustur(kriterler, kriterler.SayfaNo, 100);
			var httpWebRequest = HttpWebRequestOlustur(urlOlustur());
			using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
				streamWriter.Write(json);

			var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

			using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				return streamReader.ReadToEnd();
		}
		protected HttpWebRequest HttpWebRequestOlustur(string url)
		{
			//string url = urlOlustur();
			var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.ContentType = "text/json";
			httpWebRequest.Method = "POST";
			return httpWebRequest;
		}
		private string urlOlustur()
		{
			return "http://localhost:9200/_search";
		}

	}


	//public class Shards
	//{
	//	public int total { get; set; }
	//	public int successful { get; set; }
	//	public int failed { get; set; }
	//}

	//public class Fields
	//{
	//	public List<string> sirketPoliceNo { get; set; }
	//	public List<string> brans { get; set; }
	//	public List<string> sigortaliAcik { get; set; }
	//	public List<string> tanzimTarihi { get; set; }
	//	public List<string> musteriAdiAcik { get; set; }
	//}

	//public class Hit
	//{
	//	public string _index { get; set; }
	//	public string _type { get; set; }
	//	public string _id { get; set; }
	//	public int _score { get; set; }
	//	public Fields fields { get; set; }
	//}

	//public class Hits
	//{
	//	public int total { get; set; }
	//	public int max_score { get; set; }
	//	public List<Hit> hits { get; set; }
	//}

	//public class Term
	//{
	//	public string term { get; set; }
	//	public int count { get; set; }
	//}

	//public class PoliceGrubu
	//{
	//	public string _type { get; set; }
	//	public int missing { get; set; }
	//	public int total { get; set; }
	//	public int other { get; set; }
	//	public List<Term> terms { get; set; }
	//}

	//public class Term2
	//{
	//	public string term { get; set; }
	//	public int count { get; set; }
	//}

	//public class Marka
	//{
	//	public string _type { get; set; }
	//	public int missing { get; set; }
	//	public int total { get; set; }
	//	public int other { get; set; }
	//	public List<Term2> terms { get; set; }
	//}

	//public class Term3
	//{
	//	public string term { get; set; }
	//	public int count { get; set; }
	//}

	//public class Brans
	//{
	//	public string _type { get; set; }
	//	public int missing { get; set; }
	//	public int total { get; set; }
	//	public int other { get; set; }
	//	public List<Term3> terms { get; set; }
	//}

	//public class Term4
	//{
	//	public string term { get; set; }
	//	public int count { get; set; }
	//}

	//public class ModelYili
	//{
	//	public string _type { get; set; }
	//	public int missing { get; set; }
	//	public int total { get; set; }
	//	public int other { get; set; }
	//	public List<Term4> terms { get; set; }
	//}

	//public class Facets
	//{
	//	public PoliceGrubu policeGrubu { get; set; }
	//	public Marka marka { get; set; }
	//	public Brans brans { get; set; }
	//	public ModelYili modelYili { get; set; }
	//}

	//public class RootObject
	//{
	//	public int took { get; set; }
	//	public bool timed_out { get; set; }
	//	public Shards _shards { get; set; }
	//	public Hits hits { get; set; }
	//	public Facets facets { get; set; }
	//}
}
