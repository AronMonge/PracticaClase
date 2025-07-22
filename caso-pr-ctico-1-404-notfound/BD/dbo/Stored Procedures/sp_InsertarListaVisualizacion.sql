CREATE PROCEDURE dbo.sp_InsertarListaVisualizacion
    @UsuarioId     INT,
    @Tipo          NVARCHAR(50),
    @Titulo        NVARCHAR(255),
    @Detalle       NVARCHAR(500) = NULL,
    @FechaAgregado DATETIME      = NULL  -- si no se pasa, usa DEFAULT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.ListaVisualizacion
        (UsuarioId, Tipo, Titulo, Detalle, FechaAgregado)
    VALUES
        (@UsuarioId, @Tipo, @Titulo, @Detalle,
         ISNULL(@FechaAgregado, GETDATE()));

    SELECT SCOPE_IDENTITY();
END;