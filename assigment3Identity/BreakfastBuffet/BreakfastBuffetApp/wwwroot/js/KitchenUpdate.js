"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/KitchenReportService").build();

connection.on("kitchenUpdate", function () {

    window.location.reload("kitchen");
});

connection.start().then(function () {
    console.log("Connected");
}).catch(function (err) {
    console.error(err.toString());
});
