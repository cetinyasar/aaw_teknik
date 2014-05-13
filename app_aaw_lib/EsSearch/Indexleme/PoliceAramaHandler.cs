////using AdaVeriKatmani;
////using app_aaw_lib.EsSearch.Indexleme;

////public class PoliceAramaHandler : ICariApiIstekHandler
////{
////	private readonly PoliceAramaSorguAyristiricisi _ayristirici = new PoliceAramaSorguAyristiricisi();
////	public PoliceAramaHandler() { }

////	public string IstekIsle(TemelVeriIslemleri temelVeriIslemleri, GelenIstekDetay istekDetay, AAWIndexGuncellemeMotoru igm)
////	{
////		string sorgu = (string)istekDetay.Veri["sorgu"];
////		PoliceAramaKriterler kriterler = _ayristirici.KriterleriOlustur(sorgu);
////		PoliceAramaMotoru pam = new PoliceAramaMotoru();
////		string retVal = pam.AramaYap(kriterler);
////		return retVal;
////	}
////}

//using AdaVeriKatmani;
//using app_aaw_lib.EsSearch.Indexleme;

//public class PoliceAramaHandler : ICariApiIstekHandler
//{
//	private readonly PoliceAramaSorguAyristiricisi _ayristirici = new PoliceAramaSorguAyristiricisi();
//	public PoliceAramaHandler() { }

//	public string IstekIsle(TemelVeriIslemleri temelVeriIslemleri, GelenIstekDetay istekDetay, AAWIndexGuncellemeMotoru igm)
//	{
//		string sorgu = (string)istekDetay.Veri["sorgu"];
//		PoliceAramaKriterler kriterler = _ayristirici.KriterleriOlustur(sorgu);
//		PoliceAramaMotoru pam = new PoliceAramaMotoru();
//		string retVal = pam.AramaYap(kriterler);
//		return retVal;
//	}
//}