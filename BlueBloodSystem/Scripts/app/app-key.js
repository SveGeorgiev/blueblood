$(document).ready(function () {
    if ((sessionStorage.getItem('isAuthenticated') === null || sessionStorage.getItem('isAuthenticated') === "false")
        && (sessionStorage.getItem('isAdmin') === null || sessionStorage.getItem('isAdmin') === "false")) {
        $('#keyModal').modal('show');
    }
    else {
        $('#transactions-wrapper').show();
        $('#search-wrapper').show();
        if (sessionStorage.getItem('isAdmin') !== null && sessionStorage.getItem('isAdmin') === "true") {
            var adminElements = $(".admin");
            for (var i = 0; i < adminElements.length; i++) {
                $(adminElements[i]).show();
            }
        }
    }
});

function sendUserKey() {
    var userKey = $('#userKey').val();
    if (userKey === "8814") {
        sessionStorage.setItem('isAuthenticated', true);
        $('#transactions-wrapper').show();
        $('#search-wrapper').show();
    } else if (userKey === "240514") {
        sessionStorage.setItem('isAdmin', true);
        $('#transactions-wrapper').show();
        $('#search-wrapper').show();
        var adminElements = $(".admin");
        for (var i = 0; i < adminElements.length; i++) {
            $(adminElements[i]).show();
        }
    }
    else {
        sessionStorage.setItem('isAuthenticated', false);
        sessionStorage.setItem('isAdmin', false);
        $('#transactions-wrapper').hide();
        $('#search-wrapper').hide();
        var adminElements = $(".admin");
        for (var i = 0; i < adminElements.length; i++) {
            $(adminElements[i]).hide();
        }
    }
    $('#keyModal').modal('hide');
}