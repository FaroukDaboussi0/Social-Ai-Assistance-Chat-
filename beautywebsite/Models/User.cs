using System.Runtime.Serialization;
using System.Text.Json.Serialization;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string imagepath { get; set; } 
    [JsonIgnore()]
    [IgnoreDataMember]
    public string secondname{get;set;}

    [JsonIgnore()]
    [IgnoreDataMember]
    public ICollection<Message> ?SentMessages { get; set; }
    
    [JsonIgnore()]
    [IgnoreDataMember]
  
    public ICollection<Message> ?ReceivedMessages { get; set; }
   
}
