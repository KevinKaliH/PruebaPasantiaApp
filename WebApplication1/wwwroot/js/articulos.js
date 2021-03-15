
var tableContent = document.querySelector('#table-articles');
var tbody = $("#table-content")[0];

$(document).ready(function () {
    cargar_tabla(true);
});

function cargar_tabla(isActive) {
    tbody.innerHTML = "";
    
    $('.lds-roller').removeClass('d-none');

    $.ajax({
        type: "GET",
        url: "/Articulo/GetAllArticles",
        dataType: 'json',
        data: ({ 'isActive': "" + isActive }),

        success: function (response) {
            response.data.map(function (item, index) {

                let accionMessage = item.activo ? "Retirar" : "Activar";

                tbody.innerHTML +=
                    `<tr id="${item.id}">
                    <td>
                        ${item.codigo}
                    </td>
                    <td>
                        ${item.descripcion}
                    </td>
                    <td>
                        ${item.precio}
                    </td>
                    <td>
                        ${item.costo}
                    </td>
                    <td>
                        <a href="/Articulo/Edit/${item.id}" class="btn btn-secondary">Editar</a>
                        <button onclick="retirar_articulo(${item.id})" class="btn btn-danger">${accionMessage}</button>
                    </td>
                </tr>`;
            });

            $('.lds-roller').addClass('d-none');
        }
    })
}

$("#selected_state_article").change(function () {
    let estado = $(this).children("option:selected").val();
    let isActive = estado == 1;

    cargar_tabla(isActive);
});

function retirar_articulo(id_param) {
    peticion_cambiaEstado_articulo(id_param, "Desea Retirar el articulo ?");
}

function peticion_cambiaEstado_articulo(id_param, message) {
    Swal.fire({
        title: message,
        showCancelButton: true,
        confirmButtonText: `Aceptar`,
        cancelButtonText: 'Cancelar'
    }).then((result) => {

        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: "/Articulo/Delete",
                dataType: 'json',
                data: ({ 'Id': "" + id_param }),

                success: function (response) {
                    if (response.success) {
                        remove_row(id_param);
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Solicitud fallidar!',
                        })
                    }
                }
            })
        }
    })
}

function remove_row(id) {
    let row = document.getElementById(`${id}`);

    tableContent.deleteRow(row.rowIndex);
}