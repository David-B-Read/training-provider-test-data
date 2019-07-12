CREATE TABLE [dbo].[UKRLPData]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	[UKPRN] VARCHAR(50),
	[LegalName] VARCHAR(4000),
	[TradingName] VARCHAR(4000),
	[PrimaryVerificationSource] VARCHAR(255),
	[CompanyNumber] VARCHAR(255),
	[CharityNumber] VARCHAR(255), 
    [Status] VARCHAR(255) NULL
)
