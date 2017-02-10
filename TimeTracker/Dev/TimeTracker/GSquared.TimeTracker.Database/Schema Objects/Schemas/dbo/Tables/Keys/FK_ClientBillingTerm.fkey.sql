ALTER TABLE [dbo].[Clients]
    ADD CONSTRAINT [FK_ClientBillingTerm] FOREIGN KEY ([BillingTermsId]) REFERENCES [dbo].[BillingTerms] ([BillingTermsId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

