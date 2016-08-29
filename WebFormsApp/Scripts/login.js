getStorageData = function () {

    if (document.getElementById('txtEmail').value != "")
        document.getElementById('txtEmail').value = sessionStorage.getItem('EmailOrPhoneNumber');

    if (document.getElementById('txtPassword').value != "")
        document.getElementById('txtPassword').value = sessionStorage.getItem('Password');
}

setStorageData = function () {

    sessionStorage.setItem('EmailOrPhoneNumber', document.getElementById('txtEmail').value);
    sessionStorage.setItem('Password', document.getElementById('txtPassword').value);
}

window.onload = getStorageData();