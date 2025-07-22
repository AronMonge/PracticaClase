CREATE PROCEDURE sp_EliminarFavorito
    @UsuarioId  INT,
    @FavoritoId INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Favoritos
    WHERE UsuarioId = @UsuarioId
      AND FavoritoId = @FavoritoId;

    SELECT @@ROWCOUNT AS FilasAfectadas;
END
GO
