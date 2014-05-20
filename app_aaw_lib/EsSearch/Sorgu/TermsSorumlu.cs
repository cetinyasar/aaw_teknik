using System.Collections.Generic;

namespace app_aaw_lib.EsSearch.Sorgu
{
	public class TermsSorumlu : Terms
	{
		public static TermsSorumlu Olustur()
		{
			TermsSorumlu retVal = new TermsSorumlu();
			retVal.sorumlu = new List<string>();
			return retVal;
		}

		public override void KriterEkle(string kriterAdi)
		{
			sorumlu.Add(kriterAdi);
		}

		public override int AdetAl()
		{
			return sorumlu.Count;

		}
	}
}