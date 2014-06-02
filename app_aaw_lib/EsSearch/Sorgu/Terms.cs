using System.Collections.Generic;

namespace app_aaw_lib.EsSearch.Sorgu
{
	public abstract class Terms : ITerms
	{
		public List<string> policeGrubu { get; set; }
		public List<string> marka { get; set; }
		public List<string> brans { get; set; }
		public List<string> modelYili { get; set; }
		public List<string> tali { get; set; }
		public List<string> satici { get; set; }
		public List<string> sorumlu { get; set; }
		public List<string> sirketAdi { get; set; }
		public abstract void KriterEkle(string kriterAdi);
		public abstract int AdetAl();
	}

	public interface ITerms
	{
		void KriterEkle(string kriterAdi);
		int AdetAl();
	}

}