function RegistrationView() {

    var countryId = null;
    var townId = null;
    var birthDayId = null;
    var birthMonthId = null;
    var birthYearId = null;
    var STATIC = null;

    this.addCountriesToDropdownList = addCountriesToDropdownList;
    this.addTownsToDropdownList = addTownsToDropdownList;
    this.defineSelectedCountry = defineSelectedCountry;
    this.getSelectedCountry = getSelectedCountry;
    this.getCountryName = getCountryName;   
    this.setBirthDays = setBirthDays;
    this.setTownsToDefaultState = setTownsToDefaultState;

    init();

    function init() {
        countryId = $('#Country');
        townId = $("#Town");
        birthDayId = $("#BirthDay");
        birthMonthId = $("#BirthMonth");
        birthYearId = $("#BirthYear");
        STATIC = {
            selectedCountry: null
        };
    }
    function getCountryName() {
        return countryId.val();
    }
    function addCountriesToDropdownList(countries) {
        if (countries != null) {
            var options = "";
            for (var i = 0; i < countries.length; i++)
                options += "<option value='" + countries[i] + "'  >" + countries[i] + "</option>";
            countryId.append(options);
            defineSelectedCountry();
        }
    }
    function addTownsToDropdownList(towns) {
        if (towns != null) {
            var options = "";
            for (var i = 0; i < towns.length; i++)
                options += "<option value='" + towns[i] + "'>" + towns[i] + "</option>";
            townId.html(options);
        }
    }
    function setBirthDays(model) {
        birthDayId.html(getIntOptions(1, model.daysInMonth));
        birthMonthId.html(getIntOptions(1, model.monthInYear));
        birthYearId.html(getIntOptions(model.fromYear, model.currentYear - model.fromYear));
    }
    function setTownsToDefaultState(value) {
        townId.val(value);
    }
    function getIntOptions(start, count) {
        var options = "";
        for (var i = start; i < start + count; i++)
            options += "<option val='" + i + "' >" + i + "</option>";
        return options;
    }
    function defineSelectedCountry() {
        STATIC.selectedCountry = countryId.val();
    }
    function getSelectedCountry() {
        return STATIC.selectedCountry;
    }
}




