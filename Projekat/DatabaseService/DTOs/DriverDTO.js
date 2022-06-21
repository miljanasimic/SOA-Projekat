class DriverDTO {
    constructor(id, firstname, lastname, url, dob) {
        this.id = id
        this.firstname = firstname;
        this.lastname = lastname;
        this.url = url
        this.birthdate = dob
    }
}

module.exports = {DriverDTO}