﻿CREATE TABLE [dbo].[CharityCommissionData]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	[CharityName] VARCHAR(1000),
	[CharityNumber] VARCHAR(255),
	[DateRegistered] DATETIME NULL,
	[DateRemoved] DATETIME NULL
)
