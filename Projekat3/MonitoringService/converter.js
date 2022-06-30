function convertToFloat(valueData){
    var fromBase64 = Buffer.from(valueData, 'base64')
    var data=[]
    fromBase64.forEach(el=>data.push(el));

    var buf = new ArrayBuffer(8);
    var view = new DataView(buf);

    data.forEach(function (b, i) {
        view.setUint8(i, b);
    });
    var num = view.getFloat64(0);
    return num;    
}

module.exports=convertToFloat;

