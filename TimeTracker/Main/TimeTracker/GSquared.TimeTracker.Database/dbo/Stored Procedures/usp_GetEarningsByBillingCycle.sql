
CREATE procedure [dbo].[usp_GetEarningsByBillingCycle]
(
	@asOfDate datetime = null
)

as

	select @asOfDate = isnull(@asOfDate, getdate())

	;with TotalMonthlyHours
	as
	(
		select bc.BillingCycleDescription, 
			sum(TotalTime) as TotalHours
		from TimeEntries te
			inner join Projects p
				on te.ProjectId = p.ProjectId
			inner join Clients c
				on p.ClientID = c.ClientID
			inner join BillingCycle bc
				on c.BillingCycleId = bc.BillingCycleId
		where bc.BillingCycleId = 1 
				and DateWorked between convert(datetime, convert(char(4), year(@asOfDate)) + '-' + convert(varchar(2), month(@asOfDate)) + '-01') and convert(datetime, convert(char(4), year(@asOfDate)) + '-' + convert(varchar(2), month(@asOfDate) + 1) + '-01')
		group by bc.BillingCycleDescription
	),
	TotalMidMonthHours
	as
	(
		select bc.BillingCycleDescription, 
			sum(TotalTime) as TotalHours
		from TimeEntries te
			inner join Projects p
				on te.ProjectId = p.ProjectId
			inner join Clients c
				on p.ClientID = c.ClientID
			inner join BillingCycle bc
				on c.BillingCycleId = bc.BillingCycleId
		where bc.BillingCycleId = 2
				and 
					DateWorked between convert(datetime, convert(char(4), year(@asOfDate)) + '-' + 
						case 
							when datepart(d, @asOfDate) > 15 then convert(varchar(2), month(@asOfDate))
							else convert(varchar(2), month(@asOfDate) - 1)
						end + '-16')
						and convert(datetime, convert(char(4), year(@asOfDate)) + '-' + 
						case 
							when datepart(d, @asOfDate) > 15 then convert(varchar(2), month(@asOfDate) + 1)
							else convert(varchar(2), month(@asOfDate))
						end + '-15')
		group by bc.BillingCycleDescription
	),
	TotalMonthlyAmount
	as
	(
		select bc.BillingCycleDescription, 
			sum(TotalTime) * HourlyBillingRate as TotalAmount
		from TimeEntries te
			inner join Projects p
				on te.ProjectId = p.ProjectId
			inner join Clients c
				on p.ClientID = c.ClientID
			inner join BillingCycle bc
				on c.BillingCycleId = bc.BillingCycleId
		where bc.BillingCycleId = 1 
				and DateWorked between convert(datetime, convert(char(4), year(@asOfDate)) + '-' + convert(varchar(2), month(@asOfDate)) + '-01') and convert(datetime, convert(char(4), year(@asOfDate)) + '-' + convert(varchar(2), month(@asOfDate) + 1) + '-01')
		group by bc.BillingCycleDescription, HourlyBillingRate
	),
	TotalMidMonthAmount
	as
	(
		select bc.BillingCycleDescription, 
			sum(TotalTime) * HourlyBillingRate as TotalAmount
		from TimeEntries te
			inner join Projects p
				on te.ProjectId = p.ProjectId
			inner join Clients c
				on p.ClientID = c.ClientID
			inner join BillingCycle bc
				on c.BillingCycleId = bc.BillingCycleId
		where bc.BillingCycleId = 2
				and 
					DateWorked between convert(datetime, convert(char(4), year(@asOfDate)) + '-' + 
						case 
							when datepart(d, @asOfDate) > 15 then convert(varchar(2), month(@asOfDate))
							else convert(varchar(2), month(@asOfDate) - 1)
						end + '-16')
						and convert(datetime, convert(char(4), year(@asOfDate)) + '-' + 
						case 
							when datepart(d, @asOfDate) > 15 then convert(varchar(2), month(@asOfDate) + 1)
							else convert(varchar(2), month(@asOfDate))
						end + '-15')
		group by bc.BillingCycleDescription, HourlyBillingRate
	)

	-- Get the Monthly Total
	select tmh.BillingCycleDescription, 
		tmh.TotalHours as Hours,
		Sum(tma.TotalAmount) as Amount
	from TotalMonthlyHours tmh
			inner join TotalMonthlyAmount tma
				on tmh.BillingCycleDescription = tma.BillingCycleDescription
	group by tmh.BillingCycleDescription, tmh.TotalHours


	union

	-- Get the Mid-month totals
	select tmh.BillingCycleDescription, 
		tmh.TotalHours as Hours,
		Sum(tma.TotalAmount) as Amount
	from TotalMidMonthHours tmh
			inner join TotalMidMonthAmount tma
				on tmh.BillingCycleDescription = tma.BillingCycleDescription
	group by tmh.BillingCycleDescription, tmh.TotalHours