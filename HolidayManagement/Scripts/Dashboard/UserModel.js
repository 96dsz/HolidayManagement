function UserModel(data) {
    this.firstName = ko.observable(null);
    this.lastName = ko.observable(null);
    this.email = ko.observable(null);
    this.hireDate = ko.observable(null);
    this.maxDays = ko.observable(null);
    this.team = new TeamModel();

    if (data != null) {
        this.firstName(data.FirstName);
        this.lastName(data.LastName);
        this.email(data.AspnetUser.Email);
        this.hireDate(data.HireDate);
        this.maxDays(data.maxDays);

        this.team = new TeamModel(data.Team);
    }
}