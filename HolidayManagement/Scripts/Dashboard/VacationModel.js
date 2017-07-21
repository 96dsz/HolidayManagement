function VacationModel(data) {
    var _self = this;

    this.startDate = null;
    this.endDate = null;
    this.userId = null;


    if (data != null) {
        this.startDate = data.StartDate;
        this.endDate = data.Enddate;
        this.userId = data.UserId;
    }
}