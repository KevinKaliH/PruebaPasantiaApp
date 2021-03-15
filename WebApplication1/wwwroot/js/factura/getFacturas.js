

var tableContent = document.querySelector('#table-facturas');
var tbody = $("#table-content")[0];

$(document).ready(function () {
    cargar_tabla();
});

function cargar_tabla() {
    tbody.innerHTML = "";

    $('.lds-roller').removeClass('d-none');


    $.get("Factura/GetAllFacturas", function (response) {
        response.data.map(function (item) {

            tbody.innerHTML +=
                `<tr>
                   <td>
                        ${item.numeroFact}
                    </td>
                    <td>
                        ${item.fechaRegistro}
                    </td>
                    <td>
                        ${item.subtotal}
                    </td>
                    <td>
                        ${item.impuesto}
                    </td>
                    <td>
                        ${item.total}
                    </td>
                </tr>`;

                $('.lds-roller').addClass('d-none');
            })
        });
}