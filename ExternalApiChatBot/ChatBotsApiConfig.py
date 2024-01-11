


from datetime import datetime
import os

from fastapi import FastAPI
from openai import OpenAI
from pydantic import  BaseModel
from typing import List

app2 = FastAPI()




class Message(BaseModel):
  
    Id: int
    SenderId: int
    ReceiverId: int  
    Content: str
    Thread: int 
    Date: datetime 


class HttpMessageRequest(BaseModel):

   
    messageschat: List[Message]
    chatid: int
    datelastmessage: str
    lastmsg: str


def get_name_from_id(user_id: int) -> str:
    if user_id == 1:
        return "user"
    else:
        return "you"


def to_readable_msg(messages: List[Message]) -> str:
    string_messages = ""
    for message in messages:
        sender_name = get_name_from_id(message.SenderId)
        string_messages += f"{sender_name}: {message.Content} || "
    return string_messages
def get_context_from_chatID(chatID: int) -> str:
    if chatID == 3:
        return "Guide"
    elif chatID == 4:
        return "Therapist"
    elif chatID == 5:
        return "Financial Adviso"
    elif chatID == 6:
        return "Career Coach"
    elif chatID == 7:
        return "Wellness Expert"
    elif chatID == 8:
        return "Teacher"
    else:
        return "unknown"
print(os.environ.get('OPENAI_API_KEY'))
def generateresponse(context :str , messages:str,lastmsg:str,lastdate:str) -> str:
    client = OpenAI()
    
    completion = client.chat.completions.create(
    model="gpt-3.5-turbo",
    messages=[
        {"role": "system", "content": f"you are a  :{context}/n you are in conversation now ; complete it :  this is the previous conversaiton  {messages}  now you have capability to remember specific conversations /n and  this is the date of the last time we talked : {lastdate}  "  },
        {"role": "user", "content": f"{lastmsg}"}
    ]
    )
    return completion.choices[0].message.content 



@app2.post("/api/endpoint")
async def process_request(request_data: HttpMessageRequest):
    # Access the received data
    received_chat_id = request_data.chatid
    string_messages = to_readable_msg(request_data.messageschat)
    context = get_context_from_chatID(request_data.chatid)
    response=generateresponse(context,string_messages,request_data.lastmsg,request_data.datelastmessage)
    
    return  response
