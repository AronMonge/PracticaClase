CREATE PROCEDURE dbo.sp_EliminarListaVisualizacion
    @UsuarioId INT,
    @ListaId   INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.ListaVisualizacion
    WHERE UsuarioId = @UsuarioId
      AND ListaId   = @ListaId;

    SELECT @@ROWCOUNT;  -- >0 si borró algo
END;