CREATE TABLE [dbo].[Tbl_Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[APaterno] [nvarchar](100) NOT NULL,
	[AMaterno] [nvarchar](100) NOT NULL,
	[Correo] [nvarchar](100) NOT NULL,
	[Contrasena] [nvarchar](150) NOT NULL,
	CONSTRAINT UC_Correo UNIQUE (Correo),
	PRIMARY KEY (IdUsuario)
);

CREATE TABLE [dbo].[RegistroEntradaSalida](
	[IdTabla] [int] IDENTITY(1,1) NOT NULL,
	[FechaHora] [datetime] NOT NULL DEFAULT (getdate()),
	[TipoRegistro] [nvarchar](1) NOT NULL,
	[IdUsuario] [int] NULL,
	PRIMARY KEY (IdTabla)
);
----------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[sp_ActualizarUsuario]
@IdUsuario int,
@Nombre nvarchar(100),
@APaterno nvarchar(100),
@AMaterno nvarchar(100),
@Correo nvarchar(100),
@Contrasena nvarchar(150)

AS
BEGIN TRY
	SET NOCOUNT ON;	
	UPDATE [dbo].[Tbl_Usuario]
	   SET [Nombre] = @Nombre
		  ,[APaterno] = @APaterno
		  ,[AMaterno] = @AMaterno
		  ,[Correo] = @Correo
		  ,[Contrasena] = @Contrasena
	 WHERE IdUsuario = @IdUsuario;
END TRY
BEGIN CATCH
    SELECT
    ERROR_NUMBER() AS NumeroError,
    ERROR_MESSAGE() AS MensajeError;
END CATCH
GO
----------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[sp_EliminarUsuario]
    @IdUsuario int
AS
BEGIN
	SET NOCOUNT ON;
	
	DELETE FROM [dbo].[Tbl_Usuario]
    WHERE IdUsuario = @IdUsuario
END
GO
----------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[sp_InsertarUsuario]
@IdUsuario int = null,
@Nombre nvarchar(100),
@APaterno nvarchar(100),
@AMaterno nvarchar(100),
@Correo nvarchar(100),
@Contrasena nvarchar(150)

AS
BEGIN TRY
	SET NOCOUNT ON;
	INSERT INTO [dbo].[Tbl_Usuario]
			   ([Nombre]
			   ,[APaterno]
			   ,[AMaterno]
			   ,[Correo]
			   ,[Contrasena])
		 VALUES
			   (@Nombre
			   ,@APaterno
			   ,@AMaterno
			   ,@Correo
			   ,@Contrasena)
    SELECT SCOPE_IDENTITY() AS IdUsuario
END TRY
BEGIN CATCH
    SELECT
    ERROR_NUMBER() AS NumeroError,
    ERROR_MESSAGE() AS MensajeError;
END CATCH
GO
----------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[sp_ObtenerCredenciales]
AS  
BEGIN  
   SELECT IdUsuario,Nombre,Correo,Contrasena 
   FROM Tbl_Usuario;
END
GO
----------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[sp_ObtenerRegistroAsistencia]
AS  
BEGIN  
	SELECT u.IdUsuario, u.Nombre, u.APaterno, u.AMaterno, r.FechaHora, r.TipoRegistro
	FROM Tbl_Usuario u
	INNER JOIN RegistroEntradaSalida r ON u.IdUsuario = r.IdUsuario
	--where u.IdUsuario = 4;
END
GO
----------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[sp_ObtenerTodoRegistrosES]
AS  
BEGIN  

   SELECT *
   FROM [dbo].[RegistroEntradaSalida]

END
GO
----------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[sp_ObtenerTodosUsuarios]
AS  
BEGIN  

   SELECT *
   FROM Tbl_Usuario

END
GO
----------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[sp_RegistrarEntrada]
@IdTabla int = null,
@TipoRegistro nvarchar(1),
@IdUsuario int
AS
BEGIN TRY	
	SET NOCOUNT ON;
	INSERT INTO [dbo].[RegistroEntradaSalida]
				([TipoRegistro]
				,[IdUsuario])
			VALUES
				(@TipoRegistro,
				@IdUsuario)
END TRY
BEGIN CATCH
    SELECT
    ERROR_NUMBER() AS NumeroError,
    ERROR_MESSAGE() AS MensajeError;
END CATCH
GO
----------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[sp_RegistrarEntradaSalida]
@IdTabla int = null,
@TipoRegistro nvarchar(1),
@IdUsuario int,
@Resultado nvarchar(100) OUTPUT
AS
BEGIN TRY	
	IF EXISTS (SELECT IdUsuario FROM Tbl_Usuario WHERE IdUsuario = @IdUsuario)
		BEGIN 
			--SET NOCOUNT ON;
			INSERT INTO [dbo].[RegistroEntradaSalida]
					   ([TipoRegistro]
					   ,[IdUsuario])
				 VALUES
					   (@TipoRegistro,
						@IdUsuario)
			SET @Resultado = 'EXISTE'
		END
	ELSE
		SET @Resultado = 'NOEXISTE'	
