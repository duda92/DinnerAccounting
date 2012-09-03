//----------------------------------------------------------------------------------------------
//              UpdateDatesListAndDisplayingProposition
//parameters:
//          proposition_id (id of continuous proposition which must be requested for display)
//if not defined value is taken from '#date_select'
//if it is empty - do nothing but clear '#continuous_proposition'
//----------------------------------------------------------------------------------------------
function UpdateDatesListAndDisplayingProposition(proposition_id) {
    $.ajax('/api/Propositions/GetPropositionsDates', {
        success: function (dates_dictionary) {
            $('#date_select').empty();
            $.each(dates_dictionary, function (key, value) {
                var id = key;
                var val = value;
                var option = $('<option />').append(value);
                option.val(key);
                option.appendTo($('#date_select'));
            });

            if (proposition_id == null)
                proposition_id = $('#date_select').val();

            if (proposition_id != null)
                UpdateContinuousProposition(proposition_id);
            else
                ClearContinuousProposition();
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}

//----------------------------------------------------------------------------------------------
//              ClearContinuousProposition
//----------------------------------------------------------------------------------------------
//clears div with accordion items-list of day proposition
function ClearContinuousProposition() {
    $('#continuous_proposition_parent').empty();
}

//----------------------------------------------------------------------------------------------
//              UpdateContinuousProposition
//----------------------------------------------------------------------------------------------
//creates response and redraws div with accordion items-list of day proposition
// parameters:
//  id - id of proposition for request and redraw
//   if defined gets the proposition with this id
//   if not defined get current (if current not found - gets selected options from '#date_select' if it null or result not found - clear '#continuous_proposition')
function UpdateContinuousProposition(id) {
    var address = null;
    if (typeof (id) !== 'undefined' && id != null)
        address = "/api/Propositions/Get/" + id;
    else
        address = "/api/Propositions/Current";

    $.ajax(address, {
        success: function (ContinuousProposition, textStatus, jqXHR) {
            $('#continuous_proposition_parent').empty();

            $('#remove_proposition_link').css('display', 'inline');

            $('#remove_proposition_link_container').empty();
            $('<a>', {
                text: '',
                title: 'Удалить предложение',
                href: "#",
                click: function () { delete_proposition($('#date_select').val()); }
            }).append('<img id="deletepropBtn" src ="../Scripts/images/delete.png" />').appendTo($('#controlpanel'));

            $('<div id="continuous_proposition"/>').appendTo($('#continuous_proposition_parent'));

            var StartDate = ContinuousProposition.StartDate.split("T")[0];
            var EndDate = ContinuousProposition.EndDate.split("T")[0];
            var DayPropositions = ContinuousProposition.DayPropositions;

            $.each(DayPropositions, function (key_dp, DayProposition) {
                $('#continuous_proposition').append('<h3> <a href="#" id="date">' + DayProposition.Date.split("T")[0] + '</a></h3>');

                var productsHtml = $('<div/>');

                var proposition_table = $('<table />');

                var sorted_day_proposition_products = sort_products(DayProposition.Products);

                var tr_add_product = $('<tr />');

                $('<a >', {
                    text: '',
                    title: 'Добавить продукт',
                    href: '#',
                    click: function () { create_product(DayProposition.Id); return false; }
                }).append('<img src ="../Scripts/images/add.png" style="width:20px;"/>').appendTo(tr_add_product);

                tr_add_product.appendTo(proposition_table);

                $.each(sorted_day_proposition_products, function (key_dp_pr, Product) {
                    if (typeof (Product.Summary) == 'undefined')
                        Product.Summary = '';

                    var tr = $('<tr />').append('<td><b>' + Product.Title + '</b><br/>' + Product.Summary + '</td><td>' + Product.Price + '</td>');

                    var edit_td = $('<td />');
                    var delete_td = $('<td />');

                    $('<a>', {
                        text: '',
                        title: 'Редактировать',
                        href: '#',
                        click: function () { edit_product(Product.Id); return false; }
                    }).append('<img src ="../Scripts/images/edit.png" style="width:20px;"/>').appendTo(edit_td);

                    $('<a>', {
                        text: '',
                        title: 'Удалить',
                        href: '#',
                        click: function () { delete_product(Product.Id); return false; }
                    }).append('<img src ="../Scripts/images/delete.png" style="width:20px;"/>').appendTo(delete_td);

                    edit_td.appendTo(tr);
                    delete_td.appendTo(tr);

                    tr.appendTo(proposition_table);
                });

                proposition_table.appendTo(productsHtml);

                $('#continuous_proposition').append(productsHtml);
            });
            //-----------------------------------

            $('#continuous_proposition').append('<h3> <a href="#">Также ежедневно в меню</a></h3>');

            var productsHtml = $('<div/>');

            var proposition_table = $('<table />');

            var sorted_every_day_proposition_products = sort_products(ContinuousProposition.Products);

            var tr_add_product = $('<tr />');

            $('<a>', {
                text: '',
                title: 'Добавить продукт',
                href: '#',
                click: function () { create_product(ContinuousProposition.Id); return false; }
            }).append('<img src ="../Scripts/images/add.png" style="width:20px;"/>').appendTo(tr_add_product);

            tr_add_product.appendTo(proposition_table);

            $.each(sorted_every_day_proposition_products, function (key_dp_pr, Product) {
                if (typeof (Product.Summary) == 'undefined')
                    Product.Summary = '';
                var tr = $('<tr />').append('<td><b>' + Product.Title + '</b><br/>' + Product.Summary + '</td><td>' + Product.Price + '</td>');

                var edit_td = $('<td />');
                var delete_td = $('<td />');

                $('<a>', {
                    text: '',
                    title: 'Редактировать',
                    href: '#',
                    click: function () { edit_product(Product.Id); return false; }
                }).append('<img src ="../Scripts/images/edit.png" style="width:20px;"/>').appendTo(edit_td);

                $('<a>', {
                    text: '',
                    title: 'Удалить',
                    href: '#',
                    click: function () { delete_product(Product.Id); return false; }
                }).append('<img src ="../Scripts/images/delete.png" style="width:20px;"/>').appendTo(delete_td);

                edit_td.appendTo(tr);
                delete_td.appendTo(tr);

                tr.appendTo(proposition_table);
            });
            proposition_table.appendTo(productsHtml);

            $('#continuous_proposition').append(productsHtml);

            //-----------------------------------
            $('#continuous_proposition').addClass('wide-accordion');
            $('#continuous_proposition').accordion('destroy').accordion({ clearStyle: true, autoHeight: false });
            $("#continuous_proposition").accordion("option", "active", accordion_index);
        },
        error: function (jqXHR, textStatus, errorThrown) {
        } //on error
    });
}