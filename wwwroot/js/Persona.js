window.onload = datosPersona();


document.getElementById("Altura").addEventListener("input", function () {
            
    // Obtener el valor actual del input
let value = $(this).val();

// Eliminar cualquier carácter que no sea un dígito o coma
value = value.replace(/[^0-9,]/g, '');
value = value.replace(/\B(?=(\d{2})+(?!\d))/g, ",")

// Asegurarse de que haya solo una coma y que no esté al inicio
if (value.indexOf(",") !== -1) {
let partes = value.split(",");

partes[1] = partes[1].slice(0, 2); // Limitar los decimales a 2 dígitos
value = partes[0] + ',' + partes[1];
}

// Actualizar el valor del input con el formato final
$(this).val(value);
});

document.getElementById("peso").addEventListener("input", function () {

    // Obtener el valor actual del input
let value = $(this).val();

// Eliminar cualquier carácter que no sea un dígito o coma
value = value.replace(".", ",")
value = value.replace(/[^0-9,]/g, '');
value = value.replace(/^(\d{3})(\d+)/, '$1,$2'); // Inserta la coma después de los primeros 3 dígitos

// Asegurarse de que haya solo una coma y que no esté al inicio
if (value.indexOf(",") !== -1) {
let partes = value.split(",");

partes[0] = partes[0].slice(0, 3);
partes[1] = partes[1].slice(0, 3); // Limitar los decimales a 3 dígitos
value = partes[0] + ',' + partes[1];
}

// Actualizar el valor del input con el formato final
$(this).val(value);
});


function datosPersona(){
    let usuarioID = document.getElementById("UsuarioID").value;
    $.ajax({
        url: "/Persona/GetPersona",
        type: "GET",
        data: { usuarioID: usuarioID },
        dataType: "json",
        success: function (data) {
            
            document.getElementById("Altura").value = data.altura;
            document.getElementById("peso").value = data.peso;
            document.getElementById("fecha").value = data.fechaNacimiento;
            document.getElementById("sexo").value = data.sexo;
            document.getElementById("nombre").value = data.nombre;
        }
    });
    let peso = document.getElementById("Altura").value
    let altura = document.getElementById("peso").value 

    peso = peso.replace(".", ",")
    altura = altura.replace(".", ",")

    document.getElementById("Altura").value = altura;
    document.getElementById("peso").value = peso;
}

function GuardarPersona(){
    let usuarioID = document.getElementById("UsuarioID").value;
    let altura = document.getElementById("Altura").value;
    let peso = document.getElementById("peso").value;
    let fecha = document.getElementById("fecha").value;
    let sexo = document.getElementById("sexo").value;
    let nombre = document.getElementById("nombre").value;

    peso = peso.replace(".", ",")
    altura = altura.replace(".", ",")

    console.log(fecha);
    $.ajax({
        url: "/Persona/GuardarDatosPersona",
        type: "POST",
        data: { usuarioID: usuarioID, altura: altura, peso: peso, fecha: fecha, sexo: sexo, nombre: nombre },
        success: function (data) {
            if (data != "") {
                Swal.fire(data);
            }
        }
    });    
}