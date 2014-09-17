import unittest
from . import Song
from datetime import datetime, timedelta
from peewee import DateTimeField

class TestNewSong(unittest.TestCase):

    def test_idIsNotNone(self):
        song = Song.Song.newInstance(_title="", _album="", _artist="")
        self.assertIsNotNone(song.id)

    def test_title(self):
        expected = "test_title"
        song = Song.Song.newInstance(_title=expected, _album="", _artist="")
        self.assertTrue(song.Title == expected)

    def test_album(self):
        expected = "test_album"
        song = Song.Song.newInstance(_title="", _album=expected, _artist="")
        self.assertTrue(song.Album == expected)

    def test_artist(self):
        expected = "test_artist"
        song = Song.Song.newInstance(_title="", _album="", _artist=expected)
        self.assertTrue(song.Artist == expected)

    def test_markPlayed(self):
        song = Song.Song.newInstance(_title="", _album="", _artist="")
        self.assertTrue(song.LastPlayedDate != None)
        currentLastPlayedDate = song.LastPlayedDate
        song.markPlayed()
        self.assertTrue(song.LastPlayedDate != currentLastPlayedDate)

    def test_isPlayableReturnsFalseIfTimeIsWithin30Min(self):
        expected = False
        song = Song.Song.newInstance(_title="", _album="", _artist="")
        self.assertTrue(song.LastPlayedDate != None)
        #time should be utc now + 1 minute, which would leave us within the 30 minute limit
        song.LastPlayedDate = datetime.utcnow() + timedelta(minutes=1)
        actual = song.isPlayable()
        self.assertTrue(actual == expected)

    def test_isPlayableReturnsTrueIfTimeIsOutsideOf30Min(self):
        expected = True
        song = Song.Song.newInstance(_title="", _album="", _artist="")
        self.assertTrue(song.LastPlayedDate != None)
        #time should be utc now + 35 minutes, which would leave us outside of the 30 minute limit
        song.LastPlayedDate = datetime.utcnow() + timedelta(minutes=35)
        actual = song.isPlayable()
        self.assertTrue(actual == expected)



if __name__ == "__main__":
    unittest.main()
