function TodayDateFormat(daysOffset) {
    if (typeof daysOffset == 'undefined') {
        daysOffset = 0;
    }
    var date = new Date().addDays(daysOffset);
    var formattedDate = (date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear();
    return formattedDate;
}

function getDay(date) {
    for (var i = 0; i < viewModel.DaysList().length; i = i + 1) {
        var day = viewModel.DaysList()[i];
        if (day.Date == date) {
            return viewModel.DaysList()[i];
        }
    }
    return null;
}

var viewModel =
    {
        DateStart: ko.observable(TodayDateFormat()),
        DateEnd: ko.observable(TodayDateFormat(7)),

        DaysList: ko.observableArray([]),

        ChosenDay: ko.observable(new Day('', new Status(0, []), { Complexes: [], EveryDay: [], Also: [] }, [])),
        ChosenDate: ko.observable(),

        HasDayStatusChanges: ko.observable(false),
        HasOrderChanges: ko.observable(false),
        ChosenOrder: ko.observable(new Order(0, [], '', [], {})),

        display_chosen_date: ko.observable(),
        display_order_status: ko.observable(),


        ApplyStatus: function (day) {

            var date = day.Date;
            var statusValue = day.Status().StatusValue();

            SaveStatus(date, statusValue);
            var serverStatus = GetStatus(date);
            day.Status(new Status(serverStatus.Status, serverStatus.AvaliableStatuses));
            viewModel.UpdatePersons();
            viewModel.HasDayStatusChanges(false);
            viewModel.HasOrderChanges(false);

        },

        SaveOrder: function (order) {
            order = order.getObject();
            putOrder(order);
            viewModel.UpdateOrder();
            viewModel.HasOrderChanges(false);

            viewModel.ChosenOrder('');
        },

        EditOrder: function (order) {

            viewModel.ChosenOrder(order);
            viewModel.HasOrderChanges(true);
            viewModel.ChosenOrder().Editable(true);
        },

        CancelEditOrder: function (arg) {
            viewModel.UpdateOrder();
            viewModel.HasOrderChanges(false);
            viewModel.ChosenOrder('');
        },

        UpdateOrder: function () {
            var date = this.ChosenOrder().Date();
            var person = this.ChosenOrder().Person;

            var order = getOrderByDateAndPerson(date, person);
            this.ChosenOrder().Id(order.Id);
            this.ChosenOrder().OrderDetails(order.OrderDetails);
            this.ChosenOrder().Date(order.Date);
            this.ChosenOrder().Statuses(order.Statuses);
            this.ChosenOrder().Person = order.Person;
        },

        UpdatePersons: function () {
            var day = this.ChosenDay();
            ko.utils.arrayForEach(day.Orders(), function (order) {
                console.log(order().Person.Id);
                var person = getPerson(order().Person.Id);
                order().Person.Balance(person.Balance);
            });
        },

        UpdateDaysList: function () {
            var vm = this;

            var dateStart = this.DateStart();
            var dateEnd = this.DateEnd();

            var serverDaysList = getDaysList(dateStart, dateEnd);
            this.DaysList.removeAll();
            $.each(serverDaysList, function (key, serverDay) {
                var day = ObjToObservableDay(serverDay);         
                vm.DaysList.push(day);
            });
        }
    };

    viewModel.ChosenDate.subscribe(function (newChosenDate) {
        viewModel.display_chosen_date(newChosenDate.split('T')[0]);
        var day = getDay(newChosenDate);
        if (typeof day == 'undefined' || day == null) {
            viewModel.ChosenDay(new Day('', new Status(0, []), { Complexes: [], EveryDay: [], Also: [] }, []));
            return;
        }
        viewModel.ChosenDay(day);
    });

    viewModel.HasOrderChanges.subscribe(function (value) {
        if (value == true) {
            $('.ui-accordion-header').addClass('ui-state-disabled');
            $('.save-day-status-button').addClass('ui-state-disabled');
        }
        else {
            $('.ui-accordion-header').removeClass('ui-state-disabled');
            $('.save-day-status-button').removeClass('ui-state-disabled');
        }
     });
    
