using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_aaw_lib.Cesitli
{
	public class Parametre
	{
		//public static string Al(string ad, KullaniciParametreTip tip, int kullaniciId)
		public static string Al(string ad)
		{
			//string sql = "SELECT * FROM " + IAVeriAraclar.IA_KULLANICI_PARAMETRE + " WHERE " + IAVeriAraclar.AD + " = :1  AND  " + IAVeriAraclar.KULLANICI_ID + " = :2 ";
			//List<object> sqlParametreler = new List<object> { ad, kullaniciId };
			//if (tip != null)
			//{
			//	sql += " AND " + IAVeriAraclar.TIP + " = :3 ";
			//	sqlParametreler.Add(tip.ToString());
			//}
			//DataTable dt = VeriIslemleri.Doldur(sql, sqlParametreler.ToArray());
			//if (dt.Rows.Count != 1)
			//{
			//	return "";
			//}
			//IAMKullaniciParametre retVal = new IAMKullaniciParametre();
			//retVal.doldur(dt.Rows[0]);
			//return retVal.Deger;
			return "";
		}
	}
	public enum KullaniciParametreTip
	{
		GorevListeSira,
		GorevListeDeadlineFiltre,
		OrtakGenel,
		OrtakLisans,
		Diger
	}

}
