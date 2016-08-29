

var countSchoolCities = 0;

var baseUrl = '/webapi/shared/';

var ids = [];

function getAges(id) {
    $.ajax({
        type: 'GET',
        url: '/webapi/shared/GetAgeYears',
        dataType: 'json',

        success: function (data) {
            if ($.inArray(id, ids) == -1) {
                $.each(data, function (index, value) {
                    $(id).append("<option value='" + value + "' >" + value + "</option>");
                    ids.push(id);
                })
            }
        },
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }
    })
}

function fillDropdownList(idOfSelectTag, data) {

    var val = $(idOfSelectTag + '>option:first').val();

    $(idOfSelectTag + '>option:first').remove();

    $.each(data, function (index, value) {
        $(idOfSelectTag).append('<option value="' + value + '">' + value + '</option>')

        if (val == value) {
            $(idOfSelectTag + '>option:last-child').prop("selected", "selected")
        }
    });
}

function getCountries() {

    if (countSchoolCities < 1) {
        $.ajax({
            url: baseUrl + "GetCountries",
            type: 'GET',
            dataType: 'json',

            success: function (data) {

                fillDropdownList("#ddlSchoolCountry", data);
                countSchoolCities++;
            },
            error: function (xhr, status, p3) {
                sweetAlert(status);
            }
        });
    }
}
function ddlCountryQueryForTowns() {

    var id = $("#ddlSchoolCountry").find('option:selected').text();

    $.ajax({
        type: 'GET',
        url: baseUrl + 'GetProfileTowns/' + id,
        dataType: 'json',
        success: function (data) {

            var array = [];
            $("#ddlSchoolTown > option").remove();

            $.each(data, function (index, value) {
                $("#ddlSchoolTown").append('<option value="' + value + '" >' + value + '</option>');
            });
        },
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }
    })
}


