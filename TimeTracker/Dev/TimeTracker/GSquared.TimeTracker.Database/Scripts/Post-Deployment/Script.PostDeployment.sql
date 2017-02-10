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

/****** [dbo].[aspnet_Users] ******/
INSERT INTO [dbo].[aspnet_Users]([ApplicationId],[UserId],[UserName],[LoweredUserName],[MobileAlias],[IsAnonymous],[LastActivityDate]) VALUES ('0389FFBE-0304-440F-A7A9-922C2211B782','9D39FCCA-5701-4A9A-B1B7-30E4C2D32CB8','DBoss','dboss',null,0,getdate())

/****** [dbo].[aspnet_Membership] ******/
INSERT INTO [dbo].[aspnet_Membership]([ApplicationId],[UserId],[Password],[PasswordFormat],[PasswordSalt],[Email],[LoweredEmail],[IsApproved],[IsLockedOut],[CreateDate],[LastLoginDate],[LastPasswordChangedDate],[LastLockoutDate],[FailedPasswordAttemptCount],[FailedPasswordAttemptWindowStart],[FailedPasswordAnswerAttemptCount],[FailedPasswordAnswerAttemptWindowStart])
VALUES ('0389FFBE-0304-440F-A7A9-922C2211B782','9D39FCCA-5701-4A9A-B1B7-30E4C2D32CB8','4PJ0zb1Ue3haaPDItrGM3Wnt1vI=',1,'rfdjax1XZ35L099DBWyTgg==','darren@gsquaredsoftware.com','darren@gsquaredsoftware.com',1,0,getdate(),getdate(),getdate(),getdate(),0,getdate(),0,getdate())

/****** [dbo].[UserProfile] ******/
INSERT INTO [dbo].[UserProfile]([UserId],[FirstName],[MiddleInitial],[LastName]) VALUES ('9D39FCCA-5701-4A9A-B1B7-30E4C2D32CB8','Darren',null,'Boss')

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
INSERT [dbo].[Clients] ([ClientId], [ClientName], [BillingTermsId], [BillingCycleId], [UserId]) VALUES (1, N'Greg Oden', 3, 1,'9D39FCCA-5701-4A9A-B1B7-30E4C2D32CB8')
INSERT [dbo].[Clients] ([ClientId], [ClientName], [BillingTermsId], [BillingCycleId], [UserId]) VALUES (2, N'Dog Catchers', 2, 1,'9D39FCCA-5701-4A9A-B1B7-30E4C2D32CB8')
INSERT [dbo].[Clients] ([ClientId], [ClientName], [BillingTermsId], [BillingCycleId], [UserId]) VALUES (3, N'Obama', 1, 2,'9D39FCCA-5701-4A9A-B1B7-30E4C2D32CB8')
INSERT [dbo].[Clients] ([ClientId], [ClientName], [BillingTermsId], [BillingCycleId], [UserId]) VALUES (4, N'Obstacles, Inc', 1, 2,'9D39FCCA-5701-4A9A-B1B7-30E4C2D32CB8')
INSERT [dbo].[Clients] ([ClientId], [ClientName], [BillingTermsId], [BillingCycleId], [UserId]) VALUES (5, N'Lasso''s', 1, 1,'9D39FCCA-5701-4A9A-B1B7-30E4C2D32CB8')
INSERT [dbo].[Clients] ([ClientId], [ClientName], [BillingTermsId], [BillingCycleId], [UserId]) VALUES (6, N'Contoso', 1, 2,'9D39FCCA-5701-4A9A-B1B7-30E4C2D32CB8')
INSERT [dbo].[Clients] ([ClientId], [ClientName], [BillingTermsId], [BillingCycleId], [UserId]) VALUES (7, N'Overseas, Ltd.', 2, 1,'9D39FCCA-5701-4A9A-B1B7-30E4C2D32CB8')
INSERT [dbo].[Clients] ([ClientId], [ClientName], [BillingTermsId], [BillingCycleId], [UserId]) VALUES (8, N'Test Add', 1, 2,'9D39FCCA-5701-4A9A-B1B7-30E4C2D32CB8')
INSERT [dbo].[Clients] ([ClientId], [ClientName], [BillingTermsId], [BillingCycleId], [UserId]) VALUES (9, N'Test Add/Edit', 1, 1,'9D39FCCA-5701-4A9A-B1B7-30E4C2D32CB8')
INSERT [dbo].[Clients] ([ClientId], [ClientName], [BillingTermsId], [BillingCycleId], [UserId]) VALUES (11, N'Another Test Addition', 1, 2,'9D39FCCA-5701-4A9A-B1B7-30E4C2D32CB8')
INSERT [dbo].[Clients] ([ClientId], [ClientName], [BillingTermsId], [BillingCycleId], [UserId]) VALUES (12, N'Another', 1, 1,'9D39FCCA-5701-4A9A-B1B7-30E4C2D32CB8')
SET IDENTITY_INSERT [dbo].[Clients] OFF

