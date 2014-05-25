using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdaHttpHandler;
using AdaVeriKatmani;
using app_aaw_lib.EsSearch.Indexleme;

namespace app_aaw_lib.EsSearch
{
	[AdaHttpHandlerClass("AyarHttpHandler")]
	public class AyarHttpHandler
	{
		[AdaHttpHandlerMethod("AyarlariKaydet")]
		public string AyarlariKaydet(Ayarlar ayar)
		{
			return ayar.AramaSunucuAdresi;
		}

		[AdaHttpHandlerMethod("IndexOlustur")]
		public string IndexOlustur(IndexOlusturmaKriterleri ayar)
		{
			//ICariApiIstekHandler handler = handlerYarat(iVeri);
			AAWIndexGuncellemeMotoru igm = new AAWIndexGuncellemeMotoru("http://localhost:9200");
			//string sonuc = handler.IstekIsle(veriIslemleriOlustur(), iVeri, igm);
			PoliceTopluIndexleHandler handler = new PoliceTopluIndexleHandler();
			string sonuc = handler.IstekIsle(veriIslemleriOlustur(), igm, ayar.BaslangicTanzimTarihi, ayar.BitisTanzimTarihi);
			return sonuc;
		}

		[AdaHttpHandlerMethod("TumIndexSil")]
		public string TumIndexSil()
		{
			//ICariApiIstekHandler handler = handlerYarat(iVeri);
			AAWIndexGuncellemeMotoru igm = new AAWIndexGuncellemeMotoru("http://localhost:9200");
			//string sonuc = handler.IstekIsle(veriIslemleriOlustur(), iVeri, igm);
			TumIndexSilHandler handler = new TumIndexSilHandler();
			string sonuc = handler.IstekIsle(veriIslemleriOlustur(), igm);
			return sonuc;
		}

		private static TemelVeriIslemleri veriIslemleriOlustur()
		{
			string connectionString = "Provider=vfpoledb.1;Collating Sequence=TURKISH;DATE=BRITISH;connection Timeout=1200;Data Source=D:\\Evrim\\ADADATA.DBC";
			VeritabaniTipi veritabaniTipi = VeritabaniTipi.FoxPro;
			return new TemelVeriIslemleri(veritabaniTipi, connectionString);
		}
	}



	public class IndexOlusturmaKriterleri
	{
		public DateTime BaslangicTanzimTarihi = DateTime.Now;
		public DateTime BitisTanzimTarihi = DateTime.Now;
	}

	public class Ayarlar
	{
		public bool IndexOlusturuluyor = false;
		public string AramaSunucuAdresi = "";
		public int SayfadaGosterilecekSonucAdedi = 0;
		public string ListelenecekKolonlar = "";
		public int GruplardaGosterilecekAdet = 0;
	}
}
