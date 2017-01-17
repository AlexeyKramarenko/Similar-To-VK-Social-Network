function goToPeoplePage(friendsOnly, online) {

    var targetUrl = '/people';

    if (online == undefined)
        online = false;

    var UserID = '0';

    if (friendsOnly == true) {

        UserID = document.getElementById('hdnCurrentUserID').value;
    }

    targetUrl += "/UserID=" + UserID + "/Online=" + online;

    location.href = targetUrl;
}