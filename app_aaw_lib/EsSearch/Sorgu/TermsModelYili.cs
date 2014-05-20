using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_aaw_lib.EsSearch.Sorgu
{
	public class TermsModelYili : Terms
	{
		public static TermsModelYili Olustur()
		{
			TermsModelYili retVal = new TermsModelYili();
			retVal.modelYili = new List<string>();
			return retVal;
		}

		public override void KriterEkle(string kriterAdi)
		{
			modelYili.Add(kriterAdi);
		}

		public override int AdetAl()
		{
			return modelYili.Count;
		}
	}
}