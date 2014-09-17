from peewee import *
from datetime import datetime
from BaseModel import BaseModel
from uuid import uuid4

class UserClaim(BaseModel):
    id = CharField(primary_key=False)
    userId = CharField()
    claim = CharField()
    createdDate = DateTimeField()


    #static methods
    @staticmethod
    def newInstance(_userid, _claim):
        userClaim = UserClaim()
        userClaim.id = str(uuid4())
        userClaim.userId = _userid
        userClaim.claim = _claim
        userClaim.createdDate = datetime.utcnow()
        return userClaim
