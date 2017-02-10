ALTER TABLE [dbo].[Projects]
    ADD CONSTRAINT [FK_ClientProject] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([ClientId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

