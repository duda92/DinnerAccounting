function getContinuousPropositionsDates() {
    var continuousPropositionsDates = new Array();
    var url = '/api/Propositions/GetPropositionsDates/';
    $.ajax(url, {
        async: false,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data, textStatus, jqXHR) {
            $.each(data, function (key, value) {
                continuousPropositionsDates.push({ Id: key, Dates: value });
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
    return continuousPropositionsDates;
};

function getContinuousProposition (id) {
    var continuousProposition = {};
    var url = '/api/Propositions/Get/';
    $.ajax(url, {
        async: false,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: { id: id },
        success: function (data, textStatus, jqXHR) {
            continuousProposition = data;
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
    return continuousProposition;
}

var viewModel = {

    ContinuousPropositionDatesList: ko.observableArray(getContinuousPropositionsDates()),

    ChosenContinuousPropositionId: ko.observable(),
    ChosenContinuousProposition: ko.observable(getEmptyObservableContinuousProposition()),
    ChosenDayProposition: ko.observable(getEmptyObservableDayProposition()),
    ChosenProduct: ko.observable(getEmptyObservableProduct()),

    OrderEditEnable: ko.observable(false),

    AddContinuousProposition: function () {

    },

    DeleteChosenContinuousProposition: function () {
    },

    AddProduct: function (product) {
        viewModel.ChosenProduct(getEmptyObservableProduct());
        viewModel.OrderEditEnable(true);
    },

    EditProduct: function (product) {
        var tempProduct = new Product({Id: product.Id(), Title: product.Title(), Summary: product.Summary(), Price: product.Price(), isComplex: product.isComplex() })
        viewModel.ChosenProduct(tempProduct);
        viewModel.OrderEditEnable(true);
    },

    RemoveProduct: function (product) {
    },

    SaveProduct: function () {
        var productQuery = Enumerable.From(viewModel.ChosenDayProposition().Products()).Where(function (product) {
            var itemId = product.Id();
            var editableId = viewModel.ChosenProduct().Id();
            return itemId == editableId;
        });
        if (productQuery.Count() == 0)
            viewModel.ChosenDayProposition().Products.push(viewModel.ChosenProduct());
        else {
            
            productQuery.ToArray()[0].Id(viewModel.ChosenProduct().Id());
            productQuery.ToArray()[0].Title(viewModel.ChosenProduct().Title());
            productQuery.ToArray()[0].Price(viewModel.ChosenProduct().Price());
            productQuery.ToArray()[0].Summary(viewModel.ChosenProduct().Summary());
            productQuery.ToArray()[0].isComplex(viewModel.ChosenProduct().isComplex());
        }
        viewModel.OrderEditEnable(false);
    },

    CancelProduct: function () {
        viewModel.ChosenProduct(getEmptyObservableProduct());
        viewModel.OrderEditEnable(false);
    },


    SaveContinuousProposition: function () {
    }
};