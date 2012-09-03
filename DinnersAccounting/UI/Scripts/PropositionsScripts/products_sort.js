//----------------------------------------------------------------------------------------------
//              sort_products
//----------------------------------------------------------------------------------------------
//this fuction returns sorted list of products (complexes go first and then go not complexes)
function sort_products(products_list) {
    var sorted_day_proposition_products = [];

    $.each(products_list, function (key_dp_pr, Product) {
        if (Product.isComplex == true)
            sorted_day_proposition_products.push(Product);
    });

    $.each(products_list, function (key_dp_pr, Product) {
        if (Product.isComplex == false)
            sorted_day_proposition_products.push(Product);
    });

    return sorted_day_proposition_products;
}
