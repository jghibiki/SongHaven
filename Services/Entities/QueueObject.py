from peewee import *
import sys, os

sys.path.insert(0,os.path.abspath("../../Database/"))
from BaseModel import BaseModel

from Song import Song


class QueueObject(BaseModel):
    QueuePosition = IntegerField(PrimaryKeyField=True)
    SongId = IntegerField()
    CreatedDate = DateTimeField()

