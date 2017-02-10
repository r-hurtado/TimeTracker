CREATE TABLE [dbo].[Projects] (
    [ProjectId]        INT            IDENTITY (1, 1) NOT NULL,
    [ClientId]         INT            NOT NULL,
    [ProjectName]      NVARCHAR (150) NOT NULL,
    [BillingCode]      NVARCHAR (15)  NULL,
    [QuickenProjectId] NVARCHAR (250) NULL
);

