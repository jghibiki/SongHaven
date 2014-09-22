from peewee import *
from datetime import datetime, timedelta
from BaseModel import BaseModel
from uuid import uuid4

class RecentPlay(BaseModel):
    id = CharField(primary_key=True)
    userId = CharField()
    songId = CharField()
    createdDate = DateTimeField(default=datetime.utcnow)



    #static methods
    @staticmethod
    def newInstance(_userid, _songid):
        play = RecentPlay()
        play.id = str(uuid4())
        play.userId = _userid
        play.songId = _songid
        return play


