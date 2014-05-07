using System.Collections.Generic;

/// <summary>
/// Summary description for AramaKriterleri
/// </summary>
public class AramaKriterleri
{
	public string Query { get; set; }
	public SecilebilirKriter SecilebilirKriterler { get; set; }
	public SecilebilirKriter SeciliKriterler { get; set; }
	public AramaKriterleri()
	{
		

	}

	public static AramaKriterleri Olustur()
	{
		AramaKriterleri retVal = new AramaKriterleri();
		retVal.SecilebilirKriterler = SecilebilirKriter.Olustur();
		retVal.SeciliKriterler = SecilebilirKriter.Olustur();
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
	public int Adet { get; set; }
}

public class Guvenlik
{
}