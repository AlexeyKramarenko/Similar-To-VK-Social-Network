function checkUrl(UserID) {

    var currentPage = window.location.pathname;

    sessionStorage.setItem('human', document.getElementById('txtName').value);  

    if (currentPage != '/people.aspx') {

        var url = '/people.aspx';

        if (UserID != null)
            url += '?UserID=' + UserID;

        location.href = url;
    }
}