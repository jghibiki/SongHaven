from setuptools import setup
from pip.req import parse_requirements

setup(name='SongHaven',
        version='0.0',
        install_requires=['flask', 'Jinja2', 'MarkupSafe', 'Pygments', 'Werkzueg', 'argparse', 'distribute', 'itsdangerous', 'peewee', 'wsgiref']
        )
