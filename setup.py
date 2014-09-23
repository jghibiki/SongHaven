from setuptools import setup
from pip.req import parse_requirements

install_reqs = parse_requiremetns("requirements.txt")
reqs = [str(ir.req) for ir in install_reqs]


setup(name='SongHaven',
        version='0.0',
        install_requires=reqs
        )
