import unittest
from . import UserClaim

class TestUserClaim(unittest.TestCase):

    def test_idIsNotNone(self):
        userClaim = UserClaim.UserClaim.newInstance(_userid="", _claim="")
        self.assertIsNotNone(userClaim.id)

    def test_userId(self):
        expected = "test userId"
        userClaim = UserClaim.UserClaim.newInstance(_userid=expected, _claim="")
        actual = userClaim.userId
        self.assertTrue(actual == expected)

    def test_claim(self):
        expected = "test.claim"
        userClaim = UserClaim.UserClaim.newInstance(_userid="", _claim=expected)
        actual = userClaim.claim
        self.assertTrue(actual == expected)
