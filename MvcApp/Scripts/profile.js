
$(document).ready(function () {

    goToSelectedTab();

    //getEducationInfo();
    //getCountries();

});

//----------------------------------------------------

var baseUrl = '/webapi/Profile/';


var countSchoolStartYears = 0;
var countSchoolFinishYears = 0;
var countBirthDays = 0;
var countBirthMonths = 0;
var countBirthYears = 0;


function resetCountValues() {
    countBirthDays = 0;
    countBirthMonths = 0;
    countBirthYears = 0;
};

//------------------------------


var countSchoolCities = 0;

/*
function getCountries() {

    if (countSchoolCities < 1) {
        $.ajax({
            url: '/webapi/shared/GetCountries',
            type: 'GET',
            dataType: 'json',

            success: function (data) {

                fillOutDropDownList("#SchoolCountry", data);
                countSchoolCities++;
                ddlCountryQueryForTowns();
            },
            error: function (xhr, status, p3) {
                sweetAlert(status);
            }
        });
    }
}
*/

function ddlCountryQueryForTowns() {

    var id = $("#SchoolCountry").find('option:selected').text();
    if (id != "")
        $.ajax({
            type: 'GET',
            url: '/webapi/shared/GetProfileTowns/' + id,
            dataType: 'json',
            success: function (data) {

                var array = [];
                $("#SchoolTown > option").remove();

                $.each(data, function (index, value) {
                    $("#SchoolTown").append('<option value="' + value + '" >' + value + '</option>');
                });
            },
            error: function (xhr, status, p3) {
                alert(status);
            }
        })
}

function updateFinishYears() {

    var selectedStartYear = $("#StartSchoolYear").find('option:selected').text();
    var selectedFinishYear = $("#FinishSchoolYear").find('option:selected').text();

    if (selectedStartYear != "")
        $.ajax({
            type: 'GET',
            url: '/profile/GetFinishYears',
            dataType: 'json',
            data: { "selectedStartYear": selectedStartYear },
            success: function (finishYears) {

                var finishSchoolYear = $("#FinishSchoolYear");

                finishSchoolYear.html('');

                for (var i = 0; i < finishYears.length; i++) {

                    var year = finishYears[i];

                    if (year == selectedFinishYear)
                        finishSchoolYear.append("<option value='" + year + "'>" + year + "</option>");
                    else
                        finishSchoolYear.append("<option selected='selected' value='" + year + "'>" + year + "</option>");

                }

            },
            error: function (xhr, status, p3) {
                alert(status);
            }
        })
}


//-------------------------------
//function updateUserBirthDay() {

//    var val = $('#ddlBirthDay').val();
//    var profileId = $('#hdnID').val();

//    var Profile = {
//        ProfileID: profileId,
//        BirthDay: val
//    };

//    $.ajax({
//        url: baseUrl + 'UpdateUserBirthDay',
//        type: 'PUT',
//        contentType: "application/json;charset=utf-8",

//        data: JSON.stringify(Profile),
//        error: function (xhr, status, p3) {
//            sweetAlert(status);
//        }

//    })
//};
//function updateUserBirthMonth() {

//    var val = $('#ddlBirthMonth').val();
//    var profileId = $('#hdnID').val();

//    var Profile = {
//        ProfileID: profileId,
//        BirthMonth: val
//    };

//    $.ajax({
//        url: baseUrl + 'UpdateUserBirthMonth',
//        type: 'PUT',
//        contentType: "application/json;charset=utf-8",

//        data: JSON.stringify(Profile),
//        error: function (xhr, status, p3) {
//            sweetAlert(status);
//        }

//    })
//};
//function updateUserBirthYear() {

//    var val = $('#ddlBirthYear').val();
//    var profileId = $('#hdnID').val();

//    var Profile = {
//        ProfileID: profileId,
//        BirthYear: val
//    };

//    $.ajax({
//        url: baseUrl + 'UpdateUserBirthYear',
//        type: 'PUT',
//        contentType: "application/json;charset=utf-8",

//        data: JSON.stringify(Profile),
//        error: function (xhr, status, p3) {
//            sweetAlert(status);
//        }

