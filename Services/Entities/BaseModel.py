import MySQLDataBase
from peewee import *

db = MySQLDataBase("SongHaven")

class BaseModel(Model):
    class Meta:
        database = db
