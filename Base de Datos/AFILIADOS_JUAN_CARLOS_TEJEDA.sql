

CREATE DATABASE AFILIADOS_JUAN_CARLOS_TEJEDA
go

use  AFILIADOS_JUAN_CARLOS_TEJEDA
GO

CREATE TABLE ESTATUS(
Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL
,Estatus varchar(10)
)
GO
INSERT INTO ESTATUS(Estatus) VALUES('ACTIVO'),('INACTIVO')
GO

CREATE TABLE AFILIADOS(
Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL
,Nombres VARCHAR(50)
,Apellidos VARCHAR(50)
,Fecha_Nacimiento DATETime
,Sexo VARCHAR(1)
,Cedula VARCHAR(11)
,Nss VARCHAR(9)
,Fecha_Registro DATETIME
,Monto_Consumido DECIMAL(18,2)
,Id_Estatus INT
,Id_Planes INT
)
ALTER TABLE AFILIADOS ADD CONSTRAINT AFILIADOS_Id_Estatus FOREIGN KEY (Id_Estatus) REFERENCES ESTATUS (Id)
GO

CREATE TABLE PLANES(
Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL
,Planes VARCHAR(100)
,Monto_Cobertura DECIMAL(18,2)
,Fecha_Registro DATETIME
,Id_Estatus INT
)
ALTER TABLE PLANES ADD CONSTRAINT PLANES_Id_Estatus FOREIGN KEY (Id_Estatus) REFERENCES ESTATUS (Id)
GO
--temporal
INSERT INTO PLANES(Planes,Monto_Cobertura,Fecha_Registro,Id_Estatus) VALUES
('Plan 1',200000.0,GETDATE(),1)
,('Plan 2',100000.0,GETDATE()-1,1)
go




CREATE PROCEDURE AFILIADOS_INSERT
(
@Nombres VARCHAR(50)
,@Apellidos VARCHAR(50)
,@Fecha_Nacimiento DATETIME
,@Sexo VARCHAR(1)
,@Cedula VARCHAR(11)
,@Nss VARCHAR(9)
,@Fecha_Registro DATETIME
,@Monto_Consumido DECIMAL(18,2)
,@Id_Estatus INT
,@Id_Planes INT
) 
AS 
BEGIN
INSERT INTO AFILIADOS(
Nombres
, Apellidos
, Fecha_Nacimiento
, Sexo
, Cedula
, Nss
, Fecha_Registro
, Monto_Consumido
, Id_Estatus
, Id_Planes) 
VALUES(
@Nombres 
,@Apellidos 
,@Fecha_Nacimiento
,@Sexo
,@Cedula 
,@Nss 
,@Fecha_Registro 
,@Monto_Consumido 
,@Id_Estatus 
,@Id_Planes 
)  
END;
go

CREATE PROCEDURE AFILIADOS_UPDATE
(
@Id INT
,@Nombres VARCHAR(50)
,@Apellidos VARCHAR(50)
,@Fecha_Nacimiento DATEtime
,@Sexo VARCHAR(1)
,@Cedula VARCHAR(11)
,@Nss VARCHAR(9)
,@Fecha_Registro DATETIME
,@Monto_Consumido DECIMAL(18,2)
,@Id_Estatus INT
,@Id_Planes INT
) 
AS 
BEGIN
	UPDATE AFILIADOS
	SET Nombres = @Nombres
	, Apellidos  =@Apellidos
	, Fecha_Nacimiento = @Fecha_Nacimiento
	, Sexo = @Sexo
	, Cedula = @Cedula 
	, Nss = @Nss 
	, Fecha_Registro = @Fecha_Registro
	, Monto_Consumido  = @Monto_Consumido
	, Id_Estatus = @Id_Estatus
	, Id_Planes = @Id_Planes
	WHERE Id = @Id
 
END;

go

CREATE PROCEDURE AFILIADOS_CAMBIAR_ESTATUS
@Id INT
AS
BEGIN   
   UPDATE AFILIADOS  
   SET Id_Estatus = (SELECT top 1 Id from ESTATUS where Id != Id_Estatus)
   WHERE Id = @Id;
END;

go


CREATE PROCEDURE AFILIADOS_SELECT
(
@Id INT = NULL
)
AS
Begin
SELECT Id
, Nombres
, Apellidos
, Fecha_Nacimiento
, Sexo
, Cedula
, Nss
, Fecha_Registro
, Monto_Consumido
, Id_Estatus
, Id_Planes
FROM AFILIADOS
WHERE (Id= @Id  OR @Id IS NULL OR @ID = 0)
End

GO
CREATE PROCEDURE AFILIADOS_CONSULTA
(
@Id INT = NULL
)
AS
Begin
SELECT AFILIADOS.Id
, Nombres
, Apellidos
, Fecha_Nacimiento
, Sexo
, Cedula
, Nss
, AFILIADOS.Fecha_Registro
, Monto_Consumido
, ESTATUS.Estatus
, PLANES.Planes
, PLANES.Monto_Cobertura
, (PLANES.Monto_Cobertura - Monto_Consumido) as Monto_Restante
FROM AFILIADOS
INNER JOIN ESTATUS ON ESTATUS.Id = AFILIADOS.Id_Estatus
INNER JOIN PLANES ON PLANES.Id = AFILIADOS.Id_Planes
WHERE (AFILIADOS.Id = @Id  OR @Id IS NULL OR @ID = 0)
End

GO 
CREATE PROCEDURE ESTATUS_SELECT
(
@Id INT = NULL
)
AS
Begin
SELECT Id
, Estatus
FROM ESTATUS
WHERE (Id= @Id  OR @Id IS NULL OR @ID = 0)
End

GO 
CREATE PROCEDURE PLANES_SELECT
(
@Id INT = NULL
)
AS
Begin
SELECT Id
, Planes
, Monto_Cobertura
, Fecha_Registro
, Id_Estatus
FROM PLANES
WHERE (Id= @Id  OR @Id IS NULL OR @ID = 0)
End
