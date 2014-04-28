angular.module('ada.tarih', ['ui.mask'])
    .directive('adaTarih', function ()
    {
        return {
            restrict: 'E',
            template: '<input ada-tarih-internal type="text" style="width:{{width}}px;" ng-model="ngModelDegeri" ui-mask="99.99.9999" class={{classAdi}} maxlength="10"  />',
            scope: { ngModelDegeri: '=ngModel' },
            link: function(scope, element, attrs)
            {
                scope.classAdi = attrs.class;
                scope.width = element.hasClass("input-buyuk") ? "85" : "60";
            }
        };
    })
    .directive("adaTarihInternal", function ($filter)
    {
        return {
            restrict: 'A',
            require: "ngModel",
            priority: 150,
            scope: { ngModelDegeri: '=ngModel' },
            link: function (scope, elm, attrs, ctrl)
            {
                ctrl.$parsers.push(function (viewValue)
                {
                    if (isUndefined(viewValue))
                        return new Date(1, 1, 1);
                    if (!tarihDogru(viewValue))
                        return new Date(1, 1, 1);
                    var date = new Date(parseInt(viewValue.substr(4, 4)), parseInt(viewValue.substr(2, 2)) - 1, parseInt(viewValue.substr(0, 2)));
                    //var date = new Date(Date.UTC(viewValue.substr(4, 4), viewValue.substr(2, 2) - 1, viewValue.substr(0, 2)));
                    //date.setHours(0);
                    return date;
                });
                ctrl.$formatters.push(function (modelValue)
                {
                    if (typeof(modelValue) == 'undefined')
                        return "";
                    if (modelValue == null)
                        return "";
                    if (typeof(modelValue.getFullYear) == 'undefined')
                        return "";
                    if (modelValue.getFullYear() == 1 || modelValue.getFullYear() == 1901)
                        return "";
                    return $filter('date')(modelValue, 'dd.MM.yyyy');
                });
                elm.on('keydown', function (evt)
                {   
                    var keyCode = evt.which || evt.keyCode;
                    var lRak = "0";
                    var oElement = evt.target || evt.srcElement;

                    if (keyCode == 96)
                        lRak = "0";
                    else if (keyCode == 97)
                        lRak = "1";
                    else if (keyCode == 98)
                        lRak = "2";
                    else if (keyCode == 99)
                        lRak = "3";
                    else if (keyCode == 100)
                        lRak = "4";
                    else if (keyCode == 101)
                        lRak = "5";
                    else if (keyCode == 102)
                        lRak = "6";
                    else if (keyCode == 103)
                        lRak = "7";
                    else if (keyCode == 104)
                        lRak = "8";
                    else if (keyCode == 105)
                        lRak = "9";
                    else
                        lRak = String.fromCharCode(keyCode);

                    if (lRak == "B" || lRak == "Y" || lRak == "D" || keyCode == 109 || keyCode == 107)
                    {
                        var ekle = 0;
                        if (lRak == "Y" || keyCode == 107)
                            ekle = -1;
                        if (lRak == "D" || keyCode == 109)
                            ekle = 1;

                        var d = new Date();
                        if (keyCode == 109 || keyCode == 107)
                        {
                            var tmpTar = oElement.value;
                            if (tmpTar.length == 10)
                            {

                                d.setFullYear(tmpTar.substr(6), tmpTar.substr(3, 2) - 1, tmpTar.substr(0, 2));
                            }
                        }
                        if (isNaN(d.getTime()))
                            d = new Date();
                        d.setDate(d.getDate() - ekle);
                        var lGun = d.getDate() + "";
                        var lAy = d.getMonth() + 1 + "";
                        var lYil = d.getFullYear() + "";
	                    debugger;
                        oElement.value = lGun.PadLeft(2, "0") + "." + lAy.PadLeft(2, "0") + "." + lYil;
                        //$scope.ngModelDegeri = new Date(Date.UTC(oElement.value.substr(4, 4), oElement.value.substr(2, 2) - 1, oElement.value.substr(0, 2)));
                        //evt.returnValue = 0;
                    }
                });
                elm.on('keyup', function (evt)
                {
                    var oElement = evt.target || evt.srcElement;
                    var deger = oElement.value.replace(/_/g, '');
                    if (deger.length == 10)
                        if (!tarihDogru(deger.replace('.', '').replace('.', '')))
                            oElement.value = "";
                });
                var tarihDogru = function(deger)
                {
                    if (deger.length != 8)
                        return false;
                    var checkstr = "0123456789";
                    var dateTemp = "";
                    var seperator = ".";
                    var day;
                    var month;
                    var year;
                    var leap = 0;
                    var err = 0;
                    var i;
                    err = 0;
                    /* Delete all chars except 0..9 */
                    for (i = 0; i < deger.length; i++)
                    {
                        if (checkstr.indexOf(deger.substr(i, 1)) >= 0)
                        {
                            dateTemp = dateTemp + deger.substr(i, 1);
                        }
                    }
                    deger = dateTemp;
                    /* Always change date to 8 digits - string*/
                    /* if year is entered as 2-digit / always assume 20xx */
                    if (deger.length == 6)
                        deger = deger.substr(0, 4) + '20' + deger.substr(4, 2);
                    if (deger.length != 8)
                        err = 19;
                    /* year is wrong if year = 0000 */
                    year = deger.substr(4, 4);
                    if (year == 0 || year > 2100 || year < 1900)
                        err = 20;
                    /* Validation of month*/
                    month = deger.substr(2, 2);
                    if ((month < 1) || (month > 12))
                        err = 21;
                    /* Validation of day*/
                    day = deger.substr(0, 2);
                    if (day < 1)
                        err = 22;
                    /* Validation leap-year / february / day */
                    if ((year % 4 == 0) || (year % 100 == 0) || (year % 400 == 0))
                        leap = 1;
                    if ((month == 2) && (leap == 1) && (day > 29))
                        err = 23;
                    if ((month == 2) && (leap != 1) && (day > 28))
                        err = 24;
                    /* Validation of other months */
                    if ((day > 31) && ((month == "01") || (month == "03") || (month == "05") || (month == "07") || (month == "08") || (month == "10") || (month == "12")))
                        err = 25;
                    if ((day > 30) && ((month == "04") || (month == "06") || (month == "09") || (month == "11")))
                        err = 26;
                    /* if 00 ist entered, no error, deleting the entry */
                    if ((day == 0) && (month == 0) && (year == 00))
                        err = 0;
                    day = "";
                    month = "";
                    year = "";
                    seperator = "";

                    if (err == 0)
                        return true;
                    else
                        return false;
                };
            }
        };
    });
    
