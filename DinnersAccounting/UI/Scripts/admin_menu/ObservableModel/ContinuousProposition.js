//-----------------------CONTINUOUS_PROPOSITION--------------------------------------------
var ContinuousProposition = function (continuousProposition) {
    var self = this;
    self.Id = ko.observable(continuousProposition.Id);
    self.DateStart = ko.observable(continuousProposition.StartDate);
    self.DateEnd = ko.observable(continuousProposition.EndDate);
    self.DayPropositions = ko.observableArray();
    $.each(continuousProposition.DayPropositions, function (key, dayProposition) {
        var observableDayProposition = new DayProposition(dayProposition);
        self.DayPropositions.push(observableDayProposition);
    });
    self.Products = ko.observableArray();
    $.each(continuousProposition.Products, function (key, product) {
        var observableProduct = new Product(product);
        self.Products.push(observableProduct);
    });

    return {
        Id: self.Id,
        DateStart: self.DateStart,
        DateEnd: self.DateEnd,
        DayPropositions: self.DayPropositions,
        Products: self.Products
    };
};

function getEmptyObservableContinuousProposition() {
    return new ContinuousProposition({ Id: 0, DateStart: null, DateEnd: null, DayPropositions: [], Products: [] });
}
//-----------------------------------------------------------------------------------------