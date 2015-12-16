
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/16/2015 12:37:06
-- Generated from EDMX file: C:\Users\gaetb\Desktop\Projet\WazabiCraft\Wazabi\Model\Model1.edmx
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

IF OBJECT_ID(N'[dbo].[FK_DeJoueurPartie_Des]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DeJoueurPartie] DROP CONSTRAINT [FK_DeJoueurPartie_Des];
GO
IF OBJECT_ID(N'[dbo].[FK_DeJoueurPartie_JoueurParties]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DeJoueurPartie] DROP CONSTRAINT [FK_DeJoueurPartie_JoueurParties];
GO
IF OBJECT_ID(N'[dbo].[FK_JoueurJoueurPartie]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JoueurParties] DROP CONSTRAINT [FK_JoueurJoueurPartie];
GO
IF OBJECT_ID(N'[dbo].[FK_JoueurPartie1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Parties] DROP CONSTRAINT [FK_JoueurPartie1];
GO
IF OBJECT_ID(N'[dbo].[FK_JoueurPartie2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Parties] DROP CONSTRAINT [FK_JoueurPartie2];
GO
IF OBJECT_ID(N'[dbo].[FK_JoueurPartieCarte_Cartes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JoueurPartieCarte] DROP CONSTRAINT [FK_JoueurPartieCarte_Cartes];
GO
IF OBJECT_ID(N'[dbo].[FK_JoueurPartieCarte_JoueurParties]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JoueurPartieCarte] DROP CONSTRAINT [FK_JoueurPartieCarte_JoueurParties];
GO
IF OBJECT_ID(N'[dbo].[FK_JoueurPartiePartie]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JoueurParties] DROP CONSTRAINT [FK_JoueurPartiePartie];
GO
IF OBJECT_ID(N'[dbo].[FK_PartieCarte_Cartes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PartieCarte] DROP CONSTRAINT [FK_PartieCarte_Cartes];
GO
IF OBJECT_ID(N'[dbo].[FK_PartieCarte_Parties]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PartieCarte] DROP CONSTRAINT [FK_PartieCarte_Parties];
GO
IF OBJECT_ID(N'[dbo].[FK_PartieJoueurPartie]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JoueurParties] DROP CONSTRAINT [FK_PartieJoueurPartie];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Cartes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cartes];
GO
IF OBJECT_ID(N'[dbo].[DeJoueurPartie]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeJoueurPartie];
GO
IF OBJECT_ID(N'[dbo].[Des]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Des];
GO
IF OBJECT_ID(N'[dbo].[JoueurPartieCarte]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JoueurPartieCarte];
GO
IF OBJECT_ID(N'[dbo].[JoueurParties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JoueurParties];
GO
IF OBJECT_ID(N'[dbo].[Joueurs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Joueurs];
GO
IF OBJECT_ID(N'[dbo].[PartieCarte]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PartieCarte];
GO
IF OBJECT_ID(N'[dbo].[Parties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Parties];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Cartes'
CREATE TABLE [dbo].[Cartes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CodeEffet] nvarchar(max)  NOT NULL,
    [Cout] int  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Effet] nvarchar(max)  NOT NULL,
    [ImageRef] nvarchar(max)  NOT NULL,
    [NbCartes] int  NOT NULL
);
GO

-- Creating table 'Des'
CREATE TABLE [dbo].[Des] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Valeur] nvarchar(max)  NOT NULL,
    [ImageRef] nvarchar(max)  NULL
);
GO

-- Creating table 'JoueurParties'
CREATE TABLE [dbo].[JoueurParties] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Ordre] int  NOT NULL,
    [Joueur_Id] int  NOT NULL,
    [PartieJoueurPartie_JoueurPartie_Id] int  NOT NULL,
    [JoueurPartiePartie_JoueurPartie_Id] int  NOT NULL,
    [JoueursQuiDoiventPasserTour_JoueurPartie_Id] int  NULL
);
GO

-- Creating table 'Joueurs'
CREATE TABLE [dbo].[Joueurs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Pseudo] nvarchar(max)  NOT NULL,
    [MotDePasse] varbinary(max)  NOT NULL,
    [Sel] varbinary(max)  NOT NULL,
    [IconeRef] nvarchar(max)  NULL
);
GO

-- Creating table 'Parties'
CREATE TABLE [dbo].[Parties] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(max)  NOT NULL,
    [DateHeureCreation] datetime  NOT NULL,
    [Sens] bit  NOT NULL,
    [Etat] int  NOT NULL,
    [Vainqueur_Id] int  NULL,
    [Createur_Id] int  NOT NULL
);
GO

-- Creating table 'DeJoueurPartie'
CREATE TABLE [dbo].[DeJoueurPartie] (
    [Des_Id] int  NOT NULL,
    [DeJoueurPartie_Des_Id] int  NOT NULL
);
GO

-- Creating table 'JoueurPartieCarte'
CREATE TABLE [dbo].[JoueurPartieCarte] (
    [Cartes_Id] int  NOT NULL,
    [JoueurPartieCarte_Cartes_Id] int  NOT NULL
);
GO

-- Creating table 'PartieCarte'
CREATE TABLE [dbo].[PartieCarte] (
    [Pioche_Id] int  NOT NULL,
    [PartieCarte_Cartes_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

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

-- Creating primary key on [Id] in table 'JoueurParties'
ALTER TABLE [dbo].[JoueurParties]
ADD CONSTRAINT [PK_JoueurParties]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

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

-- Creating primary key on [Des_Id], [DeJoueurPartie_Des_Id] in table 'DeJoueurPartie'
ALTER TABLE [dbo].[DeJoueurPartie]
ADD CONSTRAINT [PK_DeJoueurPartie]
    PRIMARY KEY CLUSTERED ([Des_Id], [DeJoueurPartie_Des_Id] ASC);
GO

-- Creating primary key on [Cartes_Id], [JoueurPartieCarte_Cartes_Id] in table 'JoueurPartieCarte'
ALTER TABLE [dbo].[JoueurPartieCarte]
ADD CONSTRAINT [PK_JoueurPartieCarte]
    PRIMARY KEY CLUSTERED ([Cartes_Id], [JoueurPartieCarte_Cartes_Id] ASC);
GO

-- Creating primary key on [Pioche_Id], [PartieCarte_Cartes_Id] in table 'PartieCarte'
ALTER TABLE [dbo].[PartieCarte]
ADD CONSTRAINT [PK_PartieCarte]
    PRIMARY KEY CLUSTERED ([Pioche_Id], [PartieCarte_Cartes_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

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

-- Creating foreign key on [PartieJoueurPartie_JoueurPartie_Id] in table 'JoueurParties'
ALTER TABLE [dbo].[JoueurParties]
ADD CONSTRAINT [FK_PartieJoueurPartie]
    FOREIGN KEY ([PartieJoueurPartie_JoueurPartie_Id])
    REFERENCES [dbo].[Parties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartieJoueurPartie'
CREATE INDEX [IX_FK_PartieJoueurPartie]
ON [dbo].[JoueurParties]
    ([PartieJoueurPartie_JoueurPartie_Id]);
GO

-- Creating foreign key on [Vainqueur_Id] in table 'Parties'
ALTER TABLE [dbo].[Parties]
ADD CONSTRAINT [FK_JoueurPartie1]
    FOREIGN KEY ([Vainqueur_Id])
    REFERENCES [dbo].[Joueurs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JoueurPartie1'
CREATE INDEX [IX_FK_JoueurPartie1]
ON [dbo].[Parties]
    ([Vainqueur_Id]);
GO

-- Creating foreign key on [Createur_Id] in table 'Parties'
ALTER TABLE [dbo].[Parties]
ADD CONSTRAINT [FK_JoueurPartie2]
    FOREIGN KEY ([Createur_Id])
    REFERENCES [dbo].[Joueurs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JoueurPartie2'
CREATE INDEX [IX_FK_JoueurPartie2]
ON [dbo].[Parties]
    ([Createur_Id]);
GO

-- Creating foreign key on [Des_Id] in table 'DeJoueurPartie'
ALTER TABLE [dbo].[DeJoueurPartie]
ADD CONSTRAINT [FK_DeJoueurPartie_Des]
    FOREIGN KEY ([Des_Id])
    REFERENCES [dbo].[Des]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [DeJoueurPartie_Des_Id] in table 'DeJoueurPartie'
ALTER TABLE [dbo].[DeJoueurPartie]
ADD CONSTRAINT [FK_DeJoueurPartie_JoueurParties]
    FOREIGN KEY ([DeJoueurPartie_Des_Id])
    REFERENCES [dbo].[JoueurParties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DeJoueurPartie_JoueurParties'
CREATE INDEX [IX_FK_DeJoueurPartie_JoueurParties]
ON [dbo].[DeJoueurPartie]
    ([DeJoueurPartie_Des_Id]);
GO

-- Creating foreign key on [Cartes_Id] in table 'JoueurPartieCarte'
ALTER TABLE [dbo].[JoueurPartieCarte]
ADD CONSTRAINT [FK_JoueurPartieCarte_Cartes]
    FOREIGN KEY ([Cartes_Id])
    REFERENCES [dbo].[Cartes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [JoueurPartieCarte_Cartes_Id] in table 'JoueurPartieCarte'
ALTER TABLE [dbo].[JoueurPartieCarte]
ADD CONSTRAINT [FK_JoueurPartieCarte_JoueurParties]
    FOREIGN KEY ([JoueurPartieCarte_Cartes_Id])
    REFERENCES [dbo].[JoueurParties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JoueurPartieCarte_JoueurParties'
CREATE INDEX [IX_FK_JoueurPartieCarte_JoueurParties]
ON [dbo].[JoueurPartieCarte]
    ([JoueurPartieCarte_Cartes_Id]);
GO

-- Creating foreign key on [Pioche_Id] in table 'PartieCarte'
ALTER TABLE [dbo].[PartieCarte]
ADD CONSTRAINT [FK_PartieCarte_Cartes]
    FOREIGN KEY ([Pioche_Id])
    REFERENCES [dbo].[Cartes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PartieCarte_Cartes_Id] in table 'PartieCarte'
ALTER TABLE [dbo].[PartieCarte]
ADD CONSTRAINT [FK_PartieCarte_Parties]
    FOREIGN KEY ([PartieCarte_Cartes_Id])
    REFERENCES [dbo].[Parties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartieCarte_Parties'
CREATE INDEX [IX_FK_PartieCarte_Parties]
ON [dbo].[PartieCarte]
    ([PartieCarte_Cartes_Id]);
GO

-- Creating foreign key on [JoueurPartiePartie_JoueurPartie_Id] in table 'JoueurParties'
ALTER TABLE [dbo].[JoueurParties]
ADD CONSTRAINT [FK_JoueurPartiePartie]
    FOREIGN KEY ([JoueurPartiePartie_JoueurPartie_Id])
    REFERENCES [dbo].[Parties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JoueurPartiePartie'
CREATE INDEX [IX_FK_JoueurPartiePartie]
ON [dbo].[JoueurParties]
    ([JoueurPartiePartie_JoueurPartie_Id]);
GO

-- Creating foreign key on [JoueursQuiDoiventPasserTour_JoueurPartie_Id] in table 'JoueurParties'
ALTER TABLE [dbo].[JoueurParties]
ADD CONSTRAINT [FK_JoueursQuiDoiventPasserTour]
    FOREIGN KEY ([JoueursQuiDoiventPasserTour_JoueurPartie_Id])
    REFERENCES [dbo].[Parties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JoueursQuiDoiventPasserTour'
CREATE INDEX [IX_FK_JoueursQuiDoiventPasserTour]
ON [dbo].[JoueurParties]
    ([JoueursQuiDoiventPasserTour_JoueurPartie_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------