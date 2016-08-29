$(document).ready(function () {

    createShorthands();
})

var lastDialogId = -1;

$('.linkToDialog').click(function () {

    var id = $(this).attr("data-dialogid");

    if (lastDialogId != id) {

        $("#conversation").empty();

        lastDialogId = id;

        $.ajax({
            url: '/WebServices/DialogService.asmx/GetDialogByID',
            type: 'GET',
            datatype: 'json',
            contentType: 'application/json; charset=utf-8',
            data: { id: id },

            success: function (data, status) {

                $("#conversation").append(data.d);


            },
            error: function (xhr, status, p3) {
                sweetAlert(status);
            }
        })
    }
});




function getUsersInfoForDialogForm(elem) {

    var d = {

        info: {
            receiversId: $(elem).attr('data-interlocutoruserid')
        }
    }
    $.ajax({
        url: '/WebServices/DialogService.asmx/GetUsersInfoForDialogForm',
        type: 'POST',
        datatype: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(d),

        success: function (data) {

            $("table#write_form").html("");

            var tbody =
                  "<tbody>" +
                    "<tr>" +
                        "<td>" +
                            "<img class='avatar' src='" + data.d.CurrentUserAvatar + "' />" +
                        "</td>" +
                        "<td id='Message'>" +
                            "<textarea></textarea>" +
                        "</td>" +
                        "<td>" +
                            "<img class='avatar' src='" + data.d.InterlocutorUserAvatar + "' />" +
                        "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>" + data.d.CurrentUsername + "</td>" +
                        "<td>" +
                            "<div>" +
                                "<input id='sendMessage' onclick='sendMessageToInterlocutor()' type='button' value='Send' />" +
                            "</div>" +
                            "<div></div>" +
                        "</td>" +
                        "<td>" + data.d.InterlocutorUsername + "</td>" +
                    "</tr>" +
                "</tbody>";

            $("table#write_form").append(tbody);
        },
        error: function (xhr, status, p3) {
            sweetAlert(status);
        }
    })
}

function sendMessageToInterlocutor() {

    var FromUserId = $('#hdnCurrentUserID').val();
    var ToUserId = $('#hdnMyCurrentInterlocutor').attr('value');
    var dialogID = $('#hdnActiveDialogID').attr('value');
    var message = $('textarea').val();

    var hub = $.connection.chatHub.server;


    hub.sendMessage(FromUserId, ToUserId, dialogID, message);

}


function CheckRelationshipType(elem) {

    var selectedEl = $(elem).prev();

    var relationshipID = selectedEl.val();

    var senderUserId = $(elem).attr('data-senderid');

    var obj = {
        senderUserId: senderUserId,
        relationshipId: relationshipID
    }
    $.ajax({
        url: '/WebServices/DialogService.asmx/AddRelationshipDefinition',
        type: 'POST',
        datatype: 'json',
        contentType: 'application/json; charset=utf-8',

        data: JSON.stringify(obj),

        success: function (data) {

            switch (relationshipID) {

                case 1:
                    alert("Поздравляем Вас с приобретением нового друга.");
                    break;

                case 2:
                    alert("Поздравляем Вас, теперь у Вас есть подписчик.");
                    break;
            }
        },
        error: function (xhr, status, p3) {
            alert();
        }
    })
}



function createShorthands() {

    var shorthand = $(".dialogs_comment>p").text().substring(0, 30) + '...';

    $(".dialogs_comment>p").text(shorthand);
}