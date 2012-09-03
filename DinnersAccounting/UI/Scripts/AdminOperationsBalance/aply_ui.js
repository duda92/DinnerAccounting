
function apply_ui() {
    $(viewModel.people_list).selectable({
        selected: function (event, ui) {

            var personId = parseInt(ui.selected.id);
            var person = get_balance(personId);
            
            show_operations_form(person);
        },
        unselected: function (event, ui) { hide_operations_form(); }
    });
    $(viewModel.dialog).dialog({ autoOpen: false, width: 700, height: 500, modal: true });
}

function validate_amount(value) {
    var re = /^-?[0-9]{1,6}([,.][0-9]{1,2})?$/;
    var regTest = re.test(value);
    if (regTest == false) {
        $('#enter_sum_validation_title').css('display', 'block');
        return false;
    }
    else {
        $('#enter_sum_validation_title').css('display', 'none');
        return true;
    }
}

function create_operation(){
    var amount = $('#amount').val();
    if (validate_amount(amount) == false)
        return false;

    var summary = $('#summary').val();
    var personId = parseInt($('#person_id').val());
    $.ajax('/api/People/PerformAccountOperation/',
        {
            type: 'Get',
            data: { personId: personId, amount: amount, summary: summary },
            success: function (data, textStatus, jqXHR) {
                var person = get_balance(personId);
                show_operations_form(person);
            },
            error: function (jqXHR, textStatus, errorThrown) {
            }
        });
    
}

function show_operations_form(person) {
    clear_form();
    $('#person_id').val(person.Id);
    $('#name_of_selected').html(person.DomainName);
    $('#balance_of_selected').html(person.Balance);
    $('#operations_div').css('visibility', 'visible');
    $('#enter_sum_validation_title').css('display', 'none');
    show_history();
}

function hide_operations_form() {
    clear_form();
    clear_history();
    $('#operations_div').css('visubility', 'hidden');
}

function clear_form() {
    $('#name_of_selected').empty();
    $('#balance_of_selected').empty();
    $("#amount").val('');
    $("#summary").val('');
    clear_history();
}

function get_balance(person_id) {
    var person = {};
    $.ajax('/api/People/GetBalance/',
        {
            type: 'Get',
            async: false,
            data: { personId: person_id },
            success: function (person_data, textStatus, jqXHR) {
                person = JSON.parse(person_data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
            }
        });
        return person;
    }

    function get_person_history(person_id) {
        var history;
        $.ajax('/api/People/GetHistory/',
        {
            type: 'Get',
            async: false,
            contentType: "application/xml; charset=utf-8;",
            data: { personId: person_id },
            success: function (history_data, textStatus, jqXHR) {
                history = JSON.parse(history_data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
            }
        });
        return history;
    }
    