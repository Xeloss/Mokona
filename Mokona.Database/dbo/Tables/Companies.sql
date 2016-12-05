CREATE TABLE [dbo].[Companies] (
    [Id]          INT            IDENTITY (1000, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL,
    [Annotations] NVARCHAR (MAX) NULL,
    [Domain]      NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_dbo.Companies] PRIMARY KEY CLUSTERED ([Id] ASC)
);

