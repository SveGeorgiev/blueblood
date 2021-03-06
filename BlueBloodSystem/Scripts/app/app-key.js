﻿$(document).ready(function () {
    if ((sessionStorage.getItem('isAuthenticated') === null || sessionStorage.getItem('isAuthenticated') === "false")
        && (sessionStorage.getItem('isAdmin') === null || sessionStorage.getItem('isAdmin') === "false")) {
        $('#keyModal').modal('show');
    }
    else {
        $('#content-wrapper').show();
        $('#search-wrapper').show();
        if (sessionStorage.getItem('isAdmin') !== null && sessionStorage.getItem('isAdmin') === "true") {
            var adminElements = $(".admin");
            for (var i = 0; i < adminElements.length; i++) {
                $(adminElements[i]).show();
            }
        }
        $('#wrongPassword').hide();
    }

    $('.modal-dialog').on('keypress', function (e) {
        if (e.keyCode === 13) {
            e.preventDefault();
            $('#modalDialogSubmitButton').click();
        }
    });
});

function sendUserKey() {
    var userKey = $('#userKey').val();
    if (userKey === "8814") {
        $('#wrongPassword').hide();
        sessionStorage.setItem('isAuthenticated', true);
        $('#content-wrapper').show();
        $('#search-wrapper').show();
    } else if (userKey === "240514") {
        $('#wrongPassword').hide();
        sessionStorage.setItem('isAdmin', true);
        $('#content-wrapper').show();
        $('#search-wrapper').show();
        var adminElements = $(".admin");
        for (var i = 0; i < adminElements.length; i++) {
            $(adminElements[i]).show();
        }
    }
    else {
        sessionStorage.setItem('isAuthenticated', false);
        sessionStorage.setItem('isAdmin', false);
        $('#content-wrapper').hide();
        $('#search-wrapper').hide();
        var adminElements = $(".admin");
        for (var i = 0; i < adminElements.length; i++) {
            $(adminElements[i]).hide();
        }
        $('#wrongPassword').show();
    }
    $('#keyModal').modal('hide');
}