/*
using Npgsql;
using System;
using System.Data;
using MTCG.Models;

namespace MTCG.Repositories
{
    public class UserRepository
    {
        public UserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        private readonly string connectionString;

        public void Add(user user )
        {
            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();

            command.CommandText = "INSERT INTO users (name,password, coins, elo) " +
                "VALUES (@name, @age, @description) RETURNING id";
            AddParameterWithValue(command, "name", DbType.String, user.Username);
            AddParameterWithValue(command, "password", DbType.String, user.Username);
            AddParameterWithValue(command, "coins", DbType.Int32, user.coins);
            AddParameterWithValue(command, "elo", DbType.Int32, user.ELO);
            user.Id = (int)(command.ExecuteScalar() ?? 0);
        }
        public IEnumerable<user> GetAll()
        {
            List<user> result = null;

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = @"SELECT id, name, password, coins, elo FROM users";

            using (IDataReader reader = command.ExecuteReader())
                while (reader.Read())
                {
                    result.Add(new user()
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        coins = reader.GetInt32(2),
                        ELO = reader.GetInt32(2)
                    });
                }
            return result;
        }

        public user? GetById(int? id)
        {
            if (id == null)
                throw new ArgumentException("Id must not be null");

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = @"SELECT id, name, password, coins, elo FROM users WHERE id=@id";
            AddParameterWithValue(command, "id", DbType.Int32, id);

            using IDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new user()
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    coins = reader.GetInt32(2),
                    ELO = reader.GetInt32(2)
                };
            }
            return null;
        }

        public void Update(user user)
        {
            if (user.Id == null)
                throw new ArgumentException("Id must not be null");

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "UPDATE person SET name=@name, password=@password, coins=@coins, elo=@elo WHERE id=@id";
            AddParameterWithValue(command, "id", DbType.Int32, user.Id);
            AddParameterWithValue(command, "name", DbType.String, user.Username);
            AddParameterWithValue(command, "coins", DbType.Int32, user.coins);
            AddParameterWithValue(command, "elo", DbType.Int32, user.ELO);
            command.ExecuteNonQuery();
        }

        public void Delete(user user)
        {
            if (user.Id == null)
                throw new ArgumentException("Id must not be null");

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "DELETE FROM users WHERE id=@id";
            AddParameterWithValue(command, "id", DbType.Int32, user.Id);
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
*/