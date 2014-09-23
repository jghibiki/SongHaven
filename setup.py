from setuptools import setup
import os

with open("requirements.txt") as f:
    required = f.read().splitlines()

setup(name='SongHaven',
        version='0.0',
        install_requires=required
        )
