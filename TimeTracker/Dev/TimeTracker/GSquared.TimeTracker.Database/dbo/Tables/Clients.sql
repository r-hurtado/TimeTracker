CREATE TABLE [dbo].[Clients] (
    [ClientId]       INT              IDENTITY (1, 1) NOT NULL,
    [ClientName]     NVARCHAR (MAX)   NOT NULL,
    [BillingTermsId] INT              NOT NULL,
    [BillingCycleId] INT              NOT NULL,
    [IsActive]       BIT              CONSTRAINT [DF_Clients_IsActive] DEFAULT ((1)) NOT NULL,
    [UserId]         UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED ([ClientId] ASC),
    CONSTRAINT [FK_ClientBillingTerm] FOREIGN KEY ([BillingTermsId]) REFERENCES [dbo].[BillingTerms] ([BillingTermsId]),
    CONSTRAINT [FK_Clients_BillingCycle] FOREIGN KEY ([BillingCycleId]) REFERENCES [dbo].[BillingCycle] ([BillingCycleId]), 
    CONSTRAINT [FK_Clients_aspnet_Users] FOREIGN KEY (UserId) REFERENCES aspnet_Users(UserId)
);




GO
CREATE NONCLUSTERED INDEX [IX_FK_ClientBillingTerm]
    ON [dbo].[Clients]([BillingTermsId] ASC);

