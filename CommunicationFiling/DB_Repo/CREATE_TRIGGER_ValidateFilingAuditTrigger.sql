
CREATE TRIGGER ValidateFilingAuditTrigger
ON GEN.Filing

AFTER UPDATE, INSERT 
AS 
--** Si el ID de auditoria existe **--
IF ((select inserted.AuditId from inserted) > 0)
	BEGIN
		--** Valida si hubo UPDATE **--
		IF (EXISTS (SELECT * FROM DELETED))
		BEGIN
			UPDATE GEN.Audit 
			SET ModificationDate = GETDATE(),
				ModificationUserId = CreationUserId
			WHERE Id = (select inserted.AuditId from inserted)
		END
	END
--** Si el ID de auditoria no existe **--
ELSE
	BEGIN
		DECLARE @IdentityOutput TABLE (ID BIGINT)
		--** Crea un registro de auditoria con datos de modificacion **--
		IF EXISTS (SELECT * FROM DELETED)
			BEGIN
				INSERT INTO GEN.Audit (CreationDate, ModificationDate, CreationUserId, ModificationUserId, IsValid)
				OUTPUT inserted.Id INTO @IdentityOutput
				VALUES (GETDATE(), GETDATE(), (SELECT Id FROM [SEG].[User] WHERE FirstName like 'ADMIN'), (SELECT Id FROM [SEG].[User] WHERE FirstName like 'ADMIN'), 1)
			END
		--** Crea un registro de auditoria solo con datos de creacion **--
		ELSE
			BEGIN
				INSERT INTO GEN.Audit (CreationDate, CreationUserId, IsValid)
				OUTPUT inserted.Id INTO @IdentityOutput
				VALUES (GETDATE(), (SELECT Id FROM [SEG].[User] WHERE FirstName like 'ADMIN'), 1)
			END

		--** Actualiza el ID de auditoria **--
		UPDATE GEN.Filing
		SET AuditId = (select max(ID) from @IdentityOutput)
		WHERE Id = (select inserted.Id from inserted)
	END;
