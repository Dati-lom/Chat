using Chat.Context;
using Chat.Hub;
using Chat.Services;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddResponseCompression(opt =>
    opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" })
);
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://chat-dat-lom", "https://*.chat-dat-lom.netlify.app", "https://chat-dat-lom", "https://chat-dat-lom.netlify.app","http://localhost:3000").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    });
});
builder.Services.AddDbContext<MessagesDB>();

builder.Services.AddTransient<IChatService, ChatService>();

var app = builder.Build();

app.UseResponseCompression();





// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseCors();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/chathub");

app.Run();