namespace app_aaw_lib.EsSearch.Sorgu
{
	public class QueryString
	{
		public string query { get; set; }

		public static QueryString Olustur()
		{
			QueryString retVal = new QueryString();
			retVal.query = "";
			return retVal;
		}
	}
}