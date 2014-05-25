using AdaVeriKatmani;
using app_aaw_lib.EsSearch.Indexleme;

public class TumIndexSilHandler : PoliceIndexleHandler //, ICariApiIstekHandler
{
	public TumIndexSilHandler() { }

	public string IstekIsle(TemelVeriIslemleri temelVeriIslemleri, AAWIndexGuncellemeMotoru igm)
	{
		Tvi = temelVeriIslemleri;
		igm.TumIndexleriSil(false);
		return "";
	}

}