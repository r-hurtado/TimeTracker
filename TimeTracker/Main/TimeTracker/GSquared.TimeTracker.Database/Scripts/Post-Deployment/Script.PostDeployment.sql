/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

delete from TimeEntries
go
delete from Projects
go
delete from Clients
go
delete from BillingCycle
go
delete from BillingTerms
go


/****** Object:  Table [dbo].[BillingTerms]    Script Date: 04/25/2012 17:15:32 ******/
SET IDENTITY_INSERT [dbo].[BillingTerms] ON
INSERT [dbo].[BillingTerms] ([BillingTermsId], [BillingTermsDescription], [NumberOfDaysToPay]) VALUES (1, N'Net 15', 15)
INSERT [dbo].[BillingTerms] ([BillingTermsId], [BillingTermsDescription], [NumberOfDaysToPay]) VALUES (2, N'Net 30', 30)
INSERT [dbo].[BillingTerms] ([BillingTermsId], [BillingTermsDescription], [NumberOfDaysToPay]) VALUES (3, N'Net 60', 60)
SET IDENTITY_INSERT [dbo].[BillingTerms] OFF

/****** Object:  Table [dbo].[BillingCycle]    Script Date: 04/25/2012 17:15:32 ******/
SET IDENTITY_INSERT [dbo].[BillingCycle] ON
INSERT [dbo].[BillingCycle] ([BillingCycleId], [BillingCycleDescription]) VALUES (1, N'First of Month')
INSERT [dbo].[BillingCycle] ([BillingCycleId], [BillingCycleDescription]) VALUES (2, N'Mid Month')
SET IDENTITY_INSERT [dbo].[BillingCycle] OFF

/****** Object:  Table [dbo].[Clients]    Script Date: 04/25/2012 17:15:32 ******/
SET IDENTITY_INSERT [dbo].[Clients] ON
INSERT [dbo].[Clients] ([ClientId], [ClientName], [HourlyBillingRate], [BillingTermsId], [BillingCycleId]) VALUES (1, N'Greg Oden', CAST(75000.00 AS Decimal(7, 2)), 3, 1)
INSERT [dbo].[Clients] ([ClientId], [ClientName], [HourlyBillingRate], [BillingTermsId], [BillingCycleId]) VALUES (2, N'Dog Catchers', CAST(210.50 AS Decimal(7, 2)), 2, 1)
INSERT [dbo].[Clients] ([ClientId], [ClientName], [HourlyBillingRate], [BillingTermsId], [BillingCycleId]) VALUES (3, N'Obama', CAST(375.00 AS Decimal(7, 2)), 1, 2)
INSERT [dbo].[Clients] ([ClientId], [ClientName], [HourlyBillingRate], [BillingTermsId], [BillingCycleId]) VALUES (4, N'Obstacles, Inc', CAST(22.50 AS Decimal(7, 2)), 1, 2)
INSERT [dbo].[Clients] ([ClientId], [ClientName], [HourlyBillingRate], [BillingTermsId], [BillingCycleId]) VALUES (5, N'Lasso''s', CAST(7.50 AS Decimal(7, 2)), 1, 1)
INSERT [dbo].[Clients] ([ClientId], [ClientName], [HourlyBillingRate], [BillingTermsId], [BillingCycleId]) VALUES (6, N'Contoso', CAST(50.00 AS Decimal(7, 2)), 1, 2)
INSERT [dbo].[Clients] ([ClientId], [ClientName], [HourlyBillingRate], [BillingTermsId], [BillingCycleId]) VALUES (7, N'Overseas, Ltd.', CAST(18.00 AS Decimal(7, 2)), 2, 1)
INSERT [dbo].[Clients] ([ClientId], [ClientName], [HourlyBillingRate], [BillingTermsId], [BillingCycleId]) VALUES (8, N'Test Add', CAST(42.50 AS Decimal(7, 2)), 1, 2)
INSERT [dbo].[Clients] ([ClientId], [ClientName], [HourlyBillingRate], [BillingTermsId], [BillingCycleId]) VALUES (9, N'Test Add/Edit', CAST(100.00 AS Decimal(7, 2)), 1, 1)
INSERT [dbo].[Clients] ([ClientId], [ClientName], [HourlyBillingRate], [BillingTermsId], [BillingCycleId]) VALUES (11, N'Another Test Addition', CAST(250.00 AS Decimal(7, 2)), 1, 2)
INSERT [dbo].[Clients] ([ClientId], [ClientName], [HourlyBillingRate], [BillingTermsId], [BillingCycleId]) VALUES (12, N'Another', CAST(20.00 AS Decimal(7, 2)), 1, 1)
SET IDENTITY_INSERT [dbo].[Clients] OFF

