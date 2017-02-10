CREATE PROCEDURE dbo.usp_GetInvoiceEntries(
	@quickbooksProjectId varchar(255),
	@fromDate datetime,
	@toDate datetime,
	@includeProjectNameInDescription bit
)

AS

select Convert(varchar(10), DateWorked, 101) as DateWorked, 
	case @includeProjectNameInDescription
		when 1 then [ProjectName] + ' - ' + [Description]
		else [Description]
	end as Description, 
	sum(TotalTime) as Hours,
	HourlyBillingRate as Rate,
	sum(TotalTime) * HourlyBillingRate as Amount
from TimeEntries te
	inner join Projects p
		on te.ProjectId = p.ProjectId
	inner join Clients c
		on p.ClientID = c.ClientID
where p.QuickbooksProjectId = @quickbooksProjectId
	and DateWorked between @fromDate and @toDate
group by DateWorked, [Description], [ProjectName], HourlyBillingRate
order by DateWorked, [Description]