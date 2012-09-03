//------------------------ORDER_DETAILS---------------------------------------------------
var OrderDetail = function (id, product, quantity) {
    var self = this;
    self.Id = id;
    self.Product = product;
    self.Quantity = ko.observable(quantity);

    return {
        Id: self.Id,
        Product: self.Product,
        Quantity: self.Quantity
    };
};
//-----------------------------------------------------------------------------------------