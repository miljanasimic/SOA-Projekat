const axios = require('axios').default;
const path="http://edgex-core-command:48082/api/v1/device/name/TestApp/command/color"
function sendCommand(newColor){
    axios.put(path, {
        color: newColor
      })
      .then(function (response) {
        console.log(response);
      })
      .catch(function (error) {
        console.log(error);
      });
}

module.exports=sendCommand