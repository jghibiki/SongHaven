
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/24/2014 18:11:47
-- Generated from EDMX file: C:\Users\jghibiki\Source\Repos\songhaven\SongHaven\SongHaven\SongHaven.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SongHaven];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Request__fk_song__31EC6D26]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Requests] DROP CONSTRAINT [FK__Request__fk_song__31EC6D26];
GO
IF OBJECT_ID(N'[dbo].[FK_Request__fk_user]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Requests] DROP CONSTRAINT [FK_Request__fk_user];
GO
IF OBJECT_ID(N'[dbo].[FK__Messages__fk_use__49C3F6B7]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK__Messages__fk_use__49C3F6B7];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Requests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Requests];
GO
IF OBJECT_ID(N'[dbo].[Songs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Songs];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Commands]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Commands];
GO
IF OBJECT_ID(N'[dbo].[Messages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Messages];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Requests'
CREATE TABLE [dbo].[Requests] (
    [guid_id] uniqueidentifier  NOT NULL,
    [fk_song] uniqueidentifier  NOT NULL,
    [fk_user] uniqueidentifier  NOT NULL,
    [dt_created_date] datetime  NOT NULL
);
GO

-- Creating table 'Songs'
CREATE TABLE [dbo].[Songs] (
    [guid_id] uniqueidentifier  NOT NULL,
    [nvc_title] nvarchar(120)  NOT NULL,
    [nvc_album] nvarchar(120)  NOT NULL,
    [nvc_artist] nvarchar(120)  NOT NULL,
    [int_number_of_plays] int  NOT NULL,
    [dt_created_date] datetime  NOT NULL,
    [dt_last_played_date] datetime  NULL,
    [nvc_file_type] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [guid_id] uniqueidentifier  NOT NULL,
    [nvc_username] nvarchar(60)  NOT NULL,
    [nvc_first_name] nvarchar(60)  NOT NULL,
    [nvc_last_name] nvarchar(60)  NOT NULL,
    [dt_created_date] datetime  NOT NULL,
    [int_account_strikes] int  NOT NULL,
    [dt_date_banned] datetime  NULL,
    [nvc_mvc_id] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Commands'
CREATE TABLE [dbo].[Commands] (
    [int_id] int IDENTITY(1,1) NOT NULL,
    [int_command] int  NOT NULL
);
GO

-- Creating table 'Messages'
CREATE TABLE [dbo].[Messages] (
    [guid_id] uniqueidentifier  NOT NULL,
    [content] nvarchar(255)  NOT NULL,
    [date_created] datetime  NOT NULL,
    [fk_user] uniqueidentifier  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [guid_id] in table 'Requests'
ALTER TABLE [dbo].[Requests]
ADD CONSTRAINT [PK_Requests]
    PRIMARY KEY CLUSTERED ([guid_id] ASC);
GO

-- Creating primary key on [guid_id] in table 'Songs'
ALTER TABLE [dbo].[Songs]
ADD CONSTRAINT [PK_Songs]
    PRIMARY KEY CLUSTERED ([guid_id] ASC);
GO

-- Creating primary key on [guid_id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([guid_id] ASC);
GO

-- Creating primary key on [int_id] in table 'Commands'
ALTER TABLE [dbo].[Commands]
ADD CONSTRAINT [PK_Commands]
    PRIMARY KEY CLUSTERED ([int_id] ASC);
GO

-- Creating primary key on [guid_id] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [PK_Messages]
    PRIMARY KEY CLUSTERED ([guid_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [fk_song] in table 'Requests'
ALTER TABLE [dbo].[Requests]
ADD CONSTRAINT [FK__Request__fk_song__31EC6D26]
    FOREIGN KEY ([fk_song])
    REFERENCES [dbo].[Songs]
        ([guid_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Request__fk_song__31EC6D26'
CREATE INDEX [IX_FK__Request__fk_song__31EC6D26]
ON [dbo].[Requests]
    ([fk_song]);
GO

-- Creating foreign key on [fk_user] in table 'Requests'
ALTER TABLE [dbo].[Requests]
ADD CONSTRAINT [FK_Request__fk_user]
    FOREIGN KEY ([fk_user])
    REFERENCES [dbo].[Users]
        ([guid_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Request__fk_user'
CREATE INDEX [IX_FK_Request__fk_user]
ON [dbo].[Requests]
    ([fk_user]);
GO

-- Creating foreign key on [fk_user] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK__Messages__fk_use__49C3F6B7]
    FOREIGN KEY ([fk_user])
    REFERENCES [dbo].[Users]
        ([guid_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Messages__fk_use__49C3F6B7'
CREATE INDEX [IX_FK__Messages__fk_use__49C3F6B7]
ON [dbo].[Messages]
    ([fk_user]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------