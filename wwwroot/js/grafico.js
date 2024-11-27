
document.addEventListener('DOMContentLoaded', (event) => {
    const select = document.getElementById('MesEjercicioBuscar');
    const currentMonth = new Date().getMonth() + 1;
    select.value = currentMonth;
});

let graficoEjercicio;
let graficoDonutEjercicio;

$("#MesEjercicioBuscar, #AnioEjercicioBuscar").change(function () {
    if (graficoDonutEjercicio) graficoDonutEjercicio.destroy();
    if (graficoEjercicio) graficoEjercicio.destroy();
    mostrarGrafico();
    GraficoDonut();
});

$("#TipoEjercicioBuscarID").change(function () {
    if (graficoEjercicio) graficoEjercicio.destroy();
    mostrarGrafico();
});

window.onload = function() {
    // Setear el select en el mes actual
    const select = document.getElementById('MesEjercicioBuscar');
    const currentMonth = new Date().getMonth() + 1;
    select.value = currentMonth;

    // Llamar a las funciones después de setear el select
    mostrarGrafico();
    GraficoDonut();
};



function mostrarGrafico() {



    let TipoEjercicioID = document.getElementById("TipoEjercicioBuscarID").value;
    let mes = document.getElementById("MesEjercicioBuscar").value;
    let anio = document.getElementById("AnioEjercicioBuscar").value;
    let usuarioID = document.getElementById("UsuarioID").value;

    $.ajax({
        // la URL para la petición
        url: '../../EjerciciosFisicos/GraficoLinearEjecicios',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { TipoEjercicioID: TipoEjercicioID, mes: mes, anio: anio, usuarioID: usuarioID},
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (ejercicioFisicos) {

            console.log(ejercicioFisicos);

            let labels = [];
            let data = [];
            let diasConEjercicio = 0;
            let totalMinutos = 0;

            $.each(ejercicioFisicos, function (index, ejercicioFisico) {
                labels.push(ejercicioFisico.dia );
                data.push(ejercicioFisico.cantidadMinutos);

                totalMinutos += ejercicioFisico.cantidadMinutos;

                if (ejercicioFisico.cantidadMinutos > 0) 
                {diasConEjercicio +=1;}

                var inputEjercicio = document.getElementById("TipoEjercicioBuscarID");

                var nombreEjercicio = inputEjercicio.options[inputEjercicio.selectedIndex].text;

                let diasSinEjercicio = ejercicioFisicos.length - diasConEjercicio;


                $("#ConEjercicio").text(totalMinutos + " MINUTOS EN " + diasConEjercicio + " DÍAS");
            $("#SinEjercicio").text(diasSinEjercicio + " DÍAS SIN "+ nombreEjercicio);

            });
            const ctx = document.getElementById('grafico_linea');

            graficoEjercicio = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: labels,
                    datasets: [{
                        label: ' Minutos',
                        data: data,
                        borderColor: '#d00f0f',
                        backgroundColor: '#d00f0f',
                        tension: 0.3,
                        borderWidth: 1
                    }]
                },
                options: {scales: {
                    y: {
                        beginAtZero: true,
                        ticks: {
                            fontColor: 'white'
                        }
                    },
                    x:{
                        ticks: {
                            fontColor: 'white'}
                    },
                    
                    }
                }
            })
        }
    })
}




function GraficoDonut(){

    let mes = document.getElementById("MesEjercicioBuscar").value;
    let anio = document.getElementById("AnioEjercicioBuscar").value;
    let usuarioID = document.getElementById("UsuarioID").value;

    $.ajax({
        type: "POST",
        url: '../../EjerciciosFisicos/GraficoCircularEjecicios',
        data: {mes: mes, anio: anio, usuarioID: usuarioID},
        success: function (VistaTipoEjercicioFisico) {
            console.log(VistaTipoEjercicioFisico);
           
            let labels = [];
            let data = [];
            let fondo = [];
            $.each(VistaTipoEjercicioFisico, function (index, tipoEjercicio) {

                labels.push(tipoEjercicio.nombre);
                var color = generarColorRojo();
                fondo.push(color);
                data.push(tipoEjercicio.cantidadMinutos);

            });

            const ctxPie = document.getElementById("grafico_donut");
            graficoDonutEjercicio = new Chart(ctxPie, {
                type: 'doughnut',
                data: {
                    labels: labels,
                    datasets: [{
                        data: data,
                        backgroundColor: fondo,
                        borderColor: 'transparent',
                        spacing: 5,
                        borderRadius: 20,
                        
                    }],
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        tooltip: {
                            enabled: true
                        }
                    },
                    cutout: '85%'
                }
            });
        }
    });
}

function generarColorRojo() {
    // El valor de GG será alto (de 128 a 255) para garantizar que predomine el verde.
    // Los valores de RR y BB serán bajos (de 0 a 127).

    let rr = Math.floor(Math.random() * 128) + 128; // 0 a 127
    let gg = Math.floor(Math.random() * 128); // 128 a 255
    let bb = Math.floor(Math.random() * 128); // 0 a 127

    // Convertimos a hexadecimal y formateamos para que tenga siempre dos dígitos.
    let colorHex = `#${rr.toString(16).padStart(2, '0')}${gg.toString(16).padStart(2, '0')}${bb.toString(16).padStart(2, '0')}`;
    return colorHex;
}

