from peewee import *
import os

os.path.append("../../Database/")
from BaseModel import BaseModel

from Song import Song


class QueueObject(BaseModel):
    QueuePosition = IntegerField(PrimaryKeyField=True)
    SongId = IntegerField()
    CreatedDate = DateTimeField()

