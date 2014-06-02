<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="sso.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<script src="mdn-cookie.js"></script>
	<script type="text/javascript">
		function gir()
		{
			var aaa = sso.gir();
			var devamUrl = urlParametrelerindenDegerAl("cont");
			if (devamUrl != "")
				window.location = devamUrl;
			else
				window.location = "default.aspx";
		}

		function isUndefined(a) {
			return typeof a == 'undefined';
		}

		function urlParametreleriAl() {
			var str = window.location.href;
			//if (!isUndefined($.History))
			//	str = $.History.getHash();
			if (str == "")
				str = window.location.href;
			return decodeURIComponent(str);
		}

		function urlParametrelerindenDegerAl(paramAdi) {
			var params = urlParametreleriAl();
			if (params.split('?').length <= 1)
				return "";
			params = params.split('?')[1].split('&');
			for (var i = 0; i < params.length; i++) {
				var tokens = params[i].split('=');
				if (tokens.length == 2 && tokens[0] == paramAdi)
					return tokens[1];
			}
			return "";
		}

	</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <button type="button" onclick="gir();"> Gir </button>
    </div>
    </form>
</body>
</html>
