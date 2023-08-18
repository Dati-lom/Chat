using Chat.Context;
using Chat.Models;
using Chat.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController:ControllerBase
{

    private readonly IConfiguration _configuration;
    private readonly IChatService _chatService;

    public ChatController(IConfiguration configuration, IChatService chatService)
    {
        _configuration = configuration;
        _chatService = chatService;
    }
    

    [HttpPost("filter")]
    public ActionResult<List<MessageModel>> GetMessages([FromBody]List<string> tags)
    {
       var res = _chatService.LoadMessages(tags);
       if (res == null) return BadRequest("Returned Empty (most likely)");
       
        return Ok(res);
    }

}