CREATE PROCEDURE [dbo].[usp_GetITIMReport] (@clientId int, @month int, @year int)
AS
BEGIN
	
	select DateWorked,
		'adev RG  ' +  Left(Isnull(BillingCode, '22025040') + '        ', 8) + Left(ProjectName + '              ', 14) + Left(Isnull([Description], '') + '                         ', 25) + Left(Left('0' + cast(Sum(TotalTime) as varchar(10)), 2) + Left(Replace(cast((Sum(TotalTime) % 1) * 60 as varchar(50)), '.', ''), 2), 4) as ITIMLine
	from TimeEntries te
		inner join Projects p
			on te.ProjectId = p.ProjectId
		inner join Clients c
			on p.ClientId = c.ClientId
	where p.ClientId = @clientId
		and DatePart(m, DateWorked) = @month
		and DatePart(yyyy, DateWorked) = @year
	group by BillingCode, ProjectName, [Description], DateWorked
	order by DateWorked
END