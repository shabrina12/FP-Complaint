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
                width: "4%",
                render: (data, type, row, meta) => {
                    return meta.row + 1;
                }
            },
            {
                data: "title",
                width: "20%"
            },
            /*{ data: "description" },*/
            { data: "orderId" },
            {
                data: "status",
                width: "12%",
                render: (data) => {
                    switch (data) {
                        case 0:
                            return `<div class="badge bg-warning">Submitted</div>`
                            break;
                        case 1:
                            return `<div class="badge bg-info">On Process</div>`
                            break;
                        case 2:
                            return `<div class="badge bg-success">Completed</div>`
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
                orderable: false,
                width: "18%",
                render: (data, type, row) => {
                    return `<button class="btn btn-success btn-fill" onclick="EditData(${row.id})" data-bs-toggle="modal">
                                <i class="fa fa-pencil-square-o" aria-hidden="true"></i> Edit
                            </button>
                            <button class="btn btn-danger btn-fill" onclick="Delete(${row.id})" data-bs-toggle="modal">
                                <i class="fa fa-trash" aria-hidden="true"></i> Delete
                            </button>`
                }
            },
            {
                data: "",
                orderable: false,
                render: (data, type, row) => {
                    if (isAdmin == "False") {
                        let primary = row.status == 2 ? "primary" : "secondary"
                        let disabledBtn = row.status == 2 ? "" : "disabled"
                        let icon = disabledBtn == "" ?
                            `<i class="fa fa-eye" aria-hidden="true"></i>` :
                            `<i class="fa fa-eye-slash" aria-hidden="true"></i>`
                        return `<button class="btn btn-${primary} btn-fill" onclick="showResolution(${row.id})" ${disabledBtn} data-bs-toggle="modal" data-bs-target="#detailResolutionModal">
                                    ${icon} View
                                </button>`
                    }
                    return `<button class="btn btn-primary btn-fill" onclick="addResolution(${row.id})">
                                <i class="fa fa-user-plus" aria-hidden="true"></i> Assign
                            </button>`
                },
                width: "10%"
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

function showResolution(id) {
    let allResolution;
    let complaintResolution;
    $.ajax({
        url: "https://localhost:7127/api/complaint/"+id,
        headers: {
            Authorization: "Bearer " + token
        }
    }).done((result) => {
        let data = result.data
        $("#complaintTitle").val(data.title)
        $("#complaintDesc").val(data.description)
    });
    $.ajax({
        url: "https://localhost:7127/api/resolution",
        headers: {
            Authorization: "Bearer " + token
        }
    }).done((result) => {
        allResolution = result.data
        complaintResolution = allResolution.filter(r => r.complaintId == id)[0]
        $("#resolutionId").val(complaintResolution.id)
        $("#resolutionDesc").val(complaintResolution.description)
        if (complaintResolution.status != null) {
            console.log("hidden")
            $("#actionGroup").addClass("visually-hidden")
            $("#statusText").removeClass("visually-hidden")
            if (complaintResolution.status == 0) {
                $("#statusText").text("You rejected this solution")
                $("#statusText").removeClass("btn-success")
                $("#statusText").addClass("btn-danger")
            } else {
                $("#statusText").text("You accepted this solution")
                $("#statusText").removeClass("btn-danger")
                $("#statusText").addClass("btn-success")
            }
        } else {
            $("#actionGroup").removeClass("visually-hidden")
            $("#statusText").addClass("visually-hidden")
        }
    });
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


//accept resolution
$("#acceptButton").on("click", function () {
    let data = {
        entityId: $("#resolutionId").val(),
        status: 1
    }
    changeResolutionStatus(data)

})

//reject resolution
$("#rejectButton").on("click", function () {
    let data = {
        entityId: $("#resolutionId").val(),
        status: 0
    }
    changeResolutionStatus(data)
})

//change resolution status
function changeResolutionStatus(data) {
    $.ajax({
        url: "https://localhost:7127/api/resolution/changestatus",
        method: "POST",
        headers: headers,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data)
    }).done((result) => {
        $("#detailResolutionModal").modal("hide")
        Swal.fire({
            icon: 'success',
            title: 'Successfully update data',
            showConfirmButton: false,
            timer: 1000
        });
        $("#feedbackModal").modal("show")
        $("#feedbackResolutionId").val(data.entityId)
    }).fail((e) => {
        console.log(e)
        $("#detailResolutionModal").modal("hide")
        Swal.fire({
            icon: 'error',
            title: 'Failed to update data',
            showConfirmButton: false,
            timer: 1000
        });
    })
}

//submit feedback
$("#feedbackSubmit").on("click", function () {
    let resolutionId = $("#feedbackResolutionId").val()
    let feedbackDesc = $("#feedbackDesc").val()
    /*let feedbackRating = $("#feedbackRating").val()*/
    let feedbackRating = $('input[name="rating"]:checked').val();
    const data = {
        resolutionId,
        description: feedbackDesc,
        rating: feedbackRating
    }
    submitFeedback(data)
})

function submitFeedback(data) {
    $.ajax({
        url: "https://localhost:7127/api/feedback",
        method: "POST",
        headers: headers,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data)
    }).done((result) => {
        $("#feedbackModal").modal("hide")
        $("#feedbackResolutionId").val("")
        $("#feedbackDesc").val("")
        $("#feedbackRating").val("5")
        Swal.fire({
            icon: 'success',
            title: 'Thanks for your feedback',
            showConfirmButton: false,
            timer: 1000
        });
    }).fail((e) => {
        console.log(e)
        $("#feedbackModal").modal("hide")
        Swal.fire({
            icon: 'error',
            title: 'Failed to submit feedback',
            showConfirmButton: false,
            timer: 1000
        });
    })
}