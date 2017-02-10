CREATE PROCEDURE [dbo].[usp_GetInvoiceHeader]
	@quickbooksProjectId varchar(255),
	@fromDate datetime,
	@toDate datetime
AS
	select Isnull(sum(p.HourlyBillingRate * t.TotalTime), 0) as Total,
			bt.BillingTermsDescription
	from Projects p
			inner join Clients c
				on p.ClientId = c.ClientId
			inner join BillingTerms bt
				on c.BillingTermsId = bt.BillingTermsId
			left join TimeEntries t
				on t.ProjectId = p.ProjectId
	where IsNull(t.DateWorked, @fromDate) between @fromDate and @toDate
			and p.QuickbooksProjectId = @quickbooksProjectId
	group by QuickbooksProjectId, BillingTermsDescription