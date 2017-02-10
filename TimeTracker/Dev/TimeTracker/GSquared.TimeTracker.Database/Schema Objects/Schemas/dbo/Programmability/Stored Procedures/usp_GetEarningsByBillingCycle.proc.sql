
create procedure [dbo].[usp_GetEarningsByBillingCycle]

as

	select bc.BillingCycleDescription, 
		sum(TotalTime) as Hours,
		sum(TotalTime) * HourlyBillingRate as Amount
	from TimeEntries te
		inner join Projects p
			on te.ProjectId = p.ProjectId
		inner join Clients c
			on p.ClientID = c.ClientID
		inner join BillingCycle bc
			on c.BillingCycleId = bc.BillingCycleId
	where (bc.BillingCycleId = 2 and DateWorked between convert(datetime, convert(char(4), year(getdate())) + '-' + convert(varchar(2), month(getdate())) + '-15') and convert(datetime, convert(char(4), year(getdate())) + '-' + convert(varchar(2), month(getdate()) + 1) + '-15'))
		or (bc.BillingCycleId = 1 and DateWorked between convert(datetime, convert(char(4), year(getdate())) + '-' + convert(varchar(2), month(getdate())) + '-01') and convert(datetime, convert(char(4), year(getdate())) + '-' + convert(varchar(2), month(getdate()) + 1) + '-01'))
	group by bc.BillingCycleDescription, HourlyBillingRate


