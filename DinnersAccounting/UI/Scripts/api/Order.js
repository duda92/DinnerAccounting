//-----------------------------------------------------------------------------------------
function getOrderByDateForCurrentPerson(date) {
    
    if (typeof date == 'undefined')
        return getEmptyOrder();
    
    var order = {};
    $.ajax('/api/Orders/GetByDateForCurrentUser', {
        async: false,
        data: { date: date },
        success: function (data) {
            data = JSON.parse(data);
            if (data == null) {
                order = getEmptyOrder(date);
                return;
            }
            order.Id = data.Id;
            order.OrderDetails = new Array();
            $.each(data.OrderDetail, function (key, val) {
                var detail = new OrderDetail(val.Id, val.Product, val.Quantity);
                order.OrderDetails.push(detail);
            });
            order.Statuses = new Array();
            $.each(data.Statuses, function (key, val) {
                var status = new OrderStatus(val.Id, val.StatusValue, val.Description, val.Date, val.isCurrent);
                order.Statuses.push(status);
            });

            order.Person = null;
            order.Date = data.Date;
        }
    });
    return order;
}

//-----------------------------------------------------------------------------------------
function getOrderByDateAndPerson(date, person) {

    if (typeof date == 'undefined' || typeof person == 'undefined')
        return getEmptyOrder();

    var newOrder = {};
    $.ajax('/api/Orders/GetByDateAndPerson', {
        async: false,
        data: { date: date, personId: person.Id },
        success: function (order) {
            order = JSON.parse(order);
            if (order == null) {
                newOrder = getEmptyOrder(date);
                return;
            }

            newOrder.Id = order.Id;
            newOrder.OrderDetails = new Array();
            $.each(order.OrderDetail, function (key, val) {
                var detail = new OrderDetail(val.Id, val.Product, val.Quantity);
                newOrder.OrderDetails.push(detail);
            });

            newOrder.Statuses = new Array();
            $.each(order.Statuses, function (key, val) {
                var status = new OrderStatus(val.Id, val.StatusValue, val.Description, val.Date, val.isCurrent);
                newOrder.Statuses.push(status);
            });

            newOrder.Person = order.Person;
            newOrder.Date = order.Date;
        }
    });
    return newOrder;
}
//-----------------------------------------------------------------------------------------
function putOrder(order) {
    $.ajax('/api/Orders/Create', {
        type: 'post',
        async: false,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(order),
        success: function () {
            return order;
        }
    });
}
//-----------------------------------------------------------------------------------------

function getOrderById(orderId) {
    var order = { };
    $.ajax('/api/Orders/Get', {
        async: false,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: { orderId: orderId },
        success: function (_order, textStatus, jqXHR) {
            order = JSON.parse(_order);
        }
    });
    return order;
}