function getPerson(id) {
    var person = { };
    var url = '/api/People/GetBalance/';
    $.ajax(url, {
        async: false,
        data: { personId: id },
        success: function (data, textStatus, jqXHR) {
            data = JSON.parse(data);
            person.Id = data.Id;
            person.DomainName = data.DomainName;
            person.Balance = data.Balance;
        }
    });
    return person;
}
