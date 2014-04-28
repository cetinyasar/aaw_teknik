var iamOrtakLibraryYuklendi = true;

Array.prototype.remove = function (from, to)
{
    var rest = this.slice((to || from) + 1 || this.length);
    this.length = from < 0 ? this.length + from : from;
    return this.push.apply(this, rest);
};

String.prototype.TarihOkunabilirYap = function ()
{
    var tarih = this.TarihStringNesneyeCevir();
    return tarih + " - fark: " + (tarih - new Date());
};

String.prototype.TarihAl = function ()
{
    if (this.length < 19)
        return new Date(1, 1, 1);
    return new Date(this.substr(0, 4), this.substr(5, 2) * 1 - 1, this.substr(8, 2), this.substr(11, 2), this.substr(14, 2), this.substr(17, 2), 0);
};

Date.prototype.KolayOkunurAl = function ()
{
    var simdi = new Date();
    var saatBolumu = (this.getHours() + "").PadLeft(2, '0') + ":" + (this.getMinutes() + "").PadLeft(2, '0');
    if (simdi.toDateString() == this.toDateString())
        return "Bugün " + saatBolumu;
    if (simdi.AddDays(1).toDateString() == this.toDateString())
        return "Yarın " + saatBolumu;
    if (simdi.AddDays(-1).toDateString() == this.toDateString())
        return "Dün " + saatBolumu;
    var yilBolumu = (this.getDate() + "").PadLeft(2, '0') + "." + ((this.getMonth() + 1) + "").PadLeft(2, '0') + "." + this.getFullYear();
    return yilBolumu + " " + saatBolumu;
};

String.prototype.PadLeft = function (uzunluk, padKarakteri)
{
    var pad = "";
    for (var i = 0; i < uzunluk; i++)
        pad += padKarakteri;
    return pad.substring(0, pad.length - this.length) + this;
};

Date.prototype.AddDays = function (days)
{
    var dat = new Date(this.valueOf());
    dat.setDate(dat.getDate() + days);
    return dat;
};

Date.prototype.AddHours = function (hours)
{
    return new Date(this.getTime() + hours * 60000 * 60);
};

Date.prototype.AddMinutes = function (minutes)
{
    return new Date(this.getTime() + minutes * 60000);
};

Date.prototype.TanimsizTarih = function ()
{
    return this.getFullYear() == 1 || this.getFullYear() == 1901;
};

Date.prototype.AdaToShortDateString = function ()
{
    return (this.getDate() + "").PadLeft(2, '0') + "." + ((this.getMonth() + 1) + "").PadLeft(2, '0') + "." + this.getFullYear();
    //return this.getDay().PadLeft(2, '0') + "." + this.getMonth().PadLeft(2, '0') + "." + this.getFullYear();
};

Date.prototype.JSONStringYap = function()
{
    return this.getFullYear() + "-" + ((this.getMonth() + 1) + "").PadLeft(2, '0') + "-" + (this.getDate() + "").PadLeft(2, '0') + "T" + (this.getHours() + "").PadLeft(2, '0') + ":" + (this.getMinutes() + "").PadLeft(2, '0') + ":" + (this.getSeconds() + "").PadLeft(2, '0');
};

String.prototype.OnHanelidenTarihAl = function ()
{
    if (this.length < 10)
        return new Date(1, 1, 1);
    return new Date(this.substr(6, 4), this.substr(3, 2) * 1 - 1, this.substr(0, 2), 0, 0, 0, 0);
};

function sunucuyaGondermedenOnceIsle(nesne)
{
    var tarihleriTarihStrYap = function(input)
    {
        if (typeof input !== "object")
            return input;
        for (var key in input)
        {
            if (isUndefined(input) || input == null)
                continue;
            if (!input.hasOwnProperty(key))
                continue;
            var value = input[key];
            if (value != null && !isUndefined(value.getFullYear))
                input[key] = value.JSONStringYap();
            else if (value != null && typeof value === "object")//recursive
                tarihleriTarihStrYap(value);
        }
    };

    tarihleriTarihStrYap(nesne);
    return nesne;
}

function sunucudanAldiginVeriyiIsle(nesne)
{
    var regexIso8601 = /(\d{4})-(\d{2})-(\d{2})T(\d{2})\:(\d{2})\:(\d{2})/;
    var tarihStringleriTarihYap = function (input)
    {
        if (typeof input !== "object")
            return input;
        for (var key in input)
        {
            if (isUndefined(input) || input == null)
                continue;
            if (!input.hasOwnProperty(key))
                continue;
            var value = input[key];
            //alert(key + ": " + value);
            if (typeof value === "string" && value.match(regexIso8601))
                input[key] = value.TarihAl();
            else if (value != null && typeof value === "object")//recursive
                tarihStringleriTarihYap(value);
        }
    };

    tarihStringleriTarihYap(nesne);
    return nesne;
}

function isUndefined(a)
{
	return typeof a == 'undefined';
}
