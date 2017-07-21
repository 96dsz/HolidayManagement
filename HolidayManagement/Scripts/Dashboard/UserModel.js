function UserModel(data) {   
    this.id = ko.observable(null);
    this.firstName = ko.observable(null);
    this.lastName = ko.observable(null);
    this.email = ko.observable(null);
    this.hireDate = ko.observable(null);
    this.maxDays = ko.observable(null);
    this.team = new TeamModel();
    this.createUser = ko.observable(true);
    this.role = new RoleModel();
    this.fullName = ko.observable(null);

    if (data != null) {
        this.id(data.ID);
        this.firstName(data.FirstName);
        this.lastName(data.LastName);
        this.email(data.AspnetUser.Email);
        this.hireDate(data.HireDate);
        this.maxDays(data.maxDays);
        var fullName = data.FirstName + ' ' + data.LastName;
        this.fullName(fullName);
       
      
        this.role = new RoleModel(data.Role);
        this.team = new TeamModel(data.Team);
    }    
}