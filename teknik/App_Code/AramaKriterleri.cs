using System.Collections.Generic;

/// <summary>
/// Summary description for AramaKriterleri
/// </summary>
public class AramaKriterleri
{
	public string Query { get; set; }
	public SecilebilirKriter SecilebilirKriterler { get; set; }
	public AramaKriterleri()
	{
		

	}

	public static AramaKriterleri Olustur()
	{
		AramaKriterleri retVal = new AramaKriterleri();
		retVal.SecilebilirKriterler = SecilebilirKriter.Olustur();
		return retVal;
	}
}

public class SecilebilirKriter
{
	public List<Kriter> PoliceGrubu { get; set; }
	public List<Kriter> Brans { get; set; }
	public List<Kriter> Tali { get; set; }
	public List<Kriter> Satici { get; set; }
	public List<Kriter> Marka { get; set; }
	

	public static SecilebilirKriter Olustur()
	{
		SecilebilirKriter retVal = new SecilebilirKriter();
		retVal.PoliceGrubu = new List<Kriter>();
		retVal.Brans = new List<Kriter>();
		retVal.Tali = new List<Kriter>();
		retVal.Satici = new List<Kriter>();
		retVal.Marka = new List<Kriter>();
		return retVal;
	}
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