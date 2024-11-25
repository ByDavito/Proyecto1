window.onload = getInformeLugares()

function getInformeLugares() {
    let fechaDesde = document.getElementById("fechaDesde").value;
    let fechaHasta = document.getElementById("fechaHasta").value;
    let usuarioID = document.getElementById("UsuarioID").value;
    $.ajax({
        
        // la URL para la petición
        url: '../../EjerciciosFisicos/informeLugar',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { fechaDesde : fechaDesde, fechaHasta : fechaHasta, usuarioID : usuarioID },
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
                <tr class="fila-resaltar">
                    <td>${ejercicio.nombre}</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
             `;
             $.each(ejercicio.lugarEjercicios, function (Index, data){
                contenidoTabla += `
                <tr class="fila-resaltar">
                    <td></td>
                    <td>${data.inicioString}</td>
                    <td>${data.finString}</td>
                    <td>${data.ejercicioNombre}</td>
                    <td>${data.observaciones}</td>
                    <td>${data.duracion} min</td>
                    <td>${data.kcal} Kcal</td>

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