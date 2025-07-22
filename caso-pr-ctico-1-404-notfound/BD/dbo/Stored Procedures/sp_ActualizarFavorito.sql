
CREATE PROCEDURE dbo.sp_ActualizarFavorito
  @FavoritoId         INT,
  @Tipo               NVARCHAR(20),
  @Titulo             NVARCHAR(200),
  @Comentario         NVARCHAR(500) = NULL,
  @CalificacionUsuario BIT          = NULL
AS
BEGIN
  SET NOCOUNT ON;

  UPDATE dbo.Favoritos
  SET
    Tipo               = @Tipo,
    Titulo             = @Titulo,
    Comentario         = @Comentario,
    CalificacionUsuario= @CalificacionUsuario
  WHERE FavoritoId = @FavoritoId;

  SELECT @@ROWCOUNT AS FilasAfectadas;
END