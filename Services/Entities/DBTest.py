from peewee import *
from Song import Song
from Request import Request
from User import User
from RecentPlay import RecentPlay
from UserClaim import UserClaim
from UserToken import UserToken

errors = []
warnings = []

#test songs
song = Song.newInstance(_title="title", _album="album", _artist="artist")
song.save(force_insert=True)

#test users
user = User.newInstance("username", "firstname", "lastname", "email", "password")
exception = user.save(force_insert=True)
if exception is not None:
    if exception[0] == "warning":
        warnings.append(exception[1])

#test requests
request = Request.newInstance(song.id, user.id)
request.save(force_insert=True)

#test recent play
recentPlay = RecentPlay.newInstance(user.id, song.id)
recentPlay.save(force_insert=True)

#test userClaim
userClaim = UserClaim.newInstance(user.id, "song.read")
exception = userClaim.save(force_insert=True)
if exception is not None:
    if exception[0] == "warning":
        warnings.append(exception[1])


#test user token
userToken = UserToken.newInstance(user.id)
userToken.save(force_insert=True)


print
print "Database Results:"
print "-----------------"
foundSongs =  Song.select()
print "Songs:"
for foundSong in foundSongs:
    print "\t" + foundSong.id
    print "\t\t" + "title = " + foundSong.title
    print "\t\t" + "artist = " + foundSong.artist
    print "\t\t" + "album = " + foundSong.album
    print "\t\t" + "number of plays = " + str(foundSong.numberOfPlays)



print
print "Requests:"
foundRequests= Request.select()
for foundRequest in foundRequests:
    print "\t" + foundRequest.id
    print "\t\t" + "SongId = " + foundRequest.songId
    print "\t\t" + "UserId = " + foundRequest.userId
    print "\t\t" + "createdDate = " + str(foundRequest.createdDate)

print
print "Users:"
foundUsers = User.select()
for foundUser in foundUsers:
    print "\t" + foundUser.id
    print "\t\t" + "username = " + foundUser.username
    print "\t\t" + "firstName = " + foundUser.firstName
    print "\t\t" + "lastName = " + foundUser.lastName
    print "\t\t" + "email = " + foundUser.email
    print "\t\t" + "createdDate = " + str(foundUser.createdDate)
    print "\t\t" + "accountStrikes = " + str(foundUser.accountStrikes)
    print "\t\t" + "dateBanned = " + str(foundUser.dateBanned)
    print "\t\t" + "timesBanned = " + str(foundUser.timesBanned)


print
print "RecentPlays:"
foundRecentPlays = RecentPlay.select()
for foundRecentPlay in foundRecentPlays:
    print "\t" + foundRecentPlay.id
    print "\t\t" + "userid = " + foundRecentPlay.userId
    print "\t\t" + "songid = " + foundRecentPlay.songId
    print "\t\t" + "createdDate = " + str(foundRecentPlay.createdDate)

print
print "UserClaims:"
foundUserClaims = UserClaim.select()
for foundUserClaim in foundUserClaims:
    print "\t" + foundUserClaim.id
    print "\t\t" + "userId = " + foundUserClaim.userId
    print "\t\t" + "claim = " + foundUserClaim.claim
    print "\t\t" + "createdDate = " + str(foundUserClaim.createdDate)


print
print "User Tokens"
foundUserTokens = UserToken.select()
for foundUserToken in foundUserTokens:
    print "\t" + foundUserToken.id
    print "\t\t" + "userId = " + foundUserToken.userId
    print "\t\t" + "createdate" + str(foundUserToken.createdDate)
    print "\t\t" + "createdDate" + str(foundUserToken.expirationDate)


print
print "---------------------------------"
print str(len(warnings)) + " Warnings Encountered"
print "---------------------------------"

for item in warnings:
    print "Warning: " + item
    print


print
print "---------------------------------"
print str(len(errors)) + " Errors Encountered"
print "---------------------------------"

for item in errors:
    print "Error: " + item
    print

