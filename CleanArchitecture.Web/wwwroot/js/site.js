
$('#edit-name').on('keypress', function (event) {
    var regex = new RegExp("^[a-zA-Z0-9]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});

ValidateAlphanumeric('edit-year', 4);
ValidateAlphanumeric('edit-wins', 2);

function ValidateAlphanumeric(id, lenght) {
    $('#' + id).on('keypress', function (event) {
        var value = document.getElementById(id).value;
        text = window.getSelection().toString();
        if (value.length < lenght || text != '') {
            return true;
        }
        return false;
    });
}