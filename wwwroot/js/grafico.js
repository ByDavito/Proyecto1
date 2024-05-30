
window.onload = mostrarGrafico

let graficoEjercicio


function mostrarGrafico() {



    let tipoEjercicio = document.getElementById("TipoEjercicioBuscarID").value;
    let mes = document.getElementById("MesEjercicioBuscar").value;
    let anio = document.getElementById("AnioEjercicioBuscar").value;

    $.ajax({
        // la URL para la petición
        url: '../../EjerciciosFisicos/GraficoEjecicios',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { tipoEjercicio: tipoEjercicio, mes: mes, anio: anio },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (ejercicioFisicos) {

            let labels = [];
            let data = [];

            $.each(ejercicioFisicos, function (index, ejercicioFisico) {
                labels.push(ejercicioFisico.dia + "de" + ejercicioFisico.mes);
                data.push(ejercicioFisico.cantidadMinutos);

            });
            const ctx = document.getElementById('grafico_linea');

            graficoEjercicio = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: labels,
                    datasets: [{
                        label: '# of Votes',
                        data: data,
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            })
        }
    })
}