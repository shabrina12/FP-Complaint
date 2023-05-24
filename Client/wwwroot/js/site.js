// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function formatDate(type, data) {
    const monthArray = [
        'January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'Desember'
    ]
    let date = new Date(data)
    let year = date.getFullYear()
    let month = date.getMonth()
    let dateNumber = date.getDate().toString().padStart(2, 0)
    let hour = date.getHours().toString().padStart(2, 0)
    let minute = date.getMinutes().toString().padStart(2, 0)
    let second = date.getSeconds().toString().padStart(2, 0)

    let fullDate = `${dateNumber} ${monthArray[month]} ${year}`
    let fullTime = `${hour}:${minute}:${second}`

    switch (type) {
        case 1:
            return fullDate
            break;
        case 2:
            return fullDate + ' ' + fullTime
            break;
        default:
            return new Date(data)
    }
}