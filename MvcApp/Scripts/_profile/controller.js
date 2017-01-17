
function ProfileController(VIEW, SERVICE) {
     
    this.getProfileSchoolTown = getProfileSchoolTown;
    this.getProfileBirthdays = getProfileBirthdays;
    this.getProfileBirthMonths = getProfileBirthMonths;
    this.getProfileBirthYears = getProfileBirthYears;
    this.getProfileSchoolStartYears = getProfileSchoolStartYears;
    this.getProfileSchoolFinishYears = getProfileSchoolFinishYears;
    this.updateContactInfo = updateContactInfo;
    this.updateEducationInfo = updateEducationInfo;
    this.updateFinishYears = updateFinishYears;
    this.updateMainInfo = updateMainInfo;
    this.updateInterestsInfo = updateInterestsInfo;
    this.updateUserBirthDay = updateUserBirthDay;
    this.updateUserBirthMonth = updateUserBirthMonth;
    this.updateUserBirthYear = updateUserBirthYear;

    activate();

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
               .then(function (years) {
                   VIEW.updateFinishYears(years, finishSchoolYear);
               }, function (error) {
                   alert(error.responseText);
               });
    }

    function updateMainInfo() {
        var info = VIEW.getMainInfo();
        SERVICE.updateMainInfo(info)
               .then(VIEW.onSuccess, VIEW.onFailure);
    }
    function updateContactInfo() {
        var info = VIEW.getContactInfo();
        SERVICE.updateContactInfo(info)
               .then(VIEW.onSuccess, VIEW.onFailure);
    }
    function updateInterestsInfo() {
        var info = VIEW.getInterestsInfo();
        SERVICE.updateInterestsInfo(info)
               .then(VIEW.onSuccess, VIEW.onFailure);
    }
    function updateEducationInfo() {
        var info = VIEW.getEducationInfo();
        SERVICE.updateEducationInfo(info)
               .then(function (graduationYear) {
                   getProfileSchoolTown();
                   updateFinishYears(graduationYear);
                   VIEW.onSuccess();
               },
               VIEW.onFailure);
    }
}
