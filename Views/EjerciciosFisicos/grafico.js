function mostrarGrafico() {
    let ejercicio = document.getElementById("EjercicioFisicoID").value;
    let mes = document.getElementById("Fecha").value;
    let año = document.getElementById("FechaHasta").value;

    $.ajax({
        // la URL para la petición
        url: '../../EjerciciosFisicos/GraficoEjecicios',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { tipoEjercicio : tipoEjercicio, mes : mes, anio : anio },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (Ejercicios) {
    
        }});
}