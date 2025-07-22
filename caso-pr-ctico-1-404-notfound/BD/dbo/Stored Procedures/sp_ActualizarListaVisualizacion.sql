CREATE PROCEDURE dbo.sp_ActualizarListaVisualizacion
    @ListaId   INT,
    @Tipo      NVARCHAR(50),
    @Titulo    NVARCHAR(255),
    @Detalle   NVARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.ListaVisualizacion
    SET
        Tipo    = @Tipo,
        Titulo  = @Titulo,
        Detalle = @Detalle
    WHERE ListaId = @ListaId;
    
    SELECT @@ROWCOUNT;  -- devuelve 1 si actualizó, 0 si no existía
END;