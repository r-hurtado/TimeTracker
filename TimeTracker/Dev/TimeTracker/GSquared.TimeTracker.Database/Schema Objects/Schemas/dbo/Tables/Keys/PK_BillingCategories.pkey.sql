﻿ALTER TABLE [dbo].[BillingCategories]
    ADD CONSTRAINT [PK_BillingCategories] PRIMARY KEY CLUSTERED ([BillingCategoryId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
