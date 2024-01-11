using System;
public class MessageViewModel
{
  private static int latestId = 0;

  public int Id { get; private set; }
  public List<Message> ?messageschat;
  public User chat;
  public String datelastmessage;
  public String lastmsg;
 
public MessageViewModel()
    {   if (latestId==6) //6 is the number of chatbots
            {Id=1;
            latestId=1;
            }
            else{
              
   
        Id = ++latestId;  }// Increment latestId and assign it to the Id property
        messageschat = new List<Message>();
        
    }
}