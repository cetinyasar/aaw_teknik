using System.Collections.Generic;

namespace app_aaw_lib.EsSearch.Indexleme
{
	public enum NormalizationTip { Normal, SadeceNormalized, NormalizedVeAcik }
	public enum AlanTipi { String, DateTime, Int }

	public class IndexMetadata
	{
		public int Id;
		public string KayitTipiAdi;
		public List<KayitAlan> Alanlar;
	}
}
