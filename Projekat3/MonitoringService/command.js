const axios = require('axios').default;
const path="http://command:48082/api/v1/device/07e07c76-7e75-474b-a562-40c196d5b79b/command/567afc4c-dbf8-4fc7-9b7a-1afab5e8086c"
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