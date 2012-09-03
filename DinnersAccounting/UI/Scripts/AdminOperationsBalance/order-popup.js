function PopupOrder(orderId, viewModel) {
    var order = getOrderById(orderId);
    if (typeof viewModel != 'undefined') {
        viewModel.Order.Id(order.Id);
        viewModel.Order.OrderDetails(order.OrderDetail);
        viewModel.Order.Date(order.Date);
        viewModel.Order.Statuses(order.Statuses);
        viewModel.Order.Person = order.Person;
        viewModel.Order.Editable(false);
        show_order(viewModel);
    }
    
}

function show_order(viewModel) {
    $(viewModel.dialog).dialog('open');
}
