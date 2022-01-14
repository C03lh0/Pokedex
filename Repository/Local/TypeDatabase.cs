using Microsoft.Data.Sqlite;
using Model.Model.Type_Classes;
using System.Collections.Generic;
using Type = Model.Model.Type_Classes.Type;

namespace Repository.Local
{
    public class TypeDatabase
    {
        public static string PathDatabase { get; set; }

        public static void InsertPokemonType(int PokemonID, string Type)
        {
            using (SqliteConnection connection = new SqliteConnection($"Filename={PathDatabase}"))
            {
                connection.Open();

                SqliteCommand insertCommand = new SqliteCommand("INSERT INTO type_tb (pokemon_id, type) VALUES (@pokemon_id, @type)", connection);
                insertCommand.Parameters.AddWithValue("@pokemon_id", PokemonID);
                insertCommand.Parameters.AddWithValue("@type", Type);
                insertCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        public static List<TypeWrapper> FindPokemonTypeByID(int ID)
        {
            using (SqliteConnection connection = new SqliteConnection($"Filename={PathDatabase}"))
            {
                connection.Open();

                SqliteCommand selectCommand = new SqliteCommand("SELECT type FROM type_tb WHERE pokemon_id = @value", connection);
                selectCommand.Parameters.AddWithValue("@value", ID);
                SqliteDataReader queryResponse = selectCommand.ExecuteReader();

                List<TypeWrapper> pokemonTypes = new List<TypeWrapper>();

                while (queryResponse.Read())
                {
                    pokemonTypes.Add(new TypeWrapper { Type = new Type(queryResponse.GetString(0)) });
                }

                queryResponse.Close();
                return pokemonTypes;
            }
        }
    }
}
