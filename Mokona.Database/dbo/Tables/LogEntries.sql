CREATE TABLE [dbo].[LogEntries] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [TimeStamp]   DATETIME       NOT NULL,
    [UserId]      INT            NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.LogEntries] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.LogEntries_dbo.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[LogEntries]([UserId] ASC);

