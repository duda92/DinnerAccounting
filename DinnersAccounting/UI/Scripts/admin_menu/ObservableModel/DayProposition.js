//-----------------------DAY_PROPOSITION---------------------------------------------------
function DayProposition(dayProposition) {
    var self = this;
    self.Id = ko.observable(dayProposition.Id);
    self.Date = ko.observable(dayProposition.Date);

    self.Products = ko.observableArray();
    $.each(dayProposition.Products, function (key, product) {
        var observableProduct = new Product(product);
        self.Products.push(observableProduct);
    });

    
    self.AddProduct = function(product) {
        self.Products.push(product);
    };
    self.RemoveProduct = function (product) {
        self.Products.remove(function (item) { return item.Id == product.Id(); });
    };

    return {
        Id: self.Id,
        Date: self.Date,
        Products: self.Products,
        display_date: self.display_date,
               
        AddProduct: self.AddProduct,
        RemoveProduct: self.RemoveProduct
    };
};

function getEmptyObservableDayProposition() {
    return new DayProposition({ Id: 0, Date: null, Products: [] });
}
//----------------------------------------------------------------------------------------------