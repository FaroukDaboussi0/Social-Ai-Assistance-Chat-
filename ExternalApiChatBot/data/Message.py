
from datetime import datetime

from pydantic import BaseModel


class Message(BaseModel):
  
    Id: int
    SenderId: int
    ReceiverId: int  
    Content: str
    Thread: int 
    Date: datetime 
