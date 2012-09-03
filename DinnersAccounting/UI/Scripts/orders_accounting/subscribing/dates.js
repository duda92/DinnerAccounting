viewModel.DateStart.subscribe(function (newDate) {
    viewModel.UpdateDaysList();
});

viewModel.DateEnd.subscribe(function (newDate) {
    viewModel.UpdateDaysList();
});

