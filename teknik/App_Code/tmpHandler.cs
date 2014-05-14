using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using AdaGenel.Cesitli;
using AdaVeriKatmani;
using app_aaw_lib;
using app_aaw_lib.EsSearch.Indexleme;
using Newtonsoft.Json;

/// <summary>
/// Summary description for tmpHandler
/// </summary>
public class tmpHandler : IHttpHandler
{

	public void ProcessRequest(HttpContext context)
	{
		loginYapilmadiysaCoreUygulamayaSorarakLoginYap();
		if (loginOlmus())
		{
			string rawUrl = context.Request.RawUrl;
			int slashIndex = rawUrl.LastIndexOf("/", StringComparison.Ordinal);
			if (slashIndex >= 0)
				rawUrl = rawUrl.Substring(slashIndex + 1);
			HttpHandlerBilgiOlusturmaSonucu handlerBilgiOlusturmaSonucu = handlerBilgiOlustur(rawUrl);
			if (!handlerBilgiOlusturmaSonucu.Basarili)
			{
				context.Response.Write("HTTP handler bilgisi oluşturulamadı (622.65 - " + handlerBilgiOlusturmaSonucu.Mesaj + " - " + rawUrl + ")");
				return;
			}
			ilgiliHandlerinIlgiliMetodunuCalistir(handlerBilgiOlusturmaSonucu.Bilgi, context, rawUrl);
		}
		else
			loginOlmadinHatasiGonder();
	}

	private void loginOlmadinHatasiGonder()
	{
		throw new NotImplementedException();
	}

