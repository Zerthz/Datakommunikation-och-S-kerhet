let id = Math.random();
let isClosing = false;

let message = {
    Id: id,
    Message: "Hejsan",
    Counter: 0
};


const socket = new WebSocket("ws://localhost:5207/ws");


socket.addEventListener('open', (event) => {
    console.log("Socket open", event);

    let intervalId = setInterval(() => {
        if (isClosing === true) {
            clearInterval(intervalId);
        } else {
            let messageJson = JSON.stringify(message);
            socket.send(messageJson);
            message.Counter = message.Counter + 1
        }        

    }, 3000)
});

socket.addEventListener('message', (event) => {
    let response = JSON.parse(event.data);
    console.log(`Message from server: Msg Id: ${response.Id}\n \"${response.Message}\"\nClosing Socket`);
    if (response.IsClosing) {
        socket.close();
        isClosing = response.IsClosing;
    }
});

