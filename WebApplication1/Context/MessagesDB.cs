using Chat.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Context;

public class MessagesDB : DbContext
{
    private readonly IConfiguration _configuration;

    public MessagesDB(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
    }

    public DbSet<MessageModel> Message{ get; set; }
}