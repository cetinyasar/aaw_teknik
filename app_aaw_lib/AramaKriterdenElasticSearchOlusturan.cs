using System.Collections.Generic;
using app_aaw_lib.EsSearch;
using app_aaw_lib.EsSearch.Sorgu;

namespace app_aaw_lib
{
	public class AramaKriterdenElasticSearchOlusturan
	{
		private readonly AramaKriterleri _aramaKriterleri;

		public AramaKriterdenElasticSearchOlusturan(AramaKriterleri ak)
		{
			_aramaKriterleri = ak;
		}

		public ElasticSearchGet Olustur()
		{
			ElasticSearchGet retVal = new ElasticSearchGet();// ElasticSearchGet.Olustur();
			retVal.fields = new List<string> { "sirketPoliceNo", "brans", "sigortaliAcik", "musteriAdiAcik", "tanzimTarihi", "plaka", "marka" };

			retVal.from = _aramaKriterleri.From;
			retVal.size = _aramaKriterleri.Size;
			if (!string.IsNullOrEmpty(_aramaKriterleri.Query))
			{
				retVal.query = Query.Olustur();
				retVal.query.query_string.query = _aramaKriterleri.Query;
			}

			List<EsAnd> kriterlerindenTermsFiltreleriAl = aramaKriterlerindenTermsFiltreleriAl();
			if (kriterlerindenTermsFiltreleriAl.Count > 0)
			{
				retVal.filter = EsFilter.Olustur();
				retVal.filter.and = kriterlerindenTermsFiltreleriAl;
			}

			retVal.facets = aramaKriterlerindenFacetsAl();
			return retVal;
		}

		private EsFacets aramaKriterlerindenFacetsAl()
		{
			EsFacets retVal = new EsFacets();

			retVal.policeGrubu = bosFacetOlustur("policeGrubu");
			retVal.brans = bosFacetOlustur("brans");
			retVal.marka = bosFacetOlustur("marka");
			retVal.modelYili = bosFacetOlustur("modelYili");
			retVal.tali = bosFacetOlustur("tali");
			retVal.satici = bosFacetOlustur("satici");
			retVal.sorumlu = bosFacetOlustur("sorumlu");

			//EsAnd policeGrubuAnd = policeGrubuKriterleriniEkle();
			//EsAnd markaAnd = markaKriterleriniEkle();
			//EsAnd bransAnd = bransKriterleriniEkle();

			EsAnd markaAnd;
			EsAnd bransAnd;
			EsAnd modelYiliAnd;
			EsAnd taliAnd;
			EsAnd saticiAnd;
			EsAnd sorumluAnd;
			EsAnd policeGrubuAnd;
			aramaIcinTermsOlustur(out policeGrubuAnd, out markaAnd, out bransAnd, out modelYiliAnd, out taliAnd, out saticiAnd, out sorumluAnd);

			//if (policeGrubuAnd.terms.AdetAl() > 0)
				retVal.policeGrubu = facetFiltreAyarla(new List<EsAnd> { markaAnd, bransAnd, modelYiliAnd, taliAnd, saticiAnd, sorumluAnd }, "policeGrubu");
			//if (markaAnd.terms.AdetAl() > 0)
				retVal.marka = facetFiltreAyarla(new List<EsAnd> { policeGrubuAnd, bransAnd, modelYiliAnd, taliAnd, saticiAnd, sorumluAnd }, "marka");
			//if (bransAnd.terms.AdetAl() > 0)
				retVal.brans = facetFiltreAyarla(new List<EsAnd> { markaAnd, policeGrubuAnd, modelYiliAnd, taliAnd, saticiAnd, sorumluAnd }, "brans");
			//if (modelYiliAnd.terms.AdetAl() > 0)
				retVal.modelYili = facetFiltreAyarla(new List<EsAnd> { markaAnd, bransAnd, policeGrubuAnd, taliAnd, saticiAnd, sorumluAnd }, "modelYili");
			//if (taliAnd.terms.AdetAl() > 0)
				retVal.tali = facetFiltreAyarla(new List<EsAnd> { markaAnd, bransAnd, modelYiliAnd, policeGrubuAnd, saticiAnd, sorumluAnd }, "tali");
			//if (saticiAnd.terms.AdetAl() > 0)
				retVal.satici = facetFiltreAyarla(new List<EsAnd> { markaAnd, bransAnd, modelYiliAnd, taliAnd, policeGrubuAnd, sorumluAnd }, "satici");
			//if (sorumluAnd.terms.AdetAl() > 0)
				retVal.sorumlu = facetFiltreAyarla(new List<EsAnd> { markaAnd, bransAnd, modelYiliAnd, taliAnd, saticiAnd, policeGrubuAnd }, "sorumlu");

			if (_aramaKriterleri.SecilebilirKriterler.TanzimTarihAraligi.IlkTarih.Year > 1910)
			{
				retVal.policeGrubu.facet_filter.and.filters.Add(tanzimTarihiRangeEkle());
				retVal.brans.facet_filter.and.filters.Add(tanzimTarihiRangeEkle());
				retVal.marka.facet_filter.and.filters.Add(tanzimTarihiRangeEkle());
			}
			return retVal;
		}

