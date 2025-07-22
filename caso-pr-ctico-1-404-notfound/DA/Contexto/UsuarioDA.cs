using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DA.Contexto
{
    public class UsuarioDA : IUsuarioDA
    {
        private readonly string _conn;
        public UsuarioDA(IConfiguration cfg) => _conn = cfg.GetConnectionString("DefaultConnection")!;

        public async Task<IEnumerable<Usuario>> ObtenerTodos()
        {
             using var db = DbConnectionFactory.Create(_conn);
            return await db.QueryAsync<Usuario>(
                @"SELECT UsuarioId, Rol, NombreUsuario, Email, Contrasena, FechaRegistro
                  FROM Usuarios");
        }

        public async Task<Usuario?> ObtenerPorId(int id)
        {
             using var db = DbConnectionFactory.Create(_conn);
            return await db.QuerySingleOrDefaultAsync<Usuario>(
                @"SELECT UsuarioId, Rol, NombreUsuario, Email, Contrasena, FechaRegistro
                  FROM Usuarios
                  WHERE UsuarioId = @Id", new { Id = id });
        }

        public async Task<int> Crear(Usuario u)
        {
            const string sql = @"
                INSERT INTO Usuarios
                    (Rol, NombreUsuario, Email, Contrasena, FechaRegistro)
                OUTPUT INSERTED.UsuarioId
                VALUES (@Rol, @NombreUsuario, @Email, @Contrasena, @FechaRegistro)";
             using var db = DbConnectionFactory.Create(_conn);
            return await db.QuerySingleAsync<int>(sql, u);
        }

        public async Task<bool> Eliminar(int id)
        {
             using var db = DbConnectionFactory.Create(_conn);
            var filas = await db.ExecuteAsync(
                "DELETE FROM Usuarios WHERE UsuarioId = @Id", new { Id = id });
            return filas > 0;
        }
    }
}
