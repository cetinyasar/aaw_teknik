using System.Collections.Generic;

namespace app_aaw_lib.EsSearch.Indexleme
{
	public class GelenIstekDetay
	{
		public GelenIstekDetay()
		{
			Guvenlik = new HandlerGuvenlik();
			Veri = new Dictionary<string, object>();
		}
		public HandlerGuvenlik Guvenlik = new HandlerGuvenlik();
		public string Tipi { get; set; }
		public Dictionary<string, object> Veri;
		//public int SayfaNo { get; set; }
	}
}