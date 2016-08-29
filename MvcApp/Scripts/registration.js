
$(document).ready(function () {

    getBirthDays();

    getCountries();

    $("#Country").change(function () {

        setTownsToDefault();
        getTownsByCountryName();
    });
})

function getBirthDays() {

    var daysInMonth = 31;
    var monthInYear = 12;
    var fromYear = 1941;
    var currentYear = new Date().getFullYear();

    $('#BirthDay').html(getIntOptions(1, daysInMonth));
    $('#BirthMonth').html(getIntOptions(1, monthInYear));
    $('#BirthYear').html(getIntOptions(fromYear, currentYear - fromYear));
}

function getIntOptions(start, count) {

    var options = "";

    for (var i = start; i < start + count; i++)
        options += "<option val='" + i + "' >" + i + "</option>";

    return options;
}

function setTownsToDefault() {

    $('#Town').val("0");
}

function getCountries() {

    var countries = "";

    $.ajax({

        url: '/webapi/people/GetCountries',

        type: 'GET',
        dataType: 'json',
        
        success: function (data) {

            if (data != null) {

                var options = "";

                for (var i = 0; i < data.length; i++) {
                  
                    options += "<option value='" + data[i].CountryName + "'  >" + data[i].CountryName + "</option>";

                }

                $('#Country').append(options);

                var id = $("#Country option:selected").val();

                if (id != 0)
                    getTownsByCountryName();
            }
        }
    });
}
function getTownsByCountryName() {

    var id = $("#Country option:selected").val();

    $.ajax({

        url: '/webapi/people/GetTownsByCountryName/' + id,

        type: 'GET',

        success: function (data) {

            if (data != null) {

                var countries = "";// "<option  value=''>Choose town</option>";

                for (var i = 0; i < data.length; i++) {

                    countries += "<option value='" + data[i].TownName + "'>" + data[i].TownName + "</option>";
                }

                $('#Town').empty();

                $('#Town').append(countries);

                //if (localStorage.selectedTownValue != undefined)//if(postback==true)
                //{
                //    var town = localStorage.selectedTownValue;

                //    $('#Town').val(town);
                //}

            }
        }
    });
}

