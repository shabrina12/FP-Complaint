
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
            { data: "hiringDate" }
        ],
        aLengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, "All"]],
        iDisplayLength: 5,
        dom: 'lBfrtip',
        buttons: [
            {
                extend: 'copyHtml5', className: 'custom-btn'
            },
            {
                extend: 'excelHtml5',
                className: 'custom-btn',
                text: '<i class="fas fa-file-excel"></i>',
                titleAttr: 'Excel'
            },
            {
                extend: 'csvHtml5',
                className: 'custom-btn',
                text: '<i class="fa-solid fa-file-csv"></i>',
                titleAttr: 'CSV'
            },
            {
                extend: 'pdfHtml5',
                className: 'custom-btn',
                text: '<i class="fa-solid fa-file-pdf"></i>',
                titleAttr: 'PDF'
            }
        ]
    });
    setInterval(function () {
        table.ajax.reload();
    }, 30000);
});