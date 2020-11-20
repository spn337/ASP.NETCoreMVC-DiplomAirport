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

$(".custom-file-input").on("change", function () {
    var fileLabel = $(this).next('.custom-file-label');
    var files = $(this)[0].files;
    if (files.length > 1) {
        fileLabel.html(files.length + ' files selected');
    }
    else if (files.length == 1) {
        fileLabel.html(files[0].name);
    }
});
