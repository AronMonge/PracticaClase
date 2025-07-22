
CREATE PROCEDURE dbo.sp_ObtenerFavoritoPorId
  @FavoritoId INT
AS
BEGIN
  SET NOCOUNT ON;

  SELECT
    FavoritoId,
    UsuarioId,
    Tipo,
    Titulo,
    Comentario,
    CalificacionUsuario,
    FechaFavorito
  FROM dbo.Favoritos
  WHERE FavoritoId = @FavoritoId;
END