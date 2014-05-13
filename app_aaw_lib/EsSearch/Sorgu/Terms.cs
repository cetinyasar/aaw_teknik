using System.Collections.Generic;

namespace app_aaw_lib.EsSearch.Sorgu
{
	public abstract class Terms
	{
		public List<string> policeGrubu { get; set; }
		public List<string> marka { get; set; }
		public List<string> brans { get; set; }
	}
}