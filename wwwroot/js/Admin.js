window.onload = getUsuarios();

function getUsuarios() {
    $.ajax({
        // la URL para la petición
        url: '../../Admin/GetCuentas',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: {},
        // especifica si la petición es de tipo POST o GET
        type: 'GET',
        // el tipo de información que se espera de respuesta
        success: function (usuarios) {
            const tabla = document.getElementById('List-Usuarios');
            let contenidoTabla = ``;
            console.log(usuarios);
            $.each(usuarios, function (index, usuario) {
                contenidoTabla += `
                <tr${Index === usuario.length - 1 ? ' class="ultima-fila fila-resaltar"' : ''} class="fila-resaltar">
                    <td class="borde-td align-middle"><div><p>${usuario.nombre}</p></div></td>
                    <td class="borde-td align-middle"><div><p>${usuario.email}</p></div></td>
                    <td class="borde-td align-middle"><div><p>${usuario.altura}</p></div></td>
                    <td class="borde-td align-middle" style="max-width: 8rem;"><div><p>${usuario.peso}</p></div></td>
                    <td class="borde-td align-middle" style="max-width: 8rem;"><div><p>${usuario.sexo}</p></div></td>
                    <td class="borde-td align-middle"><div><p>${usuario.rol}</p></div></td>
                    </td>
                </tr>`;
            });
            tabla.innerHTML = contenidoTabla;
        },
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al guardar el registro');
        }
    });
}