﻿CREATE TABLE [dbo].[BillingTerms] (
    [BillingTermsId]          INT           IDENTITY (1, 1) NOT NULL,
    [BillingTermsDescription] NVARCHAR (25) NOT NULL,
    [NumberOfDaysToPay]       INT           NOT NULL
);

