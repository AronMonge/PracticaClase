using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DA.Contexto
{
    public class FavoritoDA : IFavoritoDA
    {
        private readonly string _conn;
        public FavoritoDA(IConfiguration cfg)
            => _conn = cfg.GetConnectionString("DefaultConnection")!;

        public async Task<IEnumerable<Favorito>> ObtenerPorUsuario(int usuarioId)
        {
            using var db = DbConnectionFactory.Create(_conn);
            var p = new DynamicParameters();
            p.Add("@UsuarioId", usuarioId, DbType.Int32);

            return await db.QueryAsync<Favorito>(
                "sp_ObtenerFavoritosPorUsuario",
                p,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Favorito> ObtenerPorId(int usuarioId, int favoritoId)
        {
            using var db = DbConnectionFactory.Create(_conn);
            var fav = await db.QuerySingleOrDefaultAsync<Favorito>(
                "sp_ObtenerFavoritoPorId",
                new { FavoritoId = favoritoId },
                commandType: CommandType.StoredProcedure
            );
            return fav!;
        }

        public async Task<int> Crear(Favorito f)
        {
            using var db = DbConnectionFactory.Create(_conn);
            var p = new DynamicParameters();
            p.Add("@UsuarioId", f.UsuarioId, DbType.Int32);
            p.Add("@Tipo", f.Tipo, DbType.String);
            p.Add("@Titulo", f.Titulo, DbType.String);
            p.Add("@Comentario", f.Comentario, DbType.String);
            p.Add("@CalificacionUsuario", f.CalificacionUsuario, DbType.Boolean);
            p.Add("@FechaFavorito", f.FechaFavorito, DbType.DateTime);

            return await db.ExecuteScalarAsync<int>(
                "sp_InsertarFavorito",
                p,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> Actualizar(int usuarioId, int favoritoId, Favorito f)
        {
            using var db = DbConnectionFactory.Create(_conn);
            var p = new DynamicParameters();
            p.Add("@FavoritoId", favoritoId, DbType.Int32);
            p.Add("@Tipo", f.Tipo, DbType.String);
            p.Add("@Titulo", f.Titulo, DbType.String);
            p.Add("@Comentario", f.Comentario, DbType.String);
            p.Add("@CalificacionUsuario", f.CalificacionUsuario, DbType.Boolean);

            var filas = await db.ExecuteScalarAsync<int>(
                "sp_ActualizarFavorito",
                p,
                commandType: CommandType.StoredProcedure
            );
            return filas > 0;
        }

        public async Task<bool> Eliminar(int usuarioId, int favoritoId)
        {
            using var db = DbConnectionFactory.Create(_conn);
            var filas = await db.ExecuteScalarAsync<int>(
                "sp_EliminarFavorito",
                new { FavoritoId = favoritoId },
                commandType: CommandType.StoredProcedure
            );
            return filas > 0;
        }
    }
}

