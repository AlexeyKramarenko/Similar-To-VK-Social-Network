
$(document).ready(function () {

    if (!String.prototype.startsWith) {
        Object.defineProperty(String.prototype, 'startsWith', {
            enumerable: false,
            configurable: false,
            writable: false,
            value: function (searchString, position) {
                position = position || 0;
                return this.lastIndexOf(searchString, position) === position;
            }
        });
    }

    getAgeValues();
    getCountries();

    if (getParameterByName('UserID') != '0')
        setFriendsSearchMode();

    else
        setPeopleSearchMode();

    getUsers();

    attachElementsToPostBack();
});

var baseUri = "/webapi/people/";

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

function getCountries() {

    var countries = "";

    $.ajax({

        url: baseUri + 'GetCountries',

        type: 'GET',

        success: function (data) {

            var options = "<option value='0'>Choose country</option>";

            for (var i = 0; i < data.length; i++) {

                options += "<option value='" + data[i].CountryID + "'  >" + data[i].CountryName + "</option>";

            }

            $('#ddlCountry').append(options);

            var id = $("#ddlCountry option:selected").val();
            if (id != 0)
                getTownsByCountry();
        }
    });
}
function getTownsByCountry() {

    var id = $("#ddlCountry option:selected").val();

    $.ajax({

        url: baseUri + 'GetTownsByCountry/' + id,

        type: 'GET',

        success: function (data) {

            var countries = "<option value='0' >Choose town</option>";

            for (var i = 0; i < data.length; i++) {

                countries += "<option value='" + data[i].TownID + "'>" + data[i].TownName + "</option>";
            }

            $('#ddlTown').empty();

            $('#ddlTown').append(countries);

            getUsers();
        }
    });
}



//function getParameterByName(name, url) {

//    if (!url)
//        url = window.location.href;

//    name = name.replace(/[\[\]]/g, "\\$&");

//    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
//        results = regex.exec(url);

//    if (!results) return null;

//    if (!results[2]) return '';

//    return decodeURIComponent(results[2].replace(/\+/g, " "));
//}

function getParameterByName(name, url) {

    if (!url)
        url = window.location.href;

    var array = url.split("/");

    var parameterValue;

    for (var i = 0; i < array.length; i++) {

        var str = array[i].toString();
        var result;
        if (str.startsWith(name)) {
            parameterValue = str.substring(str.indexOf("=") + 1);            
        }
        
    }
    return parameterValue;
}

function removeFromFriends(id) {

    $.ajax({
        url: baseUri + 'RemoveFromFriends/' + id,
        type: 'DELETE',
        success: function () {

            $('#' + id).remove();
        }
    });
}

var selectedDdlTo;
attachElementsToPostBack = function () {

    $("#ddlFrom").change(function () {

        reactOnChangeValue();
    });

    $("#ddlCountry").change(function () {

        setTownsToDefault();
        getTownsByCountry();


        getUsers();

    });

    $("#ddlTown, #ddlFrom, #ddlTo").change(function () {

        getUsers();
    });
    $("#btnPeople").on('click', function () {

        setPeopleSearchMode();
        getUsers();
    });
    $("#btnFriends").on('click', function () {

        setFriendsSearchMode();
        getUsers();
    });
    $("input[name=gender]").on('click', function () {

        getUsers();
    });
}

setTownsToDefault = function () {

    $('#ddlTown').val("0");
}

var searchMode = null;

var SEARCHMODE = {

    PEOPLE: { value: "0" },
    FRIENDS: { value: $('#hdnCurrentUserID').val() }
};

function setPeopleSearchMode() {

    searchMode = SEARCHMODE.PEOPLE;
    $('#searchMode').text("Current search mode: PEOPLE");
}

function setFriendsSearchMode() {

    searchMode = SEARCHMODE.FRIENDS;
    $('#searchMode').text("Current search mode: FRIENDS ONLY");
}

reactOnChangeValue = function () {

    selectedDdlTo = $("#ddlTo option:selected").text();

    var startFromValue = $("#ddlFrom option:selected").text();

    var options = "";

    for (var i = startFromValue; i <= 120; i++) {

        options += '<option value="' + i + '">' + i + '</option>';
    }

    $("#ddlTo").empty().append(options);

    if (startFromValue < selectedDdlTo) {

        $('#ddlTo option[value=' + selectedDdlTo + ']').prop('selected', true);
    }
    else {

        $('#ddlTo option[value=' + 120 + ']').prop('selected', true);
    }
}

var receiversUserID;

function setRecipientUserID(id) {

    receiversUserID = id;
}

function createNewDialog() {

    var message = {

        ReceiversUserID: receiversUserID,
        Body: $('#messageBody').val()
    };

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: '/WebServices/DialogService.asmx/CreateNewDialog',
        data: JSON.stringify({ message: message }),
        dataType: "json",
        success: function (data, status) {

            $('#modal-1').trigger('click'); //close???

            alert(JSON.parse(data.d));
        },
        error: function (request, status, error) {
            alert(request + status + error);

        }
    });
}

function getUsers() {

    var UserID = searchMode.value;

    var name = "";
    var country = 0;
    var town = 0;

    if ($('#txtName').val() == "") {

        name = sessionStorage.getItem('human');

        $('#txtName').val(name);
    }
    else
        name = $('#txtName').val();

    var online = getParameterByName('Online');
    var from = $('#ddlFrom').val();
    var to = $('#ddlTo').val();


    if ($('#ddlCountry').val() != null)
        country = $('#ddlCountry').val();

    if ($('#ddlTown').val() != null)
        town = $('#ddlTown').val();

    var gender = $("input[name=gender]:checked").val();



    var url;

    if (name != "")
        url = baseUri + 'GetUsersList/' + from + '/' + to + '/' + country + '/' + town + '/' + UserID + '/' + gender + '/' + name + '/' + online;

    else
        url = baseUri + 'GetUsersList/' + from + '/' + to + '/' + country + '/' + town + '/' + UserID + '/' + gender + '/' + online;

    $.ajax({

        url: url,
        type: 'GET',

        success: function (data) {

            $('tbody#users').text("");

            var rows = "";

            if (data != null) {
                for (var i = 0; i < data.length; i++) {

                    rows +=

                     "<tr id='" + data[i].UserID + "'>" +
                         "<td>" +
                             "<a href='/main/mainpage/" + data[i].UserID + "'  >" +

                                 "<img src='" + data[i].ImageUrl + "' width='100px' height='100px' />" +

                             "</a>" +
                         "</td>" +
                         "<td>" +
                             "<a href='/main/mainpage/" + data[i].UserID + "' >" +

                                data[i].UserName + "<br/><span style='color:grey'>(" + data[i].FirstName + " " + data[i].LastName + ")</span> " +

                             "</a>" +
                         "</td>" +
                         "<td>" +
                             "<ul>" +
                                 "<li>" +
                                     "<label for='modal-1'><a onclick=\"setRecipientUserID('" + data[i].UserID + "')\" >Send message</a></label></li>" +
                                 "<li>" +
                                     "<a href=\"/people/UserID=" + data[i].UserID + "/Online=false\"  >Browse friends</a></li>" +
                                 "<li>" +
                                     "<a onclick=\"removeFromFriends('" + data[i].UserID + "');return false;\"   >Remove from friends</a></li>" +
                             "</ul>" +
                         "</td>" +
                     "</tr>";
                }

                $('#users').append(rows);
            }
        }
    });
}