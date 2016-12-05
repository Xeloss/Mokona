CREATE TABLE [dbo].[Users] (
    [Id]                 INT            IDENTITY (1000, 1) NOT NULL,
    [LoginName]          NVARCHAR (MAX) NULL,
    [Password]           NVARCHAR (MAX) NULL,
    [FirstName]          NVARCHAR (MAX) NULL,
    [LastName]           NVARCHAR (MAX) NULL,
    [Email]              NVARCHAR (MAX) NULL,
    [CompanyId]          INT            NOT NULL,
    [Deleted]            BIT            NOT NULL,
    [DeletedDate]        DATETIME       NULL,
    [EmailWasVerified]   BIT            CONSTRAINT [DF_Users_EmailWasVerified] DEFAULT ((0)) NOT NULL,
    [UserApprovalStatus] INT            CONSTRAINT [DF_Users_UserApprovalStatus] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Users_dbo.Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Companies] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_CompanyId]
    ON [dbo].[Users]([CompanyId] ASC);

