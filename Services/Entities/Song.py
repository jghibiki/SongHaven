from peewee import *
from datetime import datetime
from BaseModel import BaseModel
from uuid import uuid4


class Song(BaseModel):
    """
    An object encapsulating a song.
    """
    id = CharField(primary_key=True)
    title = CharField()
    artist = CharField()
    album = CharField()
    numberOfPlays = IntegerField()
    createdDate = DateTimeField(default=datetime.utcnow)
    lastPlayedDate = DateTimeField(default=datetime.utcnow)


    def save(self, force_insert=False):
        """
        Saves the object to the database.

        Args:
            ``force_insert=False``
                Determines if we should forceibly insert an object to the database. This sort of behavior may be needed if we are manually assigning object IDs instead of letting peewee do it for us.
        """
        shouldForceInsert = force_insert
        super(BaseModel, self).save(force_insert=shouldForceInsert)



    @staticmethod
    def newInstance( _title, _artist, _album):
        """
        Creates a new Song instance.

        Args:
            ``_title``
                title of the song
            ``_artist``
                name of the artist who produced the song
            ``_album``
                name of the album that the song is from

        Returns:
            a new Song object
        """
        song = Song()
        song.id = str(uuid4())
        song.title = _title
        song.artist = _artist
        song.album = _album
        song.numberOfPlays = 0
        return song



