class CircuitDTO {
    constructor(name, ref, lat, lng, alt, url) {
        this.name = name
        this.circuitRef = ref
        this.latitude = lat
        this.longitude = lng
        this.altitude = alt
        this.url = url
    }
}

module.exports = { CircuitDTO }