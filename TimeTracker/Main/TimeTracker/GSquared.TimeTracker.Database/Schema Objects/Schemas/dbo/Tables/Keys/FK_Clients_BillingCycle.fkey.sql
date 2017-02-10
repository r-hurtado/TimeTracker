ALTER TABLE [dbo].[Clients]
    ADD CONSTRAINT [FK_Clients_BillingCycle] FOREIGN KEY ([BillingCycleId]) REFERENCES [dbo].[BillingCycle] ([BillingCycleId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

