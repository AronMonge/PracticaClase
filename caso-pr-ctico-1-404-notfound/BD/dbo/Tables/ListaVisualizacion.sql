CREATE TABLE [dbo].[ListaVisualizacion] (
    [ListaId]       INT            IDENTITY (1, 1) NOT NULL,
    [UsuarioId]     INT            NOT NULL,
    [Tipo]          NVARCHAR (50)  NOT NULL,
    [Titulo]        NVARCHAR (255) NOT NULL,
    [Detalle]       NVARCHAR (500) NULL,
    [FechaAgregado] DATETIME       DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([ListaId] ASC),
    CONSTRAINT [FK_Lista_Usuarios] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuarios] ([UsuarioId]) ON DELETE CASCADE
);

