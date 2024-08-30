


from datetime import datetime
import os

from fastapi import FastAPI

from pydantic import  BaseModel
from typing import List

import uvicorn

from data.HttpMessageRequest import HttpMessageRequest

from data.Message import Message
from gemini_api import bard

app2 = FastAPI()







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
        return "Financial Advisor"
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
    general_context = f"you are a  :{context}/n you are in conversation now ; complete it :  this is the previous conversaiton  {messages}  now you have capability to remember specific conversations /n and  this is the date of the last time we talked : {lastdate}  "  
    message = f"user last new message : {lastmsg}"
    task = 'continue the chat with the user , be as much as you can humain '
    outputformat = 'output format : only your response **no extra words**  , dont return long boring message '
    prompt = general_context + message +  task + outputformat
    r = bard(prompt).replace("\n", "").replace("\\", "")


    return r



@app2.post("/api/endpoint")
async def process_request(request_data: HttpMessageRequest):
    # Access the received data
    received_chat_id = request_data.chatid
    string_messages = to_readable_msg(request_data.messageschat)
    context = get_context_from_chatID(request_data.chatid)
    response=generateresponse(context,string_messages,request_data.lastmsg,request_data.datelastmessage)
    print(response)
    return  response


def main():
    uvicorn.run("ChatBotsApiConfig:app2", host="127.0.0.1", port=8000, reload=True)

if __name__ == "__main__":
    main()