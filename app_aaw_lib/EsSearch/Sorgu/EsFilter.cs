using System.Collections.Generic;

namespace app_aaw_lib.EsSearch.Sorgu
{
	public class EsFilter
	{
		public List<EsAnd> and { get; set; }
		
		public static EsFilter Olustur()
		{
			EsFilter retVal = new EsFilter();
			retVal.and = new List<EsAnd>();
			return retVal;
		}
	}
}