
var token = sessionStorage.getItem("JWToken");
var headers = {};
if (token) {
    headers.Authorization = 'Bearer ' + token;
}

$(document).ready(function () {
    let table = new $('#tableResolution').DataTable({
        ajax: {
            url: "https://localhost:7127/api/resolution",
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
            { data: "employeeId" },
            { data: "complaintId" },
            { data: "description" },
            {
                data: "status",
                render: (data) => {
                    switch (data) {
                        case 0:
                            return `<div class="btn btn-danger">Rejected</div>`
                            break;
                        case 1:
                            return `<div class="btn btn-success">Accepted</div>`
                            break;
                        default:
                            return `<div class="btn btn-warning">Draft</div>`
                            break;
                    }
                }
            },
            {
                data: "dateCreated",
                render: (data) => {
                    return formatDate(2, data)
                }
            },
            {
                data: "dateUpdated",
                render: (data) => {
                    return formatDate(2, data)
                }
            },
            {
                data: "",
                render: (data, type, row) => {
                    return `<button class="btn btn-success" onclick="EditData(${row.id})" data-bs-toggle="modal">Edit</button>
                    <button class="btn btn-danger" onclick="Delete(${row.id})" data-bs-toggle="modal">Delete</button>`
                }
            },
        ],
        aLengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, "All"]],
        iDisplayLength: 5,
        dom: 'lBfrtip',
        buttons: [
            'copyHtml5', 'excelHtml5', 'csvHtml5', 'pdfHtml5'
        ]
    });
});

// Edit record by id
function EditData(id) {

    $("#EditResolutionModal").modal('show');

    var url = "https://localhost:7127/api/resolution/" + id;

    $.ajax({
        type: "GET",
        url: url,
        headers: headers,
        success: function (data) {
            var obj = data.data;
            console.log(data.data);
            $("#employeeId").val(obj.employeeId);
            $("#complaintId").val(obj.complaintId);
            $("#editDesc").val(obj.description);
            $("#editStatus").val(obj.status);

            $("#SaveResolution").click(function () {
                var newDesc = $("#editDesc").val();
                var newStatus = $("#editStatus").val();
     
                $.ajax({
                    type: "PUT",
                    url: url,
                    data: JSON.stringify({
                        id: id,
                        employeeId: obj.employeeId,
                        complaintId: obj.complaintId,
                        description: newDesc,
                        status: newStatus,
                        dateCreated: obj.dateCreated, 
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
                        $("#EditResolutionModal").modal("hide");
                        $('#tableResolution').DataTable().ajax.reload();
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

// Delete record by id
function Delete(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: "https://localhost:7127/api/resolution/" + id,
                headers: headers,
            }).done((result) => {
                Swal.fire(
                    'Deleted!',
                    'Your data has been deleted.',
                    'success'
                );
                $('#tableResolution').DataTable().ajax.reload();
            });
        }
    });
}