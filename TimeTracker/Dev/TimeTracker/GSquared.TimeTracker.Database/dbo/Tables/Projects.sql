CREATE TABLE [dbo].[Projects] (
    [ProjectId]           INT            IDENTITY (1, 1) NOT NULL,
    [ClientId]            INT            NOT NULL,
    [ProjectName]         NVARCHAR (150) NOT NULL,
    [BillingCode]         NVARCHAR (15)  NULL,
    [QuickbooksProjectId] NVARCHAR (250) NULL,
    [HourlyBillingRate]   DECIMAL (7, 2) NOT NULL,
    [IsActive]            BIT            CONSTRAINT [DF_Projects_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED ([ProjectId] ASC),
    CONSTRAINT [FK_ClientProject] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([ClientId])
);