/****** Object:  Table [dbo].[Projects]    Script Date: 04/25/2012 17:15:32 ******/
SET IDENTITY_INSERT [dbo].[Projects] ON
INSERT [dbo].[Projects] ([ProjectId], [ClientId], [ProjectName], [BillingCode], [QuickbooksProjectId]) VALUES (1, 6, N'Data Fancification', NULL, NULL)
INSERT [dbo].[Projects] ([ProjectId], [ClientId], [ProjectName], [BillingCode], [QuickbooksProjectId]) VALUES (2, 5, N'Website Updates', NULL, NULL)
INSERT [dbo].[Projects] ([ProjectId], [ClientId], [ProjectName], [BillingCode], [QuickbooksProjectId]) VALUES (3, 5, N'Database Maintenance', NULL, NULL)
INSERT [dbo].[Projects] ([ProjectId], [ClientId], [ProjectName], [BillingCode], [QuickbooksProjectId]) VALUES (4, 3, N'White House Maintenance', NULL, NULL)
INSERT [dbo].[Projects] ([ProjectId], [ClientId], [ProjectName], [BillingCode], [QuickbooksProjectId]) VALUES (5, 1, N'Trawl Logbook', NULL, NULL)
INSERT [dbo].[Projects] ([ProjectId], [ClientId], [ProjectName], [BillingCode], [QuickbooksProjectId]) VALUES (6, 2, N'Dog Tracker', NULL, NULL)
INSERT [dbo].[Projects] ([ProjectId], [ClientId], [ProjectName], [BillingCode], [QuickbooksProjectId]) VALUES (7, 2, N'Adoption Reporting', NULL, NULL)
INSERT [dbo].[Projects] ([ProjectId], [ClientId], [ProjectName], [BillingCode], [QuickbooksProjectId]) VALUES (8, 2, N'Vehicle Tracker', NULL, NULL)
INSERT [dbo].[Projects] ([ProjectId], [ClientId], [ProjectName], [BillingCode], [QuickbooksProjectId]) VALUES (9, 4, N'Database Maintenance', NULL, NULL)
INSERT [dbo].[Projects] ([ProjectId], [ClientId], [ProjectName], [BillingCode], [QuickbooksProjectId]) VALUES (10, 7, N'Data Analysis Tool', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Projects] OFF

/****** Object:  Table [dbo].[TimeEntries]    Script Date: 04/25/2012 17:15:32 ******/
SET IDENTITY_INSERT [dbo].[TimeEntries] ON
INSERT [dbo].[TimeEntries] ([TimeEntryId], [UserId], [FromTime], [ToTime], [TotalTime], [DateWorked], [IsBillable], [ProjectId], [Description], [CreatedAt], [CreatedBy]) VALUES (2, N'00000000-0000-0000-0000-000000000000', CAST(0x00009F3200D63BC0 AS DateTime), CAST(0x00009F32011826C0 AS DateTime), CAST(4.00 AS Decimal(4, 2)), CAST(0x00009F3200000000 AS DateTime), 1, 5, NULL, CAST(0x00009F32016B4624 AS DateTime), N'dboss')
INSERT [dbo].[TimeEntries] ([TimeEntryId], [UserId], [FromTime], [ToTime], [TotalTime], [DateWorked], [IsBillable], [ProjectId], [Description], [CreatedAt], [CreatedBy]) VALUES (3, N'00000000-0000-0000-0000-000000000000', CAST(0x00009F3200709C20 AS DateTime), CAST(0x00009F3200CF5DF0 AS DateTime), CAST(5.75 AS Decimal(4, 2)), CAST(0x00009F3200000000 AS DateTime), 1, 6, NULL, CAST(0x00009F320177E2FA AS DateTime), N'dboss')
INSERT [dbo].[TimeEntries] ([TimeEntryId], [UserId], [FromTime], [ToTime], [TotalTime], [DateWorked], [IsBillable], [ProjectId], [Description], [CreatedAt], [CreatedBy]) VALUES (4, N'00000000-0000-0000-0000-000000000000', CAST(0x00009F3300C1A250 AS DateTime), CAST(0x00009F3300C9DFB0 AS DateTime), CAST(0.50 AS Decimal(4, 2)), CAST(0x00009F3300000000 AS DateTime), 1, 4, NULL, CAST(0x00009F3300D2CEA9 AS DateTime), N'dboss')
SET IDENTITY_INSERT [dbo].[TimeEntries] OFF
