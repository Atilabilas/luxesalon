IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [BlogPosts] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(200) NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    [Summary] nvarchar(max) NULL,
    [HeaderImageUrl] nvarchar(max) NULL,
    [PublishedAt] datetime2 NOT NULL,
    [Author] nvarchar(max) NOT NULL,
    [IsPublished] bit NOT NULL,
    CONSTRAINT [PK_BlogPosts] PRIMARY KEY ([Id])
);

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(200) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [StockQuantity] int NOT NULL,
    [ImageUrl] nvarchar(max) NULL,
    [Category] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);

CREATE TABLE [Services] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [DurationMinutes] int NOT NULL,
    [Category] nvarchar(max) NOT NULL,
    [ImageUrl] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Services] PRIMARY KEY ([Id])
);

CREATE TABLE [Appointments] (
    [Id] int NOT NULL IDENTITY,
    [CustomerName] nvarchar(100) NOT NULL,
    [CustomerEmail] nvarchar(max) NOT NULL,
    [CustomerPhone] nvarchar(max) NOT NULL,
    [ServiceId] int NOT NULL,
    [AppointmentDate] datetime2 NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [Notes] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Appointments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Appointments_Services_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [Services] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Appointments_ServiceId] ON [Appointments] ([ServiceId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251220085517_Initial', N'10.0.1');

COMMIT;
GO

BEGIN TRANSACTION;
CREATE TABLE [PriceItems] (
    [Id] int NOT NULL IDENTITY,
    [Category] nvarchar(100) NOT NULL,
    [ServiceName] nvarchar(150) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [Description] nvarchar(300) NULL,
    [DisplayOrder] int NOT NULL,
    [IsActive] bit NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_PriceItems] PRIMARY KEY ([Id])
);

CREATE TABLE [SiteImages] (
    [Id] int NOT NULL IDENTITY,
    [Section] nvarchar(100) NOT NULL,
    [ImageUrl] nvarchar(500) NOT NULL,
    [Title] nvarchar(200) NULL,
    [Description] nvarchar(500) NULL,
    [AltText] nvarchar(100) NULL,
    [DisplayOrder] int NOT NULL,
    [IsActive] bit NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_SiteImages] PRIMARY KEY ([Id])
);

CREATE TABLE [TeamMembers] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Role] nvarchar(100) NOT NULL,
    [ImageUrl] nvarchar(500) NULL,
    [Bio] nvarchar(500) NULL,
    [DisplayOrder] int NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_TeamMembers] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251220185156_AddCMSEntities', N'10.0.1');

COMMIT;
GO

BEGIN TRANSACTION;
CREATE TABLE [SiteContents] (
    [Id] int NOT NULL IDENTITY,
    [Key] nvarchar(max) NOT NULL,
    [Section] nvarchar(max) NOT NULL,
    [Value] nvarchar(max) NOT NULL,
    [ContentType] nvarchar(max) NOT NULL,
    [DisplayOrder] int NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_SiteContents] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251221053043_AddSiteContent', N'10.0.1');

COMMIT;
GO

