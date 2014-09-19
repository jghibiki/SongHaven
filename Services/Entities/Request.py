from peewee import *
from datetime import datetime
from Song import Song
from BaseModel import BaseModel
from uuid import uuid4


class Request(BaseModel):
    id = CharField(primary_key=True)
    songId = CharField()
    createdDate = DateTimeField()

    @staticmethod
    def newInstance(_songid):
        queueObject = Request()
        queueObject.id = str(uuid4())
        queueObject.songId = _songid
        queueObject.createdDate = datetime.utcnow()
        return queueObject