		private void aramaIcinTermsOlustur(out EsAnd policeGrubuAnd, out EsAnd markaAnd, out EsAnd bransAnd, out EsAnd modelYiliAnd, out EsAnd taliAnd,
			out EsAnd saticiAnd, out EsAnd sorumluAnd)
		{
			policeGrubuAnd = kriterleriniEkle(TermsPoliceGrubu.Olustur(), _aramaKriterleri.SecilebilirKriterler.PoliceGrubu);
			markaAnd = kriterleriniEkle(TermsMarka.Olustur(), _aramaKriterleri.SecilebilirKriterler.Marka);
			bransAnd = kriterleriniEkle(TermsBrans.Olustur(), _aramaKriterleri.SecilebilirKriterler.Brans);
			modelYiliAnd = kriterleriniEkle(TermsModelYili.Olustur(), _aramaKriterleri.SecilebilirKriterler.ModelYili);
			taliAnd = kriterleriniEkle(TermsTali.Olustur(), _aramaKriterleri.SecilebilirKriterler.ModelYili);
			saticiAnd = kriterleriniEkle(TermsSatici.Olustur(), _aramaKriterleri.SecilebilirKriterler.ModelYili);
			sorumluAnd = kriterleriniEkle(TermsSorumlu.Olustur(), _aramaKriterleri.SecilebilirKriterler.ModelYili);
		}

		private facet facetFiltreAyarla(IEnumerable<EsAnd> liste, string facetAdi)
		{
			facet facet = bosFacetOlustur(facetAdi);
			facet.facet_filter = new FacetFacetFilter();
			facet.facet_filter.and = new FacetAnd();
			facet.facet_filter.and.filters = new List<EsAnd>();

			int cnt = 0;
			foreach (EsAnd esAnd in liste)
			{
				if (esAnd.terms.AdetAl() == 0)
					continue;

				cnt++;
				facet.facet_filter.and.filters.Add(esAnd);
			}
			if (cnt == 0)
				facet.facet_filter = null;

			return facet;
		}

		private facet bosFacetOlustur(string facetAdi)
		{
			facet facet = new facet();

			facet.terms = new FacetTerms();
			facet.terms.field = facetAdi;
			facet.terms.size = 50;

			//facet.facet_filter = new FacetFacetFilter();
			//facet.facet_filter.and = new FacetAnd();
			//facet.facet_filter.and.filters = new List<EsAnd>();

			return facet;
		}

		private List<EsAnd> aramaKriterlerindenTermsFiltreleriAl()
		{
			List<EsAnd> esAnd = new List<EsAnd>();

			EsAnd policeGrubuAnd;
			EsAnd markaAnd;
			EsAnd bransAnd;
			EsAnd modelYiliAnd;
			EsAnd taliAnd;
			EsAnd saticiAnd;
			EsAnd sorumluAnd;
			aramaIcinTermsOlustur(out policeGrubuAnd, out markaAnd, out bransAnd, out modelYiliAnd, out taliAnd, out saticiAnd, out sorumluAnd);

			esAndListeyeEkle(policeGrubuAnd, esAnd);
			esAndListeyeEkle(markaAnd, esAnd);
			esAndListeyeEkle(bransAnd, esAnd);
			esAndListeyeEkle(modelYiliAnd, esAnd);
			esAndListeyeEkle(taliAnd, esAnd);
			esAndListeyeEkle(saticiAnd, esAnd);
			esAndListeyeEkle(sorumluAnd, esAnd);

			if (_aramaKriterleri.SecilebilirKriterler.TanzimTarihAraligi.IlkTarih.Year > 1910)
				esAnd.Add(tanzimTarihiRangeEkle());
			return esAnd;
		}

		private void esAndListeyeEkle(EsAnd esAnd, List<EsAnd> esAndList)
		{
			if (esAnd.terms.AdetAl() > 0)
				esAndList.Add(esAnd);
		}

		private EsAnd tanzimTarihiRangeEkle()
		{
			EsAnd rangeAnd = new EsAnd() { range = EsRange.Olustur() };
			rangeAnd.range.tanzimTarihi.from = _aramaKriterleri.SecilebilirKriterler.TanzimTarihAraligi.IlkTarih;
			rangeAnd.range.tanzimTarihi.to = _aramaKriterleri.SecilebilirKriterler.TanzimTarihAraligi.SonTarih;
			return rangeAnd;
		}

		/// <summary>
		/// new TermsPoliceGrubu() { policeGrubu = new List string() }
		/// _aramaKriterleri.SecilebilirKriterler.PoliceGrubu
		/// </summary>
		/// <param name="terms"></param>
		/// <param name="kriterler"></param>
		/// <returns></returns>
		private EsAnd kriterleriniEkle(ITerms terms, IEnumerable<Kriter> kriterler)
		{
			EsAnd and = new EsAnd() { terms = terms };
			foreach (Kriter kr in kriterler)
			{
				if (kr.Secili)
					and.terms.KriterEkle(kr.Adi);
			}
			return and;
		}
	}

}