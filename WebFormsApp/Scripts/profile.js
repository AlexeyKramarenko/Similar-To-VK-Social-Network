
$(document).ready(function () {

    goToSelectedTab();

    getEducationInfo();

    ddlCountryQueryForTowns();
    
});

//----------------------------------------------------

var baseUrl = 'webapi/Profile/';


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


function getCountries() {

    if (countSchoolCities < 1) {
        $.ajax({
            url: 'webapi/shared/GetCountries',
            type: 'GET',
            dataType: 'json',

            success: function (data) {

                Func("#ddlSchoolCountry", data);
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
        url: 'webapi/shared/GetProfileTowns/' + id,
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

//-------------------------------
function updateUserBirthDay() {

    var val = $('#ddlBirthDay').val();
    var profileId = $('#hdnID').val();

    var Profile = {
        ProfileID: profileId,
        BirthDay: val
    };

    $.ajax({
        url: baseUrl + 'UpdateUserBirthDay',
        type: 'PUT',
        contentType: "application/json;charset=utf-8",

        data: JSON.stringify(Profile),
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }

    })
};
function updateUserBirthMonth() {

    var val = $('#ddlBirthMonth').val();
    var profileId = $('#hdnID').val();

    var Profile = {
        ProfileID: profileId,
        BirthMonth: val
    };

    $.ajax({
        url: baseUrl + 'UpdateUserBirthMonth',
        type: 'PUT',
        contentType: "application/json;charset=utf-8",

        data: JSON.stringify(Profile),
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }

    })
};
function updateUserBirthYear() {

    var val = $('#ddlBirthYear').val();
    var profileId = $('#hdnID').val();

    var Profile = {
        ProfileID: profileId,
        BirthYear: val
    };

    $.ajax({
        url: baseUrl + 'UpdateUserBirthYear',
        type: 'PUT',
        contentType: "application/json;charset=utf-8",

        data: JSON.stringify(Profile),
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }

    })
};
function Func(idOfSelectTag, data) {

    var val = $(idOfSelectTag + '>option:first').val();

    $(idOfSelectTag + '>option:first').remove();

    $.each(data, function (index, value) {
        $(idOfSelectTag).append('<option value="' + value + '">' + value + '</option>')

        if (val == value) {
            $(idOfSelectTag + '>option:last-child').prop("selected", "selected")
        }
    });
}

function getProfileBirthDays() {

    if (countBirthDays < 1) {
        $.ajax({
            url: baseUrl + "GetProfileBirthDays",
            type: 'GET',
            dataType: 'json',

            success: function (data) {

                Func("#ddlBirthDay", data);
                countBirthDays++;
            },
            error: function (xhr, status, p3) {
                sweetAlert(status);
            }
        });
    }
}
function getProfileBirthMonths() {
    if (countBirthMonths < 1) {
        $.ajax({
            url: baseUrl + "GetProfileBirthMonths",
            type: 'GET',
            dataType: 'json',

            success: function (data) {

                Func('#ddlBirthMonth', data);
                countBirthMonths++;
            },
            error: function (xhr, status, p3) {
                sweetAlert(status);
            }
        });
    }
}

function getProfileSchoolStartYears() {

    if (countSchoolStartYears < 1) {
        $.ajax({
            url: baseUrl + "GetProfileSchoolStartYears",
            type: 'GET',
            dataType: 'json',

            success: function (data) {

                Func('#ddlSchoolBeginningYear', data);
                countSchoolStartYears++;
            },
            error: function (xhr, status, p3) {
                sweetAlert(status);
            }
        });
    }
}
function getProfileSchoolFinishYears() {

    if (countSchoolFinishYears < 1) {
        $.ajax({
            url: baseUrl + "GetProfileSchoolFinishYears",
            type: 'GET',
            dataType: 'json',

            success: function (data) {

                Func('#ddlSchoolGraduationYear', data);
                countSchoolFinishYears++;
            },
            error: function (xhr, status, p3) {
                sweetAlert(status);
            }
        });
    }
}
function getProfileBirthYears() {

    if (countBirthYears < 1) {
        $.ajax({
            url: baseUrl + "GetProfileBirthYears",
            type: 'GET',
            dataType: 'json',

            success: function (data) {

                Func('#ddlBirthYear', data);
                countBirthYears++;
            },
            error: function (xhr, status, p3) {
                sweetAlert(status);
            }
        });
    }
}
function getEducationInfo() {

    $.ajax({
        type: 'GET',
        url: baseUrl + 'GetEducationInfo',
        dataType: 'json',

        success: function (data) {

            $("#ddlSchoolCountry").val(data[0]),
            $("#ddlSchoolTown").val(data[1]),
            $("#txtSchool").val(data[2]),
            $("#ddlSchoolBeginningYear").val(data[3]),
            $("#ddlSchoolGraduationYear").val(data[4])
        },
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }
    });
}


function updateEducationData() {

    var Profile = {
        profileID : $('#hdnID').val(),
        SchoolCountry: $("#ddlSchoolCountry").val(),
        SchoolTown: $("#ddlSchoolTown").val(),
        School: $("#txtSchool").val(),
        StartScoolYear: $("#ddlSchoolBeginningYear").val(),
        FinishScoolYear: $("#ddlSchoolGraduationYear").val()
    };

    $.ajax({
        type: 'PUT',
        url: baseUrl + 'UpdateProfileEducationData',
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(Profile),
        success: function () {

            alert("Data changed succesfully");
       
        },
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }
    });

    return false;
}


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

