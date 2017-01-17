
function ProfileView() {
    this.init();
}

ProfileView.prototype = function () {

    return {
        addDataToDropdownlist: addDataToDropdownlist,  
        addTownsToDropdownlist: addTownsToDropdownlist,
        getChangedBirthdayProfile: getChangedBirthdayProfile,
        getChangedBirthMonthProfile: getChangedBirthMonthProfile,
        getChangedBirthYearProfile: getChangedBirthYearProfile,
        getChangedEducationData: getChangedEducationData,  
        getSchoolStartYear: getSchoolStartYear,
        getSchoolFinishYear: getSchoolFinishYear,
        getSelectedSchoolCountry: getSelectedSchoolCountry,
        goToSelectedTab: goToSelectedTab,
        init: init,
        isSchoolStartYearsIdHasData: isSchoolStartYearsIdHasData,
        isSchoolFinishYearsIdHasData: isSchoolFinishYearsIdHasData,
        isBirthDaysIdHasData: isBirthDaysIdHasData,
        isBirthMonthsIdHasData: isBirthMonthsIdHasData,
        isBirthYearsIdHasData: isBirthYearsIdHasData, 
        setSchoolStartYearsIdHasData: setSchoolStartYearsIdHasData, 
        setSchoolFinishYearsIdHasData: setSchoolFinishYearsIdHasData, 
        setBirthDaysIdHasData: setBirthDaysIdHasData, 
        setBirthMonthsIdHasData: setBirthMonthsIdHasData, 
        setBirthYearsIdHasData: setBirthYearsIdHasData,
        updateFinishYears: updateFinishYears,
    }

    var STATIC = null;

    function init() {
        STATIC = {
            schoolStartYearsIdHasData: false,
            schoolFinishYearsIdHasData: false,
            birthDaysIdHasData: false,
            birthMonthsIdHasData: false,
            birthYearsIdHasData: false
        }
    }
    function isSchoolStartYearsIdHasData() {
        return STATIC.schoolStartYearsIdHasData;
    }
    function setSchoolStartYearsIdHasData() {
        STATIC.schoolStartYearsIdHasData = true;
    }
    function isSchoolFinishYearsIdHasData() {
        return STATIC.schoolFinishYearsIdHasData;
    }
    function setSchoolFinishYearsIdHasData() {
        STATIC.schoolFinishYearsIdHasData = true;
    }
    function isBirthDaysIdHasData() {
        return STATIC.birthDaysIdHasData;
    }
    function setBirthDaysIdHasData() {
        STATIC.birthDaysIdHasData = true;
    }
    function isBirthMonthsIdHasData() {
        return STATIC.birthMonthsIdHasData;
    }
    function setBirthMonthsIdHasData() {
        STATIC.birthMonthsIdHasData = true;
    }
    function isBirthYearsIdHasData() {
        return STATIC.birthYearsIdHasData;
    }
    function setBirthYearsIdHasData() {
        STATIC.birthYearsIdHasData = true;
    }
    function getSelectedSchoolCountry() {
        var country = $("#SchoolCountry").find('option:selected').text();
        return country;
    }
    function addTownsToDropdownlist(userTown, towns) {
        $("#SchoolTown").empty();
        $.each(towns, function (index, town) {
            if (town == userTown)
                $("#SchoolTown").append("<option selected='selected' value='" + town + "' >" + town + "</option>");
            else
                $("#SchoolTown").append('<option value="' + town + '" >' + town + '</option>');
        });
    }
    function getChangedBirthdayProfile() {
        var profile = {
            ProfileID: $('#hdnID').val(),
            BirthDay: $('#ddlBirthDay').val()
        };
        return profile;
    }
    function getChangedBirthMonthProfile() {
        var profile = {
            ProfileID: $('#hdnID').val(),
            BirthMonth: $('#ddlBirthMonth').val()
        };
        return profile;
    }
    function getChangedBirthYearProfile() {
        var profile = {
            ProfileID: $('#hdnID').val(),
            BirthYear: $('#ddlBirthYear').val()
        };
        return profile;
    }
    function getChangedEducationData() {
        var profile = {
            ProfileID: $('#hdnID').val(),
            SchoolCountry: $("#SchoolCountry").val(),
            SchoolTown: $("#SchoolTown").val(),
            School: $("#School").val(),
            StartScoolYear: $("#StartSchoolYear").val(),
            FinishScoolYear: $("#FinishSchoolYear").val()
        };
        return profile;
    }
    function updateFinishYears(finishYears, selectedFinishYear) {
        var finishSchoolYear = $("#FinishSchoolYear");
        finishSchoolYear.empty();
        var startYear = $("#StartSchoolYear").val();
        for (var i = 0; i < finishYears.length; i++) {
            var year = finishYears[i];
            if (year == selectedFinishYear && startYear < selectedFinishYear)
                finishSchoolYear.append("<option selected='selected' value='" + year + "'>" + year + "</option>");
            else
                finishSchoolYear.append("<option value='" + year + "'>" + year + "</option>");
        }
    }
    function goToSelectedTab() {
        var href = $(location).attr('href');
        var index = href.indexOf("?") + 1;
        if (index != 0) {
            var id = href.substring(index);
            $(id).click();
        }
        else {
            $('ul.nav > li:first-child').addClass('active');
        }
        $('div.tab-content').removeClass('unvisible');
    }
    function addDataToDropdownlist(dropdownlist, data) {

        var val = $(dropdownlist + '>option:first').val();
        $(dropdownlist + '>option:first').remove();

        $.each(data, function (index, value) {
            $(dropdownlist).append('<option value="' + value + '">' + value + '</option>')
            if (val == value) {
                $(dropdownlist + '>option:last-child').prop("selected", "selected")
            }
        });
    }
    function getSchoolStartYear() {
        var selectedStartYear = $("#StartSchoolYear").find('option:selected').text();
        return selectedStartYear;
    }
    function getSchoolFinishYear() {
        var selectedFinishYear = $("#FinishSchoolYear").find('option:selected').text();
        return selectedFinishYear;
    }
}()


