from peewee import *
from datetime import datetime
from Song import Song
from BaseModel import BaseModel
from uuid import uuid4


class Request(BaseModel):
    id = CharField(primary_key=True)
    songId = CharField()
    userId = CharField()
    createdDate = DateTimeField(default=datetime.utcnow)

    @staticmethod
    def newInstance(_songid, _userid):
        request= Request()
        request.id = str(uuid4())
        request.songId = _songid
        request.userId = _userid
        return request

