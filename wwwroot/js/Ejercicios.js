function GetEjercicios() {{
    $.ajax({
        url: "/Ejercicios/GetEjercicios",

        data: {id : 0},

        type: "GET",

        success: function (Ejercicios) {

            $.each(Ejercicios, function (i, item) {
                
            })
            
        }
    }