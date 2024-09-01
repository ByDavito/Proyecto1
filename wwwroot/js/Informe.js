window.onload = getInforme()

function getInforme() {
    let fechaDesde = document.getElementById("fechaInicio").value;
    let fechaHasta = document.getElementById("fechaFin").value;


    $.ajax({
        // la URL para la petición
        url: '../../EjerciciosFisicos/ListadoInforme',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { fechaDesde : fechaDesde, fechaHasta : fechaHasta},
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (ejercicios) {

            console.log(ejercicios);

            let contenidoTabla = ``;
            
            

            $.each(ejercicios, function (Index, ejercicio) {  

                console.log(ejercicio);
                
                contenidoTabla += `
                <tr${Index === ejercicios.length - 1 ? ' class="ultima-fila fila-resaltar"' : ''} class="fila-resaltar">
                    <td>${ejercicio.tipoEjercicioNombre}</td>
                </tr>
             `;
             $.each(ejercicio.ejercicios, function (Index, data){
                contenidoTabla += `
                <tr>
                    <td></td>
                    <td>${data.inicioString}</td>
                    <td>${data.finString}</td>
                    <td>${data.estadoInicio}</td>
                    <td>${data.estadoFin}</td>
                    <td>${data.observaciones}</td>
                    <td>${data.duracion}</td>
                </tr>
                `
             })

            });

            document.getElementById("tbody-ejerciciosFisicos").innerHTML = contenidoTabla;

        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al cargar el listado');
        }
    });
}