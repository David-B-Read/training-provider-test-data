CREATE TABLE [dbo].[CompaniesHouseData]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	[CompanyNumber] NVARCHAR(255) NOT NULL,
	[CompanyName] NVARCHAR(255) NOT NULL,
	[IncorporationDate] NVARCHAR(255) NULL
)
