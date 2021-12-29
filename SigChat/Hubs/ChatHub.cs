using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SigChat.Data;
using SigChat.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SigChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<ChatUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ChatHub(UserManager<ChatUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task BroadCastFromClient(string message)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(Context.User);

                Message m = new Message()
                {
                    MessageBody = message,
                    FromUser = currentUser,
                    MessageDate = DateTime.Now
                };
                _context.Messages.Add(m);
                await _context.SaveChangesAsync();

                await Clients.All.SendAsync(
                    "BroadCast",
                    new
                    {
                        messageBody = m.MessageBody,
                        fromUser = currentUser.Email,
                        messageDate = m.MessageDate.ToString("hh:mm tt MMM dd", CultureInfo.InvariantCulture)
                    });
            }catch(Exception ex)
            {
                await Clients.Caller.SendAsync("HubError", new { error = ex.Message });
            }
        }
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync(
                "UserConnected",
                new
                {
                    connectionId = Context.ConnectionId,
                    connectionDTime = DateTime.Now,
                    messageDTime = DateTime.Now.ToString("hh:mm tt MMM dd", CultureInfo.InvariantCulture)
                });
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync(
                "UserDisconnected",
                $"User Disconnected, connectionId: {Context.ConnectionId}");
        }
    }
}
