function edit_product(product_id) {
    if (hasUnsavedData == true) {
        AlertHasUnsavedData();
        return false;
    }

    hide_panels();

    $("#edit_product").removeAttr("style").hide().fadeIn();

    $.getJSON('/api/Products/Get/' + product_id,
        function (product) {
            $('#edit_product_title').val(product.Title);
            $('#edit_product_summary').val(product.Summary);
            $('#edit_product_price').val(product.Price);
            $('#edit_product_is_complex').attr('checked', product.isComplex);
            $('#edit_product_id').val(product.Id);

            hide_validation_errors();

            hasUnsavedData = true;
        });
}

function edit_submit() {
    accordion_index = $("#continuous_proposition").accordion("option", "active");

    var Id = $('#edit_product_id').val();
    var Title = $('#edit_product_title').val();
    var Summary = $('#edit_product_summary').val();
    var Price = $('#edit_product_price').val().replace(',', '.');
    var isComplex = $('#edit_product_is_complex').is(':checked');

    if (validate_title('edit', Title) == false || validate_price('edit', Price) == false)
        return false;

    var Product = { Id: Id, Title: Title, Summary: Summary, Price: Price, isComplex: isComplex };

    $.ajax({
        type: 'POST',
        url: '/api/Products/Edit/',
        data: Product,
        success:
        function (data, textStatus, jqXHR) {
            var continuous_proposition_id = $('#date_select').val();
            UpdateContinuousProposition(continuous_proposition_id);
            hide_panels();
        }
    })
}

function create_product(proposition_id) {
    if (hasUnsavedData == true) {
        AlertHasUnsavedData();
        return false;
    }

    hide_panels();
    hide_validation_errors();

    $("#create_product").removeAttr("style").hide().fadeIn();

    $('#create_product_title').val('');
    $('#create_product_summary').val('');
    $('#create_product_price').val('0.0');
    $('#create_product_is_complex').attr('checked', false);

    $('#create_product_id').val(0);
    $('#create_product_proposition_id').val(proposition_id);

    hasUnsavedData = true;
}

function create_submit() {
    accordion_index = $("#continuous_proposition").accordion("option", "active");

    var propositionId = $('#create_product_proposition_id').val();

    var Id = $('#create_product_id').val();
    var Title = $('#create_product_title').val();
    var Summary = $('#create_product_summary').val();
    var Price = $('#create_product_price').val().replace(',', '.');
    var isComplex = $('#create_product_is_complex').is(':checked');

    if (validate_title('create', Title) == false || validate_price('create', Price) == false)
        return false;

    var product = { Id: Id, Title: Title, Summary: Summary, Price: Price, isComplex: isComplex };

    $.ajax({
        type: 'POST',
        url: '/api/Products/Create/' + propositionId,
        data: product,
        success:
        function (data, textStatus, jqXHR) {
            UpdateContinuousProposition($('#date_select').val());
            hide_panels();
        }
    })
}

function delete_product(product_id) {
    accordion_index = $("#continuous_proposition").accordion("option", "active");

    $.ajax({
        type: 'POST',
        url: '/api/Products/Delete/' + product_id,
        success:
        function (data, textStatus, jqXHR) {
            UpdateContinuousProposition($('#date_select').val());
            hide_panels();
        }
    })
}

function delete_proposition(continuous_proposition_id) {
    if (continuous_proposition_id != null) {
        $.ajax({
            type: 'POST',
            url: '/api/Propositions/Delete/' + continuous_proposition_id,
            success:
        function (data, textStatus, jqXHR) {
            UpdateDatesListAndDisplayingProposition();
            hide_panels();
        }
        });
    }
}

function product_cancel() {
    $("#create_product").removeAttr("style").hide().fadeIn();

    $('#create_product_title').val('');
    $('#create_product_summary').val('');
    $('#create_product_price').val('0.0');
    $('#create_product_is_complex').attr('checked', false);

    hide_validation_errors();

    $('#create_product_id').val(0);
    $('#create_product_proposition_id').val(0);
    hide_panels();
}