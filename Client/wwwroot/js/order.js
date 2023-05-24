$(document).ready(function () {
        let t = $("#table").DataTable({
        ajax: {
            url: "https://localhost:7127/api/order",
            headers: {
                Authorization: "Bearer " + sessionStorage.getItem("JWToken")
            },
        },
        columns: [
            {
                defaultContent: '',
                searchable: false,
                orderable: false,
            },
            {
                data: 'id',
                searchable: false
            },
            {
                data: 'orderDate',
                searchable: false,
                type: 'date',
                render: function (data) {
                    return formatDate(2, data)
                }
            },
            {
                data: 'id',
                orderable: false,
                searchable: false,
                render: function (data) {
                    return `
                        <button class="btn btn-sm btn-warning complaint-btn"><i class="fa fa-file-text" aria-hidden="true"></i> Complaint</button>
                        <button onclick="deleteOrder(${data})" class="btn btn-sm btn-danger"><i class="fa fa-trash"></i> Delete</button>
                    `
                }
            }
        ],
        order: [[1, 'asc']],
    });

    t.on('order.dt search.dt', function () {
        let i = 1;

        t.cells(null, 0, { search: 'applied', order: 'applied' }).every(function (cell) {
            this.data(i++);
        });
    }).draw();
});

function addOrder() {
    let data = {}
    $.ajax({
        url: "https://localhost:7127/api/order/",
        method: "POST",
        headers: {
            Authorization: "Bearer " + sessionStorage.getItem("JWToken")
        },
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data)
    }).done((res) => {
        $("#table").DataTable().ajax.reload()
    }).fail((e) => {
        console.log(e.status + ' ' + e.statusText)
    })
}

function deleteOrder(id) {
    $.ajax({
        url: "https://localhost:7127/api/order/"+id,
        method: "DELETE",
        headers: {
            Authorization: "Bearer " + sessionStorage.getItem("JWToken")
        }
    }).done((res) => {
        $("#table").DataTable().ajax.reload()
    }).fail((e) => {
        console.log(e.status + ' ' + e.statusText)
    })
}