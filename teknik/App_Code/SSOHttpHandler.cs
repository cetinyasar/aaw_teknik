//using System.Web;
//using AdaGenel.Cesitli;
//using AdaHttpHandler;
//using AdaSSO;

//namespace app_teknik
//{
//	[AdaHttpHandlerClass("SSOHttpHandler")]
//	public class SSOHttpHandler
//	{
//		[AdaHttpHandlerMethod("SessionBootstrapAyarlariniAl")]
//		public SessionBootstrapAyarlar SessionBootstrapAyarlariniAl()
//		{
//			string sessionKey = Araclar.RastgeleString(30);
//			string cookieDomain = System.Configuration.ConfigurationManager.AppSettings["CookieDomain"];
//			string loginUrl = System.Configuration.ConfigurationManager.AppSettings["LoginUrl"];
//			return new SessionBootstrapAyarlar { SessionKey = sessionKey, CookieDomain = cookieDomain, LoginUrl = loginUrl };
//		}

//		[AdaHttpHandlerMethod("LoginKontrol")]
//		public LoginKontrolSonuc LoginKontrol()
//		{
//			SSOYonetici yonetici = new SSOYonetici(new ElasticSearchIletisimcisi());
//			LoginAramaSonuc sonuc = yonetici.LoginAl(new HttpRequestAyrinti(HttpContext.Current.Request));
//			return new LoginKontrolSonuc { LoginYapilmis = sonuc.LoginBulundu, LoginUrl = System.Configuration.ConfigurationManager.AppSettings["LoginUrl"] };
//		}
//	}

//	public class LoginKontrolSonuc
//	{
//		public bool LoginYapilmis;
//		public string LoginUrl;
//	}

//	public class SessionBootstrapAyarlar
//	{
//		public string SessionKey { get; set; }
//		public string CookieDomain { get; set; }
//		public string LoginUrl { get; set; }
//	}
//}