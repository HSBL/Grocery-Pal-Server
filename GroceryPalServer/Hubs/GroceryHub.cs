using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace GroceryPalServer.Hubs
{
    public class GroceryHub : Hub
    {
        public async Task Update()
        {
            await Clients.All.SendAsync("RecieveUpdate");
        }
    }
}
