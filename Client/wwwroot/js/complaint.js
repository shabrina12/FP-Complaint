
var token = sessionStorage.getItem("JWToken");
var headers = {};
if (token) {
    headers.Authorization = 'Bearer ' + token;
}

$(document).ready(function () {
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
            {
                data: "",
                render: (data, type, row) => {
                    return `<button class="btn btn-success" onclick="EditData(${row.id})" data-bs-toggle="modal">Edit</button>`
                }
            },
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

//Show The Popup Modal For Edit University Record
function EditData(id) {

    $("#EditComplaintModal").modal('show');

    var url = "https://localhost:7127/api/complaint/" + id;

    $.ajax({
        type: "GET",
        url: url,
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (data) {
            var obj = data.data;
            console.log(data.data);
            $("#editTitle").val(obj.title);
            $("#editDesc").val(obj.description);
            $("#editOrderId").val(obj.orderId);

            $("#SaveComplaint").click(function () {
                var newTitle = $("#editTitle").val();
                var newDesc = $("#editDesc").val();
                var newOrderId = $("#editOrderId").val();
     
                $.ajax({
                    type: "PUT",
                    url: url,
                    data: JSON.stringify({
                        id: id,
                        title: newTitle,
                        description: newDesc,
                        orderId: newOrderId,
                        status: data.data.status,
                        dateCreated: data.data.dateCreated, 
                        dateUpdated: new Date()
                    }),
                    datatype: "json",
                    headers: {
                        'Authorization': 'Bearer ' + token,
                        'Content-Type': 'application/json'
                    },
                    success: function (result) {
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'Successfully update data',
                            showConfirmButton: false,
                            timer: 1500
                        });
                        $("#EditComplaintModal").modal("hide");
                        setInterval('location.reload()', 1500);
                    },
                    error: function (er) {
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Failed to update data!'
                        });
                    }
                });
            })
        }
    })
}