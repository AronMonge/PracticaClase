CREATE TABLE [dbo].[ListaVisualizaciones] (
    [ListaId]       INT      IDENTITY (1, 1) NOT NULL,
    [UsuarioId]     INT      NOT NULL,
    [IdApi]         INT      NOT NULL,
    [Prioridad]     INT      DEFAULT ((0)) NOT NULL,
    [FechaAgregado] DATETIME DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([ListaId] ASC),
    CONSTRAINT [FK_ListaVisualizaciones_Usuarios] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuarios] ([UsuarioId]) ON DELETE CASCADE
);

