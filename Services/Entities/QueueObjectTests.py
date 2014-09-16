import unittest
import sys
from . import QueueObject
from . import Song
from mock import patch
from peewee import *
from uuid import uuid4

class TestTypes(unittest.TestCase):

    def setUp(self):
        song = Song.Song()
        song.id = uuid4()
        self.ClassUnderTest = QueueObject.QueueObject.newInstance(song)
        self.ClassUnderTest.id = 123


    def test_EnsureQueuePositionIsTypeIntegerField(self):
        self.assertTrue(isinstance(self.ClassUnderTest.id, int))

    def test_EnsureSongIdIsTypeIntegerField(self):
        self.assertTrue(isinstance(self.ClassUnderTest.SongId, CharField))


if __name__ == '__main__':
    unittest.main()