END TRY
BEGIN CATCH
    SELECT
    ERROR_NUMBER() AS NumeroError,
    ERROR_MESSAGE() AS MensajeError;
END CATCH
----------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[sp_ValidarExisteUsuario]
    --@IdUsuario int,
	@Correo nvarchar(100),
	@Resultado nvarchar(100) OUTPUT
AS
BEGIN
	IF EXISTS (SELECT Correo FROM Tbl_Usuario WHERE Correo = LTRIM(RTRIM(@Correo)))

		--Ya existe un usuario registrado con ese correo = 000
		SET @Resultado = 'true'
	ELSE
		--El usuario no existe = 111
		SET @Resultado = 'false'
END
GO
----------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[sp_VerificarExisteUsuario]
@IdUsuario int,
@Resultado nvarchar(100) OUTPUT
AS
BEGIN TRY	
	IF EXISTS (SELECT IdUsuario FROM Tbl_Usuario WHERE IdUsuario = @IdUsuario)
		BEGIN 
			SET @Resultado = 'EXISTE'
		END
	ELSE
		SET @Resultado = 'NOEXISTE'	
END TRY
BEGIN CATCH
    SELECT
    ERROR_NUMBER() AS NumeroError,
    ERROR_MESSAGE() AS MensajeError;
END CATCH
GO
----------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[sp_VerificarRegistroEntrada]
    @IdUsuario int,
	@TipoRegistro nvarchar(1),
	@Resultado nvarchar(100) OUTPUT
AS
BEGIN TRY
	IF EXISTS (select IdUsuario, TipoRegistro from RegistroEntradaSalida where TipoRegistro = @TipoRegistro and IdUsuario = @IdUsuario)
		BEGIN 
			--El usuario ya registro la entrada.
			SET @Resultado = 'ENTRADAREGISTRADA'
		END
	ELSE
		--El usuario no ha registro entrada.
		SET @Resultado = 'SINENTRADAREGISTRADA'
END TRY
BEGIN CATCH
    SELECT
    ERROR_NUMBER() AS NumeroError,
    ERROR_MESSAGE() AS MensajeError;
END CATCH
GO
---***************************PRUEBAS***************************
---***************************PRUEBAS***************************
---***************************PRUEBAS***************************
SELECT GETDATE()
--SELECT * FROM RegistroEntradaSalida
SELECT * FROM RegistroEntradaSalida
WHERE FechaHora >= CONVERT(datetime, convert(varchar(10), GETDATE() ,120), 120)
AND   FechaHora <  DATEADD(day, 1, convert(datetime, convert(varchar(10), getdate(), 120), 120))

SELECT * FROM Tbl_Usuario u
INNER JOIN RegistroEntradaSalida r ON u.IdUsuario = r.IdUsuario;

SELECT u.IdUsuario, CONCAT(u.Nombre, ' ', u.APaterno, ' ', u.AMaterno) as 'Nombre Completo', r.FechaHora, r.TipoRegistro
FROM Tbl_Usuario u
INNER JOIN RegistroEntradaSalida r ON u.IdUsuario = r.IdUsuario
where u.IdUsuario = 4;

--------------------------------------------------------------------------------
Declare @IdUsuario int,
		@TipoRegistro nvarchar(1),
		@Resultado nvarchar(100)
set @IdUsuario = 53;
set @TipoRegistro = '1';

IF EXISTS (
	select IdUsuario, TipoRegistro from [dbo].[RegistroEntradaSalida]
	where TipoRegistro = @TipoRegistro and IdUsuario = @IdUsuario)

	--El usuario ya registro la entrada.
	SET @Resultado = 'ENTRADAREGISTRADA'
ELSE
	--El usuario no ha registro entrada.
	SET @Resultado = 'SINENTRADAREGISTRADA'

PRINT @Resultado 
--------------------------------------------------------------------------------
Declare @Resultado nvarchar(100)
EXEC sp_RegistrarEntradaSalida
		@TipoRegistro = '2',
		@IdUsuario = 55,
		@Resultado = @Resultado OUTPUT
PRINT @Resultado

Declare @Resultado nvarchar(100)
	IF EXISTS (SELECT Correo FROM Tbl_Usuario WHERE Correo = @Correo)
		SET @Resultado = 'Ya existe un usuario registrado.'
	ELSE
		SET @Resultado = 'El usuario no existe.'
PRINT @Resultado

-----------------
Declare @Correo nvarchar(100),
		@Resultado nvarchar(100)

EXEC	sp_ValidarExisteUsuario
		@Correo = ' test',
		@Resultado = @Resultado OUTPUT
PRINT @Resultado
---------------------------
--Declare @ResultMessage nvarchar(250)
EXEC	[dbo].[sp_InsertarUsuario]		
		@Nombre = 'lalo',
		@APaterno = 'mar',
		@AMaterno = 'esp',
		@Correo = 'test00',
		@Contrasena = '123'--,
		--@ResultMessage = @ResultMessage OUTPUT
--PRINT @ResultMessage
--SELECT @IdUsuario