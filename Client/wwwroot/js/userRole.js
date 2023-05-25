
var token = sessionStorage.getItem("JWToken");
var headers = {};
if (token) {
    headers.Authorization = 'Bearer ' + token;
}

$(document).ready(function () {
    let table = new $('#tableUserRole').DataTable({
        ajax: {
            url: "https://localhost:7127/api/userrole",
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
            { data: "userId" },
       /*     { data: "email" },*/
            { data: "roleId" },
            {
                data: "",
                render: (data, type, row) => {
                    return `<button class="btn btn-success" onclick="EditData(${row.id})" data-bs-toggle="modal">Edit</button>`
                }
            },
        ],
        aLengthMenu: [[5, 10, 15, 20, -1], [5, 10, 15, 20, "All"]],
        iDisplayLength: 10,
        dom: 'lBfrtip',
        buttons: [
            'copyHtml5', 'excelHtml5', 'csvHtml5', 'pdfHtml5'
        ]
    });
});

// Edit record by id
function EditData(id) {

    $("#EditUserRoleModal").modal('show');

    var url = "https://localhost:7127/api/userrole/" + id;

    $.ajax({
        type: "GET",
        url: url,
        headers: headers,
        success: function (data) {
            var obj = data.data;
            $("#UserId").val(obj.userId);
            $("#editRoleId").val(obj.roleId);

            $("#SaveUserRole").click(function () {
                var newRoleId = $("#editRoleId").val();

                $.ajax({
                    type: "PUT",
                    url: url,
                    data: JSON.stringify({
                        id: id,
                        userId: obj.userId,
                        roleId: newRoleId,
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
                        $("#EditUserRoleModal").modal("hide");
                        $('#tableUserRole').DataTable().ajax.reload();
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