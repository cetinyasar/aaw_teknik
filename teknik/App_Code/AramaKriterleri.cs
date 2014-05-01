using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AramaKriterleri
/// </summary>
public class AramaKriterleri
{
	public string Baslik { get; set; }
	public Guvenlik Guvenlik {get;set;}
	public string Tipi { get; set; }
	public string Sorgu { get; set; }
	public DateTime TanzimTarihiBaslangic { get; set; }
	public DateTime TanzimTarihiBitis { get; set; }
	public string BransBaslangic { get; set; }
	public string BransBitis { get; set; }
	public string PlakaIlKodu { get; set; }
	public string ModelYili { get; set; }
	public string Marka { get; set; }
	public string Tip { get; set; }
	public string SigortaliIlKodu { get; set; }

	public AramaKriterleri()
	{
		

	}
}

public class Guvenlik
{
}