/****** Object:  Table [dbo].[Projects]    Script Date: 04/25/2012 17:15:32 ******/
SET IDENTITY_INSERT [dbo].[Projects] ON
INSERT [dbo].[Projects] ([ProjectId], [ClientId], [ProjectName], [BillingCode], [QuickbooksProjectId], [HourlyBillingRate], [IsActive]) VALUES (1, 6, N'Data Fancification', NULL, NULL, CAST(75.0 as decimal(7,2)), 1)
INSERT [dbo].[Projects] ([ProjectId], [ClientId], [ProjectName], [BillingCode], [QuickbooksProjectId], [HourlyBillingRate], [IsActive]) VALUES (2, 5, N'Website Updates', NULL, NULL, CAST(75.0 as decimal(7,2)), 1)
INSERT [dbo].[Projects] ([ProjectId], [ClientId], [ProjectName], [BillingCode], [QuickbooksProjectId], [HourlyBillingRate], [IsActive]) VALUES (3, 5, N'Database Maintenance', NULL, NULL, CAST(75.0 as decimal(7,2)), 1)
INSERT [dbo].[Projects] ([ProjectId], [ClientId], [ProjectName], [BillingCode], [QuickbooksProjectId], [HourlyBillingRate], [IsActive]) VALUES (4, 3, N'White House Maintenance', NULL, NULL, CAST(75.0 as decimal(7,2)), 1)
INSERT [dbo].[Projects] ([ProjectId], [ClientId], [ProjectName], [BillingCode], [QuickbooksProjectId], [HourlyBillingRate], [IsActive]) VALUES (5, 1, N'Trawl Logbook', NULL, NULL, CAST(75.0 as decimal(7,2)), 1)
INSERT [dbo].[Projects] ([ProjectId], [ClientId], [ProjectName], [BillingCode], [QuickbooksProjectId], [HourlyBillingRate], [IsActive]) VALUES (6, 2, N'Dog Tracker', NULL, NULL, CAST(75.0 as decimal(7,2)), 1)
INSERT [dbo].[Projects] ([ProjectId], [ClientId], [ProjectName], [BillingCode], [QuickbooksProjectId], [HourlyBillingRate], [IsActive]) VALUES (7, 2, N'Adoption Reporting', NULL, NULL, CAST(75.0 as decimal(7,2)), 1)
INSERT [dbo].[Projects] ([ProjectId], [ClientId], [ProjectName], [BillingCode], [QuickbooksProjectId], [HourlyBillingRate], [IsActive]) VALUES (8, 2, N'Vehicle Tracker', NULL, NULL, CAST(75.0 as decimal(7,2)), 1)
INSERT [dbo].[Projects] ([ProjectId], [ClientId], [ProjectName], [BillingCode], [QuickbooksProjectId], [HourlyBillingRate], [IsActive]) VALUES (9, 4, N'Database Maintenance', NULL, NULL, CAST(75.0 as decimal(7,2)), 1)
INSERT [dbo].[Projects] ([ProjectId], [ClientId], [ProjectName], [BillingCode], [QuickbooksProjectId], [HourlyBillingRate], [IsActive]) VALUES (10, 7, N'Data Analysis Tool', NULL, NULL, CAST(75.0 as decimal(7,2)), 1)
SET IDENTITY_INSERT [dbo].[Projects] OFF

