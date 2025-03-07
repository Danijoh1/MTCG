using Npgsql;
using System;
using System.Data;
using MTCG.Models;

namespace MTCG.Repositories
{
    public class CardRepository
    {
        public CardRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        private readonly string connectionString;

        public void Add(card card, packages package)
        {
            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();

            command.CommandText = "INSERT INTO cards (id, name, damage, pid ,deck) " +
                "VALUES (@id, @name, @damage, @pid, @deck)";
            AddParameterWithValue(command, "id", DbType.String, card.id);
            AddParameterWithValue(command, "name", DbType.String, card.name);
            AddParameterWithValue(command, "damage", DbType.String, card.damage);
            AddParameterWithValue(command, "pid", DbType.Int32, package.id);
            AddParameterWithValue(command, "deck", DbType.Boolean, false);
        }
        public List<card> GetStackOfUser(user user)
        {
            List<card> stack = null;

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = @"SELECT id, name,damage FROM cards WHERE uid=@uid";
            AddParameterWithValue(command, "uid", DbType.Int32, user.id);
            command.ExecuteNonQuery();

            using (IDataReader reader = command.ExecuteReader())
                while (reader.Read())
                {
                    stack.Add(new card()
                    {
                        id = reader.GetString(1),
                        name = reader.GetString(1),
                        damage = reader.GetInt32(0),
                    });
                }
            return stack;
        }

        public card GetCardById(string id)
        {
            card card = new card();
            card.id = id;

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = @"SELECT name,damage FROM cards WHERE id=@id";
            AddParameterWithValue(command, "id", DbType.Int32, id);
            command.ExecuteNonQuery();

            using (IDataReader reader = command.ExecuteReader())
            while (reader.Read())
            {
                card.name = reader.GetString(1);
                card.damage = reader.GetInt32(0);
            }
            return card;
        }

        public void AddOwner(user user, packages package)
        {
            if (package.id == null)
                throw new ArgumentException("Id must not be null");

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "UPDATE cards SET @uid=uid WHERE pid=@pid";
            AddParameterWithValue(command, "pid", DbType.Int32, package.id);
            AddParameterWithValue(command, "uid", DbType.Int32, user.id);
            command.ExecuteNonQuery();
        }

        public List<card> GetDeck(user user)
        {
            List<card> stack = null;

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = @"SELECT id, name,damage FROM cards WHERE uid=@uid and deck=true";
            AddParameterWithValue(command, "uid", DbType.Int32, user.id);
            command.ExecuteNonQuery();

            using (IDataReader reader = command.ExecuteReader())
                while (reader.Read())
                {
                    stack.Add(new card()
                    {
                        id = reader.GetString(1),
                        name = reader.GetString(1),
                        damage = reader.GetInt32(0),
                    });
                }
            return stack;
        }

        public void AddToDeck(user user, card card)
        {
            if (user.id == null)
                throw new ArgumentException("Id must not be null");

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "UPDATE cards SET @deck=deck WHERE uid=@uid";
            AddParameterWithValue(command, "uid", DbType.Int32, user.id);
            AddParameterWithValue(command, "uid", DbType.String, card.id);
            AddParameterWithValue(command, "deck", DbType.Boolean, true);
            command.ExecuteNonQuery();
        }

        public void Delete(card card)
        {
            if (card.id == null)
                throw new ArgumentException("Id must not be null");

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "DELETE FROM cards WHERE id=@id";
            AddParameterWithValue(command, "id", DbType.String, card.id);
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
