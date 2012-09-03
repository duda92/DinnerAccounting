//-----------------------------------------------------------------------------------------
//------------------------ORDER------------------------------------------------------------
var Order = function (id, orderDetails, date, statuses, person) {
    var self = this;
    self.Id = ko.observable(id);
    self.OrderDetails = ko.observableArray(orderDetails);
    self.Date = ko.observable(date);
    self.Statuses = ko.observableArray(statuses);
    self.Person = person,
    self.Editable = ko.observable(true);

    self.removeDetail = function (detail) {
        self.OrderDetails.remove(function (item) { return item.Product.Id == detail.Product.Id; });
    };

    self.addToOrder = function (product) {
        var hasProduct = false;
        var newDetail;

        ko.utils.arrayForEach(self.OrderDetails(), function (detail) {
            if (detail.Product.Id == product.Id) {
                hasProduct = true;
                newDetail = detail;
            }
        });

        if (hasProduct == true)
            newDetail.Quantity(newDetail.Quantity() + 1);
        else {
            if (self.OrderDetails().length == 0) {
                var newDetails = new Array();
                newDetails.push(new OrderDetail(0, product, 1));
                self.OrderDetails(newDetails);

                return;
            }

            newDetail = new OrderDetail(0, product, 1);
            self.OrderDetails.push(newDetail);
        }
    };


    self.getObject = function () {
        var details = new Array();
        ko.utils.arrayForEach(self.OrderDetails(), function (detail) {
            details.push({ Id: detail.Id, Product: detail.Product, Quantity: detail.Quantity() });
        });
        var order =
            {
                Id: self.Id(),
                Date: self.Date(),
                OrderDetail: details,
                Person: self.Person
            };
        return order;
    },

    self.text_date = ko.computed(function () { if (typeof self.Date() == typeof '') return self.Date().split('T')[0]; }, this);

    self.text_current_status = ko.observable(CurrentStatusValueToText(statuses));

    self.Statuses.subscribe(function (statuses) {
        self.text_current_status(CurrentStatusValueToText(statuses));
    }, this);


    return {
        Id: self.Id,
        OrderDetails: self.OrderDetails,
        Date: self.Date,
        Statuses: self.Statuses,
        Person: self.Person,
        
        removeDetail: self.removeDetail,
        addToOrder: self.addToOrder,
        getObject: self.getObject,

        text_current_status: self.text_current_status,
        text_date: self.text_date,
        Editable: self.Editable
    };
};

//-----------------------------------------------------------------------------------------
function CurrentStatusValueToText(Statuses) {
    if (typeof Statuses == 'undefined')
        return '';
    var statusValue = 0;
    $.each(Statuses, function (key, status) {
        if (status.isCurrent == true)
            statusValue = status.StatusValue;
    });
    switch (statusValue) {
        case 0:
            return "Создание";
        case 1:
            return "Редактирование";
        case 2:
            return "Отправлен";
        case 3:
            return "Доставлен";
        case 4:
            return "Не доставлен";
        case 5:
            return "Оплачено";    
        default:
            return "";
    }
};
//-----------------------------------------------------------------------------------------
function getEmptyObservableOrder() {
    var orderDetails = new Array();
    var orderStatuses = new Array();
    var order = new Order(0, orderDetails, 0, orderStatuses, null);
    return order;
}
//-----------------------------------------------------------------------------------------
function getEmptyOrder(date) {
    if (typeof date == 'undefined') {
        date = 0;
    }
    var orderDetails = new Array();
    var orderStatuses = new Array();
    var order = { Id: 0, OrderDetails: orderDetails, Date: date, Statuses: orderStatuses, Person: null };
    return order;
}
//-----------------------------------------------------------------------------------------