// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function animacionMenu() {
    var menuIcon = document.getElementById('botonMenu');
    if (menuIcon.classList.contains('animated')) {
        menuIcon.classList.remove('animated');

    }
    else{
        menuIcon.classList.add('animated');
    }
    
}

