CREATE TABLE [dbo].[BillingCycle] (
    [BillingCycleId]          INT           IDENTITY (1, 1) NOT NULL,
    [BillingCycleDescription] VARCHAR (150) NOT NULL,
    CONSTRAINT [PK_BillingCycle] PRIMARY KEY CLUSTERED ([BillingCycleId] ASC)
);

