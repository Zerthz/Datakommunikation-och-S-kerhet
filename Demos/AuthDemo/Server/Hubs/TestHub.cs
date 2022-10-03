using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs
{
    [Authorize]
    public class TestHub : Hub
    {
        [Authorize("OnlyAllowName")]
        public int Restricted()
        {
            return 42;
        }
    }
}
