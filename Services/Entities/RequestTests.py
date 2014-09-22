import unittest
import sys
from . import Request
from . import Song
from mock import patch
from peewee import *
from uuid import uuid4

class TestTypes(unittest.TestCase):

    def setUp(self):
        self.expectedSong = "abcdefg"
        self.expectedUser = "xyz"
        self.ClassUnderTest = Request.Request.newInstance(self.expectedSong, self.expectedUser)

    def test_idIsNotNone(self):
        self.assertIsNotNone(self.ClassUnderTest.id)

    def test_songIdIsNotNone(self):
        self.assertIsNotNone(self.ClassUnderTest.songId)

    def test_songIdIsExpected(self):
        self.assertEqual(self.ClassUnderTest.songId, self.expectedSong)

    def test_userIdIsExpected(self):
        self.assertEqual(self.ClassUnderTest.userId, self.expectedUser)

    def test_createdDateIsNotnone(self):
        self.assertIsNotNone(self.ClassUnderTest.createdDate)


if __name__ == '__main__':
    unittest.main()


