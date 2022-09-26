"use strict";

/// <reference path="signalr/dist/browser/signalr.js" />

var connection =
    new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5233/TestHub")
        .withAutomaticReconnect()
        .build()

connection.start()
    .then(async () => {
        console.log("Connected!");

        const response = await connection.invoke("Complex", {
            name: "Felix",
            Age: 27
        });

        console.log("RESPONSE ", response);
    })
    .catch(e => {
        console.log(e);
    });

connection.on("Log", (message) => {
    console.log(message);
});

