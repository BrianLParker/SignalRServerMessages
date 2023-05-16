using Microsoft.AspNetCore.SignalR;
using SignalRServerMessages.Server.Hubs;

namespace SignalRServerMessages.Server.Services
{
    public interface ISomeService
    {
    }
    public class SomeService : ISomeService
    {
        private readonly IHubContext<ChatHub> chatHubContext;
        private readonly System.Timers.Timer timer;
        private int count;

        public SomeService(IHubContext<ChatHub> chatHubContext)
        {
            this.chatHubContext = chatHubContext;
            this.timer = new System.Timers.Timer(2000);
            this.timer.Elapsed += Timer_Elapsed;
            this.count = 0;
            this.timer.Start();
        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            this.count++;

            Task.Run(() => this.chatHubContext.Clients.All.SendAsync("ReceiveMessage", "SomeService", $"Count: {this.count}"));
        }
    }
}
