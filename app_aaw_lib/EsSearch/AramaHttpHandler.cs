using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using app_aaw_lib.EsSearch.Sorgu;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace app_aaw_lib.EsSearch
{
	public class AramaHttpHandler
	{
		public void Deneme(HttpContext context)
		{
			
		}

		public void Ara(HttpContext context)
		{
			AramaKriterleri ak = RequestNesneOlustur<AramaKriterleri>(context);

			AramaKriterdenElasticSearchOlusturan akeso = new AramaKriterdenElasticSearchOlusturan(ak);
			ElasticSearchGet elasticSearchGet = akeso.Olustur();

			JsonSerializerSettings jss = new JsonSerializerSettings();
			jss.NullValueHandling = NullValueHandling.Ignore;
			string serializeObject = JsonConvert.SerializeObject(elasticSearchGet, jss);

			PoliceAramaMotoru pam = new PoliceAramaMotoru();
			string aramaSonuc = pam.AramaYap(serializeObject);

			ResponseAyarla(context, "{ \"Sonuc\" : " + aramaSonuc + ", \"Kriterler\" : " + JsonConvert.SerializeObject(ak) + " }");
			//ResponseAyarla(context, sonuc.Basarili ? new FaaliyetIslemOutput {Basarili = true, Faaliyet = new IsFaaliyet().Doldur(IsAkisiInstance.Al(input.IsAkisiInstanceId, TemelVeriIslemleriOlustur()).FaaliyetAl(input.FaaliyetInstanceId), TumKullaniciAdlariniGetir(), IAMSessionNesneleri.Kullanici, TemelVeriIslemleriOlustur())} : new FaaliyetIslemOutput {Basarili = false, Mesaj = sonuc.Mesaj});
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
}
