using System.Collections.Generic;
using System.Data;
using AdaVeriKatmani;
using app_aaw_lib.EsSearch.Indexleme;
using Newtonsoft.Json.Linq;

public class PoliceTekIndexleHandler : PoliceIndexleHandler, ICariApiIstekHandler
{
	public PoliceTekIndexleHandler()
	{
	}

	public string IstekIsle(TemelVeriIslemleri temelVeriIslemleri, GelenIstekDetay istekDetay, AAWIndexGuncellemeMotoru igm)
	{
		Tvi = temelVeriIslemleri;
		//Response = response;

		primaryKeyeGoreKayitAlanOlustur(istekDetay, igm);
		return "";
	}

	private void primaryKeyeGoreKayitAlanOlustur(GelenIstekDetay istekDetay, AAWIndexGuncellemeMotoru igm)
	{
		IndexMetadata im = new IndexMetadata { KayitTipiAdi = "police", Alanlar = new List<KayitAlan>() };

		JArray keyler = (JArray)istekDetay.Veri["fPrkPol"];
		foreach (int prk in keyler)
		{
			im.Id = prk;
			DataTable dtPol = Tvi.Doldur("SELECT * FROM POL WHERE FPRKPOL = " + prk);
			DataTable dtPol2 = Tvi.Doldur("SELECT * FROM POL2 WHERE FPRKPOL = " + prk);
			if (dtPol.Rows.Count == 1)
			{
				DataRow pol2DataRow = dtPol2.Rows[0];
				DataTable dtUruneOzel = Tvi.Doldur("SELECT * FROM " + dtPol.Rows[0]["PolGrp"].ToString() + " WHERE FPRKPOL = " + prk);
				DataRow drUruneOzel = null;
				if (dtUruneOzel.Rows.Count == 1)
					drUruneOzel = dtUruneOzel.Rows[0];

				DataTable dtMus = Tvi.Doldur("SELECT * FROM MUS WHERE FPRKMUS = " + dtPol.Rows[0]["FFRKMUS"].ToString());
				DataRow drMus = null;
				if (dtMus.Rows.Count == 1)
					drMus = dtMus.Rows[0];
				DataTable dtTa = Tvi.Doldur("SELECT * FROM MUS WHERE FPRKMUS = " + dtPol.Rows[0]["FFRKTA"].ToString());
				DataRow drTa = null;
				if (dtTa.Rows.Count == 1)
					drTa = dtTa.Rows[0];
				DataTable dtSat = Tvi.Doldur("SELECT * FROM MUS WHERE FPRKMUS = " + dtPol.Rows[0]["FFRKSAT"].ToString());
				DataRow drSat = null;
				if (dtSat.Rows.Count == 1)
					drSat = dtSat.Rows[0];
				DataTable dtSor = Tvi.Doldur("SELECT * FROM MUS WHERE FPRKMUS = " + dtPol.Rows[0]["FFRKSOR"].ToString());
				DataRow drSor = null;
				if (dtSor.Rows.Count == 1)
					drSor = dtSor.Rows[0];

				im.Alanlar.AddRange(KayitAlanOlustur(dtPol.Rows[0], pol2DataRow, drUruneOzel, drMus, drTa, drSat, drSor, new Dictionary<int,app_aaw_lib.Cesitli.Sirket>()));
				igm.Guncelle(im, false);

			}
		}

	}
}