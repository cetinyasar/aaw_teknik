using System.Collections.Generic;

namespace app_aaw_lib.EsSearch.Sorgu
{
	public class ElasticSearchGet
	{
		public int from { get; set; }
		public int size { get; set; }
		public List<string> fields { get; set; }
		public Query query { get; set; }
		public EsFilter filter { get; set; }
		public EsFacets facets { get; set; }
	}
}