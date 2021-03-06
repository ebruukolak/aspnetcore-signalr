﻿"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:2321/messageHub").build();

connection.on("SendMessage", function (message) {
    $('#messageSpan').text(message);
    $('.alert').css('display', 'inline-block');

});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

var btn = document.getElementById('sendButton');
if (btn) {
    el.addEventListener('click', swapper, false); addEventListener("click", function (event) {
        connection.invoke("SendMessage", message).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    });
}

