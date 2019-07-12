CREATE PROCEDURE [dbo].[rpt_ProvidersWithCompanyNumbers]
AS
	SELECT
	ukrlp.UKPRN,
	ukrlp.LegalName,
	ukrlp.TradingName,
	ukrlp.CompanyNumber,
	ukrlp.PrimaryVerificationSource,
	ukrlp.[Status] AS [UKRLP Status],
	chdata.IncorporationDate,
	chdata.DissolutionDate,
	chdata.CompanyCategory,
	chdata.CompanyStatus AS [Companies House Status]
	FROM UKRLPData ukrlp
	INNER JOIN CompaniesHouseData chdata
	ON chdata.CompanyNumber = ukrlp.CompanyNumber
	ORDER BY chdata.IncorporationDate DESC
