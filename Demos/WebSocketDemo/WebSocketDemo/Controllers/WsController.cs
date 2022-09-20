using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace WebSocketDemo.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class WsController : ControllerBase
    {
        private readonly ILogger<WsController> _logger;

        public WsController(ILogger<WsController> logger)
        {
            _logger = logger;
        }
        [HttpGet("/ws")]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await Reader(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        private async Task Reader(WebSocket webSocket)
        {
            // för att skicka till clienten också
            // webSocket.SendAsync();
            var bytes = Encoding.UTF8.GetBytes("Ready to recieve!");
            await webSocket.SendAsync(bytes, WebSocketMessageType.Text, WebSocketMessageFlags.EndOfMessage, CancellationToken.None);
           
            while (!webSocket.CloseStatus.HasValue)
            {
                var buffer = new byte[2048];
                var content = await webSocket.ReceiveAsync(buffer, CancellationToken.None);

                var s = Encoding.UTF8.GetString(buffer, 0, content.Count);
                _logger.LogInformation(s);

              
            }

           

            await webSocket.CloseAsync(
                    webSocket.CloseStatus.Value,
                webSocket.CloseStatusDescription,
                CancellationToken.None);


        }
    }
}
