ALTER TABLE [dbo].[TimeEntries]
    ADD CONSTRAINT [FK_TimeEntries_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([ProjectId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

