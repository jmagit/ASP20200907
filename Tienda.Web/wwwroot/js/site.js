$(document).ready(function () {
    $(document)
        .ajaxStart(function () {
            $('#trabajandoAJAX').show();
        }).ajaxStop(function () {
            $('#trabajandoAJAX').hide();
        });
});