from peewee import *
from datetime import datetime, timedelta
from BaseModel import BaseModel
from uuid import uuid4

class UserToken(BaseModel):
    id = CharField(primary_key=True, unique=True)
    userId = CharField(unique=True)
    createdDate = DateTimeField()
    expirationDate = DateTimeField()

    def save(self, force_insert=False):
        """
        Saves the object to the database.

        Args:
            - force_insert=False
                Determines if we should forceibly insert an object to the database. This sort of behavior may be needed if we are manually assigning object IDs instead of letting peewee do it for us.

        """
        shouldForceInsert = force_insert
        try:
            super(BaseModel, self).save(force_insert=shouldForceInsert)
        except IntegrityError, details:
            return ("warning", details[1])


    #static methods
    @staticmethod
    def newInstance(_userId):
        userToken = UserToken()
        userToken.id = str(uuid4())
        userToken.userId = _userId
        userToken.createdDate = datetime.utcnow()
        userToken.expirationDate = userToken.createdDate + timedelta(minutes=60)
        return userToken
