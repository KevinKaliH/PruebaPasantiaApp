
function cargarArticulos() {
    $.ajax({
        type: "GET",
        url: "/Articulo/GetAllArticles",
        dataType: 'json',
        data: ({ 'isActive': true }),

        success: function (response) {
            if (response.data != null) {
                resultset = response.data;
            }
        }
    });
}

function realtime_busqueda() {
    var keyPressTimeout;

    $("#search_text").keyup(function () {
        let codigo = this.value;

        if (codigo.trim().length > 0) {
            filter_search(codigo);
        } else {
            articuloSelected = null;
            showFields("", "", "");
        }
    });
}

function filter_search(value) {
    let result = resultset.find(e => e.codigo.startsWith(value));

    if (result != null) {
        articuloSelected = result;
        showFields(result.descripcion, result.precio, result.codigo);
    } else {
        articuloSelected = null;
        showFields("", "", "");
    }
}

function showFields(descripcion, precio,codigo) {
    $("#Descripcion").val(descripcion);
    $("#Precio").val(precio);
    $("#Codigo").val(codigo);

}