
$(document).ready(function () {
    
    getPrivacyVisibilityLevels();

    //при клике по ссылке для указания приватности
    $("a.privacy").click(function () {

        GetPrivacies(this);
    });
});


var baseUri = "webapi/Settings/";


//считывание из БД значений приватности для каждой ссылки
//узнать id профиля,
//чтобы при вызове updatePrivacyVisibilityLevel() был указан Id профиля,
//на который ссылаются эти уровни доступности
function getPrivacyVisibilityLevels() {

    $.ajax({
        type: 'GET',
        url: baseUri + "GetProfileID",
        datatype: 'json',
        async: true,
        success: function (id) {

            $('#hdnProfileID').val(id);
             
            $.ajax({
                type: 'GET',
                url: baseUri + "GetPrivacyVisibilityLevels/" + id,
                datatype: 'json',
                success: function (data) {

                    var anchorsArray = [];

                    $("a.privacy").each(function () {

                        anchorsArray.push($(this));
                    });

                    $.each(anchorsArray, function (index, anchor) {

                        anchor.html($(data).get(index));
                    });
                },
                error: function (xhr, status, p3) {
                    sweetAlert(status);
                }
            });
        }
    });
}
 
//Этот метод срабатывает, когда пользователь кликает по тегу <select> и выбирает значения видимости раздела.
// privacyFlagTypeID - id заголовка(названия раздела, например "Кто видит комментарии к записям")
// visibilityLevelId - уровень видимости(доступности) данного заголовка
function updatePrivacyVisibilityLevel(privacyFlagTypeID, visibilityLevelId) {

    var privacyFlagTypeID = privacyFlagTypeID.replace('privacy_', '');

    var profileId = $('#hdnProfileID').val();

    //объект privacyFlag - связующее звено между профилем юзера и видимость его(профиля) разделов 
    var privacyFlag = {
        ProfileID: profileId,
        PrivacyFlagTypeID: privacyFlagTypeID,
        VisibilityLevelId: visibilityLevelId
    };
    $.ajax({
        type: "PUT",
        url: baseUri + "UpdatePrivacyFlagForChoosenSection",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(privacyFlag),
        async: true,
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }
    });
}


function GetPrivacies(el) {

    $('a.privacy').show();

    //удалить все выпадающие списки (с id='choosePrivacy'), содержащие уровни видимости
    if ($("#choosePrivacy").length)
        $("#choosePrivacy").remove();

    //отобразить ссылки на уровни видимости(на случай если какая-то не отображена)
    $(el).show();

    //узнать на какую ссылку кликнул юзер
    var id = $(el).attr('id');

    //считать текущее значение ссылки(на которую кликнул юзер) чтобы
    //отобразить её значение в выпадающем списке на 1-м месте
    var currentValue = $(el).html();

    //скрыть эту ссылку
    $(el).hide();

    //создается выпадающий список #choosePrivacy на месте ссылки
    $(el).after(

        "<select size='1' id='choosePrivacy'  " +

        //при изменении значения выпадающего списка отобразить скрытую ранее ссылку
        "onchange=\"$('a.privacy').show();" +

        //запись в скрытую ранее ссылку нового значения из выпадающего списка
        "$('a#" + id + "').html(" +
                "$('#choosePrivacy').find('option:selected').text()" +

        "); " +
        //выполнится SQL-команда UPDATE для нвого значения уровня видимости данного раздела
        "updatePrivacyVisibilityLevel('" + id + "',$('select#choosePrivacy option:selected').val());" +
        //по завершении операции удалить этот выпадающий список
        "$(this).remove();" + "\" >" +

        "<option value=\"\" selected disabled>" + currentValue + "</option>" +
        "<option value=\"1\">All users</option>" +
        "<option value=\"2\">Only friends</option>" +
        "<option value=\"3\">Only me</option>" +

        "</select>"

        );
}

