function SaveStatus(date, statusValue) {

    $.ajax('/api/Orders/SetDayStatus', {
        async: false,
        data: { date: date, statusValue: statusValue }
    });
}

function GetStatus(date) {
    var obj = {};
    $.ajax('/api/Orders/GetDayStatus', {
        async: false,
        data: { date: date },
        success: function (data) {
            data = JSON.parse(data);
            obj.AvaliableStatuses = data.AvaliableStatuses;
            obj.Status = data.Status;
        }
    });
    return obj;
}