// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function showAlert(message, type = 'info') {
    return Swal.fire({ text: message, icon: type });
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