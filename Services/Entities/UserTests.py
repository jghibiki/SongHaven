import unittest
from . import User
from datetime import datetime, timedelta
from peewee import DateTimeField

class TestUser(unittest.TestCase):

    def test_idIsNotNone(self):
        user = User.User.newInstance(_username="", _firstName="", _lastName="", _email="", _password="")
        self.assertIsNotNone(user.id)

    def test_userName(self):
        expected = "test username"
        user = User.User.newInstance(_username=expected, _firstName="", _lastName="", _email="", _password="")
        actual = user.username
        self.assertTrue(actual == expected)

    def test_firstName(self):
        expected = "test firstName"
        user = User.User.newInstance(_username="", _firstName=expected, _lastName="", _email="", _password="")
        actual = user.firstName
        self.assertTrue(actual == expected)

    def test_lastName(self):
        expected = "test lastName"
        user = User.User.newInstance(_username="", _firstName="", _lastName=expected, _email="", _password="")
        actual = user.lastName
        self.assertTrue(actual == expected)

    def test_email(self):
        expected = "test email"
        user = User.User.newInstance(_username="", _firstName="", _lastName="", _email=expected, _password="")
        actual = user.email
        self.assertTrue(actual == expected)

    def test_password(self):
        expected = "test password"
        user = User.User.newInstance(_username="", _firstName="", _lastName="", _email="", _password=expected)
        actual = user.password
        self.assertTrue(actual == expected)


if __name__ == "__main__":
    unittest.main()

