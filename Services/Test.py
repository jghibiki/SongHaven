from flask import Flask, request
from flask.ext import restful
from flask.ext.restful import reqparse, fields, marshal_with
from requests import put,get
from Entities.User import User
from Entities.UserToken import UserToken

class testRequestToken(restful.Resource):
    def get(self):
        return get('songhaven.ndacm.org:8080/auth/request-token/username/password')

