﻿
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
            {
                data: "status",
                width: "12%",
                render: (data) => {
                    switch (data) {
                        case 0:
                            return `<div class="btn btn-warning btn-fill">Submitted</div>`
                            break;
                        case 1:
                            return `<div class="btn btn-info btn-fill">On Process</div>`
                            break;
                        case 2:
                            return `<div class="btn btn-success btn-fill">Completed</div>`
                            break;
                        default:
                            return "Unknown"
                            break;
                    }
                }
            },
            {
                data: "dateCreated",
                width: "12%",
                render: (data) => {
                    return formatDate(2, data)
                }
            },
            {
                data: "dateUpdated",
                width: "12%",
                render: (data) => {
                    return formatDate(2, data)
                }
            },
            {
                data: "",
                render: (data, type, row) => {
                    return `<button class="btn btn-success btn-fill" onclick="EditData(${row.id})" data-bs-toggle="modal">Edit</button>
                    <button class="btn btn-danger btn-fill" onclick="Delete(${row.id})" data-bs-toggle="modal">Delete</button>`
                }
            },
            {
                data: "",
                render: (data, type, row) => {
                    if (isAdmin == "False") {
                        return null
                    }
                    return `<button class="btn btn-primary btn-fill" onclick="addResolution(${row.id})">
                                Assign
                            </button>`
                },
                width: "15%"
            },
        ],
        aLengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, "All"]],
        iDisplayLength: 5,
        dom: 'lBfrtip',
        buttons: [
            'copyHtml5', 'excelHtml5', 'csvHtml5', 'pdfHtml5'
        ]
    });

    if (isAdmin == "True") {
        $.ajax({
            url: "https://localhost:7127/api/employee/staff",
            headers: {
                Authorization: 'Bearer ' + token,
            }
        }).done((result) => {
            let data = result.data;
            data.forEach(item => {
                $("#editEmployeeId").append(`
                <option value="${item.id}">${item.name} </option >
            `);
            })
        });
    };
});

function addResolution(id) {
    $("#AddResolutionModal").modal("show");
    $("#editComplaintId").val(id);
}

// Edit record by id
function EditData(id) {

    $("#EditComplaintModal").modal('show');

    var url = "https://localhost:7127/api/complaint/" + id;

    $.ajax({
        type: "GET",
        url: url,
        headers: headers,
        success: function (data) {
            var obj = data.data;
            $("#editTitle").val(obj.title);
            $("#editDesc").val(obj.description);
            $("#editOrderId").val(obj.orderId);

            $("#SaveComplaint").click(function () {
                var newTitle = $("#editTitle").val();
                var newDesc = $("#editDesc").val();
     
                $.ajax({
                    type: "PUT",
                    url: url,
                    data: JSON.stringify({
                        id: id,
                        title: newTitle,
                        description: newDesc,
                        orderId: obj.orderId,
                        status: obj.status,
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
                        $("#EditComplaintModal").modal("hide");
                        $('#tableComplaint').DataTable().ajax.reload();
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
                url: "https://localhost:7127/api/complaint/" + id,
                headers: headers,
            }).done((result) => {
                Swal.fire(
                    'Deleted!',
                    'Your data has been deleted.',
                    'success'
                );
                $('#tableComplaint').DataTable().ajax.reload();
            });
        }
    });
}