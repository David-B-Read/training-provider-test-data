CREATE TABLE [dbo].[UKRLPData]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	[UKPRN] NVARCHAR(50),
	[LegalName] NVARCHAR(4000),
	[TradingName] NVARCHAR(4000),
	[PrimaryVerificationSource] NVARCHAR(255),
	[CompanyNumber] NVARCHAR(255),
	[CharityNumber] NVARCHAR(255), 
    [Status] NVARCHAR(255) NULL
)