angular.module('ada.saat', ['ui.mask'])
    .directive('adaSaat', function ()
    {
        return {
            restrict: 'E',
            template: '<input ada-saat-internal type="text" style="width:{{width}}px;" ng-model="ngModelDegeri" ui-mask="99:99" class={{classAdi}} maxlength="5" ng-change="alert(1);"  />',
            scope: { ngModelDegeri: '=ngModel' },
            link: function (scope, element, attrs)
            {
                scope.classAdi = attrs.class;
                scope.width = element.hasClass("input-buyuk") ? "45" : "30";
            }
        };
    })
    .directive("adaSaatInternal", function ()
    {
        return {
            restrict: 'A',
            require: "ngModel",
            priority: 150,
            scope: { ngModelDegeri: '=ngModel' },
            link: function (scope, elm, attrs, ctrl)
            {
                $(elm).timepicker();

                elm.on('focus', function (evt)
                {
                    scope.ngModelDegeri = evt.target.value;
                    scope.$apply();
                });

                ctrl.$parsers.push(function (viewValue)//her zaman 5 haneli değer dönmesi için. bu olmadığı zaman elle 23:15 yazıldığında mask yüzünden 2315 dönüyor
                {
                    if (isUndefined(viewValue))
                        return null;
                    if (viewValue.length == 5)
                        return viewValue;
                    if (viewValue.length == 4)
                        return viewValue.substr(0, 2) + ":" + viewValue.substr(2, 2);
                    return "";
                });
                
                elm.on('keyup', function (evt)
                {
                    var oElement = evt.target || evt.srcElement;
                    var deger = oElement.value.replace(/_/g, '');
                    if (deger.length == 5)
                        if (deger.substring(0, 2) > 23 || deger.substring(3, 5) > 59)
                            oElement.value = "";
                });

                //elm.on('change', function (evt)
                //{
                //    console.log("change: " + evt);
                //});
                
                //ctrl.$viewChangeListeners(elm.value, function (v)
                //{
                //    console.log('value changed, new value is: ' + v);
                //});
            }
        };
    });

angular.module('ada.dosya.upload', [])
    .directive('adaDosyaUpload', function ()
    {
        return {
            restrict: 'A',
            link: function (scope, elmnt, attrs)
            {
                scope.AdaDosyaContext = new Object();
                scope.AdaDosyaContext.yuklenenDosyalar = new Array();
                scope.AdaDosyaContext.cevapsizIstekAdedi = 0;
                var tmp = new qq.FineUploader({
                    text: { uploadButton: attrs.butonMetin },
                    element: elmnt[0],
                    multiple: true,
                    request: { endpoint: attrs.endpoint },
                    callbacks:
                    {
                        onSubmit: function (id, filename)
                        {
                            scope.AdaDosyaContext.cevapsizIstekAdedi++;
                            if (scope.AdaDosyaContext.cevapsizIstekAdedi == 1)//sadece ilkinde beklemeye al
                                beklemeyeAl();
                        },
                        onComplete: function (id, fileName, response)
                        {
                            if (response.success)
                            {
                                scope.AdaDosyaContext.yuklenenDosyalar.push({ "DosyaAdi": response.dosyaAdi, "DosyaPath": response.dosyaPath });
                                scope.AdaDosyaContext.cevapsizIstekAdedi--;
                                if (scope.AdaDosyaContext.cevapsizIstekAdedi == 0)
                                    eval("scope." + attrs.yuklemeBitinceCagirilacak + "(scope.AdaDosyaContext.yuklenenDosyalar);");
                            }   
                            else
                                PopupMesajGoster("Hata", response.hata);
                        }
                    }
                });
            }
        };
    });
