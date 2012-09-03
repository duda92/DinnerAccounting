//----------------------------------------------------------------------------------------------
function getContinuousPropositions() {
    var continuousPropositions = new Array();
    var url = '/api/Propositions/GetAvaliableCps/';
    $.ajax(url, {
        async: false,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data, textStatus, jqXHR) {
            $.each(data, function (key, continuousProposition) {

                var continuousPropositionProducts = new Array();
                $.each(continuousProposition.Products, function (key, product) {
                    continuousPropositionProducts.push(new Product({ Id: product.Id, Title: product.Title, Price: product.Price, Summary: product.Summary, isComplex: product.isComplex }));
                });

                var dayPropositions = new Array();
                $.each(continuousProposition.DayPropositions, function (key, dayProposition) {
                    var products = new Array();
                    $.each(dayProposition.Products, function (key, product) {
                        products.push(new Product({ Id: product.Id, Title: product.Title, Price: product.Price, Summary: product.Summary, isComplex: product.isComplex }));
                    });
                    dayPropositions.push(new DayProposition(dayProposition.Id, dayProposition.Date, products));
                });

                continuousPropositions.push(new ContinuousProposition(continuousProposition.Id, continuousProposition.StartDate, continuousProposition.EndDate, dayPropositions, continuousPropositionProducts));
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
    return continuousPropositions;
};
//----------------------------------------------------------------------------------------------