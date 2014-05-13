namespace app_aaw_lib.EsSearch.Sorgu
{
	public class EsRange
	{
		public EsTanzimTarihi tanzimTarihi { get; set; }

		public static EsRange Olustur()
		{
			EsRange retVal = new EsRange();
			retVal.tanzimTarihi = new EsTanzimTarihi();
			return retVal;
		}
	}
}