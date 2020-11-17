function showConfirmDeleteSpan(id, isDeleteCliked) {
    var deleteSpan = 'deleteSpan_' + id;
    var confirmDeleteSpan = 'confirmDeleteSpan_' + id;

    if (isDeleteCliked) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    }
    else {
        $('#' + deleteSpan).show();
        $('#' + confirmDeleteSpan).hide();
    }
}