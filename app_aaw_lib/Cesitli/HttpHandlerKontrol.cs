using System;
using AdaHttpHandler;
using AdaHttpHandler.Olay;
using AdaSSO;
using AdaVeriKatmani;

namespace app_aaw_lib.Cesitli
{
	[AdaHttpHandlerOlayYoneticisi]
	public class HttpHandlerKontrol
	{
		[AdaHttpHandlerKontrolMetodu]
		public static HandlerOncesiKontrolSonucu IAMHandlerOncesiKontrolYap(HttpHandlerBilgi handlerBilgileri)
		{
			yapilmadiysaUygulamaIlkAyarlariYap();
			HandlerOncesiKontrolSonucu retVal = new HandlerOncesiKontrolSonucu { Basarili = true };
			OrtakKontrolMotoru ortakKontrolMotoru = new OrtakKontrolMotoru();
			if (!ortakKontrolMotoru.LoginKontroluYap(handlerBilgileri, AAWSessionNesneleri.Kullanici))
				retVal = new HandlerOncesiKontrolSonucu { Basarili = false, Mesaj = "Login olmalısınız" };
			if (retVal.Basarili && !ortakKontrolMotoru.RolKontroluYap(handlerBilgileri, AAWSessionNesneleri.Kullanici))
				retVal = new HandlerOncesiKontrolSonucu { Basarili = false, Mesaj = "İşlemi yapmak için gerekli role sahip değilsiniz" };
			return retVal;
		}

		private static bool uygulamaIlkAyarlarYapildi = false;
		public static string onemsizLockNesnesi = "asascetinassa";
		private static void yapilmadiysaUygulamaIlkAyarlariYap()
		{
			lock (onemsizLockNesnesi)
			{
				if (uygulamaIlkAyarlarYapildi)
					return;
				VeritabaniSabitler.VERITABANI_TIPI = (VeritabaniTipi)Enum.Parse(typeof(VeritabaniTipi), System.Configuration.ConfigurationManager.AppSettings["VeritabaniTipi"]);
				VeritabaniSabitler.VERITABANI_BAGLANTI_STRING = System.Configuration.ConfigurationManager.AppSettings["VeritabaniBaglantiString"];
				AAWSabitler.VeritabaniTipi = VeritabaniSabitler.VERITABANI_TIPI;
				AAWSabitler.VeritabaniBaglantiString = VeritabaniSabitler.VERITABANI_BAGLANTI_STRING;
				AAWSabitler.ElasticSearchUrl = System.Configuration.ConfigurationManager.AppSettings["ElasticSearchUrl"];
				AAWSabitler.ElasticSearchIndexAdi = "iam";
				//VeritabaniVekiliFabrikasi.Tip = VeritabaniVekilTipi.FoxPro;
				AdaSSOSabitler.ElasticSearchUrl = AAWSabitler.ElasticSearchUrl;
				AdaSSOSabitler.ElasticSearchIndexAdi = "adasso";
				uygulamaIlkAyarlarYapildi = true;
			}
		}

	}
}
