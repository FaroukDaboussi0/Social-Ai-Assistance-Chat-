using System;
public class HttpMessageRequest
{
   public int Id { get; private set; }
  public List<Message> ?messageschat;
  public int chatid;
  public String datelastmessage;
  public String lastmsg;
    
    public HttpMessageRequest(String content, List<Message> messageschat,int chatID, String datelastmessage){
        this.lastmsg=content;
        this.messageschat=messageschat;
        this.chatid = chatID;
        this.datelastmessage=datelastmessage;
    }
    
}