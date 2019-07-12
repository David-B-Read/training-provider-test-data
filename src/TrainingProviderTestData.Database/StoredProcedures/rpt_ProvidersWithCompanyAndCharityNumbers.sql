CREATE PROCEDURE [dbo].[rpt_ProvidersWithCompanyAndCharityNumbers]
AS
	SELECT
	ukrlp.UKPRN,
	ukrlp.LegalName,
	ukrlp.TradingName,
	ukrlp.CompanyNumber,
	ukrlp.PrimaryVerificationSource,
	ukrlp.[Status] AS [UKRLP Status],
	chdata.IncorporationDate AS [Companies House Incorporation Date],
	chdata.DissolutionDate AS [Companies House Dissolution Date],
	chdata.CompanyCategory,
	chdata.CompanyStatus AS [Companies House Status],
	ccdata.DateRegistered AS [Charity Commission Date Registered],
	ccdata.DateRemoved AS [Charity Commission Date Removed]
	FROM UKRLPData ukrlp
	INNER JOIN CompaniesHouseData chdata
	ON chdata.CompanyNumber = ukrlp.CompanyNumber
	INNER JOIN CharityCommissionData ccdata
	ON ccdata.CharityNumber = ukrlp.CharityNumber
	ORDER BY chdata.IncorporationDate DESC
