CREATE TABLE [dbo].[RolePermissions] (
    [Id]         INT IDENTITY (1000, 1) NOT NULL,
    [RoleId]     INT NOT NULL,
    [EntityId]   INT NULL,
    [EntityType] INT NOT NULL,
    [Permission] INT NOT NULL,
    [IsDenied]   BIT NOT NULL,
    CONSTRAINT [PK_dbo.RolePermissions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.RolePermissions_dbo.Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_RoleId]
    ON [dbo].[RolePermissions]([RoleId] ASC);

