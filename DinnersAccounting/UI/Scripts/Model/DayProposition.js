//-----------------------DAY_PROPOSITION---------------------------------------------------
function DayProposition(id, date, products) {
    var self = this;
    self.Id = id;
    self.Date = date;
    self.Products = products;

    self.display_date = ko.computed(function () {
        return this.Date.split('T')[0];
    }, this);

    return {
        Id: self.Id,
        Date: self.Date,
        Products: self.Products,
        display_date: self.display_date
    };
};

//----------------------------------------------------------------------------------------------