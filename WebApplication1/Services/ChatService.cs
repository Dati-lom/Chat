using Chat.Context;
using Chat.Hub;
using Chat.Models;

namespace Chat.Services;

public class ChatService:IChatService
{
    private readonly MessagesDB _messagesDb;
    

    public ChatService(MessagesDB messagesDb)
    {
        _messagesDb = messagesDb;
    }
    
    public List<MessageModel> LoadMessages(List<string> tags)
    {
        var messages = _messagesDb.Message.ToList();
        
        if (tags != null && tags.Count > 0)
        {
            messages = messages
                .Where(message => string.IsNullOrEmpty(message.Tags) || 
                                  message.Tags.Split(',').Any(tag => tags.Contains(tag)))
                .ToList();
        }
        else
        {
            messages = messages
                .Where(message => string.IsNullOrEmpty(message.Tags))
                .ToList();
        }
        return messages;
    }
    
}