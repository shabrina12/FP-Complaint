
var token = sessionStorage.getItem("JWToken");
var headers = {};
if (token) {
    headers.Authorization = 'Bearer ' + token;
}

$(document).ready(function () {
    let table = new $('#tableUser').DataTable({
        ajax: {
            url: "https://localhost:7127/api/user",
            dataType: "json",
            headers: headers,
            dataSrc: "data"
        },
        columns: [
            {
                data: "",
                render: (data, type, row, meta) => {
                    return meta.row + 1;
                }
            },
            { data: "name" },
            { data: "email" },
            { data: "gender" },
            {
                data: "birthDate",
                render: (data) => {
                    return formatDate(1, data)
                }
            },
            { data: "phoneNumber" },
            { data: "role.name" },
        ],
        aLengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, "All"]],
        iDisplayLength: 10,
        dom: 'lBfrtip',
        buttons: [
            'copyHtml5', 'excelHtml5', 'csvHtml5', 'pdfHtml5'
        ]
    });
    setInterval(function () {
        table.ajax.reload();
    }, 30000);
});