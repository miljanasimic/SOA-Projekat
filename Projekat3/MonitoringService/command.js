const axios = require('axios').default;
const path="http://edgex-core-command:48082/api/v1/device/name/TestApp/command/color"
function sendCommand(newColor, parameterName){
    axios.put(path, {
        color: newColor,
        parameterName: parameterName
      })
      .catch(function (error) {
        console.log(error);
      });
}

module.exports=sendCommand