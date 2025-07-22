CREATE PROCEDURE dbo.sp_EliminarUsuario
    @UsuarioId INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM dbo.Usuarios
     WHERE UsuarioId = @UsuarioId;

    SELECT @@ROWCOUNT AS FilasAfectadas;
END