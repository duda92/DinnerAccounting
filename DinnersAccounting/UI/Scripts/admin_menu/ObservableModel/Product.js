//------------------------PRODUCT---------------------------------------------------
var Product = function (product) {
    var self = this;
    self.Id = ko.observable(product.Id);
    self.Title = ko.observable(product.Title);
    self.Summary = ko.observable(product.Summary);
    self.Price = ko.observable(product.Price);
    self.isComplex = ko.observable(product.isComplex);

    return {
        Id: self.Id,
        Title: self.Title,
        Summary: self.Summary,
        Price: self.Price,
        isComplex: self.isComplex
    };
};

function getEmptyObservableProduct() {
    return new Product({ Id: 0, Title: '', Summary: '', Price: 0, isComplex: false });
}

function Update(source, destination) {
    destination.Id(source.Id());
    destination.Title(source.Title());
    destination.Summary(source.Summary());
    destination.Price(source.Price());
    destination.isComplex(source.isComplex());
}

function getObservableClone(source) {
    return new Product({ Id: source.Id(), Title: source.Title(), Summary: source.Summary(), Price: source.Price(), isComplex: source.isComplex() });
}


//-----------------------------------------------------------------------------------------