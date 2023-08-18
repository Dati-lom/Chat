using Chat.Context;
using Chat.Models;

namespace Chat.Services;

public interface IChatService
{
    public List<MessageModel> LoadMessages(List<string> tags);
    
}