using System.Collections.Generic;

namespace app_aaw_lib.EsSearch.Sorgu
{
	public class TermsTali : Terms
	{
		public static TermsTali Olustur()
		{
			TermsTali retVal = new TermsTali();
			retVal.tali = new List<string>();
			return retVal;
		}

		public override void KriterEkle(string kriterAdi)
		{
			tali.Add(kriterAdi);
		}

		public override int AdetAl()
		{
			return tali.Count;
		}
	}
}