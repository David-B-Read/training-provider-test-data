CREATE PROCEDURE [dbo].[rpt_ProvidersWithCharityNumbers]
AS
	SELECT
	ukrlp.UKPRN,
	ukrlp.LegalName,
	ukrlp.TradingName,
	ukrlp.CompanyNumber,
	ukrlp.PrimaryVerificationSource,
	ukrlp.[Status] AS [UKRLP Status],
	ccdata.DateRegistered,
	ccdata.DateRemoved
	FROM UKRLPData ukrlp
	INNER JOIN CharityCommissionData ccdata
	ON ccdata.CharityNumber = ukrlp.CharityNumber
	ORDER BY ccdata.DateRegistered DESC
