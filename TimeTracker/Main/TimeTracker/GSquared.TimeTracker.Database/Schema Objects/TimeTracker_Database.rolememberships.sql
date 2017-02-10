EXECUTE sp_addrolemember @rolename = N'aspnet_Personalization_FullAccess', @membername = N'ttusr';


GO
EXECUTE sp_addrolemember @rolename = N'aspnet_Personalization_BasicAccess', @membername = N'aspnet_Personalization_FullAccess';


GO
EXECUTE sp_addrolemember @rolename = N'aspnet_Personalization_ReportingAccess', @membername = N'aspnet_Personalization_FullAccess';


GO
EXECUTE sp_addrolemember @rolename = N'aspnet_WebEvent_FullAccess', @membername = N'ttusr';


GO
EXECUTE sp_addrolemember @rolename = N'db_owner', @membername = N'ttadmin';


GO
EXECUTE sp_addrolemember @rolename = N'db_datareader', @membername = N'ttusr';


GO
EXECUTE sp_addrolemember @rolename = N'db_datawriter', @membername = N'ttusr';


GO
EXECUTE sp_addrolemember @rolename = N'aspnet_Membership_FullAccess', @membername = N'ttusr';


GO
EXECUTE sp_addrolemember @rolename = N'aspnet_Membership_BasicAccess', @membername = N'aspnet_Membership_FullAccess';


GO
EXECUTE sp_addrolemember @rolename = N'aspnet_Membership_ReportingAccess', @membername = N'aspnet_Membership_FullAccess';


GO
EXECUTE sp_addrolemember @rolename = N'aspnet_Profile_FullAccess', @membername = N'ttusr';


GO
EXECUTE sp_addrolemember @rolename = N'aspnet_Profile_BasicAccess', @membername = N'aspnet_Profile_FullAccess';


GO
EXECUTE sp_addrolemember @rolename = N'aspnet_Profile_ReportingAccess', @membername = N'aspnet_Profile_FullAccess';


GO
EXECUTE sp_addrolemember @rolename = N'aspnet_Roles_FullAccess', @membername = N'ttusr';


GO
EXECUTE sp_addrolemember @rolename = N'aspnet_Roles_BasicAccess', @membername = N'aspnet_Roles_FullAccess';


GO
EXECUTE sp_addrolemember @rolename = N'aspnet_Roles_ReportingAccess', @membername = N'aspnet_Roles_FullAccess';

