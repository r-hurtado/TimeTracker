CREATE PROCEDURE [dbo].[usp_GetTimesheetReportDataByWeek]
	(
	@fromDate datetime,
	@toDate datetime
	)
AS
	select dt.START_OF_WEEK_STARTING_SUN_DATE as [Week],
			Sum(TotalTime) as TotalTime,
			Sum(TotalRevenue) as TotalRevenue,
			1875.0 as RevenueGoal,
			25.0 as TimeGoal
	from vw_TimeEntriesWithRevenue e
		inner join F_TABLE_DATE(@fromDate, @toDate) dt
			on e.DateWorked = dt.[Date]
	where e.DateWorked between @fromDate and DateAdd(d, 1, @toDate)
	group by dt.START_OF_WEEK_STARTING_SUN_DATE
