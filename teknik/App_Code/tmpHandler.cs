﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Summary description for tmpHandler
/// </summary>
public class tmpHandler : IHttpHandler
{
	public tmpHandler()
	{

	}
	public void ProcessRequest(HttpContext context)
	{
		HttpRequest request = context.Request;

		StreamReader sr = new StreamReader(context.Request.InputStream);
		string json = sr.ReadToEnd();
		var degerler = JsonConvert.DeserializeObject<AramaKriterleri>(json);

		HttpResponse response = context.Response;
		string url = request.RawUrl;
		//string readToEnd = new StreamReader("D:\\Cetin\\Belgelerim\\Visual Studio 2013\\Projects\\aaw_apps\\teknik\\policeAramaSonuc.json").ReadToEnd();
		string readToEnd = new StreamReader("C:\\GitHub\\aaw_teknik\\teknik\\policeAramaSonuc.json").ReadToEnd();

		degerler.SeciliKriterler = SecilebilirKriter.Olustur();
		foreach (Kriter kriter in degerler.SecilebilirKriterler.PoliceGrubu)
		{
			if (kriter.Secili)
				degerler.SeciliKriterler.PoliceGrubu.Add(new Kriter {Adet = kriter.Adet, Adi = kriter.Adi});

		}

		response.Write("{ \"Sonuc\" : " + readToEnd + ", \"Kriterler\" : " + JsonConvert.SerializeObject(degerler) + " }");
	}

	public bool IsReusable { get; private set; }
}