namespace app_aaw_lib.EsSearch.Sorgu
{
	public class Query
	{
		public QueryString query_string { get; set; }

		public static Query Olustur()
		{
			Query retVal = new Query();
			retVal.query_string = QueryString.Olustur();
			return retVal;
		}
	}
}