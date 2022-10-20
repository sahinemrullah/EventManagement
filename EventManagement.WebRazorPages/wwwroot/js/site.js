// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('[data-value]').on('click', function () {
        $('#page_number').val($(this).attr('data-value'));
        $('#page_form').submit();
    });
});

function showList(e) {
    var $gridCont = $('.grid-container');
    e.preventDefault();
    $gridCont.hasClass('list-view') ? $gridCont.removeClass('list-view') : $gridCont.addClass('list-view');
}

function gridList(e) {
    var $gridCont = $('.grid-container')
    e.preventDefault();
    $gridCont.removeClass('list-view');
}

function fireDialog(title, message, type) {
    swal.fire({
        title: title,
        html: `<pre>${message}</pre>`,
        icon: type
    });
}

function fireDialogRedirectOnClose(title, message, type, redirectUrl) {
    swal.fire({
        title: title,
        html: `<pre>${message}</pre>`,
        icon: type
    }).then(function () {
        {
            window.location = redirectUrl;
        }
    });
}

$(document).on('click', '.btn-grid', gridList);
$(document).on('click', '.btn-list', showList);