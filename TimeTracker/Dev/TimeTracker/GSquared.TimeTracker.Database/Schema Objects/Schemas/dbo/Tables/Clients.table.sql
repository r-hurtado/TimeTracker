CREATE TABLE [dbo].[Clients] (
    [ClientId]          INT            IDENTITY (1, 1) NOT NULL,
    [ClientName]        NVARCHAR (MAX) NOT NULL,
    [HourlyBillingRate] DECIMAL (7, 2) NOT NULL,
    [BillingTermsId]    INT            NOT NULL,
    [BillingCycleId]    INT            NOT NULL
);

