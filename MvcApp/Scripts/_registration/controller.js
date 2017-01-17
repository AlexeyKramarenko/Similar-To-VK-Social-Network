
function RegistrationController(VIEW, SERVICE) {
        
    this.getTowns = getTowns;
    //this.activate = activate;
    
    this.__getCountries = getCountries;
    this.__setBirthdays = setBirthdays;
    this.__setTownsListToDefaultState = setTownsListToDefaultState;
    this.__setTowns = setTowns;
    
    activate();

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
}
 