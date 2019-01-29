var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.start().catch(function (err) {
    return console.error(err.toString());
});

connection.on("ReceiveMessage", function (user, message) {
    var date = new Date();
    var readyMessage = date.getHours() + ":" + date.getMinutes() + " " + user + ": " + message;
    var li = document.createElement("li");
    li.textContent = readyMessage;
    document.getElementById("messagesList").appendChild(li);
});

document.getElementById("sendMessageButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("sendChatName").addEventListener("click", function (event) {
    var chatName = document.getElementById("chatNameInput").value;
    connection.invoke("ChooseChat", chatName).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});