import unittest
import os

os.path.append("../Production/")
os.path

#if nose is probing this then
#we want it to run the tests
if __name__ == '__main__':
    unittest.main()

class TestTypes(unittest.TestCase):

    def setUp(self):
        ClassUnderTest = QueueObject(QueuePosition=0, SongId=0)


    def EnsureQueuePositionIsTypeIntegerField(self):
        assert ClassUnderTest.QueuePosition is IntegerField

    def EnsureSongIdIsTypeIntegerField(self):
        assert ClassUnderTest.SongId is IntegerField
