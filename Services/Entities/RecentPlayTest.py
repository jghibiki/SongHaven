import unittest
from . import RecentPlay


class TestRecentPlay(unittest.TestCase):

    def test_idIsNotNone(self):
        play = RecentPlay.RecentPlay.newInstance(_userid="", _songid="")
        self.assertIsNotNone(play.id)

    def test_userId(self):
        expected = "test userid"
        play = RecentPlay.RecentPlay.newInstance(_userid=expected, _songid="")
        actual = play.userId
        self.assertTrue(actual == expected)

    def test_songId(self):
        expected = "test songid"
        play = RecentPlay.RecentPlay.newInstance(_userid="", _songid=expected)
        actual = play.songId
        self.assertTrue(actual == expected)
