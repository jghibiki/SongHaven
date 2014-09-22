import unittest
from datetime import timedelta
from . import UserToken

class UserTokenTests(unittest.TestCase):

    def setUp(self):
        self.ClassUnderTest = UserToken.UserToken.newInstance("test id")

    def test_ensureId(self):
        self.assertIsNotNone(self.ClassUnderTest.id)

    def test_ensureUserId(self):
        self.assertIsNotNone(self.ClassUnderTest.userId)

    def test_ensureCreatedDate(self):
        self.assertIsNotNone(self.ClassUnderTest.createdDate)

    def test_ensureExpirationDate(self):
        self.assertIsNotNone(self.ClassUnderTest.expirationDate)
        self.assertEqual(self.ClassUnderTest.expirationDate, self.ClassUnderTest.createdDate + timedelta(minutes=60))
