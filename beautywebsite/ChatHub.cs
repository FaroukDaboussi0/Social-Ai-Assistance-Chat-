using beautywebsite.Services;
using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
    private readonly UserService _userService;
    private readonly MessageService _messageService;
    public ChatHub(UserService us,MessageService ms){
        _userService=us;
        _messageService=ms;
    }
    public async Task SendMessageToChat(string chatname, string message)

    {   // Broadcast message to all clients
        await Clients.All.SendAsync("SendMessage",chatname,message);
        User chat = _userService.GetUserByName(chatname) ;
        Console.WriteLine(chat.Id);
        
        String response =await _messageService.Send(chat.Id,1,message);
        Console.WriteLine(response);
        // Broadcast message to all clients
        await Clients.All.SendAsync("ReceiveMessage", chatname, response);
    }
   
}
