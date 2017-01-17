
function Ajax() {
}

Ajax.prototype.ajaxCall = function (ajaxConfig) {

    var promiseObj = new Promise(function (resolve, reject) {
        var req = $.ajax(ajaxConfig);
        req.success(resolve);
        req.error(reject);
    });

    return promiseObj;
}
