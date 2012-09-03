//------------------------ORDER_STATUS-----------------------------------------------------
var OrderStatus = function (id, statusValue, description, date, isCurrent) {
    var self = this;
    self.Id = id;
    self.StatusValue = statusValue;
    self.Description = description;
    self.Date = date;
    self.isCurrent = isCurrent;

    return {
        Id: self.Id,
        StatusValue: self.StatusValue,
        Description: self.Description,
        Date: self.Date,
        isCurrent: self.isCurrent
    };
};
//-----------------------------------------------------------------------------------------