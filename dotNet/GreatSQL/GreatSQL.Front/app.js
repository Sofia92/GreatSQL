var Express = require("express");
var App = Express();

App.use(Express.static("site"));

var Server = App.listen(3000, function () {
    var host = Server.address().address;
    var port = Server.address().port;
   
    console.log("Example app listening at http://%s:%s", host, port);

    console.log("Start !");
});