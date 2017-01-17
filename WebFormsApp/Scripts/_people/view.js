
function PeopleView() {
    this.init();
}

PeopleView.prototype = function () {

    return {
        addTownsToDropdownlist: addTownsToDropdownlist, 
        constructUsersListUrl: constructUsersListUrl,
        getAgeValues: getAgeValues,
        getCurrentMessage: getCurrentMessage, 
        getValueByParameterName: getValueByParameterName,
        getSelectedCountryId: getSelectedCountryId,
        init: init,
        onSuccessCreateDialog: onSuccessCreateDialog,
        removeFromFriendsInMarkup: removeFromFriendsInMarkup,  
        setPeopleSearchMode: setPeopleSearchMode,
        setFriendsSearchMode: setFriendsSearchMode,
        setRecipientUserID: setRecipientUserID, 
        setTownsToDefault: setTownsToDefault,
        setCountries: setCountries,
        showUserList: showUserList,
        updateAgeValue: updateAgeValue,
    }
    var STATIC = null;
    var SEARCHMODE = null;

    function init() {
        STATIC = {
            selectedCountry: null,
            receiversUserID: null,
            searchMode: null
        };
        SEARCHMODE = {
            PEOPLE: { value: 0 },
            FRIENDS: { value: $('#hdnCurrentUserID').val() }
        };
    }
    function setCountries(countries) {
        var options = "<option value='0'>Choose country</option>";
        for (var i = 0; i < countries.length; i++) {
            var index = i + 1;
            options += "<option value='" + index + "'  >" + countries[i] + "</option>";
        }
        $('#ddlCountry').append(options);
    }
    function getSelectedCountryId() {
        var id = $("#ddlCountry").val();
        return id;
    }
    function addTownsToDropdownlist(towns) {
        $('#ddlTown').empty();
        if (towns != null) {
            var options = "<option value='0' >Choose town</option>";
            for (var i = 0; i < towns.length; i++) {
                options += "<option value='" + towns[i].TownID + "'>" + towns[i].TownName + "</option>";
            }
            $('#ddlTown').append(options);
        }
    }
    function removeFromFriendsInMarkup(id) {
        $('#' + id).remove();
    }
    function setRecipientUserID(id) {
        STATIC.receiversUserID = id;
    }
    function getCurrentMessage() {
        var message = {
            ReceiversUserID: STATIC.receiversUserID,
            Body: $('#messageBody').val()
        };
        return message;
    }
    function onSuccessCreateDialog(data) {
        $('#modal-1').trigger('click');
        $('#messageBody').empty();
        alert(JSON.parse(data.d));
    }

    function setPeopleSearchMode() {
        STATIC.searchMode = SEARCHMODE.PEOPLE;
        $('#searchMode').text("Current search mode: PEOPLE");
    }
    function setFriendsSearchMode() {
        STATIC.searchMode = SEARCHMODE.FRIENDS;
        $('#searchMode').text("Current search mode: FRIENDS ONLY");
    }
    function showUserList(data) {
        $('#users').empty();
        if (data != null) {
            var rows = "";
            for (var i = 0; i < data.length; i++) {
                rows +=
                 "<tr id='" + data[i].UserID + "'>" +
                     "<td>" +
                         "<a href='/mypage.aspx?userid=" + data[i].UserID + "'  >" +
                             "<img src='/" + data[i].ImageUrl + "' width='100px' height='100px' />" +
                         "</a>" +
                     "</td>" +
                     "<td>" +
                         "<a href='/mypage.aspx?userid=" + data[i].UserID + "' >" +
                            data[i].UserName + "<br/><span style='color:grey'>(" + data[i].FirstName + " " + data[i].LastName + ")</span> " +
                         "</a>" +
                     "</td>" +
                     "<td>" +
                         "<ul>" +
                             "<li>" +
                                 "<label for='modal-1'><a onclick=\"vm.setRecipientUserID('" + data[i].UserID + "')\" >Send message</a></label></li>" +
                             "<li>" +
                                 "<a href='../../people.aspx?userid=" + data[i].UserID + "'  >Browse friends</a></li>" +
                             "<li>" +
                                 "<a onclick=\"return vm.removeFromFriends('" + data[i].UserID + "')\"   >Remove from friends</a></li>" +
                         "</ul>" +
                     "</td>" +
                 "</tr>";
            }
            $('#users').html(rows);
        }
    }
    function getAgeValues() {

        var options = "";
        for (var i = 0; i < 121; i++) {
            options += "<option>" + i + "</option>";
        }
        $('#ddlFrom').append(options);
        $('#ddlTo').append(options);
        $("#ddlFrom option:first").attr('selected', 'selected');
        $("#ddlTo option:last").attr('selected', 'selected');
    }
    function getValueByParameterName(name, url) {
        if (!url)
            url = window.location.href;
        var array = url.split("/");
        var parameterValue;
        for (var i = 0; i < array.length; i++) {
            var str = array[i].toString();
            var result;
            if (str.indexOf(name) == 0) {
                parameterValue = str.substring(str.indexOf("=") + 1);
            }
        }
        return parameterValue;
    }
    function updateAgeValue() {
        var toValue = $("#ddlTo option:selected").text();
        var fromValue = $("#ddlFrom option:selected").text();
        var options = "";
        for (var i = fromValue; i <= 120; i++) {
            options += '<option value="' + i + '">' + i + '</option>';
        }
        $("#ddlTo").empty().append(options);
        if (fromValue < toValue) {
            $('#ddlTo option[value=' + toValue + ']').prop('selected', true);
        }
        else {
            $('#ddlTo option[value=' + 120 + ']').prop('selected', true);
        }
    }
    function setTownsToDefault() {
        $('#ddlTown').val("0");
    }
    function constructUsersListUrl() {
        var UserID = STATIC.searchMode.value;
        var countryId = 0;
        var townId = 0;
        var name = null;
        if ($('#txtName').val() == "") {
            name = sessionStorage.getItem('human');
            $('#txtName').val(name);
        }
        else
            name = $('#txtName').val();
        var online = getValueByParameterName('Online');
        var from = $('#ddlFrom').val();
        var to = $('#ddlTo').val();
        if ($('#ddlCountry').val() != null)
            countryId = $('#ddlCountry').val();
        if ($('#ddlTown').val() != null)
            townId = $('#ddlTown').val();
        var gender = $("input[name=gender]:checked").val();
        var url;
        if (name != "")
            url = '/webapi/people/GetUsersList/' + from + '/' + to + '/' + countryId + '/' + townId + '/' + UserID + '/' + gender + '/' + name + '/' + online;
        else
            url = '/webapi/people/GetUsersList/' + from + '/' + to + '/' + countryId + '/' + townId + '/' + UserID + '/' + gender + '/' + online;
        return url;
    }
}()
