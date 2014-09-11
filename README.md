SongHaven
=========

An audio controller for playing music on a host machine and controlling via web clients

CI Build:
=========

[![Build Status](https://travis-ci.org/jghibiki/SongHaven.svg?branch=master)](https://travis-ci.org/jghibiki/SongHaven)


Directory Structure:
====================
    .
    |-README.md
    |-AuthServer/
    |-IdentityServer/
    |-Web/
    | |-index.html
    | |-assets/
    | |-js/
    |-Services/
    | |-Users/
    | | |-Production/
    | | | |-User.py
    | | | |-UserManagementService.py
    | | |-UnitTests/
    | | | |-UserTests.py
    | | | |-UserManagementServiceTests.py
    | |-Songs/
    | | |-Production/
    | | | |-Song.py
    | | | |-Request.py
    | | | |-Queue.py
    | | | |-UploadManagementService.py
    | | | |-PlaylistManagementService.py
    | | | |-SongManagementService.py
    | | |-UnitTests/
    | | | |-SongTests.py
    | | | |-RequestTests.py
    | | | |-QueueTests.py
    | | | |-UploadMangementServiceTests.py
    | | | |-PlaylistManagementServiceTests.py
    | | | |-SongManagementServiceTests.py
