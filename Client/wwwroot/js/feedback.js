
var token = sessionStorage.getItem("JWToken");
var headers = {};
if (token) {
    headers.Authorization = 'Bearer ' + token;
}

$(document).ready(function () {
    let table = new $('#tableFeedback').DataTable({
        ajax: {
            url: "https://localhost:7127/api/feedback",
            dataType: "json",
            headers: headers,
            dataSrc: "data"
        },
        columns: [
            {
                data: "",
                width: "1%",
                render: (data, type, row, meta) => {
                    return meta.row + 1;
                }
            },
            { data: "resolutionId" },
            {
                data: "description",
                width: "25%"
            },
            { data: "rating" },
            //{
            //    data: "",
            //    render: (data, type, row) => {
            //        //`<button class="btn btn-success btn-fill" onclick="EditData(${row.id})" data-bs-toggle="modal">Edit</button>
            //        return `
            //        <button class="btn btn-danger btn-fill" onclick="Delete(${row.id})" data-bs-toggle="modal">Delete</button>`
            //    }
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

// Edit record by id
function EditData(id) {

    $("#EditFeedbackModal").modal('show');

    var url = "https://localhost:7127/api/feedback/" + id;

    $.ajax({
        type: "GET",
        url: url,
        headers: headers,
        success: function (data) {
            var obj = data.data;
            $("#resolutionId").val(obj.resolutionId);
            $("#editDesc").val(obj.description);
            $("#editRating").val(obj.rating);

            $("#SaveFeedback").click(function () {
                var newDesc = $("#editDesc").val();
                var newRating = $("#editRating").val();

                $.ajax({
                    type: "PUT",
                    url: url,
                    data: JSON.stringify({
                        id: id,
                        resolutionId: obj.resolutionId,
                        description: newDesc,
                        rating: newRating,
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
                        $("#EditFeedbackModal").modal("hide");
                        $('#tableFeedback').DataTable().ajax.reload();
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
                url: "https://localhost:7127/api/feedback/" + id,
                headers: headers,
            }).done((result) => {
                Swal.fire(
                    'Deleted!',
                    'Your data has been deleted.',
                    'success'
                );
                $('#tableFeedback').DataTable().ajax.reload();
            });
        }
    });
}