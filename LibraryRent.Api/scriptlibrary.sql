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
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241009003950_inicio'
)
BEGIN
    IF SCHEMA_ID(N'Library') IS NULL EXEC(N'CREATE SCHEMA [Library];');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241009003950_inicio'
)
BEGIN
    CREATE TABLE [Library].[Clientes] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(150) NOT NULL,
        [Apellidos] nvarchar(200) NOT NULL,
        [Dni] nvarchar(10) NOT NULL,
        [Edad] int NOT NULL,
        CONSTRAINT [PK_Clientes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241009003950_inicio'
)
BEGIN
    CREATE TABLE [Library].[libros] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(150) NOT NULL,
        [Autor] nvarchar(200) NOT NULL,
        [ISBN] nvarchar(50) NOT NULL,
        [Estado] bit NOT NULL,
        CONSTRAINT [PK_libros] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241009003950_inicio'
)
BEGIN
    CREATE TABLE [Library].[Pedido] (
        [Id] int NOT NULL IDENTITY,
        [FechaPedido] datetime NOT NULL DEFAULT (GETDATE()),
        [ClienteId] int NOT NULL,
        [Estado] bit NOT NULL,
        CONSTRAINT [PK_Pedido] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Pedido_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Library].[Clientes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241009003950_inicio'
)
BEGIN
    CREATE TABLE [Library].[DetallePedido] (
        [Id] int NOT NULL IDENTITY,
        [IdPedido] int NOT NULL,
        [LibroId] int NOT NULL,
        CONSTRAINT [PK_DetallePedido] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_DetallePedido_Pedido_IdPedido] FOREIGN KEY ([IdPedido]) REFERENCES [Library].[Pedido] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_DetallePedido_libros_LibroId] FOREIGN KEY ([LibroId]) REFERENCES [Library].[libros] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241009003950_inicio'
)
BEGIN
    CREATE INDEX [IX_DetallePedido_IdPedido] ON [Library].[DetallePedido] ([IdPedido]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241009003950_inicio'
)
BEGIN
    CREATE INDEX [IX_DetallePedido_LibroId] ON [Library].[DetallePedido] ([LibroId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241009003950_inicio'
)
BEGIN
    CREATE INDEX [IX_Pedido_ClienteId] ON [Library].[Pedido] ([ClienteId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241009003950_inicio'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241009003950_inicio', N'8.0.10');
END;
GO

COMMIT;
GO

