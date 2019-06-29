"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:2321/messageHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("SendMessage", function (message) {
    $('#messageSpan').text(message);
    $('.alert').css('display', 'inline-block');

});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
