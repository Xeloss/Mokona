CREATE TABLE [dbo].[UserRoles] (
    [User_Id] INT NOT NULL,
    [Role_Id] INT NOT NULL,
    CONSTRAINT [PK_dbo.UserRoles] PRIMARY KEY CLUSTERED ([User_Id] ASC, [Role_Id] ASC),
    CONSTRAINT [FK_dbo.UserRoles_dbo.Roles_Role_Id] FOREIGN KEY ([Role_Id]) REFERENCES [dbo].[Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.UserRoles_dbo.Users_User_Id] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Role_Id]
    ON [dbo].[UserRoles]([Role_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_User_Id]
    ON [dbo].[UserRoles]([User_Id] ASC);

