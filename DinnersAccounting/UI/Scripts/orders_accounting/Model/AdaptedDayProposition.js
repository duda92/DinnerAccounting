//-----------------------DAY_PROPOSITION---------------------------------------------------
function DayProposition(date, complexes, also, everyDay) {
    var self = this;
    
    self.Date = date;
    self.Complexes = complexes;
    self.Also = also;
    self.EveryDay = everyDay;

    return {
        
        Date: self.Date,
        Complexes: self.Complexes,
        Also: self.Also,
        EveryDay: self.EveryDay,
        display_date: self.display_date
    };
};

//------------