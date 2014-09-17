from peewee import *
from datetime import datetime,timedelta
from BaseModel import BaseModel
from uuid import uuid4

class User(BaseModel):
    id = CharField(primary_key=False)
    username = CharField()
    firstName = CharField()
    lastName = CharField()
    email = CharField()
    cretedDate = DateTimeField()
    accountStrikes = IntegerField()
    dateBanned = DateTimeField()
    timesBanned = IntegerField()
    password = CharField()


    #static methods
    @staticmethod
    def newInstance(_username, _firstName, _lastName, _email, _password):
        user = User()
        user.id = str(uuid4())
        user.username = _username
        user.firstName = _firstName
        user.lastName = _lastName
        user.email = _email
        user.password = _password
        user.createdDate = datetime.utcnow()
        user.accountStrikes = 0
        user.dateBanned = None
        user.timesBanned = 0
        return user

