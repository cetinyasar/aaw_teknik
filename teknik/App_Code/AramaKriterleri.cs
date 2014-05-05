using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AramaKriterleri
/// </summary>
public class AramaKriterleri
{
	public SecilebilirKriter SecilebilirKriterler { get; set; }
	//public string Sonuc { get; set; }
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

	public static SecilebilirKriter Olustur()
	{
		SecilebilirKriter retVal = new SecilebilirKriter();
		retVal.PoliceGrubu = new List<Kriter>();
		return retVal;
	}
}

public class Kriter
{
	public bool Secili { get; set; }
	public string Adi { get; set; }
	public int Count { get; set; }
}

public class Guvenlik
{
}