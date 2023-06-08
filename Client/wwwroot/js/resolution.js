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
                width: "2%",
                render: (data, type, row, meta) => {
                    return meta.row + 1;
                }
            },
            { data: "complaintId" },
            /*{ data: "description" },*/
            {
                data: "status",
                render: (data, type, row) => {
                    switch (data) {
                        case 0:
                            return `<div class="badge bg-danger">Rejected</div>`
                            break;
                        case 1:
                            return `<div class="badge bg-success">Accepted</div>`
                            break;
                        default:
                            if (row.complaint.status == 2) {
                                return `<div class="badge bg-info">Pending</div>`
                            }
                            return `<div class="badge bg-warning">Draft</div>`
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
                orderable: false,
                render: (data, type, row) => {
                    return `<button class="btn btn-success btn-fill" onclick="EditData(${row.id})">
                                <i class="fa fa-pencil-square-o" aria-hidden="true"></i> Edit
                            </button>
                            <button class="btn btn-danger btn-fill" onclick="Delete(${row.id})">
                                <i class="fa fa-trash" aria-hidden="true"></i> Delete
                            </button>`
                }
            },
            //{
            //    data: "",
            //    render: (data, type, row) => {
            //        return `<button class="btn btn-primary btn-fill" onclick="addFeedback(${row.id})">
            //                    Feedback
            //                </button>`                
            //    },
            //    width: "15%"
            //},
        ],
        aLengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, "All"]],
        iDisplayLength: 5,
        dom: 'lBfrtip',
        buttons: [
            'copyHtml5', 'excelHtml5', 'csvHtml5', 'pdfHtml5'
        ]
    });
});

function addFeedback(id) {
    $("#AddFeedbackModal").modal("show");
    $("#resolutionId").val(id);
}

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
            $("#employeeId").val(obj.employeeId);
            $("#complaintId").val(obj.complaintId);
            $("#editDesc").val(obj.description);
            $("#editStatus").val(obj.status);
            $("#resolutionCreated").val(obj.dateCreated);
            $("#resolutionId").val(obj.id)
            $("#complaintTitle").val(obj.complaint.title)
            $("#complaintDesc").val(obj.complaint.description)
            if (obj.status != null) {
                $("#editDesc").prop("disabled", true)
            } else {
                $("#editDesc").prop("disabled", false)
            }
            if (obj.complaint.status == 2) {
                $("#completedResolution").prop("disabled", true)
                $("#completedResolution").prop("checked", true)
            } else {
                $("#completedResolution").prop("disabled", false)
                $("#completedResolution").prop("checked", false)
            }
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

$("#SaveResolution").click(function () {
    let completed = $("#completedResolution").is(":checked") && !$("#completedResolution").is(":disabled")
    var newDesc = $("#editDesc").val();
    var newStatus = $("#editStatus").val();
    if (newStatus == "") {
        newStatus = null
    }
    let id = $("#resolutionId").val();

    if (completed) {
        $.ajax({
            url: "https://localhost:7127/api/complaint/changestatus",
            method: "POST",
            headers: headers,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                entityId: $("#complaintId").val(),
                status: 2
            })
        }).fail((e) => {
            console.log(e)
        })
    }

    $.ajax({
        type: "PUT",
        url: "https://localhost:7127/api/resolution/" + id,
        data: JSON.stringify({
            id: id,
            employeeId: $("#employeeId").val(),
            complaintId: $("#complaintId").val(),
            description: newDesc,
            status: newStatus,
            dateCreated: $("#resolutionCreated").val(),
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