window.onload = getEstadoEmocional()

function GetEjercicios() {{
    $.ajax({
        // la URL para la petición
        url: '../../Ejercicios/GetEjercicios',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: {  },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (Ejercicios) {

            $("#ModalTipoEjercicio").modal("hide");
            LimpiarModal();
            let contenidoDropdown = ``;
            

            $.each(Ejercicios, function (Index, Ejercicio) {  
                
                contenidoDropdown += `
                <li><a class="dropdown-item" href="#">${Ejercicio.nombre}</a></li>
             `;

            });

            document.getElementById("dropdown-Ejercicio").innerHTML = contenidoDropdown;

        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al cargar el listado');
        }
    });
}}


function getEstadoEmocional(){
    $.ajax({
         // la URL para la petición
         url: '../../EjerciciosFisicos/GetEstadoEmocional',
         // la información a enviar
         // (también es posible utilizar una cadena de datos)
         data: {  },
         // especifica si será una petición POST o GET
         type: 'POST',
         // el tipo de información que se espera de respuesta
         dataType: 'json',
         // código a ejecutar si la petición es satisfactoria;
         // la respuesta es pasada como argumento a la función
         success: function (EstadoEmocional) {
 
             $("#ModalTipoEjercicio").modal("hide");
             LimpiarModal();
             let contenidoDropdown = ``;
             
 
             $.each(EstadoEmocional, function (Index, EjercicioFisico) {  
                 
                 contenidoDropdown += `
                 <li><a class="dropdown-item" href="#">${EjercicioFisico.estadoEmocional}</a></li>
              `;
 
             });
 
             document.getElementById("dropdown-Ejercicio").innerHTML = contenidoDropdown;
 
         },
 
         // código a ejecutar si la petición falla;
         // son pasados como argumentos a la función
         // el objeto de la petición en crudo y código de estatus de la petición
         error: function (xhr, status) {
             console.log('Disculpe, existió un problema al cargar el listado');
         }
     });
    }
