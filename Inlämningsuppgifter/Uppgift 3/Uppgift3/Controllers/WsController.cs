using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Uppgift3.Models;

namespace Uppgift3.Controllers
{
   
    // det överliggande är exakt som i vår demo
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
            string? closereason = null;
            while (!webSocket.CloseStatus.HasValue)
            {
                var buffer = new byte[2048];
                var content = await webSocket.ReceiveAsync(buffer, CancellationToken.None);

                // deserialisera strängen till ett objekt
                var msgJson = Encoding.UTF8.GetString(buffer, 0, content.Count);
                var msg = JsonSerializer.Deserialize<MessageModel>(msgJson);
                // säkerställ att vi inte har null msg
                if(msg == null)
                {
                    _logger.LogError(nameof(msg) + "is null");
                    break;
                }
                
                // vi tar emot max 5 meddelanden sen stänger vi ned connectionen.
                if(5 <= msg.Counter)
                {
                    Random r = new Random();
                    var response = new ResponseModel { Id = r.NextSingle(), 
                        RecievedId = msg.Id, 
                        Message = $"I reached {msg.Counter} messages from you, that's enough son",
                        IsClosing = true
                    };

                    // serialisera respons objektet
                    var jsonResponse = JsonSerializer.Serialize(response);
                    var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);
                    
                    // skicka vår respons
                    await webSocket.SendAsync(
                        responseBytes,
                        WebSocketMessageType.Text,
                        WebSocketMessageFlags.EndOfMessage,
                        CancellationToken.None);

                    // stäng connection ordentligt
                    _logger.LogInformation($"Reached {msg.Counter} recieved messages, will close the websocket now");
                    closereason = $"Reached {msg.Counter} recieved messages";
                    break;
                }
                // skriv ut meddelandet till konsollen
                _logger.LogInformation(msg.Message + " " + msg.Id + " " + msg.Counter);

            }

            // stäng connection
            await webSocket.CloseAsync(
                     WebSocketCloseStatus.NormalClosure, 
                closereason ?? "Closing", 
                CancellationToken.None);
        }
    }
}
