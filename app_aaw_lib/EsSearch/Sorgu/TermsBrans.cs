using System.Collections.Generic;

namespace app_aaw_lib.EsSearch.Sorgu
{
	public class TermsBrans : Terms
	{
		public static TermsPoliceGrubu Olustur()
		{
			TermsPoliceGrubu retVal = new TermsPoliceGrubu();
			retVal.policeGrubu = new List<string>();
			return retVal;
		}
	}
}