using System;
using AdaPublicGenel.Extensions;

namespace app_aaw_lib.EsSearch.Indexleme
{
	public class KayitAlan
	{
		public string AlanAdi;
		public IKayitAlanTip Deger { get; set; }
		public NormalizationTip NT;
		public static KayitAlan Olustur(string alanAdi, IKayitAlanTip deger, NormalizationTip normalizationTip)
		{
			return new KayitAlan { AlanAdi = alanAdi, Deger = deger, NT = normalizationTip };
		}
	}

	public interface IKayitAlanTip
	{
		string JsonOlustur(NormalizationTip normalizationTipi, string alanAdi);
	}

	public class astring : IKayitAlanTip
	{
		public string Deger { get; set; }
		public string JsonOlustur(NormalizationTip normalizationTipi, string alanAdi)
		{
			string retVal = "";
			if (this.Deger.ToString().Trim() != "")
			{
				if (normalizationTipi == NormalizationTip.Normal)
					retVal += "\"" + alanAdi + "\":\"" + this.Deger.ESIllegalKarakterTemizle() + "\"";
				if (normalizationTipi == NormalizationTip.NormalizedVeAcik)
					retVal += "\"" + alanAdi + "\":\"" + this.Deger.IleriNormalization() + "\",\"" + alanAdi + "Acik" + "\":\"" + this.Deger.ESIllegalKarakterTemizle() + "\"";
			}

			return retVal;
		}
	}

	public class aint : IKayitAlanTip
	{
		public int Deger { get; set; }
		public string JsonOlustur(NormalizationTip normalizationTipi, string alanAdi)
		{
			return "\"" + alanAdi + "\":\"" + this.Deger.ToString() + "\"";
		}
	}

	public class adatetime : IKayitAlanTip
	{
		public DateTime Deger { get; set; }
		public string JsonOlustur(NormalizationTip normalizationTipi, string alanAdi)
		{
			return "\"" + alanAdi + "\":\"" + this.Deger.ESTarihStringAl() + "\"";
		}
	}
}
