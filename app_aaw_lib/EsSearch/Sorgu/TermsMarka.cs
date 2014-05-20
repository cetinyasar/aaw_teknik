using System.Collections.Generic;

namespace app_aaw_lib.EsSearch.Sorgu
{
	public class TermsMarka : Terms
	{
		public static TermsMarka Olustur()
		{
			TermsMarka retVal = new TermsMarka();
			retVal.marka = new List<string>();
			return retVal;
		}

		public override void KriterEkle(string kriterAdi)
		{
			marka.Add(kriterAdi);
		}

		public override int AdetAl()
		{
			return marka.Count;
		}
	}
}