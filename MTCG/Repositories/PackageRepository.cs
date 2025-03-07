using Npgsql;
using System;
using System.Data;
using MTCG.Models;

namespace MTCG.Repositories
{
    public class PackageRepository
    {
        public PackageRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        private readonly string connectionString;

        public packages Add()
        {
            packages package = new packages();
            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();

            command.CommandText = "INSERT INTO packages (isSold) Values(@isSold) RETURNING id";
            AddParameterWithValue(command, "isSold", DbType.Boolean, false);
            package.id = (int)(command.ExecuteScalar() ?? 0);
            package.isSold = false;
            return package;
        }

        public packages GetUnsoldPackage()
        {
            packages package = new packages();

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = @"SELECT FIRST id FROM packages WHERE isSold = false";
            command.ExecuteNonQuery();

            using (IDataReader reader = command.ExecuteReader())
                while (reader.Read())
                {
                    package.isSold = reader.GetBoolean(1);
                    package.id = reader.GetInt32(0);
                }
            return package;
        }

        public void SellPackage(packages package)
        {
            if (package == null)
                throw new ArgumentException("Username must not be null");

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "UPDATE packages SET isSold=@isSold WHERE id=@id";
            AddParameterWithValue(command, "isSold", DbType.Boolean, true);
            AddParameterWithValue(command, "id", DbType.Int32, package.id);
            command.ExecuteNonQuery();
        }

        public void Delete(packages package)
        {
            if (package.id == null)
                throw new ArgumentException("Id must not be null");

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "DELETE FROM packages WHERE id=@id";
            AddParameterWithValue(command, "id", DbType.Int32, package.id);
            command.ExecuteNonQuery();
        }


        public static void AddParameterWithValue(IDbCommand command, string parameterName, DbType type, object value)
        {
            var parameter = command.CreateParameter();
            parameter.DbType = type;
            parameter.ParameterName = parameterName;
            parameter.Value = value ?? DBNull.Value;
            command.Parameters.Add(parameter);
        }
    }
}
