from peewee import *
from datetime import datetime,timedelta
from BaseModel import BaseModel
from uuid import uuid4

class User(BaseModel):
    """
    A class encapsulating a user in the system.
    """
    id = CharField(primary_key=True)
    username = CharField()
    firstName = CharField()
    lastName = CharField()
    email = CharField()
    createdDate = DateTimeField(default=datetime.utcnow)
    accountStrikes = IntegerField()
    dateBanned = DateTimeField(null=True, default=None)
    timesBanned = IntegerField()
    password = CharField()


    #static methods
    @staticmethod
    def newInstance(_username, _firstName, _lastName, _email, _password):
        """
        Creates a new instance of a User.
        """
        user = User()
        user.id = str(uuid4())
        user.username = _username
        user.firstName = _firstName
        user.lastName = _lastName
        user.email = _email
        user.password = _password
        user.dateBanned = None
        user.accountStrikes = 0
        user.dateBanned = None
        user.timesBanned = 0
        return user

