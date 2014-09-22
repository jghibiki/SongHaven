from peewee import *
from datetime import datetime
from BaseModel import BaseModel
from uuid import uuid4


class Song(BaseModel):
    id = CharField(primary_key=True)
    title = CharField()
    artist = CharField()
    album = CharField()
    numberOfPlays = IntegerField()
    createdDate = DateTimeField(default=datetime.utcnow)
    lastPlayedDate = DateTimeField(default=datetime.utcnow)


    @staticmethod
    def newInstance( _title, _artist, _album):
        song = Song()
        song.id = str(uuid4())
        song.title = _title
        song.artist = _artist
        song.album = _album
        song.numberOfPlays = 0
        return song



