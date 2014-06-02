using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AdaGenel.Cesitli;

namespace app_aaw_lib.EsSearch.Indexleme
{
	public class AAWIndexGuncellemeMotoru
	{
		private string _url = "";
		private readonly string _elasticSearchUrl = "";
		public AAWIndexGuncellemeMotoru(string elasticSearchUrl)
		{
			_elasticSearchUrl = elasticSearchUrl;
		}

		public void Guncelle(IndexMetadataList bilgiler, bool yeniThreaddeIslemYap)
		{
			topluUrlOlustur();
			object[] parametreler = { bilgiler, yeniThreaddeIslemYap };
			if (yeniThreaddeIslemYap)
			{
				ParameterizedThreadStart ts = topluGirisYap;
				Thread t = new Thread(ts) { CurrentCulture = Thread.CurrentThread.CurrentCulture, CurrentUICulture = Thread.CurrentThread.CurrentUICulture };
				t.Start(parametreler);
			}
			else
				topluGirisYap(parametreler);
		}

		private void topluGirisYap(object parametre)
		{
			IndexMetadataList bilgiler = (IndexMetadataList)((object[])parametre)[0];
			bool yeniThreaddeIslemYap = (bool)((object[])parametre)[1];
			StringBuilder sb = new StringBuilder();
			foreach (KeyValuePair<int, IndexMetadata> keyValuePair in bilgiler)
			{
				string idx = "{ \"index\" : { \"_index\" : \"aaw\", \"_type\" : \"" + bilgiler.KayitTipiAdi + "\", \"_id\" : \"" + keyValuePair.Key + "\" } }";
				sb.Append(idx + "\r\n");
				sb.Append(jsonOlustur(keyValuePair.Value) + "\r\n");
			}
			//StreamWriter sw = new StreamWriter("d:\\cetin"+Araclar.RastgeleRakam(6)+".json");
			//sw.WriteLine(sb.ToString());
			//sw.Close();
			topluEkle(sb.ToString(), yeniThreaddeIslemYap);
		}

		private void topluEkle(string json, bool yeniThreaddeIslemYap)
		{

			var httpWebRequest = (HttpWebRequest)WebRequest.Create(_url);
			httpWebRequest.ContentType = "text/json";
			httpWebRequest.Method = "POST";
			try
			{
				using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
				{
					streamWriter.Write(json);
				}
				var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					var responseText = streamReader.ReadToEnd();
				}
			}
			catch (Exception ex)
			{
				//eğer işlem yeni bir threadde yapılıyorsa arka plan işlemidir. bu durumda burada exception atmaman gerekir.
				//aynı thread içinde yapılan bir işlem ise exception at. 
				if (!yeniThreaddeIslemYap)
					throw new Exception("topluEkle hata (" + ex.Message + ") -- JSON: " + json);
				}

		}

		public void Guncelle(IndexMetadata bilgiler, bool yeniThreaddeIslemYap)
		{
			urlOlustur(bilgiler.Id, bilgiler.KayitTipiAdi);
			object[] parametreler = { bilgiler, yeniThreaddeIslemYap };
			if (yeniThreaddeIslemYap)
			{
				ParameterizedThreadStart ts = tekGirisYap;
				Thread t = new Thread(ts) { CurrentCulture = Thread.CurrentThread.CurrentCulture, CurrentUICulture = Thread.CurrentThread.CurrentUICulture };
				t.Start(parametreler);
			}
			else
				tekGirisYap(parametreler);

		}

		private void tekGirisYap(object parametre)
		{
			IndexMetadata bilgiler = (IndexMetadata)((object[])parametre)[0];
			bool yeniThreaddeIslemYap = (bool)((object[])parametre)[1];
			tekEkle(jsonOlustur(bilgiler), yeniThreaddeIslemYap);
		}

		private void tekEkle(string json, bool yeniThreaddeIslemYap)
		{

			var httpWebRequest = (HttpWebRequest)WebRequest.Create(_url);
			httpWebRequest.ContentType = "text/json";
			httpWebRequest.Method = "PUT";
			try
			{
				using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
				{
					streamWriter.Write(json);
				}
				var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					var responseText = streamReader.ReadToEnd();
				}
			}
			catch (Exception ex)
			{
				//eğer işlem yeni bir threadde yapılıyorsa arka plan işlemidir. bu durumda burada exception atmaman gerekir.
				//aynı thread içinde yapılan bir işlem ise exception at. 
				if (!yeniThreaddeIslemYap)
					throw new Exception("tekEkle hata (" + ex.Message + ") -- JSON: " + json);
			}

		}

		//$ curl -s -XPOST localhost:9200/_bulk --data-binary @dosya.txt
		private void topluUrlOlustur()
		{
			_url = _elasticSearchUrl + "/_bulk ";
		}
		//$ curl -XPUT 'http://localhost:9200/twitter/tweet/_mapping' -d '
		private void urlOlustur(int fPrkPol, string kayitTipiAdi)
		{
			_url = _elasticSearchUrl + "/aaw/" + kayitTipiAdi + "/" + fPrkPol;
		}

		private void silUrlOlustur()
		{
			_url = _elasticSearchUrl;
		}

		private string jsonOlustur(IndexMetadata ikb)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("{");
			foreach (KayitAlan ka in ikb.Alanlar)
			{
				string json = ka.Deger.JsonOlustur(ka.NT, ka.AlanAdi);
				sb.Append(json + (string.IsNullOrEmpty(json) ? "" : ","));
			}
			return sb.ToString().TrimEnd(new char[] { ',' }) + "}";
		}

		public void TumIndexleriSil(bool yeniThreaddeIslemYap)
		{
			silUrlOlustur();
			object[] parametreler = { yeniThreaddeIslemYap };
			if (yeniThreaddeIslemYap)
			{
				ParameterizedThreadStart ts = indexSil;
				Thread t = new Thread(ts) { CurrentCulture = Thread.CurrentThread.CurrentCulture, CurrentUICulture = Thread.CurrentThread.CurrentUICulture };
				t.Start(parametreler);
			}
			else
				indexSil(parametreler);
		}

		private void indexSil(object parametre)
		{
			//IndexMetadata bilgiler = (IndexMetadata)((object[])parametre)[0];
			bool yeniThreaddeIslemYap = (bool)((object[])parametre)[0];

			var httpWebRequest = (HttpWebRequest)WebRequest.Create(_url);
			httpWebRequest.ContentType = "text/json";
			httpWebRequest.Method = "delete";
			try
			{
				using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
				{
					streamWriter.Write("aaw");
				}
				var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					var responseText = streamReader.ReadToEnd();
				}
			}
			catch (Exception ex)
			{
				//eğer işlem yeni bir threadde yapılıyorsa arka plan işlemidir. bu durumda burada exception atmaman gerekir.
				//aynı thread içinde yapılan bir işlem ise exception at. 
				if (!yeniThreaddeIslemYap)
					throw new Exception("indexSil hata (" + ex.Message + ")");
			}
		}
	}

}
