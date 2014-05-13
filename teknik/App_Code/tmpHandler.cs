using System;
using System.IO;
using System.Linq;
using System.Web;
using AdaVeriKatmani;
using app_aaw_lib;
using app_aaw_lib.EsSearch.Indexleme;
using Newtonsoft.Json;

/// <summary>
/// Summary description for tmpHandler
/// </summary>
public class tmpHandler : IHttpHandler
{
	//public void ProcessRequest(HttpContext context)
	//{
	//	HttpRequest request = context.Request;

	//	StreamReader sr = new StreamReader(context.Request.InputStream);
	//	string json = sr.ReadToEnd();
	//	AramaKriterleri degerler = JsonConvert.DeserializeObject<AramaKriterleri>(json);

	//	HttpResponse response = context.Response;
	//	string url = request.RawUrl;
	//	//string readToEnd = new StreamReader("D:\\Cetin\\Belgelerim\\Visual Studio 2013\\Projects\\aaw_apps\\teknik\\policeAramaSonuc.json").ReadToEnd();
	//	string readToEnd = new StreamReader("C:\\GitHub\\aaw_teknik\\teknik\\policeAramaSonuc.json").ReadToEnd();
	//	response.Write("{ \"Sonuc\" : " + readToEnd + ", \"Kriterler\" : " + JsonConvert.SerializeObject(degerler) + " }");
	//}

	public void ProcessRequest(HttpContext context)
	{
		HttpRequest request = context.Request;
		HttpResponse response = context.Response;

		string url = request.RawUrl;
		url = HttpUtility.UrlDecode(url);
		var originalStream = new StreamReader(request.InputStream);
		//var json = originalStream.ReadToEnd();
		var json =
			"{\"Guvenlik\":{\"TarihSaat\":\"2014-02-25T15:19:26.4884203+02:00\",\"KullaniciAdi\":\"cetin\",\"Parola\" : \"xxxxxxxx\"},\"Tipi\":\"PoliceTopluIndexle\",\"Veri\" : {\"fTnzTar\":[\"2013-12-01T15:19:26.4884203+02:00\",\"2013-12-31T15:19:26.4884203+02:00\"]}}";

		GelenIstekDetay iVeri = IndexlemeVeriOlustur(json);
		//security token'ı kontrol et hata verirse dön
		////reflectionla ilgili handler sınıfını oluştur, IstekIsle(VeritabaniIslemleri) metodunu çağır
		ICariApiIstekHandler handler = handlerYarat(iVeri);

		AAWIndexGuncellemeMotoru igm = new AAWIndexGuncellemeMotoru("http://localhost:9200");
		string sonuc = handler.IstekIsle(veriIslemleriOlustur(), iVeri, igm);
		response.Write(sonuc);
	}

	private GelenIstekDetay IndexlemeVeriOlustur(string json)
	{
		GelenIstekDetay deserializeObject = Newtonsoft.Json.JsonConvert.DeserializeObject<GelenIstekDetay>(json);
		return deserializeObject;
	}

	private ICariApiIstekHandler handlerYarat(GelenIstekDetay gid)
	{
		if (gid.Tipi == "PoliceTekIndexle")
			return new PoliceTekIndexleHandler();
		if (gid.Tipi == "PoliceTopluIndexle")
			return new PoliceTopluIndexleHandler();
		if (gid.Tipi == "TumIndexSil")
			return new TumIndexSilHandler();
		return null;
	}

	private static TemelVeriIslemleri veriIslemleriOlustur()
	{
		string connectionString = "Provider=vfpoledb.1;Collating Sequence=TURKISH;DATE=BRITISH;connection Timeout=1200;Data Source=D:\\Evrim\\ADADATA.DBC";
		VeritabaniTipi veritabaniTipi = VeritabaniTipi.FoxPro;
		return new TemelVeriIslemleri(veritabaniTipi, connectionString);
	}
	public bool IsReusable { get; private set; }
}