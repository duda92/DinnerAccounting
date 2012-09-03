function apply_ui() {
    $("#create_proposition_start_date").datepicker();

    $("#create_proposition_end_date").datepicker();

    $('#create_proposition_dialog').dialog({
        resizable: true,
        autoOpen: false,
        modal: true,
        width: 400,
        height: 400,
        buttons: {
            'Продолжить': function () {
                
                var start_date = $('#create_proposition_start_date').val();
                var end_date = $('#create_proposition_end_date').val();

                if (ValidateDates(start_date, end_date) == false)
                    return false;

                var cp = { Id: 0, StartDate: start_date, EndDate: end_date };

                $.ajax({
                    type: 'POST',
                    url: '/api/Propositions/Create/',
                    data: cp,
                    success:
                        function (id, textStatus, jqXHR) {
                            UpdateDatesListAndDisplayingProposition(id);
                        }
                });

                $(this).dialog('close');
            },
            'Отмена': function () {
                $(this).dialog('close');
            }
        }

    });

    $('#unsaved_data_alert').dialog({
        resizable: true,
        autoOpen: false,
        modal: true,
        width: 300,
        height: 200,
        buttons: {
            'Ok': function () {
                $(this).dialog('close');
            }
        }

    });

}
