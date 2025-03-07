using Npgsql;
using System;
using System.Data;
using MTCG.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

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
                "VALUES (@name, @password, @coins, @elo) RETURNING id";
            AddParameterWithValue(command, "name", DbType.String, user.username);
            AddParameterWithValue(command, "password", DbType.String, user.password);
            AddParameterWithValue(command, "coins", DbType.Int32, user.coins);
            AddParameterWithValue(command, "elo", DbType.Int32, user.ELO);
            user.id = (int)(command.ExecuteScalar() ?? 0);
        }
        public IEnumerable<user> GetScore()
        {
            List<user> result = null;

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = @"SELECT name, elo FROM users";

            using (IDataReader reader = command.ExecuteReader())
                while (reader.Read())
                {
                    result.Add(new user()
                    {
                        username = reader.GetString(1),
                        ELO = reader.GetInt32(289)
                    });
                }
            return result;
        }

        public user? GetByUsername(string name)
        {
            if (name == null)
                throw new ArgumentException("Username must not be null");

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = @"SELECT id, name, password, coins, elo, battles FROM users WHERE name=@name";
            AddParameterWithValue(command, "name", DbType.String, name);

            using IDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new user()
                {
                    id = reader.GetInt32(0),
                    username = reader.GetString(1),
                    password = reader.GetString(1),
                    coins = reader.GetInt32(2),
                    ELO = reader.GetInt32(2),
                    battlesFought = reader.GetInt32(2)
                };
            }
            return null;
        }

        public void UpdateUserInfo(user user, string name, string bio, string image)
        {
            if (user.id == null)
                throw new ArgumentException("Id must not be null");

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "UPDATE users SET name=@name, bio=@bio, image=@image FROM users WHERE id=@id";
            AddParameterWithValue(command, "name", DbType.String, name);
            AddParameterWithValue(command, "bio", DbType.String, bio);
            AddParameterWithValue(command, "image", DbType.String, image);
            AddParameterWithValue(command, "id", DbType.Int32, user.id);
            command.ExecuteNonQuery();
        }

        public void UpdateCoins(user user)
        {
            if (user.id == null)
                throw new ArgumentException("Id must not be null");

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "UPDATE users SET coins=@coinsFROM users WHERE id=@id";
            AddParameterWithValue(command, "coins", DbType.Int32, user.coins);
            AddParameterWithValue(command, "id", DbType.Int32, user.id);
            command.ExecuteNonQuery();
        }

        public void Delete(user user)
        {
            if (user.id == null)
                throw new ArgumentException("Id must not be null");

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "DELETE FROM users WHERE id=@id";
            AddParameterWithValue(command, "id", DbType.Int32, user.id);
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
