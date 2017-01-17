
function SettingsView() { }

SettingsView.prototype = function () {

    return {
        getPrivacyFlag: getPrivacyFlag,
        getPrivacy: getPrivacy,
        setProfileId: setProfileId,
        showSavingVisibilityLevels: showSavingVisibilityLevels, 
    }

    function setProfileId(id) {
        $('#hdnProfileID').val(id);
    }
    function showSavingVisibilityLevels(data) {
        //var values = JSON.parse(data);
        var anchorsArray = [];
        $("a.privacy").each(function () {
            anchorsArray.push($(this));
        });
        $.each(anchorsArray, function (index, anchor) {
            var template = data[index];
            anchor.html(template);
        });
    }
    function getPrivacyFlag(privacyFlagTypeID, visibilityLevelId) {

        //privacyFlag связует раздел сайта и его видимость для разных типов посетителей
        var privacyFlag = {
            ID : 0,
            ProfileID: $('#hdnProfileID').val(),
            PrivacyFlagTypeID: privacyFlagTypeID.replace('privacy_', ''),
            VisibilityLevelId: visibilityLevelId
        };
        return privacyFlag;
    }
    function getPrivacy(sender) {

        $('a.privacy').show();

        //удалить все выпадающие списки (с id='choosePrivacy'), содержащие уровни видимости
        if ($("#choosePrivacy").length)
            $("#choosePrivacy").remove();

        //отобразить ссылки на уровни видимости(на случай если какая-то не отображена)
        $(sender).show();

        //узнать на какую ссылку кликнул юзер
        var id = $(sender).attr('id');

        //считать текущее значение ссылки(на которую кликнул юзер) чтобы
        //отобразить её значение в выпадающем списке на 1-м месте
        var currentValue = $(sender).text();

        //скрыть эту ссылку
        $(sender).hide();

        //создается выпадающий список #choosePrivacy на месте ссылки
        $(sender).after(

            "<select size='1' id='choosePrivacy'  " +

            //при изменении значения выпадающего списка отобразить скрытую ранее ссылку
            "onchange=\"$('a.privacy').show();" +

            //запись в скрытую ранее ссылку нового значения из выпадающего списка
            "$('a#" + id + "').html(" +
                    "$('#choosePrivacy').find('option:selected').text()" +

            "); " +
            //выполнится SQL-команда UPDATE для нового значения уровня видимости данного раздела
            "return vm.updatePrivacyVisibilityLevel(" + id + ",$('select#choosePrivacy').val());" +
            //по завершении операции удалить этот выпадающий список
            "$(this).remove();" + "\" >" +

            "<option value=\"\" selected disabled>" + currentValue + "</option>" +
            "<option value=\"1\">All users</option>" +
            "<option value=\"2\">Friends</option>" +
            "<option value=\"3\">Only me</option>" +

            "</select>"
            );
    }
}()
