
function ProfileService() {

    this.getProfileSchoolTown = getProfileSchoolTown;
    this.getProfileTowns = getProfileTowns;
    this.getProfileBirthDays = getProfileBirthDays;
    this.getProfileBirthMonths = getProfileBirthMonths;
    this.getProfileSchoolStartYears = getProfileSchoolStartYears;
    this.getProfileSchoolFinishYears = getProfileSchoolFinishYears;
    this.getProfileBirthYears = getProfileBirthYears;
    this.getFinishYears = getFinishYears;
    this.updateContactInfo = updateContactInfo;
    this.updateEducationInfo = updateEducationInfo;
    this.updateInterestsInfo = updateInterestsInfo;
    this.updateMainInfo = updateMainInfo;
    this.updateProfileEducationData = updateProfileEducationData;
    this.updateUserBirthDay = updateUserBirthDay;
    this.updateUserBirthMonth = updateUserBirthMonth;
    this.updateUserBirthYear = updateUserBirthYear;

    var ajax = null;

    init();

    function init() {
        ajax = new Ajax().ajaxCall;
    }

    function getProfileSchoolTown() {

        var config = {
            type: 'GET',
            url: '/webapi/shared/GetProfileSchoolTown',
            dataType: 'json'
        };
        return ajax(config);
    }
    function getProfileTowns(id) {

        var config = {
            type: 'GET',
            url: '/webapi/shared/GetProfileTowns/' + id,
            dataType: 'json'
        };
        return ajax(config);
    }
    function updateUserBirthDay(profile) {

        var config = {
            url: '/webapi/Profile/UpdateUserBirthDay',
            type: 'PUT',
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(profile)
        };
        return ajax(config);
    }
    function updateUserBirthMonth(profile) {

        var config = {
            url: '/webapi/Profile/UpdateUserBirthMonth',
            type: 'PUT',
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(profile)
        };
        return ajax(config);
    }
    function updateUserBirthYear(profile) {

        var config = {
            url: '/webapi/Profile/UpdateUserBirthYear',
            type: 'PUT',
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(profile)
        };
        return ajax(config);
    }
    function getProfileBirthDays() {

        var config = {
            url: "/webapi/Profile/GetProfileBirthDays",
            type: 'GET',
            dataType: 'json'
        };
        return ajax(config);
    }
    function getProfileBirthMonths() {

        var config = {
            url: "/webapi/Profile/GetProfileBirthMonths",
            type: 'GET',
            dataType: 'json'
        };
        return ajax(config);
    }
    function getProfileSchoolStartYears() {

        var config = {
            url: "/webapi/Profile/GetProfileSchoolStartYears",
            type: 'GET',
            dataType: 'json'
        };
        return ajax(config);
    }
    function getProfileSchoolFinishYears() {

        var config = {
            url: "/webapi/Profile/GetProfileSchoolFinishYears",
            type: 'GET',
            dataType: 'json'
        };
        return ajax(config);
    }
    function getProfileBirthYears() {

        var config = {
            url: "/webapi/Profile/GetProfileBirthYears",
            type: 'GET',
            dataType: 'json'
        };
        return ajax(config);
    }
    function updateProfileEducationData(profile) {

        var config = {
            type: 'PUT',
            url: '/webapi/Profile/UpdateProfileEducationData',
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(profile)
        };
        return ajax(config);
    }
    function getFinishYears(selectedStartYear) {

        var config = {
            type: 'GET',
            url: '/webapi/profile/GetFinishYears',
            dataType: 'json',
            data: { "selectedStartYear": selectedStartYear }
        };
        return ajax(config);
    }

    //-------------mvc ajax actions---------------------
    function updateMainInfo(info) {

        var config = {
            type: 'POST',
            url: '/Profile/MainInfo',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(info),
        };
        return ajax(config);
    }
    function updateContactInfo(info) {

        var config = {
            type: 'POST',
            url: '/Profile/ContactInfo',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(info),
        };
        return ajax(config);
    }

    function updateInterestsInfo(info) {

        var config = {
            type: 'POST',
            url: '/Profile/InterestsInfo',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(info),
        };
        return ajax(config);
    }
    function updateEducationInfo(info) {

        var config = {
            type: 'POST',
            url: '/Profile/EducationInfo',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(info),
        };
        return ajax(config);
    }
}
 