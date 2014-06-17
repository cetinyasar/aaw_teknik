using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdaHttpHandler;
using AdaHttpHandler.Olay;
using app_aaw_lib.Cesitli;

namespace app_aaw_lib.HttpHandlers.Olay
{
	[AdaHttpHandlerOlayYoneticisi]
	public class AppHttpHandlerOlay
	{
		[AdaHttpHandlerKontrolMetodu]
		public static HandlerOncesiKontrolSonucu IAMHandlerOncesiKontrolYap(HttpHandlerBilgi handlerBilgileri)
		{
			UygulamaAyarYoneticisi.YapilmadiysaUygulamaIlkAyarlariYap();
			HandlerOncesiKontrolSonucu retVal = new HandlerOncesiKontrolSonucu { Basarili = true };
			OrtakKontrolMotoru ortakKontrolMotoru = new OrtakKontrolMotoru();
			if (!ortakKontrolMotoru.LoginKontroluYap(handlerBilgileri, AAWSessionNesneleri.Kullanici))
				retVal = new HandlerOncesiKontrolSonucu { Basarili = false, Mesaj = "Login olmalısınız" };
			if (retVal.Basarili && !ortakKontrolMotoru.RolKontroluYap(handlerBilgileri, AAWSessionNesneleri.Kullanici))
				retVal = new HandlerOncesiKontrolSonucu { Basarili = false, Mesaj = "İşlemi yapmak için gerekli role sahip değilsiniz" };
			return retVal;
		}

		public static string OnemsizLockNesnesi = "frqwedffasdf";

	}
}
