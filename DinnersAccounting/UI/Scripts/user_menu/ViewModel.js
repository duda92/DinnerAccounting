//-----------------------VIEW_MODEL-------------------------------------------------------------
var viewModel = {
    ContinuousPropositions: getContinuousPropositions(),
    ChosenContinuousProposition: ko.observable(),
    ChosenDate: ko.observable(),

    Order: getEmptyObservableOrder(),

    HasChanges: ko.observable(false),
    OrderReadOnly: false,
    AddProductToOrder: function (product) {
        viewModel.Order.addToOrder(product);
        viewModel.HasChanges(true);
    },

    SaveOrder: function () {
        var order = viewModel.Order.getObject();
        putOrder(order);
        viewModel.UpdateOrder(order.Date);
        viewModel.HasChanges(false);
    },

    CancelEditOrder: function () {
        viewModel.UpdateOrder(viewModel.Order.Date());
        viewModel.HasChanges(false);
    },

    RemoveFromOrder: function (detail) {
        viewModel.Order.removeDetail(detail);
        viewModel.HasChanges(true);
    },

    UpdateOrder: function (newDate) {
        var order = getOrderByDateForCurrentPerson(newDate);
        this.Order.Id(order.Id);
        this.Order.OrderDetails(order.OrderDetails);
        this.Order.Date(order.Date);
        this.Order.Statuses(order.Statuses);
        this.Order.Person = order.Person;
    },

    ChosenDateText: ko.observable()
};

viewModel.ChosenDate.subscribe(function (newDate) {
    if (newDate != '')
        viewModel.UpdateOrder(newDate);
    viewModel.ChosenDateText(newDate.split('T')[0]);
});

viewModel.ChosenContinuousProposition.subscribe(function (newProposition) {
    viewModel.ChosenDate('');
});

//----------------------------------------------------------------------------------------------
