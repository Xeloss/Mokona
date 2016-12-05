CREATE TABLE [dbo].[Roles] (
    [Id]          INT            IDENTITY (1000, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [IsAdmin]     BIT            NOT NULL,
    [CompanyId]   INT            NULL,
    CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Roles_CompanyId]
    ON [dbo].[Roles]([Id] ASC);

