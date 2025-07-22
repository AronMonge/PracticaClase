
CREATE PROCEDURE dbo.sp_ObtenerFavoritosPorUsuario
  @UsuarioId INT
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
  WHERE UsuarioId = @UsuarioId
  ORDER BY FechaFavorito DESC;
END