using System.Collections.Generic;
using System.Data;
using AdaGenel.Cesitli;
using AdaVeriKatmani;

namespace app_aaw_lib.EsSearch.Indexleme
{
	public abstract class PoliceIndexleHandler
	{
		protected TemelVeriIslemleri Tvi;
		//protected HttpResponse Response;

		protected IEnumerable<KayitAlan> KayitAlanOlustur(DataRow polRow, DataRow pol2Row, DataRow uruneOzelRow, DataRow drMus, DataRow drTa, DataRow drSat, DataRow drSor)
		{
			List<KayitAlan> retVal = new List<KayitAlan>();
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.PoliceGrubu, new astring() { Deger = polRow["POLGRP"].ToString() }, NormalizationTip.Normal));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.Brans, new astring() { Deger = polRow["BRANS"].ToString() }, NormalizationTip.Normal));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.SirketPoliceNo, new astring() { Deger = polRow["FSiRPOLNO"].ToString().Trim() }, NormalizationTip.Normal));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.SirketTecditNo, new astring() { Deger = polRow["TECDiT_NO"].ToString().Trim() }, NormalizationTip.Normal));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.SirketZeyilNo, new astring() { Deger = polRow["ZEYL_NO"].ToString().Trim() }, NormalizationTip.Normal));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.TanzimTarihi, new adatetime() { Deger = Araclar.ParseDateTime(polRow["FTNZTAR"].ToString()) }, NormalizationTip.Normal));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.VadeBaslangic, new adatetime() { Deger = Araclar.ParseDateTime(polRow["FBASTAR"].ToString()) }, NormalizationTip.Normal));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.VadeBitis, new adatetime() { Deger = Araclar.ParseDateTime(polRow["FBiTTAR"].ToString()) }, NormalizationTip.Normal));

			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.Plaka, new astring() { Deger = polRow["PLAKA"].ToString().Trim() }, NormalizationTip.Normal));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.PlakaIlKodu, new astring() { Deger = plakaIlKoduAl(polRow["PLAKA"].ToString().Trim()) }, NormalizationTip.Normal));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.Sigortali, new astring() { Deger = polRow["SiGADi"].ToString().Trim() }, NormalizationTip.NormalizedVeAcik));

			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.VergiKimlikNo, new astring() { Deger = polRow["fVerKimNo"].ToString().Trim() }, NormalizationTip.Normal));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.DaskPoliceNo, new astring() { Deger = polRow["fDskPolNo"].ToString().Trim() }, NormalizationTip.Normal));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.SirketDaskPoliceNo, new astring() { Deger = polRow["fSirDskPNo"].ToString().Trim() }, NormalizationTip.Normal));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.EskiPoliceNo, new astring() { Deger = polRow["fEskPolNo"].ToString().Trim() }, NormalizationTip.Normal));

			if (pol2Row != null)
				retVal.AddRange(pol2AlanlariniEkle(pol2Row));
			if (uruneOzelRow != null)
				retVal.AddRange(uruneOzelAlanlariEkle(polRow["POLGRP"].ToString(), uruneOzelRow));

			retVal.AddRange(musteriBilgileriniEkle(drMus));
			retVal.AddRange(taliBilgileriniEkle(drTa));
			retVal.AddRange(saticiBilgileriniEkle(drSat));
			retVal.AddRange(sorumluBilgileriniEkle(drSor));


			return retVal;
		}

		private string plakaIlKoduAl(string trim)
		{
			string retVal = "";
			foreach (char chr in trim)
			{
				if (char.IsDigit(chr))
					retVal += chr;
				else
					return retVal;
			}
			return retVal;
		}

		private IEnumerable<KayitAlan> musteriBilgileriniEkle(DataRow drMus)
		{
			if (drMus == null)
				return new List<KayitAlan>();

			List<KayitAlan> retVal = new List<KayitAlan>
			{
				KayitAlan.Olustur(PoliceTerimleri.MusteriPrk, new astring() {Deger = drMus["fprkmus"].ToString()}, NormalizationTip.Normal),
				KayitAlan.Olustur(PoliceTerimleri.MusteriMuhasebeHesapKodu, new astring() {Deger = drMus["fMusHesKod"].ToString()}, NormalizationTip.Normal), 
				KayitAlan.Olustur(PoliceTerimleri.MusteriAdi, new astring() {Deger = drMus["fMusAdi"].ToString()}, NormalizationTip.NormalizedVeAcik),
				KayitAlan.Olustur(PoliceTerimleri.MusteriSoyadi, new astring() {Deger = drMus["fMusSoyAdi"].ToString()}, NormalizationTip.NormalizedVeAcik),
				KayitAlan.Olustur(PoliceTerimleri.MusteriAdiSoyadi, new astring() {Deger = drMus["fMusAdi"].ToString().Trim() + " " + drMus["fMusSoyAdi"].ToString().Trim()}, NormalizationTip.NormalizedVeAcik), 
				KayitAlan.Olustur(PoliceTerimleri.MusteriUnvani, new astring() {Deger = drMus["fMusUnv"].ToString()}, NormalizationTip.NormalizedVeAcik),
				KayitAlan.Olustur(PoliceTerimleri.MusteriTcKimlikNo, new astring() {Deger = drMus["fMusTcKimN"].ToString()}, NormalizationTip.Normal),
				KayitAlan.Olustur(PoliceTerimleri.MusteriKullaniciHesapKodu, new astring() {Deger = drMus["fKulHesKod"].ToString()}, NormalizationTip.Normal), 
				KayitAlan.Olustur(PoliceTerimleri.MusteriDogumTarihi, new adatetime() {Deger = Araclar.ParseDateTime(drMus["fDogTar"].ToString())}, NormalizationTip.Normal)
			};
			return retVal;
		}

		private IEnumerable<KayitAlan> taliBilgileriniEkle(DataRow drTa)
		{
			if (drTa == null)
				return new List<KayitAlan>();

			List<KayitAlan> retVal = new List<KayitAlan>
			{
				KayitAlan.Olustur(PoliceTerimleri.TaliPrk, new astring() {Deger = drTa["fprkmus"].ToString()}, NormalizationTip.Normal), 
				KayitAlan.Olustur(PoliceTerimleri.TaliMuhasebeHesapKodu, new astring() {Deger = drTa["fMusHesKod"].ToString()}, NormalizationTip.Normal),
				KayitAlan.Olustur(PoliceTerimleri.TaliAdi, new astring() {Deger = drTa["fMusAdi"].ToString()}, NormalizationTip.NormalizedVeAcik), 
				KayitAlan.Olustur(PoliceTerimleri.TaliSoyadi, new astring() {Deger = drTa["fMusSoyAdi"].ToString()}, NormalizationTip.NormalizedVeAcik),
				KayitAlan.Olustur(PoliceTerimleri.TaliAdiSoyadi, new astring() {Deger = drTa["fMusAdi"].ToString().Trim() + " " + drTa["fMusSoyAdi"].ToString().Trim()}, NormalizationTip.NormalizedVeAcik), 
				KayitAlan.Olustur(PoliceTerimleri.TaliUnvani, new astring() {Deger = drTa["fMusUnv"].ToString()}, NormalizationTip.NormalizedVeAcik),
				KayitAlan.Olustur(PoliceTerimleri.TaliTcKimlikNo, new astring() {Deger = drTa["fMusTcKimN"].ToString()}, NormalizationTip.Normal),
				KayitAlan.Olustur(PoliceTerimleri.TaliKullaniciHesapKodu, new astring() {Deger = drTa["fKulHesKod"].ToString()}, NormalizationTip.Normal), 
				KayitAlan.Olustur(PoliceTerimleri.TaliDogumTarihi, new adatetime() {Deger = Araclar.ParseDateTime(drTa["fDogTar"].ToString())}, NormalizationTip.Normal)
			};
			return retVal;
		}

		private IEnumerable<KayitAlan> saticiBilgileriniEkle(DataRow drSat)
		{
			if (drSat == null)
				return new List<KayitAlan>();

			List<KayitAlan> retVal = new List<KayitAlan> {KayitAlan.Olustur(PoliceTerimleri.SaticiPrk, new astring() {Deger = drSat["fprkmus"].ToString()}, NormalizationTip.Normal), 
				KayitAlan.Olustur(PoliceTerimleri.SaticiMuhasebeHesapKodu, new astring() {Deger = drSat["fMusHesKod"].ToString()}, NormalizationTip.Normal), 
				KayitAlan.Olustur(PoliceTerimleri.SaticiAdi, new astring() {Deger = drSat["fMusAdi"].ToString()}, NormalizationTip.NormalizedVeAcik),
				KayitAlan.Olustur(PoliceTerimleri.SaticiSoyadi, new astring() {Deger = drSat["fMusSoyAdi"].ToString()}, NormalizationTip.NormalizedVeAcik),
				KayitAlan.Olustur(PoliceTerimleri.SaticiAdiSoyadi, new astring() {Deger = drSat["fMusAdi"].ToString().Trim() + " " + drSat["fMusSoyAdi"].ToString().Trim()}, NormalizationTip.NormalizedVeAcik), 
				KayitAlan.Olustur(PoliceTerimleri.SaticiUnvani, new astring() {Deger = drSat["fMusUnv"].ToString()}, NormalizationTip.NormalizedVeAcik),
				KayitAlan.Olustur(PoliceTerimleri.SaticiTcKimlikNo, new astring() {Deger = drSat["fMusTcKimN"].ToString()}, NormalizationTip.Normal),
				KayitAlan.Olustur(PoliceTerimleri.SaticiKullaniciHesapKodu, new astring() {Deger = drSat["fKulHesKod"].ToString()}, NormalizationTip.Normal), 
				KayitAlan.Olustur(PoliceTerimleri.SaticiDogumTarihi, new adatetime() {Deger = Araclar.ParseDateTime(drSat["fDogTar"].ToString())}, NormalizationTip.Normal)};
			return retVal;
		}

		private IEnumerable<KayitAlan> sorumluBilgileriniEkle(DataRow drSor)
		{
			if (drSor == null)
				return new List<KayitAlan>();

			List<KayitAlan> retVal = new List<KayitAlan>
			{
				KayitAlan.Olustur(PoliceTerimleri.SorumluPrk, new astring() {Deger = drSor["fprkmus"].ToString()}, NormalizationTip.Normal), 
				KayitAlan.Olustur(PoliceTerimleri.SorumluMuhasebeHesapKodu, new astring() {Deger = drSor["fMusHesKod"].ToString()}, NormalizationTip.Normal), 
				KayitAlan.Olustur(PoliceTerimleri.SorumluAdi, new astring() {Deger = drSor["fMusAdi"].ToString()}, NormalizationTip.NormalizedVeAcik), 
				KayitAlan.Olustur(PoliceTerimleri.SorumluSoyadi, new astring() {Deger = drSor["fMusSoyAdi"].ToString()}, NormalizationTip.NormalizedVeAcik), 
				KayitAlan.Olustur(PoliceTerimleri.SorumluAdiSoyadi, new astring() {Deger = drSor["fMusAdi"].ToString().Trim() + " " + drSor["fMusSoyAdi"].ToString().Trim()}, NormalizationTip.NormalizedVeAcik), 
				KayitAlan.Olustur(PoliceTerimleri.SorumluUnvani, new astring() {Deger = drSor["fMusUnv"].ToString()}, NormalizationTip.NormalizedVeAcik), 
				KayitAlan.Olustur(PoliceTerimleri.SorumluTcKimlikNo, new astring() {Deger = drSor["fMusTcKimN"].ToString()}, NormalizationTip.Normal), 
				KayitAlan.Olustur(PoliceTerimleri.SorumluKullaniciHesapKodu, new astring() {Deger = drSor["fKulHesKod"].ToString()}, NormalizationTip.Normal), 
				KayitAlan.Olustur(PoliceTerimleri.SorumluDogumTarihi, new adatetime() {Deger = Araclar.ParseDateTime(drSor["fDogTar"].ToString())}, NormalizationTip.Normal)
			};
			return retVal;
		}

		private IEnumerable<KayitAlan> uruneOzelAlanlariEkle(string polGrp, DataRow urunRow)
		{
			if (polGrp == "KKO")
				return kkoAlanlariniEkle(urunRow);
			if (polGrp == "TRF")
				return trfAlanlariniEkle(urunRow);
			return new List<KayitAlan>();
		}

		private IEnumerable<KayitAlan> pol2AlanlariniEkle(DataRow pol2DataRow)
		{
			List<KayitAlan> retVal = new List<KayitAlan>();
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.Ilce, new astring() { Deger = pol2DataRow["ilce"].ToString() }, NormalizationTip.NormalizedVeAcik));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.RizikoIlce, new astring() { Deger = pol2DataRow["rizilce"].ToString() }, NormalizationTip.NormalizedVeAcik));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.RizikoIl, new astring() { Deger = pol2DataRow["rizil"].ToString() }, NormalizationTip.NormalizedVeAcik));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.TcKimlikNo, new astring() { Deger = pol2DataRow["fTcKimNo"].ToString() }, NormalizationTip.Normal));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.SigortaEttiren, new astring() { Deger = pol2DataRow["fSigEtt"].ToString() }, NormalizationTip.NormalizedVeAcik));
			return retVal;
		}
		private IEnumerable<KayitAlan> kkoAlanlariniEkle(DataRow kkoRow)
		{
			List<KayitAlan> retVal = new List<KayitAlan>();
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.AracKodu, new astring() { Deger = kkoRow["fArcKod"].ToString() }, NormalizationTip.Normal));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.Marka, new astring() { Deger = kkoRow["Marka"].ToString() }, NormalizationTip.NormalizedVeAcik));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.Model, new astring() { Deger = kkoRow["Model"].ToString() }, NormalizationTip.NormalizedVeAcik));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.ModelYili, new astring() { Deger = kkoRow["ModYil"].ToString() }, NormalizationTip.Normal));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.MotorNo, new astring() { Deger = kkoRow["Motor_No"].ToString() }, NormalizationTip.Normal));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.SasiNo, new astring() { Deger = kkoRow["Sasi_No"].ToString() }, NormalizationTip.Normal));
			return retVal;
		}
		private IEnumerable<KayitAlan> trfAlanlariniEkle(DataRow trfRow)
		{
			List<KayitAlan> retVal = new List<KayitAlan>();
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.AracKodu, new astring() { Deger = trfRow["fArcKod"].ToString() }, NormalizationTip.Normal));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.Marka, new astring() { Deger = trfRow["Marka"].ToString() }, NormalizationTip.NormalizedVeAcik));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.Model, new astring() { Deger = trfRow["Model"].ToString() }, NormalizationTip.NormalizedVeAcik));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.ModelYili, new astring() { Deger = trfRow["ModYil"].ToString() }, NormalizationTip.Normal));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.MotorNo, new astring() { Deger = trfRow["Motor_No"].ToString() }, NormalizationTip.Normal));
			retVal.Add(KayitAlan.Olustur(PoliceTerimleri.SasiNo, new astring() { Deger = trfRow["Sasi_No"].ToString() }, NormalizationTip.Normal));
			return retVal;
		}
	}
}