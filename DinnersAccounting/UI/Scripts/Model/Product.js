//------------------------PRODUCT---------------------------------------------------
var Product = function (product) {
    var self = this;
    self.Id = product.Id;
    self.Title = product.Title;
    self.Summary = product.Summary;
    self.Price = product.Price;
    self.isComplex = product.isComplex;

    self.add_to_order = function (param) {
        alert('add to order ' + param);
    };

    return {
        Id: self.Id,
        Title: self.Title,
        Summary: self.Summary,
        Price: self.Price,
        isComplex: self.isComplex,
        add_to_order: self.add_to_order
    };
};
//-----------------------------------------------------------------------------------------