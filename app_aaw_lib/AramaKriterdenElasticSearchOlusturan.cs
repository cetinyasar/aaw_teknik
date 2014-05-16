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

			EsAnd markaAnd = markaKriterleriniEkle();
			EsAnd bransAnd = bransKriterleriniEkle();
			EsAnd policeGrubuAnd = policeGrubuKriterleriniEkle();

			if (((TermsPoliceGrubu)(policeGrubuAnd.terms)).policeGrubu.Count > 0)
			{
				retVal.brans.facet_filter.and.filters.Add(policeGrubuAnd);
				retVal.marka.facet_filter.and.filters.Add(policeGrubuAnd);
			}

			if (((TermsMarka)(markaAnd.terms)).marka.Count > 0)
			{
				retVal.policeGrubu.facet_filter.and.filters.Add(markaAnd);
				retVal.brans.facet_filter.and.filters.Add(markaAnd);
			}

			if (((TermsBrans) (bransAnd.terms)).brans.Count > 0)
			{
				retVal.policeGrubu.facet_filter.and.filters.Add(bransAnd);
				retVal.marka.facet_filter.and.filters.Add(bransAnd);
			}

			if (_aramaKriterleri.SecilebilirKriterler.TanzimTarihAraligi.IlkTarih.Year > 1910)
			{
				retVal.policeGrubu.facet_filter.and.filters.Add(tanzimTarihiRangeEkle());
				retVal.brans.facet_filter.and.filters.Add(tanzimTarihiRangeEkle());
				retVal.marka.facet_filter.and.filters.Add(tanzimTarihiRangeEkle());
			}
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

			//if (_aramaKriterleri.SecilebilirKriterler.PoliceGrubu.Count > 0)
			EsAnd policeGrubuAnd = policeGrubuKriterleriniEkle();
			if (((TermsPoliceGrubu)(policeGrubuAnd.terms)).policeGrubu.Count > 0)
				esAnd.Add(policeGrubuAnd);

			EsAnd markaAnd = markaKriterleriniEkle();
			if (((TermsMarka)(markaAnd.terms)).marka.Count > 0)
				esAnd.Add(markaAnd);

			EsAnd bransAnd = bransKriterleriniEkle();
			if (((TermsBrans)(bransAnd.terms)).brans.Count > 0)
				esAnd.Add(bransAnd);

			if (_aramaKriterleri.SecilebilirKriterler.TanzimTarihAraligi.IlkTarih.Year > 1910)
				esAnd.Add(tanzimTarihiRangeEkle());
			return esAnd;
		}

		private EsAnd tanzimTarihiRangeEkle()
		{
			EsAnd rangeAnd = new EsAnd() { range = EsRange.Olustur() };
			rangeAnd.range.tanzimTarihi.from = _aramaKriterleri.SecilebilirKriterler.TanzimTarihAraligi.IlkTarih;
			rangeAnd.range.tanzimTarihi.to = _aramaKriterleri.SecilebilirKriterler.TanzimTarihAraligi.SonTarih;
			return rangeAnd;
		}

		private EsAnd markaKriterleriniEkle()
		{
			EsAnd item = new EsAnd() { terms = new TermsMarka() { marka = new List<string>() } };
			foreach (Kriter kr in _aramaKriterleri.SecilebilirKriterler.Marka)
			{
				if (kr.Secili)
					((TermsMarka)(item.terms)).marka.Add(kr.Adi);
			}
			return item;
		}

		private EsAnd policeGrubuKriterleriniEkle()
		{
			EsAnd and = new EsAnd() { terms = new TermsPoliceGrubu() { policeGrubu = new List<string>() } };
			foreach (Kriter kr in _aramaKriterleri.SecilebilirKriterler.PoliceGrubu)
			{
				if (kr.Secili)
					((TermsPoliceGrubu)(and.terms)).policeGrubu.Add(kr.Adi);
			}
			return and;
		}

		private EsAnd bransKriterleriniEkle()
		{
			EsAnd and = new EsAnd() { terms = new TermsBrans() { brans = new List<string>() } };
			foreach (Kriter kr in _aramaKriterleri.SecilebilirKriterler.Brans)
			{
				((TermsBrans)(and.terms)).brans.Add(kr.Adi);
			}
			return and;
		}
	}
}