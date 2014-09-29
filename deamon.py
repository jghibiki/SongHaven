from flask import Flask
from flask.ext import restful
from Services.AuthenticationService import *

app = Flask("SongHaven")
api = restful.Api(app)

#authentication service endpoints
api.add_resource(requestToken, '/auth/request-token/get/<string:username>/<string:password>', endpoint="request-token-get")
api.add_resource(requestToken, '/auth/request-token/post/', endpoint="request-token-post")
api.add_resource(shutdownAuth, '/auth/shutdown/')



if __name__ == "__main__":
    app.run(debug=True, host='0.0.0.0', port=8080)
