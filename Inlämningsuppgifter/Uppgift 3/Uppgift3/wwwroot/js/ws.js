let id = Math.random();

let message = {
    Id: id,
    Message: "Hejsan",
    Counter: 0
};


const socket = new WebSocket("ws://localhost:5207/ws");


socket.addEventListener('open', (event) => {
    console.log("Socket open", event);

    setInterval(() => {
        let messageJson = JSON.stringify(message);
        socket.send(messageJson);
        message.Counter = message.Counter + 1
    }, 3000)
});


