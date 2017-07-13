function TeamModel(data) {
    this. id = ko.observable(0);
    this.description = null;

    if (data != null) {
        this.description = data.Description;
        this.id(data.ID);
    }
}
    
