using System;
using System.Collections.Generic;

namespace app_aaw_lib
{
	/// <summary>
	/// Summary description for AramaKriterleri
	/// </summary>
	public class AramaKriterleri
	{
		public int From { get; set; }
		public int Size { get; set; }
		public string Query { get; set; }
		public SecilebilirKriter SecilebilirKriterler { get; set; }
		public AramaKriterleri()
		{
			From = 0;
			Size = 100;
		}

		public static AramaKriterleri Olustur()
		{
			AramaKriterleri retVal = new AramaKriterleri();
			retVal.From = 0;
			retVal.Size = 100;
			retVal.SecilebilirKriterler = SecilebilirKriter.Olustur();
			return retVal;
		}
	}

	public class SecilebilirKriter
	{
		public List<Kriter> PoliceGrubu { get; set; }
		public List<Kriter> Brans { get; set; }
		public List<Kriter> Marka { get; set; }
		public List<Kriter> ModelYili { get; set; }
		public List<Kriter> Tali { get; set; }
		public List<Kriter> Satici { get; set; }
		public List<Kriter> Sorumlu { get; set; }
		
		public BaslangicBitisTarihi TanzimTarihAraligi { get; set; }

		public SecilebilirKriter()
		{
			TanzimTarihAraligi = new BaslangicBitisTarihi();
		}

	

		public static SecilebilirKriter Olustur()
		{
			SecilebilirKriter retVal = new SecilebilirKriter();
			retVal.PoliceGrubu = new List<Kriter>();
			retVal.Brans = new List<Kriter>();
			retVal.Marka = new List<Kriter>();
			retVal.ModelYili = new List<Kriter>();
			retVal.Tali = new List<Kriter>();
			retVal.Satici = new List<Kriter>();
			retVal.Sorumlu = new List<Kriter>();

			retVal.TanzimTarihAraligi = new BaslangicBitisTarihi();
			return retVal;
		}
	}

	public class BaslangicBitisTarihi
	{
		public DateTime IlkTarih { get; set; }
		public DateTime SonTarih { get; set; }
	}

	public class Kriter
	{
		public bool Secili { get; set; }
		public string Adi { get; set; }
		public int Adet { get; set; }
	}

	public class Guvenlik
	{
	}
}