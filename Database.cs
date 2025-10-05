using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MySqlConnector;

namespace ServerManager
{
    public static class Database
    {
        private static readonly Dictionary<string, string> _schemas = new();

        public sealed class SchemaTestResult
        {
            public string Schema { get; init; } = "";
            public bool Ok { get; init; }
            public string Message { get; init; } = "";
            public int? ErrorCode { get; init; } // MySQL error number (e.g., 1045)
        }

        public static void ConfigureAll(AppSettings.SqlSettings s)
        {
            _schemas.Clear();
            AddSchema("mm_device_db", s);
            AddSchema("mm_front_db", s);
            AddSchema("mm_game_db_release", s);
            AddSchema("mm_user_db", s);
        }

        private static void AddSchema(string schema, AppSettings.SqlSettings s)
        {
            var csb = new MySqlConnectionStringBuilder
            {
                Server = s.Ip,
                Port = (uint)s.Port,
                UserID = s.Username,
                Password = s.Password,
                Database = schema,
                Pooling = true,
                ConnectionTimeout = 5, // quick fail
                DefaultCommandTimeout = 10
            };
            _schemas[schema] = csb.ConnectionString;
        }

        public static MySqlConnection CreateConnection(string schema)
        {
            if (!_schemas.TryGetValue(schema, out var cs))
                throw new InvalidOperationException($"Schema not configured: {schema}");
            return new MySqlConnection(cs);
        }

        public static async Task<SchemaTestResult> TestAsync(string schema, CancellationToken ct = default)
        {
            try
            {
                await using var conn = CreateConnection(schema);
                await conn.OpenAsync(ct).ConfigureAwait(false);
                await using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT 1";
                _ = await cmd.ExecuteScalarAsync(ct).ConfigureAwait(false);
                return new SchemaTestResult { Schema = schema, Ok = true, Message = "OK" };
            }
            catch (MySqlException mex)
            {
                return new SchemaTestResult
                {
                    Schema = schema,
                    Ok = false,
                    ErrorCode = mex.Number,
                    Message = MapMySqlError(mex)
                };
            }
            catch (Exception ex)
            {
                return new SchemaTestResult
                {
                    Schema = schema,
                    Ok = false,
                    Message = $"Unexpected error: {ex.Message}"
                };
            }
        }

        private static string MapMySqlError(MySqlException ex)
        {
            // Common MySQL error codes:
            // 1045: Access denied (bad username/password)
            // 1042: Bad host / Can't connect to MySQL server
            // 1049: Unknown database
            // 2002/2003: Can't connect (timeout / refused)
            // 2058: Authentication plugin error (e.g., caching_sha2)
            return ex.Number switch
            {
                1045 => "Access denied: check username/password in settings.json.",
                1042 => "Cannot connect: check server IP/host and port.",
                1049 => "Unknown database: schema doesn't exist on the server.",
                2002 => "Can't connect: server refused/timeout (port/firewall?).",
                2003 => "Can't connect: server unreachable (IP/port?).",
                2058 => "Auth plugin issue (e.g., caching_sha2). Check server auth settings.",
                _ => $"MySQL error {ex.Number}: {ex.Message}"
            };
        }

        public static async Task<DataTable> QueryAsync(string schema, string sql, object? parameters = null, CancellationToken ct = default)
        {
            var dt = new DataTable();
            await using var conn = CreateConnection(schema);
            await conn.OpenAsync(ct).ConfigureAwait(false);
            await using var cmd = BuildCommand(conn, sql, parameters);
            await using var r = await cmd.ExecuteReaderAsync(ct).ConfigureAwait(false);
            dt.Load(r);
            return dt;
        }

        private static MySqlCommand BuildCommand(MySqlConnection conn, string sql, object? parameters)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            if (parameters != null)
            {
                foreach (var p in parameters.GetType().GetProperties())
                {
                    var name = p.Name.StartsWith("@") ? p.Name : "@" + p.Name;
                    cmd.Parameters.AddWithValue(name, p.GetValue(parameters) ?? DBNull.Value);
                }
            }
            return cmd;
        }
    }
}
