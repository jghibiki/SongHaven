from peewee import *
from datetime import datetime,timedelta
from BaseModel import BaseModel
from uuid import uuid4

class User(BaseModel):
    """
    A class encapsulating a user in the system.
    """
    id = CharField(primary_key=True)
    username = CharField(unique=True)
    firstName = CharField()
    lastName = CharField()
    email = CharField()
    createdDate = DateTimeField(default=datetime.utcnow)
    accountStrikes = IntegerField()
    dateBanned = DateTimeField(null=True, default=None)
    timesBanned = IntegerField()
    password = CharField()

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


