
//variables globales
var resultset = new Array();
var articuloSelected;
var detalles = new Array();
var impuestoStatic = 0.05;

var subtotal_global = 0.0;
var impuesto_global = impuestoStatic;
var total_global = 0.0;

var tbody_content = document.getElementById("tbody-articles");
var tableContent = document.querySelector('#table-content');

$(document).ready(function () {
    $("#lds-roller").hide();


    $("#valorImp").val(impuestoStatic * 100);
    $("#Impuesto").val(impuestoStatic * 100);

    asignar_fechaHoy()
    cargarArticulos();
    realtime_busqueda();
    $("#btn-agregar").click(validate_add_articulo);
});

function asignar_fechaHoy() {
    let fecha = new Date();
    var mes = (fecha.getMonth() + 1);
    var dia = fecha.getDate();
    if (mes < 10)
        mes = "0" + mes;
    if (dia < 10)
        dia = "0" + dia;

    var hoy = fecha.getFullYear() + '-' + mes + '-' + dia;

    $('#FechaRegistro').val(hoy);
    //$('#FechaRegistro').val(new Date().toJSON().slice(0, 19));
}

function validate_add_articulo() {
    
    if (articuloSelected != null) {
        let cant = $("#Cantidad").val();

        if (!isNaN(cant)) {
            if (cant <= 0) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Cantidad no puede ser menor que 0!',
                });
            } else {
                let find = detalles.find(e => e.id == articuloSelected.id);
                if (find == null)
                    agregar_articulo(cant);
                else
                    Swal.fire({
                        icon: 'error',
                        title: 'El producto ya fue agregado!!',
                        text: 'Para modificar cantidad, porfavor hacer click en Quitar! y agregarlo nuevamente',
                    });
            }
        } else {
            let find = detalles.find(e => e.id == articuloSelected.id);
            if (find != null)
                Swal.fire({
                    icon: 'advertise',
                    title: 'Cantidad Requerida...',
                    text: 'Ingresar cantidad a facturar!',
                });
        }
    }
}

function agregar_articulo(cant) {
    let info = articuloSelected;
    let subt = cant * articuloSelected.precio;
    let tot = subt + (subt * impuestoStatic);

    calcula_valores(1, tot);

    let data = {
        Id: articuloSelected.id,
        precio: articuloSelected.precio,
        cantidad: cant,
        subtotal: subt,
        impuesto: impuestoStatic,
        total: tot
    };

    detalles.push(data);

    tbody_content.innerHTML +=
        `<tr id='${info.id}'>
            <td>${info.codigo}</td>
            <td>${info.descripcion}</td>
            <td>${info.precio}</td>
            <td>${cant}</td>
            <td>${data.subtotal}</td>
            <td>${data.impuesto}</td>
            <td>${data.total}</td>
            <td>
                <button onclick="retirar_articulo_table(${data.Id})" class="btn btn-danger">Quitar</button>
            </td>
        </tr>`;

    limpiar_inputs();
}

function retirar_articulo_table(id_param) {
    let row = document.getElementById(`${id_param}`);

    let index = detalles.findIndex((item) => item.Id == id_param);

    calcula_valores(2, detalles[index].total);

    detalles.splice(index, 1);

    tableContent.deleteRow(row.rowIndex);
}

function calcula_valores(option, value) {

    switch (option) {
        case 1:
            subtotal_global += value;
            total_global += value;
            break;
        case 2:
            subtotal_global = subtotal_global - value;
            total_global = total_global - value;
            break;
    }

    let imp = subtotal_global * impuestoStatic;
    total_global = imp + subtotal_global;

    $("#Subtotal").val(subtotal_global);
    $('#Impuesto').val(impuestoStatic);
    $("#Total").val(total_global);
}

function limpiar_inputs() {
    articuloSelected = null;
    showFields("", "", "");
    $("#Cantidad").val("");
    $("#search_text").val("");
}

function submit_factura() {
    if (detalles.length == 0) {
        Swal.fire({
            icon: 'error',
            title: 'Agregar detalles de articulos',
            text: 'No ha agregado ningun articulo!',
        });
    }

    let facturaDto = {
        FechaRegistro: $("#FechaRegistro").val(),
        Subtotal: subtotal_global,
        Total: total_global,
        Impuesto: impuesto_global
    }

    $.ajax({
        type: "POST",
        url: "/Factura/Create",
        dataType: 'json',
        data: facturaDto,
        success: function (response) {
            save_details(response.data);
        }
    })
}

function save_details(idFactura) {
    detalles.forEach((item, index) => {
        let detail = {
            IdArticulo: item.Id,
            precio: item.precio,
            cantidad: item.cantidad,
            subtotal: item.subtotal,
            impuesto: item.impuesto,
            total: item.total,
            NumeroFact: idFactura
        };

        $.ajax({
            type: "POST",
            url: "/Factura/CreateDetails",
            dataType: 'json',
            data: detail,

            success: function (response) {
                console.log(response);
            }
        })
    });
}