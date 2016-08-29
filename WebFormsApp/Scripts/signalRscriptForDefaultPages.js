$(function () {


    // Declare a proxy to reference the hub. 
    var chatHub = $.connection.chatHub;
    var userId = $('#hdnUserID').val();

    // Start Hub
    $.connection.hub.start().done(function () {

        chatHub.server.connect(userId);

    });

});


