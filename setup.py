from setuptools import setup
from pip.req import parse_requirements

setup(name='SongHaven',
        version='0.0',
        install_requires=["flask", "jinja2", "markupsafe", "pygments", "werkzeug", "argparse", "distribute", "docutils", "itsdangerous", "peewee", "wsgiref"]
        )
