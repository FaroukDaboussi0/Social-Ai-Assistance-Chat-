using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using beautywebsite.Models;
using beautywebsite.Services;
namespace beautywebsite.Controllers;
public class ChatController : Controller
{
    private readonly MessageService messageService;
    
    public ChatController(MessageService ms){
        messageService=ms;
    }

  
    public IActionResult messagerie()
    { List<MessageViewModel> messageViewModel = messageService.conversationsToView();
       
        return View(messageViewModel);
    }
    

   
}