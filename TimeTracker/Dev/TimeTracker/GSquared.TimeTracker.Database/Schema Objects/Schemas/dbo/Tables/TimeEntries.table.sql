﻿CREATE TABLE [dbo].[TimeEntries] (
    [TimeEntryId] INT              IDENTITY (1, 1) NOT NULL,
    [ProjectId]   INT              NOT NULL,
    [UserId]      UNIQUEIDENTIFIER NOT NULL,
    [FromTime]    DATETIME         NULL,
    [ToTime]      DATETIME         NULL,
    [TotalTime]   DECIMAL (4, 2)   NOT NULL,
    [DateWorked]  DATETIME         NOT NULL,
    [IsBillable]  BIT              NOT NULL,
    [Description] VARCHAR (250)    NULL,
    [CreatedAt]   DATETIME         NOT NULL,
    [CreatedBy]   NVARCHAR (25)    NOT NULL
);

