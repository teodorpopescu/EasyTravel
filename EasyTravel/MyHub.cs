using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace GlassBall
{
    [HubName("myHub")]
    public class MyHub : Hub
    {
        public override Task OnConnected()
        {
            return base.OnConnected();
        }
        public void Send(string userid, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.Client(userid).broadcastMessage(message);
        }
    }
}