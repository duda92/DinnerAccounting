/// <reference path="aply_ui.js" />
function show_history() {
    clear_history();
    var person_id = $('#person_id').val();
    var person_history = get_person_history(person_id);
    draw_history(person_history);
}

function clear_history() {
    $('#history').empty();
}

function draw_history(history) {
    var table = $('<table>').css('width', '100%').css('text-align', 'center').appendTo($('#history'));
    var th_date = $('<th>').append($('<label>').html('Дата')).appendTo(table);
    var th_amount = $('<th>').append($('<label>').html('Сумма')).appendTo(table);
    var th_summary = $('<th>').append($('<label>').html('Комментарий')).appendTo(table);
    
    $.each(history, function (index) {
        add_history_item(history[index], table);
    });
}

function add_history_item(history_item, table) {
    var tr = $('<tr>').appendTo(table);
    var td_date = $('<td>').append($('<label>').html(history_item.Date.split("T")[0])).appendTo(tr);
    var amount_color = '#000000'; var amount_text = history_item.Amount;
    if (history_item.Amount > 0 && typeof history_item.OrderId == 'undefined') {
        amount_color = 'Green';
        amount_text = '+' + amount_text;
    }
    else {
        amount_text = '-' + amount_text;
    }
    var td_amount = $('<td>').append($('<label>').css('color', amount_color).html(amount_text)).appendTo(tr);
    var td_summary = $('<td>').append($('<label>').html(history_item.Summary)).appendTo(tr);
    if (typeof history_item.OrderId != 'undefined') {
        var td_navigation = $('<td>').append($('<a href="#">').click(function () { PopupOrder(history_item.OrderId, viewModel); }).html('Перейти к заказу')).appendTo(tr);
    }
}

