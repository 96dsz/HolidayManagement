function CalendarModel(data) {
    var _self = this;

    this.day = ko.observable();
    this.description = null;
    this.name = null;
    this.isFreeDay = null;
    this.vacations = ko.observable([]);

    if (data != null) {
        this.description = data.Description;
        this.day(data.Day);
        this.name = data.Name;
        this.isFreeDay = data.IsFreeDay;

        var vacations = _.map(data.Vacations, function(v){
            return VacationModel(v);
        });
        this.vacations(vacations);
    }
    function getMonth(data)
    {

    }
       
}