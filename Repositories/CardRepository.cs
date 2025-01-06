using MTCG.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Repositories
{
    public class CardRepository
    {
        public CardRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        private readonly string connectionString;

        public void Add(card card)
        {
            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();

            command.CommandText = "INSERT INTO packages (cardId,name,damage) " +
                "VALUES (@cardId, @name, @damage) RETURNING id";
            AddParameterWithValue(command, "cardId", DbType.String, card.id);
            AddParameterWithValue(command, "name", DbType.String, card.name);
            AddParameterWithValue(command, "damage", DbType.Decimal, card.damage);
            card.dbId = (int)(command.ExecuteScalar() ?? 0);
        }
        public IEnumerable<card> GetAll()
        {
            List<card> result = null;

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = @"SELECT id, cardId,name,damage FROM card";

            using (IDataReader reader = command.ExecuteReader())
                while (reader.Read())
                {
                    result.Add(new card()
                    {
                        dbId = reader.GetInt32(0),
                        id = reader.GetString(1),
                        name = reader.GetString(2),
                        damage = (float)reader.GetDecimal(1),
                    });
                }
            return result;
        }

        public card? GetById(int? dbId)
        {
            if (dbId == null)
                throw new ArgumentException("Id must not be null");

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = @"SELECT id, cardId,name,damage FROM card WHERE id=@id";
            AddParameterWithValue(command, "id", DbType.Int32, dbId);

            using IDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new card()
                {
                    dbId = reader.GetInt32(0),
                    id = reader.GetString(1),
                    name = reader.GetString(2),
                    damage = (float)reader.GetDecimal(1),
                };
            }
            return null;
        }

        public void Update(card card)
        {
            if (card.dbId == null)
                throw new ArgumentException("Id must not be null");

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "UPDATE person SET @cardId=cardId,@name=name, @damage=damage WHERE id=@id";
            AddParameterWithValue(command, "id", DbType.Int32, card.dbId);
            AddParameterWithValue(command, "cardId", DbType.String, card.id);
            AddParameterWithValue(command, "name", DbType.String, card.name);
            AddParameterWithValue(command, "damage", DbType.Decimal, card.damage);
            command.ExecuteNonQuery();
        }

        public void Delete(card card)
        {
            if (card.dbId == null)
                throw new ArgumentException("Id must not be null");

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "DELETE FROM card WHERE id=@id";
            AddParameterWithValue(command, "id", DbType.Int32, card.dbId);
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
