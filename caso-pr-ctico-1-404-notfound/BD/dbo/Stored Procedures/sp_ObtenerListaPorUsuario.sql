CREATE PROCEDURE dbo.sp_ObtenerListaPorUsuario
    @UsuarioId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ListaId,
        UsuarioId,
        Tipo,
        Titulo,
        Detalle,
        FechaAgregado
    FROM dbo.ListaVisualizacion
    WHERE UsuarioId = @UsuarioId
    ORDER BY FechaAgregado DESC;
END;