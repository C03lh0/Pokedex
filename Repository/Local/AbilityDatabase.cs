using Microsoft.Data.Sqlite;
using Model.Model;
using Model.Model.Ability_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Local
{
    public class AbilityDatabase
    {
        public static string PathDatabase { get; set; }

        public static void InsertPokemonAbility(int pokemonID, string ability)
        {
            using (SqliteConnection connection = new SqliteConnection($"Filename={PathDatabase}"))
            {
                connection.Open();

                SqliteCommand insertCommand = new SqliteCommand("INSERT INTO ability_tb (pokemon_id, ability) VALUES (@pokemon_id, @ability)", connection);
                insertCommand.Parameters.AddWithValue("@pokemon_id", pokemonID);
                insertCommand.Parameters.AddWithValue("@ability", ability);
                insertCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        public static List<AbilityWrapper> FindPokemonAbilityByID(int ID)
        {
            using (SqliteConnection connection = new SqliteConnection($"Filename={PathDatabase}"))
            {
                connection.Open();

                SqliteCommand selectCommand = new SqliteCommand("SELECT ability FROM ability_tb WHERE pokemon_id = @value", connection);
                selectCommand.Parameters.AddWithValue("@value", ID);
                SqliteDataReader queryResponse = selectCommand.ExecuteReader();

                List<AbilityWrapper> pokemonAbilities = new List<AbilityWrapper>();

                while (queryResponse.Read())
                {
                    pokemonAbilities.Add(new AbilityWrapper { Ability = new Ability(queryResponse.GetString(0)) });
                }

                queryResponse.Close();
                return pokemonAbilities;
            }
        }
    }
}
