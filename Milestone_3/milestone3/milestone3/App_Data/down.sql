﻿ALTER TABLE [dbo].[Topics] DROP CONSTRAINT [FK_dbo.Categories]
ALTER TABLE [dbo].[Categories] DROP CONSTRAINT [PK_dbo.Categories]
ALTER TABLE [dbo].[Topics] DROP CONSTRAINT [PK_dbo.Topics]
DROP TABLE [dbo].[Categories]
DROP TABLE [dbo].[Topics]