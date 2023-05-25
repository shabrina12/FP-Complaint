
var token = sessionStorage.getItem("JWToken");
var headers = {};
if (token) {
    headers.Authorization = 'Bearer ' + token;
}

$(document).ready(function () {
    let table = new $('#tableEmployee').DataTable({
        ajax: {
            url: "https://localhost:7127/api/employee/master",
            dataType: "json",
            headers: headers,
            dataSrc: "message"
        },
        columns: [
            {
                data: "",
                render: (data, type, row, meta) => {
                    return meta.row + 1;
                }
            },
            { data: "profileId" },
            { data: "fullName" },
            { data: "gender" },
            {
                data: "hiringDate",
                render: (data) => {
                    return formatDate(1, data)
                }
            }
        ],
        aLengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, "All"]],
        iDisplayLength: 5,
        dom: 'lBfrtip',
        buttons: [
            'copyHtml5', 'excelHtml5', 'csvHtml5', 'pdfHtml5'
        ]
    });
});