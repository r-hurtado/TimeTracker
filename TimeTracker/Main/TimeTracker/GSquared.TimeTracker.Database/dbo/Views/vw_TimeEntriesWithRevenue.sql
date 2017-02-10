
CREATE VIEW [dbo].[vw_TimeEntriesWithRevenue]
AS
SELECT        dbo.TimeEntries.TimeEntryId, dbo.TimeEntries.UserId, dbo.TimeEntries.TotalTime, dbo.TimeEntries.DateWorked, dbo.TimeEntries.IsBillable, 
                         dbo.Projects.HourlyBillingRate, DATEPART(ww, dbo.TimeEntries.DateWorked) AS Week, 
                         CASE IsBillable WHEN 1 THEN dbo.TimeEntries.TotalTime * dbo.Projects.HourlyBillingRate ELSE 0 END AS TotalRevenue
FROM            dbo.TimeEntries INNER JOIN
						 dbo.Projects ON dbo.TimeEntries.ProjectId = dbo.Projects.ProjectId
								INNER JOIN
                         dbo.Clients ON dbo.Projects.ClientId = dbo.Clients.ClientId