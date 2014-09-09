from peewee import *
import sys

sys.path.append("../../Database/")
from BaseModel import BaseModel

from Song import Song


class QueueObject(BaseModel):
    QueuePosition = IntegerField(PrimaryKeyField=True)
    SongId = IntegerField()
    CreatedDate = DateTimeField()

