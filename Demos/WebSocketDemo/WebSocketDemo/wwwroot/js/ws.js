let clientId = `Client${Math.random()}`

const socket = new WebSocket("ws://localhost:5156/ws");

socket.onopen = e => {
    console.log("Socket open", e);

    setInterval(() => {
        socket.send(`Hej från ${clientId}`);
    }, 1000)
};