//-----------------------CONTINUOUS_PROPOSITION--------------------------------------------
var ContinuousProposition = function (id, dateStart, dateEnd, dayPropositions, products) {
    var self = this;
    self.Id = id;
    self.DateStart = dateStart;
    self.DateEnd = dateEnd;
    self.DayPropositions = dayPropositions;
    self.Products = products;

    return {
        Id: self.Id,
        DateStart: self.DateStart,
        DateEnd: self.DateEnd,
        DayPropositions: self.DayPropositions,
        Products: self.Products
    };
};
//-----------------------------------------------------------------------------------------