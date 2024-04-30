window.onload = GetEjercicios()

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
            //$("#tbody-tipoejercicios").empty();
            let contenidoTabla = ``;

            $.each(Ejercicios, function (Index, Ejercicio) {  
                contenidoTabla += `
                <tr${Index === Ejercicios.length - 1 ? ' class="ultima-fila fila-resaltar"' : ''} class="fila-resaltar">
                    <td class="blur">${Ejercicio.nombre}</td>
                    <td class="text-center">
                    <button type="button" class="btn btn-success" onclick="AbrirModalEditar(${Ejercicio.idEjercicio})">
                    Editar
                    </button>
                    </td>
                    <td class="text-center">
                    <button type="button" class="btn btn-danger" onclick="EliminarRegistro(${Ejercicio.idEjercicio})">
                    Eliminar
                    </button>
                    </td>
                </tr>
             `;
            

                //  $("#tbody-tipoejercicios").append(`
                //     <tr>
                //         <td>${tipoDeEjercicio.descripcion}</td>
                //         <td class="text-center"></td>
                //         <td class="text-center"></td>
                //     </tr>
                //  `);
            });

            document.getElementById("tbody-tipoejercicios").innerHTML = contenidoTabla;

        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al cargar el listado');
        }
    });
}}

$(document).ready(function() {
    // Utilizamos un evento delegado para que se aplique a las filas que se generen dinámicamente
    $(document).on('mouseenter', '.fila-resaltar', function() {
      $(this).addClass('table-active');
    });
  
    $(document).on('mouseleave', '.fila-resaltar', function() {
      $(this).removeClass('table-active');
    });
  });

function LimpiarModal(){
    document.getElementById("TipoEjercicioID").value = 0;
    document.getElementById("descripcion").value = "";
}

function NuevoRegistro(){
    $("#ModalTitulo").text("Nuevo Tipo de Ejercicio");
}

function AbrirModalEditar(idEjercicio){
    $.ajax({
        // la URL para la petición
        url: '../../Ejercicios/GetEjercicios',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { idEjercicio: idEjercicio},
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (Ejercicios) {
            let ejercicio = Ejercicios[0];
            document.getElementById("TipoEjercicioID").value = idEjercicio;
            $("#ModalTitulo").text("Editar Tipo de Ejercicio");
            document.getElementById("descripcion").value = ejercicio.nombre;
            $("#ModalTipoEjercicio").modal("show");
        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al consultar el registro para ser modificado.');
        }
    });
}

function GuardarRegistro(){
    //GUARDAMOS EN UNA VARIABLE LO ESCRITO EN EL INPUT DESCRIPCION
    let idEjercicio = document.getElementById("TipoEjercicioID").value;
    let nombre = document.getElementById("descripcion").value;
    let eliminado = false
    //POR UN LADO PROGRAMAR VERIFICACIONES DE DATOS EN EL FRONT CUANDO SON DE INGRESO DE VALORES Y NO SE NECESITA VERIFICAR EN BASES DE DATOS
    //LUEGO POR OTRO LADO HACER VERIFICACIONES DE DATOS EN EL BACK, SI EXISTE EL ELEMENTO SI NECESITAMOS LA BASE DE DATOS.
    console.log(nombre);
    $.ajax({
        // la URL para la petición
        url: '../../Ejercicios/GuardarTipoEjercicio',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { idEjercicio: idEjercicio, nombre: nombre},
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (resultado) {

            if(resultado != ""){
                alert(resultado);
            }
            GetEjercicios();
        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al guardar el registro');
        }
    });    
}

function EliminarRegistro(idEjercicio){
    $.ajax({
        // la URL para la petición
        url: '../../Ejercicios/EliminarTipoEjercicio',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { idEjercicio: idEjercicio},
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (resultado) {           
            GetEjercicios();
        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al eliminar el registro.');
        }
    });    

}