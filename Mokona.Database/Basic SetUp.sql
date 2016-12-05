USE [MokonaDB]
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

GO
INSERT [dbo].[Roles] ([Id], [Name], [Description], [IsAdmin], [CompanyId]) VALUES (1, N'SysAdmin', N'System Admin', 0, NULL)
GO
INSERT [dbo].[Roles] ([Id], [Name], [Description], [IsAdmin], [CompanyId]) VALUES (1000, N'Admin', N'Generic Admin', 1, NULL)
GO
INSERT [dbo].[Roles] ([Id], [Name], [Description], [IsAdmin], [CompanyId]) VALUES (1002, N'User', N'Generic User', 0, NULL)
GO
INSERT [dbo].[Roles] ([Id], [Name], [Description], [IsAdmin], [CompanyId]) VALUES (1003, N'MokonaUser', N'Mokona User', 0, 1001)
GO
INSERT [dbo].[Roles] ([Id], [Name], [Description], [IsAdmin], [CompanyId]) VALUES (1004, N'MokonaAdmin', N'Mokona Administrator', 1, 1001)
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO

SET IDENTITY_INSERT [dbo].[Companies] ON 

GO
INSERT [dbo].[Companies] ([Id], [Name], [Annotations], [Domain]) VALUES (1001, N'Mokona S.A.', N'IT Forge', N'mokona')
GO
SET IDENTITY_INSERT [dbo].[Companies] OFF
GO

SET IDENTITY_INSERT [dbo].[Users] ON 

GO
INSERT [dbo].[Users] ([Id], [LoginName], [Password], [FirstName], [LastName], [Email], [CompanyId], [Deleted], [DeletedDate], [EmailWasVerified], [UserApprovalStatus]) VALUES (1, N'galu', N'aSW1Xl8WdlOo9/DHdzkg8A==', N'Leandro', N'Galluppi', N'leandrog@lagash.com', 1001, 0, NULL, 1, 1)
GO

SET IDENTITY_INSERT [dbo].[Users] OFF
GO
INSERT [dbo].[UserRoles] ([User_Id], [Role_Id]) VALUES (1, 1000)
GO