function getDaysList(dateStart, dateEnd) {

    var startDateDay = dateStart.split('/')[1];
    var startDateMonth = dateStart.split('/')[0];
    var startDateYear = dateStart.split('/')[2];

    var endDateDay = dateEnd.split('/')[1];
    var endDateMonth = dateEnd.split('/')[0];
    var endDateYear = dateEnd.split('/')[2];

    var url = '/api/Orders/GetDaysList';

    var days;
    $.ajax(url, {
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async: false,
        data: { start_date_day: startDateDay, start_date_month: startDateMonth, start_date_year: startDateYear, end_date_day: endDateDay, end_date_month: endDateMonth, end_date_year: endDateYear },
        success: function (data) {
            days = JSON.parse(data);
        }
    });

    return days;
}