function Status(statusValue, avalibleStatuses) {
    var self = this;

    self.StatusValue = ko.observable(statusValue);
    self.AvaliableStatuses = avalibleStatuses;

    self.LastSavedStatus = ko.observable(statusValue);

    self.StatusToText = function(statusValue) {
        switch (statusValue) {
            case 0:
                return "Заказов не было";
            case 1:
                return "Не отправлен";
            case 2:
                return "Отправлен";
            case 3:
                return "Доставлен";
            case 4:
                return "Не доставлен";
            case 5:
                return "Оплачено";

            default:
                return "Не определен";
        }
    };

    self.DisplayStatus = ko.computed(function () {

        switch (self.LastSavedStatus()) {
            case 0:
                return "Заказов не было";
            case 1:
                return "Не отправлен";
            case 2:
                return "Отправлен";
            case 3:
                return "Доставлен";
            case 4:
                return "Не доставлен";
            case 5:
                return "Оплачено";

            default:
                return "Не определен";
        }

    }, this);

    self.StatusValue.subscribe(function (newChosenDate) {
        viewModel.HasDayStatusChanges(true);
        
    });

    return {
        StatusValue: self.StatusValue,
        DisplayStatus: self.DisplayStatus,
        StatusText: self.StatusText,
        AvaliableStatuses : self.AvaliableStatuses,
        StatusToText : self.StatusToText,
        LastSavedStatus: self.LastSavedStatus
    };
}

