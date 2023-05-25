
var token = sessionStorage.getItem("JWToken");
var headers = {};
if (token) {
    headers.Authorization = 'Bearer ' + token;
}

$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "https://localhost:7127/api/auth/me",
        datatype: "json",
        headers: headers,
        success: function (data) {
            let tes = formatDate(1, data.data.profile.birthDate);
            console.log(tes);
            let fullName = "";
            let email = "";
            let birthDate = "";
            let phoneNumber = "";

            fullName = `${data.data.profile.firstName} ${data.data.profile.lastName} `
            $("#fullName").html(fullName);
            email = `${data.data.email}`
            $("#email").html(email);

            if (data.data.profile.gender === 0) {
                $("#gender").html("Male");
            } else if (data.data.profile.gender === 1) {
                $("#gender").html("Female");
            }

            birthDate = formatDate(1, `${data.data.profile.birthDate}`)
            $("#birthDate").html(birthDate);
            phoneNumber = `${data.data.profile.phoneNumber}`
            $("#phoneNumber").html(phoneNumber);
        }
    });
});