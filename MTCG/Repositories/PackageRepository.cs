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

        public void Add(packages package )
        {
            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();

            command.CommandText = "INSERT INTO packages (cardid1,cardid2, cardid3, cardid4,cardid5) " +
                "VALUES (@cardid1, @cardid2, @cardid3, @cardid4, @cardid5) RETURNING id";
            AddParameterWithValue(command, "cardid1", DbType.String, package.cardId1);
            AddParameterWithValue(command, "cardid2", DbType.String, package.cardId2);
            AddParameterWithValue(command, "cardid3", DbType.String, package.cardId3);
            AddParameterWithValue(command, "cardid4", DbType.String, package.cardId4);
            AddParameterWithValue(command, "cardid5", DbType.String, package.cardId5);
            package.Id = (int)(command.ExecuteScalar() ?? 0);
        }
        public IEnumerable<packages> GetAll()
        {
            List<packages> result = null;

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = @"SELECT id, cardid1,cardid2, cardid3, cardid4,cardid5 FROM packages";

            using (IDataReader reader = command.ExecuteReader())
                while (reader.Read())
                {
                    result.Add(new packages()
                    {
                        Id = reader.GetInt32(0),
                        cardId1 = reader.GetString(1),
                        cardId2 = reader.GetString(1),
                        cardId3 = reader.GetString(1),
                        cardId4 = reader.GetString(1),
                        cardId5 = reader.GetString(1),

                    });
                }
            return result;
        }

        public packages? GetById(int? id)
        {
            if (id == null)
                throw new ArgumentException("Id must not be null");

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = @"SELECT id, cardid1,cardid2, cardid3, cardid4,cardid5 FROM packages WHERE id=@id";
            AddParameterWithValue(command, "id", DbType.Int32, id);

            using IDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new packages()
                {
                    Id = reader.GetInt32(0),
                    cardId1 = reader.GetString(1),
                    cardId2 = reader.GetString(1),
                    cardId3 = reader.GetString(1),
                    cardId4 = reader.GetString(1),
                    cardId5 = reader.GetString(1)
                };
            }
            return null;
        }

        public void Update(packages package)
        {
            if (package.Id == null)
                throw new ArgumentException("Id must not be null");

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "UPDATE person SET @cardid1=cardid1,@cardid2=cardid2, @cardid3=cardid3, @cardid4=cardid4,@cardid5=cardid5 WHERE id=@id";
            AddParameterWithValue(command, "id", DbType.Int32, package.Id);
            AddParameterWithValue(command, "cardid1", DbType.String, package.cardId1);
            AddParameterWithValue(command, "cardid2", DbType.String, package.cardId2);
            AddParameterWithValue(command, "cardid3", DbType.String, package.cardId3);
            AddParameterWithValue(command, "cardid4", DbType.String, package.cardId4);
            AddParameterWithValue(command, "cardid5", DbType.String, package.cardId5);
            command.ExecuteNonQuery();
        }

        public void Delete(packages package)
        {
            if (package.Id == null)
                throw new ArgumentException("Id must not be null");

            using IDbConnection connection = new NpgsqlConnection(connectionString);
            using IDbCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "DELETE FROM packages WHERE id=@id";
            AddParameterWithValue(command, "id", DbType.Int32, package.Id);
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
