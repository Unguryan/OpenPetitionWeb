/*/*<reference path="../lib/jquery/jquery.js" />*/



"use strict";

if (typeof getCookie("Token") !== 'undefined') {

    var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();
    connection.on("displayNotification", (mes) => {
        console.log(mes);
        alert(mes);
        //var heading = document.createElement("h3");
        //heading.textContent = articleHeading;
        //var p = document.createElement("p");
        //p.innerText = articleContent; var div = document.createElement("div");
        //div.appendChild(heading);
        //div.appendChild(p);

        /* document.getElementById("articleList").appendChild(div);*/
    });

    connection.start().catch(function (err) {
        return console.error(err.toString());
    }).then(function () {

        connection.invoke('GetConnectionId')
            .then(function (connectionId) {
                //console.log("Hi");
                //console.log(jQuery.alert("YO"));
                //console.log("Hi");
                //console.log(typeof connectionId);

                $.ajax({
                    dataType: "json",
                    type: "POST",
                    url: "/User/SetId",
                    contentType: "application/json; charset=utf-8",
                    //data: connectionId,
                    data: JSON.stringify(connectionId),
                    //success: function (data) {
                    //    console.log(("OK"));
                    //},
                    //error: function (error) { console.log("NOT OK"); }
                });
            });

    });
}

function getCookie(cookieName) {
    let cookie = {};
    document.cookie.split(';').forEach(function (el) {
        let [key, value] = el.split('=');
        cookie[key.trim()] = value;
    })
    return cookie[cookieName];
}