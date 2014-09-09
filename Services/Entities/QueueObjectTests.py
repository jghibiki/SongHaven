import unittest
import sys

sys.path.append("../Production/QueueObject.py")
import QueueObjects

#if nose is probing this then
#we want it to run the tests
if __name__ == '__main__':
    unittest.main()

class TestTypes(unittest.TestCase):

    def setUp(self):
        ClassUnderTest = QueueObject(QueuePosition=0, SongId=0)

    def EnsureQueuePositionIsTypeIntegerField(self):
        self.assertTrue(ClassUnderTest.QueuePosition is IntegerField)

    def EnsureSongIdIsTypeIntegerField(self):
        self.assertTrue(ClassUnderTest.SongId is IntegerField)
