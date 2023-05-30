$(document).ready(function () {
    if (isAdmin) {
        getEntityCount("user")
        getEntityCount("employee")
    }
    if (isAdmin || isStaff) {
        getEntityCount("resolution")
        getEntityCount("feedback")
    }
    if (isAdmin || isUser) {
        getEntityCount("order")
        getEntityCount("complaint")
    }
})

function getEntityCount(entity) {
    const baseUrl = "https://localhost:7127/api/";
    $.ajax({
        url: baseUrl + entity,
        headers: {
            Authorization: "Bearer " + sessionStorage.getItem("JWToken")
        }
    }).done((result) => {
        let count = result.data.length
        var stepTime = Math.abs(Math.floor(500 / count));
        let current = 0;
        
        var timer = setInterval(function () {
            current += 1;
            $("#" + entity + "Count").text(current)
            if (current == count) {
                clearInterval(timer);
            }
        }, stepTime);
    })
}