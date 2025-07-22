
CREATE PROCEDURE dbo.sp_EliminarFavorito
  @FavoritoId INT
AS
BEGIN
  SET NOCOUNT ON;

  DELETE FROM dbo.Favoritos
  WHERE FavoritoId = @FavoritoId;

  SELECT @@ROWCOUNT AS FilasEliminadas;
END