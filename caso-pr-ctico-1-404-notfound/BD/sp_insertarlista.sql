CREATE PROCEDURE sp_InsertarListaVisualizacion
    @UsuarioId     INT,
    @IdApi         INT,
    @Prioridad     INT,
    @FechaAgregado DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO ListaVisualizaciones (UsuarioId, IdApi, Prioridad, FechaAgregado)
    VALUES (
        @UsuarioId,
        @IdApi,
        @Prioridad,
        ISNULL(@FechaAgregado, GETDATE())
    );

    SELECT SCOPE_IDENTITY() AS NuevoListaId;
END
GO
