from flask import Flask
from flask.ext import restful
from flask.ext.restful import reqparse, fields, marshal_with
from requests import put,get
from Entities.User import User
from Entities.UserToken import UserToken

class requestToken(restful.Resource):
    def __init__(self):
        self.reqparse = reqparse.RequestParser()
        self.reqparse.add_argument('username', type=str, location = 'json', required=True)
        self.reqparse.add_argument('password', type=str, location = 'json', required=True)


    def get(self, username, password):
        if(username == None):
            return {"error" : "None Parameter - username"}
        if password == None:
            return {"error" : "None Paremeter - password"}

        result = createToken(username, password)

        if result[0] == "success":
            return { "userToken" : result[1] }
        elif result[0] == "error":
            return { "error" : result[1] }
        elif result[0] == "warning":
            return { "warning" : result[1] }
        else:
            return None

    def post(self):
        args = self.reqparse.parse_args()
        username = args.username
        password = args.password

        result = self.createToken(username, password)

        if result[0] == "success":
            return { "userToken" : result[1] }
        elif result[0] == "error":
            return { "error" : result[1] }
        elif result[0] == "warning":
            return { "warning" : result[1] }
        else:
            return None


    def createToken(self, username, password):
        try:
            user = User.get(User.username == username, User.password==password)
            userid = user.id

            userToken = UserToken.newInstance(userid)
            returnStatus =  userToken.save(force_insert=True)
            if returnStatus is not None:
                return returnStatus
            return ("success", userToken.id)

        except:
            return ("error", "Username and password combination could not be verified.")


