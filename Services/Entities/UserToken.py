from peewee import *
from datetime import datetime, timedelta
from BaseModel import BaseModel
from uuid import uuid4

class UserToken:
    id = CharField(primary_key=True)
    userId = CharField()
    createdDate = DateTimeField()
    expirationDate = DateTimeField()


    #static methods
    @staticmethod
    def newInstance(_userId):
        userToken = UserToken()
        userToken.id = str(uuid4())
        userToken.userId = _userId
        userToken.createdDate = datetime.utcnow()
        userToken.expirationDate = userToken.createdDate + timedelta(minutes=60)
        return userToken
