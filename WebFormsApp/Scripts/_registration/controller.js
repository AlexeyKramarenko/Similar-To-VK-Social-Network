/// <reference path="/Scripts/require.js"/>

function RegistrationController(view, service) {
    this.init(view, service);
    this.activate();
}

RegistrationController.prototype = function () {

    return { 
        activate: activate,
        getTowns: getTowns, 
        getCountries: getCountries,
        init: init,
        setBirthdays: setBirthdays,
        setTownsListToDefaultState: setTownsListToDefaultState,
        setTowns: setTowns,
    }

    var VIEW, SERVICE;
    
    function init(view, service) {
        VIEW = view;
        SERVICE = service;
    }
    function activate() {
        setBirthdays();
        getCountries().then(setTownsListToDefaultState)
                      .then(setTowns);
    }
    function getTowns() {
        VIEW.defineSelectedCountry();
        setTownsListToDefaultState();
        setTowns();
    }
    function getCountries() {
        var promise = SERVICE.getCountries()
                             .then(VIEW.addCountriesToDropdownList, console.log);
        return promise;
    }
    function setTowns() {
        var country = VIEW.getSelectedCountry();
        SERVICE.getTownsByCountryName(country)
               .then(VIEW.addTownsToDropdownList, console.log);
    }
    function setBirthdays() {
        var birthday = SERVICE.getBirthdayModel();
        VIEW.setBirthDays(birthday);
    }
    function setTownsListToDefaultState() {
        var value = SERVICE.getDefaultTownValue();
        VIEW.setTownsToDefaultState(value);
    }
}()

