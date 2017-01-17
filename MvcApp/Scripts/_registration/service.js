
function RegistrationService() {
    
    var defaultTownValue, ajax;

    this.getCountries = getCountries;
    this.getTownsByCountryName = getTownsByCountryName;
    this.getBirthdayModel = getBirthdayModel;
    this.getDefaultTownValue = getDefaultTownValue;    

    init();

    function init() {
        ajax = new Ajax().ajaxCall;
        defaultTownValue = 0;
    }
    function getDefaultTownValue() {  
        return defaultTownValue;
    }
    function getCountries() {

        var config = {
            url: '/webapi/people/GetCountries',
            type: 'GET',
            dataType: 'json'
        };

        return ajax(config);
    }
    function getTownsByCountryName(id) {

        var config = {
            url: '/webapi/people/GetTownsByCountryName/' + id,
            type: 'GET',
            dataType: 'json'
        };
        return ajax(config);
    }
    function getBirthdayModel() {

        var model = {
            daysInMonth: 31,
            monthInYear: 12,
            fromYear: 1941,
            currentYear: new Date().getFullYear()
        }
        return model;
    }
}
 
