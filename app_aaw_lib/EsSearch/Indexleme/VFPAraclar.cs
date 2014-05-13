using System;

namespace app_aaw_lib.EsSearch.Indexleme
{
	internal class VFPAraclar
	{
		public static string sqlIcinTarihStrAl(DateTime tarih)
		{
			return "CTOD('" + tarih.Month.ToString().PadLeft(2, '0') + "/" + tarih.Day.ToString().PadLeft(2, '0') + "/" +
			       tarih.Year.ToString() + "')";
		}
	}
}