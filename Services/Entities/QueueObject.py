from peewee import *
from datetime import datetime
import sys, os
from Song import Song
from BaseModel import BaseModel


class QueueObject(BaseModel):
    id = IntegerField(primary_key=False)
    SongId = CharField()
    CreatedDate = DateTimeField()

    @staticmethod
    def newInstance(_Song):
        queueObject = QueueObject()
        queueObject.SongId = Song.id
        queueObject.CreatedDate = datetime.utcnow()
        return queueObject

