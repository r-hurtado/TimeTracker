CREATE PROCEDURE [dbo].[usp_GetWeeklyTimesheet] ( @weekOf DateTime )

AS

select ClientName + ':' + ProjectName as Job, 
	'Programming - Standard' as ServiceItem, 
	[Description], 
	case DatePart(dw, DateWorked)
		when 1 then sum(TotalTime)
		else NULL
	end as TotalTimeSunday,
	case DatePart(dw, DateWorked)
		when 2 then sum(TotalTime)
		else NULL
	end as TotalTimeMonday,
	case DatePart(dw, DateWorked)
		when 3 then sum(TotalTime)
		else NULL
	end as TotalTimeTuesday,
	case DatePart(dw, DateWorked)
		when 4 then sum(TotalTime)
		else NULL
	end as TotalTimeWednesday,
	case DatePart(dw, DateWorked)
		when 5 then sum(TotalTime)
		else NULL
	end as TotalTimeThursday,
	case DatePart(dw, DateWorked)
		when 6 then sum(TotalTime)
		else NULL
	end as TotalTimeFriday,
	case DatePart(dw, DateWorked)
		when 7 then sum(TotalTime)
		else NULL
	end as TotalTimeSaturday
from TimeEntries te
	inner join Projects p
		on te.ProjectId = p.ProjectId
	inner join Clients c
		on p.ClientID = c.ClientID
where ClientName not in('ODOT', 'ODFW')
group by ClientName, ProjectName, [Description], DateWorked, DatePart(wk, DateWorked)
having DatePart(wk, DateWorked) = DatePart(wk, @weekOf)
order by ClientName, ProjectName, [Description]