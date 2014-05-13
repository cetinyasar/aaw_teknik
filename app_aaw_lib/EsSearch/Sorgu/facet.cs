namespace app_aaw_lib.EsSearch.Sorgu
{
	public abstract class facet
	{
		public FacetTerms terms { get; set; }
		public FacetFacetFilter facet_filter { get; set; }
	}
}