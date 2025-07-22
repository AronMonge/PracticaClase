using Microsoft.Data.SqlClient;
using System.Data;

namespace DA.Contexto
{
    public static class DbConnectionFactory
    {
        public static IDbConnection Create(string connString)
            => new SqlConnection(connString);
    }
}
