import unittest
from . import Song
from datetime import datetime, timedelta
from peewee import DateTimeField

class TestSong(unittest.TestCase):

    def test_idIsNotNone(self):
        song = Song.Song.newInstance(_title="", _album="", _artist="")
        self.assertIsNotNone(song.id)

    def test_title(self):
        expected = "test_title"
        song = Song.Song.newInstance(_title=expected, _album="", _artist="")
        self.assertTrue(song.title == expected)

    def test_album(self):
        expected = "test_album"
        song = Song.Song.newInstance(_title="", _album=expected, _artist="")
        self.assertTrue(song.album == expected)

    def test_artist(self):
        expected = "test_artist"
        song = Song.Song.newInstance(_title="", _album="", _artist=expected)
        self.assertTrue(song.artist == expected)


if __name__ == "__main__":
    unittest.main()
