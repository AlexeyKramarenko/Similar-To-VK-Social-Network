var users = (function () {
    function findUsers(friendsOnly, online) {

        var pathName = window.location.pathname;
        
        if (pathName.lastIndexOf('/people', 0) === 0) {

            if (vm) {
                var userID = null;
                var newUrl = null;                

                if (friendsOnly === true) {
                    userID = document.getElementById('hdnCurrentUserID').value;                                       
                    view.setFriendsSearchMode();
                }
                else {
                    userID = 0;
                    view.setPeopleSearchMode();
                } 
                newUrl = "/people/UserID=" + userID + "/Online=" + online;
                history.pushState({}, null, newUrl);
                vm.getUsers();
            }
        }
        else {
            var targetUrl = '/people';
            if (online == undefined)
                online = false;
            var UserID = 0;
            if (friendsOnly === true) {
                UserID = document.getElementById('hdnCurrentUserID').value;
            }
            targetUrl += "/UserID=" + UserID + "/Online=" + online;
            location.href = targetUrl;
        }
    }
    return {
        findUsers: findUsers
    }
})()