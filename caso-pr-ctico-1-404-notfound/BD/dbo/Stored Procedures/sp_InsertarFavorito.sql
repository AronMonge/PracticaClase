
CREATE PROCEDURE dbo.sp_InsertarFavorito
  @UsuarioId          INT,
  @Tipo               NVARCHAR(20),
  @Titulo             NVARCHAR(200),
  @Comentario         NVARCHAR(500) = NULL,
  @CalificacionUsuario BIT          = NULL,
  @FechaFavorito      DATETIME      = NULL
AS
BEGIN
  SET NOCOUNT ON;

  IF @FechaFavorito IS NULL
    SET @FechaFavorito = GETDATE();

  INSERT INTO dbo.Favoritos
    (UsuarioId, Tipo, Titulo, Comentario, CalificacionUsuario, FechaFavorito)
  VALUES
    (@UsuarioId, @Tipo, @Titulo, @Comentario, @CalificacionUsuario, @FechaFavorito);

  -- Devuelve el nuevo FavoritoId
  SELECT CAST(SCOPE_IDENTITY() AS INT) AS FavoritoId;
END