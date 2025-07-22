CREATE PROCEDURE dbo.sp_InsertarUsuario
    @Rol           NVARCHAR(20),
    @NombreUsuario NVARCHAR(50),
    @Email         NVARCHAR(100),
    @Contrasena    NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.Usuarios (Rol, NombreUsuario, Email, Contrasena)
    VALUES (@Rol, @NombreUsuario, @Email, @Contrasena);

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NuevoUsuarioId;
END