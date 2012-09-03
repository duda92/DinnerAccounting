function Day(date, status, proposition, orders) {
    var self = this;
    self.Date = date;
    self.Status = ko.observable(status);
    self.Proposition = proposition;
    self.Orders = ko.observableArray(orders);

    self.display_date = ko.computed(function() {
        return self.Date.split('T')[0];
    }, this);

    return {
        Date: self.Date,
        Status: self.Status,
        Proposition: self.Proposition,
        Orders: self.Orders,
        display_date: self.display_date
    };
}

function ObjToObservableDay(serverDay) {
    var date = serverDay.Date;
    var dayStatus = new Status(serverDay.Status.StatusValue, serverDay.Status.AvalibleStatuses);

    var orders = new Array();
    $.each(serverDay.Orders, function (key, order) {
        var newOrder = {};
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

        var observableOrder = new Order(newOrder.Id, newOrder.OrderDetails, newOrder.Date, newOrder.Statuses, new Person(newOrder.Person.Id, newOrder.Person.DomainName, newOrder.Person.Balance));
        observableOrder.Editable(false);
        orders.push(ko.observable(observableOrder));
    });

    var proposition = new DayProposition(serverDay.Proposition.Date, serverDay.Proposition.Complexes, serverDay.Proposition.Also, serverDay.Proposition.EveryDay);
    var day = new Day(date, dayStatus, proposition, orders);
    return day;
}
                