CREATE PROCEDURE dbo.usp_GetInvoiceEntries(
	@clientName varchar(150),
	@fromDate datetime,
	@toDate datetime
)

AS

select Convert(varchar(10), DateWorked, 101) as DateWorked, 
	'Darren Boss' as Programmer,
	'Programming - Standard' as Item,
	[Description], 
	sum(TotalTime) as Hours,
	HourlyBillingRate as Rate,
	sum(TotalTime) * HourlyBillingRate as Amount
from TimeEntries te
	inner join Projects p
		on te.ProjectId = p.ProjectId
	inner join Clients c
		on p.ClientID = c.ClientID
where ClientName = @clientName
	and DateWorked between @fromDate and @toDate
group by DateWorked, [Description], HourlyBillingRate
order by DateWorked, [Description]