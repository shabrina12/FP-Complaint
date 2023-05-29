
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
            let fullName = `${data.data.profile.firstName} ${data.data.profile.lastName}`
            $("#fullName").html(fullName);

            let email = `${data.data.email}`
            $("#email").html(email);

            if (data.data.profile.gender === 0) {
                $("#gender").html("Male")
                var imageshown = "/img/male.jpg"
            } else if (data.data.profile.gender === 1) {
                $("#gender").html("Female")
                var imageshown = "/img/female.jpg"
            }

            document.getElementById('profileImage').src = imageshown;

            let birthDate = formatDate(1, `${data.data.profile.birthDate}`)
            $("#birthDate").html(birthDate);

            let phoneNumber = `${data.data.profile.phoneNumber}`
            $("#phoneNumber").html(phoneNumber);
        }
    });
});