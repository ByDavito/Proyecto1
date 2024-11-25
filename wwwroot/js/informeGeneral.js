window.onload = getInforme()

function generarColorRojo() {
    // El valor de GG será alto (de 128 a 255) para garantizar que predomine el verde.
    // Los valores de RR y BB serán bajos (de 0 a 127).

    let rr = Math.floor(Math.random() * 100) + 128; // 0 a 127
    let gg = Math.floor(Math.random() * 100); // 128 a 255
    let bb = Math.floor(Math.random() * 100); // 0 a 127
    let aa = Math.round(0.389 * 255);

    // Convertimos a hexadecimal y formateamos para que tenga siempre dos dígitos.
    let colorHex = `#${rr.toString(16).padStart(2, '0')}${gg.toString(16).padStart(2, '0')}${bb.toString(16).padStart(2, '0')}${aa.toString(16).padStart(2, '0')}`;
    return colorHex;
}

function changeColor() {
    
        const elementos = document.querySelectorAll('.td-selector');

        console.log(elementos);
        $.each(elementos, function (Index, elemento){
        
        if (elemento.getAttribute('style')) {
            elemento.removeAttribute('style');
        } else if(Index === elementos.length  == 0) {
            getInforme();
            return false;
        }
    });
    }


function getInforme() {
    let fechaDesde = document.getElementById("fechaDesde").value;
    let fechaHasta = document.getElementById("fechaHasta").value;
    let usuarioID = document.getElementById("UsuarioID").value;
    let color = "";

    $.ajax({
        // la URL para la petición
        url: '../../Eventos/InformeCompleto',
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

                color = generarColorRojo();

                
                contenidoTabla += `
                <tr class="fila-resaltar">
                    <td style="background-color: ${color}" class="td-selector">${ejercicio.nombre}</td>
                    <td style="background-color: ${color}" class="td-selector"></td>
                    <td style="background-color: ${color}" class="td-selector"></td>
                    <td style="background-color: ${color}" class="td-selector"></td>
                    <td style="background-color: ${color}" class="td-selector"></td>
                    <td style="background-color: ${color}" class="td-selector hidden-mobile"></td>
                    <td style="background-color: ${color}" class="td-selector hidden-mobile"></td>
                    <td style="background-color: ${color}" class="td-selector hidden-mobile"></td>
                    <td style="background-color: ${color}" class="td-selector"></td>
                </tr>
             `;
             $.each(ejercicio.vistaLugar, function (Index, data){
                contenidoTabla += `
                <tr class="fila-resaltar">
                    <td style="background-color: ${color}" class="td-selector"></td>
                    <td style="background-color: ${color}" class="td-selector">${data.nombre}</td>
                    <td style="background-color: ${color}" class="td-selector"></td>
                    <td style="background-color: ${color}" class="td-selector"></td>
                    <td style="background-color: ${color}" class="td-selector"></td>
                    <td style="background-color: ${color}" class="td-selector hidden-mobile"></td>
                    <td style="background-color: ${color}" class="td-selector hidden-mobile"></td>
                    <td style="background-color: ${color}" class="td-selector hidden-mobile"></td>
                    <td style="background-color: ${color}" class="td-selector"></td>

                </tr>
                `

                $.each(data.vistaTipoEjercicios, function (Index, item){
                    contenidoTabla += `
                    <tr class="fila-resaltar">
                        <td style="background-color: ${color}" class="td-selector"></td>
                        <td style="background-color: ${color}" class="td-selector"></td>
                        <td style="background-color: ${color}" class="td-selector">${item.nombre}</td>
                        <td style="background-color: ${color}" class="td-selector"></td>
                        <td style="background-color: ${color}" class="td-selector"></td>
                        <td style="background-color: ${color}" class="td-selector hidden-mobile"></td>
                        <td style="background-color: ${color}" class="td-selector hidden-mobile"></td>
                        <td style="background-color: ${color}" class="td-selector hidden-mobile"></td>
                        <td style="background-color: ${color}" class="td-selector"></td>
                    </tr>
                    `
                    $.each(item.vistaEjerciciosGeneral, function (Index, data){
                        contenidoTabla += `
                        <tr class="fila-resaltar">
                            <td style="background-color: ${color}" class="td-selector"></td>
                            <td style="background-color: ${color}" class="td-selector"></td>
                            <td style="background-color: ${color}" class="td-selector"></td>
                            <td style="background-color: ${color}" class="td-selector">${data.finString}</td>
                            <td style="background-color: ${color}" class="td-selector">${data.inicioString}</td>
                            <td style="background-color: ${color}" class="td-selector hidden-mobile">${data.estadoInicio}</td>
                            <td style="background-color: ${color}" class="td-selector hidden-mobile">${data.estadoFin}</td>
                            <td style="background-color: ${color}" class="td-selector hidden-mobile">${data.observaciones}</td>
                            <td style="background-color: ${color}" class="td-selector">${data.kcal}</td>
                        </tr>
                        `
                     })
                 })
             })

             let elementos = document.querySelectorAll('.section-bg');

             elementos.forEach(elemento => {
                elemento.style.backgroundColor = "rgb(200,30,2,0.5)";
            });

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