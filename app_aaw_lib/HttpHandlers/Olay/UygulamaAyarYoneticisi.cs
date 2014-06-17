using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdaGenel.Cesitli;
using AdaHttpHandler;
using AdaHttpHandler.Lisans;
using AdaSSO;
using AdaVeriKatmani;
using app_aaw_lib.Cesitli;

namespace app_aaw_lib.HttpHandlers.Olay
{
	public class UygulamaAyarYoneticisi
	{
		private static bool _uygulamaIlkAyarlarYapildi;

		public static void UygulamaIlkAyarlariYap()
		{
			_uygulamaIlkAyarlarYapildi = false;
			YapilmadiysaUygulamaIlkAyarlariYap();
		}

		public static void YapilmadiysaUygulamaIlkAyarlariYap()
		{
			lock (AppHttpHandlerOlay.OnemsizLockNesnesi)
			{
				if (_uygulamaIlkAyarlarYapildi)
					return;

				VeritabaniSabitler.VERITABANI_TIPI = (VeritabaniTipi)Enum.Parse(typeof(VeritabaniTipi), System.Configuration.ConfigurationManager.AppSettings["VeritabaniTipi"]);
				VeritabaniSabitler.VERITABANI_BAGLANTI_STRING = System.Configuration.ConfigurationManager.AppSettings["VeritabaniBaglantiString"];
				//IAMSabitler.IAMTestModu = System.Configuration.ConfigurationManager.AppSettings["IAMTestModu"] == "1";
				//IAMSabitler.VeritabaniTipi = VeritabaniSabitler.VERITABANI_TIPI;
				//IAMSabitler.VeritabaniBaglantiString = VeritabaniSabitler.VERITABANI_BAGLANTI_STRING;
				//IAMSabitler.ElasticSearchIndexAdi = "iam";

				//VeritabaniVekiliFabrikasi.Tip = VeritabaniVekilTipi.FoxPro;
				AdaSSOSabitler.ElasticSearchIndexAdi = "adasso";

				//OrtakParametreYoneticisi ortakParametreYoneticisi = new OrtakParametreYoneticisi();
				//IAMSabitler.ElasticSearchUrl = ortakParametreYoneticisi.TekParametreAl(OrtakParametreTip.ElasticSearchUrl);
				AdaSSOSabitler.CookieDomain = Parametre.Al("CookieDomain");
				AdaSSOSabitler.LoginUrl = Parametre.Al("LoginUrl");
				Sabit.ROOT_PATH = Parametre.Al("RootPath");
				Sabit.YUKLENEN_DOSYALAR_PATH = Sabit.ROOT_PATH + "\\" + "yuklenenDosyalar";
				Sabit.GECICI_DOSYALAR_PATH = Sabit.ROOT_PATH + "\\" + "geciciDosyalar";
				AdaSSOSabitler.ElasticSearchUrl = "http://localhost:9200"; //Parametre.Al("ElasticSearchUrl");


				AAWSabitler.VeritabaniTipi = VeritabaniSabitler.VERITABANI_TIPI;
				AAWSabitler.VeritabaniBaglantiString = VeritabaniSabitler.VERITABANI_BAGLANTI_STRING;
				//AAWSabitler.ElasticSearchUrl = System.Configuration.ConfigurationManager.AppSettings["ElasticSearchUrl"];
				AAWSabitler.ElasticSearchIndexAdi = "adasso";


				AdaHttpHandlerSabitler.LisansKonfigurasyon = new LisansKonfigurasyon(true, Parametre.Al("Lisans"), Parametre.Al("MusteriNumarasi"), AAWSabitler.LisansSistemiIcinUygulamaId, null);
				AdaHttpHandlerSabitler.HataLogTut = true;

				//OtomatikCalistirilacakKomutYoneticisi.CalistirilmayanKomutlariCalistir();

				_uygulamaIlkAyarlarYapildi = true;
			}
		}

		public static bool UygulamaninCalisabilmesiIcinGerekliTemelAyarlarYapilmis()
		{
			//OrtakParametreYoneticisi ortakParametreYoneticisi = new OrtakParametreYoneticisi();
			string elasticSearchUrl = Parametre.Al("ElasticSearchUrl");
			string loginUrl = Parametre.Al("LoginUrl");
			string lisans = Parametre.Al("Lisans");
			string musteriNo = Parametre.Al("MusteriNumarasi");
			string rootPath = Parametre.Al("RootPath");
			bool tumTemelAyarlarGirilmis = !String.IsNullOrEmpty(elasticSearchUrl) && !String.IsNullOrEmpty(loginUrl) && !String.IsNullOrEmpty(lisans) && !String.IsNullOrEmpty(musteriNo) && !String.IsNullOrEmpty(rootPath);

			if (!tumTemelAyarlarGirilmis)
				return false;

			//tüm ayarlar tamsa, lisans kontrolü yapman lazım, eğer lisans sorunlu ise yine false dön, ayarlar sayfasından doğru lisans girilsin
			LisansKontrolMotoru lkm = new LisansKontrolMotoru(new LisansKonfigurasyon(true, lisans, musteriNo, AAWSabitler.LisansSistemiIcinUygulamaId, null));
			return lkm.LisansKontrolEt().Basarili;
		}

	}
}
