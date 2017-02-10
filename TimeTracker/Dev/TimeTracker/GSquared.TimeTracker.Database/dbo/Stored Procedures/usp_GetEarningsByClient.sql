CREATE PROCEDURE dbo.usp_GetEarningsByClient
(
	@mon int,
	@yr int
)

AS

select ClientName, TotalTime * HourlyBillingRate as TotalMoney
from Clients c
	inner join Projects p
		on c.ClientId = p.ClientId
	inner join (
		select ProjectId, Sum(TotalTime) as TotalTime
		from TimeEntries
		where Month(DateWorked) = @mon
				and Year(DateWorked) = @yr
		group by ProjectId
	) t
		on p.ProjectId = t.ProjectId
order by ClientName