using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AdaKullaniciYonetimi;
using AdaSSO;

namespace app_aaw_lib.Cesitli
{
	public class AAWSessionNesneleri
	{
		public static Kullanici Kullanici
		{
			get
			{
				if (!buRequestIcinOncedenKontrolYapildi())
				{
					SSOYonetici yonetici = new SSOYonetici(new ElasticSearchIletisimcisi());
					LoginAramaSonuc sonuc = yonetici.LoginAl(new HttpRequestAyrinti().Doldur(HttpContext.Current.Request));
					HttpContext.Current.Items.Add("AAW_Kullanici", sonuc.LoginBulundu ? veritabanindanBilgiIleKullaniciNesnesiOlustur(sonuc.LoginBilgi.KullaniciId) : new Kullanici());
				}
				return (Kullanici)HttpContext.Current.Items["AAW_Kullanici"];
			}
		}

		private static Kullanici veritabanindanBilgiIleKullaniciNesnesiOlustur(int kullaniciId)
		{
			Kullanici kullanici = new Kullanici();
			Kullanici.KullaniciDoldur(kullaniciId, kullanici, AAWSabitler.VeriIslemleriOlustur());
			return kullanici;
		}

		private static bool buRequestIcinOncedenKontrolYapildi()
		{
			return HttpContext.Current.Items["AAW_Kullanici"] != null;
		}

	}
}