//    })
//};
function fillOutDropDownList(idOfSelectTag, data) {

    var val = $(idOfSelectTag + '>option:first').val();

    $(idOfSelectTag + '>option:first').remove();

    $.each(data, function (index, value) {
        $(idOfSelectTag).append('<option value="' + value + '">' + value + '</option>')

        if (val == value) {
            $(idOfSelectTag + '>option:last-child').prop("selected", "selected")
        }
    });
}

//function getProfileBirthDays() {

//    if (countBirthDays < 1) {
//        $.ajax({
//            url: baseUrl + "GetProfileBirthDays",
//            type: 'GET',
//            dataType: 'json',

//            success: function (data) {

//                fillOutDropDownList("#ddlBirthDay", data);
//                countBirthDays++;
//            },
//            error: function (xhr, status, p3) {
//                alert(status);
//            }
//        });
//    }
//}
//function getProfileBirthMonths() {
//    if (countBirthMonths < 1) {
//        $.ajax({
//            url: baseUrl + "GetProfileBirthMonths",
//            type: 'GET',
//            dataType: 'json',

//            success: function (data) {

//                fillOutDropDownList('#ddlBirthMonth', data);
//                countBirthMonths++;
//            },
//            error: function (xhr, status, p3) {
//                alert(status);
//            }
//        });
//    }
//}

//function getProfileSchoolStartYears() {

//    if (countSchoolStartYears < 1) {
//        $.ajax({
//            url: baseUrl + "GetProfileSchoolStartYears",
//            type: 'GET',
//            dataType: 'json',

//            success: function (data) {

//                fillOutDropDownList('#ddlSchoolBeginningYear', data);
//                countSchoolStartYears++;
//            },
//            error: function (xhr, status, p3) {
//                alert(status);
//            }
//        });
//    }
//}
//function getProfileSchoolFinishYears() {

//    if (countSchoolFinishYears < 1) {
//        $.ajax({
//            url: baseUrl + "GetProfileSchoolFinishYears",
//            type: 'GET',
//            dataType: 'json',

//            success: function (data) {

//                fillOutDropDownList('#ddlSchoolGraduationYear', data);
//                countSchoolFinishYears++;
//            },
//            error: function (xhr, status, p3) {
//                alert(status);
//            }
//        });
//    }
//}
//function getProfileBirthYears() {

//    if (countBirthYears < 1) {
//        $.ajax({
//            url: baseUrl + "GetProfileBirthYears",
//            type: 'GET',
//            dataType: 'json',

//            success: function (data) {

//                fillOutDropDownList('#ddlBirthYear', data);
//                countBirthYears++;
//            },
//            error: function (xhr, status, p3) {
//                alert(status);
//            }
//        });
//    }
//}
//function getEducationInfo() {

//    $.ajax({
//        type: 'GET',
//        url: baseUrl + 'GetEducationInfo',
//        dataType: 'json',

//        success: function (data) {

//            $("#ddlSchoolCountry").val(data[0]),
//            $("#ddlSchoolTown").val(data[1]),
//            $("#txtSchool").val(data[2]),
//            $("#ddlSchoolBeginningYear").val(data[3]),
//            $("#ddlSchoolGraduationYear").val(data[4])
//        },
//        error: function (xhr, status, p3) {
//            alert(status);
//        }
//    });
//}


//function updateEducationData() {

//    var profile = {
//        profileID: $('#hdnID').val(),
//        SchoolCountry: $("#ddlSchoolCountry").val(),
//        SchoolTown: $("#ddlSchoolTown").val(),
//        School: $("#txtSchool").val(),
//        StartScoolYear: $("#ddlSchoolBeginningYear").val(),
//        FinishScoolYear: $("#ddlSchoolGraduationYear").val()
//    };

//    $.ajax({
//        type: 'PUT',
//        url: baseUrl + 'UpdateProfileEducationData',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify(profile),
//        success: function (data) {

//            alert("Data changed succesfully");

//        },
//        error: function (xhr, status, p3) {

//            alert(xhr.statusText);
//        }
//    });

//}


function goToSelectedTab() {

    var href_ = $(location).attr('href');

    var index = href_.indexOf("?") + 1;

    if (index != 0) {
        var id = href_.substring(index);

        $(id).click();
    }
    else {
        $('ul.nav > li:first-child').addClass('active');
    }

    $('div.tab-content').removeClass('unvisible');

}

