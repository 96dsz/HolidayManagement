function RoleModel(data) {
    this.id = ko.observable(0);
    this.name = null;

    if (data != null) {
        this.name = data.Name;
        this.id(data.Id);
    }
}
