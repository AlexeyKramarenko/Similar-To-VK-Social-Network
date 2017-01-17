function MessagesView() {
    this.init();
}

MessagesView.prototype = function () {

    return {
        createDialogForm: createDialogForm,
        checkDialogID: checkDialogID,
        getReceiversInfo: getReceiversInfo,
        getRelationshipDefinition: getRelationshipDefinition,
        getIdOfDialog: getIdOfDialog,
        init: init,
        showDialog: showDialog
    }

    var STATIC = null;

    function init() {
        STATIC = {
            lastDialogId: -1
        };
    }
    function getReceiversInfo(sender) {

        //for asmx service
        var d = {
            info: {
                receiversId: $(sender).attr('data-interlocutoruserid')
            }
        }
        return d;
    }

    function createDialogForm(dialogForm) {

        var tbody =
              "<tbody>" +
                "<tr>" +
                    "<td>" +
                        "<img class='avatar' src='" + dialogForm.d.CurrentUserAvatar + "' />" +
                    "</td>" +
                    "<td id='Message'>" +
                        "<textarea></textarea>" +
                    "</td>" +
                    "<td>" +
                        "<img class='avatar' src='" + dialogForm.d.InterlocutorUserAvatar + "' />" +
                    "</td>" +
                "</tr>" +
                "<tr>" +
                    "<td>" + dialogForm.d.CurrentUsername + "</td>" +
                    "<td>" +
                        "<div>" +
                            "<input id='sendMessage' onclick='signalr.sendMessageToInterlocutor()' type='button' value='Send' />" +
                        "</div>" +
                        "<div></div>" +
                    "</td>" +
                    "<td>" + dialogForm.d.InterlocutorUsername + "</td>" +
                "</tr>" +
            "</tbody>";

        $("table#write_form").html(tbody);
    }

    function getRelationshipDefinition(sender) {
        var selectedEl = $(sender).prev();
        var relationshipID = selectedEl.val();
        var senderUserId = $(sender).attr('data-senderid');
        var obj = {
            senderUserId: senderUserId,
            relationshipId: relationshipID
        }
        return obj;
    }


    function checkDialogID(id) {
        if (id != STATIC.lastDialogId) {
            STATIC.lastDialogId = id;
            return true;
        }
        return false;
    }

    function showDialog(messages) {

        var html = "";

        for (var i = 0; i < messages.d.length; i++) {
            html +=
                       "<tr>" +
                            "<td>" +
                             "<span class='UserName'>" + messages.d[i].InterlocutorUserName + "<br/></span>" +
                             "</td>" +
                             "<td></td>" +
                             "<td></td>" +
                       "</tr>" +

                       "<tr>" +
                            "<td class='log_author'>" +
                                "<img src='" + messages.d[i].InterlocutorAvatar + "' />" +
                            "</td>" +
                            "<td class='log_body'><p>" + messages.d[i].MessageText + "</p></td>" +
                            "<td class='log_date'>" + messages.d[i].CreateDate + "</td>" +
                       "</tr>";
        }
        $("#conversation").append(html);
        $(".myBox").scrollTop(1000000);
    }

    function getIdOfDialog(sender) {
        var id = $(sender).attr("data-dialogid");
        $('#hdnActiveDialogID').val(id);
        return id;
    }
}()
