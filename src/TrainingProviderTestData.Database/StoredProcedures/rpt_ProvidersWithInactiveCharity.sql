CREATE PROCEDURE [dbo].[rpt_ProvidersWithInactiveCharity]
AS
	SELECT
	ukrlp.[UKPRN],
	ukrlp.[LegalName],
	ukrlp.[TradingName],
	ukrlp.[PrimaryVerificationSource],
	ukrlp.[CompanyNumber],
	ukrlp.[CharityNumber],
	ukrlp.[Status],
	ccdata.DateRegistered,
	ccdata.DateRemoved
	FROM [dbo].[UKRLPData] ukrlp
	INNER JOIN CharityCommissionData ccdata
	ON ccdata.CharityNumber = ukrlp.CharityNumber
	WHERE ukrlp.charitynumber NOT IN ('')
	AND ukrlp.[status]  = 'Active'
	AND ccdata.DateRemoved IS NOT NULL
