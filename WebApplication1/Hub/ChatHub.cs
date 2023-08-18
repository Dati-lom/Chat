using Chat.Context;
using Chat.Models;

namespace Chat.Hub;
using Microsoft.AspNetCore.SignalR;


public class ChatHub:Hub
{
    private readonly MessagesDB _messagesDb;

    public ChatHub(MessagesDB messagesDb)
    {
        _messagesDb = messagesDb;
    }

    public async Task sendMessage(MessageModel message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
        _messagesDb.Message.Add(message);
        _messagesDb.SaveChanges();
    }
}