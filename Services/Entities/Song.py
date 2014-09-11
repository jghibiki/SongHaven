from peewee import *
from datetime import datetime
from BaseModel import BaseModel
from uuid import uuid4


class Song(BaseModel):
    id = CharField(primary_key=False)
    Title = CharField()
    Artist = CharField()
    Album = CharField()
    NumberOfPlays = IntegerField()
    CreatedDate = DateTimeField()
    UpdatedDate = DateTimeField()

    @staticmethod
    def NewSong( _title, _artist, _album):
        song = Song()
        song.id = str(uuid4())
        song.Title = _title
        song.Artist = _artist
        song.Album = _album
        song.NumberOfPlays = 0
        song.CreatedDate = datetime.utcnow()
        song.UpdatedDate = datetime.utcnow()
        return song



