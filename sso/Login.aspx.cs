using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdaGenel.Cesitli;
using AdaSSO;
using AdaVeriKatmani;
using AjaxPro;

namespace sso
{
	[AjaxPro.AjaxNamespace("sso")]
	public partial class Login : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string sonuc = AAWSabit.YapilmadiysaUygulamaIlkAyarlariYap(System.Configuration.ConfigurationManager.AppSettings["RootPath"], System.Configuration.ConfigurationManager.AppSettings["TestSunucusu"], System.Configuration.ConfigurationManager.AppSettings["VeritabaniBaglantiString"], System.Configuration.ConfigurationManager.AppSettings["VeritabaniTipi"], HttpContext.Current, System.Configuration.ConfigurationManager.AppSettings["GorevDosyalarDizini"], System.Configuration.ConfigurationManager.AppSettings["SSOCookieDomain"]);
			string cookieDomain = AAWSabit.SSOCookieDomain;
			Page.ClientScript.RegisterStartupScript(GetType(), "adaSSOJS" + ID, "if (docCookies.getItem('adaSessionId') == null){docCookies.setItem('adaSessionId', '" + Araclar.RastgeleString(30) + "', false, null, " + (cookieDomain == null ? "null" : "'" + cookieDomain + "'") + ");}", true);
			AjaxPro.Utility.RegisterTypeForAjax(typeof(Login), Page);
		}

		[AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
		public string gir()
		{
			SSOYonetici yonetici = new SSOYonetici(new ElasticSearchIletisimcisi());
			yonetici.BasariliLoginKaydet(1, new HttpRequestAyrinti(HttpContext.Current.Request));
			return "ok";
		}
	}

	public class AAWSabit
	{
		public static bool UygulamaIlkAyarlarYapildi = false;
		public const string OnemsizSifrelemeAnahtari = "ADatmptmpada123";
		public static string SiteUrl = "";
		public static string SSOCookieDomain;
		public static string ElasticSearchUrl = "http://localhost:9200"; //varsayılan değer
		public static string UygulamaIlkAyarlarLock = "tmptmptmpt";

		public static string YapilmadiysaUygulamaIlkAyarlariYap(string rootPath, string testSunucusu, string veritabaniBaglantiString, string veritabaniTipi, HttpContext context, string gorevDosyalarDizini, string ssoCookieDomain)
		{
			lock (UygulamaIlkAyarlarLock)
			{
				if (UygulamaIlkAyarlarYapildi)
				{
					//TmpData += "yapılmış,";
					return "0";
				}
				//TmpData += "YAIPLIYOR,";
				AdaGenel.Cesitli.Sabit.ROOT_PATH = string.IsNullOrEmpty(rootPath) ? context.Server.MapPath("").Replace("\\ajaxpro", "").Replace("\\style", "").Replace("\\images", "").Replace("\\scripts", "").Replace("\\UI", "").Replace("\\jQuery", "").Replace("\\cupertino", "") : rootPath;
				AdaGenel.Cesitli.Sabit.YUKLENEN_DOSYALAR_PATH = AdaGenel.Cesitli.Sabit.ROOT_PATH + "\\YuklenenDosyalar";
				AdaGenel.Cesitli.Sabit.TEST_SUNUCUSU = testSunucusu == "1";
				AdaGenel.Cesitli.Sabit.GECICI_DOSYALAR_PATH = AdaGenel.Cesitli.Sabit.ROOT_PATH + "\\GeciciDosyalar";
				AdaGenel.Cesitli.Sabit.GOREV_DOSYALAR_PATH = gorevDosyalarDizini;
				VeritabaniSabitler.VERITABANI_TIPI = (VeritabaniTipi)Enum.Parse(typeof(VeritabaniTipi), veritabaniTipi);
				VeritabaniSabitler.VERITABANI_BAGLANTI_STRING = veritabaniBaglantiString;
				SSOCookieDomain = ssoCookieDomain;
				SiteUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;
				UygulamaIlkAyarlarYapildi = true;
				TekrarlanabilirIlkAyarlariYap();

				return "1";
			}
		}

		public static void TekrarlanabilirIlkAyarlariYap()
		{
			ssoAyarlariniYap();
		}

		private static void ssoAyarlariniYap()
		{
			AdaSSOSabitler.ElasticSearchIndexAdi = "adasso";
			TemelVeriIslemleri tvi = new TemelVeriIslemleri(VeritabaniSabitler.VERITABANI_TIPI, VeritabaniSabitler.VERITABANI_BAGLANTI_STRING);
			AdaSSOSabitler.ElasticSearchUrl = "http://localhost:9200";
		}


	}

}