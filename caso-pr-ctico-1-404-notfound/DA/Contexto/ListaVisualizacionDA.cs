using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DA.Contexto
{
    public class ListaVisualizacionDA : IListaVisualizacionDA
    {
        private readonly string _conn;
        public ListaVisualizacionDA(IConfiguration cfg)
            => _conn = cfg.GetConnectionString("DefaultConnection")!;

        public async Task<IEnumerable<ListaVisualizacion>> ObtenerPorUsuario(int usuarioId)
        {
            using var db = DbConnectionFactory.Create(_conn);
            var p = new DynamicParameters();
            p.Add("@UsuarioId", usuarioId, DbType.Int32);

            return await db.QueryAsync<ListaVisualizacion>(
                "sp_ObtenerListaPorUsuario",
                p,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<ListaVisualizacion?> ObtenerDetalle(int usuarioId, int listaId)
        {
            using var db = DbConnectionFactory.Create(_conn);
            var sql = @"
                SELECT ListaId, UsuarioId, Tipo, Titulo, Detalle, FechaAgregado
                  FROM dbo.ListaVisualizacion
                 WHERE UsuarioId = @UsuarioId
                   AND ListaId   = @ListaId";
            return await db.QuerySingleOrDefaultAsync<ListaVisualizacion>(
                sql,
                new { UsuarioId = usuarioId, ListaId = listaId }
            );
        }

        public async Task<int> Crear(ListaVisualizacion lista)
        {
            using var db = DbConnectionFactory.Create(_conn);
            var p = new DynamicParameters();
            p.Add("@UsuarioId", lista.UsuarioId, DbType.Int32);
            p.Add("@Tipo", lista.Tipo, DbType.String);
            p.Add("@Titulo", lista.Titulo, DbType.String);
            p.Add("@Detalle", lista.Detalle, DbType.String);
            p.Add("@FechaAgregado", lista.FechaAgregado, DbType.DateTime);

            return await db.ExecuteScalarAsync<int>(
                "sp_InsertarListaVisualizacion",
                p,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> Editar(int usuarioId, int listaId, ListaVisualizacion lista)
        {
            using var db = DbConnectionFactory.Create(_conn);
            var p = new DynamicParameters();
            p.Add("@ListaId", listaId, DbType.Int32);
            p.Add("@Tipo", lista.Tipo, DbType.String);
            p.Add("@Titulo", lista.Titulo, DbType.String);
            p.Add("@Detalle", lista.Detalle, DbType.String);

            var affected = await db.ExecuteScalarAsync<int>(
                "sp_ActualizarListaVisualizacion",
                p,
                commandType: CommandType.StoredProcedure
            );
            return affected > 0;
        }

        public async Task<bool> Eliminar(int usuarioId, int listaId)
        {
            using var db = DbConnectionFactory.Create(_conn);
            var p = new DynamicParameters();
            p.Add("@UsuarioId", usuarioId, DbType.Int32);
            p.Add("@ListaId", listaId, DbType.Int32);

            var affected = await db.ExecuteScalarAsync<int>(
                "sp_EliminarListaVisualizacion",
                p,
                commandType: CommandType.StoredProcedure
            );
            return affected > 0;
        }
    }
}

