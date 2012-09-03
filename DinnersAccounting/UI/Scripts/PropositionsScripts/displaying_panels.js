function hide_panels() {

    $("#edit_product").css('visibility', 'hidden');
    $("#create_product").css('visibility', 'hidden');

    hasUnsavedData = false;
}

function hide_validation_errors() {
    $('#edit_product_validation_title').css('display', 'none');
    $('#edit_product_validation_price').css('display', 'none');
    $('#create_product_validation_price').css('display', 'none');
    $('#create_product_validation_title').css('display', 'none');

}

function AlertHasUnsavedData()
{
    $('#unsaved_data_alert').dialog('open');
}

function add_continuous_proposition() {
    $('#create_proposition_dialog').dialog('open');
}