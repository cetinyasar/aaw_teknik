using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdaGenel.Cesitli;
using AdaVeriKatmani;

namespace app_aaw_lib.Cesitli
{
	public class AAWSabitler
	{
		public static string ElasticSearchIndexAdi = "aaw";
		public static string ElasticSearchUrl;
		public static Encoding Encoding = Araclar.TurkceEncodingAl();
		public static string VeritabaniBaglantiString;
		public static VeritabaniTipi VeritabaniTipi;

		public static TemelVeriIslemleri VeriIslemleriOlustur()
		{
			return new TemelVeriIslemleri(VeritabaniTipi, VeritabaniBaglantiString);
		}
	}
}
