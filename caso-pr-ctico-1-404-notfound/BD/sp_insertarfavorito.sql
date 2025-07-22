CREATE PROCEDURE sp_InsertarFavorito
    @UsuarioId    INT,
    @IdApi        INT,
    @FechaFavorito DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Favoritos (UsuarioId, IdApi, FechaFavorito)
    VALUES (
        @UsuarioId,
        @IdApi,
        ISNULL(@FechaFavorito, GETDATE())
    );

    
    SELECT SCOPE_IDENTITY() AS NuevoFavoritoId;
END
GO
