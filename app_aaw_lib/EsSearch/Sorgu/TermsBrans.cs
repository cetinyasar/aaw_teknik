using System.Collections.Generic;

namespace app_aaw_lib.EsSearch.Sorgu
{
	public class TermsBrans : Terms
	{
		public static TermsBrans Olustur()
		{
			TermsBrans retVal = new TermsBrans();
			retVal.brans = new List<string>();
			return retVal;
		}

		public override void KriterEkle(string kriterAdi)
		{
			brans.Add(kriterAdi);
		}

		public override int AdetAl()
		{
			return brans.Count;
		}
	}
}