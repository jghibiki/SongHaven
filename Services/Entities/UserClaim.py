from peewee import *
from datetime import datetime
from BaseModel import BaseModel
from uuid import uuid4

class UserClaim(BaseModel):
    id = CharField(primary_key=False)
    userId = CharField()
    claim = CharField()
    createdDate = DateTimeField()

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
    def newInstance(_userid, _claim):
        userClaim = UserClaim()
        userClaim.id = str(uuid4())
        userClaim.userId = _userid
        userClaim.claim = _claim
        userClaim.createdDate = datetime.utcnow()
        return userClaim
