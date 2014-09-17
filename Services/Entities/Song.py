from peewee import *
from datetime import datetime, timedelta
from BaseModel import BaseModel
from uuid import uuid4


class Song(BaseModel):
    id = CharField(primary_key=False)
    Title = CharField()
    Artist = CharField()
    Album = CharField()
    NumberOfPlays = IntegerField()
    CreatedDate = DateTimeField()
    LastPlayedDate = DateTimeField()


    @staticmethod
    def newInstance( _title, _artist, _album):
        song = Song()
        song.id = str(uuid4())
        song.Title = _title
        song.Artist = _artist
        song.Album = _album
        song.NumberOfPlays = 0
        song.CreatedDate = datetime.utcnow()
        song.LastPlayedDate = datetime.utcnow()
        return song



    def isPlayable(self):
        elapsedTime = self.LastPlayedDate - datetime.utcnow()

        if(elapsedTime >= timedelta(minutes=30)):
            return True
        else:
            return False

    def markPlayed(self):
        self.LastPlayedDate = datetime.utcnow()

