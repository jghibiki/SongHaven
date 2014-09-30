import peewee
from peewee import *
from Song import Song
from Request import Request
from User import User
from RecentPlay import RecentPlay
from UserClaim import UserClaim
from UserToken import UserToken
db = MySQLDatabase("SongHaven", user="SongHavenUsr", passwd="mary1234")

peewee.drop_model_tables([Song, Request, User, RecentPlay, UserClaim, UserToken])
peewee.create_model_tables([Song, Request, User, RecentPlay, UserClaim, UserToken])
