import unittest
import sys
from . import Request
from . import Song
from mock import patch
from peewee import *
from uuid import uuid4

class TestTypes(unittest.TestCase):

    def setUp(self):
        self.expected = "abcdefg"
        self.ClassUnderTest = Request.Request.newInstance(self.expected)

    def test_idIsNotNone(self):
        self.assertIsNotNone(self.ClassUnderTest.id)

    def test_songIdIsNotNone(self):
        self.assertIsNotNone(self.ClassUnderTest.songId)

    def test_songIdIsExpected(self):
        self.assertTrue(self.ClassUnderTest.songId == self.expected)

    def test_createdDateIsNotnone(self):
        self.assertIsNotNone(self.ClassUnderTest.createdDate)


if __name__ == '__main__':
    unittest.main()


