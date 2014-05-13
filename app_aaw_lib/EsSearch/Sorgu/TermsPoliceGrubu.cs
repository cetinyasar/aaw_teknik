using System.Collections.Generic;

namespace app_aaw_lib.EsSearch.Sorgu
{
	public class TermsPoliceGrubu : Terms
	{
		public static TermsPoliceGrubu Olustur()
		{
			TermsPoliceGrubu retVal = new TermsPoliceGrubu();
			retVal.policeGrubu = new List<string>();
			return retVal;
		}
	}
}