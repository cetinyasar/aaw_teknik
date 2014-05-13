using AdaVeriKatmani;
using app_aaw_lib.EsSearch.Indexleme;

public class TumIndexSilHandler : PoliceIndexleHandler, ICariApiIstekHandler
{
	public TumIndexSilHandler() { }

	public string IstekIsle(TemelVeriIslemleri temelVeriIslemleri, GelenIstekDetay istekDetay, AAWIndexGuncellemeMotoru igm)
	{
		Tvi = temelVeriIslemleri;
		//Response = response;
		igm.TumIndexleriSil(false);
		return "";
	}

}