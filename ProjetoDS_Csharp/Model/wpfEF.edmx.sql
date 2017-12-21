
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/20/2017 21:54:57
-- Generated from EDMX file: C:\Users\TI01VM\Documents\Visual Studio 2015\Projects\ProjetoDS_Csharp\ProjetoDS_Csharp\Model\wpfEF.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [wpf_db];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UsuarioSistemaImportaDocumento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ImportaDocumentos] DROP CONSTRAINT [FK_UsuarioSistemaImportaDocumento];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ImportaDocumentos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ImportaDocumentos];
GO
IF OBJECT_ID(N'[dbo].[Perfil]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Perfil];
GO
IF OBJECT_ID(N'[dbo].[UsuarioSistemas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsuarioSistemas];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ImportaDocumentos'
CREATE TABLE [dbo].[ImportaDocumentos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UsuarioSistemaId] int  NOT NULL,
    [Nome] nvarchar(200)  NOT NULL,
    [DataRegistro] datetime  NOT NULL,
    [ArquivoMortoSN] bit  NOT NULL,
    [NumeroDocumento] bigint  NOT NULL
);
GO

-- Creating table 'Perfils'
CREATE TABLE [dbo].[Perfils] (
    [Id] int  NOT NULL,
    [CodigoPerfil] nchar(1)  NULL,
    [NomePerfil] nvarchar(50)  NULL
);
GO

-- Creating table 'UsuarioSistemas'
CREATE TABLE [dbo].[UsuarioSistemas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(50)  NOT NULL,
    [Senha] nvarchar(12)  NOT NULL,
    [Perfil] nvarchar(1)  NOT NULL,
    [Nome] nvarchar(200)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ImportaDocumentos'
ALTER TABLE [dbo].[ImportaDocumentos]
ADD CONSTRAINT [PK_ImportaDocumentos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Perfils'
ALTER TABLE [dbo].[Perfils]
ADD CONSTRAINT [PK_Perfils]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UsuarioSistemas'
ALTER TABLE [dbo].[UsuarioSistemas]
ADD CONSTRAINT [PK_UsuarioSistemas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UsuarioSistemaId] in table 'ImportaDocumentos'
ALTER TABLE [dbo].[ImportaDocumentos]
ADD CONSTRAINT [FK_UsuarioSistemaImportaDocumento]
    FOREIGN KEY ([UsuarioSistemaId])
    REFERENCES [dbo].[UsuarioSistemas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuarioSistemaImportaDocumento'
CREATE INDEX [IX_FK_UsuarioSistemaImportaDocumento]
ON [dbo].[ImportaDocumentos]
    ([UsuarioSistemaId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------