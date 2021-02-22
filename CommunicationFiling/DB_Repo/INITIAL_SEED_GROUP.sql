	DECLARE @IdentityOutput TABLE (ID BIGINT)
	
	INSERT INTO [SEG].[User](FirstName, LastName, NumberID, Phonenumber, Email, IsValid)
	VALUES ('ADMIN', 'GENERAL', '10001000', '3005000000', 'admingeneral@alpha.com', 1) 

	INSERT INTO [GEN].[Audit](CreationDate, CreationUserId, IsValid)
	OUTPUT inserted.Id INTO @IdentityOutput
	VALUES (GETDATE(), (SELECT Id FROM [SEG].[User] WHERE NumberID = '10001000') , 1) 

	INSERT INTO ADM.CorrespondenceType(Code, TypeName, IsValid, AuditId)
	VALUES ('CE', 'CORRESPONDENCIA EXTERNA', 1, (select max(ID) from @IdentityOutput)) 

	INSERT INTO [GEN].[Audit](CreationDate, CreationUserId, IsValid)
	OUTPUT inserted.Id INTO @IdentityOutput
	VALUES (GETDATE(), (SELECT Id FROM [SEG].[User] WHERE NumberID = '10001000') , 1) 

	INSERT INTO ADM.CorrespondenceType(Code, TypeName, IsValid, AuditId)
	VALUES ('CI', 'CORRESPONDENCIA INTERNA', 1, (select max(ID) from @IdentityOutput)) 

	INSERT INTO [GEN].[Audit](CreationDate, CreationUserId, IsValid)
	OUTPUT inserted.Id INTO @IdentityOutput
	VALUES (GETDATE(), (SELECT Id FROM [SEG].[User] WHERE NumberID = '10001000') , 1) 

	INSERT INTO [SEG].[User](FirstName, LastName, NumberID, Phonenumber, Email, IsValid, AuditId)
	VALUES ('JUAN', 'ALVAREZ', '91815364', '3015000000', 'juan.alvarez@alpha.com', 1, (select max(ID) from @IdentityOutput)) 

	INSERT INTO [GEN].[Audit](CreationDate, CreationUserId, IsValid)
	OUTPUT inserted.Id INTO @IdentityOutput
	VALUES (GETDATE(), (SELECT Id FROM [SEG].[User] WHERE NumberID = '10001000') , 1) 

	INSERT INTO [SEG].[User](FirstName, LastName, NumberID, Phonenumber, Email, IsValid, AuditId)
	VALUES ('JACINTO', 'CASALLAS', '4509876', '3155000000', 'jacinto.casallas@alpha.com', 1, (select max(ID) from @IdentityOutput)) 

	INSERT INTO [GEN].[Audit](CreationDate, CreationUserId, IsValid)
	OUTPUT inserted.Id INTO @IdentityOutput
	VALUES (GETDATE(), (SELECT Id FROM [SEG].[User] WHERE NumberID = '10001000') , 1) 

	INSERT INTO [SEG].[User](FirstName, LastName, NumberID, Phonenumber, Email, IsValid, AuditId)
	VALUES ('ALBERTO', 'MARQUEZ', '1121456900', '3175000000', 'marquez_j@ymail.com', 1, (select max(ID) from @IdentityOutput)) 

	INSERT INTO [GEN].[Audit](CreationDate, CreationUserId, IsValid)
	OUTPUT inserted.Id INTO @IdentityOutput
	VALUES (GETDATE(), (SELECT Id FROM [SEG].[User] WHERE NumberID = '10001000') , 1) 

	INSERT INTO [SEG].[User](FirstName, LastName, NumberID, Phonenumber, Email, IsValid, AuditId)
	VALUES ('GUSTAVO ANDRES', 'CEPEDA', '1200400700', '3115000000', 'gustance@ymail.com', 1, (select max(ID) from @IdentityOutput)) 
