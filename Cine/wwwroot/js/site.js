// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.addEventListener('DOMContentLoaded', (event) => {
    const spanMensagem = document.getElementById('spanMensagem');
    const divMensagem = document.getElementById('divMensagem');
    
    if (spanMensagem && divMensagem) {
        if (spanMensagem.classList.length === 0) {
            divMensagem.classList.remove('mb-5');
            divMensagem.classList.add('mb-4');
        } else {
            divMensagem.classList.remove('mb-4');
            divMensagem.classList.add('mb-5');
        }
    }
});