using System.Collections.Generic;

namespace app_aaw_lib.EsSearch.Sorgu
{
	public class TermsSatici : Terms
	{
		public static TermsSatici Olustur()
		{
			TermsSatici retVal = new TermsSatici();
			retVal.satici = new List<string>();
			return retVal;
		}

		public override void KriterEkle(string kriterAdi)
		{
			satici.Add(kriterAdi);
		}

		public override int AdetAl()
		{
			return satici.Count;
		}
	}
}