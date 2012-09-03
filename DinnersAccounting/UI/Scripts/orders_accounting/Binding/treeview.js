ko.bindingHandlers.treeview = {
    init: function (element, valueAccessor) {
        var options = valueAccessor() || {};
        setTimeout(function () {
            $('.treeview').treeview();
        });
    },
    update: function (element, valueAccessor) {
        $('.treeview').treeview();
    }
};