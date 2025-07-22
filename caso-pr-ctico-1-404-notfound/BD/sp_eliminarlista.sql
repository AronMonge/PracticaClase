CREATE PROCEDURE sp_EliminarListaVisualizacion
    @UsuarioId INT,
    @ListaId   INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM ListaVisualizaciones
    WHERE UsuarioId = @UsuarioId
      AND ListaId = @ListaId;

    SELECT @@ROWCOUNT AS FilasAfectadas;
END
GO
