
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/13/2015 18:14:36
-- Generated from EDMX file: C:\Users\pwasilewski\documents\visual studio 2013\Projects\Wazabi\Wazabi\Model\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Wazabi];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PartieCarte]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cartes] DROP CONSTRAINT [FK_PartieCarte];
GO
IF OBJECT_ID(N'[dbo].[FK_PartieJoueur]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Parties] DROP CONSTRAINT [FK_PartieJoueur];
GO
IF OBJECT_ID(N'[dbo].[FK_PartieJoueurPartie]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Parties] DROP CONSTRAINT [FK_PartieJoueurPartie];
GO
IF OBJECT_ID(N'[dbo].[FK_PartieJoueurPartie1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JoueurParties] DROP CONSTRAINT [FK_PartieJoueurPartie1];
GO
IF OBJECT_ID(N'[dbo].[FK_JoueurJoueurPartie]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JoueurParties] DROP CONSTRAINT [FK_JoueurJoueurPartie];
GO
IF OBJECT_ID(N'[dbo].[FK_JoueurPartieCarte_JoueurPartie]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JoueurPartieCarte] DROP CONSTRAINT [FK_JoueurPartieCarte_JoueurPartie];
GO
IF OBJECT_ID(N'[dbo].[FK_JoueurPartieCarte_Carte]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JoueurPartieCarte] DROP CONSTRAINT [FK_JoueurPartieCarte_Carte];
GO
IF OBJECT_ID(N'[dbo].[FK_JoueurPartieDe_JoueurPartie]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JoueurPartieDe] DROP CONSTRAINT [FK_JoueurPartieDe_JoueurPartie];
GO
IF OBJECT_ID(N'[dbo].[FK_JoueurPartieDe_De]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JoueurPartieDe] DROP CONSTRAINT [FK_JoueurPartieDe_De];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Joueurs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Joueurs];
GO
IF OBJECT_ID(N'[dbo].[Parties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Parties];
GO
IF OBJECT_ID(N'[dbo].[JoueurParties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JoueurParties];
GO
IF OBJECT_ID(N'[dbo].[Cartes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cartes];
GO
IF OBJECT_ID(N'[dbo].[Des]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Des];
GO
IF OBJECT_ID(N'[dbo].[JoueurPartieCarte]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JoueurPartieCarte];
GO
IF OBJECT_ID(N'[dbo].[JoueurPartieDe]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JoueurPartieDe];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Joueurs'
CREATE TABLE [dbo].[Joueurs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Pseudo] nvarchar(max)  NOT NULL,
    [MotDePasse] varbinary(max)  NOT NULL,
    [Sel] varbinary(max)  NOT NULL,
    [CodeBloc] int  NOT NULL
);
GO

-- Creating table 'Parties'
CREATE TABLE [dbo].[Parties] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateHeureCreation] nvarchar(max)  NOT NULL,
    [Sens] nvarchar(max)  NOT NULL,
    [Etat] nvarchar(max)  NOT NULL,
    [Vainqueur_Id] int  NULL,
    [JoueurCourant_Id] int  NOT NULL
);
GO

-- Creating table 'JoueurParties'
CREATE TABLE [dbo].[JoueurParties] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Ordre] nvarchar(max)  NOT NULL,
    [PartieJoueurPartie1_JoueurPartie_Id] int  NOT NULL,
    [Joueur_Id] int  NOT NULL
);
GO

-- Creating table 'Cartes'
CREATE TABLE [dbo].[Cartes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CodeEffet] nvarchar(max)  NOT NULL,
    [Cout] nvarchar(max)  NOT NULL,
    [PartieCarte_Carte_Id] int  NOT NULL
);
GO

-- Creating table 'Des'
CREATE TABLE [dbo].[Des] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Valeur] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'JoueurPartieCarte'
CREATE TABLE [dbo].[JoueurPartieCarte] (
    [JoueurPartieCarte_Carte_Id] int  NOT NULL,
    [Cartes_Id] int  NOT NULL
);
GO

-- Creating table 'JoueurPartieDe'
CREATE TABLE [dbo].[JoueurPartieDe] (
    [JoueurPartieDe_De_Id] int  NOT NULL,
    [Des_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Joueurs'
ALTER TABLE [dbo].[Joueurs]
ADD CONSTRAINT [PK_Joueurs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Parties'
ALTER TABLE [dbo].[Parties]
ADD CONSTRAINT [PK_Parties]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JoueurParties'
ALTER TABLE [dbo].[JoueurParties]
ADD CONSTRAINT [PK_JoueurParties]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Cartes'
ALTER TABLE [dbo].[Cartes]
ADD CONSTRAINT [PK_Cartes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Des'
ALTER TABLE [dbo].[Des]
ADD CONSTRAINT [PK_Des]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [JoueurPartieCarte_Carte_Id], [Cartes_Id] in table 'JoueurPartieCarte'
ALTER TABLE [dbo].[JoueurPartieCarte]
ADD CONSTRAINT [PK_JoueurPartieCarte]
    PRIMARY KEY CLUSTERED ([JoueurPartieCarte_Carte_Id], [Cartes_Id] ASC);
GO

-- Creating primary key on [JoueurPartieDe_De_Id], [Des_Id] in table 'JoueurPartieDe'
ALTER TABLE [dbo].[JoueurPartieDe]
ADD CONSTRAINT [PK_JoueurPartieDe]
    PRIMARY KEY CLUSTERED ([JoueurPartieDe_De_Id], [Des_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PartieCarte_Carte_Id] in table 'Cartes'
ALTER TABLE [dbo].[Cartes]
ADD CONSTRAINT [FK_PartieCarte]
    FOREIGN KEY ([PartieCarte_Carte_Id])
    REFERENCES [dbo].[Parties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartieCarte'
CREATE INDEX [IX_FK_PartieCarte]
ON [dbo].[Cartes]
    ([PartieCarte_Carte_Id]);
GO

-- Creating foreign key on [Vainqueur_Id] in table 'Parties'
ALTER TABLE [dbo].[Parties]
ADD CONSTRAINT [FK_PartieJoueur]
    FOREIGN KEY ([Vainqueur_Id])
    REFERENCES [dbo].[Joueurs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartieJoueur'
CREATE INDEX [IX_FK_PartieJoueur]
ON [dbo].[Parties]
    ([Vainqueur_Id]);
GO

-- Creating foreign key on [JoueurCourant_Id] in table 'Parties'
ALTER TABLE [dbo].[Parties]
ADD CONSTRAINT [FK_PartieJoueurPartie]
    FOREIGN KEY ([JoueurCourant_Id])
    REFERENCES [dbo].[JoueurParties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartieJoueurPartie'
CREATE INDEX [IX_FK_PartieJoueurPartie]
ON [dbo].[Parties]
    ([JoueurCourant_Id]);
GO

-- Creating foreign key on [PartieJoueurPartie1_JoueurPartie_Id] in table 'JoueurParties'
ALTER TABLE [dbo].[JoueurParties]
ADD CONSTRAINT [FK_PartieJoueurPartie1]
    FOREIGN KEY ([PartieJoueurPartie1_JoueurPartie_Id])
    REFERENCES [dbo].[Parties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartieJoueurPartie1'
CREATE INDEX [IX_FK_PartieJoueurPartie1]
ON [dbo].[JoueurParties]
    ([PartieJoueurPartie1_JoueurPartie_Id]);
GO

-- Creating foreign key on [Joueur_Id] in table 'JoueurParties'
ALTER TABLE [dbo].[JoueurParties]
ADD CONSTRAINT [FK_JoueurJoueurPartie]
    FOREIGN KEY ([Joueur_Id])
    REFERENCES [dbo].[Joueurs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JoueurJoueurPartie'
CREATE INDEX [IX_FK_JoueurJoueurPartie]
ON [dbo].[JoueurParties]
    ([Joueur_Id]);
GO

-- Creating foreign key on [JoueurPartieCarte_Carte_Id] in table 'JoueurPartieCarte'
ALTER TABLE [dbo].[JoueurPartieCarte]
ADD CONSTRAINT [FK_JoueurPartieCarte_JoueurPartie]
    FOREIGN KEY ([JoueurPartieCarte_Carte_Id])
    REFERENCES [dbo].[JoueurParties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Cartes_Id] in table 'JoueurPartieCarte'
ALTER TABLE [dbo].[JoueurPartieCarte]
ADD CONSTRAINT [FK_JoueurPartieCarte_Carte]
    FOREIGN KEY ([Cartes_Id])
    REFERENCES [dbo].[Cartes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JoueurPartieCarte_Carte'
CREATE INDEX [IX_FK_JoueurPartieCarte_Carte]
ON [dbo].[JoueurPartieCarte]
    ([Cartes_Id]);
GO

-- Creating foreign key on [JoueurPartieDe_De_Id] in table 'JoueurPartieDe'
ALTER TABLE [dbo].[JoueurPartieDe]
ADD CONSTRAINT [FK_JoueurPartieDe_JoueurPartie]
    FOREIGN KEY ([JoueurPartieDe_De_Id])
    REFERENCES [dbo].[JoueurParties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Des_Id] in table 'JoueurPartieDe'
ALTER TABLE [dbo].[JoueurPartieDe]
ADD CONSTRAINT [FK_JoueurPartieDe_De]
    FOREIGN KEY ([Des_Id])
    REFERENCES [dbo].[Des]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JoueurPartieDe_De'
CREATE INDEX [IX_FK_JoueurPartieDe_De]
ON [dbo].[JoueurPartieDe]
    ([Des_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------