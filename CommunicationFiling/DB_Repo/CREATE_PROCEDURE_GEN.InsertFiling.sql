
/****************************************************************************************
   $Archivo: InsertFiling
   $Autor:  John Heiller Martinez Cubillos                      
   $Fecha solicitud:       
   $Fecha Creación:  19-02-2021      
   $Solicitante:   
   $Objetivo: Registrar una radicacion de comunicacion devolviendo el consecutivo respectivo
*****************************************************************************************/
CREATE PROCEDURE GEN.InsertFiling
     @StorageAddress NVARCHAR(1000),
     @Base64File NVARCHAR(MAX),
 	 @SenderUserId BIGINT,
 	 @AddresseeUserId BIGINT,
 	 @CorrespondenceTypeId BIGINT,
	 @AuditId BIGINT,
	 @IsValid BIT,
	 @Consecutive NVARCHAR(15) OUTPUT
AS 
SET NOCOUNT ON;

BEGIN
	-- ** Declara varaibles internas ** --
	DECLARE @COUNT_FILING BIGINT
	DECLARE @CODE_COMTYPE NVARCHAR(5)

	-- ** Consulta parametrica de tipos de correspondencia ** --
	SELECT @COUNT_FILING = COUNT(*) FROM GEN.Filing FI WHERE FI.CorrespondenceTypeId = @CorrespondenceTypeId
	SELECT @CODE_COMTYPE = CT.Code FROM ADM.CorrespondenceType CT WHERE CT.Id = @CorrespondenceTypeId
	SET @COUNT_FILING = @COUNT_FILING + 1
	
	-- ** Valida y crea formato de nuevo consecutivo ** --
	IF (@COUNT_FILING < 100000000)
		BEGIN
			SET @Consecutive = CONCAT(@CODE_COMTYPE, RIGHT(100000000 + @COUNT_FILING, 8)) 
		END
	ELSE
		BEGIN
			SET @Consecutive = CONCAT(@CODE_COMTYPE, CAST(@COUNT_FILING AS NVARCHAR))
		END

	-- ** Evalua que el consecutivo generado no exista ** --
	WHILE ((SELECT COUNT(*) FROM GEN.Filing FI WHERE FI.Consecutive LIKE @Consecutive ) > 0)  
		BEGIN
			SET @COUNT_FILING = @COUNT_FILING + 1
			IF @COUNT_FILING < 100000000
				BEGIN
					SET @Consecutive = CONCAT(@CODE_COMTYPE, RIGHT(100000000 + @COUNT_FILING, 8))
				END
			ELSE
				BEGIN
					SET @Consecutive = CONCAT(@CODE_COMTYPE, CAST(@COUNT_FILING AS NVARCHAR))
				END
		END  

	-- ** Registra la radicacion del comunicado ** --
	INSERT INTO GEN.Filing (Consecutive, CorrespondenceTypeId, StorageAddress, Base64File, SenderUserId, AddresseeUserId, IsValid, AuditId)
	VALUES (@Consecutive, @CorrespondenceTypeId, @StorageAddress, @Base64File, @SenderUserId, @AddresseeUserId, @IsValid, @IsValid)

END;