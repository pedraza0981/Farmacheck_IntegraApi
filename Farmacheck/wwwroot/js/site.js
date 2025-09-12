// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    const $btnAsignaciones = $("#btnAsignaciones");

    $("#btnAgregarChecklist").on("click", function () {
        $btnAsignaciones.hide();
    });

    $("#btnEditarChecklist").on("click", function () {
        $btnAsignaciones.show();
    });
});
function showAlert(message, type = 'info') {
    return Swal.fire({ text: message, icon: type }).then(result => {
        const modal = $('#processingModal');
        if (modal.length) {
            modal.modal('hide');
        }
        return result;
    });
}

function confirmAction(message) {
    return Swal.fire({
        text: message,
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Aceptar',
        cancelButtonText: 'Cancelar'
    }).then(result => result.isConfirmed);
}