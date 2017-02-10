CREATE TABLE [dbo].[UserProfile]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [MiddleInitial] NCHAR(1) NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [ExportHoursToQuickbooks] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_UserProfile_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [aspnet_Users]([UserId])
)
