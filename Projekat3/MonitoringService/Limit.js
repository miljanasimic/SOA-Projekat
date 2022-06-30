class Limit{
    constructor(){
        this.temperatureLimit = 13.0;
        this.pressureLimit = 94200.0;
        this.humidityLimit = 88.0;
        this.tAboveLimit=0;
        this.pAboveLimit=0;
        this.hAboveLimit=0;
    }

    getTemperatureLimit(){ return this.temperatureLimit;}
    getHumidityLimit(){ return this.humidityLimit;}
    getPressureLimit(){ return this.pressureLimit;}

    setTemperatureLimit(newLimit){ this.temperatureLimit=newLimit;}
    setHumidityLimit(newLimit){ this.humidityLimit=newLimit;}
    setPressureLimit(newLimit){ this.pressureLimit=newLimit;}

    getTemperatureAboveLimit(){ return this.tAboveLimit;}
    getHumidityAboveLimit(){ return this.hAboveLimit;}
    getPressureAboveLimit(){ return this.pAboveLimit;}

    clearTemperatureAboveLimit(){ this.tAboveLimit=0;}
    clearHumidityAboveLimit(){ this.hAboveLimit=0;}
    clearPressureAboveLimit(){ this.pAboveLimit=0;}
    
    incrTemperatureAboveLimit(){ this.tAboveLimit++;}
    incrHumidityAboveLimit(){ this.hAboveLimit++;}
    incrPressureAboveLimit(){ this.pAboveLimit++;}
}

module.exports = Limit;