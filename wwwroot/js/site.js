// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {
    var menuIcon = document.querySelector('.navbar-menu-icon');
    var offcanvasBody = document.querySelector('.offcanvas-body');
    var offcanvas = document.getElementById('offcanvasNavbar'); // Accedemos al offcanvas existente
    var animacion = document.querySelector('.menu__icon');
  
    // Movemos el botón al offcanvas cuando se muestra
    offcanvas.addEventListener('shown.bs.offcanvas', function () {
    
      menuIcon.parentNode.removeChild(menuIcon); // Eliminamos el botón de su posición actual
      offcanvasBody.insertBefore(menuIcon, offcanvasBody.firstChild); // Lo agregamos al offcanvas
      animacion.focus();
    });
  
    // Movemos el botón al navbar cuando se oculta
    offcanvas.addEventListener('hidden.bs.offcanvas', function () {
      menuIcon.parentNode.removeChild(menuIcon); // Eliminamos el botón de su posición actual
      document.querySelector('.container-fluid').appendChild(menuIcon); // Lo agregamos al navbar
      animacion.blur();
    });
  });