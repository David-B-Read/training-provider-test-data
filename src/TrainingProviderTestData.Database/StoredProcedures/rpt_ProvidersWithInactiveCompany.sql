CREATE PROCEDURE [dbo].[rpt_ProvidersWithInactiveCompany]
AS
	SELECT DISTINCT
	ukrlp.[UKPRN],
	ukrlp.[LegalName],
	ukrlp.[TradingName],
	ukrlp.[PrimaryVerificationSource],
	ukrlp.[CompanyNumber],
	ukrlp.[CharityNumber],
	ukrlp.[Status] AS [UKRLP Status],
	chdata.CompanyStatus AS [Companies House Status],
	chdata.IncorporationDate,
	chdata.DissolutionDate
	FROM [dbo].[UKRLPData] ukrlp
	INNER JOIN CompaniesHouseData chdata
	ON chdata.CompanyNumber = ukrlp.CompanyNumber
	WHERE ukrlp.CompanyNumber NOT IN ('')
	AND ukrlp.[status]  = 'Active'
	AND chdata.CompanyStatus NOT IN ('Active')
