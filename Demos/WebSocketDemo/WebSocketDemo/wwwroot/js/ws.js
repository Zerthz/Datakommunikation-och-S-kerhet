let clientId = `Client${Math.random()}`

const socket = new WebSocket("ws://localhost:5156/ws");


socket.addEventListener('open', (event) => {
    console.log("Socket open", e);

    setInterval(() => {
        socket.send(`Hej från ${clientId}`);
    }, 3000)
});



socket.addEventListener('message', (event) => {
    console.log('Message from server ', event.data);
});