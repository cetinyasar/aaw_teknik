using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

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
		HttpResponse response = context.Response;
//		System.Threading.Thread.Sleep(5000);
		string url = request.RawUrl;
		//string readToEnd = new StreamReader("D:\\Cetin\\Belgelerim\\Visual Studio 2013\\Projects\\aaw_apps\\teknik\\policeAramaSonuc.json").ReadToEnd();
		string readToEnd = new StreamReader("C:\\GitHub\\aaw_teknik\\teknik\\policeAramaSonuc.json").ReadToEnd();
		response.Write(readToEnd);
	}

	public bool IsReusable { get; private set; }
}