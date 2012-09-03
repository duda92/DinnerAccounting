function validate_title(create_or_edit, value) {
    if (value.trim() == "") {
        if (create_or_edit == 'create')
            $('#create_product_validation_title').css('display', 'block');
        if (create_or_edit == 'edit')
            $('#edit_product_validation_title').css('display', 'block');

        return false;
    }
    else {
        if (create_or_edit == 'create')
            $('#create_product_validation_title').css('display', 'none');
        if (create_or_edit == 'edit')
            $('#edit_product_validation_title').css('display', 'none');

        return true;
    }
}

function validate_price(create_or_edit, value) {
    var re = /^[0-9]{1,6}([,.][0-9]{1,2})?$/;
    var regTest = re.test(value);
    if (regTest == false) {
        if (create_or_edit == 'create')
            $('#create_product_validation_price').css('display', 'block');
        if (create_or_edit == 'edit')
            $('#edit_product_validation_price').css('display', 'block');

        return false;
    }
    else {
        if (create_or_edit == 'create')
            $('#create_product_validation_price').css('display', 'none');
        if (create_or_edit == 'edit')
            $('#edit_product_validation_price').css('display', 'none');

        return true;
    }
}

function ValidateDates(start_date, end_date) {

    if (end_date.trim() == "" || start_date.trim() == "") {
        $('#create_proposition_validation').text('Enter valid dates!');
        $('#create_proposition_validation').css('display', 'block');
        return false;
    }
            
    var StartDate = new Date(start_date);
    var EndDate = new Date(end_date);

    if (StartDate > EndDate) {
        $('#create_proposition_validation').css('display', 'block');
        $('#create_proposition_validation').text('Start date must be less than End date!');
        return false;
    }
    else {
        $('#create_proposition_validation').css('display', 'none');
        $('#create_proposition_validation').text('');
        return true;
    }
}