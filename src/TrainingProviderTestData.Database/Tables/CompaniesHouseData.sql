CREATE TABLE [dbo].[CompaniesHouseData]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	[CompanyNumber] NVARCHAR(255) NOT NULL,
	[CompanyName] NVARCHAR(255) NOT NULL,
	[IncorporationDate] DATETIME NULL,
	[DissolutionDate] DATETIME NULL,
	[CompanyCategory] NVARCHAR(255) NULL,
	[CompanyStatus] NVARCHAR(255) NULL,
	[CountryOfOrigin] NVARCHAR(255) NULL, 
    [DirectorsCount] INT NULL, 
    [PSCsCount] INT NULL
)
