function Person(id, domainName, balance) {
    var self = this;

    self.Id = id;
    self.DomainName = domainName;
    self.Balance = ko.observable(balance); 

    return {
        Id : self.Id,
        DomainName : self.DomainName,
        Balance : self.Balance
    };
}

