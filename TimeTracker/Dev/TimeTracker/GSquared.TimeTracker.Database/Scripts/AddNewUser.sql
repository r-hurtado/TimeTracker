USE [TimeTracker]
GO

	DECLARE @UserName NVARCHAR(256) = 'rlanghorne'
	DECLARE @Email NVARCHAR(256) = 'bobby.langhorne@gmail.com'

	declare @UserId uniqueidentifier
	execute [dbo].[aspnet_Users_CreateUser] '0389FFBE-0304-440F-A7A9-922C2211B782', @UserName, 0, GETDATE(), @UserId OUTPUT
	select @UserId

    DECLARE @IsLockedOut bit
    SET @IsLockedOut = 0

    DECLARE @LastLockoutDate  datetime
    SET @LastLockoutDate = CONVERT( datetime, '17540101', 112 )

    DECLARE @FailedPasswordAttemptCount int
    SET @FailedPasswordAttemptCount = 0

    DECLARE @FailedPasswordAttemptWindowStart  datetime
    SET @FailedPasswordAttemptWindowStart = CONVERT( datetime, '17540101', 112 )

    DECLARE @FailedPasswordAnswerAttemptCount int
    SET @FailedPasswordAnswerAttemptCount = 0

    DECLARE @FailedPasswordAnswerAttemptWindowStart  datetime
    SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )

	INSERT INTO dbo.aspnet_Membership
                ( ApplicationId,
                  UserId,
                  Password,
                  PasswordSalt,
                  Email,
                  LoweredEmail,
                  PasswordQuestion,
                  PasswordAnswer,
                  PasswordFormat,
                  IsApproved,
                  IsLockedOut,
                  CreateDate,
                  LastLoginDate,
                  LastPasswordChangedDate,
                  LastLockoutDate,
                  FailedPasswordAttemptCount,
                  FailedPasswordAttemptWindowStart,
                  FailedPasswordAnswerAttemptCount,
                  FailedPasswordAnswerAttemptWindowStart )
     VALUES
           ('0389FFBE-0304-440F-A7A9-922C2211B782'
           ,@UserId
           ,'4PJ0zb1Ue3haaPDItrGM3Wnt1vI='
           ,'rfdjax1XZ35L099DBWyTgg=='
           ,@Email
		   ,LOWER(@Email)
		   ,NULL
		   ,NULL
		   ,1
		   ,1
		   ,0
		   ,GETDATE()
		   ,GETDATE()
		   ,GETDATE()
		   ,@LastLockoutDate
		   ,@FailedPasswordAttemptCount
		   ,@FailedPasswordAttemptWindowStart
		   ,@FailedPasswordAnswerAttemptCount
		   ,@FailedPasswordAnswerAttemptWindowStart
           )
GO


