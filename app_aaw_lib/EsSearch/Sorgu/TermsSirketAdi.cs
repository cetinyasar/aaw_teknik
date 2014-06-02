using System.Collections.Generic;

namespace app_aaw_lib.EsSearch.Sorgu
{
	public class TermsSirketAdi : Terms
	{
		public static TermsSirketAdi Olustur()
		{
			TermsSirketAdi retVal = new TermsSirketAdi();
			retVal.sirketAdi = new List<string>();
			return retVal;
		}

		public override void KriterEkle(string kriterAdi)
		{
			marka.Add(kriterAdi);
		}

		public override int AdetAl()
		{
			return sirketAdi.Count;
		}
	}
}