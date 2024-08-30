using System;
using System.Reflection.Metadata.Ecma335;
using beautywebsite.Services;
namespace beautywebsite.Services;
public class MessageService
{
    private readonly MessagingDbContext _context ;
    private readonly ChatbotsService _chatbotsService;
    private readonly UserService _userservice;
  
    public MessageService(MessagingDbContext cx, UserService us,ChatbotsService cs){
        _chatbotsService=cs;
        _userservice=us;
        _context=cx;
    
    }
    public List<Message> GetMessagesByChat(int chatId)
    {
        return _context.Messages
            .Where(m => m.ReceiverId == chatId || m.SenderId == chatId)
            .OrderBy(m => m.Date)
            .ToList();
    }
    public string GetFormattedDate(DateTime datelastmessage)
{
    DateTime today = DateTime.Today;
    DateTime yesterday = today.AddDays(-1);

    if (datelastmessage.Date == today)
    {
        return "Today";
    }
    else if (datelastmessage.Date == yesterday)
    {
        return "Yesterday";
    }
    else if (datelastmessage.Date > yesterday && datelastmessage.Date < today)
    {
        // For other days, return the day name (e.g., Monday, Tuesday, etc.)
        return datelastmessage.ToString("dddd");
    }
    else
    {
        // Handle other cases or return the full date as needed
        return datelastmessage.ToString("MM/dd/yyyy");
    }
}
//-----------------------------------------------------
    public List<MessageViewModel> conversationsToView(){
        List<MessageViewModel> lf = new List<MessageViewModel>();
        for (int i = 3; i < 9; i++)
        {
        MessageViewModel m = new MessageViewModel();
        m.messageschat=GetMessagesByChat(i);
        m.chat=_userservice.getuserbyid(i);    
        if (m.messageschat.Any())
        {
            m.lastmsg=m.messageschat[m.messageschat.Count - 1].Content;
            m.datelastmessage = GetFormattedDate(m.messageschat.Max(msg => msg.Date));
        }
        else
        {
            m.lastmsg="";
            m.datelastmessage = "";
        }
        
        lf.Add(m);
        }
        return lf;
    }
 public async Task<String> Send(int ReceiverId,int SenderId,String Content){
   
    List<Message> ?messageschat=GetMessagesByChat(ReceiverId);
    String datelastmessage = messageschat.Any()
    ? GetFormattedDate(messageschat.Max(msg => msg.Date))
    : "";
    HttpMessageRequest request = new HttpMessageRequest(Content,messageschat,ReceiverId,datelastmessage);
        try
        {
            Message message=new Message(ReceiverId,SenderId,Content);
            // Save the incoming message
            _context.Messages.Add(message);
            _context.SaveChanges();
            
            
            // Call the external API to get a reply based on the message
            string reply = await _chatbotsService.GetReplyFromLocalAPI(request);
            Message reply_m = new Message(SenderId,ReceiverId,reply);
            // Save the reply
            _context.Messages.Add(reply_m);
            _context.SaveChanges();

             return reply;
        }
        catch (Exception ex)
        {
            // Handle exception or error case appropriately
            // Log the error, throw an exception, or return a default reply
            throw new Exception("Error occurred while processing the message: " + ex.Message);
        }
}
/**
public async Task<String> sendcodeto(String phonenumber){
  String pass= await _securityService.sendcodeto(phonenumber);
    
    return pass;
}
public async Task<String> getstatus(String phonenumber,String code){
return null;
}**/
}
