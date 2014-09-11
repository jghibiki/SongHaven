from peewee import *

db = MySQLDatabase(database="SongHaven", user="SongHavenUsr", passwd="mary1234")

class BaseModel(Model):
    class Meta:
        database = db
