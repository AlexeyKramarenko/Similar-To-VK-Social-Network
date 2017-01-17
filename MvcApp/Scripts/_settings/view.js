
function SettingsView() {

    this.getPrivacyFlag = getPrivacyFlag;
    this.getPrivacy = getPrivacy;
    this.onEmailFailure = onEmailFailure;
    this.onPasswordFailure = onPasswordFailure;
    this.onPhoneNumberFailure = onPhoneNumberFailure;
    this.onSuccess = onSuccess;
    this.setProfileId = setProfileId;
    this.showSavingVisibilityLevels = showSavingVisibilityLevels;
     
    function setProfileId(id) {
        $('#hdnProfileID').val(id);
    }
    function showSavingVisibilityLevels(data) {
        var anchorsArray = [];
        $("a.privacy").each(function () {
            anchorsArray.push($(this));
        });
        $.each(anchorsArray, function (index, anchor) {
            var template = data[index];
            anchor.html(template);
        });
    } 
    function getPrivacyFlag(privacyFlagTypeID, visibilityLevelID) {

        //privacyFlag связует раздел сайта и его видимость для разных типов посетителей
        var PrivacyFlag = {
            ProfileID: $('#hdnProfileID').val(),
            PrivacyFlagTypeID: privacyFlagTypeID.replace('privacy_', ''),
            VisibilityLevelID: visibilityLevelID
        };
        return PrivacyFlag;
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
            "vm.updatePrivacyVisibilityLevel('" + id + "',$('select#choosePrivacy option:selected').val());" +
            //по завершении операции удалить этот выпадающий список
            "$(this).remove();" + "\" >" +

            "<option value=\"\" selected disabled>" + currentValue + "</option>" +
            "<option value=\"1\">All users</option>" +
            "<option value=\"2\">Friends</option>" +
            "<option value=\"3\">Only me</option>" +

            "</select>"
            );
    } 
    function onSuccess() {
        alert("Profile updated successfully");
    }
    function onPasswordFailure(data) {

        alert("An error occured");

        //$('#PasswordInfo').html('');
        $('#PasswordInfo').html(data.responseText);
    } 
    function onEmailFailure(data) {

        alert("An error occured");

        //$('#EmailInfo').html('');
        $('#EmailInfo').html(data.responseText);
    } 
    function onPhoneNumberFailure(data) {

        alert("An error occured");

        //$('#PhoneNumberInfo').html('');
        $('#PhoneNumberInfo').html(data.responseText);
    } 
}
 
