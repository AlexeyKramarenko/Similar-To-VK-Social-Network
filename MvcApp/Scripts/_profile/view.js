
function ProfileView() {

    this.addDataToDropdownlist = addDataToDropdownlist;
    this.addTownsToDropdownlist = addTownsToDropdownlist;
    this.getChangedBirthdayProfile = getChangedBirthdayProfile;
    this.getChangedBirthMonthProfile = getChangedBirthMonthProfile;
    this.getChangedBirthYearProfile = getChangedBirthYearProfile;
    this.getChangedEducationData = getChangedEducationData;
    this.getMainInfo = getMainInfo;
    this.getContactInfo = getContactInfo;
    this.getInterestsInfo = getInterestsInfo;
    this.getEducationInfo = getEducationInfo;
    this.getSchoolStartYear = getSchoolStartYear;
    this.getSchoolFinishYear = getSchoolFinishYear;
    this.getSelectedSchoolCountry = getSelectedSchoolCountry;
    this.goToSelectedTab = goToSelectedTab;
    this.isSchoolStartYearsIdHasData = isSchoolStartYearsIdHasData;
    this.isSchoolFinishYearsIdHasData = isSchoolFinishYearsIdHasData;
    this.isBirthDaysIdHasData = isBirthDaysIdHasData;
    this.isBirthMonthsIdHasData = isBirthMonthsIdHasData;
    this.isBirthYearsIdHasData = isBirthYearsIdHasData;
    this.onFailure = onFailure;
    this.onSuccess = onSuccess;
    this.setSchoolStartYearsIdHasData = setSchoolStartYearsIdHasData;
    this.setSchoolFinishYearsIdHasData = setSchoolFinishYearsIdHasData;
    this.setBirthDaysIdHasData = setBirthDaysIdHasData;
    this.setBirthMonthsIdHasData = setBirthMonthsIdHasData;
    this.setBirthYearsIdHasData = setBirthYearsIdHasData;
    this.updateFinishYears = updateFinishYears;
     
    var STATIC = null;

    init();

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
    //--------------------------
    function getMainInfo() {
        var mainInfo = {

            ProfileID: $("#ProfileID").val(),
            FirstName: $("#FirstName").val(),
            LastName: $("#LastName").val(),
            Gender: $("#Gender").val(),
            Married: $("#Married").val(),
            BirthDay: $("#BirthDay").val(),
            BirthMonth: $("#BirthMonth").val(),
            BirthYear: $("#BirthYear").val(),
            HomeTown: $("#HomeTown").val(),
            Language: $("#Language").val()
        };
        return mainInfo;
    }

    function getContactInfo() {
        var contacts = {

            ProfileID: $("#ProfileID").val(),
            Country: $("#Country").val(),
            Town: $("#Town").val(),
            PhoneNumber: $("#PhoneNumber").val(),
            Skype: $("#Skype").val(),
            WebSite: $("#WebSite").val()
        };
        return contacts;
    }
    function getInterestsInfo() {
        var interests = {

            ProfileID: $("#ProfileID").val(),
            Activity: $("#Activity").val(),
            Interests: $("#Interests").val(),
            Music: $("#Music").val(),
            Films: $("#Films").val(),
            Books: $("#Books").val(),
            Games: $("#Games").val(),
            Quotes: $("#Quotes").val(),
            AboutMe: $("#AboutMe").val()
        };
        return interests;
    }
    function getEducationInfo() {
        var educationInfo = {

            ProfileID: $("#ProfileID").val(),
            SchoolCountry: $("#SchoolCountry").val(),
            SchoolTown: $("#SchoolTown").val(),
            School: $("#School").val(),
            StartSchoolYear: $("#StartSchoolYear").val(),
            FinishSchoolYear: $("#FinishSchoolYear").val(),

            SchoolCountries: null,
            SchoolTowns: null,
            StartYears: null,
            FinishYears: null
        };
        return educationInfo;
    }
    function onSuccess() {
        alert("Profile updated successfully");
    }

    function onFailure(message) {
        if (message == null)
            alert("An error occurred");
        else
            alert(message);
    }
}
 


