namespace app_aaw_lib.EsSearch.Sorgu
{
	public class EsAnd
	{
		public ITerms terms { get; set; }
		public EsRange range { get; set; }
	}
}