using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdaGenel.Cesitli;
using AdaVeriKatmani;
using Newtonsoft.Json.Linq;

namespace app_aaw_lib.EsSearch.Indexleme
{
	public class PoliceTopluIndexleHandler : PoliceIndexleHandler, ICariApiIstekHandler
	{

		public PoliceTopluIndexleHandler()
		{

		}

		public string IstekIsle(TemelVeriIslemleri temelVeriIslemleri, GelenIstekDetay istekDetay, AAWIndexGuncellemeMotoru igm)
		{
			Tvi = temelVeriIslemleri;
			//Response = response;

			tanzimTarihineGoreKayitAlanOlustur(istekDetay, igm);
			return "";
		}

		private void tanzimTarihineGoreKayitAlanOlustur(GelenIstekDetay istekDetay, AAWIndexGuncellemeMotoru igm)
		{
			JArray keyler = (JArray)istekDetay.Veri["fTnzTar"];
			DateTime tanzim1 = (DateTime)keyler[0];
			DateTime tanzim2 = (DateTime)keyler[1];

			string polKomut = "SELECT * FROM POL WHERE FTNZTAR BETWEEN " + VFPAraclar.sqlIcinTarihStrAl(tanzim1) + " and " + VFPAraclar.sqlIcinTarihStrAl(tanzim2);
			DataTable dtPol = Tvi.Doldur(polKomut + " ORDER BY FPRKPOL");
			DataTable dtPol2 = Tvi.Doldur("SELECT * FROM POL2 WHERE EXISTS(" + polKomut.Replace("SELECT *", "SELECT '.'") + " AND POL.FPRKPOL = POL2.FPRKPOL)");
			DataTable dtKko = Tvi.Doldur("SELECT * FROM KKO WHERE EXISTS(" + polKomut.Replace("SELECT *", "SELECT '.'") + " AND POL.FPRKPOL = KKO.FPRKPOL)");
			DataTable dtTrf = Tvi.Doldur("SELECT * FROM TRF WHERE EXISTS(" + polKomut.Replace("SELECT *", "SELECT '.'") + " AND POL.FPRKPOL = TRF.FPRKPOL)");

			DataTable dtMus = Tvi.Doldur("SELECT FPRKMUS,FMUSHESKOD,FMUSADi,FMUSSOYADi,FMUSUNV,FMUSTCKiMN,FKULHESKOD,FDOGTAR FROM MUS WHERE EXISTS(" + polKomut.Replace("SELECT *", "SELECT '.'") + " AND POL.FFRKMUS = MUS.FPRKMUS)");
			DataTable dtTa = Tvi.Doldur("SELECT FPRKMUS,FMUSHESKOD,FMUSADi,FMUSSOYADi,FMUSUNV,FMUSTCKiMN,FKULHESKOD,FDOGTAR FROM MUS WHERE EXISTS(" + polKomut.Replace("SELECT *", "SELECT '.'") + " AND POL.FFRKTA = MUS.FPRKMUS)");
			DataTable dtSat = Tvi.Doldur("SELECT FPRKMUS,FMUSHESKOD,FMUSADi,FMUSSOYADi,FMUSUNV,FMUSTCKiMN,FKULHESKOD,FDOGTAR FROM MUS WHERE EXISTS(" + polKomut.Replace("SELECT *", "SELECT '.'") + " AND POL.FFRKSAT = MUS.FPRKMUS)");
			DataTable dtSor = Tvi.Doldur("SELECT FPRKMUS,FMUSHESKOD,FMUSADi,FMUSSOYADi,FMUSUNV,FMUSTCKiMN,FKULHESKOD,FDOGTAR FROM MUS WHERE EXISTS(" + polKomut.Replace("SELECT *", "SELECT '.'") + " AND POL.FFRKSOR = MUS.FPRKMUS)");

			IndexMetadataList iml = new IndexMetadataList();
			iml.KayitTipiAdi = "police";

			int cnt = 0;
			foreach (DataRow dr in dtPol.Rows)
			{
				IndexMetadata im = new IndexMetadata();
				im.KayitTipiAdi = "police";
				im.Alanlar = new List<KayitAlan>();

				int prk = Araclar.ParseInt(dr["fPrkPol"].ToString());
				im.Id = prk;
				DataRow[] pol2DataRow = dtPol2.Select("FPRKPOL = " + prk);

				DataTable dtUruneOzel = null;

				//Tvi.Doldur("SELECT * FROM " + dtPol.Rows[0]["PolGrp"].ToString() + " WHERE FPRKPOL = " + prk);
				DataRow drUruneOzel = null;
				if (dr["POLGRP"].ToString() == "TRF")
					drUruneOzel = dtTrf.Select("FPRKPOL = " + prk)[0];
				else if (dr["POLGRP"].ToString() == "KKO")
					drUruneOzel = dtKko.Select("FPRKPOL = " + prk)[0];

				DataRow drMus = (dtMus.Select("FPRKMUS = " + dr["FFRKMUS"]).Length == 1 ? dtMus.Select("FPRKMUS = " + dr["FFRKMUS"])[0] : null);
				DataRow drTa = (dtTa.Select("FPRKMUS = " + dr["FFRKTA"]).Length == 1 ? dtTa.Select("FPRKMUS = " + dr["FFRKTA"])[0] : null);
				DataRow drSat = (dtSat.Select("FPRKMUS = " + dr["FFRKSAT"]).Length == 1 ? dtSat.Select("FPRKMUS = " + dr["FFRKSAT"])[0] : null);
				DataRow drSor = (dtSor.Select("FPRKMUS = " + dr["FFRKSOR"]).Length == 1 ? dtSor.Select("FPRKMUS = " + dr["FFRKSOR"])[0] : null);


				im.Alanlar.AddRange(KayitAlanOlustur(dr, pol2DataRow[0], drUruneOzel, drMus, drTa, drSat, drSor));
				iml.Add(prk, im);
				if (iml.Count == 1000)
				{
					igm.Guncelle(iml, false);
					iml = new IndexMetadataList();
					iml.KayitTipiAdi = "police";
				}
				cnt++;
			}
			igm.Guncelle(iml, false);
		}

	}
}
