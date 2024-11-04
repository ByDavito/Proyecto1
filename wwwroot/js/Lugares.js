window.onload = GetEjercicios

function GetEjercicios() {{
    let usuarioID = document.getElementById("UsuarioID").value;
    $.ajax({
        // la URL para la petición
        url: '../../Lugares/GetLugares',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { usuarioID: usuarioID},
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (Lugares) {

            $("#ModalTipoEjercicio").modal("hide");
            LimpiarModal();
            //$("#tbody-tipoejercicios").empty();
            let contenidoTabla = ``;

            $.each(Lugares, function (Index, Lugar) {  if (Lugar.eliminado == false){
                contenidoTabla += `
                <tr${Index === Lugares.length - 1 ? ' class="ultima-fila fila-resaltar"' : ''} class="fila-resaltar">
                    <td class="align-middle" style="font-size: 1.3rem">${Lugar.nombre}</td>
                    <td class="align-middle">
                    <button type="button" class="btn btn-icono edit" onclick="AbrirModalEditar(${Lugar.lugarID})">
                    <i class="bi bi-pencil-square "></i>
                    </button>
                    </td>
                    <td class="align-middle">
                    <button type="button" class="btn btn-icono delete" onclick="ValidacionEliminar(${Lugar.lugarID})">
                    <i class="bi bi-trash3"></i>
                    </button>
                    </td>
                </tr>
             `;
        }});

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
    $("#ModalTitulo").text("Nuevo Tipo de Lugar");
}

function AbrirModalEditar(LugarID){
    $.ajax({
        // la URL para la petición
        url: '../../Lugares/GetLugares',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { LugarID: LugarID},
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (Lugares) {

            console.log(Lugares);
            let ejercicio = Lugares[0];
            
            document.getElementById("TipoEjercicioID").value = LugarID;
            $("#ModalTitulo").text("Editar Tipo de Lugar");
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
    let LugarID = document.getElementById("TipoEjercicioID").value;
    let nombre = document.getElementById("descripcion").value;
    let eliminado = false;
    let usuarioID = document.getElementById("UsuarioID").value;
    //POR UN LADO PROGRAMAR VERIFICACIONES DE DATOS EN EL FRONT CUANDO SON DE INGRESO DE VALORES Y NO SE NECESITA VERIFICAR EN BASES DE DATOS
    //LUEGO POR OTRO LADO HACER VERIFICACIONES DE DATOS EN EL BACK, SI EXISTE EL ELEMENTO SI NECESITAMOS LA BASE DE DATOS.
   
    $.ajax({
        // la URL para la petición
        url: '../../Lugares/GuardarLugar',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { LugarID: LugarID, nombre: nombre, usuarioID: usuarioID},
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (resultado) {

            if(resultado != ""){
                Swal.fire(resultado);
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



function EliminarRegistro(LugarID){
    console.log(LugarID)
    $.ajax({
        // la URL para la petición
        url: '../../Lugares/DesactivarTipoEjercicio',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { LugarID: LugarID},
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

function ValidacionEliminar(ejercicioFisicoID){
    Swal.fire
    ({
        title: "¿Estas seguro?",
        text: "Este dato se eliminará!",
        icon: "warning",
        showCancelButton: true,
        cancelButtonText: "No, cancelar!",
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, borrar!"
    })
    .then((result) => 
    {
        if (result.isConfirmed) {
        Swal.fire({
            title: "Borrado!",
            text: "Tu registro ha sido eliminado.",
            icon: "success",
        });
        EliminarRegistro(ejercicioFisicoID)
        }
    });
}