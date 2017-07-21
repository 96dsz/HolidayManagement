function DashboardModel() {
    var _self = this;
    this.message = null;
    this.teamList = [];
    this.roleList = [];
    this.calendar = ko.observableArray();
    this.dateTimeReviver = [];
    this.monthName = ko.observable();

    this.users = ko.observable([]);

    this.manageUser = new UserModel();

    this.errorMessage = ko.observable("");

    this.month = ko.observable("");
    this.year = ko.observable("");
    this.fromDate = ko.observable();
    this.endDate = ko.observable();

    this.initialize = function (data) {
        _self.message = data.Message;

        var users = _.map(data.UserList, function (user) {
            return new UserModel(user);
        });

        _self.users(users);

        _self.teamList = _.map(data.TeamList, function (team) {
            return new TeamModel(team);

        });

        _self.roleList = _.map(data.RoleList, function (role) {
            return new RoleModel(role);
        });

        _self.dateTimeReviver = _.map(DashboardModel.dateTimeReviver, function (dateTime) {
            return new dateTimeReviver(dateTime);

        });
        var calendar = _.map(data.Calendar.MonthDays, function (calendar, index) {
            return new CalendarModel(calendar);
        });

        _self.calendar(calendar);
        
        var currentDate = new Date();
        _self.month(currentDate.getMonth() + 1);
        _self.year(currentDate.getFullYear());

        _self.monthName(data.Calendar.Month);

    };

    this.createUser = function (data) {
        $.ajax({
            url: "/Account/CreateUser",
            type: "POST",
            data: {
                firstName: _self.manageUser.firstName(),
                lastName: _self.manageUser.lastName(),
                teamId: _self.manageUser.team.id(),                
                AspnetUser: {
                    Email: _self.manageUser.email(),
                    Roles: [{RoleId: _self.manageUser.role.id()}]
                }
            },

            success: function (data) {
                if (data.successed) {
                    var users = _self.users();
                    users.push(_self.manageUser);

                    _self.users(users);


                    $('#myModal').modal('hide');
                }
                else {
                    _self.errorMessage(data.messages);
                }
            }

        }

        )
    };
    this.editUser = function (data) {
        $.ajax({
            url: "/Account/EditUser",
            type: "POST",
            data: {
                id: _self.manageUser.id(),
                firstName: _self.manageUser.firstName(),
                lastName: _self.manageUser.lastName(),
                teamId: _self.manageUser.team.id(),
                roleId: _self.manageUser.role.id(),
                AspnetUser: {
                    Email: _self.manageUser.email()
                }
            },

            success: function (data) {
                if (data.successed) {
                    if (_self.manageUser.id() == 0) {
                        var users = _self.users();
                        _self.users(users);



                    }
                    else {
                        var user = _.find(data.UserList, function (user) {
                            return user.id() == _self.manageUser.id();
                        });

                        if (user != null) {
                            user.firstName(_self.manageUser.firstName());
                            user.lastName(_self.manageUser.lastName());
                            user.AspnetUser.Email(_self.manageUser.AspnetUser.Email());
                            user.hireDate(_self.manageUser.hireDate());
                            user.maxDays(_self.manageUser.maxDays());

                        }
                    }

                    $('#myModal').modal('hide');
                }
                else {
                    _self.errorMessage(data.messages);
                }
            }

        }

        )
    };

    this.edit = function (data) {

        $.ajax({
            url: "/Account/GetUserById",
            type: "POST",
            data: {
                id: data.id
            },

            success: function (data) {
                if (data.user != null) {
                    _self.manageUser.id(data.user.ID);
                    _self.manageUser.firstName(data.user.FirstName);
                    _self.manageUser.lastName(data.user.LastName);
                    if (data.user.AspnetUser != null)
                        _self.manageUser.email(data.user.AspnetUser.Email);
                    _self.manageUser.hireDate(data.user.HireDate);
                    _self.manageUser.maxDays(data.user.maxDays);

                    _self.manageUser.team = new TeamModel(data.user.Team);

                    _self.manageUser.createUser(false);
                    $('#myModal').modal('show');
                }
            }
        });
    }
    var setDateWithZero = function (date) {

        if (date < 10)

            date = "0" + date;

        return date;

    };

    var dateTimeReviver = function (value) {

        var match;

        if (typeof value === 'string') {

            match = /\/Date\((\d*)\)\//.exec(value);

            if (match) {

                var date = new Date(+match[1]);

                return date.getFullYear() + "-" + setDateWithZero(date.getMonth() + 1) + "-" +

                setDateWithZero(date.getDate()) +

                "T" + setDateWithZero(date.getHours()) + ":" +

                setDateWithZero(date.getMinutes()) + ":" + setDateWithZero(date.getSeconds()) + "." +

                date.getMilliseconds();
            }
        }

        return value;
    }
        this.prevMonth = function () {
            if (_self.month() == 1) {
                _self.month(12);
                _self.year(_self.year() - 1);
            }
            else
                _self.month(_self.month() - 1);
            $.ajax({
                url: "/Dashboard/GetMonth",
                type: "GET",
                data: {
                    month: _self.month(),
                    year: _self.year(),
                },
                success: function (data) {
                    _self.monthName(data.calendar.Month)
                    var days = _.map(data.calendar.MonthDays, function (day, index) {
                        return new CalendarModel(day);
                    });
                    _self.calendar(days);
                }
            })
        }

        this.nextMonth = function () {
            if (_self.month() == 12) {
                _self.month(1);
                _self.year(_self.year() + 1);
            }
            else
                _self.month(_self.month() + 1);

            $.ajax({
                url: "/Dashboard/GetMonth",
                type: "GET",
                data: {
                    month: _self.month(),
                    year: _self.year(),
                },
                success: function (data) {
                    _self.monthName(data.calendar.Month)
                    var days = _.map(data.calendar.MonthDays, function (day, index) {
                        return new CalendarModel(day);
                    });
                    _self.calendar(days);
                }
            })
        }
}
                 

  

function InitializeDashboardModel(data) {

    DashboardModel.instance = new DashboardModel();



    DashboardModel.instance.initialize(data);



    ko.applyBindings(DashboardModel.instance);

}