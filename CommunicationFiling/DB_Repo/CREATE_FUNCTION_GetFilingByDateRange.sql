CREATE FUNCTION GetFilingByDateRange
(
	@InitialDate  date,
	@FinalDate    date
)
RETURNS TABLE
AS
RETURN
(
	SELECT GF.Consecutive AS CONSECUTIVO, 
			CONVERT(DATE,GA.CreationDate) AS FECHA,
			AC.TypeName AS TIPOCOMUNICADO,
			CONCAT(SUS.FirstName, ' ', SUS.LastName) AS REMITENTE, 
			CONCAT(SU.FirstName, ' ', SU.LastName) AS DESTINATARIO, 
			ISNULL(GF.Base64File, GF.StorageAddress) AS COMUNICADO
	FROM GEN.Audit GA
	INNER JOIN GEN.Filing GF ON GF.AuditId = GA.Id
	INNER JOIN [SEG].[User] SU ON GF.AddresseeUserId = SU.Id
	INNER JOIN [SEG].[User] SUS ON GF.SenderUserId = SUS.Id
	INNER JOIN ADM.CorrespondenceType AC ON AC.Id = GF.CorrespondenceTypeId
	WHERE CONVERT(DATE, GA.CreationDate) BETWEEN @InitialDate AND @FinalDate
)	
