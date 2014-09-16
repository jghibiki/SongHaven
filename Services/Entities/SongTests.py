import unittest
from . import Song

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


if __name__ == "__main__":
    unittest.main()
