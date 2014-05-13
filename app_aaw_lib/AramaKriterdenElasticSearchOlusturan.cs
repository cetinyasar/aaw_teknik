using System.Collections.Generic;
using app_aaw_lib.EsSearch;
using app_aaw_lib.EsSearch.Sorgu;

namespace app_aaw_lib
{
	public class AramaKriterdenElasticSearchOlusturan
	{
		private readonly AramaKriterleri ak;

		public AramaKriterdenElasticSearchOlusturan(AramaKriterleri ak)
		{
			this.ak = ak;
		}

		public ElasticSearchGet Olustur()
		{
			ElasticSearchGet retVal = new ElasticSearchGet();// ElasticSearchGet.Olustur();

			retVal.from = ak.From;
			retVal.size = ak.Size;
			if (!string.IsNullOrEmpty(ak.Query))
			{
				retVal.query = Query.Olustur();
				retVal.query.query_string.query = ak.Query;
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
			retVal.policeGrubu.facet_filter.and.filters.Add(tanzimTarihiRangeEkle());
			if (ak.SecilebilirKriterler.Marka.Count > 0)
				retVal.policeGrubu.facet_filter.and.filters.Add(markaKriterleriniEkle());
			if (ak.SecilebilirKriterler.Brans.Count > 0)
				retVal.policeGrubu.facet_filter.and.filters.Add(bransKriterleriniEkle());

			retVal.brans = bosFacetOlustur("brans");
			retVal.brans.facet_filter.and.filters.Add(tanzimTarihiRangeEkle());
			if (ak.SecilebilirKriterler.Marka.Count > 0)
				retVal.brans.facet_filter.and.filters.Add(markaKriterleriniEkle());
			if (ak.SecilebilirKriterler.PoliceGrubu.Count > 0)
				retVal.brans.facet_filter.and.filters.Add(policeGrubuKriterleriniEkle());

			retVal.marka = bosFacetOlustur("marka");
			retVal.marka.facet_filter.and.filters.Add(tanzimTarihiRangeEkle());
			if (ak.SecilebilirKriterler.PoliceGrubu.Count > 0)
				retVal.marka.facet_filter.and.filters.Add(policeGrubuKriterleriniEkle());
			if (ak.SecilebilirKriterler.Brans.Count > 0)
				retVal.marka.facet_filter.and.filters.Add(bransKriterleriniEkle());
			
			return retVal;
		}

		private facet bosFacetOlustur(string facetAdi)
		{
			FacetPoliceGrubu facet = new FacetPoliceGrubu();

			facet.terms = new FacetTerms();
			facet.terms.field = facetAdi;
			facet.terms.size = 10;

			facet.facet_filter = new FacetFacetFilter();
			facet.facet_filter.and = new FacetAnd();
			facet.facet_filter.and.filters = new List<object>();

			return facet;
		}

		private List<EsAnd> aramaKriterlerindenTermsFiltreleriAl()
		{
			List<EsAnd> esAnd = new List<EsAnd>();

			if (ak.SecilebilirKriterler.PoliceGrubu.Count > 0)
				esAnd.Add(policeGrubuKriterleriniEkle());

			if (ak.SecilebilirKriterler.Marka.Count > 0)
				esAnd.Add(markaKriterleriniEkle());

			if (ak.SecilebilirKriterler.Brans.Count > 0)
				esAnd.Add(bransKriterleriniEkle());

			if (ak.SecilebilirKriterler.TanzimTarihAraligi.IlkTarih.Year != 1)
				esAnd.Add(tanzimTarihiRangeEkle());
			return esAnd;
		}

		private EsAnd tanzimTarihiRangeEkle()
		{
			EsAnd rangeAnd = new EsAnd() { range = EsRange.Olustur() };
			rangeAnd.range.tanzimTarihi.from = ak.SecilebilirKriterler.TanzimTarihAraligi.IlkTarih;
			rangeAnd.range.tanzimTarihi.to = ak.SecilebilirKriterler.TanzimTarihAraligi.SonTarih;
			return rangeAnd;
		}

		private EsAnd markaKriterleriniEkle()
		{
			EsAnd item = new EsAnd() { terms = new TermsMarka() { marka = new List<string>() } };
			foreach (Kriter kr in ak.SecilebilirKriterler.Marka)
			{
				((TermsMarka)(item.terms)).marka.Add(kr.Adi);
			}
			return item;
		}

		private EsAnd policeGrubuKriterleriniEkle()
		{
			EsAnd and = new EsAnd() { terms = new TermsPoliceGrubu() { policeGrubu = new List<string>() } };
			foreach (Kriter kr in ak.SecilebilirKriterler.PoliceGrubu)
			{
				((TermsPoliceGrubu)(and.terms)).policeGrubu.Add(kr.Adi);
			}
			return and;
		}

		private EsAnd bransKriterleriniEkle()
		{
			EsAnd and = new EsAnd() { terms = new TermsBrans() { brans = new List<string>() } };
			foreach (Kriter kr in ak.SecilebilirKriterler.Brans)
			{
				((TermsBrans)(and.terms)).brans.Add(kr.Adi);
			}
			return and;
		}
	}
}