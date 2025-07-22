CREATE TABLE [dbo].[Usuarios] (
    [UsuarioId]     INT            IDENTITY (1, 1) NOT NULL,
    [Rol]           NVARCHAR (20)  NOT NULL,
    [NombreUsuario] NVARCHAR (50)  NOT NULL,
    [Email]         NVARCHAR (100) NOT NULL,
    [Contrasena]    NVARCHAR (100) NOT NULL,
    [FechaRegistro] DATETIME       DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([UsuarioId] ASC)
);