	private void ilgiliHandlerinIlgiliMetodunuCalistir(HttpHandlerBilgi handlerBilgi, HttpContext context, string rawUrl)
	{
		try
		{
			handlerBilgi.Metod.Invoke(Activator.CreateInstance(handlerBilgi.Type), new object[] { context });
		}
		catch (Exception ex)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Kullanıcı: ");
			sb.Append("Request: " + new StreamReader(context.Request.InputStream).ReadToEnd());
			sb.Append("URL: " + rawUrl);
			sb.Append(ex.Message + " -- " + ex.StackTrace);
			if (ex.InnerException != null)
				sb.Append(ex.InnerException.Message + " -- " + ex.InnerException.StackTrace);
			Araclar.RastgeleIsimliDosyaKaydet("iamHttpHandlerHata", string.Join("\r", sb.ToString()), "txt");
			//	context.Response.Write(JsonConvert.SerializeObject(IstekSonuc.Hata("HTTP isteği işlenirken hata oluştu (" + sb + ")")));
			//throw new Exception((ex.InnerException??ex).Message);

			context.Response.Write(JsonConvert.SerializeObject(new IslemSonuc { Mesaj = (ex.InnerException ?? ex).Message })); // IstekSonuc.Hata((ex.InnerException ?? ex).Message)));
		}
	}


	private bool loginOlmus()
	{
		return true;
	}

	private void loginYapilmadiysaCoreUygulamayaSorarakLoginYap()
	{
		
	}

	private HttpHandlerBilgiOlusturmaSonucu handlerBilgiOlustur(string rawUrl)
	{
		int index = rawUrl.IndexOf(".iam", StringComparison.Ordinal);
		if (index == -1)
			return new HttpHandlerBilgiOlusturmaSonucu { Basarili = false, Mesaj = "(436.66)" };
		string sinifVeMetodAdi = rawUrl.Substring(0, index);
		index = sinifVeMetodAdi.LastIndexOf(".", StringComparison.Ordinal);
		if (index == -1)
			return new HttpHandlerBilgiOlusturmaSonucu { Basarili = false, Mesaj = "(436.67)" };
		string sinifAdi = sinifVeMetodAdi.Substring(0, index);
		string metodAdi = sinifVeMetodAdi.Substring(index + 1);
		Type handlerType = handlerTypeOlustur(sinifAdi);
		if (handlerType == null)
		{
			return new HttpHandlerBilgiOlusturmaSonucu { Basarili = false, Mesaj = "(436.68)" };
		}

		MethodInfo metod = handlerType.GetMethod(metodAdi);
		if (metod == null)
			return new HttpHandlerBilgiOlusturmaSonucu { Basarili = false, Mesaj = "(436.69)" };
		return new HttpHandlerBilgiOlusturmaSonucu { Basarili = true, Bilgi = new HttpHandlerBilgi { Type = handlerType, Metod = metod } };
	}

	private static Type handlerTypeOlustur(string sinifAdi)
	{
		Type handlerType = Type.GetType("IAM.Resources.HttpHandlers." + sinifAdi + "HttpHandler");
		return handlerType;
	}

	public class HttpHandlerBilgiOlusturmaSonucu : IstekSonuc
	{
		public HttpHandlerBilgi Bilgi;
	}

	public class HttpHandlerBilgi
	{
		public Type Type;
		public MethodInfo Metod;
	}

	public class IstekSonuc
	{
		public bool Basarili;
		public string Mesaj;
	}

	//public void ProcessRequest(HttpContext context)
	//{
	//	HttpRequest request = context.Request;

	//	StreamReader sr = new StreamReader(context.Request.InputStream);
	//	string json = sr.ReadToEnd();
	//	AramaKriterleri degerler = JsonConvert.DeserializeObject<AramaKriterleri>(json);

	//	HttpResponse response = context.Response;
	//	string url = request.RawUrl;
	//	string readToEnd = new StreamReader("D:\\Cetin\\Belgelerim\\Visual Studio 2013\\Projects\\aaw_apps\\teknik\\policeAramaSonuc.json").ReadToEnd();
	//	//string readToEnd = new StreamReader("C:\\GitHub\\aaw_teknik\\teknik\\policeAramaSonuc.json").ReadToEnd();
	//	response.Write("{ \"Sonuc\" : " + readToEnd + ", \"Kriterler\" : " + JsonConvert.SerializeObject(degerler) + " }");
	//}

	//public void ProcessRequest(HttpContext context)
	//{
	//	HttpRequest request = context.Request;
	//	HttpResponse response = context.Response;

	//	string url = request.RawUrl;
	//	url = HttpUtility.UrlDecode(url);
	//	var originalStream = new StreamReader(request.InputStream);
	//	//var json = originalStream.ReadToEnd();
	//	var json =
	//		"{\"Guvenlik\":{\"TarihSaat\":\"2014-02-25T15:19:26.4884203+02:00\",\"KullaniciAdi\":\"cetin\",\"Parola\" : \"xxxxxxxx\"},\"Tipi\":\"PoliceTopluIndexle\",\"Veri\" : {\"fTnzTar\":[\"2013-12-01T15:19:26.4884203+02:00\",\"2013-12-31T15:19:26.4884203+02:00\"]}}";

	//	GelenIstekDetay iVeri = IndexlemeVeriOlustur(json);
	//	//security token'ı kontrol et hata verirse dön
	//	////reflectionla ilgili handler sınıfını oluştur, IstekIsle(VeritabaniIslemleri) metodunu çağır
	//	ICariApiIstekHandler handler = handlerYarat(iVeri);

	//	AAWIndexGuncellemeMotoru igm = new AAWIndexGuncellemeMotoru("http://localhost:9200");
	//	string sonuc = handler.IstekIsle(veriIslemleriOlustur(), iVeri, igm);
	//	response.Write(sonuc);
	//}

	//private GelenIstekDetay IndexlemeVeriOlustur(string json)
	//{
	//	GelenIstekDetay deserializeObject = Newtonsoft.Json.JsonConvert.DeserializeObject<GelenIstekDetay>(json);
	//	return deserializeObject;
	//}

	//private ICariApiIstekHandler handlerYarat(GelenIstekDetay gid)
	//{
	//	if (gid.Tipi == "PoliceTekIndexle")
	//		return new PoliceTekIndexleHandler();
	//	if (gid.Tipi == "PoliceTopluIndexle")
	//		return new PoliceTopluIndexleHandler();
	//	if (gid.Tipi == "TumIndexSil")
	//		return new TumIndexSilHandler();
	//	return null;
	//}

	//private static TemelVeriIslemleri veriIslemleriOlustur()
	//{
	//	string connectionString = "Provider=vfpoledb.1;Collating Sequence=TURKISH;DATE=BRITISH;connection Timeout=1200;Data Source=D:\\Evrim\\ADADATA.DBC";
	//	VeritabaniTipi veritabaniTipi = VeritabaniTipi.FoxPro;
	//	return new TemelVeriIslemleri(veritabaniTipi, connectionString);
	//}
	public bool IsReusable { get; private set; }
}