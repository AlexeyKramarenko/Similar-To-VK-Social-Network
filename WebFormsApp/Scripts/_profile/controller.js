
function ProfileController(view, service) {
    this.init(view, service);
    this.activate();
}
ProfileController.prototype = function () {

    return { 
        activate: activate,
        getProfileSchoolTown: getProfileSchoolTown,
        getProfileBirthdays: getProfileBirthdays,
        getProfileBirthMonths: getProfileBirthMonths,
        getProfileBirthYears: getProfileBirthYears,
        getProfileSchoolStartYears: getProfileSchoolStartYears,
        getProfileSchoolFinishYears: getProfileSchoolFinishYears,
        init: init,
        updateUserBirthDay: updateUserBirthDay,
        updateUserBirthMonth: updateUserBirthMonth,
        updateUserBirthYear: updateUserBirthYear, 
        updateFinishYears: updateFinishYears
    }
    var VIEW, SERVICE;

    function init(view, service) {
        VIEW = view;
        SERVICE = service;
    }
    function activate() {
        getProfileSchoolTown();
        VIEW.goToSelectedTab(); 
    }
    function getProfileSchoolTown() {
        SERVICE.getProfileSchoolTown()
               .then(function (data, textStatus, jqXHR) {

                   var userTown = data;
                   var country = VIEW.getSelectedSchoolCountry();

                   SERVICE.getProfileTowns(country)
                          .then(function (data, textStatus, jqXHR) {

                              var towns = data;
                              VIEW.addTownsToDropdownlist(userTown, towns);
                          })
               });
    }
    function updateUserBirthDay() {
        var birthday = VIEW.getChangedBirthdayProfile();
        SERVICE.updateUserBirthDay(birthday);
    }
    function updateUserBirthMonth() {
        var birthmonth = VIEW.getChangedBirthMonthProfile();
        SERVICE.updateUserBirthMonth(birthmonth);
    }
    function updateUserBirthYear() {
        var birthyear = VIEW.getChangedBirthYearProfile();
        SERVICE.updateUserBirthYear(birthyear);
    }
    function getProfileBirthdays() {
        if (VIEW.isBirthDaysIdHasData() == false) {
            SERVICE.getProfileBirthDays()
                   .then(function (data, textStatus, jqXHR) {
                       VIEW.addDataToDropdownlist("#ddlBirthDay", data);
                   });
            VIEW.setBirthDaysIdHasData();
        }
    }

    function getProfileBirthMonths() {
        if (VIEW.isBirthMonthsIdHasData() == false) {
            SERVICE.getProfileBirthDays()
                   .then(function (data, textStatus, jqXHR) {
                       VIEW.addDataToDropdownlist("#ddlBirthMonth", data);
                   });
            VIEW.setBirthMonthsIdHasData();
        }
    }
    function getProfileBirthYears() {
        if (VIEW.isBirthYearsIdHasData() == false) {
            SERVICE.getProfileBirthDays()
                   .then(function (data, textStatus, jqXHR) {
                       VIEW.addDataToDropdownlist("#ddlBirthYear", data);
                   });
            VIEW.setBirthYearsIdHasData();
        }
    }
    function getProfileSchoolStartYears() {
        if (VIEW.isSchoolStartYearsIdHasData() == false) {
            SERVICE.getProfileSchoolStartYears()
                   .then(function (data, textStatus, jqXHR) {
                       VIEW.addDataToDropdownlist("#ddlSchoolBeginningYear", data);
                   });
            VIEW.setSchoolStartYearsIdHasData();
        }
    }
    function getProfileSchoolFinishYears() {
        if (VIEW.isSchoolFinishYearsIdHasData() == false) {
            SERVICE.getProfileSchoolFinishYears()
                   .then(function (data, textStatus, jqXHR) {
                       VIEW.addDataToDropdownlist("#ddlSchoolGraduationYear", data);
                   });
            VIEW.setSchoolFinishYearsIdHasData();
        }
    }
    function updateFinishYears(finishSchoolYear) {
        var year = VIEW.getSchoolStartYear();
        SERVICE.getFinishYears(year)
               .then(function (years) { VIEW.updateFinishYears(years, finishSchoolYear) });
    }
}()
