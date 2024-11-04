window.onload = GetEventos

function GetEventos() {{
    $.ajax({
        // la URL para la petición
        url: '../../Eventos/GetEventos',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: {  },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (Eventos) {

            $("#ModalTipoEjercicio").modal("hide");
            LimpiarModal();
            
            let contenidoTabla = ``;

            $.each(Eventos, function (Index, Evento) {  if (Evento.eliminado == false){
                contenidoTabla += `
                <tr${Index === Eventos.length - 1 ? ' class="ultima-fila fila-resaltar"' : ''} class="fila-resaltar">
                    <td class="align-middle" style="font-size: 1.3rem">${Evento.nombre}</td>
                    <td class="align-middle">
                    <button type="button" class="btn btn-icono edit" onclick="AbrirModalEditar(${Evento.eventoID})">
                    <i class="bi bi-pencil-square "></i>
                    </button>
                    </td>
                    <td class="align-middle">
                    <button type="button" class="btn btn-icono delete" onclick="ValidacionEliminar(${Evento.eventoID})">
                    <i class="bi bi-trash3"></i>
                    </button>
                    </td>
                </tr>
             `;
        }});

            document.getElementById("tbody-tipoEvento").innerHTML = contenidoTabla;

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
    $("#ModalTitulo").text("Nuevo Evento");
}

function AbrirModalEditar(EventoID){
    $.ajax({
        // la URL para la petición
        url: '../../Eventos/GetEventos',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { EventoID: EventoID},
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (Evento) {

            console.log(Evento);
            let evento = Evento[0];
            
            document.getElementById("TipoEjercicioID").value = EventoID;
            $("#ModalTitulo").text("Editar Tipo de Ejercicio");
            document.getElementById("descripcion").value = evento.nombre;
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
    let eventoID = document.getElementById("TipoEjercicioID").value;
    let nombre = document.getElementById("descripcion").value;
    let usuarioID = document.getElementById("UsuarioID").value;
    //POR UN LADO PROGRAMAR VERIFICACIONES DE DATOS EN EL FRONT CUANDO SON DE INGRESO DE VALORES Y NO SE NECESITA VERIFICAR EN BASES DE DATOS
    //LUEGO POR OTRO LADO HACER VERIFICACIONES DE DATOS EN EL BACK, SI EXISTE EL ELEMENTO SI NECESITAMOS LA BASE DE DATOS.
   
    $.ajax({
        // la URL para la petición
        url: '../../Eventos/GuardarEvento',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { eventoID: eventoID, nombre: nombre, usuarioID: usuarioID},
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
            GetEventos();
        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al guardar el registro');
        }
    });    
}

function EliminarRegistro(eventoID){
    console.log(eventoID)
    $.ajax({
        // la URL para la petición
        url: '../../Eventos/DesactivarEvento',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { eventoID: eventoID},
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

function ValidacionEliminar(eventoID){
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
        EliminarRegistro(eventoID)
        GetEventos();
        }
    });
}