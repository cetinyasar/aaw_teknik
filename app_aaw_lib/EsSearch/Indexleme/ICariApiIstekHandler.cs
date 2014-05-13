using AdaVeriKatmani;

namespace app_aaw_lib.EsSearch.Indexleme
{
	public interface ICariApiIstekHandler
	{
		string IstekIsle(TemelVeriIslemleri temelVeriIslemleri, GelenIstekDetay istekDetay, AAWIndexGuncellemeMotoru igm);
	}
}