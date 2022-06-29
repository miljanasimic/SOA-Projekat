import csv
import requests
import json
import time

edgexip = '127.0.0.1'

pressure = list()
temperature = list()
humidity = list()

with open("sensorData.csv", "r", newline='') as csvfile:
    reader = csv.DictReader(csvfile)
    for row in reader:
        pressure.append(row['pressure'])
        temperature.append(row['temperature'])
        humidity.append(row['humidity'])


def generateSensorData(pos):
    p = pressure[pos]
    t = temperature[pos]
    h = humidity[pos]
    print("Sending values: Pressure %s, Temperature %s, Humidity %s" % (p, t, h))
    return (p, t, h)


if __name__ == "__main__":
    pos = 0
    time.sleep(2)
    while(pos < len(pressure)):

        (pressureVal, temperatureVal, humidityVal) = generateSensorData(pos)

        url = 'http://%s:49986/api/v1/resource/Pressure_Temp_and_Humidity_sensor_cluster_01/pressure' % edgexip
        payload = pressureVal
        headers = {'content-type': 'application/json'}
        response = requests.post(url, data=payload, verify=False)
        print(response, response.reason, response.text)

        url = 'http://%s:49986/api/v1/resource/Pressure_Temp_and_Humidity_sensor_cluster_01/temperature' % edgexip
        payload = temperatureVal
        headers = {'content-type': 'application/json'}
        response = requests.post(url, data=payload, verify=False)

        url = 'http://%s:49986/api/v1/resource/Pressure_Temp_and_Humidity_sensor_cluster_01/humidity' % edgexip
        payload = humidityVal
        headers = {'content-type': 'application/json'}
        response = requests.post(url, data=payload, verify=False)

        pos += 1

        time.sleep(5)
