using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using AdaGenel.Cesitli;
using Newtonsoft.Json;
using app_aaw_lib;

/// <summary>
/// Summary description for tmpHandler
/// </summary>
public class TeknikHandler : IHttpHandler
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
			Araclar.RastgeleIsimliDosyaKaydet("teknikHttpHandlerHata", string.Join("\r", sb.ToString()), "txt");
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
		int index = rawUrl.IndexOf(".teknik", StringComparison.Ordinal);
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
		//Type handlerType = Type.GetType("app_aaw_lib.EsSearch." + sinifAdi + "HttpHandler");
		Assembly assembly = Assembly.Load("app_aaw_lib");
		Type handlerType = assembly.GetType("app_aaw_lib.EsSearch." + sinifAdi + "HttpHandler");
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

	public bool IsReusable { get; private set; }
}