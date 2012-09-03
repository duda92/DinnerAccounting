//----------------------------------------------------------------------------------------------
ko.bindingHandlers.accordion = {
    init: function (element, valueAccessor) {
        var options = valueAccessor() || {};
        setTimeout(function () {
            rebuild_accordion(element);
        }, 0);

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            rebuild_accordion(element);
        });
    },
    update: function (element, valueAccessor) {
        var options = valueAccessor() || {};
        rebuild_accordion(element);
    }
};

function rebuild_accordion(element) {
    $(element).accordion('destroy').accordion({ clearStyle: true, autoHeight: false, collapsible: true, active: false,
        change: function (event, ui) {
            setTimeout(function () {
                if (ui.newContent.length != 0) {
                    var date = ui.newHeader.attr("date");
                    viewModel.ChosenDate(date);
                }
                else {
                    viewModel.ChosenDate('');
                }
            }, 0);
        }
    });
    activate_disabling(element);
}

function activate_disabling(element) {
    var accordion = $(element).data("accordion");
    accordion._std_clickHandler = accordion._clickHandler;
    accordion._clickHandler = function (event, target) {
        var clicked = $(event.currentTarget || target);
        if (!clicked.hasClass("ui-state-disabled")) {
            this._std_clickHandler(event, target);
        }
    };
}