
$(document).ready(function () {

    var token = sessionStorage.getItem("JWToken");
    var headers = {};
    if (token) {
        headers.Authorization = 'Bearer ' + token;
    }

    let table = new $('#tableComplaint').DataTable({
        ajax: {
            url: "https://localhost:7127/api/complaint",
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
            { data: "title" },
            { data: "description" },
            { data: "orderId" },
            { data: "status" },
            { data: "dateCreated" },
            { data: "dateUpdated" },
            /*{
                data: "",
                render: (data, type, row) => {
                    return `<button class="btn btn-danger" onclick="Delete(${row.id})" data-bs-toggle="modal">Delete</button>
                    <button class="btn btn-success" onclick="EditData(${row.id})" data-bs-toggle="modal">Edit</button>`
                }
            },*/
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
