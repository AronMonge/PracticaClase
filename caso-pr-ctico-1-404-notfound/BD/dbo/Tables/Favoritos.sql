CREATE TABLE [dbo].[Favoritos] (
    [FavoritoId]          INT            IDENTITY (1, 1) NOT NULL,
    [UsuarioId]           INT            NOT NULL,
    [Tipo]                NVARCHAR (20)  CONSTRAINT [DF_Favoritos_Tipo] DEFAULT ('pelicula') NOT NULL,
    [Titulo]              NVARCHAR (200) NOT NULL,
    [Comentario]          NVARCHAR (500) NULL,
    [CalificacionUsuario] BIT            NULL,
    [FechaFavorito]       DATETIME       CONSTRAINT [DF_Favoritos_Fecha] DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([FavoritoId] ASC),
    CONSTRAINT [FK_Favoritos_Usuarios] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuarios] ([UsuarioId]) ON DELETE CASCADE
);

