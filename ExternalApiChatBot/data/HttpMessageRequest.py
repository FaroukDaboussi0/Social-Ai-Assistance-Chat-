
from typing import List
from pydantic import BaseModel
from data.Message import Message

class HttpMessageRequest(BaseModel):

   
    messageschat: List[Message]
    chatid: int
    datelastmessage: str
    lastmsg: str