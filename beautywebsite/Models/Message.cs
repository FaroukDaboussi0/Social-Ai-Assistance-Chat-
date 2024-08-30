using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

public class Message 
{
    public int Id { get; set; }
    
    public int SenderId { get; set; }
    [JsonIgnore()]
    [IgnoreDataMember]
    public User Sender { get; set; }
   
    public int ReceiverId { get; set; }
    [JsonIgnore()]
    [IgnoreDataMember]
    public User Receiver { get; set; }

    
    public string Content { get; set; }
    public int Thread { get; set; } =1;
    public DateTime Date { get; set; } = DateTime.Now;
    public Message(int ReceiverId,int SenderId,String Content){
       this.ReceiverId=ReceiverId;
       this.SenderId=SenderId;
       this.Content=Content;
     
    }
      
}
